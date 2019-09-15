using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCatalog
{
    class Program
    {
        struct Car
        {
            public int Id;
            public string Enterprice;
            public string Model;
            public int Year;
            public string Fuel;
            public double EngineVolume;
            public List<int> SimilarCars;
        }
        static void Main(string[] args)
        {
            Console.Clear();

            List<Car> cars = new List<Car>();
            fillAutoCatalog(cars);

            string menuPoint = "";

            while (menuPoint != "9")
            {
                while (true)
                {
                    Console.Clear();
                    Console.Write(@"Меню:
1 - Посмотреть список автомобилей
9 - Выход

Введите пункт меню: ");
                    menuPoint = Console.ReadLine();
                    if (menuPoint != "1" && menuPoint != "9")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Введите корректный пункт меню.");
                        Console.ResetColor();
                        Console.WriteLine("Нажмите любую клавишу, чтобы ввести заново.");
                        Console.ReadKey();
                    }
                    else
                    {
                        break;
                    }
                }
                int carId = 0;
                while (menuPoint != "9" && carId != -1)
                {
                    Console.Clear();
                    string title = "| Список всех автомобилей |";
                    string line = new string('-', title.Length);
                    Console.WriteLine(line);
                    Console.WriteLine(title);

                    printCarsView(cars, cars);
                    Console.Write("Введите id машины или введите -1 чтобы выйти в меню: ");
                    carId = Convert.ToInt32(Console.ReadLine());

                    if (carId != -1)
                    {
                        Car? findCar = getCarById(cars, carId);

                        if (findCar.HasValue)
                        {
                            printCarView(findCar.Value);
                            List<Car> similarCars = getCarsByIds(cars, findCar.Value.SimilarCars);
                            if (similarCars.Count > 0)
                            {
                                printCarsView(similarCars, cars);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Данного автомобиля нет в каталоге.");
                        }

                        Console.WriteLine("Нажмите любую клавишу, чтобы вернуться к списку автомобилей...");
                        Console.ReadKey();
                    }
                }
            }
        }
        static List<Car> getCarsByIds(List<Car> cars, List<int> ids)
        {
            List<Car> findCars = new List<Car>();

            for (int i = 0; i < ids.Count; i++)
            {
                Car? item = getCarById(cars, ids[i]);

                if (item.HasValue)
                {
                    findCars.Add(item.Value);
                }
            }

            return findCars;
        }
        static string getCarsNameByIds(List<Car> cars, List<int> ids)
        {
            string carsNames = "";
            for (int i = 0; i < cars.Count; i++)
            {
                bool isContaint = ids.Contains(cars[i].Id);

                if (!isContaint)
                {
                    continue;
                }

                if (carsNames == "")
                {
                    carsNames += cars[i].Enterprice + " " + cars[i].Model;
                }
                else
                {
                    carsNames += ", " + cars[i].Enterprice + " " + cars[i].Model;
                }
            }
            return carsNames;
        }
        static Car? getCarById(List<Car> cars, int id)
        {
            Car? resultCar = null;
            for (int i = 0; i < cars.Count; i++)
            {
                if (cars[i].Id == id)
                {
                    resultCar = cars[i];
                    break;
                }
            }
            return resultCar;
        }
        static void printCarView(Car item)
        {
            string line = new string('-', 74);

            Console.WriteLine($@"{line}
| {"id",20} | {item.Id,-47} |
{line}
| {"Наименование",20} | {item.Enterprice + " " + item.Model + " " + item.Year,-47} |
{line}
| {"Топливо",20} | {convertFuel(item.Fuel),-47} |
{line}
| {"Объём двигателя",20} | {item.EngineVolume,-47} |
{line}");
        }
        static void printCarsView (List<Car> carsList, List<Car> generalCarsList)
        {
            string title = $"| {"Id",3} | {"Производитель",17} | {"Модель",10} | {"Год",5} | {"Топливо",8} | {"V двигателя",12} | {"Похожие автомобили",33} |";
            string line = new string('-', title.Length);

            Console.WriteLine(line);
            Console.WriteLine(title);
            Console.WriteLine(line);

            for (int i = 0; i < carsList.Count; i++)
            {
                Car a = carsList[i];

                string similarCarsAsText = getCarsNameByIds(generalCarsList, a.SimilarCars);
                string car = $"| {a.Id,3} | {a.Enterprice,17} | {a.Model,10} | {a.Year,5} | {convertFuel(a.Fuel),8} | {a.EngineVolume,12} | {similarCarsAsText,33} |";
                Console.WriteLine(car);
            }

            Console.WriteLine(line);
        }
        static string convertFuel (string fuel)
        {
            string res = "Газ";
            if (fuel == "petrol")
            {
                return res = "Бензин";
            }
            if (fuel == "diesel")
            {
                return res = "Дизель";
            }
            return res;
        }
        static void fillAutoCatalog (List<Car> carsList)
        {
            carsList.Add(new Car()
            {
                Id = 1,
                Enterprice = "Toyota",
                Model = "Funcargo",
                Year = 2002,
                Fuel = "petrol",
                EngineVolume = 1.3,
                SimilarCars = new List<int>() { 6 }
            });
            carsList.Add(new Car()
            {
                Id = 2,
                Enterprice = "Toyota",
                Model = "Corolla",
                Year = 2004,
                Fuel = "petrol",
                EngineVolume = 1.8,
                SimilarCars = new List<int>() { 4, 5 }
            });
            carsList.Add(new Car()
            {
                Id = 3,
                Enterprice = "Toyota",
                Model = "Town Ace",
                Year = 1994,
                Fuel = "diesel",
                EngineVolume = 2.0,
                SimilarCars = new List<int>() { }
            });
            carsList.Add(new Car()
            {
                Id = 4,
                Enterprice = "Nissan",
                Model = "Bluebird",
                Year = 2008,
                Fuel = "petrol",
                EngineVolume = 1.5,
                SimilarCars = new List<int>() { 2, 5 }
            });
            carsList.Add(new Car()
            {
                Id = 5,
                Enterprice = "Honda",
                Model = "Civic",
                Year = 2006,
                Fuel = "petrol",
                EngineVolume = 1.3,
                SimilarCars = new List<int>() { 4, 2 }
            });
            carsList.Add(new Car()
            {
                Id = 6,
                Enterprice = "Nissan",
                Model = "Cube",
                Year = 2014,
                Fuel = "gas",
                EngineVolume = 1.5,
                SimilarCars = new List<int>() { 1 }
            });
        }
    }
}

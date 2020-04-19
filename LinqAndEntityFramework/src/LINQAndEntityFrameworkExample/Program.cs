using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LINQAndEntityFrameworkExample.context;
using LINQAndEntityFrameworkExample.Models;
using Microsoft.Extensions.Logging;

namespace LINQAndEntityFrameworkExample
{
    class Program
    {
        static void Main(string[] args)
        {
           
              
            InsertData();
            //QueryData();
            QueryWithFilter();
            
        }

        private static void QueryData()
        {
            var db= new CarsDbContext();

            var queryMostFuelEfficientCars=
                from car in db.Cars
                orderby car.Combined descending, car.Name ascending
                select car;

            foreach (var car in queryMostFuelEfficientCars.Take(10))
            {
                Console.WriteLine($"{car.Name} {car.Combined}");
            }
            
        }
        private static void QueryWithFilter()
        {
            var db= new CarsDbContext();

            var queryFilteringABrand=
                db.Cars.Where(c=> c.Manufacterer=="BMW")
                .OrderByDescending(c=>c.Combined)
                .ThenBy(c=>c.Name).Take(2);

            foreach (var car in queryFilteringABrand)
            {
                Console.WriteLine($"{car.Name} {car.Combined}");
            }
            
        }

        private static void InsertData()
        {
            var cars = ProcessCar("/Users/javiermayorga/Documents/Dev/LINQFundamentalsC-sharp/LinqAndEntityFramework/src/LINQAndEntityFrameworkExample/context/fuel.csv");
            var db= new CarsDbContext();
            db.Database.EnsureCreated();

            if (!db.Cars.Any())
            {
                foreach (var car in cars)
                {
                    db.Add(car);
                }
                db.SaveChanges();
            }
        }

        

        private static List<Car> ProcessCar(string path)
        {
            var query=
                    File.ReadAllLines(path)
                    .Skip(1) //skip headers
                    .Where(l=>l.Length>1) 
                    .Select(l=>
                    {
                        var column=l.Split(',');
                    
                        return new Car()
                        {
                            Year=int.Parse(column[0]),
                            Manufacterer=column[1],
                            Name=column[2],
                            Displacement=double.Parse(column[3]),
                            Cylinders=int.Parse(column[4]),
                            City=int.Parse(column[5]),
                            Highway=int.Parse(column[6]),
                            Combined=int.Parse(column[7])
                        };
                    });

                    return query.ToList();
        }
    }
}

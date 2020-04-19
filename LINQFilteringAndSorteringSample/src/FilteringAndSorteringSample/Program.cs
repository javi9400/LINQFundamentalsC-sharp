using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FilteringAndSorteringSample.Models;
using System.Xml.Linq;
namespace FilteringAndSorteringSample
{
    class Program
    {
        static void Main(string[] args)
        {
           var cars=ProcessFile("/Users/javiermayorga/Documents/Dev/LINQFundamentalsC-sharp/LINQFilteringAndSorteringSample/src/FilteringAndSorteringSample/dataSource/fuel.csv");   
           var manufacturers=ProcessManufacturers("/Users/javiermayorga/Documents/Dev/LINQFundamentalsC-sharp/LINQFilteringAndSorteringSample/src/FilteringAndSorteringSample/dataSource/manufacturers.csv");   

           /* Query all the csv */
           Console.WriteLine("Querying all the csv");

           foreach (var item in cars)
           {

               Console.WriteLine($"Car Name {item.Name}");
           }

           Console.WriteLine("***************");

            /* Querying ordering in descending order by combined fuel */

           Console.WriteLine("Querying top 10 ordering by descending order");

           var queryOrderByDescending= cars.OrderByDescending(c=>c.Combined);

            foreach (var item in queryOrderByDescending.Take(10))
           {

               Console.WriteLine($"Car Name {item.Name}");
           }

           Console.WriteLine("***************");

             /* Querying ordering using secondary sort */

           Console.WriteLine("Querying ordering using secondary sort");

           var queryOrderByAlphabet=queryOrderByDescending.ThenBy(c=>c.Name);

            foreach (var item in queryOrderByAlphabet.Take(10))
           {

               Console.WriteLine($" {item.Name} , {item.Combined}");
           }

            Console.WriteLine("***************");

         var queryManufacturer=
                        from car in cars
                        where car.Manufacterer=="BMW" && car.Year==2016
                        orderby car.Combined descending, car.Name ascending
                        select car;

        var queryManufacturer2=cars.Where(c=>c.Manufacterer=="BMW" && c.Year==2016)
                                    .OrderByDescending(c=>c.Combined)
                                    .ThenBy(c=>c.Name)
                                    .Select(c=>c);
                                    

            foreach (var item in queryManufacturer.Take(10))
           {

               Console.WriteLine($" {item.Name} , {item.Combined}");
           }

            Console.WriteLine("***************");


            Console.WriteLine("Joining and grouping");
            
             //join is able to compare base on equality
            var queryJoin=
                from car in cars
                join manufacturer in manufacturers
                    on car.Manufacterer equals manufacturer.Name
                orderby car.Combined descending, car.Name ascending
                select new 
                {
                    manufacturer.Headquarters,
                    car.Name,
                    car.Combined
  
                }; //query sintax

            var queryJoin2=
                cars.Join(manufacturers,
                 c=>c.Manufacterer,
                 m=>m.Name, (c,m)=>new {
                     m.Headquarters,
                     c.Name,
                     c.Combined
                 })
                 .OrderByDescending(c=>c.Combined)
                 .ThenBy(c=>c.Name);

            // joining base on 2 columns
            var queryJoin3=
                from car in cars
                join manufacturer in manufacturers
                    on new {car.Manufacterer, car.Year }
                    equals 
                    new {Manufacterer=manufacturer.Name,manufacturer.Year}
                orderby car.Combined descending, car.Name ascending
                select new 
                {
                    manufacturer.Headquarters,
                    car.Name,
                    car.Combined
  
                }; //query sintax   

            foreach (var car in queryJoin.Take(10))
            {
                Console.WriteLine($"{car.Headquarters} {car.Name}: {car.Combined}");
            }

           Console.WriteLine("***************");


            Console.WriteLine("Grouping");

            var queryGrouping=
                from car in cars
                group car by car.Manufacterer.ToUpper() into manufacturer
                orderby manufacturer.Key //ordering
                select manufacturer;

         var queryGroupingManufacturer=
                manufacturers.GroupJoin(cars,m=>m.Name,c=>c.Manufacterer,(m,g)=>
                new 
                {
                    Manufacturer=m,
                    Cars=g
                }).OrderBy(m=>m.Manufacturer.Name);


         foreach (var group in queryGrouping)
         {
              Console.WriteLine($"{group.Key}");

              foreach (var car in group.Take(2))
              {
                  Console.WriteLine($"\t {car.Combined} {car.Name}");
              }
         }

         Console.WriteLine("Mine");

         foreach (var group in queryGroupingManufacturer)
         {
             Console.WriteLine($"{group.Manufacturer.Name} {group.Manufacturer.Headquarters}");

             foreach (var car in group.Cars.OrderBy(c=>c.Name).Take(2))
             {
                 Console.WriteLine($"\t{car.Name} {car.Combined}");
             }

         }



        }

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query=
                    File.ReadAllLines(path)
                    .Where(l=>l.Length>1) //skip header
                    .Select(l=>
                    {
                        var columns=l.Split(',');
                        return new Manufacturer
                        {
                            Name=columns[0],
                            Headquarters=columns[1],
                            Year=int.Parse(columns[2])
                        };
                    });
                    return query.ToList();
        }

        private static List<Car> ProcessFile(string path)
        {
           return File.ReadAllLines(path)
                .Skip(1)
                .Where(line=> line.Length>1)
                .Select(Car.ParseFromCsv).ToList();

        }

    }
}

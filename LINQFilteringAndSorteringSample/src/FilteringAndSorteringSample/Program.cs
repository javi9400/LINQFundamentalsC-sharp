using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FilteringAndSorteringSample.Models;

namespace FilteringAndSorteringSample
{
    class Program
    {
        static void Main(string[] args)
        {
           var cars=ProcessFile("/Users/javiermayorga/Documents/Dev/LINQFundamentalsC-sharp/LINQFilteringAndSorteringSample/src/FilteringAndSorteringSample/dataSource/fuel.csv");   

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

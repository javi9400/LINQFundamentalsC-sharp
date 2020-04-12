using System;
using System.Collections.Generic;
using System.Linq;
using CompanySample.Extensions;

namespace CompanySample
{
    class Program
    {
        static void Main(string[] args)
        {
            var developers=new Employee[]
            {
                new Employee()
                {
                    Id=1,
                    Name="Javier"
                },
                new Employee()
                {
                    Id=2,
                    Name="Carlos"
                }
            };

           var sales=new List<Employee>()
            {
                new Employee()
                {
                    Id=3,
                    Name="Gina"
                },
                new Employee()
                {
                    Id=4,
                    Name="Elder"
                }
            };

       // Console.WriteLine(sales.Count());
        
        

        //Method aproach
        foreach (var item in developers.Where(NameStartsWithS))
        {
            Console.WriteLine($"Name {item.Name}, Id: {item.Id}");
        }

        //Lambda expression

        var query=developers.Where(x=> x.Name.StartsWith("J")).OrderBy(x=> x.Name);

         foreach (var item in query)
        {
            Console.WriteLine($"Name {item.Name}, Id: {item.Id}");
        }


        //query sintax
        var querySqlSintax= from developer in developers
                            where developer.Name.StartsWith("J")
                            orderby developer.Name descending
                            select developer;

          foreach (var item in querySqlSintax)
        {
            Console.WriteLine($"Name {item.Name}, Id: {item.Id}");
        }

        }



        private static bool NameStartsWithS(Employee employee)
        {
            return employee.Name.StartsWith("J");
        }


    }
}

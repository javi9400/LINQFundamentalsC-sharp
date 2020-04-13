using System;
using System.Collections.Generic;
using System.Linq;
using QueriesSample.Extensions;
using QueriesSample.Models;

namespace QueriesSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var movies= new List<Movie>()
            {
                new Movie()
                {
                    Id=1,
                    Title="Dark Knight",
                    Year=2014,
                    Rating=7.4f
                },

                 new Movie()
                {
                    Id=2,
                    Title="Spiderman",
                    Year=2018,
                    Rating=8.4f
                },

                 new Movie()
                {
                    Id=3,
                    Title="Inception",
                    Year=2010,
                    Rating=10.0f
                },

                new Movie()
                {
                    Id=4,
                    Title="Star Wars The Rise of skywalker",
                    Year=2015,
                    Rating=9.4f
                }

            };

            var queryMoviesGreaterThan=movies.Where(m=>m.Year>2016);

            //var moviesCount=queryMoviesGreaterThan.Count(); this will execute twice the WHERE statement

            foreach (var item in queryMoviesGreaterThan)
            {
                Console.WriteLine($"Movie Name: {item.Title}, Release Year: {item.Year}");   
            }


        }
    }
}

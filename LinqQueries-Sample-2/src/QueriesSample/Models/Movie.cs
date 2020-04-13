using System;

namespace QueriesSample.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public float Rating { get; set; }
        private int _year;
        public int Year
        {
            get 
            {
               Console.WriteLine($"Returning year {_year} for {Title} ");
               return _year; 
               
            }
            set { _year = value; }
        }
        
    }
}
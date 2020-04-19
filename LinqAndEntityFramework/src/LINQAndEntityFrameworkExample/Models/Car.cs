namespace LINQAndEntityFrameworkExample.Models
{
    public class Car
    {
        public int CarId {get;set;}
        public int Year { get; set; }
        public string Manufacterer { get; set; }
        public string Name { get; set; }

        public double Displacement { get; set; }    

        public int Cylinders { get; set; }

        public int City { get; set; }

        public int Highway  { get; set; }

        public int Combined { get; set; }
    }
}
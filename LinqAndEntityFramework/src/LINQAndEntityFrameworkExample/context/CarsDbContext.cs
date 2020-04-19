using LINQAndEntityFrameworkExample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace LINQAndEntityFrameworkExample.context
{
    public class CarsDbContext:DbContext
    {
         public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });
        public DbSet<Car> Cars {get;set;}

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {   
                optionsBuilder
                .UseLoggerFactory(MyLoggerFactory)
                .UseNpgsql("User Id=postgres;Password=password;Server=localhost;Port=5433;Database=carsDb;Integrated Security=true;Pooling=true;");
            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {    
            modelBuilder.Entity<Car>(b =>
            {
            b.HasKey(e => e.CarId);
            b.Property(e => e.CarId).ValueGeneratedOnAdd();
            });
        }


    }
}
namespace LINQAndEntityFrameworkExample.context
{
    public static class dbInitializer
    {
         public static void Initialize(CarsDbContext context)
        {
            context.Database.EnsureDeleted();
        }
    }
}
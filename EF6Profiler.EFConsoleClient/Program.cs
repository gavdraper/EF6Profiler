using System;
using System.Data.Entity;
using System.Linq;
using EF6Profiler.EFConsoleClient.Model;
using EF6Profiler.ProfileLogger.Loggers;

namespace EF6Profiler.EFConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press Enter To Start");            
            Console.ReadLine();
            Console.WriteLine("Querying EF...");
            using (var logger = new SignalRLogger())
            {
                var ctx = new CinemaContext(logger);
                //var ctx = new CinemaContext();

                //for (var i = 1; i < 10000; i++)
                //{
                //    ctx.Cinemas.Add(new Cinema() {Name = Guid.NewGuid().ToString()});
                    
                //}
                //ctx.SaveChanges();

                var cinemas = ctx.Cinemas
                    .Include(x=>x.Films)
                    .Include(x=>x.Films.Select(y=>y.Reviews))
                    .Take(100)
                    .ToList();
                var cinema = ctx.Cinemas.FirstOrDefault(x => x.Name == "Gavin Cinema");
                ctx.Cinemas.FirstOrDefault(x => x.Name == "Gavin Cinema");

                Console.WriteLine("Done!");

            }
            Console.ReadLine();
        }
    }
}

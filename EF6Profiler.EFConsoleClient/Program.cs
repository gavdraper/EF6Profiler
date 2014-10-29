using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using EF6Profiler.EFConsoleClient.Model;
using EF6Profiler.ProfileLogger;
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
                DbInterception.Add(new CommandInterceptor(logger));
                var ctx = new CinemaContext();
                var cinemas = ctx.Cinemas
                    .Include(x=>x.Films)
                    .Include(x=>x.Films.Select(y=>y.Reviews))
                    .Take(100)
                    .ToList();
                Console.WriteLine("Done!");

            }
            Console.ReadLine();
        }
    }
}

using System;
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
                var cinemas = ctx.Cinemas.ToList();
            }
            Console.ReadLine();
        }
    }
}

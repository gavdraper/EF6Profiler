using System;
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
            Console.WriteLine("PRess Enter To Start");
            Console.ReadLine();
            IProfileLogger logger = new SignalRLogger();
            var ctx = new CinemaContext(logger);
            var cinemas = ctx.Cinemas.ToList();
            Console.ReadLine();
        }
    }
}

using System;

namespace EF6Profiler.ProfileLogger.Loggers
{
    public class ConsoleLogger : IProfileLogger
    {
        public void Log(string log)
        {
            Console.WriteLine(log);
        }
    }
}

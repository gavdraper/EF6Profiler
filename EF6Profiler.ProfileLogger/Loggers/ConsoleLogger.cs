using System;

namespace EF6Profiler.ProfileLogger.Loggers
{
    public class ConsoleLogger : IProfileLogger
    {
        public void Log(CommandProfile commandProfile)
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("Start : " + commandProfile.Start.ToString("yyyy-MM-dd hh:mm:ss"));
            Console.WriteLine("Finish : " + commandProfile.End.ToString("yyyy-MM-dd hh:mm:ss"));
            Console.WriteLine("Elapsed : " + commandProfile.Elapsed);
            Console.WriteLine("Async : " + commandProfile.Async);
            Console.WriteLine("Succes : " + !commandProfile.Failed);
            if(commandProfile.Failed)
                Console.WriteLine("Exception : " + !commandProfile.Failed);
            Console.WriteLine("Type : " + commandProfile.CommandType);
            Console.WriteLine("Command");
            Console.WriteLine(commandProfile.CommandText);
        }
    }
}

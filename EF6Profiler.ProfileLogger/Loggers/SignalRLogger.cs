using System;
using Microsoft.AspNet.SignalR.Client;

namespace EF6Profiler.ProfileLogger.Loggers
{
    public class SignalRLogger : IProfileLogger, IDisposable
    {
        private HubConnection connection;
        private IHubProxy hub;

        public SignalRLogger()
        {
            connection = new HubConnection("http://localhost:8374/");
            hub = connection.CreateHubProxy("ProfileHub");
            connection.Start().ContinueWith(task =>
            {
                if (!task.IsFaulted)
                {
                    Console.WriteLine("Connected");
                }

            }).Wait();
        }
        public void Log(CommandProfile commandProfile)
        {
            if (connection.State == ConnectionState.Connected)
            {
                hub.Invoke<string>("LogProfile", commandProfile).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Console.WriteLine("There was an error calling send: {0}",
                            task.Exception.GetBaseException());
                    }
                    else
                    {
                        Console.WriteLine(task.Result);
                    }
                });
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                connection.Stop();
            }
        }
    }
}

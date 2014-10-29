using System.Windows;
using EF6Profiler.ProfileLogger;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.AspNet.SignalR;

namespace EF6Profiler.ProfilerClient
{
    public class ProfileHub : Hub
    {
        public void LogProfile(CommandProfile cp)
        {
            Messenger.Default.Send(cp);
        }
    } 
}

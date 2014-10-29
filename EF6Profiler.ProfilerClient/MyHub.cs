using System.Windows;
using EF6Profiler.ProfileLogger;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.AspNet.SignalR;

namespace EF6Profiler.ProfilerClient
{
    public class MyHub : Hub
    {
        public void LogProfile(CommandProfile cp)
        {
            MessageBox.Show("Profile Received");
            Messenger.Default.Send(cp);
        }
    } 
}

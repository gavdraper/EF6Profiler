using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;
using EF6Profiler.ProfileLogger;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.Owin.Hosting;

namespace EF6Profiler.ProfilerClient
{
    public class MainViewModel
    {
        public ObservableCollection<CommandProfile> CommandProfiles { get; set; }
    }


    public partial class MainWindow : Window
    {
        MainViewModel vm = new MainViewModel();
        

        IDisposable MySignalR { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            vm.CommandProfiles = new ObservableCollection<CommandProfile>();
            this.DataContext = vm;
            

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string url = "http://localhost:8374/";
                this.MySignalR = WebApp.Start<Startup>(url);
                if (this.MySignalR == null)
                    MessageBox.Show("web app started failed !");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
            Messenger.Default.Register<CommandProfile>(this, MessageReceived);
        }

        private void MessageReceived(CommandProfile cp)
        {
            App.Current.Dispatcher.Invoke((Action)(() => vm.CommandProfiles.Add(cp)));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.MySignalR != null)
            {
                this.MySignalR.Dispose();
                this.MySignalR = null;
            }
        }
    }




}


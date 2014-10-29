using System;
using System.Collections.ObjectModel;
using System.Windows;
using EF6Profiler.ProfileLogger;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Owin.Hosting;

namespace EF6Profiler.ProfilerClient
{
    public partial class MainWindow : Window
    {
        MainViewModel vm = new MainViewModel();

        IDisposable MySignalR { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            vm.CommandProfiles = new ObservableCollection<CommandProfileViewModel>();
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
            Application.Current.Dispatcher.Invoke(() => vm.AddProfile(new CommandProfileViewModel(cp)));
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


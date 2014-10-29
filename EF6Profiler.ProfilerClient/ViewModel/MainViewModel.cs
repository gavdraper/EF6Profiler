using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace EF6Profiler.ProfilerClient.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private CommandProfileViewModel _selectedItem;
        private Visibility _stopVisibility;
        private Visibility _startVisibiltiy;
        private bool _running;
        public ObservableCollection<CommandProfileViewModel> CommandProfiles { get; set; }

        public MainViewModel()
        {
            Running = true;
        }

        public bool Running
        {
            get { return _running; }
            set
            {
                _running = value;
                RaisePropertyChanged(() => StopVisibility);
                RaisePropertyChanged(() => StartVisibility);
            }
        }

        public CommandProfileViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
            }
        }

        public Visibility StopVisibility
        {
            get { return Running ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility StartVisibility
        {
            get { return !Running ? Visibility.Visible : Visibility.Collapsed; }
        }

        public RelayCommand ClearCommand
        {
            get
            {
                return new RelayCommand(() => CommandProfiles.Clear());
            }
        }

        public RelayCommand StopCommand
        {
            get
            {
                return new RelayCommand(() => Running = false);
            }
        }

        public RelayCommand StartCommand
        {
            get
            {
                return new RelayCommand(() => Running = true);
            }
        }
    

        public void AddProfile(CommandProfileViewModel commandProfileViewModel)
        {
            if (!Running)
                return;

            if (CommandProfiles.Count > 0)
            {
                var last = CommandProfiles.Last();
                if (last.Equals(commandProfileViewModel))
                {
                    last.Elapsed += commandProfileViewModel.Elapsed;
                    last.Count++;
                }
                else
                    CommandProfiles.Add(commandProfileViewModel);
            }
            else 
                CommandProfiles.Add(commandProfileViewModel);
        }

    }
}
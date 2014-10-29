using System;
using System.Collections.Generic;
using EF6Profiler.ProfileLogger;
using GalaSoft.MvvmLight;

namespace EF6Profiler.ProfilerClient
{
    public class CommandProfileViewModel : ViewModelBase
    {
        private CommandProfile commandProfile;
        private int _count;

        public int Count
        {
            get { return _count; }
            set { _count = value;RaisePropertyChanged("Count"); }
        }

        public DateTime Start { get { return commandProfile.Start; }  }
        public DateTime End { get { return commandProfile.End; } }
        public string ElapsedString { get { return commandProfile.Elapsed.Milliseconds + "ms"; } }

        public TimeSpan Elapsed { get { return commandProfile.Elapsed; } set { commandProfile.Elapsed = value; RaisePropertyChanged("ElapsedString"); RaisePropertyChanged("Elapsed"); } }

        public string CommandText { get { return commandProfile.CommandText; } }
        public bool Async { get { return commandProfile.Async; } }
        public bool Failed { get { return commandProfile.Failed; } }
        public string Exception { get { return commandProfile.Exception; } }

        public string ShortCommandText
        {
            get { return commandProfile.CommandText.Replace(System.Environment.NewLine, " ").Substring(0, 50); }
        }

        public CommandType CommandType { get { return commandProfile.CommandType; } }
        public Dictionary<string, string> Parameters { get { return commandProfile.Parameters; } } 

        public CommandProfileViewModel(CommandProfile commandProfile)
        {
            this.commandProfile = commandProfile;
            Count = 1;
        }   

        //TODO this could probably be done better
        public override bool Equals(object obj)
        {
            var compare = obj as CommandProfileViewModel;
            if (compare == null)
                return false;

            if (compare.Parameters != null && Parameters != null)
            {
                if (compare.Parameters.Count != Parameters.Count)
                    return false;
                foreach (var p in Parameters)
                {
                    if (!compare.Parameters.ContainsKey(p.Key))
                        return false;
                    if (compare.Parameters[p.Key] != p.Value)
                        return false;
                }
            }
            else
            {
                if (compare.Parameters == null && Parameters != null
                    || compare.Parameters != null && Parameters == null)
                    return false;
            }
            if (CommandText != compare.CommandText
                || Exception != compare.Exception)
                return false;
            return true;
        }
    }
}
using System;
using System.Collections.Generic;

namespace EF6Profiler.ProfileLogger
{
    public class CommandProfile
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public TimeSpan Elapsed { get; set; }
        public string CommandText { get; set; }
        public bool Async { get; set; }
        public bool Failed { get; set; }
        public string Exception { get; set; }
        public CommandType CommandType { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }
}

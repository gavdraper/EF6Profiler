using System.Collections.Generic;

namespace EF6Profiler.EFConsoleClient.Model
{
    public class Cinema
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual IList<Film> Films { get; set; }
    }
}

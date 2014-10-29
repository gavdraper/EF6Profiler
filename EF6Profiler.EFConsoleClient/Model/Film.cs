using System;
using System.Collections.Generic;

namespace EF6Profiler.EFSample.Model
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public virtual IList<Review> Reviews { get; set; }
    }
}

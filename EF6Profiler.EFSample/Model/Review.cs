using System;

namespace EF6Profiler.EFSample.Model
{
    public class Review
    {
        public int Id { get; set; }
        public string ReviewText { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public virtual Film Film { get; set; }
    }
}

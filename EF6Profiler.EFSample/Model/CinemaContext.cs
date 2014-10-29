using System.Data.Entity;

namespace EF6Profiler.EFSample.Model
{
    public class CinemaContext : DbContext
    {
        public IDbSet<Cinema> Cinemas { get; set; }
        public IDbSet<Film> Films { get; set; }
        public IDbSet<Review> Reviews { get; set; }
    }
}

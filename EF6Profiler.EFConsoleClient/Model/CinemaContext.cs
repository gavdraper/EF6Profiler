using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using EF6Profiler.ProfileLogger;

namespace EF6Profiler.EFConsoleClient.Model
{
    public class CinemaContext : DbContext
    {
        public IDbSet<Cinema> Cinemas { get; set; }
        public IDbSet<Film> Films { get; set; }
        public IDbSet<Review> Reviews { get; set; }

        public CinemaContext() : base() { }

        public CinemaContext(IProfileLogger logger)
            : base()
        {
            if (logger != null)
            {
                DbInterception.Add(new CommandInterceptor(logger));
            }
        }
    }
}

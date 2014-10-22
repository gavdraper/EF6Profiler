using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using EF6Profiler.ProfileLogger;
using EF6Profiler.ProfileLogger.Formatters;

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
                Database.Log = logger.Log;
                DbInterception.Add(new NLogCommandInterceptor());
            }
        }
    }


    //public class CinemaDbConfiguration : DbConfiguration
    //{
    //    public CinemaDbConfiguration()
    //    {
    //        SetDatabaseLogFormatter(
    //            (context, writeAction) => new OneLineFormatter(context, writeAction));
    //    }
    //}


}

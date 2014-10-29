using Microsoft.Owin.Cors;
using Owin;

namespace EF6Profiler.ProfilerClient
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    } 
}

using Microsoft.Owin;

using Owin;

using SignalR;

[assembly: OwinStartup(typeof(Startup))]
namespace SignalR
{
    public class Startup 
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
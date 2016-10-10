using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Microsoft.AspNet.SignalR;
using Bunchkins.SignalR;

[assembly: OwinStartup(typeof(Bunchkins.Startup))]

namespace Bunchkins
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            // app.MapSignalR();
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);

                map.RunSignalR();
            });

            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new SignalRContractResolver();
            var serializer = JsonSerializer.Create(settings);
            GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), () => serializer);
        }
    }
}

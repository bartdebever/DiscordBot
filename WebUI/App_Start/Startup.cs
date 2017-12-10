using System;
using System.Threading.Tasks;
using Discord.OAuth2;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebUI.Startup))]

namespace WebUI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID
        }
        
    }
}

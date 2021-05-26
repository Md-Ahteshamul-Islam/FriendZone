using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(FriendZone.Startup))]
namespace FriendZone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
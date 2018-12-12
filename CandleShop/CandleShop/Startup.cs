using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using CandleShop.Models;
using CandleShop.Data;
using Microsoft.Owin.Security.Cookies;


[assembly: OwinStartup(typeof(CandleShop.Startup))]

namespace CandleShop
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CandleShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;
                                              TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            app.CreatePerOwinContext(() => new AppUserDbContext(connectionString));


            app.CreatePerOwinContext<UserStore<User>>((opt, cont) => new UserStore<User>(cont.Get<AppUserDbContext>()));            app.CreatePerOwinContext<UserManager<User>>((opt, cont) => new UserManager<User>(cont.Get<UserStore<User>>()));


            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }


    }
}

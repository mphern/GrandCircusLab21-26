using CandleShop.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CandleShop.Data
{
    public class AppUserDbContext : IdentityDbContext<User>
    {
        public AppUserDbContext(string connectionString) : base(connectionString) { }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CandleShop.Data;
using CandleShop.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CandleShop.Controllers
{
    public class DatabaseController : Controller
    {
        // GET: Database
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CandleShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;
                                              TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            IdentityDbContext<User> ORM = new IdentityDbContext<User>(connectionString);
            ViewBag.UserList = ORM.Users.ToList();

            return View();
        }

        public ActionResult Products()
        {
            CandleShopEntities ORM = new CandleShopEntities();
            ViewBag.ProductList = ORM.Products.ToList();

            return View();
        }

        public ActionResult ProductSearch(string search)
        {
            CandleShopEntities ORM = new CandleShopEntities();
            ViewBag.ProductList = ORM.Products.ToList();
            ViewBag.Search = search;

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CandleShop.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace CandleShop.Controllers
{
    public class DatabaseController : Controller
    {
        public UserManager<IdentityUser> UserManager => HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();
        // GET: Database
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {

            CandleShopEntities ORM = new CandleShopEntities();
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
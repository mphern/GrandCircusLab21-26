using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CandleShop.Models;

namespace CandleShop.Controllers
{
    public class DatabaseController : Controller
    {
        // GET: Database
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
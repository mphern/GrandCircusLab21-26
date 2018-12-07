using CandleShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CandleShop.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditProductList()
        {

            CandleShopEntities ORM = new CandleShopEntities();
            ViewBag.ProductList = ORM.Products.ToList();

            return View();
        }
    }
}
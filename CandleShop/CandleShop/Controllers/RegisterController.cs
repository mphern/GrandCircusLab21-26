using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CandleShop.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Welcome(string firstName, string state)
        {
            ViewBag.FirstName = firstName;
            ViewBag.State = state;
            return View();
        }
    }
}
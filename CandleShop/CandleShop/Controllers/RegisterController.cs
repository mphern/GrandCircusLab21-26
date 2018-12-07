using CandleShop.Models;
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

        public ActionResult Welcome(User user)
        {
            CandleShopEntities ORM = new CandleShopEntities();
            List<User> userEmails = ORM.Users.Where(u => u.Email.Equals(user.Email)).ToList();
            if(userEmails.Count == 0)
            {
                ORM.Users.Add(user);
                ORM.SaveChanges();
                ViewBag.DuplicateEmail = false;
            }
            else
            {
                ViewBag.DuplicateEmail = true;
            }

            return View(user);
        }
    }
}
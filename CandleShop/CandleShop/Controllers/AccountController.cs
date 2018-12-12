using CandleShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using CandleShop.Data;

namespace CandleShop.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<User> UserManager => HttpContext.GetOwinContext().Get<UserManager<User>>();

        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel registerUser)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    UserName = registerUser.UserName,
                    Email = registerUser.Email,
                    PhoneNumber = registerUser.PhoneNumber,
                    UserID = registerUser.UserID,
                    FirstName = registerUser.FirstName,
                    LastName = registerUser.LastName,
                    Password = registerUser.Password,
                    State = registerUser.State,
                    Question = registerUser.Question,
                    Answer = registerUser.Answer
                };

                var identityResult = await UserManager.CreateAsync(user, registerUser.Password);

                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Welcome", user);
                }

                ModelState.AddModelError("", identityResult.Errors.FirstOrDefault());

                return View();
            }
            return View();
        }

        public ActionResult Welcome(User user)
        {
            const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CandleShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;
                                              TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            AppUserDbContext ORM = new AppUserDbContext(connectionString);
            CandleShopEntities ORM2 = new CandleShopEntities();
            List<User> userEmails = ORM.Users.Where(u => u.Email.Equals(user.Email)).ToList();
            if(userEmails.Count == 1)
            {
                ORM2.Users.Add(user);
                ORM2.SaveChanges();
                ViewBag.DuplicateEmail = false;
            }
            else
            {
                ViewBag.DuplicateEmail = true;
            }

            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().Get<UserManager<User>>();
                var authManager = HttpContext.GetOwinContext().Authentication;

                User user = userManager.Find(login.UserName, login.Password);
                if (user != null)
                {
                    
                    var ident = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    
                    
                    authManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        public ActionResult LogOut (LoginModel login)
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
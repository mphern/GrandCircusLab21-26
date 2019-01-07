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


namespace CandleShop.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<IdentityUser> UserManager => HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();

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
                IdentityUser user = new IdentityUser()
                {
                    UserName = registerUser.UserName,
                    Email = registerUser.Email,
                    PhoneNumber = registerUser.PhoneNumber,
                };

                var identityResult = await UserManager.CreateAsync(user, registerUser.Password);

                if (identityResult.Succeeded)
                {
                    User newUser = new User()
                    {
                        UserName = registerUser.UserName,
                        FirstName = registerUser.FirstName,
                        LastName = registerUser.LastName,
                        Email = registerUser.Email,
                        Password = registerUser.Password,
                        State = registerUser.State,
                        Question = registerUser.Question,
                        Answer = registerUser.Answer
                    };


                    return RedirectToAction("Welcome", newUser);
                }

                ModelState.AddModelError("", identityResult.Errors.FirstOrDefault());

                return View();
            }
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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();
                var authManager = HttpContext.GetOwinContext().Authentication;

                IdentityUser user = userManager.Find(login.UserName, login.Password);
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

        public ActionResult GetEmail()
        {

            return View();
        }



        public ActionResult ChangePass(string email)
        {
            const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CandleShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;
                                              TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            IdentityDbContext<IdentityUser> ORM = new IdentityDbContext<IdentityUser>(connectionString);
            ViewBag.Email = email;
            ViewBag.UserList = ORM.Users.ToList();

            return View();
        }

        [Authorize]
        public ActionResult ViewProducts()
        {
            CandleShopEntities ORM = new CandleShopEntities();
            ViewBag.ProductList = ORM.Products.ToList();

            return View();
        }

        [Authorize]
        public ActionResult AddProduct(Product selectedProduct)
        {
            CandleShopEntities ORM = new CandleShopEntities();

            Product productToAdd = ORM.Products.Find(selectedProduct.ProductID);

            productToAdd.UserName = User.Identity.Name;


            ORM.Products.Add(productToAdd);
            ORM.SaveChanges();

            ViewBag.Candle = productToAdd.Name;

            return View();
        }

        [Authorize]
        public ActionResult ViewCart()
        {
            string userName = User.Identity.Name;
            CandleShopEntities ORM = new CandleShopEntities();

            ViewBag.UserItems = ORM.Products.Where(x => x.UserName == userName);

            return View();
        }

        public ActionResult RemoveProduct(int ProductID)
        {
        
            CandleShopEntities ORM = new CandleShopEntities();

            Product product = ORM.Products.Find(ProductID);
            ORM.Products.Remove(product);
            ORM.SaveChanges();

            return RedirectToAction("ViewCart");
        }
        
    }
}
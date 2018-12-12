using CandleShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace CandleShop.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditDeleteProductList()
        {

            CandleShopEntities ORM = new CandleShopEntities();
            ViewBag.ProductList = ORM.Products.ToList();

            return View();
        }

        public ActionResult EditDeleteUserList()
        {

            CandleShopEntities ORM = new CandleShopEntities();
            ViewBag.UserList = ORM.Users.ToList();

            return View();
        }

        public ActionResult AddProduct()
        {

            return View();
        }

        public ActionResult EditProduct(int ProductID)
        {
            CandleShopEntities ORM = new CandleShopEntities();
            Product product = ORM.Products.Find(ProductID);

            return View(product); 
        }

        public ActionResult EditUsers(int UserID)
        {
            CandleShopEntities ORM = new CandleShopEntities();
            User user = ORM.Users.Find(UserID);

            return View(user);
        }

        public ActionResult SaveNewProduct(Product newProduct)
        {
            CandleShopEntities ORM = new CandleShopEntities();
            List<Product> products = ORM.Products.Where(p => p.Name.Equals(newProduct.Name)).ToList();
            if(products.Count == 0)
            {
                ORM.Products.Add(newProduct);
                ORM.SaveChanges();
            }

            return RedirectToAction("EditDeleteProductList");
        }

        public ActionResult SaveProductChanges(Product updatedProduct)
        {
            CandleShopEntities ORM = new CandleShopEntities();
            Product oldProduct = ORM.Products.Find(updatedProduct.ProductID);

            oldProduct.Name = updatedProduct.Name;
            oldProduct.Price = updatedProduct.Price;
            oldProduct.QuantityInStock = updatedProduct.QuantityInStock;

            ORM.Entry(oldProduct).State = EntityState.Modified;
            ORM.SaveChanges();

            return RedirectToAction("EditDeleteProductList");
        }

        public ActionResult DeleteProduct(int ProductID)
        {
            CandleShopEntities ORM = new CandleShopEntities();
            Product product = ORM.Products.Find(ProductID);

            ORM.Products.Remove(product);
            ORM.SaveChanges();

            return RedirectToAction("EditDeleteProductList");
        }
    }
}
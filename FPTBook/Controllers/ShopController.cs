using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FPTBook.Models;
using FPTBook.Models.ViewModels;

namespace FPTBook.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Shop
        public ActionResult Index()
        {
            var books = db.Books.ToList();
            var categories = db.Categories.ToList();

            var lsBook = new ManageBookViewModel(books, categories);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            ViewBag.CartCount = cart.GetCount();
            return View(lsBook);
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult BookDetail()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            ViewBag.CartCount = cart.GetCount();
            return View();
        }
    }
}
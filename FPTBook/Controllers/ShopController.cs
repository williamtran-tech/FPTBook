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
            var authors = db.Users.Take(3).ToList();
            var books = db.Books.Take(3).ToList();
            var categories = db.Categories.Take(3).ToList();

            var lsBook = new ManageBookViewModel(books, authors, categories);
            return View(lsBook);
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult BookDetail()
        {
            return View();
        }
    }
}
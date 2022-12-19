using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FPTBook.Models;
using FPTBook.Models.ViewModels;

namespace FPTBook.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books
        public ActionResult Index()
        {
            //ViewBag.AuthorName = db.Users.Take(1);
            var books = db.Books.ToList();
            var categories = db.Categories.ToList();

            var lsBook = new ManageBookViewModel(books, categories);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            ViewBag.CartCount = cart.GetCount();
            return View(lsBook);
        }

        //GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            ViewBag.CartCount = cart.GetCount();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            ViewBag.CartCount = cart.GetCount();
            //ViewBag.categories = db.Categories.ToList();
            ViewBag.Categories = new SelectList(db.Categories, "Id", "Name");

            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Book book, HttpPostedFileBase fileImage)
        {
            //var errors = ModelState
            //    .Where(x => x.Value.Errors.Count > 0)
            //    .Select(x => new { x.Key, x.Value.Errors })
            //    .ToArray();
            book.Category = db.Categories.Where(u => u.Id == book.Category.Id).FirstOrDefault();
            book.Date = DateTime.Now;
            //The model state is not valid because of nullable feild in the form (category select)
            if (!ModelState.IsValid)
            {
                if (fileImage != null || fileImage.ContentLength > 0)
                {
                    string _Filename = Path.GetFileName(fileImage.FileName);

                    string path = Path.Combine(Server.MapPath("/Images/"), _Filename);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                        fileImage.SaveAs(path);
                        book.ImagePath = _Filename;
                    }
                    else
                    {
                        fileImage.SaveAs(path);
                        book.ImagePath = _Filename;
                    }
                }
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            ViewBag.CartCount = cart.GetCount();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Book book = db.Books.Find(id);

            ViewBag.Categories = new SelectList(db.Categories, "Id", "Name");
            ViewBag.ImgPath = book.ImagePath;

            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Book book, HttpPostedFileBase fileImage)
        {
            //Using AsNoTracking for update the book without tracking wrong object
            var unchangeBook = db.Books.AsNoTracking().Where(u => u.Id == book.Id).FirstOrDefault();
            book.Category = db.Categories.Where(u => u.Id == book.Category.Id).FirstOrDefault();
            book.Date = DateTime.Now;

            if (!ModelState.IsValid)
            {
                if (fileImage != null || fileImage.ContentLength > 0)
                {
                    string _Filename = Path.GetFileName(fileImage.FileName);

                    string path = Path.Combine(Server.MapPath("/Images/"), _Filename);
                    

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                        fileImage.SaveAs(path);
                        book.ImagePath = _Filename;
                    }
                    else
                    {
                        //Using request map path to delete file in Server
                        string fullPath = Request.MapPath("/Images/" + unchangeBook.ImagePath);
                        
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                        fileImage.SaveAs(path);
                        book.ImagePath = _Filename;
                    }
                }
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            ViewBag.CartCount = cart.GetCount();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            ViewBag.ImgPath = book.ImagePath;
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.Book book = db.Books.Find(id);
            string fullPath = Request.MapPath("/Images/" + book.ImagePath);
            System.IO.File.Delete(fullPath);
            db.Books.Remove(book);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

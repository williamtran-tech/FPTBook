using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FPTBook.Models;
using FPTBook.Models.ViewModels;

namespace FPTBook.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            // Update cart quantity
            ViewBag.CartCount = cart.GetCount();

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return View(viewModel);
        }
        // GET: /Store/AddToCart/5
        //public ActionResult AddToCart(int id)
        //{
        //    // Retrieve the album from the database
        //    var addedBook = db.Books
        //        .Single(book => book.Id == id);

        //    // Add it to the shopping cart
        //    var cart = ShoppingCart.GetCart(this.HttpContext);
        //    ViewBag.CartCount = cart.GetCount();
        //    cart.AddToCart(addedBook);

        //    // Go back to the main store page for more shopping
        //    return RedirectToAction("Index");
        //}
        //

        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            string bookName = db.Carts
                .Single(item => item.RecordId == id).Book.Title;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(bookName) +
                    " has been removed from your shopping cart.",
                CartTotal = (double)cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }

        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            // Retrieve the album from the database
            var addedBook = db.Books
                .Single(book => book.Id == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedBook);

            // Display update cart
            var results = new ShoppingCartRemoveViewModel
            {
                CartCount = cart.GetCount()
            };
            return Json(cart.GetCount());
        }
        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}
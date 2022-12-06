using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPTBook.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}
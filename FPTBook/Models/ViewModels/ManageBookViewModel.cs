using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPTBook.Models.ViewModels
{
    public class ManageBookViewModel
    {
        public ManageBookViewModel() { }
        public ManageBookViewModel(List<Book> books, List<Category> categories)
        {
            Books = books;
            Categories = categories;
        }
        public List<Book> Books { get; set; }
        public List<Category> Categories { get; set; }
    }
} 
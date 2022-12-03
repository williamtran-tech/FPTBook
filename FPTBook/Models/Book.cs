using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPTBook.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Category Category { get; set; }
        public ApplicationUser Author { get; set; }
    }
}
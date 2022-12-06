using System.ComponentModel.DataAnnotations;

namespace FPTBook.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public virtual Book Book{ get; set; }
        public virtual Order Order { get; set; }
    }
}
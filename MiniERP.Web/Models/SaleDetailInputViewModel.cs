using System.ComponentModel.DataAnnotations;

namespace MiniERP.Web.Models
{
    public class SaleDetailInputViewModel
    {
        [Required(ErrorMessage = "Product is required.")]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit price is required.")]
        [Range(typeof(decimal), "0.01", "999999999", ErrorMessage = "Unit price must be greater than zero.")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }
    }
}

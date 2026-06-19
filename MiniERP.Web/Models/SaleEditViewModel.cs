using System.ComponentModel.DataAnnotations;

namespace MiniERP.Web.Models
{
    public class SaleEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Sale date is required.")]
        [Display(Name = "Sale Date")]
        public DateTime SaleDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Customer is required.")]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        public List<SaleDetailInputViewModel> Details { get; set; } = new();
    }
}

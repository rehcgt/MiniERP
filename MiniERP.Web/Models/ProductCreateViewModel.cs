using System.ComponentModel.DataAnnotations;

namespace MiniERP.Web.Models
{
    public class ProductCreateViewModel
    {
        [Required(ErrorMessage = "Code is required.")]
        [StringLength(50, ErrorMessage = "Code cannot exceed 50 characters.")]
        [Display(Name = "Code")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cost price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Cost price must be greater than or equal to 0.")]
        [Display(Name = "Cost Price")]
        public decimal CostPrice { get; set; }

        [Required(ErrorMessage = "Sale price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Sale price must be greater than or equal to 0.")]
        [Display(Name = "Sale Price")]
        public decimal SalePrice { get; set; }

        [Required(ErrorMessage = "Stock is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Stock must be greater than or equal to 0.")]
        [Display(Name = "Initial Stock")]
        public decimal Stock { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}

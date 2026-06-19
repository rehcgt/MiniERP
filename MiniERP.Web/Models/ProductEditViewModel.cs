using System.ComponentModel.DataAnnotations;

namespace MiniERP.Web.Models
{
    public class ProductEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El código es requerido")]
        [StringLength(50, ErrorMessage = "El código no puede exceder los 50 caracteres")]
        [Display(Name = "Código")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(200, ErrorMessage = "El nombre no puede exceder los 200 caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Descripción")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio de costo es requerido")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio de costo debe ser mayor o igual a 0")]
        [Display(Name = "Precio de Costo")]
        public decimal CostPrice { get; set; }

        [Required(ErrorMessage = "El precio de venta es requerido")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio de venta debe ser mayor o igual a 0")]
        [Display(Name = "Precio de Venta")]
        public decimal SalePrice { get; set; }

        [Required(ErrorMessage = "El stock es requerido")]
        [Range(0, double.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0")]
        [Display(Name = "Stock")]
        public decimal Stock { get; set; }

        [Required(ErrorMessage = "La categoría es requerida")]
        [Display(Name = "Categoría")]
        public int CategoryId { get; set; }
    }
}

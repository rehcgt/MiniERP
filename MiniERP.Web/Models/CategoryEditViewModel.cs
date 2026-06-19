using System.ComponentModel.DataAnnotations;

namespace MiniERP.Web.Models
{
    public class CategoryEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(120, ErrorMessage = "Category name cannot exceed 120 characters.")]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = string.Empty;
    }
}

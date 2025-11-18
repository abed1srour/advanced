using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Car
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "The Model field is required.")]
        public string Model { get; set; }
        
        [Required(ErrorMessage = "The Year field is required.")]
        public int Year { get; set; }
        
        [Required(ErrorMessage = "The Price field is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }
        
        public bool IsAvailable { get; set; }
        
        public string? ImageUrl { get; set; }
        
        [Required(ErrorMessage = "The Brand field is required.")]
        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        
        [Required(ErrorMessage = "The Category field is required.")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        
        [Required(ErrorMessage = "The Supplier field is required.")]
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }
        
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
        
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        
        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }
    }
}

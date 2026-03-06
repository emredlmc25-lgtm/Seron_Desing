using System.ComponentModel.DataAnnotations;
using SeronDesign.Models;

namespace SeronDesign.ViewModels
{
    public class ToyFormViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }
        public IFormFile Image { get; set; }
        public decimal Price { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
using System.ComponentModel.DataAnnotations;

namespace SeronDesign.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı gerekli.")]
        [Display(Name = "Kategori")]
        public string Name { get; set; }

        public ICollection<Toy> Toys { get; set; } = new List<Toy>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
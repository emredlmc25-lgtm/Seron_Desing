using System.ComponentModel.DataAnnotations;

namespace SeronDesign.Models
{
    public class Toy
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Oyuncak adı gerekli.")]
        [Display(Name = "İsim")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Resim Dosyası")]
        public string ImageName { get; set; }

        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }

        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
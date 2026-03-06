using System.ComponentModel.DataAnnotations;

namespace SeronDesign.Models
{
    public class CustomDesign
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık gerekli.")]
        [Display(Name = "Başlık")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Required(ErrorMessage = "E-posta gerekli.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        [Display(Name = "E-posta")]        
        public string CustomerEmail { get; set; }

        [Display(Name = "Müşteri Adı")]
        public string CustomerName { get; set; }

        [Display(Name = "Resim Dosyası")]
        public string ImageName { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
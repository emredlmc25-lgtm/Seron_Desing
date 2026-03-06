using System.ComponentModel.DataAnnotations;

namespace SeronDesign.ViewModels
{
    public class RegisterViewModels
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "İsim gerekli!")]
        [Display(Name = "İsim")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyisim gerekli!")]
        [Display(Name = "Soyisim")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "E-posta gerekli!")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gerekli!")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrarı gerekli!")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        [Display(Name = "Şifreyi Onayla")]
        public string ConfirmPassword { get; set; }

        public int Role { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace SeronDesign.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-posta gerekli!")]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gerekli!")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}

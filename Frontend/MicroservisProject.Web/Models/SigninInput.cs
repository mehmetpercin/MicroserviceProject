using System.ComponentModel.DataAnnotations;

namespace MicroservisProject.Web.Models
{
    public class SigninInput
    {
        [Required]
        [Display(Name = "Email adresi :")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Parola :")]
        public string Password { get; set; }
        [Display(Name = "Beni hatırla :")]
        public bool IsRemember { get; set; }
    }
}

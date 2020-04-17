using System.ComponentModel.DataAnnotations;

namespace WebCalendar.Web.Models
{
    public class LoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}
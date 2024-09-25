using System.ComponentModel.DataAnnotations;

namespace MyLMSProject.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Enter Email Address")]
        [EmailAddress]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password not match")]
        public string? ConfirmPassword { get; set; }
        public string? Mobile { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace TodoList.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
       
        public string Password { get; set; }

        [Required]

        [Compare("Password", ErrorMessage ="رمز عبور و تکرار ان یکسان نیستند")]     
        public string ConfirmPassword { get; set; }
    }
}
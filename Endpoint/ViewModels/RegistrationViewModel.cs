using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Endpoint.ViewModels
{
    public class RegistrationViewModel
    {
        [Display(Name = "Электронная почта")]
        [Required(ErrorMessage = "Не указан email")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        [Remote(action: "CheckEmail", controller: "Registration", ErrorMessage = "Email уже используется")]
        public string Email { get; set; } = null!;

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; } = null!;

        [Display(Name = "Повторите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string RepeatPassword { get; set; } = null!;
    }
}

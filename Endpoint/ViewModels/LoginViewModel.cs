using System.ComponentModel.DataAnnotations;

namespace Endpoint.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Электронная почта")]
        [Required(ErrorMessage = "Не указан email")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; } = null!;

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; } = null!;
    }
}

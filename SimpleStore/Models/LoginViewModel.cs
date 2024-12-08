using System.ComponentModel.DataAnnotations;

namespace EmptyStore.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Электронная почта")]
        [Required(ErrorMessage = "Не указан email")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string? Email { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Не указан пароль")]
        public string? Password { get; set; }
    }
}

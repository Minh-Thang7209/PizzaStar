using System;
using System.ComponentModel.DataAnnotations;

namespace PizzaStar.ViewModels
{
    public class CreateOrUpdateUserViewModel
    {
        [Key]
        public string? Id { get; set; }
        [Required(ErrorMessage = "Введіть емейл")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Не коректний емейл")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Введіть рік народження")]
        [Display(Name = "Рік народження")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Введіть номер телефону")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Номер телефону")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Підтвердьте пароль")]
        [Compare("Password", ErrorMessage = "Паролі не збігаються")]
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердити пароль")]
        public string? PasswordConfirm { get; set; }
    }
}

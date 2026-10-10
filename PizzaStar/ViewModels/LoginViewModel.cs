using System;
using System.ComponentModel.DataAnnotations;

namespace PizzaStar.ViewModels
{
    public class LoginViewModel
{
    [Required(ErrorMessage = "Введіть емейл")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Не коректний емейл")]
    [Display(Name = "Email")]
    public string? Email { get; set; }
 
    [Required(ErrorMessage = "Введіть пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string? Password { get; set; }
 
    [Display(Name = "Запам'ятати?")]
    public bool RememberMe { get; set; }
 
    public string? ReturnUrl { get; set; }
}
}

using System.ComponentModel.DataAnnotations;

namespace WorkflowAutomation.Shared.Identity;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    [Display(Name = "Электронная почта")]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "{0} Должен иметь длину от {2} до {1} символов.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Подтвердите пароль")]
    [Compare("Password", ErrorMessage = "Пароль и подтвержденный пароль не совпадают.")]
    public string ConfirmPassword { get; set; }
}
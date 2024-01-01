using System.ComponentModel.DataAnnotations;

namespace WorkflowAutomation.Shared.Identity;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    [Display(Name = "����������� �����")]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "{0} ������ ����� ����� �� {2} �� {1} ��������.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "������")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "����������� ������")]
    [Compare("Password", ErrorMessage = "������ � �������������� ������ �� ���������.")]
    public string ConfirmPassword { get; set; }
}
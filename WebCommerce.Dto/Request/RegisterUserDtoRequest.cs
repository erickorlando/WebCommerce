using System.ComponentModel.DataAnnotations;

namespace WebCommerce.Dto.Request;

public class RegisterUserDtoRequest
{
    [Required]
    [StringLength(200)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(200)]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [RegularExpression("^(19|20)\\d\\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$")]
    public string BirthDate { get; set; }

    [Required]
    [Compare(nameof(ConfirmPassword))]
    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    
}
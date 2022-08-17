using System.ComponentModel.DataAnnotations;

namespace WebCommerce.Dto.Request;

public class LoginDtoRequest
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}
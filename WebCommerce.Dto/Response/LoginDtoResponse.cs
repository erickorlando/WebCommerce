namespace WebCommerce.Dto.Response;

public class LoginDtoResponse : BaseResponse
{
    public string Token { get; set; }

    public string FullName { get; set; }
}
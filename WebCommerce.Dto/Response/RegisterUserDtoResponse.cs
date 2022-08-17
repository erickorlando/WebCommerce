namespace WebCommerce.Dto.Response;

public class RegisterUserDtoResponse : BaseResponse
{
    public string UserId { get; set; }
    public List<string> ValidationErrors { get; set; }
}
using WebCommerce.Dto.Request;
using WebCommerce.Dto.Response;

namespace WebCommerce.Services;

public interface IUserService
{
    Task<RegisterUserDtoResponse> RegisterAsync(RegisterUserDtoRequest request);

    Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request);
}
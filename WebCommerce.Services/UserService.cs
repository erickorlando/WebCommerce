using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebCommerce.DataAccess;
using WebCommerce.Dto.Request;
using WebCommerce.Dto.Response;
using WebCommerce.Entities;

namespace WebCommerce.Services;

public class UserService : IUserService
{
    private readonly UserManager<WebCommerceUserIdentity> _userRepository;
    private readonly RoleManager<IdentityRole> _roleRepository;
    private readonly IOptions<AppSettings> _options;
    private readonly ILogger<UserService> _logger;

    public UserService(UserManager<WebCommerceUserIdentity> userRepository, 
        RoleManager<IdentityRole> roleRepository,
        IOptions<AppSettings> options,
        ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _options = options;
        _logger = logger;
    }

    public async Task<RegisterUserDtoResponse> RegisterAsync(RegisterUserDtoRequest request)
    {
        var response = new RegisterUserDtoResponse();

        try
        {
            var result = await _userRepository.CreateAsync(new WebCommerceUserIdentity
            {
                UserName = request.Email,
                Name = request.FirstName,
                LastName = request.LastName,
                BirthDate = Convert.ToDateTime(request.BirthDate),
                Email = request.Email
            }, request.Password);

            if (!result.Succeeded)
            {
                response.ValidationErrors = result.Errors
                    .Select(e => e.Description)
                    .ToList();
                return response;
            }

            var user = await _userRepository.FindByEmailAsync(request.Email);
            if (user != null)
            {
                response.UserId = user.Id;

                if (!await _roleRepository.RoleExistsAsync(Constants.RolAdministrator))
                {
                    await _roleRepository.CreateAsync(new IdentityRole(Constants.RolAdministrator));
                }
                
                if (!await _roleRepository.RoleExistsAsync(Constants.RolUser))
                {
                    await _roleRepository.CreateAsync(new IdentityRole(Constants.RolUser));
                }

                if (await _userRepository.Users.CountAsync() == 1)
                {
                    await _userRepository.AddToRoleAsync(user, Constants.RolAdministrator);
                }
                else
                {
                    await _userRepository.AddToRoleAsync(user, Constants.RolUser);
                }

                response.Success = true;
            }
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            _logger.LogCritical(ex, "Error en {message}", ex.Message);
        }

        return response;
    }

    public async Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request)
    {
        var response = new LoginDtoResponse();

        try
        {
            var identity = await _userRepository.FindByEmailAsync(request.UserName);

            if (identity is null)
            {
                response.Success = false;
                response.Message = "Usuario no encontrado";
                return response;
            }

            if (!await _userRepository.CheckPasswordAsync(identity, request.Password))
            {
                response.Message = "Contraseña incorrecta";
                return response;
            }

            var expiredDate = DateTime.Now.AddHours(6);

            response.FullName = $"{identity.Name} {identity.LastName}";

            var roles = await _userRepository.GetRolesAsync(identity);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, identity.UserName),
                new Claim(ClaimTypes.Email, identity.Email),
                new Claim(ClaimTypes.GivenName, response.FullName),
                new Claim(ClaimTypes.Sid, identity.Id),
            };

            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var llaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Jwt.SecretKey));

            var credentials = new SigningCredentials(llaveSimetrica, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(credentials);

            var payload = new JwtPayload(_options.Value.Jwt.Issuer, 
                _options.Value.Jwt.Audience, authClaims,
                DateTime.Now, expiredDate);

            var token = new JwtSecurityToken(header, payload);

            response.Token = new JwtSecurityTokenHandler().WriteToken(token);
            response.Success = true;
        }
        catch (Exception ex)
        {
             response.Message = ex.Message;
            _logger.LogCritical(ex, "Error en {message}", ex.Message);
        }

        return response;
    }
}
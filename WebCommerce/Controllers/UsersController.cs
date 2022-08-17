using Microsoft.AspNetCore.Mvc;
using WebCommerce.Dto.Request;
using WebCommerce.Services;

namespace WebCommerce.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody]RegisterUserDtoRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(request);
        }

        return Ok(await _service.RegisterAsync(request));
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDtoRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(request);
        }

        return Ok(await _service.LoginAsync(request));
    }
}
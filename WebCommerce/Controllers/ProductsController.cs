using Microsoft.AspNetCore.Mvc;
using WebCommerce.Dto.Request;
using WebCommerce.Dto.Response;
using WebCommerce.Services;

namespace WebCommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(BaseResponseGeneric<List<ProductDtoResponse>>), 200)]
    public async Task<IActionResult> List(string? filter, int page = 1, int rows = 10)
    {
        return Ok(await _service.ListAsync(filter ?? string.Empty, page, rows));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductDtoRequest model)
    {
        return Ok(await _service.CreateAsync(model));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] ProductDtoRequest model)
    {
        return Ok(await _service.UpdateAsync(id, model));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _service.DeleteAsync(id));
    }
}
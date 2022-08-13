using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCommerce.DataAccess;
using WebCommerce.Dto.Request;
using WebCommerce.Dto.Response;
using WebCommerce.Entities;

namespace WebCommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly WebCommerceDbContext _context;

    public ProductsController(WebCommerceDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = new BaseResponseGeneric<List<ProductDtoResponse>>();

        try
        {
            // Lazy Loading (SIN EL INCLUDE).
            // Eager Loading (CON EL INCLUDE).
            
            response.Data = await _context.Set<Product>()
                //.Include(x => x.Category)
                .Where(p => p.Status)
                .Select(x =>
                    new ProductDtoResponse(x.Id,
                    x.Name,
                    x.UnitPrice,
                    x.Category.Name))
                .AsNoTracking() // Permite traer los datos sin seguimiento.
                .ToListAsync();
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = new BaseResponseGeneric<ProductDtoResponse>();

        try
        {
            var data = await _context.Set<Product>()
                .Where(p => p.Status)
                .Select(x =>
                    new ProductDtoResponse(x.Id,
                    x.Name,
                    x.UnitPrice,
                    x.Category.Name))
                .FirstOrDefaultAsync(x => x.Id == id);

            if (data != null)
                response.Data = data;

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductDtoRequest request)
    {
        var response = new BaseResponseGeneric<int>();

        try
        {
            var product = new Product
            {
                Name = request.Name,
                UnitPrice = request.UnitPrice,
                CategoryId = request.CategoryId,
            };

            await _context.AddAsync(product);

            await _context.SaveChangesAsync();

            response.Data = product.Id;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return Ok(response);
    }
}
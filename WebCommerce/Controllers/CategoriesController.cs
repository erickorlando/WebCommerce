﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCommerce.DataAccess;
using WebCommerce.Dto.Response;
using WebCommerce.Entities;

namespace WebCommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly WebCommerceDbContext _context;

    public CategoriesController(WebCommerceDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<BaseResponseGeneric<List<Category>>> Get()
    {
        var response = new BaseResponseGeneric<List<Category>>();

        try
        {
            response.Data = await _context.Set<Category>().ToListAsync();

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BaseResponseGeneric<Category>), 200)]
    [ProducesResponseType(typeof(BaseResponseGeneric<Category>), 404)]
    public async Task<IActionResult> Get(int id)
    {
        var response = new BaseResponseGeneric<Category>();
        try
        {
            var data = await _context.Set<Category>().SingleOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                response.Data = data;
                response.Success = true;
            }
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Category category)
    {
        var response = new BaseResponseGeneric<int>();

        try
        {
            _context.Set<Category>().Add(category);
            await _context.SaveChangesAsync();

            response.Data = category.Id;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return Ok(response);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(BaseResponse), 200)]
    [ProducesResponseType(typeof(BaseResponse), 404)]
    public async Task<IActionResult> Put(int id, Category category)
    {
        var response = new BaseResponse();

        try
        {
            var entity = await _context.Set<Category>().FindAsync(id);

            if (entity != null)
            {
                entity.Name = category.Name;
                entity.Description = category.Description;
                _context.Entry(entity).State = EntityState.Modified;
            }
            else
                response.Message = "Registro no encontrado";

            await _context.SaveChangesAsync();

            response.Success = entity != null;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(BaseResponse), 200)]
    [ProducesResponseType(typeof(BaseResponse), 404)]
    public async Task<IActionResult> Delete(int id)
    {
        var response = new BaseResponse();

        try
        {
            var entity = await _context.Set<Category>().FindAsync(id);

            if (entity != null)
                _context.Set<Category>().Remove(entity);
            else
                response.Message = "Registro no encontrado";

            response.Success = entity != null;

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response.Success ? Ok(response) : NotFound(response);
    }
}
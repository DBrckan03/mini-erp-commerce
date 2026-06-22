using ERP_sustav.DTOs;
using ERP_sustav.Models;
using ERP_sustav.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERP_sustav.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
   private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Category>> GetById(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if (category is null)
            return NotFound();
        return Ok(category);
    }
    [HttpPost]
    public async Task<ActionResult<Category>> Create(CreateCategoryDto dto)
    {
        try
        {
            var category = await _categoryService.CreateAsync(dto);
            return CreatedAtAction(
                nameof(GetById),
                new { id = category.Id },
                category);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CreateCategoryDto dto)
    {
        try
        {
            var success = await _categoryService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();
            return NoContent();
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var success = await _categoryService.DeleteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

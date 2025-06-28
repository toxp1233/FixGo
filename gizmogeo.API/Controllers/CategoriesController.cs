using gizmogeo.Application.Category.Commands.CreateCategory;
using gizmogeo.Application.Category.Commands.DeleteCategory;
using gizmogeo.Application.Category.Commands.UpdateCategory;
using gizmogeo.Application.Category.Querys.GetAllCategories;
using gizmogeo.Application.Category.Querys.GetCategoryById;
using gizmogeo.Application.Category.Querys.GetCategoryByName;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gizmogeo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllCategoriesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await mediator.Send(new GetCategoryByIdQuery(id));
        return Ok(category);
    }

    [HttpGet("by-name/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var category = await mediator.Send(new GetCategoryByNameQuery(name));
        return Ok(category);
    }


    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
    {
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result); 

    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryCommand command)
    {
        command.Id = id;
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteCategoryCommand { Id = id });
        return NoContent();
    }
}

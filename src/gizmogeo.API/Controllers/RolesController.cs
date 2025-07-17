using gizmogeo.Application.Roles.Commands.CreateRole;
using gizmogeo.Application.Roles.Commands.DeleteRole;
using gizmogeo.Application.Roles.Commands.UpdateRole;
using gizmogeo.Application.Roles.Querys.GetAllRoles;
using gizmogeo.Application.Roles.Querys.GetRoleById;
using gizmogeo.Application.Roles.Querys.GetRoleByName;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace gizmogeo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class RolesController(IMediator mediator) : ControllerBase
{


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllRolesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var role = await mediator.Send(new GetRoleByIdQuery(id));
        return Ok(role);
    }

    [HttpGet("by-name/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var role = await mediator.Send(new GetRoleByNameQuery(name));
        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
    {
        var role = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = role.Id }, role);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRoleCommand command)
    {
        command.Id = id;
        var role = await mediator.Send(command);
        return Ok(role);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteRoleCommand(id));
        return NoContent();
    }
}

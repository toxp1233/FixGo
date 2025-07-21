using gizmogeo.Application.common;
using gizmogeo.Application.ServiceRequests.Command.CreateService;
using gizmogeo.Application.ServiceRequests.Command.DeleteService;
using gizmogeo.Application.ServiceRequests.Command.UpdateService;
using gizmogeo.Application.ServiceRequests.Querys.GetAllServices;
using gizmogeo.Application.ServiceRequests.Querys.GetServiceById;
using gizmogeo.Application.ServiceRequests.Querys.GetServicesByUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace gizmogeo.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ServicesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery]PaginationParams paginationParams)
    {
        var services = await mediator.Send(new GetAllServicesQuery(paginationParams));
        return Ok(services);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var service = await mediator.Send(new GetServiceByIdQuery(id));
        return Ok(service);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetByMe()
    {
        var userId = GetUserId(); // helper method
        var services = await mediator.Send(new GetServicesByUserIdQuery(userId));
        return Ok(services);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateServiceRequestCommand command)
    {
        command.UserId = GetUserId();
        var service = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = service.Id }, service);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateServiceCommand command)
    {
        command.Id = id;
        var service = await mediator.Send(command);
        return Ok(service);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await mediator.Send(new DeleteServiceCommand(id));
        return NoContent();
    }

    private Guid GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(userIdClaim, out var userId)
            ? userId
            : throw new UnauthorizedAccessException("Invalid user ID in token.");
    }

}

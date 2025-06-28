using gizmogeo.Application.CompletedOrders.Commands;
using gizmogeo.Application.CompletedOrders.Commands.PatchNotes;
using gizmogeo.Application.CompletedOrders.Querys.GetAllCompletedOrders;
using gizmogeo.Application.CompletedOrders.Querys.GetCompletedOrdersByUsers;
using gizmogeo.Application.CompletedOrders.Querys.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace gizmogeo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Manager")]
public class CompletedOrderController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await mediator.Send(new GetAllCompletedOrdersQuery());
        return Ok(orders);
    }

    [Authorize]
    [HttpGet("by-user")]
    public async Task<IActionResult> GetByUser()
    {
        var userId = GetUserId();
        var orders = await mediator.Send(new GetCompletedOrdersByUsersQuery(userId));
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompletedOrder([FromForm] CreateCompletedOrderCommand command)
    {
        command.CompletedByUserId = GetUserId();
        var order = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await mediator.Send(new GetOrderByIdQuery(id));
        return Ok(order);
    }
    [HttpPatch("{Id:guid}/notes")]
    public async Task<IActionResult> PatchNotes(Guid Id, PatchNotesCommand command)
    {
        command.Id = Id;
        var patchedCompletedOrder = await mediator.Send(command);
        return Ok(patchedCompletedOrder);
    }
    //[HttpPatch("{Id:guid}/attachments")]

    private Guid GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            throw new UnauthorizedAccessException("Invalid or missing user ID in token.");

        return userId;
    }
}

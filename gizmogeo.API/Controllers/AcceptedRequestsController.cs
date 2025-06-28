using gizmogeo.Application.AcceptedRequests.Commands.CreateAcceptedRequest;
using gizmogeo.Application.AcceptedRequests.Commands.DeleteAcceptedRequest;
using gizmogeo.Application.AcceptedRequests.Commands.UpdateAcceptedRequest;
using gizmogeo.Application.AcceptedRequests.Querys.GetAcceptedRequestById;
using gizmogeo.Application.AcceptedRequests.Querys.GetAcceptedRequestsByAdminOrManagerId;
using gizmogeo.Application.AcceptedRequests.Querys.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace gizmogeo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Manager")]
public class AcceptedRequestsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllAcceptedRequestsQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetAcceptedRequestByIdQuery(id));
        return Ok(result);
    }

    [HttpGet("by-employee")]
    public async Task<IActionResult> GetByEmployeeId()
    {
        var result = await mediator.Send(new GetAcceptedRequestsByAdminOrManagerIdQuery(GetUserId()));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateAcceptedRequestCommand command)
    {
        command.UserId = GetUserId();
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAcceptedRequestCommand command)
    {
        command.Id = id;
        command.UserId = GetUserId();
        var UpdatedAcceptedRequest = await mediator.Send(command);
        return Ok(UpdatedAcceptedRequest);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await mediator.Send(new DeleteAcceptedRequestCommand(id));
        return NoContent();
    }

    private Guid GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            throw new UnauthorizedAccessException("Invalid or missing user ID in token.");

        return userId;
    }
}

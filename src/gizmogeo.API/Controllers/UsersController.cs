using gizmogeo.Application.common;
using gizmogeo.Application.Users.Commands.AssignRole;
using gizmogeo.Application.Users.Commands.CreateUser;
using gizmogeo.Application.Users.Commands.DeleteUser;
using gizmogeo.Application.Users.Commands.UpdateUser;
using gizmogeo.Application.Users.Querys.GetAllUsers;
using gizmogeo.Application.Users.Querys.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gizmogeo.API.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleCommand command)
        {
            var user = await mediator.Send(command);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] PaginationParams paginationParams)
        {
            var result = await mediator.Send(new GetAllUsersQuery(paginationParams));
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await mediator.Send(new GetUserByIdQuery(id));
            return Ok(user);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await mediator.Send(new DeleteUserCommand(id));
            return NoContent();
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateUserCommand command)
        {
            command.Id = id;
            var user = await mediator.Send(command);
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            var User = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = User.Id }, User);
        }
    }
}

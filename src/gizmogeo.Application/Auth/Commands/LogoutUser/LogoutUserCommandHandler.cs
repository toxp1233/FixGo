using gizmogeo.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace gizmogeo.Application.Auth.Commands.LogoutUser;

public class LogoutUserCommandHandler(IUserRepository _userRepository, IHttpContextAccessor _httpContextAccessor) : IRequestHandler<LogoutUserCommand>
{
    public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var userIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdString is null || !Guid.TryParse(userIdString, out var userId))
        {
            throw new Exception();
        }

        var user = await _userRepository.GetByIdAsync(userId);
        if (user is not null)
        {
            user.RefreshToken = null;
            user.RefreshTokenExpireDate = null;
            await _userRepository.UpdateAsync(user);
        }
    }
}


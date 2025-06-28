using AutoMapper;
using gizmogeo.Application.Auth.Commands.TokenCommands.GenerateToken;
using gizmogeo.Application.Auth.Commands.TokenCommands.ValidateRefreshToken;
using gizmogeo.Application.Auth.Dtos;
using MediatR;

namespace gizmogeo.Application.Auth.Commands.TokenCommands.RefreshToken;

public class RefreshTokenCommandHandler(IMediator mediator, IMapper mapper) : IRequestHandler<RefreshTokenCommand, TokenDto>
{
    public async Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await mediator.Send(new ValidateRefreshTokenCommand(request.UserId, request.RefreshToken)) ?? throw new Exception();
        var verifiedToken = mapper.Map<TokenDto>(user);
        verifiedToken.Token = await mediator.Send(new GenerateTokenCommand(user));
        return verifiedToken;
    }
}

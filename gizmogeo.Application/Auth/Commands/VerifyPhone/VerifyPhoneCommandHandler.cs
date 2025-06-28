using AutoMapper;
using gizmogeo.Application.Auth.Commands.RecoveryCodeHasher;
using gizmogeo.Application.Auth.Commands.TokenCommands.GenerateRefreshToken;
using gizmogeo.Application.Auth.Commands.TokenCommands.GenerateToken;
using gizmogeo.Application.Auth.Dtos;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace gizmogeo.Application.Auth.Commands.VerifyPhone;

public class VerifyPhoneCommandHandler(
    IUserRepository userRepository,
    IPhoneVerificationService phoneVerificationService,
    IMediator mediator,
    IMapper mapper
) : IRequestHandler<VerifyPhoneCommand, TokenDto>
{
    public async Task<TokenDto> Handle(VerifyPhoneCommand request, CancellationToken cancellationToken)
    {
        // 1. Check user exists
        var user = await userRepository.GetByPhoneNumberAsync(request.PhoneNumber);
        if (user is null)
            throw new NotFoundException(nameof(User), request.PhoneNumber);

        // 2. Verify code with Twilio
        var isVerified = await phoneVerificationService.VerifyAsync(request.PhoneNumber, request.Code);
        if (!isVerified)
            throw new ValidationException("Invalid verification code");

        // ✅ 3. Update phone number if it's a reset verification
        if (!string.IsNullOrWhiteSpace(user.PendingPhoneNumber))
        {
            user.PhoneNumber = user.PendingPhoneNumber;
            user.PendingPhoneNumber = null;
        }
        // 3. Update user
        user.IsPhoneNumberConfirmed = true;

        // 4. Generate tokens

        var refreshToken = await mediator.Send(new GenerateRefreshTokenCommand(user));
        string? recoveryCode = null;

        if (string.IsNullOrWhiteSpace(user.RecoveryCodeHash))
        {
            recoveryCode = await mediator.Send(new RecoveryCodeHasherCommand(user), cancellationToken);
        }

        user.RefreshToken = refreshToken;
        await userRepository.UpdateAsync(user); 
        var result = mapper.Map<TokenDto>(user);
        result.Token = await mediator.Send(new GenerateTokenCommand(user));
        result.RecoveryCode = recoveryCode;

        return result;
    }
}

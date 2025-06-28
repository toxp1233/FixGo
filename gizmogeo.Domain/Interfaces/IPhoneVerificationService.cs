namespace gizmogeo.Domain.Interfaces;
public interface IPhoneVerificationService
{
    Task SendAsync(string phoneNumber);
    Task<bool> VerifyAsync(string phoneNumber, string code);
}

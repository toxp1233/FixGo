using gizmogeo.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Verify.V2.Service;

namespace gizmogeo.Infrastructure.Services;

public class PhoneVerificationService : IPhoneVerificationService
{
    private readonly string _accountSid;
    private readonly string _authToken;
    private readonly string _verifyServiceSid;

    public PhoneVerificationService(IConfiguration config)
    {
        _accountSid = config["PhoneServiceSettings:AccountSid"]!;
        _authToken = config["PhoneServiceSettings:AuthToken"]!;
        _verifyServiceSid = config["PhoneServiceSettings:VerifyServiceId"]!;

        TwilioClient.Init(_accountSid, _authToken);
    }

    public async Task SendAsync(string phoneNumber)
    {
        var result = await VerificationResource.CreateAsync(
            to: phoneNumber,
            channel: "sms",
            pathServiceSid: _verifyServiceSid
        );

        Console.WriteLine($"Send status: {result.Status}");
    }

    public async Task<bool> VerifyAsync(string phoneNumber, string code)
    {
        var result = await VerificationCheckResource.CreateAsync(
            to: phoneNumber,
            code: code,
            pathServiceSid: _verifyServiceSid
        );

        Console.WriteLine($"Verify status: {result.Status}");
        return result.Status == "approved";
    }
}

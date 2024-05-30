namespace RtpcrCustomerApp.Common.Interfaces
{
    using RtpcrCustomerApp.Common.Models;
    using System.Security.Claims;

    public interface ITokenManager
    {
        string GenerateToken(string userId, string role, string phone, int expirationDuration);
        ClaimsPrincipal GetPrincipal(string token);
        ValidateTokenResult ValidateToken(string token);
    }
}

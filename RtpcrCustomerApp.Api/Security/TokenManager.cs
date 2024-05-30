namespace RtpcrCustomerApp.Api.Security
{
    using Microsoft.IdentityModel.Tokens;
    using RtpcrCustomerApp.BusinessModels.Common;
    using RtpcrCustomerApp.Common.Interfaces;
    using RtpcrCustomerApp.Common.Models;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net;
    using System.Security.Claims;

    public class TokenManager : ITokenManager
    {
        //private static string Secret = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";
        private const string Secret = "EAFfaSDFAfvfaerTHTURYyrtyjkrJdfhjDddsfgDFGH56w5STHRJ6RTKRfjDU3fSg4GSa3ASG4t4HSa4DSGhJU==";

        /// <summary>
        /// Generates auth token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="expirationDuration"></param>
        /// <returns></returns>
        public string GenerateToken(string userId, string role, string phone, int expirationDuration)
        {
            expirationDuration = expirationDuration > 0 ? expirationDuration : 1000000;
            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, userId),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.MobilePhone, phone),
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddMinutes(expirationDuration).ToFileTimeUtc().ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(expirationDuration),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null) return null;
                byte[] key = Convert.FromBase64String(Secret);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out securityToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        public ValidateTokenResult ValidateToken(string token)
        {
            var result = new ValidateTokenResult { Role = Role.NoAccess };
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    result.Message = "Authorization token is missing";
                    result.StatusCode = HttpStatusCode.Unauthorized;
                    return result;
                }


                var principal = GetPrincipal(token);

                if (principal == null)
                {
                    result.Message = "Invalid or no authorization token";
                    result.StatusCode = HttpStatusCode.Unauthorized;
                    return result;
                }

                var expiry = DateTime.FromFileTimeUtc(Convert.ToInt64(principal.FindFirst(ClaimTypes.Expiration).Value));

                if (DateTime.UtcNow >= expiry)
                {
                    result.Message = "Authorization token has expired";
                    result.StatusCode = HttpStatusCode.Unauthorized;
                    return result;
                }

                result.Role = (Role)Enum.Parse(typeof(Role), principal.FindFirst(ClaimTypes.Role).Value);
                result.StatusCode = HttpStatusCode.OK;
                // Validate action from ActionContext is authorized

                return result;
            }
            catch
            {
                result.StatusCode = HttpStatusCode.Unauthorized;
                result.Message = "Something went wrong";
                return result;
            }
        }
    }
}
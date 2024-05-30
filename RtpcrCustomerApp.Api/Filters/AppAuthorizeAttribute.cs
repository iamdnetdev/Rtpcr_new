namespace RtpcrCustomerApp.Api.Filters
{
    using RtpcrCustomerApp.Api.Models;
    using RtpcrCustomerApp.Api.Security;
    using RtpcrCustomerApp.BusinessModels.Common;
    using RtpcrCustomerApp.Common.Interfaces;
    using RtpcrCustomerApp.Common.Models;
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    public class AppAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly ITokenManager tokenManager;
        private RoleDefinition roleDefinition;
        public AppAuthorizeAttribute(ITokenManager tokenManager)
        {
            this.tokenManager = tokenManager;
        }

        public override void OnAuthorization(HttpActionContext context)
        {
            if (context.ActionDescriptor.GetCustomAttributes<NonAuthorizeAttribute>().Any())
            {
                // The controller action is annotated with the [NonAuthorize] custom attribute, don't do anything.
                return;
            }

            var token = context.Request.Headers?.Authorization?.Parameter;
            var scheme = context.Request.Headers?.Authorization?.Scheme ?? string.Empty;

            if (!scheme.Equals("bearer", StringComparison.InvariantCultureIgnoreCase))
            {
                context.Response = BuildMessage(new ValidateTokenResult { StatusCode = HttpStatusCode.Unauthorized, Message = "Authorization bearer token is missing" });
                return;
            }

            var validationResult = tokenManager.ValidateToken(token);

            if (validationResult.StatusCode != HttpStatusCode.OK)
            {
                context.Response = BuildMessage(validationResult);
                return;
            }
            if (!IsAuthorized(context, validationResult.Role, ref validationResult))
            {
                context.Response = BuildMessage(validationResult);
                return;
            }
        }

        private HttpResponseMessage BuildMessage(ValidateTokenResult validationResult)
        {
            return new HttpResponseMessage
            {
                StatusCode = validationResult.StatusCode,
                Content = new StringContent(validationResult.Message)
            };
        }

        private bool IsAuthorized(HttpActionContext actionContext, Role role, ref ValidateTokenResult validateTokenResult)
        {
            roleDefinition = RoleDefinitionCache.Data[role];
            var endpoint = actionContext.Request.RequestUri.LocalPath.TrimStart('/').TrimEnd('/');
            if (!roleDefinition.AuthorizedEndpoints.Contains(endpoint))
            {
                validateTokenResult.Message = "You are not authorized to access this resource";
                validateTokenResult.StatusCode = HttpStatusCode.Unauthorized;
                return false;
            }
            return true;
        }
    }
}
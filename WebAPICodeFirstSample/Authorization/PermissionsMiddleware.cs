using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPICodeFirstSample.Utils.Auth;

namespace WebAPICodeFirstSample.Authorization
{
    public class PermissionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PermissionsMiddleware> _logger;

        public PermissionsMiddleware(RequestDelegate next, ILogger<PermissionsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IUserPermissionService permissionService)
        {
            if (context.User.Identity == null || !context.User.Identity.IsAuthenticated)
            {
                await _next(context);
                return;
            }

            var cancellationToken = context.RequestAborted;

            var email = context.User.FindFirst(StandardJwtClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                await context.WriteAccessDeniedResponse("User 'email' claim is required", cancellationToken: cancellationToken);
                return;
            }

            var permissionsIdentity = await permissionService.GetUserPermissionsIdentity(email, cancellationToken);
            if (permissionsIdentity == null)
            {
                _logger.LogWarning("User {sub} does not have permissions", email);

                await context.WriteAccessDeniedResponse(cancellationToken: cancellationToken);
                return;
            }

            // User has permissions, so we add the extra identity containing the "permissions" claims
            context.User.AddIdentity(permissionsIdentity);
            await _next(context);
        }
    }
}

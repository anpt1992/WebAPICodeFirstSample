﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICodeFirstSample.Utils.Auth.PolicyProvider
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (requirement.PermissionOperator == PermissionOperator.And)
            {
                foreach (var permission in requirement.Permissions)
                {
                    if (!context.User.HasClaim(PermissionRequirement.ClaimType, permission))
                    {
                        context.Fail();
                        return Task.CompletedTask;
                    }
                }

                // identity has all required permissions
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            foreach (var permission in requirement.Permissions)
            {
                if (context.User.HasClaim(PermissionRequirement.ClaimType, permission))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }

            // identity does not have any of the required permissions
            context.Fail();
            return Task.CompletedTask;
        }
    }
}
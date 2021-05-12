using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using WebAPICodeFirstSample.Models;
using WebAPICodeFirstSample.Utils.Auth;

namespace WebAPICodeFirstSample.Authorization
{
    public interface IUserPermissionService
    {
        /// <summary>
        /// Returns a new identity containing the user permissions as Claims
        /// </summary>
        /// <param name="email">The user external id (sub claim)</param>
        /// <param name="cancellationToken"></param>
        ValueTask<ClaimsIdentity?> GetUserPermissionsIdentity(string email, CancellationToken cancellationToken);
    }

    public class UserPermissionService : IUserPermissionService
    {
        private readonly ApplicationContext _dbContext;

        public UserPermissionService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<ClaimsIdentity?> GetUserPermissionsIdentity(string email, CancellationToken cancellationToken)
        {
            var userPermissions = await
                (from up in _dbContext.AccountPermissions
                 join perm in _dbContext.Permissions on up.PermissionId equals perm.Id
                 join account in _dbContext.Accounts on up.AccountId equals account.Id
                 where account.Email == email
                 select new Claim(AppClaimTypes.Permissions, perm.Name)).ToListAsync(cancellationToken);

            return CreatePermissionsIdentity(userPermissions);
        }

        private static ClaimsIdentity? CreatePermissionsIdentity(IReadOnlyCollection<Claim> claimPermissions)
        {
            if (!claimPermissions.Any())
                return null;

            var permissionsIdentity = new ClaimsIdentity(nameof(PermissionsMiddleware), "name", "role");
            permissionsIdentity.AddClaims(claimPermissions);

            return permissionsIdentity;
        }
    }
}


using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebAPICodeFirstSample.Models;
using WebAPICodeFirstSample.Models.Repositories;
using WebAPICodeFirstSample.Services.Base;

namespace WebAPICodeFirstSample.Services
{
    public interface IAccountService : IBaseService<ApplicationUser>
    {
        //string CreateToken(ApplicationUser account);
        //ApplicationUser GetValidCredentialAccount(string email, string password);
    }
    public class AccountService : BaseService<ApplicationUser>, IAccountService
    {
        private const double EXPIRE_HOURS = 1.0;
        private readonly IConfiguration _config;
        public AccountService(IBaseRepository<ApplicationUser> repo, IConfiguration config) : base(repo)
        {
            _config = config;
        }
        //public ApplicationUser GetValidCredentialAccount(string email, string password)
        //{
        //    return _repo.GetIQueryable().Where(x => x.Email == email && x.Password == CommonFunctions.HashPassword(password)).FirstOrDefault();
        //}
        //public string CreateToken(ApplicationUser account)
        //{
        //    var key = Encoding.ASCII.GetBytes(_config["JWT:Secret"]);
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var descriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
        //            new Claim(ClaimTypes.Email, account.Email),
        //            new Claim(ClaimTypes.Role, account.Role)
        //        }),
        //        Expires = DateTime.UtcNow.AddHours(EXPIRE_HOURS),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(descriptor);
        //    return tokenHandler.WriteToken(token);
        //}
        //public override ApplicationUser Insert(ApplicationUser account)
        //{
        //    account.Password = CommonFunctions.HashPassword(account.Password);
        //    return _repo.Insert(account);
        //}

    }
}

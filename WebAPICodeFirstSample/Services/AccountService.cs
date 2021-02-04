
using System.Collections.Generic;
using WebAPICodeFirstSample.Models;
using WebAPICodeFirstSample.Models.Repository;
using WebAPICodeFirstSample.Services.Base;

namespace WebAPICodeFirstSample.Services
{
    public interface IAccountService : IBaseService<Account>
    {
        
    }
    public class AccountService : BaseService<Account>,IAccountService
    {
        public AccountService(IBaseRepository<Account> repo) : base(repo)
        {
        }        
    }
}

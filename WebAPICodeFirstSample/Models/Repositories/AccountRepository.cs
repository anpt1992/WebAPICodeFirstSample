using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICodeFirstSample.Models.Repositories
{
    public class AccountRepository : BaseRepository<Account>
    {
        public AccountRepository(ApplicationContext dbContenxt) : base(dbContenxt)
        {
          
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICodeFirstSample.Models.Repositories
{
    public class AccountRepository : BaseRepository<ApplicationUser>
    {
        public AccountRepository(ApplicationDbContext  dbContenxt) : base(dbContenxt)
        {
          
        }
    }
}

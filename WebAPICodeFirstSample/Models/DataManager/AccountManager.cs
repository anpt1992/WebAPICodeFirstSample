using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPICodeFirstSample.Models.Repository;

namespace WebAPICodeFirstSample.Models.DataManager
{
    public class AccountManager : IDataRepository<Account, long>
    {
        ApplicationContext ctx;
        public AccountManager(ApplicationContext c)
        {
            ctx = c;
        }

        public Account Get(long id)
        {
            var student = ctx.Users.FirstOrDefault(b => b.Id == id);
            return student;
        }

        public IEnumerable<Account> GetAll()
        {
            var users = ctx.Users.ToList();
            return users;
        }

        public long Add(Account user)
        {
            ctx.Users.Add(user);
            long studentID = ctx.SaveChanges();
            return studentID;
        }

        public long Delete(long id)
        {
            int iD = 0;
            var user = ctx.Users.FirstOrDefault(b => b.Id == id);
            if (user != null)
            {
                ctx.Users.Remove(user);
                iD = ctx.SaveChanges();
            }
            return iD;
        }

        public long Update(long id, Account item)
        {
            long iD = 0;
            var user = ctx.Users.Find(id);
            if (user != null)
            {
                user.FirstName = item.FirstName;
                user.LastName = item.LastName;
                user.Gender = item.Gender;               

                iD = ctx.SaveChanges();
            }
            return iD;
        }
    }
}

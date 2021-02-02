using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPICodeFirstSample.Models.Repository;

namespace WebAPICodeFirstSample.Models.DataManager
{
    public class UserManager : IDataRepository<User, long>
    {
        ApplicationContext ctx;
        public UserManager(ApplicationContext c)
        {
            ctx = c;
        }

        public User Get(long id)
        {
            var student = ctx.Users.FirstOrDefault(b => b.Id == id);
            return student;
        }

        public IEnumerable<User> GetAll()
        {
            var users = ctx.Users.ToList();
            return users;
        }

        public long Add(User user)
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

        public long Update(long id, User item)
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

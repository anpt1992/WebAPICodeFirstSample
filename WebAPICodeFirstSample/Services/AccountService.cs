using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICodeFirstSample.Services
{
    public interface IAccountService
    {
        List<string> GetAll();
    }
    public class AccountService : IAccountService
    {
        public List<string> GetAll()
        {
            return new List<string>
            {
                "Pen Drive",
                "Memory Card",
                "Mobile Phone",
                "Tablet",
                "Desktop PC",
            };
        }
    }
}

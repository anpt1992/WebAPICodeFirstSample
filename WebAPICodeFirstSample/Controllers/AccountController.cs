using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPICodeFirstSample.Models;
using WebAPICodeFirstSample.Models.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPICodeFirstSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IDataRepository<Account, long> _iRepo;
        public AccountController(IDataRepository<Account, long> repo)
        {
            _iRepo = repo;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return _iRepo.GetAll();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public Account Get(int id)
        {
            return _iRepo.Get(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] Account user)
        {
            _iRepo.Add(user);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] Account user)
        {
            _iRepo.Update(user.Id, user);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _iRepo.Delete(id);
        }
    }
}

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
    public class UserController : ControllerBase
    {
        private IDataRepository<User, long> _iRepo;
        public UserController(IDataRepository<User, long> repo)
        {
            _iRepo = repo;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _iRepo.GetAll();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _iRepo.Get(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] User user)
        {
            _iRepo.Add(user);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] User user)
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

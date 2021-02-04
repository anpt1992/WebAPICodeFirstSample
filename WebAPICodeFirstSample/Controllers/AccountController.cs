using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPICodeFirstSample.Models;
using WebAPICodeFirstSample.Models.Repositories;
using WebAPICodeFirstSample.Models.Requests;
using WebAPICodeFirstSample.Models.Responses;
using WebAPICodeFirstSample.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPICodeFirstSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<IAccountService>
    {

        public AccountController(IAccountService service, IMapper mapper) : base(service, mapper)
        {
        }
        [HttpPost]
        [Route("login")]
        public ActionResult<ResponseWrapper> Login([FromBody] LoginRequest request)
        {
            var account = _service.GetValidCredentialAccount(request.Email, request.Password);
            if (account != null)
            {
                return ok_get(new LoginResponse
                {
                    Email = account.Email,
                    DisplayName = $"{account.FirstName} {account.LastName}",
                    RoleName = account.Role,
                    Token = _service.CreateToken(account)
                }) ;
            }           
            return Unauthorized("Login fail!");
        }
        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<ResponseWrapper> Get()
        {
            var result = _service.GetAll().ToList();
            return ok_get(result);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult<ResponseWrapper> Get(int id)
        {
            var result = _service.GetById(id);
            return ok_get(result);
        }

        // POST api/<UserController>
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult<ResponseWrapper> Post([FromBody] Account account)
        {
            var result = _service.Insert(account);
            return ok_create(result);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public ActionResult<ResponseWrapper> Put([FromBody] Account account)
        {
            _service.Update(account,account.Id);
            return ok_update();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public ActionResult<ResponseWrapper> Delete(int id)
        {
            _service.Delete(id);
            return ok_delete();
        }
    }
}

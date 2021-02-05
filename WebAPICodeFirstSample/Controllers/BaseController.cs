using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAPICodeFirstSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        protected readonly T _service;
        protected readonly IMapper _mapper;

        public BaseController(T service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        protected ResponseWrapper error(HttpStatusCode code, string msg)
        {
            Response.StatusCode = (int)code;
            return new ResponseWrapper(code, msg);
        }

        protected ResponseWrapper error(string msg)
        {
            return error(HttpStatusCode.InternalServerError, msg);
        }

        protected ResponseWrapper ok_get(object data, Meta meta = null)
        {
            Response.StatusCode = (int)HttpStatusCode.OK;
            return new ResponseWrapper(HttpStatusCode.OK, data, meta);
        }

        protected ResponseWrapper ok_create(object data)
        {
            Response.StatusCode = (int)HttpStatusCode.Created;
            return new ResponseWrapper(HttpStatusCode.Created, data);
        }

        protected ResponseWrapper ok_update()
        {
            return new ResponseWrapper(HttpStatusCode.OK);
        }

        protected ResponseWrapper ok_delete()
        {
            return new ResponseWrapper(HttpStatusCode.NoContent, "Delete successful");
        }

        protected ResponseWrapper notFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return new ResponseWrapper(HttpStatusCode.NotFound, "Not found");
        }

        protected ResponseWrapper notFound(string msg)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return new ResponseWrapper(HttpStatusCode.NotFound, msg);
        }

        protected ResponseWrapper badRequest(string msg)
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return new ResponseWrapper(HttpStatusCode.BadRequest, msg);
        }

        protected string GetModelStateErrMsg()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    stringBuilder.Append(error.ErrorMessage);
                }
            }
            return stringBuilder.ToString();
        }

        protected bool IsAdmin()
        {
            return GetRoleNameFromToken() == "admin";
        }

        protected long GetAccountIdFromToken()
        {
            try
            {
                return Convert.ToInt64(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            }
            catch (Exception)
            {
                throw new Exception("Access token is obsolete. " +
                    "Please, request a new access token then try again.");
            }
        }
        protected string GetRoleNameFromToken()
        {
            return User?.FindFirst(ClaimTypes.Role)?.Value;
        }

        protected string GetUsernameFromToken()
        {
            return User?.FindFirst(ClaimTypes.Name)?.Value;
        }

        protected ActionResult<ResponseWrapper> error_operation_forbidden()
        {
            return error(HttpStatusCode.Forbidden, "Operation forbidden. " +
                "Make sure that your access token containing this operation execution right ");
        }

        protected ActionResult<ResponseWrapper> error_invalid_user()
        {
            return error(HttpStatusCode.Forbidden, "Invalid user. " +
                "Make sure that you have valid access token");
        }

        protected string GetEmailFromToken()
        {
            return User?.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}

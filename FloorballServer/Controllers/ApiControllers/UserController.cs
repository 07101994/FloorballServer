using FloorballServer.Attributes;
using FloorballServer.Models.Floorball;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bll.Repository;

namespace FloorballServer.Controllers.ApiControllers
{
    [FloorballExceptionFilter]
    [RoutePrefix("api/floorball")]
    public class UserController : BaseApiController
    {
        public UserController(IUnitOfWork UoW) : base(UoW)
        {
        }

        [Route("register")]
        [HttpGet]
        public HttpResponseMessage Register(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var result = UoW.UserRepository.CreateUser(userModel.UserName, userModel.Password, userModel.UserRole);
                
                if (result.Succeeded)
                {
                    return Request.CreateResponse(HttpStatusCode.Created);
                }

                throw new Exception("Error during created user");

            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }



    }
}

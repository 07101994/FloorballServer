using Bll.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FloorballServer.Controllers.ApiControllers
{
    //[RoutePrefix("api/floorball")]
    public abstract class BaseApiController : ApiController
    {
        protected IUnitOfWork UoW;

        public BaseApiController(IUnitOfWork UoW)
        {
            this.UoW = UoW;
        }

    }
}

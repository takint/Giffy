using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Giffy.DataAccess.Infrastructure.Identity;
using Giffy.DataAccess.Models;
using Giffy.DataAccess.Repositories;

namespace Giffy.ApiControllers
{
    public class BaseApiController : ApiController
    {
        protected AuthRepository _repo = null;
        private GiffyUserManager _userManager = null;
        private GiffyRoleManager _roleManager = null;

        protected GiffyUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<GiffyUserManager>();
            }
        }

        protected GiffyRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? Request.GetOwinContext().GetUserManager<GiffyRoleManager>();
            }
        }

        protected IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        public BaseApiController()
        {
            _repo = new AuthRepository();
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}

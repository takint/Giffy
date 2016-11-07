using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using Giffy.DataAccess.Models;
using Giffy.DataAccess.Repositories;
using Giffy.DataAccess.Services;

namespace Giffy.ApiControllers
{
    public class TeamController : ApiController
    {
        private readonly ITeamService teamService;

        public TeamController(ITeamService teamService)
        {
            this.teamService = teamService;
        }
        
        [HttpPost]
        [Authorize]
        [Route("api/team/createorupdate")]
        public async Task<GetTeamDTO> CreateOrUpdateTeam(NewTeamDTO newTeam)
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userName = ClaimsPrincipal.Current.Identity.Name;
            return await teamService.CreateOrUpdateTeam(newTeam, userName);
        }

        [Route("api/team/getteams")]
        public object GetTeams(int skip, int take)
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            string userName = ClaimsPrincipal.Current.Identity.Name;
            int count = 0;
            var teams = teamService.GetTeams(skip, take, userName, ref count);
            return new { count = count, teams = teams};
        }
    }
}

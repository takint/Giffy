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
    public class LeagueController : ApiController
    {
        private readonly ILeagueService leagueService;

        public LeagueController(ILeagueService leagueService)
        {
            this.leagueService = leagueService;
        }
        
        [HttpPost]
        [Authorize]
        [Route("api/league/createorupdate")]
        public async Task<GetLeagueDTO> CreateOrUpdateLeague(NewLeagueDTO newLeague)
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userName = ClaimsPrincipal.Current.Identity.Name;
            return await leagueService.CreateOrUpdateLeague(newLeague, userName);
        }

        [Route("api/league/getleagues")]
        public object GetLeagues(int skip, int take)
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            string userName = ClaimsPrincipal.Current.Identity.Name;
            int count = 0;
            var leagues = leagueService.GetLeagues(skip, take, userName, ref count);
            return new { count = count, leagues = leagues};
        }
    }
}

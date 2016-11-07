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
    public class PlayerController : ApiController
    {
        private readonly IPlayerService playerService;

        public PlayerController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }
        
        [HttpPost]
        [Authorize]
        [Route("api/player/createorupdate")]
        public async Task<GetPlayerDTO> CreateOrUpdatePlayer(NewPlayerDTO newPlayer)
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userName = ClaimsPrincipal.Current.Identity.Name;
            return await playerService.CreateOrUpdatePlayer(newPlayer, userName);
        }

        [Route("api/player/getplayers")]
        public object GetPlayers(int skip, int take)
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            string userName = ClaimsPrincipal.Current.Identity.Name;
            int count = 0;
            var players = playerService.GetPlayers(skip, take, userName, ref count);
            return new { count = count, players = players};
        }
    }
}

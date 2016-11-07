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
using Giffy.Entities.Models;

namespace Giffy.ApiControllers
{
    public class LikeController : ApiController
    {
        private readonly ILikeService likeService;

        public LikeController(ILikeService likeService)
        {
            this.likeService = likeService;
        }

        [Route("api/like/getlikes")]
        public async Task<IEnumerable<GetLikeDTO>> GetLikes(int skip, int take, ActionFor actionFor, int actionForId)
        {
            return await likeService.GetLikes(skip, take, actionFor, actionForId);
        }

        [HttpPost]
        [Authorize]
        [Route("api/like/toggle")]
        public async Task<GetLikeDTO> ToggleLike(NewLikeDTO newLike)
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userName = ClaimsPrincipal.Current.Identity.Name;
            newLike.ActionFor = ActionFor.Post;
            return await likeService.ToggleLike(newLike, userName);
        }
    }
}

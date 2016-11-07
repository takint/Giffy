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
    public class CommentController : ApiController
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [Route("api/comment/getcomments")]
        public async Task<IEnumerable<GetCommentDTO>> GetComments(int skip, int take, ActionFor actionFor, int actionForId)
        {
            return await commentService.GetComments(skip, take, actionFor, actionForId);
        }

        [HttpPost]
        [Authorize]
        [Route("api/comment/create")]
        public async Task<GetCommentDTO> CreateComment(NewCommentDTO newComment)
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userName = ClaimsPrincipal.Current.Identity.Name;
            newComment.ActionFor = ActionFor.Post;
            return await commentService.CreateComment(newComment, userName);
        }

    }
}

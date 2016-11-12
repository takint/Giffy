using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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
    public class PostController : ApiController
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }
        
        [HttpPost]
        [Authorize]
        [Route("api/post/create")]
        public async Task<string> CreatePost(NewPostDTO newPost)
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userName = ClaimsPrincipal.Current.Identity.Name;
            return await postService.CreatePost(newPost, userName);
        }

        [HttpPost]
        [Authorize]
        [Route("api/post/update")]
        public async Task<string> UpdatePost(NewPostDTO updatePost)
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            string userName = ClaimsPrincipal.Current.Identity.Name;

            return await postService.UpdatePost(updatePost, userName);
        }

        [HttpPost]
        [Authorize]
        [Route("api/post/delete")]
        public async Task<string> DeletePost(NewPostDTO deletePost)
        {
            Account account = new Account(
                  "doua5pgdi",
                  "696442329793211",
                  "7eVrRtWC2xJ68tiNIAmBPb93bOU");

            Cloudinary cloudinary = new Cloudinary(account);
            DelResParams delRes = new DelResParams()
            {
                PublicIds =  deletePost.Images.Where(i => i.Url != null).Select(i => i.Url.Split('/').Last()).ToList()
            };

            DelResResult results = await cloudinary.DeleteResourcesAsync(delRes);

            return await postService.DeletePost(deletePost);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("api/post/approval")]
        public async Task<string> ApprovalPost(NewPostDTO approvePost)
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            string userName = ClaimsPrincipal.Current.Identity.Name;

            return await postService.UpdatePost(approvePost, userName);
        }

        [Route("api/post/getallgags")]
        public async Task<IEnumerable<GetGagDTO>> GetAllGags(int skip, int take)
        {
            return await postService.GetAllGags(skip, take);
        }

        [Route("api/post/getgags")]
        public async Task<IEnumerable<GetGagDTO>> GetGags(int skip, int take, string userFilter, GagFilter filter = GagFilter.All, string slug = "")
        {
            return await postService.GetGags(skip, take, userFilter, filter, slug);
        }

        [Route("api/post/getgag")]
        public async Task<GetGagDTO> GetGag(string slug)
        {
            return await postService.GetGagAsync(slug);
        }
    }
}

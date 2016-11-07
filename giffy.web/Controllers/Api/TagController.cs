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
    public class TagController : ApiController
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [Route("api/tags/gettagsautocomplete")]
        public async Task<IEnumerable<TagDTO>> GetTagsAutoComplete(string searchTerm, int take, int skip)
        {
            return await _tagService.SearchTags(searchTerm, take, skip);
        }

        [Route("api/tags/gettoptags")]
        public async Task<IEnumerable<TagDTO>> GetTopTags(int top, TagType type)
        {
            return await _tagService.GetTop(top, type);
        }

        [Route("api/tags/gettag")]
        public async Task<TagDTO> GetTag(string slug)
        {
            return await _tagService.GetTag(slug);
        }
    }
}

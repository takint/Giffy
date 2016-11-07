using System.Collections.Generic;
using System.Threading.Tasks;
using Giffy.DataAccess.Infrastructure;
using Giffy.DataAccess.Models;
using Giffy.Entities.Models;

namespace Giffy.DataAccess.Services
{
    public interface IPostService : IService<Post>
    {
        Task<IEnumerable<GetGagDTO>> GetAllGags(int skip, int take);
        Task<IEnumerable<GetGagDTO>> GetGags(int skip, int take, string userFilter, GagFilter filter, string slug);
        Task<GetGagDTO> GetGagAsync(string slug);
        GetGagDTO GetGag(string slug);
        Task<string> CreatePost(NewPostDTO newPost, string userName);
        Task<string> UpdatePost(NewPostDTO updatePost, string userName);
        Task<string> DeletePost(NewPostDTO deletePost);
    }
}

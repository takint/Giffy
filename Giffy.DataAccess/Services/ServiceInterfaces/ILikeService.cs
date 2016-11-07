using System.Collections.Generic;
using System.Threading.Tasks;
using Giffy.DataAccess.Infrastructure;
using Giffy.DataAccess.Models;
using Giffy.Entities.Models;

namespace Giffy.DataAccess.Services
{
    public interface ILikeService : IService<Like>
    {
        Task<IEnumerable<GetLikeDTO>> GetLikes(int skip, int take, ActionFor actionFor, int actionForId);
        Task<GetLikeDTO> ToggleLike(NewLikeDTO newLike, string userName);
    }
}

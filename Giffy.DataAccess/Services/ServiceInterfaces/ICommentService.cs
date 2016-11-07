using System.Collections.Generic;
using System.Threading.Tasks;
using Giffy.DataAccess.Infrastructure;
using Giffy.DataAccess.Models;
using Giffy.Entities.Models;

namespace Giffy.DataAccess.Services
{
    public interface ICommentService : IService<Comment>
    {
        Task<IEnumerable<GetCommentDTO>> GetComments(int skip, int take, ActionFor actionFor, int actionForId);
        Task<GetCommentDTO> CreateComment(NewCommentDTO newComment, string userName);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Giffy.DataAccess.Infrastructure;
using Giffy.DataAccess.Models;
using Giffy.Entities.Models;

namespace Giffy.DataAccess.Services
{
    public interface ITagService : IService<Tag>
    {
        Task<IEnumerable<TagDTO>> SearchTags(string searchTerm, int take, int skip);
        Task<IEnumerable<TagDTO>> GetTop(int top, TagType type);
        Task<TagDTO> GetTag(string slug);
    }
}

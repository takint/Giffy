using System.Collections.Generic;
using System.Threading.Tasks;
using Giffy.DataAccess.Infrastructure;
using Giffy.DataAccess.Models;
using Giffy.Entities.Models;

namespace Giffy.DataAccess.Services
{
    public interface ILeagueService : IService<League>
    {
        IEnumerable<GetLeagueDTO> GetLeagues(int skip, int take, string userName, ref int count);
        Task<GetLeagueDTO> CreateOrUpdateLeague(NewLeagueDTO newLeague, string userName);
    }
}

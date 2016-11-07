using System.Collections.Generic;
using System.Threading.Tasks;
using Giffy.DataAccess.Infrastructure;
using Giffy.DataAccess.Models;
using Giffy.Entities.Models;

namespace Giffy.DataAccess.Services
{
    public interface ITeamService : IService<Team>
    {
        IEnumerable<GetTeamDTO> GetTeams(int skip, int take, string userName, ref int count);
        Task<GetTeamDTO> CreateOrUpdateTeam(NewTeamDTO newTeam, string userName);
    }
}

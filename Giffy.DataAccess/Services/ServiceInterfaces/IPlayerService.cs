using System.Collections.Generic;
using System.Threading.Tasks;
using Giffy.DataAccess.Infrastructure;
using Giffy.DataAccess.Models;
using Giffy.Entities.Models;

namespace Giffy.DataAccess.Services
{
    public interface IPlayerService : IService<Player>
    {
        IEnumerable<GetPlayerDTO> GetPlayers(int skip, int take, string userName, ref int count);
        Task<GetPlayerDTO> CreateOrUpdatePlayer(NewPlayerDTO newPlayer, string userName);
    }
}

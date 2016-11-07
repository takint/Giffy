using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Giffy.DataAccess;
using Giffy.DataAccess.Infrastructure;
using Giffy.DataAccess.Infrastructure.Identity;
using Giffy.Entities.Models;
using Giffy.DataAccess.Models;
using Giffy.DataAccess.Repositories;
using System.Data.Entity;

namespace Giffy.DataAccess.Services
{
    public class PlayerService : Service<Player>, IPlayerService
    {
        private readonly IRepository<Player> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly GiffyIdentityContext _ictx;
        private readonly GiffyUserManager _userManager;

        public PlayerService(IRepository<Player> repository, IUnitOfWork unitOfWork) : base(repository)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            _ictx = new GiffyIdentityContext();
            _userManager = new GiffyUserManager(new UserStore<User>(_ictx));
        }

        public IEnumerable<GetPlayerDTO> GetPlayers(int skip, int take, string userName, ref int count)
        {
            var players = _repository.Queryable()
                .OrderByDescending(l => l.Popular).ThenByDescending(l => l.Level).ThenByDescending(l => l.CreatedDate);
            count = players.Count();
            return players.Select(Mapper.Map<GetPlayerDTO>).Skip(skip).Take(take).ToList();
        }

        public async Task<GetPlayerDTO> CreateOrUpdatePlayer(NewPlayerDTO newPlayer, string userName)
        {
            try
            {
                User user = await _userManager.FindByNameAsync(userName);
                Player player = Mapper.Map<Player>(newPlayer);
                if(player.Id == 0)
                {
                    player.IsActived = false;
                    player.CreatedUserId = user.Id;
                    player.CreatedDate = DateTime.UtcNow;
                    player.UpdatedUserId = user.Id;
                    player.UpdatedDate = DateTime.UtcNow;

                    _repository.Insert(player);
                }
                else
                {
                    player.UpdatedUserId = user.Id;
                    player.UpdatedDate = DateTime.UtcNow;

                    _repository.Update(player);
                }


                var tagRepository = _repository.GetRepository<Tag>();

                var tag = tagRepository.Queryable().FirstOrDefault(t => t.Slug == player.Slug);

                if (tag == null)
                {
                    tag = new Tag
                    {
                        Avatar = player.Avatar,
                        FullName = player.FullName,
                        ShortName = player.ShortName,
                        NickName = player.NickName,
                        Slug = player.Slug,
                        TagType = TagType.Player
                    };

                    tagRepository.Insert(tag);
                }
                else
                {
                    tag.Avatar = player.Avatar;
                    tag.FullName = player.FullName;
                    tag.ShortName = player.ShortName;
                    tag.NickName = player.NickName;
                    tag.Slug = player.Slug;
                    tag.TagType = TagType.Player;

                    tagRepository.Update(tag);
                }

                await _unitOfWork.SaveChangesAsync();

                return Mapper.Map<GetPlayerDTO>(player);
            }
            catch (Exception ex)
            {
                throw new Exception(ResponseCodeString.PlayerCreate_Error, ex.InnerException);
            }
        }
    }
}
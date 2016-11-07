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
    public class LeagueService : Service<League>, ILeagueService
    {
        private readonly IRepository<League> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly GiffyIdentityContext _ictx;
        private readonly GiffyUserManager _userManager;

        public LeagueService(IRepository<League> repository, IUnitOfWork unitOfWork) : base(repository)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            _ictx = new GiffyIdentityContext();
            _userManager = new GiffyUserManager(new UserStore<User>(_ictx));
        }

        public IEnumerable<GetLeagueDTO> GetLeagues(int skip, int take, string userName, ref int count)
        {
            var leagues = _repository.Queryable()
                .OrderByDescending(l => l.Popular).ThenByDescending(l => l.Level).ThenByDescending(l => l.CreatedDate);
            count = leagues.Count();
            return leagues.Skip(skip).Take(take).Select(Mapper.Map<GetLeagueDTO>).ToList();
        }

        public async Task<GetLeagueDTO> CreateOrUpdateLeague(NewLeagueDTO newLeague, string userName)
        {
            try
            {
                User user = await _userManager.FindByNameAsync(userName);
                League league = Mapper.Map<League>(newLeague);
                if(league.Id == 0)
                {
                    league.IsActived = false;
                    league.CreatedUserId = user.Id;
                    league.CreatedDate = DateTime.UtcNow;
                    league.UpdatedUserId = user.Id;
                    league.UpdatedDate = DateTime.UtcNow;

                    _repository.Insert(league);
                }
                else
                {
                    league.UpdatedUserId = user.Id;
                    league.UpdatedDate = DateTime.UtcNow;

                    _repository.Update(league);
                }


                var tagRepository = _repository.GetRepository<Tag>();

                var tag = tagRepository.Queryable().FirstOrDefault(t => t.Slug == league.Slug);

                if (tag == null)
                {
                    tag = new Tag
                    {
                        Avatar = league.Logo,
                        FullName = league.FullName,
                        ShortName = league.ShortName,
                        NickName = league.NickName,
                        Slug = league.Slug,
                        TagType = TagType.League
                    };

                    tagRepository.Insert(tag);
                }
                else
                {
                    tag.Avatar = league.Logo;
                    tag.FullName = league.FullName;
                    tag.ShortName = league.ShortName;
                    tag.NickName = league.NickName;
                    tag.Slug = league.Slug;
                    tag.TagType = TagType.League;

                    tagRepository.Update(tag);
                }

                await _unitOfWork.SaveChangesAsync();

                return Mapper.Map<GetLeagueDTO>(league);
            }
            catch (Exception ex)
            {
                throw new Exception(ResponseCodeString.LeagueCreate_Error, ex.InnerException);
            }
        }
    }
}
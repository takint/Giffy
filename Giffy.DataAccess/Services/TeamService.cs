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
    public class TeamService : Service<Team>, ITeamService
    {
        private readonly IRepository<Team> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly GiffyIdentityContext _ictx;
        private readonly GiffyUserManager _userManager;

        public TeamService(IRepository<Team> repository, IUnitOfWork unitOfWork) : base(repository)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            _ictx = new GiffyIdentityContext();
            _userManager = new GiffyUserManager(new UserStore<User>(_ictx));
        }

        public IEnumerable<GetTeamDTO> GetTeams(int skip, int take, string userName, ref int count)
        {
            var teams = _repository.Queryable()
                .OrderByDescending(l => l.Popular).ThenByDescending(l => l.Level).ThenByDescending(l => l.CreatedDate);
            count = teams.Count();
            return teams.Skip(skip).Take(take).Select(Mapper.Map<GetTeamDTO>).ToList();
        }

        public async Task<GetTeamDTO> CreateOrUpdateTeam(NewTeamDTO newTeam, string userName)
        {
            try
            {
                User user = await _userManager.FindByNameAsync(userName);
                Team team = Mapper.Map<Team>(newTeam);
                if(team.Id == 0)
                {
                    team.IsActived = false;
                    team.CreatedUserId = user.Id;
                    team.CreatedDate = DateTime.UtcNow;
                    team.UpdatedUserId = user.Id;
                    team.UpdatedDate = DateTime.UtcNow;

                    _repository.Insert(team);
                }
                else
                {
                    team.UpdatedUserId = user.Id;
                    team.UpdatedDate = DateTime.UtcNow;

                    _repository.Update(team);
                }


                var tagRepository = _repository.GetRepository<Tag>();

                var tag = tagRepository.Queryable().FirstOrDefault(t => t.Slug == team.Slug);

                if (tag == null)
                {
                    tag = new Tag
                    {
                        Avatar = team.Logo,
                        FullName = team.FullName,
                        ShortName = team.ShortName,
                        NickName = team.NickName,
                        Slug = team.Slug,
                        TagType = TagType.Team
                    };

                    tagRepository.Insert(tag);
                }
                else
                {
                    tag.Avatar = team.Logo;
                    tag.FullName = team.FullName;
                    tag.ShortName = team.ShortName;
                    tag.NickName = team.NickName;
                    tag.Slug = team.Slug;
                    tag.TagType = TagType.Team;

                    tagRepository.Update(tag);
                }

                await _unitOfWork.SaveChangesAsync();

                return Mapper.Map<GetTeamDTO>(team);
            }
            catch (Exception ex)
            {
                throw new Exception(ResponseCodeString.TeamCreate_Error, ex.InnerException);
            }
        }
    }
}
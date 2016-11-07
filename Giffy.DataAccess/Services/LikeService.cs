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
    public class LikeService : Service<Like>, ILikeService
    {
        private readonly IRepository<Like> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly GiffyIdentityContext _ictx;
        private readonly GiffyUserManager _userManager;

        public LikeService(IRepository<Like> repository, IUnitOfWork unitOfWork) : base(repository)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            _ictx = new GiffyIdentityContext();
            _userManager = new GiffyUserManager(new UserStore<User>(_ictx));
        }

        public async Task<IEnumerable<GetLikeDTO>> GetLikes(int skip, int take, ActionFor actionFor, int actionForId)
        {
            var likes = await _repository.Queryable().Include(l => l.CreatedUser).Where(l => l.PostId == actionForId).OrderByDescending(p => p.CreatedDate).Skip(skip).Take(take).ToListAsync();
            return likes.Select(Mapper.Map<GetLikeDTO>);
        }

        public async Task<GetLikeDTO> ToggleLike(NewLikeDTO newLike, string userName)
        {
            try
            {
                User user = await _userManager.FindByNameAsync(userName);
                IEnumerable<Like> likes = _repository.Queryable().Where(c => c.PostId == newLike.ActionForId && c.CreatedUserId == user.Id);

                Like like = null;
                if(likes.Count() > 0)
                {
                    like = likes.FirstOrDefault();
                    _repository.Delete(like);
                }
                else
                {
                    like = Mapper.Map<Like>(newLike);
                    
                    like.CreatedUserId = user.Id;
                    like.CreatedDate = DateTime.UtcNow;
                    like.UpdatedUserId = user.Id;
                    like.UpdatedDate = DateTime.UtcNow;
                    if (newLike.ActionFor == ActionFor.Post)
                    {
                        like.PostId = newLike.ActionForId;
                    }

                    _repository.Insert(like);
                }

                await _unitOfWork.SaveChangesAsync();
                return Mapper.Map<GetLikeDTO>(like);
            }
            catch (Exception ex)
            {
                throw new Exception(ResponseCodeString.LikeCreate_Error, ex.InnerException);
            }
        }
    }
}
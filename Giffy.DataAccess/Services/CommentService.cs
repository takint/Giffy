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
    public class CommentService : Service<Comment>, ICommentService
    {
        private readonly IRepository<Comment> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly GiffyIdentityContext _ictx;
        private readonly GiffyUserManager _userManager;

        public CommentService(IRepository<Comment> repository, IUnitOfWork unitOfWork) : base(repository)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            _ictx = new GiffyIdentityContext();
            _userManager = new GiffyUserManager(new UserStore<User>(_ictx));
        }

        public async Task<IEnumerable<GetCommentDTO>> GetComments(int skip, int take, ActionFor actionFor, int actionForId)
        {
            var comments = await _repository.Queryable().Include(c => c.CreatedUser).Where(c => c.PostId == actionForId).OrderByDescending(p => p.CreatedDate).Skip(skip).Take(take).ToListAsync();
            return comments.Select(Mapper.Map<GetCommentDTO>);
        }

        public async Task<GetCommentDTO> CreateComment(NewCommentDTO newComment, string userName)
        {
            try
            {
                User user = await _userManager.FindByNameAsync(userName);
                Comment comment = Mapper.Map<Comment>(newComment);
                
                comment.CreatedUserId = user.Id;
                comment.CreatedDate = DateTime.UtcNow;
                comment.UpdatedUserId = user.Id;
                comment.UpdatedDate = DateTime.UtcNow;
                if(newComment.ActionFor == ActionFor.Post)
                {
                    comment.PostId = newComment.ActionForId;
                }

                _repository.Insert(comment);
                await _unitOfWork.SaveChangesAsync();

                var resultComment = Mapper.Map<GetCommentDTO>(comment);
                resultComment.CreatedUserAvatar = user.Avatar;
                resultComment.CreatedUserName = user.UserName;

                return resultComment;
            }
            catch (Exception ex)
            {
                throw new Exception(ResponseCodeString.CommentCreate_Error, ex.InnerException);
            }
        }
    }
}
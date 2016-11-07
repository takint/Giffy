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
    public class TagService : Service<Tag>, ITagService
    {
        private readonly IRepository<Tag> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly GiffyIdentityContext _ictx;
        private readonly GiffyUserManager _userManager;

        public TagService(IRepository<Tag> repository, IUnitOfWork unitOfWork) : base(repository)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            _ictx = new GiffyIdentityContext();
            _userManager = new GiffyUserManager(new UserStore<User>(_ictx));
        }

        public async Task<IEnumerable<TagDTO>> SearchTags(string searchTerm, int take, int skip)
        {
            IQueryable<Tag> query = _repository.Queryable()
                .Where(t => t.FullName.Contains(searchTerm) || t.ShortName.Contains(searchTerm) || t.NickName.Contains(searchTerm) || t.Slug.Contains(searchTerm))
                .OrderByDescending(t => t.FullName)
                .Skip(skip)
                .Take(take);

            return await query.Select(t => new TagDTO()
            {
                Id = t.Id,
                FullName = t.FullName,
                Name = t.ShortName,
                NickName = t.NickName,
                Avatar = t.Avatar,
                Slug = t.Slug,
                SearchCount = t.SearchCount,
                Level = t.Level
            }
            ).ToListAsync();

        }

        public async Task<IEnumerable<TagDTO>> GetTop(int top, TagType type)
        {
            IQueryable<Tag> query = _repository.Queryable().Where(t => t.SearchCount > 0 && t.TagType == type)
                .OrderByDescending(t => t.SearchCount)
                .Take(top);

            var result = await query.ToListAsync();
            return result.Select(Mapper.Map<TagDTO>);
        }

        public async Task<TagDTO> GetTag(string slug)
        {
            var tag = await _repository.Queryable().Include(t => t.Posts).FirstOrDefaultAsync(t => t.Slug == slug);
            if(tag == null)
            {
                return null;
            }
            return Mapper.Map<TagDTO>(tag);
        }
    }
}

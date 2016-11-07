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
using System.Diagnostics;
using System.Security.Claims;

namespace Giffy.DataAccess.Services
{
    public class PostService : Service<Post>, IPostService
    {
        private readonly IRepository<Post> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly GiffyIdentityContext _ictx;
        private readonly GiffyUserManager _userManager;

        public PostService(IRepository<Post> repository, IUnitOfWork unitOfWork) : base(repository)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            _ictx = new GiffyIdentityContext();
            _userManager = new GiffyUserManager(new UserStore<User>(_ictx));
        }

        public async Task<IEnumerable<GetGagDTO>> GetGags(int skip, int take, string userFilter, GagFilter filter, string slug)
        {
            bool isAdminUser = ClaimsPrincipal.Current.Claims.Any(c => c.Value == "Admin")
                || ClaimsPrincipal.Current.Claims.Any(c => c.Value == "SuperAdmin");
            IQueryable<Post> gags = _repository.Queryable();

            gags = gags.Include(p => p.Images)
                .Include(p => p.Videos)
                .Include(p => p.Likes.Select(l => l.CreatedUser))
                .Include(p => p.Comments)
                .Include(p => p.Tags)
                .Include(p => p.CreatedUser)
                .Where(p => p.PostType == PostType.GAG && p.IsActived);

            if (!string.IsNullOrEmpty(userFilter))
            {
                User postUser = await _userManager.FindByNameAsync(userFilter);
                gags = gags.Where(p => p.CreatedUserId == postUser.Id);
            }

            if (filter == GagFilter.Tag)
            {
                gags = gags.Where(p => p.Tags.Count(t => t.Slug == slug) > 0 || string.IsNullOrEmpty(slug));
                var tagRepository = _repository.GetRepository<Tag>();
                var tag = await tagRepository.Queryable().FirstOrDefaultAsync(t => t.Slug == slug);
                if(tag != null)
                {
                    tag.SearchCount++;
                }
                tagRepository.Update(tag);
                await _unitOfWork.SaveChangesAsync();
            }
            else if(filter == GagFilter.Search)
            {
                gags = gags.Where(p => p.Title.Contains(slug) || p.Description.Contains(slug) || p.Tags.Count(t => t.Slug.Contains(slug)) > 0 || string.IsNullOrEmpty(slug));
            }
            else
            {
                switch (filter) {
                    case GagFilter.Image:
                        gags = gags.Where(p => p.Images.Count(i => i.ImageType == ImageType.Static) > 0);
                        break;
                    case GagFilter.Gif:
                        gags = gags.Where(p => p.Images.Count(i => i.ImageType == ImageType.Gif) > 0);
                        break;
                    case GagFilter.Video:
                        gags = gags.Where(p => p.Videos.Count() > 0);
                        break;
                    default:
                        break;
                }

                if (!string.IsNullOrEmpty(slug))
                {
                    var indexOfSlug = _repository.Queryable().OrderByDescending(p => p.PublishedDate).Where(p => (p.IsActived == !isAdminUser) || isAdminUser).ToList().FindIndex(g => g.Slug == slug);
                    skip = indexOfSlug == -1 ? skip : indexOfSlug + skip;
                }
            }

            gags = gags.OrderByDescending(p => p.PublishedDate)
                .Skip(skip)
                .Take(take);

            var result = await gags.ToListAsync();

            return result.Select(Mapper.Map<GetGagDTO>);
        }

        public async Task<IEnumerable<GetGagDTO>> GetAllGags(int skip, int take)
        {
            bool isAdminUser = ClaimsPrincipal.Current.Claims.Any(c => c.Value == "Admin")
                || ClaimsPrincipal.Current.Claims.Any(c => c.Value == "SuperAdmin");

            IQueryable<Post> gags = _repository.Queryable();

            gags = gags.Include(p => p.Images)
                .Include(p => p.Videos)
                .Include(p => p.Likes.Select(l => l.CreatedUser))
                .Include(p => p.Comments)
                .Include(p => p.Tags)
                .Include(p => p.CreatedUser)
                .Where(p => p.PostType == PostType.GAG);

            if (isAdminUser)
            {
                gags = gags.Where(p => p.IsActived == !isAdminUser || isAdminUser);
            }
            else
            {
                gags = gags.Where(p => p.IsActived);
            }

            gags = gags.OrderByDescending(p => p.PublishedDate)
                .Skip(skip)
                .Take(take);

            var result = await gags.ToListAsync();

            return result.Select(Mapper.Map<GetGagDTO>);
        }

        public async Task<GetGagDTO> GetGagAsync(string slug)
        {
            try
            {
                Post post = await _repository.Queryable()
                .Include(p => p.Images)
                .Include(p => p.Videos)
                .Include(p => p.Likes.Select(l => l.CreatedUser))
                .Include(p => p.Comments)
                .Include(p => p.Tags)
                .Include(p => p.CreatedUser).FirstOrDefaultAsync(p => p.Slug == slug && p.IsActived);
                GetGagDTO gag = Mapper.Map<GetGagDTO>(post);

                var gagQueryable = _repository.Queryable();

                var backGag = await gagQueryable.OrderByDescending(p => p.PublishedDate).FirstOrDefaultAsync(p => p.IsActived && p.PublishedDate < gag.PublishedDate);
                var forwardGag = await gagQueryable.OrderBy(p => p.PublishedDate).FirstOrDefaultAsync(p => p.IsActived && p.PublishedDate > gag.PublishedDate);
                gag.BackGagSlug = backGag != null ? backGag.Slug : string.Empty;
                gag.ForwardGagSlug = forwardGag != null ? forwardGag.Slug : string.Empty;

                var tags = post.Tags.Select(t => t.Slug);

                var sameTagsGags = await gagQueryable
                    .Include(p => p.Images)
                    .Include(p => p.Videos)
                    .OrderByDescending(p => p.PublishedDate)
                    .Where(p => p.IsActived && p.Tags.Count(t => tags.Contains(t.Slug)) > 0 && p.Id != post.Id)
                    .Take(4).ToListAsync();
                gag.SameTagsGags = Mapper.Map<ICollection<GetGagDTO>>(sameTagsGags);

                var sameUserGags = await gagQueryable
                    .Include(p => p.Images)
                    .Include(p => p.Videos)
                    .OrderByDescending(p => p.PublishedDate)
                    .Where(p => p.IsActived && p.CreatedUserId == post.CreatedUserId && p.Id != post.Id)
                    .Take(6).ToListAsync();
                gag.SameUserGags = Mapper.Map<ICollection<GetGagDTO>>(sameUserGags);

                return gag;
            }
            catch (Exception ex)
            {
                throw new Exception(ResponseCodeString.PostGet_Error, ex.InnerException);
            }
        }

        public GetGagDTO GetGag(string slug)
        {
            try
            {
                Post post = _repository.Queryable()
                .Include(p => p.Images)
                .Include(p => p.Videos)
                .Include(p => p.Likes.Select(l => l.CreatedUser))
                .Include(p => p.Comments)
                .Include(p => p.Tags)
                .Include(p => p.CreatedUser).FirstOrDefault(p => p.Slug == slug && p.IsActived);
                GetGagDTO gag = Mapper.Map<GetGagDTO>(post);

                return gag;
            }
            catch (Exception ex)
            {
                throw new Exception(ResponseCodeString.PostGet_Error, ex.InnerException);
            }
        }

        public async Task<string> CreatePost(NewPostDTO newPost, string userName)
        {
            try
            {
                bool isAutoApproved = ClaimsPrincipal.Current.Claims.Any(c => c.Value == "Admin")
                || ClaimsPrincipal.Current.Claims.Any(c => c.Value == "SuperAdmin")
                || ClaimsPrincipal.Current.Claims.Any(c => c.Value == "Mod");

                User user = await _userManager.FindByNameAsync(userName);
                Post post = Mapper.Map<Post>(newPost);
                
                post.CreatedUserId = user.Id;
                post.CreatedDate = DateTime.UtcNow;
                post.UpdatedUserId = user.Id;
                post.UpdatedDate = DateTime.UtcNow;
                post.PublishedDate = DateTime.UtcNow;
                post.IsActived = isAutoApproved;
                _repository.Insert(post);
                await _unitOfWork.SaveChangesAsync();

                foreach (var image in newPost.Images)
                {
                    image.CreatedUserId = user.Id;
                    image.CreatedDate = DateTime.UtcNow;
                    image.UpdatedUserId = user.Id;
                    image.UpdatedDate = DateTime.UtcNow;
                    var newImage = Mapper.Map<Image>(image);
                    newImage.Post = post;
                    newImage.PostId = post.Id;
                    _repository.GetRepository<Image>().Insert(newImage);
                }

                foreach (var video in newPost.Videos)
                {
                    video.CreatedUserId = user.Id;
                    video.CreatedDate = DateTime.UtcNow;
                    video.UpdatedUserId = user.Id;
                    video.UpdatedDate = DateTime.UtcNow;
                    var newVideo = Mapper.Map<Video>(video);
                    newVideo.Post = post;
                    newVideo.PostId = post.Id;
                    _repository.GetRepository<Video>().Insert(newVideo);
                }

                await _unitOfWork.SaveChangesAsync();

                return ResponseCodeString.GagCreate_Success;
            }
            catch (Exception ex)
            {
                throw new Exception(ResponseCodeString.PostCreate_Error, ex.InnerException);
            }
        }

        public async Task<string> UpdatePost(NewPostDTO updatePost, string userName)
        {
            try
            {
                User user = await _userManager.FindByNameAsync(userName);
                Post post = Mapper.Map<Post>(updatePost);
                Post beUpdatePost = _repository.Find(post.Id);

                beUpdatePost.Title = post.Title;
                beUpdatePost.Description = post.Description;
                beUpdatePost.IsActived = post.IsActived;
                beUpdatePost.UpdatedUserId = post.Title;
                beUpdatePost.UpdatedDate = DateTime.UtcNow;
                beUpdatePost.UpdatedUserId = user.Id;

                // Will be update for update more properties of this Entity

                _repository.Update(beUpdatePost);
                await _unitOfWork.SaveChangesAsync();

                return ResponseCodeString.Action_Success;
            }
            catch (Exception ex)
            {
                throw new Exception(ResponseCodeString.PostUpdate_Error, ex.InnerException);
            }
        }

        public async Task<string> DeletePost(NewPostDTO deletePost)
        {
            try
            {
                var beDeletePost = _repository.Find(deletePost.Id);
                _repository.Delete(beDeletePost);
                await _unitOfWork.SaveChangesAsync();
                return ResponseCodeString.Action_Success;
            }
            catch (Exception ex)
            {
                throw new Exception(ResponseCodeString.PostDelete_Error, ex.InnerException);
            }
        }
    }
}
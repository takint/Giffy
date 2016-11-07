using AutoMapper;
using Giffy.Entities.Models;
using Giffy.DataAccess.Models;
using Giffy.DataAccess.Helpers;
using System.Linq;
using System.Security.Claims;

namespace Giffy.DataAccess.Mappers
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<EFOToDTOMappingProfile>();
                x.AddProfile<DTOToEFOMappingProfile>();
            });
        }

        public class DTOToEFOMappingProfile : Profile
        {
            public override string ProfileName
            {
                get { return "DTOToEFOMappings"; }
            }

            protected override void Configure()
            {
                //Mapper.CreateMap<CreateUserDTO, User>();
                //Mapper.CreateMap<NewPostDTO, Post>()
                //    .ForMember(d => d.Images, o => o.Ignore())
                //    .ForMember(d => d.Videos, o => o.Ignore())
                //    .ForMember(d => d.Slug, o => o.MapFrom(s => s.Title.GenerateSlug()));
                //Mapper.CreateMap<ImageDTO, Image>();
                //Mapper.CreateMap<VideoDTO, Video>();
                //Mapper.CreateMap<NewCommentDTO, Comment>();
                //Mapper.CreateMap<NewLikeDTO, Like>();
                //Mapper.CreateMap<NewLeagueDTO, League>()
                //    .ForMember(d => d.Slug, o => o.MapFrom(s => s.FullName.GenerateSlug()));
                //Mapper.CreateMap<NewTeamDTO, Team>()
                //    .ForMember(d => d.Slug, o => o.MapFrom(s => s.FullName.GenerateSlug()));
                //Mapper.CreateMap<NewPlayerDTO, Player>()
                //    .ForMember(d => d.Slug, o => o.MapFrom(s => s.FullName.GenerateSlug()));
                //Mapper.CreateMap<TagDTO, Tag>()
                //    .ForMember(d => d.Slug, o => o.MapFrom(s => s.FullName.GenerateSlug()));
            }
        }

        public class EFOToDTOMappingProfile : Profile
        {
            public override string ProfileName
            {
                get { return "EFOToDTOMappings"; }
            }

            protected override void Configure()
            {
                //Mapper.CreateMap<User, UserDTO>()
                //    .ForMember(d => d.DisplayName, o => o.MapFrom(s => string.IsNullOrEmpty(s.FirstName) && string.IsNullOrEmpty(s.LastName) ? s.UserName : string.Format("{0} {1}", s.FirstName, s.LastName)));
                //Mapper.CreateMap<User, GetUserDTO>()
                //    .ForMember(d => d.DisplayName, o => o.MapFrom(s => string.IsNullOrEmpty(s.FirstName) && string.IsNullOrEmpty(s.LastName) ? s.UserName : string.Format("{0} {1}", s.FirstName, s.LastName))); ;
                //Mapper.CreateMap<Post, GetGagDTO>()
                //    .ForMember(d => d.CreatedUserDisplayName, o => o.MapFrom(s => string.IsNullOrEmpty(s.CreatedUser.FirstName) && string.IsNullOrEmpty(s.CreatedUser.LastName) ? s.CreatedUser.UserName : string.Format("{0} {1}", s.CreatedUser.FirstName, s.CreatedUser.LastName)))
                //    .ForMember(d => d.CreatedUserName, o => o.MapFrom(s => s.CreatedUser.UserName))
                //    .ForMember(d => d.CommentCount, o => o.MapFrom(s => s.Comments.Count))
                //    .ForMember(d => d.LikeCount, o => o.MapFrom(s => s.Likes.Count))
                //    .ForMember(d => d.Liked, o => o.ResolveUsing(s => CheckLiked(s)));
                //Mapper.CreateMap<Image, ImageDTO>();
                //Mapper.CreateMap<Video, VideoDTO>();
                //Mapper.CreateMap<Comment, GetCommentDTO>()
                //    .ForMember(d => d.CreatedUserDisplayName, o => o.MapFrom(s => string.IsNullOrEmpty(s.CreatedUser.FirstName) && string.IsNullOrEmpty(s.CreatedUser.LastName) ? s.CreatedUser.UserName : string.Format("{0} {1}", s.CreatedUser.FirstName, s.CreatedUser.LastName)))
                //    .ForMember(d => d.CreatedUserName, o => o.MapFrom(s => s.CreatedUser.UserName));
                //Mapper.CreateMap<Like, GetLikeDTO>()
                //    .ForMember(d => d.CreatedUserDisplayName, o => o.MapFrom(s => string.IsNullOrEmpty(s.CreatedUser.FirstName) && string.IsNullOrEmpty(s.CreatedUser.LastName) ? s.CreatedUser.UserName : string.Format("{0} {1}", s.CreatedUser.FirstName, s.CreatedUser.LastName)))
                //    .ForMember(d => d.CreatedUserName, o => o.MapFrom(s => s.CreatedUser.UserName));
                //Mapper.CreateMap<League, GetLeagueDTO>();
                //Mapper.CreateMap<Team, GetTeamDTO>();
                //Mapper.CreateMap<Player, GetPlayerDTO>();
                //Mapper.CreateMap<Tag, TagDTO>()
                //    .ForMember(d => d.Name, o => o.MapFrom(s => s.ShortName))
                //    .ForMember(d => d.PostCount, o => o.MapFrom(s => s.Posts.Count));
            }

            protected bool CheckLiked(Post post)
            {
                if(post.Likes == null || post.Likes.Count == 0)
                {
                    return false;
                }
                string userName = ClaimsPrincipal.Current.Identity.Name;
                return post.Likes.Count(l => l.CreatedUser != null && l.CreatedUser.UserName == userName) > 0;
            }
        }
    }
}
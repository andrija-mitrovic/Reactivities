using Application.Activities;
using Application.Comments;
using AutoMapper;
using Domain;
using System.Linq;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity, Activity>();
            CreateMap<Activity, ActivityDto>()
                .ForMember(x => x.HostUsername, 
                    y => y.MapFrom(z => 
                        z.Attendees.FirstOrDefault(w => w.IsHost)
                        .AppUser
                        .UserName));
            CreateMap<ActivityAttendee, AttendeeDto>()
                .ForMember(x => x.DisplayName, y => y.MapFrom(z => z.AppUser.DisplayName))
                .ForMember(x => x.UserName, y => y.MapFrom(z => z.AppUser.UserName))
                .ForMember(x => x.Bio, y => y.MapFrom(z => z.AppUser.Bio))
                .ForMember(x => x.Image, y => y.MapFrom(z => z.AppUser.Photos.FirstOrDefault(t => t.IsMain).Url));
            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(x => x.Image, y => y.MapFrom(z => z.Photos.FirstOrDefault(t => t.IsMain).Url));
            CreateMap<Comment, CommentDto>()
                .ForMember(x => x.DisplayName, y => y.MapFrom(z => z.Author.DisplayName))
                .ForMember(x => x.Username, y => y.MapFrom(z => z.Author.UserName))
                .ForMember(x => x.Image, y => y.MapFrom(z => z.Author.Photos.FirstOrDefault(t => t.IsMain).Url));
        }
    }
}

using AutoMapper;
using System.Linq;

namespace Domain.AutoMapper
{
    public class AppMappingDtoDalProfile : Profile
    {
        public AppMappingDtoDalProfile()
        {
            CreateMap<DataAccess.DAL.User, DTO.User>()
                .ForMember(dest => dest.PreferencesId, opt => opt.MapFrom(src => src.Preferences.Select(p => p.Id).ToList())).ReverseMap(); ;
            CreateMap<DataAccess.DAL.Meeting, DTO.Meeting>().ReverseMap();
            CreateMap<DataAccess.DAL.MeetingStatus, DTO.MeetingStatus>().ReverseMap();
            CreateMap<DataAccess.DAL.MeetingType, DTO.MeetingType>().ReverseMap();
            CreateMap<DataAccess.DAL.Place, DTO.Place>()
                .ForMember(dest => dest.PreferencesId, opt => opt.MapFrom(src => src.Preferences.Select(p => p.Id).ToList())).ReverseMap();
            CreateMap<DataAccess.DAL.Preference, DTO.Preference>().ReverseMap();
        }
    }
}

using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Domain.AutoMapper
{
    public class AppMappingDtoDalProfile : Profile
    {
        public AppMappingDtoDalProfile()
        {
            CreateMap<DataAccess.DAL.User, Domain.DTO.User>().ReverseMap();
            CreateMap<DataAccess.DAL.Meeting, Domain.DTO.Meeting>().ReverseMap();
            CreateMap<DataAccess.DAL.MeetingStatus, Domain.DTO.MeetingStatus>().ReverseMap();
            CreateMap<DataAccess.DAL.MeetingType, Domain.DTO.MeetingType>().ReverseMap();
            CreateMap<DataAccess.DAL.Place, Domain.DTO.Place>()
                .ForMember(dest => dest.PreferencesId, opt => opt.MapFrom(src => src.Preferences.Select(p => p.Id).ToList()));
            CreateMap<DataAccess.DAL.Preference, Domain.DTO.Preference>().ReverseMap();
        }
    }
}

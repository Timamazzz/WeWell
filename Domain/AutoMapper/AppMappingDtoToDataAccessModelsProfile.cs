using AutoMapper;
using System.Linq;

namespace Domain.AutoMapper
{
    public class AppMappingDtoToDataAccessModelsProfile : Profile
    {
        public AppMappingDtoToDataAccessModelsProfile()
        {
            /*CreateMap<DataAccess.DAL.User, DTO.User>()
                .ForMember(dest => dest.PreferencesId, opt => opt.MapFrom(src => src.Preferences.Select(p => p.Id).ToList()))
                .ReverseMap(); ;
            CreateMap<DataAccess.DAL.Meeting, DTO.Meeting>().ReverseMap();
            CreateMap<DataAccess.DAL.MeetingType, DTO.MeetingType>().ReverseMap();
            CreateMap<DataAccess.DAL.Place, DTO.Place>()
                .ForMember(dest => dest.PreferencesId, opt => opt.MapFrom(src => src.Preferences.Select(p => p.Id).ToList()))
                .ForMember(dest => dest.MeetingTypesId, opt => opt.MapFrom(src => src.MeetingTypes.Select(p => p.Id).ToList()))
                .ReverseMap();
            CreateMap<DataAccess.DAL.Preference, DTO.Preference>().ReverseMap();*/
            CreateMap<DataAccess.Models.MeetingStatus, DataTransferObjects.MeetingStatus>().ReverseMap();
            CreateMap<DataAccess.Models.MeetingType, DataTransferObjects.MeetingType>().ReverseMap();
            CreateMap<DataAccess.Models.Preference, DataTransferObjects.Preference>().ReverseMap();
        }
    }
}

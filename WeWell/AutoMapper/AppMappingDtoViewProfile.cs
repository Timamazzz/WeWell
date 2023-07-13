using AutoMapper;

namespace WeWell.AutoMapper;

public class AppMappingDtoViewProfile : Profile
{
    public AppMappingDtoViewProfile()
    {
        CreateMap<DataAccess.DTO.User, ViewModels.User>().ReverseMap();
        CreateMap<DataAccess.DTO.Preference, ViewModels.Preference>().ReverseMap();
        CreateMap<DataAccess.DTO.Meeting, ViewModels.Meeting>().ReverseMap();
        CreateMap<DataAccess.DTO.MeetingStatus, ViewModels.MeetingStatus>().ReverseMap();
        CreateMap<DataAccess.DTO.MeetingType, ViewModels.MeetingType>().ReverseMap();
        CreateMap<DataAccess.DTO.Place, ViewModels.Place>().ReverseMap();
    }
}

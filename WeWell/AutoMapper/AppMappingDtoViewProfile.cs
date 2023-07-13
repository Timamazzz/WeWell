using AutoMapper;

namespace WeWell.AutoMapper;

public class AppMappingDtoViewProfile : Profile
{
    public AppMappingDtoViewProfile()
    {
        CreateMap<Domain.DTO.User, ViewModels.User>().ReverseMap();
        CreateMap<Domain.DTO.Preference, ViewModels.Preference>().ReverseMap();
        CreateMap<Domain.DTO.Meeting, ViewModels.Meeting>().ReverseMap();
        CreateMap<Domain.DTO.MeetingStatus, ViewModels.MeetingStatus>().ReverseMap();
        CreateMap<Domain.DTO.MeetingType, ViewModels.MeetingType>().ReverseMap();
        CreateMap<Domain.DTO.Place, ViewModels.Place>().ReverseMap();
    }
}

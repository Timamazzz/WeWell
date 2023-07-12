using AutoMapper;

namespace DataAccess.AutoMapper;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<DAL.User, Domain.DTO.User>().ReverseMap();
        CreateMap<DAL.Meeting, Domain.DTO.Meeting>().ReverseMap();
        CreateMap<DAL.MeetingStatus, Domain.DTO.MeetingStatus>().ReverseMap();
        CreateMap<DAL.MeetingType, Domain.DTO.MeetingType>().ReverseMap();
        CreateMap<DAL.Place, Domain.DTO.Place>().ReverseMap();
        CreateMap<DAL.Preference, Domain.DTO.Preference>().ReverseMap();
    }
}

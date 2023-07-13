using AutoMapper;

namespace DataAccess.AutoMapper;

public class AppMappingDtoDalProfile : Profile
{
    public AppMappingDtoDalProfile()
    {
        CreateMap<DAL.User, DTO.User>().ReverseMap();
        CreateMap<DAL.Meeting, DTO.Meeting>().ReverseMap();
        CreateMap<DAL.MeetingStatus, DTO.MeetingStatus>().ReverseMap();
        CreateMap<DAL.MeetingType, DTO.MeetingType>().ReverseMap();
        CreateMap<DAL.Place, DTO.Place>().ReverseMap();
        CreateMap<DAL.Preference, DTO.Preference>().ReverseMap();
    }
}

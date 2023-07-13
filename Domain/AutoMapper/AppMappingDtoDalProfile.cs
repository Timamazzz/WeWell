using AutoMapper;

namespace Domain.AutoMapper;

public class AppMappingDtoDalProfile : Profile
{
    public AppMappingDtoDalProfile()
    {
        CreateMap<DataAccess.DAL.User, DTO.User>().ReverseMap();
        CreateMap<DataAccess.DAL.Meeting, DTO.Meeting>().ReverseMap();
        CreateMap<DataAccess.DAL.MeetingStatus, DTO.MeetingStatus>().ReverseMap();
        CreateMap<DataAccess.DAL.MeetingType, DTO.MeetingType>().ReverseMap();
        CreateMap<DataAccess.DAL.Place, DTO.Place>().ReverseMap();
        CreateMap<DataAccess.DAL.Preference, DTO.Preference>().ReverseMap();
    }
}

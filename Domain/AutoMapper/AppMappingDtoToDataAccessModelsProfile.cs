using AutoMapper;
using DataAccess.Models;

namespace Domain.AutoMapper;
public class AppMappingDtoToDataAccessModelsProfile : Profile
{
    public AppMappingDtoToDataAccessModelsProfile()
    {
        CreateMap<MeetingStatus, DataTransferObjects.MeetingStatus>().ReverseMap();
        CreateMap<MeetingType, DataTransferObjects.MeetingType>().ReverseMap();
        CreateMap<Preference, DataTransferObjects.Preference>().ReverseMap();
        CreateMap<User, DataTransferObjects.User>().ReverseMap();
        CreateMap<Place, DataTransferObjects.Place>().ReverseMap();
    }
}
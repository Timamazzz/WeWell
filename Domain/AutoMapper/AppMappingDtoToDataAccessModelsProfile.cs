using AutoMapper;
using DataAccess.Models;

namespace Domain.AutoMapper;
public class AppMappingDtoToDataAccessModelsProfile : Profile
{
    public AppMappingDtoToDataAccessModelsProfile()
    {
        CreateMap<MeetingType, DataTransferObjects.MeetingType>().ReverseMap();
        CreateMap<Preference, DataTransferObjects.Preference>().ReverseMap();
        CreateMap<User, DataTransferObjects.User>().ReverseMap();
        CreateMap<Place, DataTransferObjects.Place>().ReverseMap();
        CreateMap<Meeting, DataTransferObjects.Meeting>().ReverseMap();
        CreateMap<Player, DataTransferObjects.Player>().ReverseMap();
    }
}
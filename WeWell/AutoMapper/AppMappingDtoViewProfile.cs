using AutoMapper;

namespace WeWell.AutoMapper;

public class AppMappingDtoViewProfile : Profile
{
    public AppMappingDtoViewProfile()
    {
        CreateMap<DataAccess.DTO.User, WeWell.ViewModels.User>().ReverseMap();
        CreateMap<DataAccess.DTO.Preference, WeWell.ViewModels.Preference>().ReverseMap();
    }
}

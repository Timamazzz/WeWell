using AutoMapper;
using System.Globalization;

namespace WeWell.AutoMapper;

public class TimeSpanStringConverter : IValueConverter<string, TimeSpan?>
{
    public TimeSpan? Convert(string sourceMember, ResolutionContext context)
    {
        if (TimeSpan.TryParseExact(sourceMember, @"hh\:mm", CultureInfo.InvariantCulture, out TimeSpan result))
        {
            return result;
        }
        return null;
    }
}


public class AppMappingDtoToPresentationModelsProfile : Profile
{
    public AppMappingDtoToPresentationModelsProfile()
    {
        
        var imageResolver = new ImageResolver(new HttpContextAccessor());
        CreateMap<Domain.DataTransferObjects.MeetingType, Models.MeetingType>().ReverseMap();
        CreateMap<Domain.DataTransferObjects.Preference, Models.Preference>().ReverseMap();
        //Users
        CreateMap<Domain.DataTransferObjects.User, Models.Users.UserCreate>().ReverseMap();
        CreateMap<Domain.DataTransferObjects.User, Models.Users.UserUpdate>().ReverseMap();
        CreateMap<Domain.DataTransferObjects.User, Models.Users.UserGet>()
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => imageResolver.Resolve(src, null, src.AvatarPath, null)))
            .ForMember(dest => dest.AvatarPath, opt => opt.MapFrom(src => src.AvatarPath))
            .ReverseMap();
        CreateMap<Domain.DataTransferObjects.Place, Models.Places.PlaceCreate>().ReverseMap();
        CreateMap<Domain.DataTransferObjects.Place, Models.Places.PlaceUpdate>().ReverseMap();
        CreateMap<Domain.DataTransferObjects.Place, Models.Places.PlaceGet>()
            .ForMember(dest => dest.Url,
                opt => opt.MapFrom(src => imageResolver.Resolve(src, null, src.ImagePath, null)))
            .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath))
            .ReverseMap();
        CreateMap<Domain.DataTransferObjects.Meeting, Models.Meetings.MeetingCreate>().ReverseMap();
    }
}

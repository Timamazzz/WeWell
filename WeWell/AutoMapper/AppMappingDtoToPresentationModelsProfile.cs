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
        /*//Users
        CreateMap<Domain.DTO.User, ViewModels.Users.UserCreate>()
            .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => ConvertImageToByteArray(src.Avatar)))
            .ForMember(dest => dest.AvatarExtensions, opt => opt.MapFrom(src => src.Avatar != null ? Path.GetExtension(src.Avatar.FileName) : ""))
            .ForMember(dest => dest.PreferencesId, opt => opt.MapFrom(src => src.Preferences.Select(p => p.Id).ToList())); // Map Preferences to PreferencesId
        CreateMap<Domain.DTO.User, ViewModels.Users.UserUpdate>()
            .ReverseMap()
            .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => ConvertImageToByteArray(src.Avatar)))
            .ForMember(dest => dest.AvatarExtensions, opt => opt.MapFrom(src => src.Avatar != null ? Path.GetExtension(src.Avatar.FileName) : ""));
        CreateMap<Domain.DTO.User, ViewModels.Users.UserGet>().ReverseMap();
        CreateMap<Domain.DTO.Preference, ViewModels.Preference>().ReverseMap();
        CreateMap<Domain.DTO.Meeting, ViewModels.Meeting>().ReverseMap();
        CreateMap<Domain.DTO.MeetingType, ViewModels.MeetingType>().ReverseMap();
        CreateMap<Domain.DTO.Place, ViewModels.Place>()
            .ForMember(dest => dest.StartWork, opt => opt.MapFrom(src => src.StartWork != null ? src.StartWork.Value.ToString(@"hh\:mm") : null))
            .ForMember(dest => dest.EndWork, opt => opt.MapFrom(src => src.EndWork != null ? src.EndWork.Value.ToString(@"hh\:mm") : null))
            .ForMember(dest => dest.Image, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.StartWork, opt => opt.ConvertUsing<TimeSpanStringConverter, string>())
            .ForMember(dest => dest.EndWork, opt => opt.ConvertUsing<TimeSpanStringConverter, string>())
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => ConvertImageToByteArray(src.Image)))
            .ForMember(dest => dest.ImageExtensions, opt => opt.MapFrom(src => src.Image != null ? Path.GetExtension(src.Image.FileName) : ""));
        //Meetings
        CreateMap<Domain.DTO.Meeting, ViewModels.Meetings.MeetingCreate>().ReverseMap();*/
        CreateMap<Domain.DataTransferObjects.MeetingStatus, Models.MeetingStatus>().ReverseMap();
    }

    private static byte[]? ConvertImageToByteArray(IFormFile? image)
    {
        if (image != null)
        {
            using var memoryStream = new MemoryStream();
            image.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
        return null;
    }
}

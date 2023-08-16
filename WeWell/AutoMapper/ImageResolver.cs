using AutoMapper;

namespace WeWell.AutoMapper;

public class ImageResolver : IValueResolver<object, object, string>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ImageResolver(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;

    }

    public string Resolve(object source, object destination, string destMember, ResolutionContext context)
    {
        
        var request = _httpContextAccessor.HttpContext.Request;
        var baseUrl = $"{request.Scheme}://{request.Host}";
        
        var url = $"{baseUrl}/{destMember.Replace('\\', '/')}";

        return url;
    }
}
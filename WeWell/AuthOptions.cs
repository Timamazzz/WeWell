using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WeWell;

public class AuthOptions
{
    public const string ISSUER = "WeWellServer";
    public const string AUDIENCE = "WeWellApp";
    const string KEY = "WeWellMyStrongSecretKey!";
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WeWell.Models;
using WeWell.Services;

namespace WeWell.Controllers;

[ApiController]
[Route("tokens")]
public class TokenController : ControllerBase
{
    private readonly UserService _userService;
    private readonly TokenService _tokenService;

    public TokenController(UserService userService, TokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> Refresh(TokenApiModel tokenApiModel)
    {
        if (tokenApiModel is null)
            return BadRequest("Invalid client request");

        string accessToken = tokenApiModel.AccessToken;
        string refreshToken = tokenApiModel.RefreshToken;

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        var phoneClaim = principal.Claims.FirstOrDefault(c => c.Type == "Phone");

        if (phoneClaim == null)
            return BadRequest("Invalid client request");

        string phone = phoneClaim.Value;

        var user = await _userService.GetByPhoneNumberAsync(phone);

        if (user == null || user.RefreshToken != refreshToken)
            return BadRequest("Invalid client request");
        
        if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return BadRequest("Token expired");

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userService.UpdateAsync(user);

        return Ok(new
        {
            Token = newAccessToken,
            RefreshToken = newRefreshToken
        });
    }

    [HttpPost, Authorize]
    [Route("revoke")]
    public async Task<IActionResult> Revoke(TokenApiModel tokenApiModel)
    {
        if (tokenApiModel is null)
            return BadRequest("Invalid client request");

        string accessToken = tokenApiModel.AccessToken;
        string refreshToken = tokenApiModel.RefreshToken;

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        var phoneClaim = principal.Claims.FirstOrDefault(c => c.Type == "Phone");

        if (phoneClaim == null)
            return BadRequest("Invalid client request");

        string phone = phoneClaim.Value;

        var user = await _userService.GetByPhoneNumberAsync(phone);
        if (user == null) return BadRequest();

        user.RefreshToken = null;
        await _userService.UpdateAsync(user);

        return NoContent();
    }
}
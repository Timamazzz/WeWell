﻿using AutoMapper;
using Domain.DataTransferObjects;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WeWell.Models;
using WeWell.Models.Users;
using WeWell.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WeWell.Interfaces;

namespace WeWell.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;
        private readonly ImageService _imageService;
        private readonly ITokenService _tokenService;

        public UserController(UserService userService, IMapper mapper, ImageService imageService, TokenService tokenService)
        {
            _userService = userService;
            _mapper = mapper;
            _imageService = imageService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(int?), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Create a new user")]
        public async Task<ActionResult<int?>> CreateUser(UserCreate user)
        {
            try
            {
                var userDto = _mapper.Map<Domain.DataTransferObjects.User>(user);
                var userId = await _userService.CreateAsync(userDto);
                return Ok(userId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(int?), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Register a new user")]
        public async Task<ActionResult<int?>> RegisterUser(UserCreate user)
        {
            try
            {
                var existingUser = await _userService.GetByPhoneNumberAsync(user.PhoneNumber);
                if (existingUser != null)
                {
                    return BadRequest("User with the same phone number already exists.");
                }
                
                var userDto = _mapper.Map<Domain.DataTransferObjects.User>(user);
                
                var claims = new List<Claim>
                {
                    new Claim("Name", userDto.Name ?? ""),
                    new Claim("Phone", user.PhoneNumber),
                };
                
                var accessToken = _tokenService.GenerateAccessToken(claims);
                var refreshToken = _tokenService.GenerateRefreshToken();
                    
                userDto.RefreshToken = refreshToken;
                userDto.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(31);

                int? userId = await _userService.RegisterAsync(userDto);
                    
                userDto = await _userService.GetByPhoneNumberAsync(user.PhoneNumber);
                    
                var authenticatedUser = new
                {
                    Token = accessToken,
                    RefreshToken = refreshToken,
                    User = _mapper.Map<UserGet>(userDto)
                };
                
                return Ok(authenticatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpPost("login")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 401)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Authenticate a user")]
        public async Task<ActionResult<UserGet>> AuthenticateUser(UserAuth user)
        {
            try
            {
                var userDto = await _userService.GetByPhoneNumberAsync(user.PhoneNumber);

                if (userDto == null)
                {
                    return NotFound("User not found");
                }

                var isAuthenticated = await _userService.AuthenticateUserAsync(userDto, user.Password) != null;

                if (isAuthenticated)
                {
                    var claims = new List<Claim>
                    {
                        new Claim("Name", userDto.Name ?? ""),
                        new Claim("Phone", user.PhoneNumber),
                    };
                
                    var accessToken = _tokenService.GenerateAccessToken(claims);
                    var refreshToken = _tokenService.GenerateRefreshToken();
                    
                    userDto.RefreshToken = refreshToken;
                    userDto.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(31);

                    await _userService.UpdateAsync(userDto);
                    
                    userDto = await _userService.GetByPhoneNumberAsync(user.PhoneNumber);
                    
                    var authenticatedUser = new
                    {
                        Token = accessToken,
                        RefreshToken = refreshToken,
                        User = _mapper.Map<UserGet>(userDto)
                    };

                    return Ok(authenticatedUser);
                }
                else
                {
                    return Unauthorized("Invalid password");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<UserGet>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Get all users", OperationId = "GetAllUsers")]
        public async Task<ActionResult<List<UserGet>>> GetAllUsers()
        {
            try
            {
                var userDto = await _userService.GetAllAsync();
                var users = _mapper.Map<List<UserGet>>(userDto);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(UserGet), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Get a user by ID")]
        public async Task<ActionResult<UserGet>> GetUser(int id)
        {
            try
            {
                var userDto = await _userService.GetByIdAsync(id);

                if (userDto == null)
                {
                    return NotFound();
                }

                var user = _mapper.Map<UserGet>(userDto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Update a user")]
        public async Task<ActionResult> UpdateUser(UserUpdate user)
        {
            try
            {
                User userDto = _mapper.Map<User>(user);
                await _userService.UpdateAsync(userDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Delete a user by ID")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var userDto = await _userService.GetByIdAsync(id);

                if (userDto == null)
                {
                    return NotFound();
                }

                await _userService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("avatars")]
        [Authorize]
        [DisableRequestSizeLimit]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Update Avatar User")]
        public async Task<ActionResult<UserGet>> UpdateAvatar([FromForm] Image image)
        {
            try
            {
                if (image == null || image.ImageFile == null || image.ImageFile.Length == 0)
                {
                    return BadRequest("Invalid image file");
                }

                var userDto = await _userService.GetByIdAsync(image.ParentModelId);
                if (userDto == null)
                {
                    return NotFound("User not found");
                }
                
                string pathToUpload = Path.Combine("uploads", "images", "users", image.ParentModelId.ToString());
                string newAvatarPath = await _imageService.ReplaceImage(userDto.AvatarPath, image.ImageFile, pathToUpload);

                userDto.AvatarPath = newAvatarPath;
                await _userService.UpdateAsync(userDto);

                var userGet = _mapper.Map<UserGet>(userDto);
                return Ok(userGet.Url);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpGet("phones/{phoneNumber}")]
        [Authorize]
        [ProducesResponseType(typeof(UserGet), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Get a user by phoneNumber")]
        public async Task<ActionResult<UserGet>> GetUserByPhoneNumber(string phoneNumber)
        {
            try
            {
                var userDto = await _userService.GetByPhoneNumberAsync(phoneNumber);

                if (userDto == null)
                {
                    return NotFound();
                }

                var user = _mapper.Map<UserGet>(userDto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpPost("phones")]
        [Authorize]
        [ProducesResponseType(typeof(List<string>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Get existing users by phone numbers")]
        public async Task<ActionResult<List<string>>> GetExistingUsersByPhoneNumbersBatch(List<string> phoneNumbers)
        {
            try
            {
                var existingUsers = await _userService.GetExistingUsersByPhoneNumbersBatch(phoneNumbers);
                return Ok(existingUsers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("check/{phoneNumber}")]
        [Authorize]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Check a user by phoneNumber")]
        public async Task<ActionResult<bool>> CheckUserByPhoneNumber(string phoneNumber)
        {
            try
            {
                User? userDto = await _userService.GetByPhoneNumberAsync(phoneNumber);
                return userDto != null ?  Ok(true) :  Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

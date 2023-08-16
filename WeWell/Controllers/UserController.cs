using AutoMapper;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WeWell.Models;
using WeWell.Models.Users;
using WeWell.Services;

namespace WeWell.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;
        private readonly ImageService _imageService;

        public UserController(UserService userService, IMapper mapper, ImageService imageService)
        {
            _userService = userService;
            _mapper = mapper;
            _imageService = imageService;
        }

        [HttpPost]
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

        [HttpGet]
        [ProducesResponseType(typeof(List<UserGet>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Get all users")]
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
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Update a user")]
        public async Task<ActionResult> UpdateUser(UserUpdate user)
        {
            try
            {
                Domain.DataTransferObjects.User userDto = _mapper.Map<Domain.DataTransferObjects.User>(user);
                await _userService.UpdateAsync(userDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
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

        [HttpGet("phone/{phoneNumber}")]
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

        [HttpPost("phone")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Send SMS to phone number")]
        public ActionResult<string> SendSms([FromBody] Phone phone)
        {
            try
            {
                string code = _userService.SendSms(phone.PhoneNumber);

                return Ok(code);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        
        [HttpPut("avatars")]
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
                return Ok(userGet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

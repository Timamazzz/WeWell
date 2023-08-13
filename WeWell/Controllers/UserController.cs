using AutoMapper;
using Domain.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WeWell.ViewModels;
using WeWell.ViewModels.Users;

namespace WeWell.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(UserService userService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int?), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Create a new user")]
        public async Task<ActionResult<int?>> CreateUser([FromForm] UserCreate user)
        {
            try
            {
                _userService._webRootPath = _webHostEnvironment.WebRootPath;
                var userDTO = _mapper.Map<Domain.DTO.User>(user);
                var userId = await _userService.CreateAsync(userDTO);
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
                var usersDTO = await _userService.GetAllAsync();
                var users = _mapper.Map<List<UserGet>>(usersDTO);
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
                var userDTO = await _userService.GetByIdAsync(id);

                if (userDTO == null)
                {
                    return NotFound();
                }

                var user = _mapper.Map<UserGet>(userDTO);
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
        public async Task<ActionResult> UpdateUser([FromForm] UserUpdate user)
        {
            try
            {
                _userService._webRootPath = _webHostEnvironment.WebRootPath;
                Domain.DTO.User userDTO = _mapper.Map<Domain.DTO.User>(user);
                await _userService.UpdateAsync(userDTO);

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
                _userService._webRootPath = _webHostEnvironment.WebRootPath;
                var userDTO = await _userService.GetByIdAsync(id);

                if (userDTO == null)
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
                var userDTO = await _userService.GetByPhoneNumberAsync(phoneNumber);

                if (userDTO == null)
                {
                    return NotFound();
                }

                var user = _mapper.Map<UserGet>(userDTO);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("phone/{phoneNumber}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Send SMS to phone number")]
        public ActionResult<string> SendSms(string phoneNumber)
        {
            try
            {
                string code = _userService.SendSms(phoneNumber);

                return Ok(code);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

﻿using AutoMapper;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WeWell.ViewModels;

namespace WeWell.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public UserController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int?), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Create a new user")]
        public async Task<ActionResult<int?>> CreateUser([FromForm] User user)
        {
            try
            {
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
        [ProducesResponseType(typeof(List<User>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Get all users")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            try
            {
                var usersDTO = await _userService.GetAllAsync();
                var users = _mapper.Map<List<User>>(usersDTO);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Get a user by ID")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var userDTO = await _userService.GetByIdAsync(id);

                if (userDTO == null)
                {
                    return NotFound();
                }

                var user = _mapper.Map<User>(userDTO);
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
        public async Task<ActionResult> UpdateUser([FromForm] User user)
        {
            try
            {
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
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Get a user by phoneNumber")]
        public async Task<ActionResult<User>> GetUserByPhoneNumber(string phoneNumber)
        {
            try
            {
                var userDTO = await _userService.GetByPhoneNumberAsync(phoneNumber);

                if (userDTO == null)
                {
                    return NotFound();
                }

                var user = _mapper.Map<User>(userDTO);
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
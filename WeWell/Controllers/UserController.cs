using AutoMapper;
using DataAccess;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeWell.ViewModels;

namespace WeWell.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _service;
    private readonly IMapper _mapper;

    public UserController(UserService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Добавить нового пользователя
    /// </summary>
    /// <param name="user"></param>
    [HttpPost("create")]
    public async Task<ActionResult<int>> Create([FromForm] User userData)
    {

        DataAccess.DTO.User user = _mapper.Map<DataAccess.DTO.User>(userData);

        int? id = await _service.CreateAsync(user);

        if (id >= 0)
            return id.Value;
        else
            return BadRequest();
    }
}

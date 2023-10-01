using AutoMapper;
using Domain.Services;
using WeWell.Models;

namespace WeWell.Controllers;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly PlayerService _playerService;
    private readonly IMapper _mapper;


    public PlayerController(PlayerService playerService,IMapper mapper)
    {
        _playerService = playerService;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AddPlayer(Player player)
    {
        try
        {
            var dto = _mapper.Map<Domain.DataTransferObjects.Player>(player);
            int? playerId = _playerService.CreateAsync(dto).Result;
            if (playerId != null)
            {
                return Ok("Игрок успешно добавлен.");
            }
            else
            {
                return BadRequest("Ошибка при добавлении игрока.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Ошибка при добавлении игрока: {ex.Message}");
        }
    }

    [HttpGet]
    public IActionResult GetPlayers()
    {
        try
        {
            var dtoList = _playerService.GetAllAsync().Result;
            var players = _mapper.Map<List<Player>>(dtoList)
                .OrderBy(p => p.Time)
                .ToList();
            
            if (players != null)
            {
                return Ok(players);
            }
            else
            {
                return BadRequest("Ошибка при получении игроков.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Ошибка при получении игроков: {ex.Message}");
        }
    }
}
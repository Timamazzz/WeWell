using AutoMapper;
using DataAccess.Repositories;
using Domain.DataTransferObjects;
using Domain.Interfaces;

namespace Domain.Services;

public class PlayerService
{
    private readonly PlayerRepository _repository;
    private readonly IMapper _mapper;

    public PlayerService(PlayerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int?> CreateAsync(Player player)
    {
        DataAccess.Models.Player entity = _mapper.Map<DataAccess.Models.Player>(player);
        int? id = await _repository.CreateAsync(entity);
        return id;
    }

    public async Task<List<Player>?> GetAllAsync()
    {
        List<DataAccess.Models.Player> entities = await _repository.GetAllAsync();
        List<Player> players = _mapper.Map<List<Player>>(entities);
        return players;
    }
}
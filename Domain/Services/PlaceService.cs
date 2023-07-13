using AutoMapper;
using DataAccess.Repositories;
using Domain.DTO;
using Domain.Interfaces;

namespace Domain.Services;

public class PlaceService : IService<Place>
{
    private readonly PlaceRepository _repository;
    private readonly IMapper _mapper;

    public PlaceService(PlaceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int?> CreateAsync(Place place)
    {
        DataAccess.DAL.Place entity = _mapper.Map<DataAccess.DAL.Place>(place);
        int? id = await _repository.CreateAsync(entity);
        return id;
    }

    public async Task<List<Place>?> GetAllAsync()
    {
        List<DataAccess.DAL.Place>? entities = await _repository.GetAllAsync();
        List<Place>? places = _mapper.Map<List<Place>>(entities);
        return places;
    }

    public async Task<Place?> GetByIdAsync(int id)
    {
        DataAccess.DAL.Place? entity = await _repository.GetByIdAsync(id);
        Place? place = _mapper.Map<Place>(entity);
        return place;
    }

    public async Task UpdateAsync(Place place)
    {
        DataAccess.DAL.Place entity = _mapper.Map<DataAccess.DAL.Place>(place);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}

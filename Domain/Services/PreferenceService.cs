using AutoMapper;
using DataAccess.Repositories;
using Domain.Interfaces;

namespace Domain.Services;

public class PreferenceService /*: IService<Preference>*/
{
    private readonly PreferenceRepository _repository;
    private readonly IMapper _mapper;

    public PreferenceService(PreferenceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /*public async Task<int?> CreateAsync(Preference preference)
    {
        DataAccess.DAL.Preference entity = _mapper.Map<DataAccess.DAL.Preference>(preference);
        int? id = await _repository.CreateAsync(entity);
        return id;
    }

    public async Task<List<Preference>?> GetAllAsync()
    {
        List<DataAccess.DAL.Preference>? entities = await _repository.GetAllAsync();
        List<Preference>? preferences = _mapper.Map<List<Preference>>(entities);
        return preferences;
    }

    public async Task<Preference?> GetByIdAsync(int id)
    {
        DataAccess.DAL.Preference? entity = await _repository.GetByIdAsync(id);
        Preference? preference = _mapper.Map<Preference>(entity);
        return preference;
    }

    public async Task UpdateAsync(Preference preference)
    {
        DataAccess.DAL.Preference entity = _mapper.Map<DataAccess.DAL.Preference>(preference);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }*/
}

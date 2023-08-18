using AutoMapper;
using DataAccess.Repositories;
using Domain.DataTransferObjects;
using Domain.Interfaces;

namespace Domain.Services;

public class PlaceService : IService<Place>
{
    private readonly PlaceRepository _repository;
    private readonly PreferenceRepository _repositoryPreference;
    private readonly MeetingTypeRepository _repositoryMeetingType;
    private readonly IMapper _mapper;

    public PlaceService(PlaceRepository repository, PreferenceRepository repositoryPreference, MeetingTypeRepository repositoryMeetingType, IMapper mapper)
    {
        _repository = repository;
        _repositoryPreference = repositoryPreference;
        _repositoryMeetingType = repositoryMeetingType;
        _mapper = mapper;
    }

    public async Task<int?> CreateAsync(Place place)
    {
        DataAccess.Models.Place entity = _mapper.Map<DataAccess.Models.Place>(place);

        var preferencesIdRange = place.Preferences.Select(p => p.Id).ToList();
        var newPreferences = _repositoryPreference.GetPreferencesByIdRange(preferencesIdRange);
        entity.Preferences = newPreferences;

        var typesIdRange = place.MeetingTypes.Select(m => m.Id).ToList();
        var newTypes = _repositoryMeetingType.GetMeetingTypesByIdRange(preferencesIdRange);
        entity.MeetingTypes = newTypes;
        
        int? id = await _repository.CreateAsync(entity);

        return id;
    }

    public async Task<List<Place>?> GetAllAsync()
    {
        List<DataAccess.Models.Place>? entities = await _repository.GetAllAsync();
        List<Place>? places = _mapper.Map<List<Place>>(entities);
        return places;
    }

    public async Task<Place?> GetByIdAsync(int id)
    {
        DataAccess.Models.Place? entity = await _repository.GetByIdAsync(id);
        Place? place = _mapper.Map<Place>(entity);
        return place;
    }

    public async Task UpdateAsync(Place place)
    {

        var entity = await _repository.GetByIdAsync(place.Id);
        _mapper.Map(place, entity);
        
        var preferencesIdRange = place.Preferences.Select(p => p.Id).ToList();
        var newPreferences = _repositoryPreference.GetPreferencesByIdRange(preferencesIdRange);
        entity.Preferences = newPreferences;
        
        var typesIdRange = place.MeetingTypes.Select(p => p.Id).ToList();
        var newTypes = _repositoryMeetingType.GetMeetingTypesByIdRange(typesIdRange);
        entity.MeetingTypes = newTypes;
        
        await _repository.UpdateAsync(entity);
    }


    public async Task DeleteAsync(int id)
    {
        DataAccess.Models.Place? entity = await _repository.GetByIdAsync(id);
        Place? place = _mapper.Map<Place>(entity);
        await _repository.DeleteAsync(id);
    }
}

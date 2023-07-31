using AutoMapper;
using DataAccess.Repositories;
using Domain.DTO;
using Domain.Interfaces;

namespace Domain.Services;

public class PlaceService : IService<Place>
{
    private readonly PlaceRepository _repository;
    private readonly PreferenceRepository _repositoryPreference;
    private readonly ImageService _imageService;
    private readonly IMapper _mapper;

    public PlaceService(PlaceRepository repository, PreferenceRepository repositoryPreference, IMapper mapper, ImageService imageService)
    {
        _repository = repository;
        _repositoryPreference = repositoryPreference;
        _mapper = mapper;
        _imageService = imageService;
    }

    public async Task<int?> CreateAsync(Place place)
    {
        var existingPlace = await _repository.GetByIdAsync(place.Id);
        if (existingPlace != null)
        {
            throw new Exception("Place with the same ID already exists.");
        }

        if (place.Image?.Length > 0)
        {
            place.ImagePath = await _imageService.SaveImage(place.ImageExtensions, place.Image);
        }

        DataAccess.DAL.Place entity = _mapper.Map<DataAccess.DAL.Place>(place);

        if (place.PreferencesId != null && place.PreferencesId.Any())
        {
            var preferences = await _repositoryPreference.GetPreferencesByIdsAsync(place.PreferencesId);
            entity.Preferences = preferences;
        }

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
        if (place.Image?.Length > 0)
        {
            if (place.ImagePath != null)
            {
                place.ImagePath = await _imageService.ReplaceImage(place.ImagePath, place.Image);
            }
            else
            {
                place.ImagePath = await _imageService.SaveImage(place.ImageExtensions, place.Image);
            }
        }

        DataAccess.DAL.Place entity = _mapper.Map<DataAccess.DAL.Place>(place) ?? new DataAccess.DAL.Place();

        if (place.PreferencesId != null && place.PreferencesId.Any())
        {
            var preferences = await _repositoryPreference.GetPreferencesByIdsAsync(place.PreferencesId);
            entity.Preferences = preferences;
        }
        else
        {
            entity.Preferences = null;
        }

        await _repository.UpdateAsync(entity ?? new DataAccess.DAL.Place());
    }


    public async Task DeleteAsync(int id)
    {
        DataAccess.DAL.Place? entity = await _repository.GetByIdAsync(id);
        Place? place = _mapper.Map<Place>(entity);
        ImageService.DeleteImage(place.ImagePath);

        await _repository.DeleteAsync(id);
    }
}

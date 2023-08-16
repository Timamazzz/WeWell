using AutoMapper;
using DataAccess.Repositories;
using Domain.Interfaces;

namespace Domain.Services;

public class PlaceService /*: IService<Place>*/
{
    private readonly PlaceRepository _repository;
    private readonly PreferenceRepository _repositoryPreference;
    private readonly IMapper _mapper;

    public PlaceService(PlaceRepository repository, PreferenceRepository repositoryPreference, IMapper mapper)
    {
        _repository = repository;
        _repositoryPreference = repositoryPreference;
        _mapper = mapper;
    }

    /*public async Task<int?> CreateAsync(Place place)
    {
        var existingPlace = await _repository.GetByIdAsync(place.Id);
        if (existingPlace != null)
        {
            throw new Exception("Place with the same ID already exists.");
        }

        DataAccess.DAL.Place entity = _mapper.Map<DataAccess.DAL.Place>(place);

        if (place.PreferencesId != null && place.PreferencesId.Any())
        {
            var preferences = await _repositoryPreference.GetPreferencesByIdsAsync(place.PreferencesId);
            entity.Preferences = preferences;
        }

        int? id = await _repository.CreateAsync(entity);

        if (place.Image?.Length > 0)
        {
            if (id != null) 
            {
                string pathToUpload = Path.Combine("Uploads", "Images", "Places", id.ToString());
                _imageService._webRootPath = _webRootPath;
                place.ImagePath = await _imageService.SaveImage(place.ImageExtensions, place.Image, pathToUpload);
            }
        }

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

        if (place.Image?.Length > 0)
        {
            string pathToUpload = Path.Combine("Uploads", "Images", "Places", place.Id.ToString());
            _imageService._webRootPath = _webRootPath;

            if (await IsImageExist(place.Id))
            {
                place.ImagePath = await _imageService.ReplaceImage(entity.ImagePath, place.Image, pathToUpload);
            }
            else
            {
                place.ImagePath = await _imageService.SaveImage(place.ImageExtensions, place.Image, pathToUpload);
            }
        }

        await _repository.UpdateAsync(entity ?? new DataAccess.DAL.Place());
    }


    public async Task DeleteAsync(int id)
    {
        DataAccess.DAL.Place? entity = await _repository.GetByIdAsync(id);
        Place? place = _mapper.Map<Place>(entity);
        _imageService._webRootPath = _webRootPath;
        await _imageService.DeleteImage(place.ImagePath);

        await _repository.DeleteAsync(id);
    }
    
    public async Task<bool> IsImageExist(int id)
    {
        DataAccess.DAL.Place? entity = await _repository.GetByIdAsync(id);
        return !(entity.ImagePath == null || entity.ImagePath == "");
    }*/
}

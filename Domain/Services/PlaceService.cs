using AutoMapper;
using DataAccess.Repositories;
using Domain.DTO;
using Domain.Interfaces;

namespace Domain.Services;

public class PlaceService : IService<Place>
{
    private readonly PlaceRepository _repository;
    private readonly PreferenceRepository _preferenceRepository;
    private readonly IMapper _mapper;
    private readonly string _pathToUpload;

    public PlaceService(PlaceRepository repository, PreferenceRepository preferenceRepository, IMapper mapper)
    {
        _repository = repository;
        _preferenceRepository = preferenceRepository;
        _mapper = mapper;
        _pathToUpload = "wwwroot/Upload/Images/Places";
    }

    public async Task<int?> CreateAsync(Place place)
    {
        var existingPlace = await _repository.GetByIdAsync(place.Id);
        if (existingPlace != null)
        {
            throw new Exception("Place with the same ID already exists.");
        }

        //place.ImagePath = await SaveImage(place);

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
        DataAccess.DAL.Place? entity = await _repository.GetByIdAsync(id);
        Place? place = _mapper.Map<Place>(entity);
        DeleteImage(place.ImagePath);

        await _repository.DeleteAsync(id);
    }

    private async Task<string> SaveImage(Place place)
    {
        var fileName = $"{Guid.NewGuid()}{place.ImageExtensions}";
        var filePath = Path.Combine(_pathToUpload, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await fileStream.WriteAsync(place.Image);
        }

        return filePath;
    }

    private void DeleteImage(string filePath)
    {
        if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}

using AutoMapper;
using DataAccess.Repositories;
using Domain.DTO;
using Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace Domain.Services;

public class UserService : IService<User>
{
    private readonly UserRepository _repository;
    private readonly PreferenceRepository _repositoryPreference;
    private readonly ImageService _imageService;
    private readonly SmsService _smsService;
    private readonly IMapper _mapper;
    public string _webRootPath;

    public UserService(UserRepository repository, PreferenceRepository repositoryPreference, IMapper mapper, ImageService imageService, SmsService smsService)
    {
        _repository = repository;
        _repositoryPreference = repositoryPreference;
        _mapper = mapper;
        _imageService = imageService;
        _smsService = smsService;
    }

    public async Task<int?> CreateAsync(User user)
    {
        var existingUser = await _repository.GetByIdAsync(user.Id);
        if (existingUser != null)
        {
            throw new Exception("User with the same ID already exists.");
        }

        DataAccess.DAL.User entity = _mapper.Map<DataAccess.DAL.User>(user);

        if (user.PreferencesId != null && user.PreferencesId.Any())
        {
            var preferences = await _repositoryPreference.GetPreferencesByIdsAsync(user.PreferencesId);
            entity.Preferences = preferences;
        }

        int? id = await _repository.CreateAsync(entity);

        if (user.Avatar?.Length > 0)
        {
            if (id != null)
            {
                string pathToUpload = Path.Combine("Uploads", "Images", "Users", id.ToString());
                _imageService._webRootPath = _webRootPath;
                entity.AvatarPath = await _imageService.SaveImage(user.AvatarExtensions, user.Avatar, pathToUpload);
                _repository.UpdateAsync(entity);
            }
        }

        return id;
    }

    public async Task<List<User>?> GetAllAsync()
    {
        List<DataAccess.DAL.User>? entities = await _repository.GetAllAsync();
        List<User>? users = _mapper.Map<List<User>>(entities);
        return users;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        DataAccess.DAL.User? entity = await _repository.GetByIdAsync(id);
        User? user = _mapper.Map<User>(entity);
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        DataAccess.DAL.User entity = _mapper.Map<DataAccess.DAL.User>(user);
        
        if (user.Avatar?.Length > 0)
        {
            string pathToUpload = Path.Combine("Uploads", "Images", "Users", user.Id.ToString());
            _imageService._webRootPath = _webRootPath;

            if (entity.AvatarPath != null)
            {
                entity.AvatarPath = await _imageService.ReplaceImage(entity.AvatarPath, user.Avatar, pathToUpload);
            }
            else
            {
                entity.AvatarPath = await _imageService.SaveImage(user.AvatarExtensions, user.Avatar, pathToUpload);
            }
        }

        if (user.PreferencesId != null && user.PreferencesId.Any())
        {
            var preferences = await _repositoryPreference.GetPreferencesByIdsAsync(user.PreferencesId);
            entity.Preferences = preferences;
        }
        else
        {
            entity.Preferences = null;
        }

        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        DataAccess.DAL.User? entity = await _repository.GetByIdAsync(id);
        User? user = _mapper.Map<User>(entity);

        _imageService._webRootPath = _webRootPath;
        await _imageService.DeleteImage(user.AvatarPath);

        await _repository.DeleteAsync(id);
    }

    public async Task<User> GetByPhoneNumberAsync(string phoneNumber)
    {
        var userDTO = await _repository.GetByPhoneNumberAsync(phoneNumber);
        return _mapper.Map<User>(userDTO);
    }

    public string SendSms(string phoneNumber)
    {
        string code = _smsService.SendSms(phoneNumber);

        return code;
    }
}

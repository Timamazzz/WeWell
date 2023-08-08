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
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string _pathToUpload = "/Uploads/Images/Users/Avatars";

    public UserService(UserRepository repository, PreferenceRepository repositoryPreference, IMapper mapper, ImageService imageService, SmsService smsService, IWebHostEnvironment webHostEnvironment)
    {
        _repository = repository;
        _repositoryPreference = repositoryPreference;
        _mapper = mapper;
        _imageService = imageService;
        _smsService = smsService;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<int?> CreateAsync(User user)
    {
        var existingUser = await _repository.GetByIdAsync(user.Id);
        if (existingUser != null)
        {
            throw new Exception("User with the same ID already exists.");
        }

        if (user.Avatar?.Length > 0)
        {
            user.AvatarPath = await _imageService.SaveImage(user.AvatarExtensions, user.Avatar, _webHostEnvironment.WebRootPath + _pathToUpload);
        }

        DataAccess.DAL.User entity = _mapper.Map<DataAccess.DAL.User>(user);

        if (user.PreferencesId != null && user.PreferencesId.Any())
        {
            var preferences = await _repositoryPreference.GetPreferencesByIdsAsync(user.PreferencesId);
            entity.Preferences = preferences;
        }

        int? id = await _repository.CreateAsync(entity);

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
        if (user.Avatar?.Length > 0)
        {
            if (user.AvatarPath != null)
            {
                user.AvatarPath = await _imageService.ReplaceImage(user.AvatarPath, user.Avatar, _webHostEnvironment.WebRootPath + _pathToUpload);
            }
            else
            {
                user.AvatarPath = await _imageService.SaveImage(user.AvatarExtensions, user.Avatar, _webHostEnvironment.WebRootPath + _pathToUpload);
            }
        }

        DataAccess.DAL.User entity = _mapper.Map<DataAccess.DAL.User>(user) ?? new DataAccess.DAL.User();

        if (user.PreferencesId != null && user.PreferencesId.Any())
        {
            var preferences = await _repositoryPreference.GetPreferencesByIdsAsync(user.PreferencesId);
            entity.Preferences = preferences;
        }
        else
        {
            entity.Preferences = null;
        }

        await _repository.UpdateAsync(entity ?? new DataAccess.DAL.User());
    }

    public async Task DeleteAsync(int id)
    {
        DataAccess.DAL.User? entity = await _repository.GetByIdAsync(id);
        User? user = _mapper.Map<User>(entity);
        ImageService.DeleteImage(user.AvatarPath);

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

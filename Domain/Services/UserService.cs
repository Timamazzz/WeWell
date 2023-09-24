using AutoMapper;
using DataAccess.Repositories;
using Domain.DataTransferObjects;
using Domain.Interfaces;
using BCrypt.Net;

namespace Domain.Services;

public class UserService : IService<User>
{
    private readonly UserRepository _repository;
    private readonly PreferenceRepository _repositoryPreference;
    private readonly IMapper _mapper;

    public UserService(UserRepository repository, PreferenceRepository repositoryPreference, IMapper mapper)
    {
        _repository = repository;
        _repositoryPreference = repositoryPreference;
        _mapper = mapper;
    }

    public async Task<int?> CreateAsync(User user)
    {
        DataAccess.Models.User entity = _mapper.Map<DataAccess.Models.User>(user);
        
        var preferencesIdRange = user.Preferences.Select(p => p.Id).ToList();
        var newPreferences = _repositoryPreference.GetPreferencesByIdRange(preferencesIdRange);
        entity.Preferences = newPreferences;
        
        int? id = await _repository.CreateAsync(entity);
        return id;
    }

    public async Task<int?> RegisterAsync(User user)
    {
        var existingUser = await GetByPhoneNumberAsync(user.PhoneNumber);
        if (existingUser != null)
        {
            return null;
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        int? id = await CreateAsync(user);
        return id;
    }
    
    public async Task<User?> AuthenticateUserAsync(User user, string authPassword)
    {
        if (BCrypt.Net.BCrypt.Verify(authPassword, user.Password))
        {
            return user;
        }
        else
        {
            return null;
        }
    }
    
    public async Task<List<User>?> GetAllAsync()
    {
        List<DataAccess.Models.User>? entities = await _repository.GetAllAsync();
        List<User>? users = _mapper.Map<List<User>>(entities);
        return users;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        DataAccess.Models.User? entity = await _repository.GetByIdAsync(id);
        User? user = _mapper.Map<User>(entity);
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        var entity = await _repository.GetByIdAsync(user.Id);
        _mapper.Map(user, entity);
        
        var preferencesIdRange = user.Preferences.Select(p => p.Id).ToList();
        var newPreferences = _repositoryPreference.GetPreferencesByIdRange(preferencesIdRange);
        entity.Preferences = newPreferences;
        
        await _repository.UpdateAsync(entity);
    }


    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<User> GetByPhoneNumberAsync(string phoneNumber)
    {
        var userDto = await _repository.GetByPhoneNumberAsync(phoneNumber);
        return _mapper.Map<User>(userDto);
    }
    
    public async Task<List<string>> GetExistingUsersByPhoneNumbersBatch(List<string> phoneNumbers)
    {
        var existingUsers = new List<string>();

        foreach (var phoneNumber in phoneNumbers)
        {
            User? userDto = await GetByPhoneNumberAsync(phoneNumber);
            if (userDto != null)
            {
                existingUsers.Add(phoneNumber);
            }
        }

        return existingUsers;
    }
    
}

using AutoMapper;
using DataAccess.Repositories;
using Domain.DataTransferObjects;
using Domain.Interfaces;

namespace Domain.Services;

public class UserService : IService<User>
{
    private readonly UserRepository _repository;
    private readonly PreferenceRepository _repositoryPreference;
    private readonly SmsService _smsService;
    private readonly IMapper _mapper;

    public UserService(UserRepository repository, PreferenceRepository repositoryPreference, IMapper mapper, SmsService smsService)
    {
        _repository = repository;
        _repositoryPreference = repositoryPreference;
        _mapper = mapper;
        _smsService = smsService;
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

    public string SendSms(string phoneNumber)
    {
        string code = _smsService.SendSms(phoneNumber);
        return code;
    }
}

using AutoMapper;
using DataAccess.Repositories;
using Domain.DTO;
using Domain.Interfaces;

namespace Domain.Services;

public class UserService : IService<User>
{
    private readonly UserRepository _repository;
    private readonly IMapper _mapper;

    public UserService(UserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int?> CreateAsync(User user)
    {
        DataAccess.DAL.User entity = _mapper.Map<DataAccess.DAL.User>(user);
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
        DataAccess.DAL.User entity = _mapper.Map<DataAccess.DAL.User>(user);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}

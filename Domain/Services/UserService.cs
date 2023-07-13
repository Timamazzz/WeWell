using DataAccess.Repositories;
using DataAccess.DTO;
using Domain.Interfaces;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Domain.Services;

public class UserService : IService<User>
{
    private readonly UserRepository _repository;

    public UserService(UserRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Добавление нового пользователя в бд
    /// </summary>
    /// <param name="user"></param>
    /// <returns>ID нового пользователя</returns>
    public async Task<int?> CreateAsync(User user)
    {
        int? id = await _repository.CreateAsync(user);
        return id;
    }

    // <summary>
    /// Получение всех пользоваелей
    /// </summary>
    public async Task<List<User>?> GetAllAsync()
    {
        List<User>? users = await _repository.GetAllAsync();
        return users;
    }


    /// <summary>
    /// Получение пользователя по id
    /// </summary>
    /// <param name="id"></param>
    public async Task<User?> GetByIdAsync(int id)
    {
        User? user = await _repository.GetByIdAsync(id);
        return user;
    }

    /// <summary>
    /// Обновление пользователя по id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="user"></param>
    public async Task UpdateAsync(User user)
    {
         await _repository.UpdateAsync(user);
    }

    /// <summary>
    /// Удаление пользователя по id
    /// </summary>
    /// <param name="id"></param>
    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }


}

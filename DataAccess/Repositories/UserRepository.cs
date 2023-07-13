﻿using DataAccess.DAL;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class UserRepository : IRepository<User>
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<int?> CreateAsync(User user)
    {
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();

        return user.Id;
    }

    public async Task<List<User>?> GetAllAsync()
    {
        List<User> users = await _context.Users.OrderBy(user => user.Id).ToListAsync();
        return users;
    }

    public async Task<User?> GetByIdAsync(int? id)
    {
        User? user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        _context.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        User? user = await _context.Users.FindAsync(id);

        if (user != null)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
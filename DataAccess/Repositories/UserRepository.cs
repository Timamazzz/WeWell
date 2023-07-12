using AutoMapper;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class UserRepository : IRepository<Domain.DTO.User, DAL.User>
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public UserRepository(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int?> CreateAsync(Domain.DTO.User userData)
    {
        DAL.User user = _mapper.Map<DAL.User>(userData);

        await _context.AddAsync(user);
        await _context.SaveChangesAsync();

        return user.Id;
    }

    public async Task<List<Domain.DTO.User>> GetAllAsync()
    {
        List<DAL.User> users = await _context.Users.OrderBy(user => user.Id).ToListAsync();
        List<Domain.DTO.User> result = _mapper.Map<List<Domain.DTO.User>>(users);

        return result;
    }

    public async Task<Domain.DTO.User?> GetByIdAsync(int? id)
    {
        DAL.User? user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
        Domain.DTO.User? result = _mapper.Map<Domain.DTO.User>(user);

        return result;
    }

    public async Task UpdateAsync(Domain.DTO.User userData)
    {
        DAL.User? user = await _context.Users.FindAsync(userData.Id);

        if (user != null)
        {
            _mapper.Map(userData, user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        DAL.User? user = await _context.Users.FindAsync(id);

        if (user != null)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}

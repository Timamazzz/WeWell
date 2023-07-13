using AutoMapper;
using DataAccess.Repositories;
using Domain.DTO;
using Domain.Interfaces;

namespace Domain.Services;

public class MeetingService : IService<Meeting>
{
    private readonly MeetingRepository _repository;
    private readonly IMapper _mapper;

    public MeetingService(MeetingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int?> CreateAsync(Meeting meeting)
    {
        DataAccess.DAL.Meeting entity = _mapper.Map<DataAccess.DAL.Meeting>(meeting);
        int? id = await _repository.CreateAsync(entity);
        return id;
    }

    public async Task<List<Meeting>?> GetAllAsync()
    {
        List<DataAccess.DAL.Meeting>? entities = await _repository.GetAllAsync();
        List<Meeting>? meetings = _mapper.Map<List<Meeting>>(entities);
        return meetings;
    }

    public async Task<Meeting?> GetByIdAsync(int id)
    {
        DataAccess.DAL.Meeting? entity = await _repository.GetByIdAsync(id);
        Meeting? meeting = _mapper.Map<Meeting>(entity);
        return meeting;
    }

    public async Task UpdateAsync(Meeting meeting)
    {
        DataAccess.DAL.Meeting entity = _mapper.Map<DataAccess.DAL.Meeting>(meeting);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}

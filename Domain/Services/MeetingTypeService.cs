﻿using AutoMapper;
using DataAccess.Repositories;
using Domain.DataTransferObjects;
using Domain.Interfaces;

namespace Domain.Services;

public class MeetingTypeService : IService<MeetingType>
{
    private readonly MeetingTypeRepository _repository;
    private readonly IMapper _mapper;

    public MeetingTypeService(MeetingTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int?> CreateAsync(MeetingType type)
    {
        DataAccess.Models.MeetingType entity = _mapper.Map<DataAccess.Models.MeetingType>(type);
        int? id = await _repository.CreateAsync(entity);
        return id;
    }

    public async Task<List<MeetingType>?> GetAllAsync()
    {
        List<DataAccess.Models.MeetingType>? entities = await _repository.GetAllAsync();
        List<MeetingType>? types = _mapper.Map<List<MeetingType>>(entities);
        return types;
    }

    public async Task<MeetingType?> GetByIdAsync(int id)
    {
        DataAccess.Models.MeetingType? entity = await _repository.GetByIdAsync(id);
        MeetingType? type = _mapper.Map<MeetingType>(entity);
        return type;
    }

    public async Task UpdateAsync(MeetingType type)
    {
        DataAccess.Models.MeetingType entity = _mapper.Map<DataAccess.Models.MeetingType>(type);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}

using AutoMapper;
using DataAccess.Repositories;
using Domain.DTO;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class MeetingStatusService : IService<MeetingStatus>
    {
        private readonly MeetingStatusRepository _repository;
        private readonly IMapper _mapper;

        public MeetingStatusService(MeetingStatusRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int?> CreateAsync(MeetingStatus status)
        {
            DataAccess.DAL.MeetingStatus entity = _mapper.Map<DataAccess.DAL.MeetingStatus>(status);
            int? id = await _repository.CreateAsync(entity);
            return id;
        }

        public async Task<List<MeetingStatus>?> GetAllAsync()
        {
            List<DataAccess.DAL.MeetingStatus>? entities = await _repository.GetAllAsync();
            List<MeetingStatus>? statuses = _mapper.Map<List<MeetingStatus>>(entities);
            return statuses;
        }

        public async Task<MeetingStatus?> GetByIdAsync(int id)
        {
            DataAccess.DAL.MeetingStatus? entity = await _repository.GetByIdAsync(id);
            MeetingStatus? status = _mapper.Map<MeetingStatus>(entity);
            return status;
        }

        public async Task UpdateAsync(MeetingStatus status)
        {
            DataAccess.DAL.MeetingStatus entity = _mapper.Map<DataAccess.DAL.MeetingStatus>(status);
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}

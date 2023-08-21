using AutoMapper;
using DataAccess.Repositories;
using Domain.DataTransferObjects;
using Domain.Interfaces;

namespace Domain.Services
{
    public class MeetingStatusService
    {
        private readonly IMapper _mapper;

        public MeetingStatusService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<List<MeetingStatus>?> GetAllAsync()
        {
            
            return Enum.GetValues(typeof(MeetingStatus)).Cast<MeetingStatus>().ToList();
        }
    }
}

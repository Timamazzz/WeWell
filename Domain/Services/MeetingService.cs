using AutoMapper;
using DataAccess.Repositories;
using Domain.Interfaces;

namespace Domain.Services;

public class MeetingService/* : IService<Meeting>*/
{
    private readonly MeetingRepository _repository;
    private readonly UserService _userService;
    private readonly PlaceService _placeService;
    private readonly PreferenceService _preferenceService;
    private readonly IMapper _mapper;

    public MeetingService(MeetingRepository repository, IMapper mapper, UserService userService, PlaceService placeService, PreferenceService preferenceService)
    {
        _repository = repository;
        _mapper = mapper;
        _userService = userService;
        _placeService = placeService;
        _preferenceService = preferenceService;
    }

    /*public async Task<int?> CreateAsync(Meeting meeting)
    {
        DataAccess.DAL.Meeting entity = _mapper.Map<DataAccess.DAL.Meeting>(meeting);
        
        if (meeting.CreatorId.HasValue)
        {
            DataAccess.DAL.User creator = _mapper.Map<DataAccess.DAL.User>(await _userService.GetByIdAsync(meeting.CreatorId.Value));
        }
        if (meeting.GuestId.HasValue)
        {
            DataAccess.DAL.User guest = _mapper.Map<DataAccess.DAL.User>(await _userService.GetByIdAsync(meeting.GuestId.Value));
        }
        
        
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

    private List<Preference> GetPreferences(User creator, User guest)
    {
        if (creator.isAllPreferences == true && guest.isAllPreferences == true)
        {
            // Если оба пользователь установили isAllPreferences в true,
            // вернуть массив всех предпочтений
            return creator.Preferences.Concat(guest.Preferences).ToList();
        }
        else if (creator.isAllPreferences == true)
        {
            // Если только creator установил isAllPreferences в true,
            // вернуть предпочтения guest
            return guest.Preferences;
        }
        else if (guest.isAllPreferences == true)
        {
            // Если только guest установил isAllPreferences в true,
            // вернуть предпочтения creator
            return creator.Preferences;
        }
        else
        {
            // Если оба пользователь установили isAllPreferences в false,
            // вернуть общие предпочтения, если таковые есть,
            // иначе вернуть все предпочтения
            var commonPreferences = creator.Preferences.Intersect(guest.Preferences).ToList();
            return commonPreferences.Count > 0 ? commonPreferences : creator.Preferences.Concat(guest.Preferences).ToList();
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }*/
}

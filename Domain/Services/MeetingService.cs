using AutoMapper;
using DataAccess.Repositories;
using Domain.DataTransferObjects;
using Domain.Interfaces;

namespace Domain.Services;

public class MeetingService : IService<Meeting>
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

    public async Task<int?> CreateAsync(Meeting meeting)
    {
        if(meeting.CreatorId.HasValue)
            meeting.Creator = await _userService.GetByIdAsync(meeting.CreatorId.Value);
        
        if(meeting.GuestId.HasValue)
            meeting.Guest = await _userService.GetByIdAsync(meeting.GuestId.Value);
        
        List<Preference>? preferences = await GetPreferences(meeting.Creator, meeting.Guest);
        
        Place? suitablePlace = await FindSuitablePlace(preferences, meeting.MinPrice.Value, meeting.MaxPrice.Value, meeting.MinDurationHours.Value, meeting.MaxDurationHours.Value, meeting.Type.Id);
        
        /*DataAccess.Models.Meeting entity = _mapper.Map<DataAccess.Models.Meeting>(meeting);
        int? id = await _repository.CreateAsync(entity);*/
        
        return 0;
    }

    public async Task<List<Meeting>?> GetAllAsync()
    {
        List<DataAccess.Models.Meeting>? entities = await _repository.GetAllAsync();
        List<Meeting>? meetings = _mapper.Map<List<Meeting>>(entities);
        return meetings;
    }

    public async Task<Meeting?> GetByIdAsync(int id)
    {
        DataAccess.Models.Meeting? entity = await _repository.GetByIdAsync(id);
        Meeting? meeting = _mapper.Map<Meeting>(entity);
        return meeting;
    }

    public async Task UpdateAsync(Meeting meeting)
    {
        DataAccess.Models.Meeting entity = _mapper.Map<DataAccess.Models.Meeting>(meeting);
        await _repository.UpdateAsync(entity);
    }

    private async Task<List<Preference>>? GetPreferences(User? creator, User? guest)
    {
        if (creator.IsAllPreferences == true && guest.IsAllPreferences == true)
        {
            return await _preferenceService.GetAllAsync();
        }
        else if (creator.IsAllPreferences == true)
        {
            return guest.Preferences;
        }
        else if (guest.IsAllPreferences == true)
        {
            return creator.Preferences;
        }
        else
        {
            var commonPreferences = creator.Preferences.Intersect(guest.Preferences).ToList();
            return commonPreferences.Count > 0 ? commonPreferences : creator.Preferences.Concat(guest.Preferences).ToList();
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
    
    private async Task<Place?> FindSuitablePlace(List<Preference> preferences, int minPrice, int maxPrice, int minDuration, int maxDuration, int meetingTypeId)
    {
        Place? place = await _placeService.GetByForMeetingCreate(preferences, minPrice, maxPrice, minDuration, maxDuration, meetingTypeId);

        if (place != null)
        {
            return place;
        }
        else
        {
            if (minPrice >= 500)
            {
                minPrice -= 500;
            }
            else if (minDuration >= 1)
            {
                minDuration--;
            }
            else
            {
                return null;
            }
            return await FindSuitablePlace(preferences, minPrice, maxPrice, minDuration, maxDuration, meetingTypeId);
        }
    }

}

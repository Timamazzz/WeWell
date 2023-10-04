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
    private readonly PlaceRepository _placeRepository;
    private readonly UserRepository _userRepository;
    private readonly MeetingTypeRepository _meetingTypeRepository;

    public MeetingService(MeetingRepository repository, IMapper mapper, UserService userService, 
        PlaceService placeService, PreferenceService preferenceService, PlaceRepository placeRepository, 
        UserRepository userRepository, MeetingTypeRepository meetingTypeRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _userService = userService;
        _placeService = placeService;
        _preferenceService = preferenceService;
        _placeRepository = placeRepository;
        _userRepository = userRepository;
        _meetingTypeRepository = meetingTypeRepository;
    }

    public async Task<int?> CreateAsync(Meeting meeting)
    {
        if(meeting.CreatorId.HasValue)
            meeting.Creator = await _userService.GetByIdAsync(meeting.CreatorId.Value);
        
        if(meeting.GuestId.HasValue)
            meeting.Guest = await _userService.GetByIdAsync(meeting.GuestId.Value);
        
        List<Preference>? preferences = await GetPreferences(meeting.Creator, meeting.Guest);
        
        int? suitablePlaceId = await FindSuitablePlace(preferences, meeting.MaxPrice.Value, meeting.MaxDurationHours.Value, meeting.Type.Id);
        
        meeting.IsActive = true;
        meeting.IsShowForCreator = true;
        meeting.IsShowForGuest = true;
        meeting.IsArchive = false;
        meeting.Status = DataAccess.Enums.MeetingStatus.Invited.ToString();
        
        DataAccess.Models.Meeting entity = _mapper.Map<DataAccess.Models.Meeting>(meeting);

        entity.Place = await _placeRepository.GetByIdAsync(suitablePlaceId);
        entity.Creator = await _userRepository.GetByIdAsync(meeting.CreatorId);
        entity.Guest = await _userRepository.GetByIdAsync(meeting.GuestId);
        entity.Type = await _meetingTypeRepository.GetByIdAsync(meeting.Type.Id);
        entity.DateTimeEnd = meeting.Date.AddHours((double)meeting.MaxDurationHours);
        int? id = await _repository.CreateAsync(entity);
        
        return id;
    }

    public async Task<List<Meeting>?> GetAllAsync()
    {
        List<DataAccess.Models.Meeting>? entities = await _repository.GetAllAsync();
        List<Meeting>? meetings = _mapper.Map<List<Meeting>>(entities);
        Console.WriteLine("String for debug");
        return meetings;
    }

    public async Task<Meeting?> GetByIdAsync(int id)
    {
        DataAccess.Models.Meeting? entity = await _repository.GetByIdAsync(id);
        Meeting? meeting = _mapper.Map<Meeting>(entity);
        return meeting;
    }
    
    public async Task<List<Meeting>?> GetMeetingsByUserIdAsync(int userId)
    {
        List<DataAccess.Models.Meeting>? entities = await _repository.GetMeetingsByUserIdAsync(userId);
        
        List<Meeting>? meetings = _mapper.Map<List<Meeting>>(entities);
        
        return meetings;
    }
    
    public async Task UpdateAsync(Meeting meeting)
    {
        DataAccess.Models.Meeting entity = await _repository.GetByIdAsync(meeting.Id);
        
        entity.IsActive = meeting.IsActive;
        entity.IsShowForCreator = meeting.IsShowForCreator;
        entity.IsShowForGuest = meeting.IsShowForGuest;
        
        if (Enum.TryParse<DataAccess.Enums.MeetingStatus>(meeting.Status, out var parsedStatus))
        {
            entity.Status = parsedStatus.ToString();
        }
        
        await _repository.UpdateAsync(entity);
    }
    
    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
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
            var commonPreferences = creator.Preferences
                .Where(creatorPref => guest.Preferences.Any(guestPref => creatorPref.Id == guestPref.Id))
                .ToList();
    
            if (commonPreferences.Count == 0)
            {
                commonPreferences = creator.Preferences.Concat(guest.Preferences).ToList();
            }

            return commonPreferences;
        }
    }
    
    private async Task<int?> FindSuitablePlace(List<Preference> preferences, int maxPrice, int maxDuration, int meetingTypeId)
    {
        Place? place = await _placeService.GetByForMeetingCreate(preferences, maxPrice, maxDuration, meetingTypeId);

        if (place != null)
        {
            return place.Id;
        }
        else
        {
            return null;
        }
    }
}

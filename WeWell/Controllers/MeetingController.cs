using AutoMapper;
using Domain.DataTransferObjects;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WeWell.Models.Meetings;
using Microsoft.AspNetCore.Authorization;

namespace WeWell.Controllers;


[ApiController]
[Route("meetings")]
public class MeetingsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly MeetingService _service;

    public MeetingsController(IMapper mapper, MeetingService service)
    {
        _mapper = mapper;
        _service = service;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(int?), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Create a new meeting")]
    public async Task<ActionResult<int?>> CreateMeeting(MeetingCreate meeting)
    {
        MeetingGet meetingGet = null;

        try
        {
            var meetingDto = _mapper.Map<Domain.DataTransferObjects.Meeting>(meeting);
            var meetingId = await _service.CreateAsync(meetingDto);
            if (meetingId != null)
                meetingGet = _mapper.Map<MeetingGet>(await _service.GetByIdAsync(meetingId.Value));

            return Ok(meetingGet);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(List<MeetingGet>), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Get all meetings")]
    public async Task<ActionResult<List<MeetingGet>>> GetAllMeetings()
    {
        try
        {
            var meetingsDto = await _service.GetAllAsync();
            var meetings = _mapper.Map<List<MeetingGet>>(meetingsDto);
            return Ok(meetings);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpGet("user/{userId}")]
    [Authorize]
    [ProducesResponseType(typeof(List<MeetingGet>), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Get meetings of a specific user")]
    public async Task<ActionResult<List<MeetingGet>>> GetUserMeetings(int userId)
    {
        try
        {
            var meetingsDto = await _service.GetMeetingsByUserIdAsync(userId);
            var meetings = _mapper.Map<List<MeetingGet>>(meetingsDto);
            return Ok(meetings);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Update a meeting")]
    public async Task<ActionResult<string>> UpdateMeeting(MeetingUpdate meetingUpdate)
    {
        try
        {
            Meeting meetingDto = _mapper.Map<Meeting>(meetingUpdate);
            await _service.UpdateAsync(meetingDto);

            return Ok("Meeting updated successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpDelete]
    [Authorize]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Delete all meetings")]
    public async Task<ActionResult<string>> DeleteMeetings()
    {
        try
        {
            await _service.DeleteAllMeetingsAsync();

            return Ok("Delete all meetings successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
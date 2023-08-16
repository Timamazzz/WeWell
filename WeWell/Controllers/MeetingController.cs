using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WeWell.Models.Meetings;

namespace WeWell.Controllers;


[ApiController]
[Route("meetings")]
public class MeetingsController : ControllerBase
{
    private readonly IMapper _mapper;

    public MeetingsController(IMapper mapper)
    {
        _mapper = mapper;
    }

    /*[HttpPost]
    [ProducesResponseType(typeof(int?), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Create a new meeting")]
    public async Task<ActionResult<int?>> CreateMeeting(MeetingCreate meeting)
    {
        try
        {
            var meetingDto = _mapper.Map<Domain.DTO.Meeting>(meeting);
            return Ok(meetingDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }*/
}
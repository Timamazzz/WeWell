using AutoMapper;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WeWell.Models;

namespace WeWell.Controllers;

[ApiController]
[Route("meeting-statuses")]
public class MeetingStatusController : ControllerBase
{
    private readonly MeetingStatusService _service;
    private readonly IMapper _mapper;

    public MeetingStatusController(MeetingStatusService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<MeetingStatus>), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Get all meeting statuses")]
    public async Task<ActionResult<List<MeetingStatus>>> GetAll()
    {
        try
        {
            var meetingStatuses = _service.GetAllAsync();
            return Ok(meetingStatuses);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
}
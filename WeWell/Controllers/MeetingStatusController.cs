﻿using AutoMapper;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

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
    [Authorize]
    [ProducesResponseType(typeof(List<string>), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Get all meeting statuses")]
    public async Task<ActionResult<List<string>>> GetAll()
    {
        try
        {
            var meetingStatuses = Enum.GetNames(typeof(DataAccess.Enums.MeetingStatus))
                .Select(status => status.ToString())
                .ToList();
            return Ok(meetingStatuses);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
}
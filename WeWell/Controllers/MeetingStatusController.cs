﻿using AutoMapper;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WeWell.ViewModels;

namespace WeWell.Controllers;

[ApiController]
[Route("meetingstatus")]
public class MeetingStatusController : ControllerBase
{
    private readonly MeetingStatusService _service;
    private readonly IMapper _mapper;

    public MeetingStatusController(MeetingStatusService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(typeof(int?), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Create a new meeting status")]
    public async Task<ActionResult<int?>> CreateMeetingStatus(MeetingStatus status)
    {
        try
        {
            var statusDTO = _mapper.Map<Domain.DTO.MeetingStatus>(status);
            var id = await _service.CreateAsync(statusDTO);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<MeetingStatus>), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Get all meeting statuses")]
    public async Task<ActionResult<List<MeetingStatus>>> GetAllMeetingStatuses()
    {
        try
        {
            var statusesDTO = await _service.GetAllAsync();
            var statuses = _mapper.Map<List<MeetingStatus>>(statusesDTO);
            return Ok(statuses);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MeetingStatus), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Get a meeting status by ID")]
    public async Task<ActionResult<MeetingStatus>> GetMeetingStatus(int id)
    {
        try
        {
            var statusDTO = await _service.GetByIdAsync(id);

            if (statusDTO == null)
            {
                return NotFound();
            }

            var status = _mapper.Map<MeetingStatus>(statusDTO);
            return Ok(status);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    [ProducesResponseType(typeof(void), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Update a meeting status")]
    public async Task<ActionResult> UpdateMeetingStatus(MeetingStatus status)
    {
        try
        {
            Domain.DTO.MeetingStatus statusDto = _mapper.Map<Domain.DTO.MeetingStatus>(status);
            await _service.UpdateAsync(statusDto);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Delete a meeting status by ID")]
    public async Task<ActionResult> DeleteMeetingStatus(int id)
    {
        try
        {
            var status = await _service.GetByIdAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}

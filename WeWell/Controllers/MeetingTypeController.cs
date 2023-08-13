using AutoMapper;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WeWell.ViewModels;

namespace WeWell.Controllers;

[ApiController]
[Route("meetingtypes")]
public class MeetingTypeController : ControllerBase
{
    private readonly MeetingTypeService _service;
    private readonly IMapper _mapper;

    public MeetingTypeController(MeetingTypeService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(typeof(int?), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Create a new meeting type")]
    public async Task<ActionResult<int?>> CreateMeetingType(MeetingType type)
    {
        try
        {
            var typeDTO = _mapper.Map<Domain.DTO.MeetingType>(type);
            var id = await _service.CreateAsync(typeDTO);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<MeetingType>), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Get all meeting types")]
    public async Task<ActionResult<List<MeetingType>>> GetAllMeetingTypes()
    {
        try
        {
            var typesDTO = await _service.GetAllAsync();
            var types = _mapper.Map<List<MeetingType>>(typesDTO);
            return Ok(types);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MeetingType), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Get a meeting type by ID")]
    public async Task<ActionResult<MeetingType>> GetMeetingType(int id)
    {
        try
        {
            var typeDTO = await _service.GetByIdAsync(id);

            if (typeDTO == null)
            {
                return NotFound();
            }

            var type = _mapper.Map<MeetingType>(typeDTO);
            return Ok(type);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    [ProducesResponseType(typeof(void), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Update a meeting type")]
    public async Task<ActionResult> UpdateMeetingType(MeetingType type)
    {
        try
        {
            Domain.DTO.MeetingType typeDTO = _mapper.Map<Domain.DTO.MeetingType>(type);
            await _service.UpdateAsync(typeDTO);

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
    [SwaggerOperation("Delete a meeting type by ID")]
    public async Task<ActionResult> DeleteMeetingType(int id)
    {
        try
        {
            var type = await _service.GetByIdAsync(id);

            if (type == null)
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

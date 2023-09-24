using AutoMapper;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WeWell.Models;
using Microsoft.AspNetCore.Authorization;

namespace WeWell.Controllers;

[ApiController]
[Route("preferences")]
public class PreferenceController : ControllerBase
{
    private readonly PreferenceService _service;
    private readonly IMapper _mapper;

    public PreferenceController(PreferenceService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(int?), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Create a new preference")]
    public async Task<ActionResult<int?>> CreatePreference(Preference preference)
    {
        try
        {
            var preferenceDto = _mapper.Map<Domain.DataTransferObjects.Preference>(preference);
            var id = await _service.CreateAsync(preferenceDto);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Preference>), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Get all preferences")]
    public async Task<ActionResult<List<Preference>>> GetAllPreferences()
    {
        try
        {
            var preferenceDto = await _service.GetAllAsync();
            var preferences = _mapper.Map<List<Preference>>(preferenceDto);
            return Ok(preferences);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(Preference), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Get a preference by ID")]
    public async Task<ActionResult<Preference>> GetPreference(int id)
    {
        try
        {
            var preferenceDto = await _service.GetByIdAsync(id);

            if (preferenceDto == null)
            {
                return NotFound();
            }

            var preference = _mapper.Map<Preference>(preferenceDto);
            return Ok(preference);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    [Authorize]
    [ProducesResponseType(typeof(void), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Update a preference")]
    public async Task<ActionResult> UpdatePreference(Preference preference)
    {
        try
        {
            Domain.DataTransferObjects.Preference preferenceDto = _mapper.Map<Domain.DataTransferObjects.Preference>(preference);
            await _service.UpdateAsync(preferenceDto);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(void), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Delete a preference by ID")]
    public async Task<ActionResult> DeletePreference(int id)
    {
        try
        {
            var preference = await _service.GetByIdAsync(id);

            if (preference == null)
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

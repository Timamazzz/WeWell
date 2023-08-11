using AutoMapper;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WeWell.ViewModels;

namespace WeWell.Controllers;

[ApiController]
[Route("places")]
public class PlacesController : ControllerBase
{
    private readonly PlaceService _service;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public PlacesController(PlaceService service, IMapper mapper, IWebHostEnvironment webHostEnvironment)
    {
        _service = service;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpPost]
    [ProducesResponseType(typeof(int?), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Create a new place")]
    public async Task<ActionResult<int?>> CreatePlace([FromForm] Place place)
    {
        try
        {
            _service._webRootPath = _webHostEnvironment.WebRootPath;
            var placeDTO = _mapper.Map<Domain.DTO.Place>(place);
            var id = await _service.CreateAsync(placeDTO);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Place>), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Get all places")]
    public async Task<ActionResult<List<Place>>> GetAllPlaces()
    {
        try
        {
            var placesDTO = await _service.GetAllAsync();
            var places = _mapper.Map<List<Place>>(placesDTO);
            return Ok(places);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Place), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Get a place by ID")]
    public async Task<ActionResult<Place>> GetPlace(int id)
    {
        try
        {
            var placeDTO = await _service.GetByIdAsync(id);

            if (placeDTO == null)
            {
                return NotFound();
            }

            var place = _mapper.Map<Place>(placeDTO);
            return Ok(place);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    [ProducesResponseType(typeof(void), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Update a place")]
    public async Task<ActionResult> UpdatePlace([FromForm] Place place)
    {
        try
        {
            _service._webRootPath = _webHostEnvironment.WebRootPath;
            Domain.DTO.Place placeDto = _mapper.Map<Domain.DTO.Place>(place);
            await _service.UpdateAsync(placeDto);

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
    [SwaggerOperation("Delete a place by ID")]
    public async Task<ActionResult> DeletePlace(int id)
    {
        try
        {
            _service._webRootPath = _webHostEnvironment.WebRootPath;
            var place = await _service.GetByIdAsync(id);

            if (place == null)
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
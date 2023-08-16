using AutoMapper;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WeWell.Models;
using WeWell.Models.Places;
using WeWell.Services;

namespace WeWell.Controllers;

[ApiController]
[Route("places")]
public class PlacesController : ControllerBase
{
    private readonly PlaceService _service;
    private readonly IMapper _mapper;
    private readonly ImageService _imageService;

    public PlacesController(PlaceService service, ImageService imageService, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
        _imageService = imageService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(int?), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Create a new place")]
    public async Task<ActionResult<int?>> CreatePlace(PlaceCreate place)
    {
        try
        {
            var placesDto = _mapper.Map<Domain.DataTransferObjects.Place>(place);
            var id = await _service.CreateAsync(placesDto);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<PlaceGet>), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Get all places")]
    public async Task<ActionResult<List<PlaceGet>>> GetAllPlaces()
    {
        try
        {
            var placesDto = await _service.GetAllAsync();
            var places = _mapper.Map<List<PlaceGet>>(placesDto);
            return Ok(places);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PlaceGet), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Get a place by ID")]
    public async Task<ActionResult<PlaceGet>> GetPlace(int id)
    {
        try
        {
            var placesDto = await _service.GetByIdAsync(id);

            if (placesDto == null)
            {
                return NotFound();
            }

            var place = _mapper.Map<PlaceGet>(placesDto);
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
    public async Task<ActionResult> UpdatePlace(PlaceUpdate place)
    {
        try
        {
            Domain.DataTransferObjects.Place placeDto = _mapper.Map<Domain.DataTransferObjects.Place>(place);
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
    
    [HttpPut("images")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Update Image Place")]
    public async Task<ActionResult<PlaceGet>> UpdateImage([FromForm] Image image)
    {
        try
        {
            if (image == null || image.ImageFile == null || image.ImageFile.Length == 0)
            {
                return BadRequest("Invalid image file");
            }

            var placeDto = await _service.GetByIdAsync(image.ParentModelId);
            if (placeDto == null)
            {
                return NotFound("User not found");
            }
                
            string pathToUpload = Path.Combine("uploads", "images", "places", image.ParentModelId.ToString());
            string newAvatarPath = await _imageService.ReplaceImage(placeDto.ImagePath, image.ImageFile, pathToUpload);

            placeDto.ImagePath = newAvatarPath;
            await _service.UpdateAsync(placeDto);

            var placeGet = _mapper.Map<PlaceGet>(placeDto);
            return Ok(placeGet.Url);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
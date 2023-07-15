using AutoMapper;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WeWell.ViewModels;

namespace WeWell.Controllers
{
    [ApiController]
    [Route("places")]
    public class PlacesController : ControllerBase
    {
        private readonly PlaceService _service;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PlacesController(PlaceService service, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _service = service;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int?), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation("Create a new place")]
        public async Task<ActionResult<int?>> CreatePlace([FromForm] Place place)
        {
            try
            {
                if (place.Image != null)
                {
                    place.ImagePath = await SaveImage(place.Image);
                }

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
                if (place.Image != null)
                {
                    place.ImagePath = await SaveImage(place.Image);
                }

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
                var place = await _service.GetByIdAsync(id);

                if (place == null)
                {
                    return NotFound();
                }

                await _service.DeleteAsync(id);

                // Delete the associated image file if it exists
                if (!string.IsNullOrEmpty(place.ImagePath))
                {
                    DeleteImage(place.ImagePath);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var filePath = Path.Combine(_hostEnvironment.WebRootPath, "upload/Images/Places", fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return fileName;
        }

        private void DeleteImage(string imagePath)
        {
            var filePath = Path.Combine(_hostEnvironment.WebRootPath, "upload/Images/Places", imagePath);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
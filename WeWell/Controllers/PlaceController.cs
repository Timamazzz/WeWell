using AutoMapper;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Swashbuckle.AspNetCore.Annotations;
using WeWell.Models;
using WeWell.Models.Places;
using WeWell.Services;
using Microsoft.AspNetCore.Authorization;

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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
    
    [HttpPost("excel")]
    [Authorize]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [SwaggerOperation("Create Places with excel file")]
    public async Task<ActionResult> UploadExcelFile([FromForm] ExcelFile excelFile)
    {
        if (excelFile.File == null || excelFile.File.Length == 0)
        {
            return BadRequest("Invalid file");
        }

        var fileExtension = Path.GetExtension(excelFile.File.FileName);
        if (string.IsNullOrEmpty(fileExtension) || !fileExtension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("Invalid file format. Only .xlsx files are allowed.");
        }

        try
        {
            using var stream = excelFile.File.OpenReadStream();
            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();

            if (worksheet == null)
            {
                return BadRequest("The Excel file is empty or has no worksheets.");
            }

            var places = new List<PlaceCreate>();

            var columns = new
            {
                Name = 1,
                Description = 2,
                Address = 3,
                MinPrice = 4,
                MaxPrice = 5,
                MinDurationHours = 6,
                MaxDurationHours = 7,
                PreferencesId = 8,
                MeetingTypesId = 9
            };

            for (var row = 2; row <= worksheet.Dimension.Rows; row++)
            {
                if (!string.IsNullOrWhiteSpace(worksheet.Cells[row, columns.Name]?.Value?.ToString()))
                {
                    var preferenceIdsString = worksheet.Cells[row, columns.PreferencesId]?.Value?.ToString();
                    var meetingTypesIdString = worksheet.Cells[row, columns.MeetingTypesId]?.Value?.ToString();

                    var place = new PlaceCreate
                    {
                        Name = worksheet.Cells[row, columns.Name]?.Value?.ToString(),
                        Description = worksheet.Cells[row, columns.Description]?.Value?.ToString(),
                        Address = worksheet.Cells[row, columns.Address]?.Value?.ToString(),
                        MinPrice = int.TryParse(worksheet.Cells[row, columns.MinPrice]?.Value?.ToString(), out var minPrice) ? minPrice : null,
                        MaxPrice = int.TryParse(worksheet.Cells[row, columns.MaxPrice]?.Value?.ToString(), out var maxPrice) ? maxPrice : null,
                        MinDurationHours = int.TryParse(worksheet.Cells[row, columns.MinDurationHours]?.Value?.ToString(), out var minDuration) ? minDuration : null,
                        MaxDurationHours = int.TryParse(worksheet.Cells[row, columns.MaxDurationHours]?.Value?.ToString(), out var maxDuration) ? maxDuration : null,
                        Preferences = ConvertPreferenceIdsToObjects(ParseIds(preferenceIdsString)),
                        MeetingTypes = ConvertMeetingTypeIdsToObjects(ParseIds(meetingTypesIdString))
                    };

                    places.Add(place);
                }
            }

            var countId = await _service.CreateListAsync(_mapper.Map<List<Domain.DataTransferObjects.Place>>(places));
            return Ok(countId);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    private List<int> ParseIds(string? idsString)
    {
        if (!string.IsNullOrEmpty(idsString))
        {
            return idsString.Split(',').Select(idStr =>
            {
                if (int.TryParse(idStr, out var id))
                {
                    return id;
                }
                return 0;
            }).Where(id => id != 0).ToList();
        }
        return new List<int>();
    }
    
    private List<Preference> ConvertPreferenceIdsToObjects(List<int> preferenceIds)
    {
        var preferences = new List<Preference>();
        foreach (var preferenceId in preferenceIds)
        {
            var preference = new Preference { Id = preferenceId };
            preferences.Add(preference);
        }
        return preferences;
    }
    private List<MeetingType> ConvertMeetingTypeIdsToObjects(List<int> meetingTypeIds)
    {
        var meetingTypes = new List<MeetingType>();
        foreach (var meetingTypeId in meetingTypeIds)
        {
            var meetingType = new MeetingType { Id = meetingTypeId };
            meetingTypes.Add(meetingType);
        }
        return meetingTypes;
    }
}
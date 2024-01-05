using System.Net;
using GeoLocal.Interfaces;
using GeoLocal.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GeoLocal.Controllers;

[ApiController]
[Route("[controller]")]
public class GeolocationController : ControllerBase
{
    private readonly IGeoLocationService _geoLocationService;

    public GeolocationController(IGeoLocationService geoLocationService)
    {
        _geoLocationService = geoLocationService;
    }
    
    [HttpGet("get-current-geolocation")]
    public async Task<IActionResult> GetCurrentGeolocation()
    {
        var response = await _geoLocationService.GetCurrentGeoLocationFromIpStack();
        if (response.ResponseStatus == ServiceStatusCodes.StatusCode.Success)
        {
            return Ok(response.Data);
        }

        return StatusCode((int)HttpStatusCode.BadRequest, response.Message);
    }
}
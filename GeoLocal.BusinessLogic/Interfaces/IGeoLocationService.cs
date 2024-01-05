using GeoLocal.Models;
using GeoLocal.Responses;

namespace GeoLocal.Interfaces
{
    public interface IGeoLocationService
    {
        Task<ServiceResponse<GeoLocationDto>> GetCurrentGeoLocationFromIpStack();
    }
}
using GeoLocal.Models;
using Refit;

namespace GeoLocal.Interfaces;

public interface IIpStackApiClient
{
    [Get("/check?access_key={accessToken}")]
    Task<ApiResponse<GeoLocationDto>> GetRequesterIpLookup(string accessToken);
}
using System.Net;
using GeoLocal.Configurations;
using GeoLocal.DataAccessLayer.Entities;
using GeoLocal.DataAccessLayer.Repository;
using GeoLocal.Interfaces;
using GeoLocal.Models;
using GeoLocal.Responses;
using Microsoft.Extensions.Options;

namespace GeoLocal.Services;

public class GeoLocationService : IGeoLocationService
{
    private readonly IIpStackUrlBuilder _ipStackUrlBuilder;
    private readonly IGeoLocalDbRepository _geoLocalDbRepository;
    private readonly IIpStackApiClient _ipStackApiClient;
    private readonly string _accessToken;
    
    public GeoLocationService(IIpStackUrlBuilder ipStackUrlBuilder, IGeoLocalDbRepository geoLocalDbRepository, IIpStackApiClient ipStackApiClient, IOptions<IpStackConfig> config)
    {
        _ipStackUrlBuilder = ipStackUrlBuilder;
        _geoLocalDbRepository = geoLocalDbRepository;
        _ipStackApiClient = ipStackApiClient;
        _accessToken = config.Value.AccessToken;
    }
    
    #region Public Methods

    public async Task<ServiceResponse<GeoLocationDto>> GetCurrentGeoLocationFromIpStack()
    {
        var response = _ipStackApiClient.GetRequesterIpLookup(_accessToken);
        return new ServiceResponse<GeoLocationDto>(response.Result.Content);
    }

    #endregion
}
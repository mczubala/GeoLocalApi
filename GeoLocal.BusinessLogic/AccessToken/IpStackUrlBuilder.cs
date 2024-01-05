using System.Runtime.Caching;
using GeoLocal.Configurations;
using Microsoft.Extensions.Options;

public class IpStackUrlBuilder : IIpStackUrlBuilder
{
    private readonly string _baseUrl;
    private readonly string _accessToken;
    private readonly string _outputFormat;
    private readonly string _language;    private MemoryCache _cache = new MemoryCache("AccessTokenCache");
    
    public IpStackUrlBuilder(IOptions<IpStackConfig> config)
    { 
        _baseUrl = config.Value.BaseUrl;
        _accessToken = config.Value.AccessToken;
        _language = config.Value.Language;
        _outputFormat = config.Value.OutputFormat;
    }
    
    public string GetUrlForIpStack()
    {
        var url = $"{_baseUrl}{{0}}?access_key={_accessToken}&output={_outputFormat}&language={_language}";
        return url;
    }
    
    private void SaveAccessTokenToCache(string accessToken, DateTime expirationTime)
    {
        CacheItemPolicy policy = new CacheItemPolicy
        {
            AbsoluteExpiration = expirationTime
        };
        _cache.Set("AccessToken", accessToken, policy);
    }

    private string GetAccessTokenFromCache()
    {
        return _cache.Get("AccessToken") as string;
    }
    
}
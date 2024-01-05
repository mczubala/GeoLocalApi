using Newtonsoft.Json;
using System.Collections.Generic;

namespace GeoLocal.Models;

public class GeoLocationDto
{
    [JsonProperty("ip")]
    public string Ip { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("continent_code")]
    public string ContinentCode { get; set; }

    [JsonProperty("continent_name")]
    public string ContinentName { get; set; }

    [JsonProperty("country_code")]
    public string CountryCode { get; set; }

    [JsonProperty("country_name")]
    public string CountryName { get; set; }

    [JsonProperty("region_code")]
    public string RegionCode { get; set; }

    [JsonProperty("region_name")]
    public string RegionName { get; set; }

    [JsonProperty("city")]
    public string City { get; set; }

    [JsonProperty("zip")]
    public string Zip { get; set; }

    [JsonProperty("latitude")]
    public double Latitude { get; set; }

    [JsonProperty("longitude")]
    public double Longitude { get; set; }

    [JsonProperty("location")]
    public LocationDto Location { get; set; }

    [JsonProperty("time_zone")]
    public TimeZoneDto TimeZone { get; set; }

    [JsonProperty("currency")]
    public CurrencyDto Currency { get; set; }

    [JsonProperty("connection")]
    public ConnectionDto Connection { get; set; }
}

public class LocationDto
{
    [JsonProperty("geoname_id")]
    public long GeonameId { get; set; }

    [JsonProperty("capital")]
    public string Capital { get; set; }

    [JsonProperty("languages")]
    public List<LanguageDto> Languages { get; set; }

    [JsonProperty("country_flag")]
    public string CountryFlag { get; set; }

    [JsonProperty("country_flag_emoji")]
    public string CountryFlagEmoji { get; set; }

    [JsonProperty("country_flag_emoji_unicode")]
    public string CountryFlagEmojiUnicode { get; set; }

    [JsonProperty("calling_code")]
    public string CallingCode { get; set; }

    [JsonProperty("is_eu")]
    public bool IsEu { get; set; }
}

public class LanguageDto
{
    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("native")]
    public string Native { get; set; }
}

public class TimeZoneDto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("current_time")]
    public string CurrentTime { get; set; }

    [JsonProperty("gmt_offset")]
    public int GmtOffset { get; set; }

    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("is_daylight_saving")]
    public bool IsDaylightSaving { get; set; }
}

public class CurrencyDto
{
    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("plural")]
    public string Plural { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("symbol_native")]
    public string SymbolNative { get; set; }
}

public class ConnectionDto
{
    [JsonProperty("asn")]
    public int Asn { get; set; }

    [JsonProperty("isp")]
    public string Isp { get; set; }
}

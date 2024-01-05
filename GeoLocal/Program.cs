using Azure.Identity;
using FluentValidation.AspNetCore;
using GeoLocal.Configurations;
using GeoLocal.DataAccessLayer;
using GeoLocal.DataAccessLayer.Repository;
using GeoLocal.Interfaces;
using GeoLocal.Services;
using Microsoft.Extensions.Options;
using Refit;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add the HttpClient service
builder.Services.AddHttpClient();
// Add services to the container.

var settings = new RefitSettings();
settings.ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
{
    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
});

builder.Services.AddRefitClient<IIpStackApiClient>(settings)
    .ConfigureHttpClient((sp, c) =>
    {
        var options = sp.GetRequiredService<IOptions<IpStackConfig>>().Value;
        c.BaseAddress = new Uri(options.BaseUrl);
    });

builder.Services.Configure<IpStackConfig>(builder.Configuration.GetSection("IpStackConfig"));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Lax; 
    options.HttpOnly = HttpOnlyPolicy.Always;
    options.Secure = CookieSecurePolicy.SameAsRequest;
});

builder.Services.AddScoped<IGeoLocalDbRepository, GeoLocalDbRepository>();
// builder.Services.AddScoped<IIpStackUrlBuilder>(sp =>
// {
//     var options = sp.GetRequiredService<IOptions<AllegroApiSettings>>().Value;
//     var httpClient = sp.GetRequiredService<HttpClient>();
//     return new AccessTokenProvider(httpClient, options.ClientId, options.ClientSecret, options.TokenUrl, options.AuthorizationEndpoint, sp.GetRequiredService<IGeoLocalDbRepository>());
// });
builder.Services.AddSingleton<IIpStackUrlBuilder, IpStackUrlBuilder>();
builder.Services.AddTransient<IGeoLocationService, GeoLocationService>();

builder.Services.AddDbContext<MfcDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));

builder.Services.AddControllers();

// builder.Services.AddFluentValidationAutoValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();
app.UseCookiePolicy();
app.UseAuthentication();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
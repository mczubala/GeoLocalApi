using Microsoft.AspNetCore.Mvc;

namespace GeoLocal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IIpStackUrlBuilder _ipStackUrlBuilder;

        public TestController(IHttpClientFactory clientFactory, IIpStackUrlBuilder ipStackUrlBuilder, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _ipStackUrlBuilder = ipStackUrlBuilder;
        }
        
        #region Action Methods
        
        [HttpGet("check-ipstack-access")]
        public async Task<IActionResult> CheckAccess()
        {
            var requestUrl = string.Format(_ipStackUrlBuilder.GetUrlForIpStack(), "check");

            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            var response = await SendIpStackApiRequest(request);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            return BadRequest();
        }

        private async Task<HttpResponseMessage> SendIpStackApiRequest(HttpRequestMessage request)
        {
            var client = _clientFactory.CreateClient();
            return await client.SendAsync(request);
        }
        #endregion
    }
}
using System.Globalization;
using System.Text;
using Exceptions;
using Newtonsoft.Json;
using Xunit;

namespace WebApi.Tests.V1;

public class ControllerBase : IClassFixture<WebAppFactory<Program>>
{
    private readonly HttpClient _client;

    protected ControllerBase(WebAppFactory<Program> factory)
    {
        _client = factory.CreateClient();
        //ErrorMessages.Culture = CultureInfo.CurrentCulture;
    }

    protected async Task<HttpResponseMessage> PostRequest(string method, object body)
    {
        var jsonString = JsonConvert.SerializeObject(body);

        return await _client.PostAsync(method, new StringContent(jsonString, Encoding.UTF8, "application/json"));
    }
}
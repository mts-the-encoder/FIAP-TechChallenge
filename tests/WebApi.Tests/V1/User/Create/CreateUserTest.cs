using System.Net;
using System.Text.Json;
using Communication.Requests;
using Exceptions;
using FluentAssertions;
using Microsoft.Identity.Client;
using Utils.Requests;
using Xunit;

namespace WebApi.Tests.V1.User.Create;

public class CreateUserTest : ControllerBase
{
    private const string METHOD = "user";

    public CreateUserTest(WebAppFactory<Program> factory) : base (factory)
    {
    }

    [Fact]
    public async Task Validate_Success()
    {
        var request = UserRequestBuilder.Build();

        var response = await PostRequest(METHOD, request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Validate_Error_Empty_Name()
    {
        var request = UserRequestBuilder.Build();
        request.Name = string.Empty;

        var response = await PostRequest(METHOD,request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("messages").EnumerateArray();
        errors.Should().ContainSingle().And.Contain(x => x.GetString().Equals(ErrorMessages.NOME_EM_BRANCO));
    }

    [Fact]
    public async Task Validate_Error_Empty_Email()
    {
        var request = UserRequestBuilder.Build();
        request.Email = string.Empty;

        var response = await PostRequest(METHOD,request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("messages").EnumerateArray();
        errors.Should().ContainSingle().And.Contain(x => x.GetString().Equals(ErrorMessages.EMAIL_EM_BRANCO));
    }

    [Fact]
    public async Task Validate_Error_Invalid_Cnpj()
    {
        var request = UserRequestBuilder.Build();
        request.CNPJ = "48343971/0001-60";

        var response = await PostRequest(METHOD,request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("messages").EnumerateArray();
        errors.Should().ContainSingle().And.Contain(x => x.GetString().Equals(ErrorMessages.CNPJ_INVALIDO));
    }

    [Fact]
    public async Task Validate_Error_Invalid_Phone_Number()
    {
        var request = UserRequestBuilder.Build();
        request.Phone = "11 9 40028922";

        var response = await PostRequest(METHOD,request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("messages").EnumerateArray();
        errors.Should().ContainSingle().And.Contain(x => x.GetString().Equals(ErrorMessages.TELEFONE_INVALIDO));
    }
}
using System.Net;
using System.Text.Json;
using Exceptions;
using FluentAssertions;
using Utils.Requests;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApi.Tests.V1.VariableIncome.Create;

public class CreateVariableIncomeTest : ControllerBase
{
    private const string METHOD = "variableincome";
    private Domain.Entities.User _user;
    private readonly string _password;

    public CreateVariableIncomeTest(WebAppFactory<Program> factory) : base(factory)
    {
        _user = factory.GetUser();
        _password = factory.GetPassword();
    }

    [Fact]
    public async Task Validate_Success()
    {
        var token = await Login(_user.Email, _password);

        var request = VariableIncomeRequestBuilder.Build();

        var response = await PostRequest(METHOD, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("sender").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Validate_Error_Sender_Empty()
    {
        var token = await Login(_user.Email, _password);

        var request = VariableIncomeRequestBuilder.Build();
        request.Sender = string.Empty;

        var response = await PostRequest(METHOD, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("messages").EnumerateArray();

        var expectedMessage = ErrorMessages.ResourceManager.GetString("EMISSOR_NAO_PODE_SER_VAZIO");
        errors.Should().ContainSingle().And.Contain(x => x.GetString().Equals(expectedMessage));
    }
}
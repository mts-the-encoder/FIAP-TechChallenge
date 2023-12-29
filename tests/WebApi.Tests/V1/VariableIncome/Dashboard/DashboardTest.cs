using Communication.Requests;
using FluentAssertions;
using System.Net;
using Xunit;

namespace WebApi.Tests.V1.VariableIncome.Dashboard;

public class DashboardTest : ControllerBase
{
    private const string METHOD = "dashboard";

    private readonly Domain.Entities.User _user;
    private readonly string _password;

    public DashboardTest(WebAppFactory<Program> factory) : base(factory)
    {
        _user = factory.GetUser();
        _password = factory.GetPassword();
    }

    [Fact]
    public async Task Validate_No_Investment()
    {
        var token = await Login(_user.Email, _password);

        var response = await PutRequest(METHOD,new VariableDashboardRequest(), token);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
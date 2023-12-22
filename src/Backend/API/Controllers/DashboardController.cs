using Api.Controllers;
using Api.Filters;
using Application.UseCases.DashboardVariable;
using Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class DashboardController : TechChallengeController
{
    [HttpPut]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<IActionResult> Create([FromServices] IDashboardVariableUseCase useCase, [FromBody] VariableDashboardRequest request)
    {
        var response = await useCase.Execute(request);

        return response.Investments.Any() ? Ok(response) : NoContent();
    }
}
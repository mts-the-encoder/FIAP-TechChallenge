using Api.Filters;
using Application.UseCases.VariableIncome.Create;
using Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class VariableIncomeController : TechChallengeController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromServices] ICreateVariableIncomeUseCase useCase, [FromBody] VariableIncomeRequest request)
    {
         var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
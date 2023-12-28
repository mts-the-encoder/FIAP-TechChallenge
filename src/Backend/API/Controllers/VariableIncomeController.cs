using Api.Filters;
using Application.UseCases.Update;
using Application.UseCases.VariableIncome.Create;
using Application.UseCases.VariableIncome.GetById;
using Communication.Requests;
using Microsoft.AspNetCore.Mvc;
using HashidsModelBinder = Api.Binder.HashidsModelBinder;

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

    [HttpGet("{id:hashids}")]
    public async Task<IActionResult> GetById([FromServices] IGetByIdUseCase useCase, 
        [FromRoute] [ModelBinder(typeof(HashidsModelBinder))] long id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpPut("{id:hashids}")]
    public async Task<IActionResult> Update([FromServices] IUpdateVariableIncomeUseCase useCase,
        [FromBody] VariableIncomeRequest request,
        [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id)
    {
        var response = await useCase.Execute(id, request);

        return Ok(response);
    }
}
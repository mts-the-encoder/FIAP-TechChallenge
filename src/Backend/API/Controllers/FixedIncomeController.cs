using Api.Filters;
using Application.UseCases.FixedIncome.Create;
using Application.UseCases.FixedIncome.Delete;
using Application.UseCases.FixedIncome.GetAll;
using Application.UseCases.FixedIncome.GetById;
using Application.UseCases.FixedIncome.GetCDB;
using Application.UseCases.FixedIncome.GetTesouroDireto;
using Application.UseCases.FixedIncome.Update;
using Communication.Requests;
using Microsoft.AspNetCore.Mvc;
using HashidsModelBinder = Api.Binder.HashidsModelBinder;

namespace Api.Controllers;

[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class FixedIncomeController : TechChallengeController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromServices] ICreateFixedIncomeUseCase useCase, [FromBody] FixedIncomeRequest request)
    {
         var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet("{id:hashids}")]
    public async Task<IActionResult> GetById([FromServices] IGetByIdFixedIncomeUseCase useCase, 
        [FromRoute] [ModelBinder(typeof(HashidsModelBinder))] long id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpGet("CDB")]
    public async Task<IActionResult> GetDCB([FromServices] IGetCDBUseCase useCase)
    {
        var response = await useCase.Execute();

        return Ok(response);
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAll([FromServices] IGetAllFixedIncomeUseCase useCase)
    {
        var response = await useCase.Execute();

        return Ok(response);
    }

    [HttpGet("TesouroDireto")]
    public async Task<IActionResult> GetTesouroDireto([FromServices] IGetTesouroDiretoUseCase useCase)
    {
        var response = await useCase.Execute();

        return Ok(response);
    }

    [HttpPut("{id:hashids}")]
    public async Task<IActionResult> Update([FromServices] IUpdateFixedIncomeUseCase useCase,
        [FromBody] FixedIncomeRequest request,
        [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id)
    {
        var response = await useCase.Execute(id, request);

        return Ok(response);
    }

    [HttpDelete("{id:hashids}")]
    public async Task<IActionResult> Delete([FromServices] IDeleteFixedIncomeUseCase useCase,
        [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id)
    {
        await useCase.Execute(id);

        return Ok();
    }
}
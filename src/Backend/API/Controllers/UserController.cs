using Api.Controllers;
using Application.UseCases.User.Create;
using Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserController : TechChallengeController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromServices] ICreateUseCase useCase, [FromBody] UserRequest request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
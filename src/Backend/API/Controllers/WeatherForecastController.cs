using Application.UseCases.User.Create;
using Communication.Requests;
using Exceptions;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ICreateUseCase _useCase;
    public WeatherForecastController(ICreateUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromServices] ICreateUseCase useCase)
    {
        await useCase.Execute(new UserRequest()
        {
            Email = "mts@email.com",
            Name = "mts",
            Phone = "1 9 1234-5678",
            CNPJ = "77.999.548/0001-11",
            Password = "1234567",
        });

        return Ok();
    }
}



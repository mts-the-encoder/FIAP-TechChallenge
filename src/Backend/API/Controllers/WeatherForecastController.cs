using Application.UseCases.User.Create;
using Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Get([FromServices] ICreateUseCase useCase)
    {
        var response = await useCase.Execute(new UserRequest()
        {
            Email = "mtsss@email.com",
            Name = "mts",
            Phone = "11 9 1234-5678",
            CNPJ = "77.999.548/0001-11",
            Password = "1234567",
        });

        return Ok(response);
    }
}
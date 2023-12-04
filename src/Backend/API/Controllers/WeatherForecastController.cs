using Exceptions;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using System;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var message = ErrorMessages.EMAIL_EM_BRANCO;
        Log.ForContext("UserName","mts")
            .ForContext("UserId",1)
            .Error($"{message}");

        return Ok();
    }
}



using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CurrentTemperatureController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;

    public CurrentTemperatureController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetCurrentTemperature")]
    public double Get()
    {
        var monthNumber = DateTime.Today.Month;
        var avgTemp = 5;
        if (monthNumber < 4)
            avgTemp = 5;
        else if (monthNumber >= 4 && monthNumber < 7)
            avgTemp = 12;
        else if (monthNumber >= 7 && monthNumber < 10)   
            avgTemp = 21;
        else if (monthNumber >= 10 && monthNumber < 13)  
            avgTemp = 13;


        return Math.Round(Random.Shared.NextDouble() * 2, 2) + avgTemp;

    }
}

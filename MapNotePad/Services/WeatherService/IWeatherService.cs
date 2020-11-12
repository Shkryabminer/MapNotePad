using MapNotePad.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotePad.Services.WeatherService
{
  public interface IWeatherService
    {
        Task<WeatherModel> GetWeatherData(double latitude, double longitude);

    }
}

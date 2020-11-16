using MapNotePad.Models;
using System.Threading.Tasks;

namespace MapNotePad.Services.WeatherService
{
    public interface IWeatherService
    {
        Task<WeatherModel> GetWeatherData(double latitude, double longitude);
    }
}

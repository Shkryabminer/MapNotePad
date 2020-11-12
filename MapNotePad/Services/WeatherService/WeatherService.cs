using MapNotePad.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MapNotePad.Services.WeatherService
{
    public class WeatherService : IWeatherService
    {
        public async Task<WeatherModel> GetWeatherData(double latitude, double longitude)
        {
            HttpClient client = new HttpClient();
            string path = GetPreparedParth(latitude,longitude);
            var response = await client.GetStringAsync(new Uri(path));

            var weather = JsonConvert.DeserializeObject<WeatherModel>(response);

            return weather;
        }

        #region --Private helpers

       
        private string GetPreparedParth(double latitude, double longitude)
        {
            return Constants.WeatherClient.Path.Replace("{lat}", latitude.ToString()).
                                                Replace("{lon}", longitude.ToString()).
                                                Replace("{API key}", Constants.WeatherClient.Key);
        }

        #endregion

    }
}

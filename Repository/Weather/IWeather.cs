using FPTCodeChallenge.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FPTCodeChallenge.Repository.Weather
{
    public interface IWeather
    {
        Task<Base> GetWeatherCondition(int zipCode);
        string IsRaining(int weatherCode);
        string IsNeedtoWearSunCream(int UVIndex);
        string IsCanFlyKite(int windSpeed, string IsRaining);
    }
}

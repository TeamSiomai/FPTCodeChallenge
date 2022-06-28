using FPTCodeChallenge.Model;
using FPTCodeChallenge.Repository.Weather;
using System;
using System.Threading.Tasks;

namespace FPTCodeChallenge
{
    class Program
    {
        static async Task Main(string[] args)
        {
            MainCode().Wait();
        }
        static int GetZipCode()
        {
            int zipCode = 0;
            bool valid = TryGetZipCode("Please enter your zip code: ", out zipCode);

            while (!valid)
            {
                valid = TryGetZipCode("Please enter your zip code: ", out zipCode);
            }

            return zipCode;
        }
        static bool TryGetZipCode(string message, out int zipCode)
        {
            Console.Write(message);
            return int.TryParse(Console.ReadLine(), out zipCode);
               
        }
        static async Task MainCode()
        {
            int zipcode = GetZipCode();
            Base result = new Base();
            Weather weather = new Weather();

            result = await weather.GetWeatherCondition(zipcode);
            try
            {
                if (result.current != null)
                {
                    string IsRainingResult = weather.IsRaining(result.current.weather_code);
                    Console.WriteLine("Should I go outside? {0}", IsRainingResult);
                    string UVIndexResult = weather.IsNeedtoWearSunCream(result.current.uv_index);
                    Console.WriteLine("Should I wear sunscreen? {0}", UVIndexResult);
                    string flyMyKitResult = weather.IsCanFlyKite(result.current.wind_speed, IsRainingResult);
                    Console.WriteLine("Can I fly my kite? {0}", flyMyKitResult);
                    RunAgain();
                }
                else
                {
                    Console.WriteLine("\nResult of ZipCode is Empty!");
                    RunAgain();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

        }
        static void RunAgain()
        {
            Console.Write("\nDo you want to input again? (yes/no): ");

            string input = Console.ReadLine();
            do
            {
                if (string.Equals(input, "y", StringComparison.OrdinalIgnoreCase) || string.Equals(input, "yes", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Clear();
                    MainCode().Wait();
                    break;
                }
                else if (string.Equals(input, "n", StringComparison.OrdinalIgnoreCase) || string.Equals(input, "no", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

            } while ((string.Equals(input, "n", StringComparison.OrdinalIgnoreCase) || string.Equals(input, "no", StringComparison.OrdinalIgnoreCase)));

        }

    }
}

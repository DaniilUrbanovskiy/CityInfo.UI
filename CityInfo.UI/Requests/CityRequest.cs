using System.Net.Http;

namespace CityInfo.UI.Requests
{
    public static class CityRequest
    {
        public static string GetCities(string countryName = null)
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync($"https://cityinfo-api.azurewebsites.net/city/select/{countryName}");
            return response.Result.Content.ReadAsStringAsync().Result;
        }
    }
}

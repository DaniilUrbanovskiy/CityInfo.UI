using System.Net.Http;

namespace CityInfo.UI.Requests
{
    public static class CountryRequest
    {
        public static string GetCountries()
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync("https://cityinfo-api.azurewebsites.net/country");
            return response.Result.Content.ReadAsStringAsync().Result;
        }    
    }
}

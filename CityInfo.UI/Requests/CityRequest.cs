using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

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

        public static string GetFavourites(string strToken)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);

            var response = client.GetAsync("https://cityinfo-api.azurewebsites.net/user/favourites/get");
            return response.Result.Content.ReadAsStringAsync().Result;
        }

        public static HttpResponseMessage SetFavourites(string strToken, string cityName)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);

            var response = client.PostAsync($"https://cityinfo-api.azurewebsites.net/user/favourites/set/{cityName}", null);
            return response.Result;
        }

        public static HttpResponseMessage RemoveFavourites(string strToken, string cityName)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);

            var response = client.DeleteAsync($"https://cityinfo-api.azurewebsites.net/user/favourites/remove/{cityName}");
            return response.Result;
        }
    }
}

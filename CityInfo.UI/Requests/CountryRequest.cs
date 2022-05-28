using CityInfo.UI.Models;
using RestSharp;
using RestSharp.Authenticators;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

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

        public static RestResponse AddCountries(InputFileModel file, string strToken)
        {
            var client = new RestClient($"https://cityinfo-api.azurewebsites.net/admin/country/{file.Name}");
            client.Authenticator = new JwtAuthenticator(strToken);
            var request = new RestRequest(string.Empty, Method.Post);

            byte[] data;
            using (var br = new BinaryReader(file.Image.OpenReadStream()))
            {
                data = br.ReadBytes((int)file.Image.OpenReadStream().Length);
            }

            request.AddFile("flag", data, file.Name + ".jpg");
            return client.ExecuteAsync(request).Result;
        }

        public static HttpStatusCode RemoveCountries(string countryName, string strToken)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);

            var response = client.DeleteAsync($"https://cityinfo-api.azurewebsites.net/admin/country/{countryName}");
            return response.Result.StatusCode;
        }
    }
}

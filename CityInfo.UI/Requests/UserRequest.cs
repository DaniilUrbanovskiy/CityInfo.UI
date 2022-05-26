using CityInfo.UI.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace CityInfo.UI.Requests
{
    public static class UserRequest
    {
        public static HttpResponseMessage AccessUser(User user, string accessType)
        {
            HttpClient client = new HttpClient();
            string json = JsonConvert.SerializeObject(user);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = client.PostAsync($"https://cityinfo-api.azurewebsites.net/user/{accessType}", httpContent);
            return response.Result;          
        }
    }
}

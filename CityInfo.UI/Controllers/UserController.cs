using CityInfo.UI.Models;
using CityInfo.UI.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CityInfo.UI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Registr(User user)
        {
            if (user.Login == null)
            {
                return View("Registr");
            }
            string accessType = "registr";
            var response = UserRequest.AccessUser(user, accessType);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.exMessage = response.Content.ReadAsStringAsync().Result;
                return View("Registr");
            }
            ViewBag.exMessage = string.Empty;

            return View("Login");                     
        }
      
        public IActionResult Login(User user)
        {
            if (user.Login == null)
            {
                return View("Login");
            }
            string accessType = "login";
            var response = UserRequest.AccessUser(user, accessType);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.exMessage = response.Content.ReadAsStringAsync().Result;
                return View("Login");
            }
            ViewBag.exMessage = string.Empty;
            
            HttpContext.Session.SetString("token", response.Content.ReadAsStringAsync().Result);

            return View("LoginSuccess");                     
        }
    }
}



//var strToken = HttpContext.Session.GetString("token");

//HttpClient client = new HttpClient();
//client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
//var response2 = client.GetAsync("https://cityinfo-api.azurewebsites.net/user/favourites/get");
//var a = response2.Result.Content.ReadAsStringAsync().Result;

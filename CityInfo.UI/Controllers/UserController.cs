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
            var response = UserRequest.AccessUser(user, "registr");
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
            var response = UserRequest.AccessUser(user, "login");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                ViewBag.exMessage = response.Content.ReadAsStringAsync().Result;
                return View("Login");
            }
            ViewBag.exMessage = string.Empty;
            
            HttpContext.Session.SetString("token", response.Content.ReadAsStringAsync().Result);

            return RedirectToAction("Countries", "Home");                     
        }
    }
}





using CityInfo.UI.Models;
using CityInfo.UI.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace CityInfo.UI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Countries()
        {
            var countries = JsonSerializer.Deserialize<List<Country>>(CountryRequest.GetCountries());
            var cities = JsonSerializer.Deserialize<List<City>>(CityRequest.GetCities());
            countries = countries.OrderByDescending(x => cities.Where(y => y.countryId == x.id).Count()).ToList();

            return View(countries);
        }

        [HttpGet]
        public IActionResult Cities(string countryName = null)
        {
            var result = string.Empty;
            if (countryName != null)
            {
                result = CityRequest.GetCities(countryName);
            }
            else
            {
                 result = CityRequest.GetCities();
            }           
            var cities = JsonSerializer.Deserialize<List<City>>(result);

            return View(cities);
        }

        [HttpGet]
        public IActionResult GetFavourites(string responseMessage = null)
        {
            var strToken = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(strToken)) 
            {
                return View("Login");
            }

            var result = CityRequest.GetFavourites(strToken);

            var cities = JsonSerializer.Deserialize<List<City>>(result);
            ViewBag.responseMessage = responseMessage;
            return View("Cities", cities);
        }

        [HttpGet]
        public IActionResult SetFavourites(string cityName)
        {
            var strToken = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(strToken))
            {
                return View("Login");
            }
            var response = CityRequest.Favourites(strToken, cityName, "set");
            ViewBag.responseMessage = response.Content.ReadAsStringAsync().Result;
            string responseMessage = ViewBag.responseMessage;
            ViewBag.responseMessage = string.Empty;

            return RedirectToAction("GetFavourites", "Home", new { responseMessage });
        }

        [HttpGet]
        public IActionResult RemoveFavourites(string cityName)
        {
            var strToken = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(strToken))
            {
                return View("Login");
            }
            var response = CityRequest.Favourites(strToken, cityName, "remove");
            ViewBag.responseMessage = response.Content.ReadAsStringAsync().Result;
            string responseMessage = ViewBag.responseMessage;
            ViewBag.responseMessage = string.Empty;

            return RedirectToAction("GetFavourites", "Home", new { responseMessage });
        }
    }
}

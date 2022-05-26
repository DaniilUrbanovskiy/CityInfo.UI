using CityInfo.UI.Models;
using CityInfo.UI.Requests;
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
    }
}

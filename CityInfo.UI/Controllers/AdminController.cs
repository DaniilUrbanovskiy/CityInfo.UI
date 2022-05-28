using CityInfo.UI.Models;
using CityInfo.UI.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CityInfo.UI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Page()
        {
            var strToken = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(strToken))
            {
                return View("Login");
            }
            return View("AdminPage", new InputFileModel());
        }

        public IActionResult AddCountry(InputFileModel file)
        {
            var strToken = HttpContext.Session.GetString("token");

            var response = CountryRequest.AddCountries(file, strToken);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                file.ResponseMessage = $"{file.Name} added";
                return View("AdminPage", file);
            }
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                file.ResponseMessage = "You have no permissions";
                return View("AdminPage", file);
            }
            file.ResponseMessage = "Try aggain";
            return View("AdminPage", file);
        }

        public IActionResult RemoveCountry(InputFileModel file)
        {
            var strToken = HttpContext.Session.GetString("token");
            var countryName = file.Name;

            var response = CountryRequest.RemoveCountries(countryName, strToken);
            if (response == HttpStatusCode.OK)
            {
                file.ResponseMessage = $"{file.Name} removed";
                return View("AdminPage", file);
            }
            if (response == HttpStatusCode.Forbidden)
            {
                file.ResponseMessage = "You have no permissions";
                return View("AdminPage", file);
            }
            file.ResponseMessage = "Try aggain";
            return View("AdminPage", file);
        }

        public IActionResult AddCity(InputFileModel file)
        {
            var strToken = HttpContext.Session.GetString("token");

            var response = CityRequest.AddCities(file, strToken);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                file.ResponseMessage = $"{file.Name} added";
                return View("AdminPage", file);
            }
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                file.ResponseMessage = "You have no permissions";
                return View("AdminPage", file);
            }
            file.ResponseMessage = "Try aggain";
            return View("AdminPage", file);
        }

        public IActionResult RemoveCity(InputFileModel file)
        {
            var strToken = HttpContext.Session.GetString("token");
            var cityName = file.Name;

            var response = CityRequest.RemoveCities(cityName, strToken);
            if (response == HttpStatusCode.OK)
            {
                file.ResponseMessage = $"{file.Name} removed";
                return View("AdminPage", file);
            }
            if (response == HttpStatusCode.Forbidden)
            {
                file.ResponseMessage = "You have no permissions";
                return View("AdminPage", file);
            }
            file.ResponseMessage = "Try aggain";
            return View("AdminPage", file);
        }
    }
}
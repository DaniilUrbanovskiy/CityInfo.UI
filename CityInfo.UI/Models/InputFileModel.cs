using Microsoft.AspNetCore.Http;

namespace CityInfo.UI.Models
{
    public class InputFileModel
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string CountryName { get; set; }
        public string Info { get; set; }
        public string ResponseMessage { get; set; }
    }
}

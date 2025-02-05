using System.ComponentModel.DataAnnotations;

namespace ShopTARgv23.Models.AccuWeathers
{
    public class AccuWeatherSearchViewModel
    {
        [Required(ErrorMessage = "You must enter a city name"!)]
        [RegularExpression("[A-Za-z]+$", ErrorMessage = "Only text allowed")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Entry a city name greater than 2 and lesser than 20")]
        [Display(Name = "City Name")]
        public string CityName { get; set; }
    }
}

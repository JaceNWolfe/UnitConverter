using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UnitOf;
namespace UnitConverter.Pages;

public class ConversionsModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string Input { get; set; } = string.Empty;

    public string Output { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string ConversionType { get; set; } = string.Empty ;


    public void OnGet()
    {
        ViewData["ConversionType"] = ConversionType;
        ViewData["Title"] = "Conversions - " + ConversionType;

        double value;
        //catch if not a number
        try
        {
            value = Convert.ToDouble(Input);
        }
        catch (FormatException)
        {
            ViewData["ErrorMessage"] = "Input must be a valid number.";
            return;
        }


        // switch expression on conversionType

        switch (ConversionType.ToLower())
        {
            case "milestokilometers":
                Output = new UnitOf.Length().FromMiles(value).ToKilometers().ToString();
                    break;
            case "kilometerstomiles":
                Output = new UnitOf.Length().FromKilometers(value).ToMiles().ToString();
                    break;
            case "fahrenheittocelsius":
                Output = new UnitOf.Temperature().FromFahrenheit(value).ToCelsius().ToString();
                    break;
            case "celsiustofahrenheit":
                Output = new UnitOf.Temperature().FromCelsius(value).ToFahrenheit().ToString();
                    break;
            case "poundstokilograms":
                Output = new UnitOf.Mass().FromPounds(value).ToKilograms().ToString();
                    break;
            case "kilogramstopounds":
                Output = new UnitOf.Mass().FromKilograms(value).ToPounds().ToString();
                    break;
            case "ksitopsi":
                Output = new UnitOf.Pressure().FromKSI(value).ToPSI().ToString();
                break;
            case "psitoksi":
                Output = new UnitOf.Pressure().FromPSI(value).ToKSI().ToString();
                break;
            case "sterestotuns":
                Output = new UnitOf.Volume() .FromSteres(value) .ToTuns().ToString();
                break;
            case "tunstosteres":
                Output = new UnitOf.Volume() .FromTuns(value) .ToSteres().ToString();
                break;
            default:
                ViewData["ErrorMessage"] = "Unknown conversion type. Please check spelling";
                break;
        }
    }
}

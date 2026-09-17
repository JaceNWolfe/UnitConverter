using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UnitConverter.Models;
using UnitOf;
namespace UnitConverter.Pages;

public class ConversionsModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public ConversionModel Conversion { get; set; } = new()
    {
        ConversionType = "Miles to Kilometers",
        Input = "3.1415"
    };

    [BindProperty(SupportsGet = true)]
    public string ConversionType
    {
        get => Conversion.ConversionType;
        set => Conversion.ConversionType = value;
    }

    [BindProperty(SupportsGet = true)]
    public string Input
    {
        get => Conversion.Input;
        set => Conversion.Input = value;
    }

    public string Output
    {
        get => Conversion.Output;
        set => Conversion.Output = value;
    }

    public void OnGet()
    {
        ViewData["ConversionType"] = ConversionType;
        ViewData["Title"] = "Conversions";
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
        //.Replace(" ", "") gets rid of my unwanted problem with not accepting blank spaces so that I cant mess it up.
        //Don't ask me why I have this problem. I dunno who puts spacebars in the url but apparently my fat fingers do.

        switch (Conversion.ConversionType.ToLower().Replace(" ", ""))
        {
            case var type when type == ConversionTypes.MilesToKilometers.ToLower():
                Conversion.Output = new Length().FromMiles(value).ToKilometers().ToString();
                break;
            case var type when type == ConversionTypes.KilometersToMiles.ToLower():
                Conversion.Output = new Length().FromKilometers(value).ToMiles().ToString();
                break;
            case var type when type == ConversionTypes.FahrenheitToCelsius.ToLower():
                Conversion.Output = new Temperature().FromFahrenheit(value).ToCelsius().ToString();
                break;
            case var type when type == ConversionTypes.CelsiusToFahrenheit.ToLower():
                Conversion.Output = new Temperature().FromCelsius(value).ToFahrenheit().ToString();
                break;
            case var type when type == ConversionTypes.PoundsToKilograms.ToLower():
                Conversion.Output = new Mass().FromPounds(value).ToKilograms().ToString();
                break;
            case var type when type == ConversionTypes.KilogramsToPounds.ToLower():
                Conversion.Output = new Mass().FromKilograms(value).ToPounds().ToString();
                break;
            case var type when type == ConversionTypes.KsiToPsi.ToLower():
                Conversion.Output = new Pressure().FromKSI(value).ToPSI().ToString();
                break;
            case var type when type == ConversionTypes.PsiToKsi.ToLower():
                Conversion.Output = new Pressure().FromPSI(value).ToKSI().ToString();
                break;
            case var type when type == ConversionTypes.SteresToTuns.ToLower():
                Conversion.Output = new Volume().FromSteres(value).ToTuns().ToString();
                break;
            case var type when type == ConversionTypes.TunsToSteres.ToLower():
                Conversion.Output = new Volume().FromTuns(value).ToSteres().ToString();
                break;
            default:
                ViewData["ErrorMessage"] = "Unknown conversion type. Please check spelling";
                break;
        }
    }
}

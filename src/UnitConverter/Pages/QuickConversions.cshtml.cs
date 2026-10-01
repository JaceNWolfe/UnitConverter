using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnitConverter.Models;
using UnitConverter.Services;

namespace UnitConverter.Pages;

public class QuickConversions : PageModel
{
    private readonly IConversionService conversionService;

    public QuickConversions(IConversionService conversionService)
    {
        this.conversionService = conversionService;
    }

    public string? Output { get; private set; }

    public string? ErrorMessage { get; private set; }

    public IEnumerable<SelectListItem> PoundOptions =>
    [
        new("1 pound", "1"),
        new("5 pounds", "5"),
        new("10 pounds", "10"),
        new("25 pounds", "25"),
        new("50 pounds", "50")
    ];

    public IActionResult OnGetMilesToKilometers(string input)
    {
        return PerformConversion(input, ConversionTypes.MilesToKilometers);
    }

    public IActionResult OnGetKilometersToMiles(string input)
    {
        return PerformConversion(input, ConversionTypes.KilometersToMiles);
    }

    public IActionResult OnGetFahrenheitToCelsius(string input)
    {
        return PerformConversion(input, ConversionTypes.FahrenheitToCelsius);
    }

    public IActionResult OnGetCelsiusToFahrenheit(string input)
    {
        return PerformConversion(input, ConversionTypes.CelsiusToFahrenheit);
    }

    public IActionResult OnGetPoundsToKilograms(string input)
    {
        return PerformConversion(input, ConversionTypes.PoundsToKilograms);
    }

    public IActionResult OnGetKilogramsToPounds(string input)
    {
        return PerformConversion(input, ConversionTypes.KilogramsToPounds);
    }

    public IActionResult OnGetKsiToPsi(string input)
    {
        return PerformConversion(input, ConversionTypes.KsiToPsi);
    }

    public IActionResult OnGetPsiToKsi(string input)
    {
        return PerformConversion(input, ConversionTypes.PsiToKsi);
    }

    public IActionResult OnGetSteresToTuns(string input)
    {
        return PerformConversion(input, ConversionTypes.SteresToTuns);
    }

    public IActionResult OnGetTunsToSteres(string input)
    {
        return PerformConversion(input, ConversionTypes.TunsToSteres);
    }

    private IActionResult PerformConversion(string input, string conversionType)
    {
        if (!decimal.TryParse(input, out decimal value))
        {
            ErrorMessage = "Input must be a valid number.";
            return Page();
        }

        try
        {
            Output = conversionService.Convert(value, conversionType).ToString();
        }
        catch (ArgumentException)
        {
            ErrorMessage = "Unknown conversion type. Please check spelling";
        }

        return Page();
    }
}

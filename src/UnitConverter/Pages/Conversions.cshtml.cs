using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UnitConverter.Models;
using UnitConverter.Services;

namespace UnitConverter.Pages;

public class ConversionsModel : PageModel
{
    private readonly IConversionService conversionService;

    public ConversionsModel()
    {
        conversionService = new UnitOfConversionService();
    }

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

        // Catch if input is not a number
        try
        {
            value = Convert.ToDouble(Input);
        }
        catch (FormatException)
        {
            ViewData["ErrorMessage"] = "Input must be a valid number.";
            return;
        }

        try
        {
            Conversion.Output = conversionService
                .Convert((decimal)value, Conversion.ConversionType)
                .ToString();
        }
        catch (ArgumentException)
        {
            ViewData["ErrorMessage"] = "Unknown conversion type. Please check spelling";
        }
    }
}

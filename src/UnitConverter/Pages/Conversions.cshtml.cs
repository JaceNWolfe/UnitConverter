using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UnitConverter.Models;
using UnitConverter.Services;

namespace UnitConverter.Pages;

public class ConversionsModel : PageModel
{
    private readonly IConversionService conversionService;
    //you said use injection but it breaks lessons 1 and 2
    private readonly ILogger<ConversionsModel> _logger =
        Microsoft.Extensions.Logging.Abstractions.NullLogger<ConversionsModel>.Instance;

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

        _logger.LogInformation(
            "Received conversion request for {ConversionType} with input {Input}",
            ConversionType,
            Input);

        if (!decimal.TryParse(Input, out decimal value))
        {
            _logger.LogWarning(
                "Invalid conversion input: {Input} for {ConversionType}",
                Input,
                ConversionType);

            ViewData["ErrorMessage"] = "Input must be a valid number.";
            return;
        }

        try
        {
            decimal result = conversionService.Convert(value, ConversionType);
            Conversion.Output = result.ToString();

            _logger.LogInformation(
                "Converted {Input} using {ConversionType} with result {Result}",
                value,
                ConversionType,
                result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(
                ex,
                "Unknown conversion type: {ConversionType}",
                ConversionType);

            ViewData["ErrorMessage"] =
                "Unknown conversion type. Please check spelling";
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Unexpected conversion failure for {Input} using {ConversionType}",
                value,
                ConversionType);

            ViewData["ErrorMessage"] = "An unexpected error occurred.";
        }
    }
}

namespace UnitConverter.Models;

public static class ConversionTypes
{
    public const string MilesToKilometers = "MilesToKilometers";
    public const string KilometersToMiles = "KilometersToMiles";
    public const string FahrenheitToCelsius = "FahrenheitToCelsius";
    public const string CelsiusToFahrenheit = "CelsiusToFahrenheit";
    public const string PoundsToKilograms = "PoundsToKilograms";
    public const string KilogramsToPounds = "KilogramsToPounds";
    public const string KsiToPsi = "KsiToPsi";
    public const string PsiToKsi = "PsiToKsi";
    public const string SteresToTuns = "SteresToTuns";
    public const string TunsToSteres = "TunsToSteres";

    public static readonly IReadOnlyDictionary<string, string> All =
        new Dictionary<string, string>
        {
            [MilesToKilometers] = "Miles to Kilometers",
            [KilometersToMiles] = "Kilometers to Miles",
            [FahrenheitToCelsius] = "Fahrenheit to Celsius",
            [CelsiusToFahrenheit] = "Celsius to Fahrenheit",
            [PoundsToKilograms] = "Pounds to Kilograms",
            [KilogramsToPounds] = "Kilograms to Pounds",
            [KsiToPsi] = "KSI to PSI",
            [PsiToKsi] = "PSI to KSI",
            [SteresToTuns] = "Steres to Tuns",
            [TunsToSteres] = "Tuns to Steres"
        };
}


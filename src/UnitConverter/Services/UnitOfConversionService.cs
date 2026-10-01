using UnitConverter.Models;
using UnitOf;

namespace UnitConverter.Services;

public class UnitOfConversionService : IConversionService
{
    public decimal Convert(decimal value, string conversionType)
    {
        switch (conversionType.ToLower().Replace(" ", ""))
        {
            case var type when type == ConversionTypes.MilesToKilometers.ToLower():
                return new decimal(new Length().FromMiles((double)value).ToKilometers());

            case var type when type == ConversionTypes.KilometersToMiles.ToLower():
                return new decimal(new Length().FromKilometers((double)value).ToMiles());

            case var type when type == ConversionTypes.FahrenheitToCelsius.ToLower():
                return new decimal(new Temperature().FromFahrenheit((double)value).ToCelsius());

            case var type when type == ConversionTypes.CelsiusToFahrenheit.ToLower():
                return new decimal(new Temperature().FromCelsius((double)value).ToFahrenheit());

            case var type when type == ConversionTypes.PoundsToKilograms.ToLower():
                return new decimal(new Mass().FromPounds((double)value).ToKilograms());

            case var type when type == ConversionTypes.KilogramsToPounds.ToLower():
                return new decimal(new Mass().FromKilograms((double)value).ToPounds());

            case var type when type == ConversionTypes.KsiToPsi.ToLower():
                return new decimal(new Pressure().FromKSI((double)value).ToPSI());

            case var type when type == ConversionTypes.PsiToKsi.ToLower():
                return new decimal(new Pressure().FromPSI((double)value).ToKSI());

            case var type when type == ConversionTypes.SteresToTuns.ToLower():
                return new decimal(new Volume().FromSteres((double)value).ToTuns());

            case var type when type == ConversionTypes.TunsToSteres.ToLower():
                return new decimal(new Volume().FromTuns((double)value).ToSteres());

            default:
                throw new ArgumentException(
                    $"Unknown conversion type: {conversionType}",
                    nameof(conversionType));
        }
    }
}

using System.Text.RegularExpressions;

namespace Axxes.Carpool.Api.Validators;

public static class BelgianLicensePlateNumberValidator
{
    private static readonly Regex BelgianLicensePlateRegex = new Regex("^([0-9]-)?([A-Z]{3}-[0-9]{3}|[0-9]{3}-[A-Z]{3})$");

    // 1-ABC-123
    // ABC-123
    // 123-ABC


    public static bool Validate(string licensePlate)
    {
        return BelgianLicensePlateRegex.IsMatch(licensePlate);
    }

    public static List<string> GetValidBelgianLicensePlateNumbers(List<string> licensePlateNumbers)
    {
        return licensePlateNumbers.Where(Validate).ToList();
    }

}
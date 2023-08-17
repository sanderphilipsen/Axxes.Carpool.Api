using System.Text.RegularExpressions;

namespace Axxes.Carpool.Api.Validators;

public static class BelgianLicensePlateNumberValidator
{
    private static readonly Regex BelgianLicensePlateRegex = new Regex("^([0-9]-)?([A-Z]{3}-[0-9]{3}|[0-9]{3}-[A-Z]{3})$");

    public static bool Validate(string licensePlate)
    {
        var result = BelgianLicensePlateRegex.Match(licensePlate);
        return result.Success;
    }

    public static List<string> GetValidBelgianLicensePlateNumbers(List<string> licensePlateNumbers)
        => licensePlateNumbers
            .Where(licensePlateNumber => BelgianLicensePlateRegex.Match(licensePlateNumber).Success)
            .ToList();
}
using Axxes.Carpool.Api.Validators;

namespace Axxes.Carpool.Tests.Validators;

public sealed class BelgianLicensePlateNumberValidatorTests
{
    [Theory]
    [InlineData("1-ABC-123", true)]
    [InlineData("9-ABD-183", true)]
    [InlineData("ABC-125", true)]
    [InlineData("ABC-125-8", false)]
    [InlineData("ABC-1Z5", false)]
    [InlineData("-ABC-1Z5", false)]
    [InlineData("AàC-125", false)]
    [InlineData("AàC-/125", false)]
    [InlineData("ABC-125", true)]
    [InlineData("123-ABC", true)]
    [InlineData("1-ABD-123", true)]
    public void Validate_The_licensePlate_Should_Return_The_Expected_Result(string licensePlate, bool expectedResult)
    {
        //Act
        var result = BelgianLicensePlateNumberValidator.Validate(licensePlate);

        //Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void GetValidBelgianLicensePlateNumbers_Should_Only_Return_The_Valid_LicensePlateNumbers()
    {
        //Arrange
        var validLicensePlateNumbers = new List<string> { "1-ABC-123", "ABC-123", "123-ABC" };
        var invalidLicensePlateNumbers = new List<string> { "1-ABCD-123", "-ABC-123", "1253-ABC" };

        var allLicensePlateNumbers = validLicensePlateNumbers.Concat(invalidLicensePlateNumbers).ToList();

        //Act

        var result = BelgianLicensePlateNumberValidator.GetValidBelgianLicensePlateNumbers(allLicensePlateNumbers);

        //Assert
        Assert.Equal(validLicensePlateNumbers, result);
    }


}
using Axxes.Carpool.Api.Validators;

namespace Axxes.Carpool.UnitTests.ValidatorTests;

public sealed class BelgianLicensePlateNumberValidatorTests
{
    [Fact]
    public void BelgianLicensePlateNumberValidator_Returns_True_If_Valid_LicensePlateNumber_Is_Given()
    {
        // Arrange
        var licensePlateNumber = "1-ABC-123";

        // Act
        var isValid = BelgianLicensePlateNumberValidator.Validate(licensePlateNumber);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void BelgianLicensePlateNumberValidator_Returns_False_If_InValid_LicensePlateNumber_Is_Given()
    {
        // Arrange
        var licensePlateNumber = "1-ABC-1234";

        // Act
        var isValid = BelgianLicensePlateNumberValidator.Validate(licensePlateNumber);

        // Assert
        Assert.False(isValid);
    }

    [Theory]
    [InlineData("1-ABC-123")]
    [InlineData("1-ABC-123")]
    [InlineData("123-ABC")]
    [InlineData("9-UOD-259")]
    public void BelgianLicensePlateNumberValidator_Returns_True_If_Valid_LicensePlateNumber(string licensePlateNumber)
    {
        // Act
        var isValid = BelgianLicensePlateNumberValidator.Validate(licensePlateNumber);

        // Assert
        Assert.True(isValid);
    }

    [Theory]
    [InlineData("18-ABC-123")]
    [InlineData("1-ABCD-123")]
    [InlineData("1234-ABC")]
    [InlineData("9-UOD-259-")]
    public void BelgianLicensePlateNumberValidator_Returns_False_If_Valid_LicensePlateNumber(string licensePlateNumber)
    {
        // Act
        var isValid = BelgianLicensePlateNumberValidator.Validate(licensePlateNumber);

        // Assert
        Assert.False(isValid);

    }
}
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators;
public class BacsPaymentValidatorTests
{
    private readonly BacsPaymentValidator _validator = new BacsPaymentValidator();

    [Fact]
    public void WhenIsValidIsCalled_AndAccountIsNull_ThenReturnsFalse()
    {
        // Arrange
        Account account = null;

        // Act
        var result = _validator.IsValid(account, 100);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(AllowedPaymentSchemes.FasterPayments, false)]
    [InlineData(AllowedPaymentSchemes.Chaps, false)]
    [InlineData(AllowedPaymentSchemes.Bacs, true)]
    public void WhenIsValidIsCalled_ValidatePaymentScheme_ThenReturnsExpectedStatus(AllowedPaymentSchemes allowedPayment, bool expectedResult)
    {
        // Arrange
        var account = new Account { AllowedPaymentSchemes = allowedPayment };

        // Act
        var result = _validator.IsValid(account, 100);

        // Assert
        result.Should().Be(expectedResult);
    }
}

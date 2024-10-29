using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators;

public class FasterPaymentsValidatorTests
{
    private readonly FasterPaymentsValidator _validator = new FasterPaymentsValidator();

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
    [InlineData(AllowedPaymentSchemes.FasterPayments, true)]
    [InlineData(AllowedPaymentSchemes.Chaps, false)]
    [InlineData(AllowedPaymentSchemes.Bacs, false)]
    public void WhenIsValidIsCalled_ValidatePaymentScheme_ThenReturnsExpectedStatus(AllowedPaymentSchemes allowedPayment, bool expectedResult)
    {
        // Arrange
        var account = new Account { AllowedPaymentSchemes = allowedPayment, Balance = 200m };

        // Act
        var result = _validator.IsValid(account, 100);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void WhenIsValidIsCalled_WithInsufficientBalance_ThenReturnsFalse()
    {
        // Arrange
        var account = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
            Balance = 50
        };

        // Act
        var result = _validator.IsValid(account, 100);

        // Assert
        result.Should().BeFalse();
    }
}
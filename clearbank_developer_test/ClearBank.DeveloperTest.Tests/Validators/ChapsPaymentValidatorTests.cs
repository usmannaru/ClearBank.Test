using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators;

public class ChapsPaymentValidatorTests
{
    private readonly ChapsPaymentValidator _validator = new ChapsPaymentValidator();

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
    [InlineData(AllowedPaymentSchemes.Chaps, true)]
    [InlineData(AllowedPaymentSchemes.Bacs, false)]
    public void WhenIsValidIsCalled_ValidatePaymentScheme_ThenReturnsExpectedStatus(AllowedPaymentSchemes allowedPayment, bool expectedResult)
    {
        // Arrange
        var account = new Account
        {
            AllowedPaymentSchemes = allowedPayment,
            Status = AccountStatus.Live
        };

        // Act
        var result = _validator.IsValid(account, 100);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void WhenIsValidIsCalled_WithValidPaymentScheme_ButStatusNotLive_ThenReturnsFalse()
    {
        // Arrange
        var account = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
            Status = AccountStatus.Disabled
        };

        // Act
        var result = _validator.IsValid(account, 100);

        // Assert
        result.Should().BeFalse();
    }
}
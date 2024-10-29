using ClearBank.DeveloperTest.Data.Base;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Services;
public class PaymentServiceTests
{
    private readonly IDataStore _dataStoreMock;
    private readonly IPaymentService _paymentService;

    public PaymentServiceTests()
    {
        _dataStoreMock = Substitute.For<IDataStore>();
        _paymentService = new PaymentService(_dataStoreMock);
    }

    [Fact]
    public void WhenMakePaymentIsCalled_WithBacsSchemeAndValidAccount_ThenReturnsSuccess()
    {
        // Arrange
        var account = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
            Balance = 100m
        };
        var request = new MakePaymentRequest
        {
            DebtorAccountNumber = "DB123",
            Amount = 50m,
            PaymentScheme = PaymentScheme.Bacs,
        };
        _dataStoreMock
            .GetAccount(Arg.Any<string>())
            .Returns(account);

        // Act
        var result = _paymentService.MakePayment(request);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        account.Balance.Should().Be(50m);

        _dataStoreMock.Received(1).UpdateAccount(account);
    }

    [Fact]
    public void WhenMakePaymentIsCalled_WithFasterPaymentsSchemeAndInsufficientFunds_ThenReturnsFailure()
    {
        // Arrange
        var account = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
            Balance = 30m
        };
        var request = new MakePaymentRequest
        {
            DebtorAccountNumber = "DB123",
            Amount = 50m,
            PaymentScheme = PaymentScheme.FasterPayments,
        };
        _dataStoreMock
            .GetAccount(Arg.Any<string>())
            .Returns(account);

        // Act
        var result = _paymentService.MakePayment(request);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();

        _dataStoreMock.DidNotReceive().UpdateAccount(account);
    }

    [Fact]
    public void WhenMakePaymentIsCalled_WithChapsSchmeAndAccountNotLive_ThenReturnsFailure()
    {

        // Arrange
        var account = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
            Status = AccountStatus.Disabled
        };
        var request = new MakePaymentRequest
        {
            DebtorAccountNumber = "DB123",
            Amount = 50m,
            PaymentScheme = PaymentScheme.Chaps,
        };
        _dataStoreMock
            .GetAccount(Arg.Any<string>())
            .Returns(account);

        // Act
        var result = _paymentService.MakePayment(request);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();

        _dataStoreMock.DidNotReceive().UpdateAccount(account);
    }
}

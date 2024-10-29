using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using ClearBank.DeveloperTest.Validators.Base;
using System;

namespace ClearBank.DeveloperTest.Factories;

public static class PaymentValidatorFactory
{
    public static IPaymentValidator Create(PaymentScheme scheme)
    {
        return scheme switch
        {
            PaymentScheme.Bacs => new BacsPaymentValidator(),
            PaymentScheme.Chaps => new ChapsPaymentValidator(),
            PaymentScheme.FasterPayments => new FasterPaymentsValidator(),
            _ => throw new NotSupportedException()
        };
    }
}
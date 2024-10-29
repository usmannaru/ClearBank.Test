using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.Base;

namespace ClearBank.DeveloperTest.Validators;

public class ChapsPaymentValidator : IPaymentValidator
{
    public bool IsValid(Account account, decimal amount)
    {
        return
            account != null &&
            account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) &&
            account.Status == AccountStatus.Live;
    }
}


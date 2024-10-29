using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators.Base;

public interface IPaymentValidator
{
    bool IsValid(Account account, decimal amount);
}

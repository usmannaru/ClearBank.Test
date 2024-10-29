using ClearBank.DeveloperTest.Data.Base;
using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services;

public class PaymentService : IPaymentService
{
    private readonly IDataStore _dataStore;
    public PaymentService(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public MakePaymentResult MakePayment(MakePaymentRequest request)
    {
        var account = _dataStore.GetAccount(request.DebtorAccountNumber);

        var validator = PaymentValidatorFactory.Create(request.PaymentScheme);
        var result = new MakePaymentResult
        {
            Success = validator.IsValid(account, request.Amount)
        };

        if (result.Success)
        {
            account.Balance -= request.Amount;
            _dataStore.UpdateAccount(account);
        }

        return result;
    }
}

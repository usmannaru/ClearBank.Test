using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Data.Base;
using System.Configuration;

namespace ClearBank.DeveloperTest.Factories;

public static class DataStoreFactory
{
    private const string BackupStore = "Backup";
    private const string DataStoreType = "DataStoreType";

    public static IDataStore Create()
    {
        var dataStoreType = ConfigurationManager.AppSettings[DataStoreType];
        return dataStoreType == BackupStore ? new BackupAccountDataStore() : new AccountDataStore();
    }
}

namespace AtmSystem.TransactionRepositories;

public interface ITransactionRepository
{
    void Save(BankTransaction newBankTransaction);
    IEnumerable<BankTransaction> GetTransactions();
}
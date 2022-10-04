namespace AtmSystem.TransactionRepositories;

public class InMemoryRepository : ITransactionRepository
{
    private readonly List<BankTransaction> _transactions = new();
    public void Save(BankTransaction newBankTransaction)
    {
        _transactions.Add(newBankTransaction);
    }
}
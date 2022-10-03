namespace AtmSystem;

public interface ITransactionRepository
{
    void Save(BankTransaction newBankTransaction);
}
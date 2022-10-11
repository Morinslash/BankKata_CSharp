namespace AtmSystem;

public interface IStatementFormatter
{
    void Print(IEnumerable<BankTransaction> bankTransactions);
}
namespace AtmSystem.Formatters;

public interface IStatementFormatter
{
    void Print(IEnumerable<BankTransaction> bankTransactions);
}
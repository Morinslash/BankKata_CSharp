namespace AtmSystem.Printers;

public interface IPrinter
{
    void Display(string output);
    void Print(IEnumerable<BankTransaction> bankTransactions);
}
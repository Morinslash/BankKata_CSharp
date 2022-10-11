using System.Text;
using AtmSystem.Printers;

namespace AtmSystem.Formatters;

public class AtmFormatter : IStatementFormatter
{
    private const string Header = "Date || Amount || Balance \n";
    private readonly IPrinter _printerObject;

    public AtmFormatter(IPrinter printerObject)
    {
        _printerObject = printerObject;
    }

    public void Print(IEnumerable<BankTransaction> bankTransactions)
    {
        var output = new StringBuilder(Header);
        var balance = 0;
        output.Append(string.Join(" \n", bankTransactions
            .Select(t => (Transaction: t, Balance: balance += t.Amount))
            .OrderByDescending(t => DateOnly.Parse(t.Transaction.TransactionDate!))
            .Select(t => FormatTransaction(t.Transaction, t.Balance))));
        _printerObject.Print(output.ToString());
    }

    private string FormatTransaction(BankTransaction transaction, int balance) =>
        $"{transaction.TransactionDate} || {transaction.Amount} || {balance}";
}
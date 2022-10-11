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
        output.Append(string.Join(" \n", bankTransactions
            .Select(FormatTransaction)));
        _printerObject.Print(output.ToString());
    }

    private string FormatTransaction(BankTransaction transaction) => $"{transaction.TransactionDate} || {transaction.Amount} || {transaction.Amount} \n";
}
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
            .Select(trans => (Transaction: trans, Balance: balance+=trans.Amount))
            .OrderByDescending(tran => DateOnly.Parse(tran.Transaction.TransactionDate!))
            .Select(trans => FormatTransaction(trans.Transaction, trans.Balance))));
        _printerObject.Print(output.ToString());
    }

    private string FormatTransaction(BankTransaction transaction, int balance) => $"{transaction.TransactionDate} || {transaction.Amount} || {balance}";
}
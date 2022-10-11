using AtmSystem.Printers;

namespace AtmSystem;

public class AtmFormatter : IStatementFormatter
{
    private readonly IPrinter _printerObject;

    public AtmFormatter(IPrinter printerObject)
    {
        _printerObject = printerObject;
    }

    public void Print(IEnumerable<BankTransaction> bankTransactions)
    {
        // logic to format
        var output = "";
        _printerObject.Print(output);
    }
}
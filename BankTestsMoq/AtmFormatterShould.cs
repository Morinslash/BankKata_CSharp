using AtmSystem;
using AtmSystem.Formatters;
using AtmSystem.Printers;
using Moq;

namespace BankTestsMoq;

public class AtmFormatterShould
{
    private readonly Mock<IPrinter> _printerMock;
    private readonly AtmFormatter _atmFormatter;

    public AtmFormatterShould()
    {
        _printerMock = new Mock<IPrinter>();
        _atmFormatter = new AtmFormatter(_printerMock.Object);
    }

    [Fact]
    public void Invoke_Printer_Only_With_Headers_Information_If_No_Transactions_Passed()
    {
        _atmFormatter.Print(new List<BankTransaction>());
        
        _printerMock.Verify(mock => mock.Print("Date || Amount || Balance \n"));
    }

    [Fact]
    public void Invoke_Printer_With_One_Transaction_If_One_In_List()
    {
        var bankTransactions = new List<BankTransaction>
        {
            new(){Amount = 100, TransactionDate = "14/01/2012"}
        };
        _atmFormatter.Print(bankTransactions);
        _printerMock.Verify(mock => mock.Print(
            "Date || Amount || Balance \n" +
            "14/01/2012 || 100 || 100 \n"
            ));
    }
}
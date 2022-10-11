using AtmSystem;
using AtmSystem.Calendars;
using AtmSystem.Formatters;
using AtmSystem.Printers;
using AtmSystem.TransactionRepositories;
using Moq;

namespace BankTestsMoq;

public class AtmAcceptanceTest
{
    [Fact]
    public void WithTwoDepositsAndAWithdrawPrintProperlyFormattedStatement()
    {
        Mock<IPrinter> mockPrinter = new Mock<IPrinter>();
        Mock<ICalendar> mockCalendar = new Mock<ICalendar>();
        mockCalendar
            .SetupSequence(c => c.TransactionDate())
            .Returns("10/01/2012")
            .Returns("13/01/2012")
            .Returns("14/01/2012");


        ITransactionRepository repository = new InMemoryRepository();
        var statementFormatter = new AtmFormatter(mockPrinter.Object);
        var atm = new Atm(statementFormatter, repository, mockCalendar.Object);

        atm.Deposit(1000);
        atm.Deposit(2000);
        atm.Withdraw(500);

        atm.PrintStatement();
        mockPrinter.Verify(mock => mock.Print(
            "Date || Amount || Balance \n" +
            "14/01/2012 || -500 || 2500 \n" +
            "13/01/2012 || 2000 || 3000 \n" +
            "10/01/2012 || 1000 || 1000"), Times.Once);
    }
}
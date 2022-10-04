using AtmSystem.Calendars;
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
        ITransactionRepository repository = new InMemoryRepository();
        AtmSystem.Atm atm = new AtmSystem.Atm(mockPrinter.Object, repository, mockCalendar.Object);

        atm.Deposit(1000);
        atm.Deposit(2000);
        atm.Withdraw(500);

        atm.PrintStatement();
        mockPrinter.Verify(mock => mock.Print(
            "Date || Amount || Balance \n" +
            "14/01/2012 || -500 || 2500 \n" +
            "13/01/2012 || 2000 || 3000 \n" +
            "10/01/2012 || 1000 || 1000 \n"), Times.Once);
    }
}
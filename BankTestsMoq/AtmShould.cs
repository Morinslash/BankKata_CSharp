using AtmSystem;
using Moq;

namespace BankTestsMoq;

public class AtmShould
{
    [Fact]
    public void Deposit_100_With_The_Repository()
    {
        var transactionDate = "14/01/2012";
        BankTransaction expectedBankTransaction = new BankTransaction
        {
            Amount = 100,
            TransactionDate = transactionDate
        };
        Mock<IPrinter> printer = new Mock<IPrinter>();
        Mock<ICalendar> mockCalendar = new Mock<ICalendar>();
        mockCalendar
            .Setup(calendar => calendar.TransactionDate())
            .Returns(transactionDate);
        Mock<ITransactionRepository> mockRepository = new Mock<ITransactionRepository>();

        var atm = new Atm(printer.Object, mockRepository.Object, mockCalendar.Object);

        atm.Deposit(100);

        mockRepository.Verify(mock => mock.Save(
            It.Is<BankTransaction>(ex => ex.Equals(expectedBankTransaction))), Times.Once);
    }
}
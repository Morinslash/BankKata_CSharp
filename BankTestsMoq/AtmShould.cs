using AtmSystem;
using AtmSystem.Calendars;
using AtmSystem.Printers;
using AtmSystem.TransactionRepositories;
using FluentAssertions;
using Moq;

namespace BankTestsMoq;

public class AtmShould
{
    private readonly string _transactionDate = "14/01/2012";
    private readonly Mock<IPrinter> _printer;
    private readonly Mock<ICalendar> _mockCalendar;
    private readonly Mock<ITransactionRepository> _mockRepository;
    private readonly Atm _atm;

    public AtmShould()
    {
        _printer = new Mock<IPrinter>();
        _mockCalendar = new Mock<ICalendar>();
        _mockRepository = new Mock<ITransactionRepository>();
        _atm = new Atm(_printer.Object, _mockRepository.Object, _mockCalendar.Object);
    }

    [Fact]
    public void Deposit_100_With_The_Repository()
    {
        BankTransaction expectedBankTransaction = new BankTransaction
        {
            Amount = 100,
            TransactionDate = _transactionDate
        };
        _mockCalendar
            .Setup(calendar => calendar.TransactionDate())
            .Returns(_transactionDate);

        _atm.Deposit(100);

        _mockRepository.Verify(mock => mock.Save(
            It.Is<BankTransaction>(ex => ex.Equals(expectedBankTransaction))), Times.Once);
    }

    [Fact]
    public void Not_Accept_Negative_Amount_Of_Deposit()
    {
        Action act = () => _atm.Deposit(-100);
        act.Should().Throw<ArgumentException>().WithMessage("Deposit cannot be negative.");
    }
}
using AtmSystem;
using AtmSystem.Calendars;
using AtmSystem.Formatters;
using AtmSystem.TransactionRepositories;
using FluentAssertions;
using Moq;

namespace BankTestsMoq;

public class AtmShould
{
    private readonly string _transactionDate = "14/01/2012";
    private readonly Mock<ICalendar> _mockCalendar;
    private readonly Mock<ITransactionRepository> _mockRepository;
    private readonly Mock<IStatementFormatter> _statementFormatter;
    private readonly Atm _atm;

    public AtmShould()
    {
        _mockCalendar = new Mock<ICalendar>();
        _mockRepository = new Mock<ITransactionRepository>();
        _statementFormatter = new Mock<IStatementFormatter>();
        _atm = new Atm(_statementFormatter.Object, _mockRepository.Object, _mockCalendar.Object);
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
        act.Should().Throw<ArgumentException>().WithMessage("Amount cannot be negative.");
    }

    [Fact]
    public void Should_Withdraw_100_From_Account()
    {
        BankTransaction expectedBankTransaction = new BankTransaction
        {
            Amount = -100,
            TransactionDate = _transactionDate
        };
        _mockCalendar
            .Setup(calendar => calendar.TransactionDate())
            .Returns(_transactionDate);

        _atm.Withdraw(100);

        _mockRepository.Verify(mock => mock.Save(
            It.Is<BankTransaction>(ex => ex.Equals(expectedBankTransaction))), Times.Once);
    }
    [Fact]
    public void Not_Accept_Negative_Amount_For_Withdraw()
    {
        Action act = () => _atm.Withdraw(-100);
        act.Should().Throw<ArgumentException>()
            .WithMessage("Amount cannot be negative.");
    }

    [Fact]
    public void Call_Formatter_With_Empty_List_Of_Transactions_If_Non_Done()
    {
        var bankTransactions = new List<BankTransaction>();
        _mockRepository.Setup(mock => mock.GetTransactions())
            .Returns(bankTransactions);
        _atm.PrintStatement();
        
        _statementFormatter.Verify(mock => mock.Print(
            It.Is<IEnumerable<BankTransaction>>(list => !list.Any())), Times.Once());
    }

    [Fact]
    public void Display_One_Deposit_As_One_Was_Made()
    {
        var bankTransactions = new List<BankTransaction>()
        {
            new BankTransaction(){Amount = 100, TransactionDate = _transactionDate}
        };
        _mockRepository.Setup(mock => mock.GetTransactions())
            .Returns(bankTransactions);
        _atm.PrintStatement();
        
        _statementFormatter.Verify(mock => mock.Print(
            It.Is<IEnumerable<BankTransaction>>(
                list => list.Count() == 1)), Times.Once);
    }
}
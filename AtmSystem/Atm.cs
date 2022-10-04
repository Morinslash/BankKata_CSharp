using AtmSystem.Calendars;
using AtmSystem.Printers;
using AtmSystem.TransactionRepositories;

namespace AtmSystem;

public class Atm : IAccountService
{
    private readonly IPrinter _printer;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICalendar _accountCalendar;

    public Atm(IPrinter printer, ITransactionRepository transactionRepository, ICalendar accountCalendar)
    {
        _printer = printer;
        _transactionRepository = transactionRepository;
        _accountCalendar = accountCalendar;
    }

    public void Deposit(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative.");
        }
        var bankTransaction = new BankTransaction
        {
            Amount = amount,
            TransactionDate = _accountCalendar.TransactionDate()
        };
        _transactionRepository.Save(bankTransaction);
    }

    public void Withdraw(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative.");
        }
        var bankTransaction = new BankTransaction
        {
            Amount = -amount,
            TransactionDate = _accountCalendar.TransactionDate()
        };
        _transactionRepository.Save(bankTransaction);
    }

    public void PrintStatement()
    {
        throw new NotImplementedException();
    }
}
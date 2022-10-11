using AtmSystem.Calendars;
using AtmSystem.Formatters;
using AtmSystem.TransactionRepositories;

namespace AtmSystem;

public class Atm : IAccountService
{
    private readonly IStatementFormatter _formatter;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICalendar _accountCalendar;

    public Atm(IStatementFormatter formatter, ITransactionRepository transactionRepository, ICalendar accountCalendar)
    {
        _formatter = formatter;
        _transactionRepository = transactionRepository;
        _accountCalendar = accountCalendar;
    }

    public void Deposit(int amount)
    {
        NegativeAmountGuard(amount);
        _transactionRepository.Save(CreateTransaction(amount));
    }

    public void Withdraw(int amount)
    {
        NegativeAmountGuard(amount);
        _transactionRepository.Save(CreateTransaction(-amount));
    }

    public void PrintStatement()
    {
        _formatter.Print(_transactionRepository.GetTransactions());
    }

    private void NegativeAmountGuard(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative.");
        }
    }

    private BankTransaction CreateTransaction(int amountToStore)
    {
        return new BankTransaction()
        {
            Amount = amountToStore,
            TransactionDate = _accountCalendar.TransactionDate()
        };
    }
}
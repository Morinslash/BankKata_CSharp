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
        var bankTransaction = new BankTransaction
        {
            Amount = amount,
            TransactionDate = _accountCalendar.TransactionDate()
        };
        _transactionRepository.Save(bankTransaction);
    }

    public void Withdraw(int amount)
    {
        throw new NotImplementedException();
    }

    public void PrintStatement()
    {
        throw new NotImplementedException();
    }
}
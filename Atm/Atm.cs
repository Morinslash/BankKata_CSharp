namespace Atm;

public class Atm : IAccountService
{
    private readonly IPrinter _printer;

    public Atm(IPrinter printer)
    {
        _printer = printer;
    }

    public void Deposit(int amount)
    {
        throw new NotImplementedException();
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
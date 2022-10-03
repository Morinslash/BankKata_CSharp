namespace Atm;

public interface IAccountService
{
    void Deposit(int amount);
    void Withdraw(int amount);
    void PrintStatement();
}
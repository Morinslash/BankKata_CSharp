namespace AtmSystem;

public record BankTransaction()
{
    public int Amount { get; set; }
    public string? TransactionDate { get; set; }
}
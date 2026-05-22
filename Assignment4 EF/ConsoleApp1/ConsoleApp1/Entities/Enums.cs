namespace ConsoleApp1.Entities
{
    public enum CustomerType { Individual = 1, Business = 2 }
    public enum AccountType { Savings = 1, Current = 2, Business = 3 }
    public enum OwnershipType { Primary = 1, CoHolder = 2 }
    public enum AccountStatus { Active = 1, Closed = 2 }
    public enum TransactionType { Deposit, Withdrawal, Transfer, Payment }
}
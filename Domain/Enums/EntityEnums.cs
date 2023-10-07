namespace Domain.Enums
{
    public enum StaffRole
    {
        Manager,
        Employee
    }
    public enum StaffStatus
    {
        Inactive,
        Active
    }
    public enum Gender
    {
        Male,
        Female,
        Other,
    }
    public enum CategoryStatus
    {
        Active,
        Inactive,
    }
    public enum ProductStatus
    {
        Active,
        Inactive,
        OutOfStock,
    }
    public enum CustomerStatus
    {
        Inactive,
        Active,
        Restricted,
    }
    public enum CardStatus
    {
        Inactive,
        Active,
    }
    public enum WalletStatus
    {
        Inactive,
        Active,
    }
    public enum TransactionType
    {
        Purchase,
        RechargeBalance
    }
    public enum TransactionMethod
    {
        Cash,
        eWallet,
        Card
    }
    public enum TransactionStatus
    {
        Pending,
        Completed,
        Cancelled,
        Failed,
    }
    public enum InvoiceStatus
    {
        Pending,
        Paid,
        Cancelled,
        Failed
    }
}

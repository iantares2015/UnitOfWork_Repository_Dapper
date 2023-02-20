namespace UOW_Repo_Dapper.Models.ViewModels;

public class TransactionViewModel
{
    public string CurrentName { get; set; }
    public decimal CurrentBalance { get; set; }
    public string SenderName { get; set; }
    public string ReceiverName { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
}
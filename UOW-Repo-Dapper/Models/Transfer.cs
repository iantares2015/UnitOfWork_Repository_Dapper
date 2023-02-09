namespace UOW_Repo_Dapper.Models;

public class Transfer
{
    public string Id { get; set; }

    public int SourceUserId { get; set; }

    public int TargetUserId { get; set; }

    public double Amount { get; set; }

    public DateTime Timestamp { get; set; }
}
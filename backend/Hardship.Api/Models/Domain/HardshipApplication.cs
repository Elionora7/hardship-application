namespace Hardship.Api.Models.Domain;

public class HardshipApplication
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public decimal Income { get; set; }
    public decimal Expenses { get; set; }
    public string? HardshipReason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

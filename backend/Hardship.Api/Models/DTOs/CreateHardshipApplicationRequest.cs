namespace Hardship.Api.Models.DTOs;

public class CreateHardshipApplicationRequest
{
    public string CustomerName { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }
    public decimal Income { get; set; }
    public decimal Expenses { get; set; }
    public string? HardshipReason { get; set; }
}

using SQLite;

namespace DiveHubLogbook.Models;

public class Dive
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int DiveNumber { get; set; }

    public DateTime DiveDate { get; set; } = DateTime.Today;

    public string? DiveSite { get; set; }

    public double MaxDepth { get; set; }

    public int? BottomTimeMinutes { get; set; }

    public double? WaterTemperature { get; set; }

    // Exposure Suit
    public string? SuitType { get; set; }

    public string? SuitLength { get; set; }

    public double? SuitThicknessMm { get; set; }

    public double? WeightKg { get; set; }

    // Dive Buddy
    public string? DiveBuddy { get; set; }

    public string? Notes { get; set; }
}
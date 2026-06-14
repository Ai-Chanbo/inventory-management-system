namespace InventoryManagementSystem.ViewModels;

public class InventorySummaryViewModel
{
    public int ProductId { get; set; }

    public string ProductCode { get; set; } = string.Empty;

    public string ProductName { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Unit { get; set; } = string.Empty;

    public int CurrentStock { get; set; }

    public int SafetyStock { get; set; }

    public string Status => CurrentStock <= 0 ? "OutOfStock" : CurrentStock <= SafetyStock ? "Low" : "Normal";

    public string StatusText => Status switch
    {
        "OutOfStock" => "不足",
        "Low" => "注意",
        _ => "正常"
    };

    public string StatusBadgeClass => Status switch
    {
        "OutOfStock" => "text-bg-danger",
        "Low" => "text-bg-warning",
        _ => "text-bg-success"
    };
}

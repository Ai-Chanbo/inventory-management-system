namespace InventoryManagementSystem.ViewModels;

public class DashboardViewModel
{
    public int ProductCount { get; set; }

    public int TotalStock { get; set; }

    public int LowStockCount { get; set; }

    public int OutOfStockCount { get; set; }

    public IReadOnlyList<InventorySummaryViewModel> LowStocks { get; set; } = [];

    public IReadOnlyList<CategoryStockViewModel> CategoryStocks { get; set; } = [];

    public IReadOnlyList<RecentTransactionViewModel> RecentTransactions { get; set; } = [];
}

public class CategoryStockViewModel
{
    public string Category { get; set; } = string.Empty;

    public int Quantity { get; set; }
}

public class RecentTransactionViewModel
{
    public string ProductCode { get; set; } = string.Empty;

    public string ProductName { get; set; } = string.Empty;

    public string TypeText { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public string Unit { get; set; } = string.Empty;

    public DateTime TransactionDate { get; set; }
}

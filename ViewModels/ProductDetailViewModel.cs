using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.ViewModels;

public class ProductDetailViewModel
{
    public Product Product { get; set; } = new();

    public InventorySummaryViewModel Inventory { get; set; } = new();

    public IReadOnlyList<StockTransaction> Transactions { get; set; } = [];
}

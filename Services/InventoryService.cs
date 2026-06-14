using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Services;

public class InventoryService
{
    private readonly AppDbContext _context;

    public InventoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<InventorySummaryViewModel>> GetInventorySummariesAsync()
    {
        var products = await _context.Products
            .AsNoTracking()
            .OrderBy(p => p.Category)
            .ThenBy(p => p.Code)
            .Select(p => new
            {
                p.Id,
                p.Code,
                p.Name,
                p.Category,
                p.Unit,
                p.SafetyStock,
                CurrentStock = p.StockTransactions.Sum(t => t.TransactionType == TransactionType.In ? t.Quantity : -t.Quantity)
            })
            .ToListAsync();

        return products.Select(p => new InventorySummaryViewModel
        {
            ProductId = p.Id,
            ProductCode = p.Code,
            ProductName = p.Name,
            Category = p.Category,
            Unit = p.Unit,
            SafetyStock = p.SafetyStock,
            CurrentStock = p.CurrentStock
        }).ToList();
    }

    public async Task<InventorySummaryViewModel?> GetInventorySummaryAsync(int productId)
    {
        return (await GetInventorySummariesAsync()).FirstOrDefault(x => x.ProductId == productId);
    }

    public async Task<DashboardViewModel> GetDashboardAsync()
    {
        var summaries = await GetInventorySummariesAsync();
        var recentTransactions = await _context.StockTransactions
            .AsNoTracking()
            .Include(t => t.Product)
            .OrderByDescending(t => t.TransactionDate)
            .ThenByDescending(t => t.Id)
            .Take(8)
            .Select(t => new RecentTransactionViewModel
            {
                ProductCode = t.Product!.Code,
                ProductName = t.Product.Name,
                Unit = t.Product.Unit,
                Quantity = t.Quantity,
                TypeText = t.TransactionType == TransactionType.In ? "入庫" : "出庫",
                TransactionDate = t.TransactionDate
            })
            .ToListAsync();

        return new DashboardViewModel
        {
            ProductCount = summaries.Count,
            TotalStock = summaries.Sum(x => x.CurrentStock),
            LowStockCount = summaries.Count(x => x.Status == "Low"),
            OutOfStockCount = summaries.Count(x => x.Status == "OutOfStock"),
            LowStocks = summaries.Where(x => x.Status != "Normal").OrderBy(x => x.CurrentStock).Take(6).ToList(),
            CategoryStocks = summaries
                .GroupBy(x => x.Category)
                .Select(g => new CategoryStockViewModel { Category = g.Key, Quantity = g.Sum(x => x.CurrentStock) })
                .OrderByDescending(x => x.Quantity)
                .ToList(),
            RecentTransactions = recentTransactions
        };
    }

    public async Task<bool> CanStockOutAsync(int productId, int quantity)
    {
        var inventory = await GetInventorySummaryAsync(productId);
        return inventory is not null && inventory.CurrentStock >= quantity;
    }
}

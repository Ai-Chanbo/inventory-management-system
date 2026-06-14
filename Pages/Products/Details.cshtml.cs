using InventoryManagementSystem.Data;
using InventoryManagementSystem.Services;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Pages.Products;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly InventoryService _inventoryService;

    public DetailsModel(AppDbContext context, InventoryService inventoryService)
    {
        _context = context;
        _inventoryService = inventoryService;
    }

    public ProductDetailViewModel Detail { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (product is null)
        {
            return NotFound();
        }

        Detail.Product = product;
        Detail.Inventory = await _inventoryService.GetInventorySummaryAsync(id) ?? new();
        Detail.Transactions = await _context.StockTransactions
            .AsNoTracking()
            .Where(t => t.ProductId == id)
            .OrderByDescending(t => t.TransactionDate)
            .ThenByDescending(t => t.Id)
            .ToListAsync();

        return Page();
    }
}

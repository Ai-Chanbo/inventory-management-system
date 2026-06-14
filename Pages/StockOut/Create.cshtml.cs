using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Pages.StockOut;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly InventoryService _inventoryService;

    public CreateModel(AppDbContext context, InventoryService inventoryService)
    {
        _context = context;
        _inventoryService = inventoryService;
    }

    [BindProperty]
    public StockTransaction Transaction { get; set; } = new() { TransactionType = TransactionType.Out };

    public SelectList ProductOptions { get; set; } = new(Array.Empty<SelectListItem>());

    public async Task OnGetAsync()
    {
        await LoadProductsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Transaction.TransactionType = TransactionType.Out;

        if (!await _inventoryService.CanStockOutAsync(Transaction.ProductId, Transaction.Quantity))
        {
            ModelState.AddModelError("Transaction.Quantity", "現在庫を超える出庫は登録できません。");
        }

        if (!ModelState.IsValid)
        {
            await LoadProductsAsync();
            return Page();
        }

        Transaction.CreatedAt = DateTime.Now;
        _context.StockTransactions.Add(Transaction);
        await _context.SaveChangesAsync();
        return RedirectToPage("/Inventory/Index");
    }

    private async Task LoadProductsAsync()
    {
        var products = await _context.Products
            .AsNoTracking()
            .OrderBy(p => p.Code)
            .Select(p => new { p.Id, Label = p.Code + " - " + p.Name })
            .ToListAsync();

        ProductOptions = new SelectList(products, "Id", "Label");
    }
}

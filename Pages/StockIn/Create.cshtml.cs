using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Pages.StockIn;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public StockTransaction Transaction { get; set; } = new() { TransactionType = TransactionType.In };

    public SelectList ProductOptions { get; set; } = new(Array.Empty<SelectListItem>());

    public async Task OnGetAsync()
    {
        await LoadProductsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Transaction.TransactionType = TransactionType.In;

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

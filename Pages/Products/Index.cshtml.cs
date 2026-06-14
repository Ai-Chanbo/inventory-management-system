using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Services;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Pages.Products;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly InventoryService _inventoryService;

    public IndexModel(AppDbContext context, InventoryService inventoryService)
    {
        _context = context;
        _inventoryService = inventoryService;
    }

    public IList<Product> Products { get; set; } = [];

    public IReadOnlyDictionary<int, InventorySummaryViewModel> Inventories { get; set; } = new Dictionary<int, InventorySummaryViewModel>();

    public async Task OnGetAsync()
    {
        Products = await _context.Products.AsNoTracking().OrderBy(p => p.Code).ToListAsync();
        Inventories = (await _inventoryService.GetInventorySummariesAsync()).ToDictionary(x => x.ProductId);
    }
}

using InventoryManagementSystem.Services;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InventoryManagementSystem.Pages.Inventory;

public class IndexModel : PageModel
{
    private readonly InventoryService _inventoryService;

    public IndexModel(InventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    public IReadOnlyList<InventorySummaryViewModel> Items { get; set; } = [];

    public async Task OnGetAsync()
    {
        Items = await _inventoryService.GetInventorySummariesAsync();
    }
}

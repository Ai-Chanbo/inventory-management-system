using InventoryManagementSystem.Services;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InventoryManagementSystem.Pages.Alerts;

public class IndexModel : PageModel
{
    private readonly InventoryService _inventoryService;

    public IndexModel(InventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    public IReadOnlyList<InventorySummaryViewModel> Alerts { get; set; } = [];

    public async Task OnGetAsync()
    {
        Alerts = (await _inventoryService.GetInventorySummariesAsync())
            .Where(x => x.Status != "Normal")
            .OrderBy(x => x.CurrentStock)
            .ToList();
    }
}

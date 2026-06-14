using InventoryManagementSystem.Services;
using InventoryManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InventoryManagementSystem.Pages;

public class IndexModel : PageModel
{
    private readonly InventoryService _inventoryService;

    public IndexModel(InventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    public DashboardViewModel Dashboard { get; set; } = new();

    public async Task OnGetAsync()
    {
        Dashboard = await _inventoryService.GetDashboardAsync();
    }
}

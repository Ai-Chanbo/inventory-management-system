using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Pages.Transactions;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    [BindProperty(SupportsGet = true)]
    public TransactionType? Type { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? From { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? To { get; set; }

    public IReadOnlyList<StockTransaction> Transactions { get; set; } = [];

    public async Task OnGetAsync()
    {
        var query = _context.StockTransactions
            .AsNoTracking()
            .Include(t => t.Product)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(Search))
        {
            query = query.Where(t =>
                t.Product!.Code.Contains(Search) ||
                t.Product.Name.Contains(Search) ||
                t.Product.Category.Contains(Search) ||
                (t.LocationOrPurpose != null && t.LocationOrPurpose.Contains(Search)));
        }

        if (Type.HasValue)
        {
            query = query.Where(t => t.TransactionType == Type.Value);
        }

        if (From.HasValue)
        {
            query = query.Where(t => t.TransactionDate.Date >= From.Value.Date);
        }

        if (To.HasValue)
        {
            query = query.Where(t => t.TransactionDate.Date <= To.Value.Date);
        }

        Transactions = await query
            .OrderByDescending(t => t.TransactionDate)
            .ThenByDescending(t => t.Id)
            .ToListAsync();
    }
}

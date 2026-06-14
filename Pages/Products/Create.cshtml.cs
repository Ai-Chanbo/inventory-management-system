using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Pages.Products;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Product Product { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (await _context.Products.AnyAsync(p => p.Code == Product.Code))
        {
            ModelState.AddModelError("Product.Code", "同じ商品コードが既に登録されています。");
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        Product.CreatedAt = DateTime.Now;
        _context.Products.Add(Product);
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}

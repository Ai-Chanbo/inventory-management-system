using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Pages.Products;

public class EditModel : PageModel
{
    private readonly AppDbContext _context;

    public EditModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Product Product { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        Product = product;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (await _context.Products.AnyAsync(p => p.Code == Product.Code && p.Id != Product.Id))
        {
            ModelState.AddModelError("Product.Code", "同じ商品コードが既に登録されています。");
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var existing = await _context.Products.FindAsync(Product.Id);
        if (existing is null)
        {
            return NotFound();
        }

        existing.Code = Product.Code;
        existing.Name = Product.Name;
        existing.Category = Product.Category;
        existing.Unit = Product.Unit;
        existing.SafetyStock = Product.SafetyStock;
        existing.Description = Product.Description;
        existing.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}

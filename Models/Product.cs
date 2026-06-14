using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "商品コードは必須です。")]
    [StringLength(30)]
    [Display(Name = "商品コード")]
    public string Code { get; set; } = string.Empty;

    [Required(ErrorMessage = "商品名は必須です。")]
    [StringLength(100)]
    [Display(Name = "商品名")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "カテゴリは必須です。")]
    [StringLength(40)]
    [Display(Name = "カテゴリ")]
    public string Category { get; set; } = string.Empty;

    [Required(ErrorMessage = "単位は必須です。")]
    [StringLength(20)]
    [Display(Name = "単位")]
    public string Unit { get; set; } = "個";

    [Range(0, 999999)]
    [Display(Name = "安全在庫")]
    public int SafetyStock { get; set; }

    [StringLength(200)]
    [Display(Name = "備考")]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    public ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
}

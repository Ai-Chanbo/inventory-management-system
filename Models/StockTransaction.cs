using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models;

public class StockTransaction
{
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "商品を選択してください。")]
    [Display(Name = "商品")]
    public int ProductId { get; set; }

    public Product? Product { get; set; }

    [Required]
    [Display(Name = "区分")]
    public TransactionType TransactionType { get; set; }

    [Range(1, 999999, ErrorMessage = "数量は1以上で入力してください。")]
    [Display(Name = "数量")]
    public int Quantity { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "入出庫日")]
    public DateTime TransactionDate { get; set; } = DateTime.Today;

    [StringLength(100)]
    [Display(Name = "保管場所・使用先")]
    public string? LocationOrPurpose { get; set; }

    [StringLength(200)]
    [Display(Name = "備考")]
    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Products.Any())
        {
            return;
        }

        var now = DateTime.Now;
        var products = new List<Product>
        {
            new() { Code = "SEN-001", Name = "近接センサー M12", Category = "センサー", Unit = "個", SafetyStock = 8, Description = "ライン設備の検出用", CreatedAt = now },
            new() { Code = "PLC-001", Name = "PLC 入力ユニット", Category = "PLC部品", Unit = "個", SafetyStock = 3, Description = "制御盤交換部品", CreatedAt = now },
            new() { Code = "VAL-001", Name = "5ポート電磁弁", Category = "電磁弁", Unit = "個", SafetyStock = 6, CreatedAt = now },
            new() { Code = "CYL-001", Name = "エアシリンダー 50mm", Category = "シリンダー", Unit = "本", SafetyStock = 5, CreatedAt = now },
            new() { Code = "BRG-001", Name = "深溝玉ベアリング 6204", Category = "ベアリング", Unit = "個", SafetyStock = 12, CreatedAt = now },
            new() { Code = "BLT-001", Name = "搬送ベルト 800mm", Category = "ベルト", Unit = "本", SafetyStock = 4, CreatedAt = now },
            new() { Code = "FLT-001", Name = "エアフィルター AF30", Category = "フィルター", Unit = "個", SafetyStock = 10, CreatedAt = now },
            new() { Code = "RLY-001", Name = "小型パワーリレー", Category = "リレー", Unit = "個", SafetyStock = 15, CreatedAt = now },
            new() { Code = "FUS-001", Name = "管ヒューズ 2A", Category = "ヒューズ", Unit = "個", SafetyStock = 20, CreatedAt = now },
            new() { Code = "HOS-001", Name = "油圧ホース 1/2", Category = "油圧ホース", Unit = "本", SafetyStock = 3, CreatedAt = now },
            new() { Code = "TLS-001", Name = "トルクレンチ", Category = "工具", Unit = "本", SafetyStock = 2, CreatedAt = now }
        };

        context.Products.AddRange(products);
        context.SaveChanges();

        var transactions = new List<StockTransaction>
        {
            StockIn(products[0], 25, "第1倉庫", now.AddDays(-20)), StockOut(products[0], 19, "組立ラインA", now.AddDays(-2)),
            StockIn(products[1], 8, "制御部品棚", now.AddDays(-18)), StockOut(products[1], 3, "成形機3号", now.AddDays(-4)),
            StockIn(products[2], 18, "空圧部品棚", now.AddDays(-15)), StockOut(products[2], 8, "包装ライン", now.AddDays(-5)),
            StockIn(products[3], 14, "空圧部品棚", now.AddDays(-12)), StockOut(products[3], 10, "搬送装置", now.AddDays(-1)),
            StockIn(products[4], 40, "機械部品棚", now.AddDays(-22)), StockOut(products[4], 17, "予防保全", now.AddDays(-3)),
            StockIn(products[5], 8, "搬送部品棚", now.AddDays(-16)), StockOut(products[5], 6, "検査ライン", now.AddDays(-2)),
            StockIn(products[6], 30, "消耗品棚", now.AddDays(-19)), StockOut(products[6], 14, "定期交換", now.AddDays(-6)),
            StockIn(products[7], 45, "電装部品棚", now.AddDays(-21)), StockOut(products[7], 23, "制御盤修理", now.AddDays(-1)),
            StockIn(products[8], 50, "電装部品棚", now.AddDays(-25)), StockOut(products[8], 27, "保全作業", now.AddDays(-2)),
            StockIn(products[9], 7, "油圧部品棚", now.AddDays(-14)), StockOut(products[9], 5, "プレス機", now.AddDays(-1)),
            StockIn(products[10], 5, "工具室", now.AddDays(-30)), StockOut(products[10], 1, "設備保全班", now.AddDays(-7))
        };

        context.StockTransactions.AddRange(transactions);
        context.SaveChanges();
    }

    private static StockTransaction StockIn(Product product, int quantity, string location, DateTime date) =>
        new()
        {
            ProductId = product.Id,
            TransactionType = TransactionType.In,
            Quantity = quantity,
            LocationOrPurpose = location,
            TransactionDate = date,
            CreatedAt = date
        };

    private static StockTransaction StockOut(Product product, int quantity, string purpose, DateTime date) =>
        new()
        {
            ProductId = product.Id,
            TransactionType = TransactionType.Out,
            Quantity = quantity,
            LocationOrPurpose = purpose,
            TransactionDate = date,
            CreatedAt = date
        };
}

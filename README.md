# Inventory Management System

製造業向けの在庫管理システム MVP です。ASP.NET Core Razor Pages、SQLite、Entity Framework Core、Bootstrap 5、Chart.js を使い、保全部品・消耗品・工具の在庫状況を管理します。

## Features

- ダッシュボード
- 商品マスタ管理
- 入庫登録
- 出庫登録
- 在庫数自動計算
- 安全在庫管理
- 在庫不足アラート
- Chart.js によるカテゴリ別在庫グラフ
- サンプルデータ自動投入

## Tech Stack

- .NET 10
- ASP.NET Core Razor Pages
- SQLite
- Entity Framework Core
- Bootstrap 5
- Chart.js

## Getting Started

```bash
dotnet restore
dotnet build
dotnet run
```

起動後、ブラウザで以下を開きます。

```text
http://localhost:5000
```

初回起動時に `inventory.db` が自動作成され、サンプルデータが投入されます。

## Main Screens

| Screen | Path |
|---|---|
| ダッシュボード | `/` |
| 商品マスタ | `/Products` |
| 在庫一覧 | `/Inventory` |
| 入出庫履歴 | `/Transactions` |
| 入庫登録 | `/StockIn/Create` |
| 出庫登録 | `/StockOut/Create` |
| 在庫不足アラート | `/Alerts` |

## Data Model

### Product

商品コード、商品名、カテゴリ、単位、安全在庫、備考を管理します。

### StockTransaction

商品ごとの入庫・出庫履歴を管理します。

```text
現在庫 = 入庫数量合計 - 出庫数量合計
```

## Sample Categories

- センサー
- PLC部品
- 電磁弁
- シリンダー
- ベアリング
- ベルト
- フィルター
- リレー
- ヒューズ
- 油圧ホース
- 工具

## Notes

この MVP では認証、バーコード、CSV 出力、PDF 出力は対象外です。GitHub ポートフォリオとして基本機能と画面品質を優先しています。

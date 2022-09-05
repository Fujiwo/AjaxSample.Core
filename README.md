# AjaxSample.Core

ASP.NET Core で Ajax 利用して、部分ビューを取得したり、Web API を呼んだりするサンプル。

* 2022年9月5日版
* .NET のバージョン、参照パッケージなどは AjaxSampleCore.csproj を参照のこと。

## 主なソースコード

* wwwroot
** js
*** BooksIndex.js - Books.cshtml 用
* Controllers
** BooksController.cs - Books のコントローラー
** BooksApiController.cs - Books API のコントローラー
* Models - モデル クラス群
** Author.cs
** Publisher.cs
** Book.cs
** BookContext.cs
* ViewModels
** BookViewModel.cs - Part.cshtml 用
* Views
** Books
*** Index.cshtml - 検索画面
*** Part.cshtml - 部分ビュー
* appsettings.json - 接続文字列などの設定
* Program.cs - Main

## 利用方法

1. 最初に Migrations を削除し、コマンドラインで下記を実行

> dotnet ef migrations add InitialCreate

> dotnet ef database update

2. 適宜、接続文字列を変更

appsettings.json

> "DefaultConnection": "Data Source=.\\SQLEXPRESS;Initial Catalog=BulletinBoard;Integrated Security=True;"

3. 初回起動時のみ #define INITIALIZING を有効にすると初期データが生成される
 
BooksController.cs

> #define INITIALIZING

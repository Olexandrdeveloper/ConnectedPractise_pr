using System.Text;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;

namespace ConnectedPractise_pr
{
    internal class Program
    {
        static public SqlConnection conn = null;
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StationeryCompany;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            Menu.MainMenu();
        }
    }
}



//CREATE TABLE [dbo].[Type]
//(
//	[Id] INT IDENTITY NOT NULL PRIMARY KEY,
//	[Name] NVARCHAR(MAX) NOT NULL
//)
//GO
//CREATE TABLE [dbo].[Products]
//(
//	[Id] INT IDENTITY NOT NULL PRIMARY KEY,
//	[Name] NVARCHAR(MAX) NOT NULL,
//	[TypeId] INT NOT NULL REFERENCES [dbo].[Type]([Id]),
//	[Price] DECIMAL(18, 2) NOT NULL,
//    [Quantity] INT NOT NULL
//)
//GO
//CREATE TABLE [dbo].[Manager]
//(
//	[Id] INT IDENTITY NOT NULL PRIMARY KEY,
//	[Name] NVARCHAR(MAX) NOT NULL CHECK (LEN([Name]) > 0),
//	[Surname] NVARCHAR(MAX) NOT NULL CHECK (LEN([Surname]) > 0)
//)
//GO
//CREATE TABLE [dbo].[Sale]
//(
//	[Id] INT IDENTITY NOT NULL PRIMARY KEY,
//	[ProductId] INT NOT NULL REFERENCES [dbo].[Products]([Id]),
//	[ManagerId] INT NOT NULL REFERENCES [dbo].[Manager]([Id]),
//	[Quantity] INT NOT NULL CHECK ([Quantity] > 0),
//	[SaleDate] DATETIME NOT NULL DEFAULT GETDATE(),
//	[Price] DECIMAL(18, 2) NOT NULL,
//	[BuyesCompany] NVARCHAR(MAX) NOT NULL CHECK (LEN([BuyesCompany]) > 0)
//)
//GO



//INSERT INTO [dbo].[Type] ([Name]) VALUES 
//(N'Папір та блокноти'),
//(N'Письмове приладдя'),
//(N'Офісні аксесуари');
//GO

//INSERT INTO [dbo].[Manager] ([Name], [Surname]) VALUES 
//(N'Олександр', N'Петренко'),
//(N'Марія', N'Коваленко'),
//(N'Андрій', N'Шевченко');
//GO

//INSERT INTO [dbo].[Products] ([Name], [TypeId], [Price], [Quantity]) VALUES 
//(N'Папір А4 (пачка 500 арк.)', 1, 180.50, 150),
//(N'Блокнот в клітинку 96 арк.', 1, 45.00, 300),
//(N'Ручка кулькова синя', 2, 12.00, 1000),
//(N'Олівець простий HB', 2, 8.50, 500),
//(N'Степлер офісний №10', 3, 125.00, 45),
//(N'Антистеплер', 3, 35.00, 80);
//GO

//INSERT INTO [dbo].[Sale] ([ProductId], [ManagerId], [Quantity], [SaleDate], [Price], [BuyesCompany]) VALUES 
//(1, 1, 10, GETDATE(), 1805.00, N'ТехноПром'),
//(3, 2, 50, GETDATE(), 600.00, N'Офіс-Майстер'),
//(5, 1, 2, GETDATE(), 250.00, N'ТехноПром'),
//(2, 3, 5, GETDATE(), 225.00, N'Альфа-Банк');
//GO
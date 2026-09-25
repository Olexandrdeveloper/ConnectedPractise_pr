using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ConnectedPractise_pr
{
    internal static class DatabaseOperations
    {
        private static readonly Dictionary<string, string> selectCommands = new Dictionary<string, string>
        {
            { "SELECT_1", "SELECT * FROM Products" },
            { "SELECT_2", "SELECT * FROM Type" },
            { "SELECT_3", "SELECT * FROM Manager" },
            { "SELECT_4", "SELECT TOP 1 * FROM Products ORDER BY Quantity DESC" },
            { "SELECT_5", "SELECT TOP 1 * FROM Products ORDER BY Quantity ASC" },
            { "SELECT_6", "SELECT TOP 1 * FROM Products ORDER BY Price ASC" },
            { "SELECT_7", "SELECT TOP 1 * FROM Products ORDER BY Price DESC" },
            { "SELECT_8", "SELECT * FROM Products WHERE TypeId = @TypeId" },
            { "SELECT_9", "SELECT DISTINCT p.* FROM Products p JOIN Sale s ON p.Id = s.ProductId WHERE s.ManagerId = @ManagerId" },
            { "SELECT_10", "SELECT DISTINCT p.* FROM Products p JOIN Sale s ON p.Id = s.ProductId WHERE s.BuyesCompany = @BuyesCompany" },
            { "SELECT_11", "SELECT TOP 1 * FROM Sale ORDER BY SaleDate DESC" },
            { "SELECT_12", "SELECT TypeId, AVG(CAST(Quantity AS FLOAT)) as AverageQuantity FROM Products GROUP BY TypeId" },
            { "SELECT_13", "SELECT TOP 1 m.* FROM Manager m JOIN Sale s ON m.Id = s.ManagerId GROUP BY m.Id, m.Name, m.Surname ORDER BY SUM(s.Quantity) DESC" },
            { "SELECT_14", "SELECT TOP 1 m.* FROM Manager m JOIN Sale s ON m.Id = s.ManagerId GROUP BY m.Id, m.Name, m.Surname ORDER BY SUM(s.Quantity * s.Price) DESC" },
            { "SELECT_15", "SELECT TOP 1 m.* FROM Manager m JOIN Sale s ON m.Id = s.ManagerId WHERE s.SaleDate BETWEEN @StartDate AND @EndDate GROUP BY m.Id, m.Name, m.Surname ORDER BY SUM(s.Quantity * s.Price) DESC" },
            { "SELECT_16", "SELECT TOP 1 BuyesCompany FROM Sale GROUP BY BuyesCompany ORDER BY SUM(Quantity * Price) DESC" },
            { "SELECT_17", "SELECT TOP 1 t.* FROM Type t JOIN Products p ON t.Id = p.TypeId JOIN Sale s ON p.Id = s.ProductId GROUP BY t.Id, t.Name ORDER BY SUM(s.Quantity) DESC" },
            { "SELECT_18", "SELECT TOP 1 t.* FROM Type t JOIN Products p ON t.Id = p.TypeId JOIN Sale s ON p.Id = s.ProductId GROUP BY t.Id, t.Name ORDER BY SUM(s.Quantity * s.Price) DESC" },
            { "SELECT_19", "SELECT TOP 1 p.Name FROM Products p JOIN Sale s ON p.Id = s.ProductId GROUP BY p.Id, p.Name ORDER BY SUM(s.Quantity) DESC" },
            { "SELECT_20", "SELECT p.Name FROM Products p WHERE p.Id NOT IN (SELECT DISTINCT ProductId FROM Sale WHERE SaleDate >= DATEADD(day, -@Days, GETDATE()))" }
        };

        public static void ConnectToDatabase()
        {
            try
            {
                Program.conn?.Open();
                Console.WriteLine("Успішно");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.ReadKey();
            Console.Clear();
        }

        public static void DisconnectFromDatabase()
        {
            try
            {
                Program.conn?.Close();
                Console.WriteLine("Відключено");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.ReadKey();
            Console.Clear();
        }

        public static void ExecuteSelectCommand(int id)
        {
            string key = $"SELECT_{id}";
            if (!selectCommands.ContainsKey(key)) return;

            Dictionary<string, object> p = null;
            if (id == 8)
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Type", Program.conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++) Console.Write($"{reader.GetName(i)}: {reader[i]} \t");
                            Console.WriteLine();
                        }
                    }
                }
                Console.Write("ID типу: ");
                p = new Dictionary<string, object> { { "@TypeId", int.Parse(Console.ReadLine()) } };
            }
            else if (id == 9)
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Manager", Program.conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++) Console.Write($"{reader.GetName(i)}: {reader[i]} \t");
                            Console.WriteLine();
                        }
                    }
                }
                Console.Write("ID Менеджера: ");
                p = new Dictionary<string, object> { { "@ManagerId", int.Parse(Console.ReadLine()) } };
            }
            else if (id == 10)
            {
                using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT BuyesCompany FROM Sale", Program.conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++) Console.Write($"{reader.GetName(i)}: {reader[i]} \t");
                            Console.WriteLine();
                        }
                    }
                }
                Console.Write("Покупець: ");
                p = new Dictionary<string, object> { { "@BuyesCompany", Console.ReadLine() } };
            }
            else if (id == 15)
            {
                Console.Write("Введіть початкову дату(2026-09-25): ");
                string dateStart = Console.ReadLine();
                Console.Write("Введіть кінцеву дату(2026-09-25): ");
                string dateEnd = Console.ReadLine();
                p = new Dictionary<string, object> { { "@StartDate", dateStart }, { "@EndDate", dateEnd } };
            }
            else if (id == 20)
            {
                Console.WriteLine("Введіть к-сть днів:");
                p = new Dictionary<string, object> { { "@Days", int.Parse(Console.ReadLine()) } };
            }

            Exec(selectCommands[key], p, true);
            Console.ReadKey();
            Console.Clear();
        }

        public static void ExecuteInsertCommand(int choice)
        {
            string q = "";
            var p = new Dictionary<string, object>();

            if (choice == 1)
            {
                Console.Write("Назва: ");
                p.Add("@Name", Console.ReadLine());
                Console.Write("ID типу: ");
                p.Add("@TypeId", int.Parse(Console.ReadLine()));
                Console.Write("Кількість: ");
                p.Add("@Quantity", int.Parse(Console.ReadLine()));
                Console.Write("Ціна: ");
                p.Add("@Price", decimal.Parse(Console.ReadLine()));
                q = "INSERT INTO Products (Name, TypeId, Quantity, Price) VALUES (@Name, @TypeId, @Quantity, @Price)";
            }
            else if (choice == 2)
            {
                Console.Write("Назва типу: ");
                p.Add("@Name", Console.ReadLine());
                q = "INSERT INTO Type (Name) VALUES (@Name)";
            }
            else if (choice == 3)
            {
                Console.Write("Ім'я менеджера: ");
                p.Add("@Name", Console.ReadLine());
                Console.Write("Прізвище менеджера: ");
                p.Add("@Surname", Console.ReadLine());
                q = "INSERT INTO Manager (Name, Surname) VALUES (@Name, @Surname)";
            }

            Exec(q, p, false);
            Console.WriteLine("Додано");
            Console.ReadKey();
            Console.Clear();
        }

        public static void ExecuteUpdateCommand(int choice)
        {
            string q = "";
            var p = new Dictionary<string, object>();

            if (choice == 1)
            {
                Console.Write("ID: ");
                p.Add("@Id", int.Parse(Console.ReadLine()));
                Console.Write("Назва: ");
                p.Add("@Name", Console.ReadLine());
                Console.Write("ID типу: ");
                p.Add("@TypeId", int.Parse(Console.ReadLine()));
                Console.Write("Кількість: ");
                p.Add("@Quantity", int.Parse(Console.ReadLine()));
                Console.Write("Ціна: ");
                p.Add("@Price", decimal.Parse(Console.ReadLine()));
                q = "UPDATE Products SET Name = @Name, TypeId = @TypeId, Quantity = @Quantity, Price = @Price WHERE Id = @Id";
            }
            else if (choice == 2)
            {
                Console.Write("ID: ");
                p.Add("@Id", int.Parse(Console.ReadLine()));
                Console.Write("Назва: ");
                p.Add("@Name", Console.ReadLine());
                q = "UPDATE Type SET Name = @Name WHERE Id = @Id";
            }
            else if (choice == 3)
            {
                Console.Write("ID: ");
                p.Add("@Id", int.Parse(Console.ReadLine()));
                Console.Write("Назва: ");
                p.Add("@Name", Console.ReadLine());
                Console.Write("Прізвище: ");
                p.Add("@Surname", Console.ReadLine());
                q = "UPDATE Manager SET Name = @Name, Surname = @Surname WHERE Id = @Id";
            }

            Exec(q, p, false);
            Console.WriteLine("Оновлено");
            Console.ReadKey();
            Console.Clear();
        }

        public static void ExecuteDeleteCommand(int choice)
        {
            Console.Write("ID: ");
            var p = new Dictionary<string, object> { { "@Id", int.Parse(Console.ReadLine()) } };
            string q = "";
            if (choice == 1) q = "DELETE FROM Products WHERE Id = @Id";
            else if (choice == 2) q = "DELETE FROM Type WHERE Id = @Id";
            else if (choice == 3) q = "DELETE FROM Manager WHERE Id = @Id";

            Exec(q, p, false);
            Console.WriteLine("Видалено");
            Console.ReadKey();
            Console.Clear();
        }

        private static void Exec(string query, Dictionary<string, object> parameters, bool isRead)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, Program.conn))
                {
                    if (parameters != null)
                        foreach (var kv in parameters) cmd.Parameters.AddWithValue(kv.Key, kv.Value);

                    if (isRead)
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++) Console.Write($"{reader.GetName(i)}: {reader[i]} \t");
                                Console.WriteLine();
                            }
                        }
                    }
                    else cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
}



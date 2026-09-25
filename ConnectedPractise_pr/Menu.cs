using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectedPractise_pr
{
    internal static class Menu
    {
        private static bool IsConnected() => Program.conn != null && Program.conn.State == System.Data.ConnectionState.Open;

        public static void MainMenu()
        {
            while (true)
            {
                Console.Write(
                    "====== MENU ======\n" +
                    " 0. Вихід\n" +
                    " 1. Під'єднатись до БД\n" +
                    " 2. Від'єднатись від БД\n" +
                    " 3. Показати\n" +
                    " 4. Вставити\n" +
                    " 5. Оновити\n" +
                    " 6. Видалити\n" +
                    " > ");
                byte choice = byte.TryParse(Console.ReadLine(), out byte result) ? result : (byte)0;
                Console.Clear();

                if (choice == 1)
                {
                    DatabaseOperations.ConnectToDatabase();
                }
                else if (choice == 2)
                {
                    DatabaseOperations.DisconnectFromDatabase();
                }
                else if (choice == 3)
                {
                    if (IsConnected()) SelectMenu();
                    else
                    {
                        Console.WriteLine("Спочатку підключіться");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                else if (choice == 4)
                {
                    if (IsConnected()) InsertMenu();
                    else
                    {
                        Console.WriteLine("Спочатку підключіться");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                else if (choice == 5)
                {
                    if (IsConnected()) UpdateMenu();
                    else
                    {
                        Console.WriteLine("Спочатку підключіться");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                else if (choice == 6)
                {
                    if (IsConnected()) DeleteMenu();
                    else
                    {
                        Console.WriteLine("Спочатку підключіться");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                else if (choice == 0)
                {
                    break;
                }
            }
        }

        private static void SelectMenu()
        {
            while (true)
            {
                Console.Write(
                    "====== SELECT MENU ======\n" +
                    " 0. Назад\n" +
                    " 1. Відображення всієї інформації про канцтовари\n" +
                    " 2. Відображення всіх типів канцтоварів\n" +
                    " 3. Відображення всіх менеджерів з продажу\n" +
                    " 4. Показати канцтовари з максимальною кількістю одиниць\n" +
                    " 5. Показати канцтовари з мінімальною кількістю одиниць\n" +
                    " 6. Показати канцтовари з мінімальною собівартістю одиниці\n" +
                    " 7. Показати канцтовари з максимальною собівартістю одиниці\n" +
                    " 8. Показати канцтовари заданого типу\n" +
                    " 9. Показати канцтовари, які продав певний менеджер з продажу\n" +
                    " 10. Показати канцтовари, які закупила певна фірма-покупець\n" +
                    " 11. Показати інформацію про нещодавній продаж\n" +
                    " 12. Показати середню кількість товарів по кожному типу канцтоварів\n" +
                    " 13. Показати інформацію про менеджера з найбільшою кількістю продажів за кількістю одиниць\n" +
                    " 14. Показати інформацію про менеджера з продажу з найбільшою загальною сумою прибутку\n" +
                    " 15. Показати інформацію про менеджера з продажу з найбільшою загальною сумою прибутку за вказаний проміжок часу\n" +
                    " 16. Показати інформацію про фірму-покупця, яка зробила закупку на найбільшу суму\n" +
                    " 17. Показати інформацію про тип канцтоварів з найбільшою кількістю одиниць продажів\n" +
                    " 18. Показати інформацію про тип найприбутковіших канцтоварів\n" +
                    " 19. Показати назву найпопулярніших канцтоварів за кількістю проданих одиниць\n" +
                    " 20. Показати назву канцтоварів, які не продавалися у задану кількість днів\n" +
                    " > ");
                byte choice = byte.TryParse(Console.ReadLine(), out byte result) ? result : (byte)0;
                Console.Clear();

                if (choice == 0) break;
                else DatabaseOperations.ExecuteSelectCommand(choice);
            }
        }

        private static void InsertMenu()
        {
            while (true)
            {
                Console.Write(
                    "====== INSERT MENU ======\n" +
                    " 0. Назад\n" +
                    " 1. Вставити новий канцтовар\n" +
                    " 2. Вставити новий тип канцтовару\n" +
                    " 3. Вставити нового менеджера\n" +
                    " > ");
                byte choice = byte.TryParse(Console.ReadLine(), out byte result) ? result : (byte)0;
                Console.Clear();

                if (choice == 0) break;
                else DatabaseOperations.ExecuteInsertCommand(choice);
            }
        }

        private static void UpdateMenu()
        {
            while (true)
            {
                Console.Write(
                    "====== UPDATE MENU ======\n" +
                    " 0. Назад\n" +
                    " 1. Оновити інформацію про канцтовар\n" +
                    " 2. Оновити інформацію про тип канцтовару\n" +
                    " 3. Оновити інформацію про менеджера\n" +
                    " > ");
                byte choice = byte.TryParse(Console.ReadLine(), out byte result) ? result : (byte)0;
                Console.Clear();

                if (choice == 0)
                {
                    break;
                }
                else
                {
                    DatabaseOperations.ExecuteUpdateCommand(choice);
                }
            }
        }

        private static void DeleteMenu()
        {
            while (true)
            {
                Console.Write(
                    "====== DELETE MENU ======\n" +
                    " 0. Назад\n" +
                    " 1. Видалити канцтовар\n" +
                    " 2. Видалити тип канцтовару\n" +
                    " 3. Видалити менеджера\n" +
                    " > ");
                byte choice = byte.TryParse(Console.ReadLine(), out byte result) ? result : (byte)0;
                Console.Clear();

                if (choice == 0)
                {
                    break;
                }
                else
                {
                    DatabaseOperations.ExecuteDeleteCommand(choice);
                }
            }
        }
    }
}
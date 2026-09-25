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
                    //" 4. Вставити\n" +
                    //" 5. Оновити\n" +
                    //" 6. Видалити\n" +
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
                //else if (choice == 4)
                //{
                //    if (IsConnected()) InsertMenu();
                //    else
                //    {
                //        Console.WriteLine("Спочатку підключіться");
                //        Console.ReadKey();
                //        Console.Clear();
                //    }
                //}
                //else if (choice == 5)
                //{
                //    if (IsConnected()) UpdateMenu();
                //    else
                //    {
                //        Console.WriteLine("Спочатку підключіться");
                //        Console.ReadKey();
                //        Console.Clear();
                //    }
                //}
                //else if (choice == 6)
                //{
                //    if (IsConnected()) DeleteMenu();
                //    else
                //    {
                //        Console.WriteLine("Спочатку підключіться");
                //        Console.ReadKey();
                //        Console.Clear();
                //    }
                //}
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
                    " > ");
                byte choice = byte.TryParse(Console.ReadLine(), out byte result) ? result : (byte)0;
                Console.Clear();

                if (choice == 0) break;
                else DatabaseOperations.ExecuteSelectCommand(choice);
            }
        }

        //private static void InsertMenu()
        //{
        //    while (true)
        //    {
        //        Console.Write(
        //            "====== INSERT MENU ======\n" +
        //            " 0. Назад\n" +
        //            " 1. Вставити новий товар\n" +
        //            " 2. Вставити новий тип товару\n" +
        //            " 3. Вставити нового постачальника\n" +
        //            " > ");
        //        byte choice = byte.TryParse(Console.ReadLine(), out byte result) ? result : (byte)0;
        //        Console.Clear();

        //        if (choice == 0) break;
        //        else DatabaseOperations.ExecuteInsertCommand(choice);
        //    }
        //}

        //private static void UpdateMenu()
        //{
        //    while (true)
        //    {
        //        Console.Write(
        //            "====== UPDATE MENU ======\n" +
        //            " 0. Назад\n" +
        //            " 1. Оновити інформацію про товар\n" +
        //            " 2. Оновити інформацію про тип товару\n" +
        //            " 3. Оновити інформацію про постачальника\n" +
        //            " > ");
        //        byte choice = byte.TryParse(Console.ReadLine(), out byte result) ? result : (byte)0;
        //        Console.Clear();

        //        if (choice == 0)
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            DatabaseOperations.ExecuteUpdateCommand(choice);
        //        }
        //    }
        //}

        //private static void DeleteMenu()
        //{
        //    while (true)
        //    {
        //        Console.Write(
        //            "====== DELETE MENU ======\n" +
        //            " 0. Назад\n" +
        //            " 1. Видалити товар\n" +
        //            " 2. Видалити тип товару\n" +
        //            " 3. Видалити постачальника\n" +
        //            " > ");
        //        byte choice = byte.TryParse(Console.ReadLine(), out byte result) ? result : (byte)0;
        //        Console.Clear();

        //        if (choice == 0)
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            DatabaseOperations.ExecuteDeleteCommand(choice);
        //        }
        //    }
        //}
    }
}
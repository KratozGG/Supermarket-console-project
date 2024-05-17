using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SuperMartketapp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Welcome, What do you want to do today?\n1 for Adding items to the market\n2 for Updating item prices\n3 for Deleting items from the market\n4 for Displaying all the items\nEnter the option : ");
                var choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 1)
                {
                    Console.WriteLine("Enter the item name : ");
                    string a = Console.ReadLine();
                    Console.WriteLine("Enter the item price : ");
                    int b = Convert.ToInt32(Console.ReadLine());
                    InsertCommand(a, b);
                }
                else if (choice == 2)
                {
                    Console.WriteLine("Enter the item name : ");
                    string a = Console.ReadLine();
                    Console.WriteLine("Enter the item price : ");
                    int b = Convert.ToInt32(Console.ReadLine());
                    UpdateCommand(a, b);
                }
                else if (choice == 3)
                {
                    Console.WriteLine("Enter the item name : ");
                    string a = Console.ReadLine();
                    DeleteCommand(a);
                }
                else if (choice == 4)
                {
                    DisplayCommand();
                }
                else
                {
                    Console.WriteLine("Invalid operation");
                }
                Console.WriteLine("Continue? y/n : ");
                string yesOrno = Console.ReadLine();
                if (yesOrno == "n")
                {
                    Console.WriteLine("Shutting down!");
                    break;
                }
            }
            
        }
        static void InsertCommand(string ItemName, int ItemPrice)
        {
            DBAccess Objdbaccess = new DBAccess();
            SqlCommand insertCommand = new SqlCommand("insert into marketproducts(name, price) values(@ItemName, @ItemPrice)");
            insertCommand.Parameters.AddWithValue("@ItemName", ItemName);
            insertCommand.Parameters.AddWithValue("@ItemPrice", ItemPrice);
            Objdbaccess.executeQuery(insertCommand);
            Console.WriteLine($"Added {ItemName} for {ItemPrice} successfully");
        }
        static void UpdateCommand(string ItemName, int ItemPrice)
        {
            DBAccess dbaccess = new DBAccess();
            SqlCommand updateCommand = new SqlCommand("update marketproducts set price = @ItemPrice where name = @ItemName");
            updateCommand.Parameters.AddWithValue("@ItemName", ItemName);
            updateCommand.Parameters.AddWithValue("@ItemPrice", ItemPrice);
            dbaccess.executeQuery(updateCommand);
            Console.WriteLine($"Updated the Price for {ItemName}");
        }
        static void DeleteCommand(string ItemName)
        {
            DBAccess dbaccess = new DBAccess();
            SqlCommand deleteCommand = new SqlCommand("delete from marketproducts where name = @Itemname");
            deleteCommand.Parameters.AddWithValue("@ItemName", ItemName);
            dbaccess.executeQuery(deleteCommand);
            Console.WriteLine($"Deleted the item {ItemName}");
        }
        static void DisplayCommand()
        {
            DBAccess dbaccess = new DBAccess();
            DataTable table = dbaccess.GetAllItems("marketproducts");

            if (table.Rows.Count > 0)
            {
                // Displaying column names
                foreach (DataColumn column in table.Columns)
                {
                    Console.Write($"{column.ColumnName}\t");
                }
                Console.WriteLine();

                // Displaying data
                foreach (DataRow row in table.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        Console.Write($"{item}\t");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No items found.");

            }
        }
    }
}

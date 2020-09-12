using System;
using System.Collections.Generic;
using TestAppLibrary;

namespace DbGenerator
{
    class Program
    {
        
        static void Main(string[] args)
        {

            HelperUtility utility = new HelperUtility();


            //Dictionary<string, string> tableDetails = new Dictionary<string, string>();

            //Console.WriteLine("\t\tCreate Table");

            //Console.WriteLine("\n\n\nEnter Table Name");
            //var table_Name = Console.ReadLine();
            //Console.WriteLine("\n\t\t Enter Column num");
            //int cn = Convert.ToInt32(Console.ReadLine());


            //for (var i = 0; i < cn; i++)
            //{
            //    Console.WriteLine("\n\nEnter " + i + "Column name");
            //    Console.WriteLine("Enter " + i + "DATATYPE(size)\n\n");
            //    tableDetails.Add(Console.ReadLine(), Console.ReadLine());
            //}


            //tableDetails.Add("Name1", "VARCHAR(64)");
            //tableDetails.Add("Age1", "VARCHAR(64)");
            //tableDetails.Add("Name2", "VARCHAR(64)");
            //tableDetails.Add("Name22", "VARCHAR(64)");
            //tableDetails.Add("Name222", "VARCHAR(64)");
            //tableDetails.Add("Name2222222222", "VARCHAR(64)");
            //tableDetails.Add("Age12", "VARCHAR(64)");
            //tableDetails.Add("Age122", "VARCHAR(64)");
            //tableDetails.Add("Age2121", "VARCHAR(64)");
            //tableDetails.Add("Age12121212", "VARCHAR(64)");

            //utility.Table(tableDetails, table_Name);


            Console.WriteLine("\t\t All Tables\n");
          
            var tables=utility.GetAllTableNames();


            foreach(var item in tables)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\n\n\n\t Select a Table");

            var tableName = Console.ReadLine();
            var table=utility.GetTableSchema(tableName);
            foreach (var item in table)
            {
                Console.WriteLine(item.Key+" "+item.Value);
            }

            Console.WriteLine("\n\n\n\t\t\t\t Insert data to "+tableName);

            List<string> data = new List<string>();
            foreach (var item in table.Keys)
            {
                Console.WriteLine("Enter "+item+"\n\n");
                data.Add(Console.ReadLine());
            }

            utility.InsertData(tableName,data);
            Console.WriteLine("Inserted");
            Console.WriteLine("Enter the  table Name to See the data");
            var tbl = Console.ReadLine();
            
            var Tdata=utility.getTable(tbl);
            foreach(var item in Tdata)
            {
                Console.Write(item+"   ");
            }
           

            Console.ReadKey();
            Console.ReadKey();
        }
    }
}

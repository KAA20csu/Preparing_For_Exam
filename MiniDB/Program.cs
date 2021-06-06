using System;
using Newtonsoft.Json;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;

namespace MiniDB
{
    class Program
    {
        private static string TbName { get; set; }
        private static string[] Columns { get; set; }
        private static List<Table> Tables { get; set; } = new List<Table>();
        static void Main()
        {
            Greets();
            
            Column clmn = null;
            Dictionary<string, string> clmnProperties = new Dictionary<string, string>();
            foreach (string currentColumn in Columns)
            {
                clmnProperties.Add(currentColumn, null);
                clmn = new Column(clmnProperties);
            }
            var mainTb = new Table(TbName, clmn);
            Tables.Add(mainTb);
            IsRefererence(clmn, mainTb);
            
        }
        static void IsRefererence(Column clmn, Table tb)
        {
            Console.WriteLine("Введите имя таблицы и колонки через пробел, на которую ссылается колонка, если ссылка предусматривается:");
            string[] ifReference = Console.ReadLine().Split(" ");
            if (ifReference[0] != "")
            {
                if (clmn.Columns.ContainsKey(ifReference[1]))
                {
                    clmn.Columns[ifReference[1]] = ifReference[0];
                    Main();
                }
            }
            else
            {
                string json = "";
                foreach(var tabl in Tables)
                {
                    json += JsonConvert.SerializeObject(tabl, Formatting.Indented);
                }
                File.WriteAllText("fake.txt",json);
            }
        }
        static void Greets()
        {
            Console.WriteLine("Введите имя создаваемой таблицы:");
            TbName = Console.ReadLine();
            Console.WriteLine("Введите имена столбцов с запятыми-разделителями без пробелов:");
            Columns = Console.ReadLine().Split(",");
        }
    }
    class Table
    {
        public string TableName { get; set; }
        public Column ColumnProperts { get; set; }
        public Table(string name, Column properts)
        {
            ColumnProperts = properts;
            TableName = name;
        }
    }
    class Column
    {
        public Dictionary<string, string> Columns { get; set; }
        public Column(Dictionary<string, string> columns)
        {
            Columns = columns;
        }
    }
}

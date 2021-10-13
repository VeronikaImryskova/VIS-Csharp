using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace VIS_Csharp
{
    class Program
    {
        public static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\veron\source\repos\VIS-Csharp\VIS-Csharp\Database2.mdf;Integrated Security=True";


        void TableData()
        {
            TableDataGateway gate = new TableDataGateway(connectionString);
            //DataTable table = gate.FindLibrarian(1);
            //bool gone = gate.Delete(5);

            //gate.Insert(1, "Thomas", "FirstTry", 25000);
        }

        void RowData()
        {
            RowDataGatewayFinder find = new RowDataGatewayFinder(connectionString);
            RowDataGateway row = find.Find(2);
            row.LastName = "Portuquese";

            row.Update();

            RowDataGateway row2 = new RowDataGateway(connectionString)
            {
                FirstName = "Try",
                LastName = "This",
                Pay = 30000
            };
            // row2.Insert();
            // row2 = find.Find(7);
            // row2.Delete();
        }

        void Active()
        {
            ActiveFind activeFind = new ActiveFind(connectionString);
            ActiveRecord active = activeFind.Find(3);
            int years = active.YearsWorked;
            Console.WriteLine($"{years}");
        }

        void Mapper()
        {
            DataMapper mapper = new DataMapper(connectionString);

            Librarians librarian = mapper.Create(new Librarians { FirstName = "Mapper", LastName = "Data", Pay = 20000 }).Result;
            Librarians librarian2 = mapper.Get(1).Result;

            bool gone = mapper.Delete(10).Result;
        }

        static void Main(string[] args)
        {
            Program program = new Program();

            //program.TableData();

            program.RowData();

            //program.Active();

            //program.Mapper();

            Console.WriteLine();
        }
    }
}
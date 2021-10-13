using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VIS_Csharp
{
    class TableDataGateway
    {
        private const string Get_Id = "SELECT * FROM [TABLE] WHERE ID = @Id;";
        private const string Delete_Id = "DELETE * FROM [TABLE] WHERE ID = @Id;";
        private const string Insert_Id = "INSERT INTO [TABLE] VALUES(@Id, @FirstName, @LastName, @Pay)";
        private readonly string connect;

        public TableDataGateway(string _connect)
        {
            connect = _connect;
        }

	    public DataTable FindLibrarian(int Id)
        {
            using (SqlConnection connection = new SqlConnection(connect))
            {
                connection.Open();

                SqlDataAdapter ad = new SqlDataAdapter();
                DataTable table = new DataTable();

                SqlCommand command = new SqlCommand(Get_Id, connection);
                command.Parameters.AddWithValue("Id", Id);

                ad.SelectCommand = command;
                ad.Fill(table);

                return table;
            }
        }

        public void Insert(int ID, string FirstName, string LastName, int pay)
        {
            using (SqlConnection connection = new SqlConnection(connect))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(Insert_Id, connection);
                command.Parameters.AddWithValue("Id", ID);
                command.Parameters.AddWithValue("FirstName", FirstName);
                command.Parameters.AddWithValue("LastName", LastName);
                command.Parameters.AddWithValue("Pay", pay);

                command.ExecuteNonQuery();
            }
        }

        public bool Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(connect))
            {
                bool success = false;

                connection.Open();

                SqlCommand command = new SqlCommand(Delete_Id, connection);
                command.Parameters.AddWithValue("Id", Id);

                success = command.ExecuteNonQuery() > 0;

                return success;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIS_Csharp
{
    public class RowDataGatewayFinder
    {
        private readonly string connect;

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Pay { get; set; }

        public RowDataGatewayFinder(string _connect)
        {
            connect = _connect;
        }

        public RowDataGateway Find(int id)
        {
            using (SqlConnection connection = new SqlConnection(connect))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM [TABLE] WHERE ID = @Id";

                command.Parameters.AddWithValue("Id", id);

                DataTable table = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter(command);
                ad.Fill(table);

                DataRow AllData = table.Rows[0];

                return new RowDataGateway(connect)
                {
                    Id = id,
                    FirstName = AllData.Field<string>("FirstName"),
                    LastName = AllData.Field<string>("LastName"),
                    Pay = AllData.Field<int>("Pay")
                };
            }
        }
    }

    public class RowDataGateway
    {
        private readonly string connect;

        public RowDataGateway(string _connect)
        {
            connect = _connect;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Pay { get; set; }

        public void Insert()
        {
            using (SqlConnection connection = new SqlConnection(connect))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO [TABLE](FirstName, LastName, Pay) VALUES (@FirstName, @LastName, @Pay)";

                command.Parameters.AddWithValue("FirstName", FirstName);
                command.Parameters.AddWithValue("LastName", LastName);
                command.Parameters.AddWithValue("Pay", Pay);

                Id = (int)command.ExecuteNonQuery();
            }
        }

        public void Update()
        {
            using (SqlConnection connection = new SqlConnection(connect))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE [TABLE] SET FIRSTNAME = @FirstName, LASTNAME = @LastName, PAY = @Pay WHERE ID = @Id";
                command.Parameters.AddWithValue("FirstName", FirstName);
                command.Parameters.AddWithValue("LastName", LastName);
                command.Parameters.AddWithValue("Pay", Pay);
                command.Parameters.AddWithValue("Id", Id);

                command.ExecuteNonQuery();
            }
        }

        public bool Delete()
        {
            using (SqlConnection connection = new SqlConnection(connect))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM [TABLE] WHERE ID = @Id";

                command.Parameters.AddWithValue("Id", Id);

                return command.ExecuteNonQuery() > 0;
            }
        }

    }
}

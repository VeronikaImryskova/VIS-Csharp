using System.Data;
using System.Data.SqlClient;

namespace VIS_Csharp
{
    public class ActiveFind
    {
        private readonly string connect;

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Pay { get; set; }

        public ActiveFind(string _connect)
        {
            connect = _connect;
        }

        public ActiveRecord Find(int id)
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

                return new ActiveRecord(connect)
                {
                    Id = id,
                    FirstName = AllData.Field<string>("FirstName"),
                    LastName = AllData.Field<string>("LastName"),
                    Pay = AllData.Field<int>("Pay")
                };
            }
        }
    }

    public class ActiveRecord
    {
        private readonly string connect;

        public ActiveRecord(string _connect)
        {
            connect = _connect;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Pay { get; set; }

        public int YearsWorked
        {
            get
            {
                switch (Pay)
                {
                    case 20000:
                        return 1;
                    case 25000:
                        return 3;
                    case 30000:
                        return 5;
                    default:
                        return 1;
                }
            }
        }

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
                command.CommandText = "UPDATE [TABLE] FIRSTNAME = @FirstName, LASTNAME = @LastName, PAY = @Pay WHERE ID = @Id";
                command.Parameters.AddWithValue("Id", Id);
                command.Parameters.AddWithValue("FirstName", FirstName);
                command.Parameters.AddWithValue("LastName", LastName);
                command.Parameters.AddWithValue("Pay", Pay);

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
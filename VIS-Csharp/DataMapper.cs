using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIS_Csharp
{
    public class DataMapper : InterfaceDataMapper<Librarians>
    {

        public readonly string connect;

        public DataMapper(string _connect)
        {
            connect = _connect;
        }

        public async Task<Librarians> Create(Librarians librarian)
        {
            using (SqlConnection connection = new SqlConnection(connect))
            {
                await connection.OpenAsync();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO [TABLE](FirstName, LastName, Pay) OUTPUT INSERTED.ID VALUES(@FirstName, @LastName, @Pay)";
                command.Parameters.AddWithValue("FirstName", librarian.FirstName);
                command.Parameters.AddWithValue("LastName", librarian.LastName);
                command.Parameters.AddWithValue("Pay", librarian.Pay);

                int id = (int)await command.ExecuteScalarAsync();

                librarian.Id = id;

                return librarian;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connect))
            {
                await connection.OpenAsync();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM [TABLE] WHERE ID = @Id";
                command.Parameters.AddWithValue("Id", id);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<Librarians> Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(connect))
            {
                await connection.OpenAsync();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM [TABLE] WHERE ID = @Id";
                command.Parameters.AddWithValue("Id", id);
                

                SqlDataReader read = await command.ExecuteReaderAsync();

                if (await read.ReadAsync())
                {
                    return new Librarians
                    {
                        Id = id,
                        FirstName = read.GetString(read.GetOrdinal("FirstName")),
                        LastName = read.GetString(read.GetOrdinal("LastName")),
                        Pay = read.GetFieldValue<int>(read.GetOrdinal("Pay"))
                    };
                }
                else
                {
                    return null;
                }          
            }
        }

        public async Task<Librarians> Update(int id, Librarians librarian)
        {
            using (SqlConnection connection = new SqlConnection(connect))
            {
                await connection.OpenAsync();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE [TABLE] SET FIRSTNAME = @FirstName, LASTNAME = @LastName, PAY = @Pay WHERE ID = @Id";
                command.Parameters.AddWithValue("Id", librarian.Id);
                command.Parameters.AddWithValue("FirstName", librarian.FirstName);
                command.Parameters.AddWithValue("LastName", librarian.LastName);
                command.Parameters.AddWithValue("Pay", librarian.Pay);

                await command.ExecuteNonQueryAsync();

                librarian.Id = id;

                return librarian;
            }
        }
    }
}

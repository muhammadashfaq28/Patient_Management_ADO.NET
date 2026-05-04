using ADO.NET.Data;
using ADO.NET.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace ADO.NET.Repositories.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public AuthRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> ValidateUser(string userName, string password)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand(@"
            SELECT COUNT(1) 
            FROM Users 
            WHERE UserName = @UserName AND Password = @Password", connection);

            command.Parameters.AddWithValue("@UserName", userName);
            command.Parameters.AddWithValue("@Password", password); // temp (no hashing yet)

            await connection.OpenAsync();

            var result = (int)await command.ExecuteScalarAsync();

            return result > 0;
        }
    }
}

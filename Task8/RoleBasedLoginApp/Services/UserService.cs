using MySqlConnector;
using RoleBasedLoginApp.Models;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace RoleBasedLoginApp.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        public UserService(IConfiguration config) => _config = config;
        private string ConnStr => _config.GetConnectionString("DefaultConnection")!;

        public async Task<UserModel?> ValidateUserAsync(string username, string password)
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT username, password, role FROM users WHERE username=@u LIMIT 1;";
            cmd.Parameters.AddWithValue("@u", username);
            await using var rdr = await cmd.ExecuteReaderAsync();
            if (!await rdr.ReadAsync()) return null;
            var dbPass = rdr.GetString("password");
            if (dbPass != password) return null;
            return new UserModel
            {
                Username = rdr.GetString("username"),
                Password = dbPass,
                Role = rdr.GetString("role")
            };
        }
    }
}


namespace RoleBasedLoginApp.Models
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; } // replace with hashed in production
        public string Role { get; set; }
    }
}

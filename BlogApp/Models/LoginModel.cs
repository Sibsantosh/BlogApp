namespace BlogApp.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string password { get; set; }

        public override string? ToString()
        {
            return $"id = {Email} password = {password}";
        }
    }
}

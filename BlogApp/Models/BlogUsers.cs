namespace BlogApp.Models
{
    public class BlogUsers
    {
        
            public int User_id { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public int Role { get; set; }
            public long Registration_epoch { get; set; }

        public override string? ToString()
        {
            return $"userid = {User_id} username = {Username} email = {Email} passord = {Password} Role = {Role} Registration EPoch = {Registration_epoch}";
        }

        // Additional properties or methods can be added here as needed

    }
    
}

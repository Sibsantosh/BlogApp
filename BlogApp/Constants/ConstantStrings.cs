using BlogApp.Models;

namespace BlogApp.Constants
{
    public class ConstantStrings
    {
        public static string GetConnectionString() => @"Server=localhost\SQLEXPRESS;Database=blog_database;Trusted_Connection=True;Encrypt=false;"; 
        public static string fetchAllUsers() => @"select * from BlogUsers;";
        public static string fetchRecentPost(int number) => $@"SELECT * FROM Posts ORDER BY post_id DESC OFFSET 0 ROWS FETCH NEXT {number} ROWS ONLY;";
        public static string fetchYourPost(int uid) => $@"select * from Posts where author_id = {uid};";
        public static string LoginUser(LoginModel loginModel) => $@"SELECT * FROM BlogUsers WHERE Email = '{loginModel.Email}'";
        public static string RegisterUser(BlogUsers userModel) => $@"INSERT INTO BlogUsers (username, email, password, role, registration_epoch) VALUES ('{userModel.Username}', '{userModel.Email}', '{userModel.Password}', 1, {userModel.Registration_epoch})";
        public static string CheckUserExists(BlogUsers model) => $@"SELECT * FROM BlogUsers WHERE email = '{model.Email}'";
        public static string getUserById(int id) => $@"SELECT * FROM BlogUsers WHERE user_id = {id};";
    }
}

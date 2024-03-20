using BlogApp.Models;

namespace BlogApp.Constants
{
    public class ConstantStrings
    {
        public static string GetConnectionString() => @"Server=localhost\SQLEXPRESS;Database=blog_database;Trusted_Connection=True;Encrypt=false;"; 
        public static string fetchAllUsers() => @"select * from BlogUsers;";
        public static string DeleteUser(int id) => $@"UPDATE BlogUsers set role = 4 where user_id={id};";
        public static string UpdateUsers(BlogUsers user) => $@"UPDATE BlogUsers SET username = '{user.Username}', email = '{user.Email}', role = {user.Role} WHERE user_id = {user.User_id};";
        public static string fetchRecentPost(int number) => $@"SELECT * FROM Posts ORDER BY post_id DESC OFFSET 0 ROWS FETCH NEXT {number} ROWS ONLY;";
        public static string fetchAllPost() => $@"SELECT * FROM Posts;";
        public static string fetchYourPost(int uid) => $@"select * from Posts where author_id = {uid};";
        public static string fetchSinglePost(int id) => $@"select * from Posts where post_id = {id};";
        public static string LoginUser(LoginModel loginModel) => $@"SELECT * FROM BlogUsers WHERE Email = '{loginModel.Email}'";
        public static string RegisterUser(BlogUsers userModel) => $@"INSERT INTO BlogUsers (username, email, password, role, registration_epoch) VALUES ('{userModel.Username}', '{userModel.Email}', '{userModel.Password}', 1, {userModel.Registration_epoch})";
        public static string CheckUserExists(BlogUsers model) => $@"SELECT * FROM BlogUsers WHERE email = '{model.Email}'";
        public static string getUserById(int id) => $@"SELECT * FROM BlogUsers WHERE user_id = {id};";
        public static string storeNewPost(Posts posts) => $@"INSERT INTO Posts (title, content, author_id, publication_epoch, last_updated_epoch, status,picture_data) VALUES ('{posts.Title}', '{posts.Content}', {posts.Author_id}, {posts.Publication_epoch}, {posts.Last_updated_epoch}, 1,{posts.Picture_data});";
        public static string UpdatePost(Posts posts) => $@"UPDATE Posts SET  title = '{posts.Title}',content = '{posts.Content}',last_updated_epoch={posts.Last_updated_epoch}  WHERE post_id = {posts.Post_id};";
        public static string DeletePost(int id) => $@"DELETE FROM Posts WHERE post_id = {id};";
        public static string FetchAllComments(int id) => $@"SELECT * FROM Comments WHERE post_id = {id};";
        public static string DeleteSpecificComment(int id) => $@"DELETE FROM Comments WHERE comment_id = {id};";
        public static string EditSpecificComment(CommentsModel model) => $@"UPDATE Comments set content = '{model.content}' WHERE  comment_id = {model.comment_id};";
        public static string fetchSingleComment(int id) => $@"select * from Comments where comment_id = {id};";
        public static string AddComment(CommentsModel model) => $@"INSERT INTO Comments (post_id, user_id, content, comment_epoch, user_name) VALUES({model.post_id}, {model.user_id}, '{model.content}', {model.comment_epoch},'{model.user_name}');";
    }
}

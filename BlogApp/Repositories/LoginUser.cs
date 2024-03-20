using Dapper;
using BlogApp.Contracts;
using BlogApp.Models;
using System.Data;
using BlogApp.Constants;

namespace RazorLearning.Repositories
{
    public class LoginUser : Ilogin
    {
        private readonly IDbConnection dbConnection;
        public LoginUser(IDatabaseConnection connection)
        {
            dbConnection = connection.connectDatabase();        
        }

        public async Task<BlogUsers> MakeUserLogin(LoginModel loginModel)
        {
            string loginQuery =ConstantStrings.LoginUser(loginModel);
            var data = await dbConnection.QueryAsync<BlogUsers>(loginQuery);
            var result = data.FirstOrDefault();
            return result;

        }
    }
}

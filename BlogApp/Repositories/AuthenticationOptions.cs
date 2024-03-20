using BlogApp.Constants;
using BlogApp.Contracts;
using BlogApp.Models;
using System.Data;
using Dapper;
using System.Data.Common;

namespace BlogApp.Repositories
{
    public class AuthenticationOptions : IAuthenticationOption
    {
        private readonly IDbConnection _connection;
        public AuthenticationOptions(IDatabaseConnection connection)
        {
            _connection = connection.connectDatabase();
        }
        private void CheckConnection()
        {
            if (_connection.State == ConnectionState.Closed)
            {

                _connection.Open();
            }
        }
        public async Task<BlogUsers> MakeUserLogin(LoginModel loginModel)
        {
            CheckConnection();
            string loginQuery = ConstantStrings.LoginUser(loginModel);
            var data = await _connection.QueryAsync<BlogUsers>(loginQuery);
            var result = data.FirstOrDefault();
            return result;

        }

        public async Task<int> RegisterUser(BlogUsers model)
        {
            CheckConnection();
            string registerQuery = ConstantStrings.RegisterUser(model);
            var data = await _connection.ExecuteAsync(registerQuery);
            return data;
        }
    }
}

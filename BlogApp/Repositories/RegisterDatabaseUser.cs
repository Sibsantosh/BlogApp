using Dapper;
using BlogApp.Contracts;
using BlogApp.Models;
using System.Data;
using BlogApp.Constants;

namespace RazorLearning.Repositories
{
    public class RegisterDatabaseUser : IRegisterUser
    {
        private readonly IDbConnection _connection;
        public RegisterDatabaseUser(IDatabaseConnection connection)
        {
            _connection = connection.connectDatabase();
        }

        public async Task<int> RegisterUser(BlogUsers model)
        {
            string registerQuery = ConstantStrings.RegisterUser(model);
            var data = await _connection.ExecuteAsync(registerQuery);
            return data;


        }
    }
}

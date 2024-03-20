using Dapper;
using BlogApp.Contracts;
using BlogApp.Models;
using System.Data;
using System.Data.Common;
using BlogApp.Constants;

namespace RazorLearning.Repositories
{
    public class CheckUserExist : ICheckUserExists
    {
        private readonly IDbConnection _connection;
        public CheckUserExist(IDatabaseConnection connection)
        {
            _connection = connection.connectDatabase();   
        }

        public async Task<BlogUsers> checkUserExist(BlogUsers model)
        {
            string query = ConstantStrings.CheckUserExists(model);
            var data = await _connection.QueryAsync<BlogUsers>(query);
            var result = data.FirstOrDefault();
            return result;
        }
    }
}

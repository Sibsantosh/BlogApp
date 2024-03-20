using BlogApp.Constants;
using BlogApp.Contracts;
using BlogApp.Models;
using System.Data;
using System.Data.Common;
using Dapper;

namespace BlogApp.Repositories
{
    public class UpdateOptions : IUpdateOptions
    {
        private readonly IDbConnection _connection;
        public UpdateOptions(IDatabaseConnection connection)
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

        public async Task<int> updatePost(Posts posts)
        {
            CheckConnection();
            string updatequery = ConstantStrings.UpdatePost(posts);
            return await _connection.ExecuteAsync(updatequery);

        }

        public Task<int> UpdateTheComment(CommentsModel comments)
        {
            CheckConnection() ;
            throw new NotImplementedException();
        }

        public async Task<int> UpdateUserInDB(BlogUsers model)
        {
            CheckConnection();
            string updateUserQuery = ConstantStrings.UpdateUsers(model);
            var result = await _connection.ExecuteAsync(updateUserQuery);
            return result;
        }

        public Task<int> UpdateComment(CommentsModel model)
        {
            CheckConnection();
            throw new NotImplementedException();
        }
    }
}

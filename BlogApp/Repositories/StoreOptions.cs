using Dapper;
using BlogApp.Contracts;
using BlogApp.Constants;
using BlogApp.Models;
using System.Data;
namespace BlogApp.Repositories
{
    public class StoreOptions : IStoreOptions
    {
        private readonly IDbConnection _connection;
        public StoreOptions(IDatabaseConnection connection)
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
        public async Task<int> StoreComment(CommentsModel model)
        {
            CheckConnection();
            string addCommentQuery = ConstantStrings.AddComment(model);
            var result = await _connection.ExecuteAsync(addCommentQuery);
            return result;
        }
        public async Task<int> StoreNewProject(Posts postModel)
        {
            CheckConnection();
            string postQuery = ConstantStrings.storeNewPost(postModel);
            var result = await _connection.ExecuteAsync(postQuery);
            return result;
        }
        
    }
}

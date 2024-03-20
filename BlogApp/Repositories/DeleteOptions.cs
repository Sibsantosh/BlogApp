using BlogApp.Constants;
using BlogApp.Contracts;
using Dapper;
using System.Data;

namespace BlogApp.Repositories
{
    public class DeleteOptions : IDeleteOptions
    {
        private readonly IDbConnection _connection;

        public DeleteOptions(IDatabaseConnection connection)
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
        public async Task<int> DeleteTheComment(int id)
        {
            CheckConnection();
            string deleteCommentQuery = ConstantStrings.DeleteSpecificComment(id);
            var result = await _connection.ExecuteAsync(deleteCommentQuery);
            return result;
        }


        public async Task<int> DeletePostFromDb(int id)
        {
            CheckConnection();
            string postDeleteString = ConstantStrings.DeletePost(id);
            var result = await _connection.ExecuteAsync(postDeleteString);
            return result;
        }


        public async Task<int> DeleteUser(int id)
        {
            CheckConnection();
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
            string deleteQuery = ConstantStrings.DeleteUser(id);
            var rowAffected = await _connection.ExecuteAsync(deleteQuery);
            return rowAffected;
        }

    }
}

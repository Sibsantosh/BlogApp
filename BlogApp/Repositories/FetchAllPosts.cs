using BlogApp.Constants;
using BlogApp.Contracts;
using BlogApp.Models;
using Dapper;
using System.Data;

namespace BlogApp.Repositories
{
    public class FetchAllPosts : IFetchRecentPosts
    {
        private readonly IDbConnection _connection;
        private FetchPostsResponseModel _responseModel;
        public FetchAllPosts(IDatabaseConnection conneciton, FetchPostsResponseModel responseModel)
        {
            _connection = conneciton.connectDatabase();
            _responseModel = responseModel;
        }

        public async Task<FetchPostsResponseModel> fetchRecentPost()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
            string fetchRecentPost = ConstantStrings.fetchRecentPost(6);
            var result = await _connection.QueryAsync<Posts>(fetchRecentPost);
            _responseModel.postsList = result.ToList();
            
            
            return _responseModel;
            
        }

       
    }
}

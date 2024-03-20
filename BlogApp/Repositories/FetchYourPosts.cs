using BlogApp.Constants;
using BlogApp.Models;
using System.Data.Common;
using System.Data;
using Dapper;
using BlogApp.Contracts;

namespace BlogApp.Repositories
{
    public class FetchYourPosts:IFetchYourPosts
    {
        private IDbConnection _connection;
        private FetchYourPostResponseModel _responseModel;
        public FetchYourPosts(IDatabaseConnection connection, FetchYourPostResponseModel responseModel)
        {
            _connection = connection.connectDatabase();
            _responseModel = responseModel;
            
        }
        public async Task<FetchYourPostResponseModel> fetchYourPosts()
        {
            
            var uid =  Program.authenticatedUser.User_id;
            string fetchRecentPost = ConstantStrings.fetchYourPost(uid);
            var result = await _connection.QueryAsync<Posts>(fetchRecentPost);
            _responseModel.postsList = result.ToList();
            


            return _responseModel;
        }
    }
}

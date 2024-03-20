using BlogApp.Constants;
using BlogApp.Contracts;
using BlogApp.Models;
using Dapper;
using System.Data;
using System.Data.Common;

namespace BlogApp.Repositories
{
    public class FetchOptions : IFetchOptions
    {
        private readonly IDbConnection _connection;
        private CommentResponseModel _commentResponseModel;
        private FetchPostsResponseModel _fetchPostResponseModel;
        FetchAllUsersResponseModel _fetchAllUsersResponseModel;
        private FetchYourPostResponseModel _fetchYourPostsResponseModel;
        public FetchOptions(IDatabaseConnection connection, CommentResponseModel commentResponseModel, FetchPostsResponseModel fetchPostsResponseModel, FetchAllUsersResponseModel fetchAllUsersResponseModel, FetchYourPostResponseModel fetchYourPostsResponseModel)
        {
            _connection = connection.connectDatabase();
            _commentResponseModel = commentResponseModel;
            _fetchPostResponseModel = fetchPostsResponseModel;
            _fetchAllUsersResponseModel = fetchAllUsersResponseModel;
            _fetchYourPostsResponseModel = fetchYourPostsResponseModel;
        }
        private void CheckConnection()
        {
            if (_connection.State == ConnectionState.Closed) {

                _connection.Open();
            }
        }
        public async Task<CommentResponseModel> FetchAllCommentFromDb(int post_id)
        {
            CheckConnection();
            string fetchAllCommentQuery = ConstantStrings.FetchAllComments(post_id);
            var result = await _connection.QueryAsync<CommentsModel>(fetchAllCommentQuery);
            _commentResponseModel.commentList = result.ToList();
            return _commentResponseModel;

        }

        public async Task<FetchPostsResponseModel> fetchRecentPost()
        {
            CheckConnection();
            string fetchRecentPost = ConstantStrings.fetchRecentPost(6);
            var result = await _connection.QueryAsync<Posts>(fetchRecentPost);
            _fetchPostResponseModel.postsList = result.ToList();


            return _fetchPostResponseModel;

        }

        public async Task<FetchPostsResponseModel> fetchAllPost()
        {
            CheckConnection();
            string fetchRecentPost = ConstantStrings.fetchAllPost();
            var result = await _connection.QueryAsync<Posts>(fetchRecentPost);
            _fetchPostResponseModel.postsList = result.ToList();


            return _fetchPostResponseModel;

        }
        public async Task<FetchAllUsersResponseModel> fetchAllUsers()
        {
            CheckConnection();
            string fetchAllUsersQuery = ConstantStrings.fetchAllUsers();
            var result = await _connection.QueryAsync<BlogUsers>(fetchAllUsersQuery);
            var list = result.ToList();
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            _fetchAllUsersResponseModel.usersList = list;
            return _fetchAllUsersResponseModel;

        }

        public async Task<CommentsModel> FetchTheComment(int id)
        {
            CheckConnection();
            string fetchedComment = ConstantStrings.fetchSingleComment(id);
            var result = await _connection.QueryAsync<CommentsModel>(fetchedComment);
            return result.First();

        }

        public async Task<Posts> FetchYourPostsAsync(int id)
        {
            CheckConnection();
            string fetchSinglePostQuery = ConstantStrings.fetchSinglePost(id);
            var result = await _connection.QueryAsync<Posts>(fetchSinglePostQuery);
            return result.FirstOrDefault();
        }

        public async Task<BlogUsers> getSingleUser(int id)
        {
            CheckConnection();
            string fetchQuery = ConstantStrings.getUserById(id);
            var data = await _connection.QueryAsync<BlogUsers>(fetchQuery);
            var result = data.FirstOrDefault();
            return result;
        }

        public async Task<FetchYourPostResponseModel> fetchYourPosts()
        {

            var uid = Program.authenticatedUser.User_id;
            string fetchRecentPost = ConstantStrings.fetchYourPost(uid);
            var result = await _connection.QueryAsync<Posts>(fetchRecentPost);
            _fetchYourPostsResponseModel.postsList = result.ToList();
            return _fetchYourPostsResponseModel;
        }
        public async Task<BlogUsers> checkUserExist(BlogUsers model)
        {
            CheckConnection();
            string query = ConstantStrings.CheckUserExists(model);
            var data = await _connection.QueryAsync<BlogUsers>(query);
            var result = data.FirstOrDefault();
            return result;
        }
    }
}

using BlogApp.Models;

namespace BlogApp.Contracts
{
    public interface IFetchOptions
    {
        public Task<CommentResponseModel> FetchAllCommentFromDb(int post_id);
        public Task<FetchAllUsersResponseModel> fetchAllUsers();
        public Task<FetchPostsResponseModel> fetchRecentPost();
        public Task<FetchPostsResponseModel> fetchAllPost();
        public Task<CommentsModel> FetchTheComment(int id);
        public Task<Posts> FetchYourPostsAsync(int id);
        public Task<BlogUsers> getSingleUser(int id);
        public Task<FetchYourPostResponseModel> fetchYourPosts();
        public Task<BlogUsers> checkUserExist(BlogUsers model);
    }
}

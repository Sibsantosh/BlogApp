using BlogApp.Models;

namespace BlogApp.Contracts
{
    public interface IUpdateOptions
    {
        public Task<int> UpdateTheComment(CommentsModel comments);
        public Task<int> updatePost(Posts posts);
        public Task<int> UpdateUserInDB(BlogUsers model);
        public Task<int> UpdateComment(CommentsModel model);
    }
}

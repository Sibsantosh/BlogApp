using BlogApp.Models;

namespace BlogApp.Contracts
{
    public interface IStoreOptions
    {
        public Task<int> StoreComment(CommentsModel model);
        
        public Task<int> StoreNewProject(Posts postModel);
    }
}

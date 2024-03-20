using BlogApp.Models;

namespace BlogApp.Contracts
{
    public interface IFetchYourPosts
    {
        public Task<FetchYourPostResponseModel> fetchYourPosts();
    }
}

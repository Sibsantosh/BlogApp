using BlogApp.Models;
using System.Data;

namespace BlogApp.Contracts
{
    public interface IFetchRecentPosts
    {
        public Task<FetchPostsResponseModel> fetchRecentPost();
        
    }
}

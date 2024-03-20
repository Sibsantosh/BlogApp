using BlogApp.Models;

namespace BlogApp.Contracts
{
    public interface ICheckUserExists
    {
        public Task<BlogUsers> checkUserExist(BlogUsers model);
    }
}

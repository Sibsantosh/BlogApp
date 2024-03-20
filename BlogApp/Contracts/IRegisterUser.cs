using BlogApp.Models;

namespace BlogApp.Contracts
{
    public interface IRegisterUser
    {
        public Task<int> RegisterUser(BlogUsers model);
    }
}

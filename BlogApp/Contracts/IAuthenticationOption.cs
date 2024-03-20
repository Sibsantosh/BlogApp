using BlogApp.Models;

namespace BlogApp.Contracts
{
    public interface IAuthenticationOption
    {

        public Task<BlogUsers> MakeUserLogin(LoginModel loginModel);
        public Task<int> RegisterUser(BlogUsers model);
    }
}

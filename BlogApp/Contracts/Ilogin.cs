using BlogApp.Models;
namespace BlogApp.Contracts
{
    public interface Ilogin
    {
        public Task<BlogUsers> MakeUserLogin(LoginModel loginModel);
    }
}

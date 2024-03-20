using BlogApp.Contracts;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;
using System.Reflection;


namespace BlogApp.Controllers
{
    public class BlogController : Controller
    {
       
        private readonly IFetchAllUsers _fetchAllUsers;
        private readonly IFetchRecentPosts _fetchRecentPosts;
        private readonly IFetchYourPosts _fetchYourPosts;
        private readonly ICheckUserExists checkUserExists;
        private readonly IRegisterUser registerUser;
        private readonly Ilogin ilogin;
        private bool isFirstLogin = true;
        private bool isRegistered = true;
        public BlogController(IFetchAllUsers fetchAllUsers, IFetchRecentPosts fetchRecentPosts,IFetchYourPosts fetchYourPosts, Ilogin ilogin, ICheckUserExists checkUserExists, IRegisterUser registerUser) 
        {
           
            _fetchAllUsers = fetchAllUsers;
            _fetchRecentPosts = fetchRecentPosts;
            _fetchYourPosts = fetchYourPosts;
            this.ilogin = ilogin;
            this.registerUser = registerUser;
            this.checkUserExists = checkUserExists;
            
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Logout()
        {
            Program.isLoggedIn = false;
            Program.userName = "";
            Program.isAdmin = false;
            isFirstLogin = true;
            Program.authenticatedUser = null;
            

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Index()
        {
            //  var data = await _fetchAllUsers.fetchAllUsers();
            FetchPostsResponseModel data1 = await _fetchRecentPosts.fetchRecentPost();
            ViewBag.postData = data1;
            if(Program.authenticatedUser != null)
            {
                FetchYourPostResponseModel data2 = await _fetchYourPosts.fetchYourPosts();
                ViewBag.UserPostData = data2;
            }
            return View();
        }
        public async Task<IActionResult> Dashboard(LoginModel model)
        {
            
            var result = await ilogin.MakeUserLogin(model);
            ViewBag.LoggedIn = model;
            ViewBag.RegisterUser = result;
            ViewBag.isFirstLogin = isFirstLogin;
            
            if (result != null)
            {
                isFirstLogin = false;
                if (model.Email.Equals(result.Email) && model.password.Equals(result.Password))
                {
                   Program.isLoggedIn = true;
                   Program.userName = result.Username.Split(" ")[0];
                   Program.authenticatedUser = result;
                    if (result.Role.Equals(3))
                    {
                        Program.isAdmin = true;
                    }
                    return RedirectToAction("Index");
                }
                else { return RedirectToAction("Login"); }
         
            }
           
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> RegisterUser(BlogUsers model)
        {

            var userExists = await this.checkUserExists.checkUserExist(model);
            if (userExists == null)
            {
                model.Registration_epoch = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                Console.WriteLine(model);
                var data = await registerUser.RegisterUser(model);
                isRegistered = true;
                isFirstLogin = true;
                Program.isLoggedIn = false;
                return RedirectToAction("Login");
            }
            else
            {
                isRegistered = true;
                Program.isLoggedIn = false;
                isFirstLogin = true;
                return RedirectToAction("Register");
            }


        }
    }
}

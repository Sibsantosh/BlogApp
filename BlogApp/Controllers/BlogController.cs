using BlogApp.Contracts;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;
using System.IO;



namespace BlogApp.Controllers
{
    public class BlogController : Controller
    {



        private readonly IAuthenticationOption _authenticationOptions;
        private readonly IDeleteOptions _deleteOptions;
        private readonly IFetchOptions _fetchOptions;
        private readonly IUpdateOptions _updateOptions;
        private readonly IStoreOptions _storeOptions;
       
        public BlogController( IFetchOptions fetchOptions,IUpdateOptions updateOptions,IDeleteOptions deleteOptions, IAuthenticationOption authenticationOption, IStoreOptions storeOptions)
        {

            _deleteOptions = deleteOptions;
            _authenticationOptions = authenticationOption;
            _fetchOptions = fetchOptions;
            _updateOptions = updateOptions;
            _storeOptions = storeOptions;

        }

        public IActionResult Login()
        {
           
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult PageNotFound() { 
            return View();
        }
        public IActionResult AdminPanel()
        {
            return View();
        }
        public async Task<IActionResult> NewPost()
        {
            return View();
        }
        public async Task<IActionResult> ViewPost()
        {
            return View();
        }
        public async Task<IActionResult> AllPosts()
        {
            FetchPostsResponseModel data1 = await _fetchOptions.fetchAllPost();
            ViewBag.postData = data1;
            if (Program.authenticatedUser != null)
            {
                FetchYourPostResponseModel data2 = await _fetchOptions.fetchYourPosts();
                ViewBag.UserPostData = data2;
            }
            return View();
        }
        public async Task<IActionResult> AllUsers()
        {

            var result = await _fetchOptions.fetchAllUsers();
        
            ViewBag.responseModel = result;
            //Console.WriteLine(responseModel);
            return View();

        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            //Console.WriteLine($"user deleting id = {id}");
            if (Program.isAdmin)
            {
                await _deleteOptions.DeleteUser(id);
                return RedirectToAction("AllUsers");
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public IActionResult Logout()
        {
            Program.isLoggedIn = false;
            Program.userName = "";
            Program.isAdmin = false;
            Program.isFirstLogin = true;
            Program.isRegistered = false;
            Program.authenticatedUser = null;


            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Index()
        {
            //  var data = await _fetchAllUsers.fetchAllUsers();
            FetchPostsResponseModel data1 = await _fetchOptions.fetchRecentPost();
            ViewBag.postData = data1;
            if (Program.authenticatedUser != null)
            {
                FetchYourPostResponseModel data2 = await _fetchOptions.fetchYourPosts();
                ViewBag.UserPostData = data2;
            }
            return View();
        }
        public async Task<IActionResult> Dashboard(LoginModel model)
        {

            var result = await _authenticationOptions.MakeUserLogin(model);
            Program.isFirstLogin = false;
            ViewBag.LoggedIn = model;
            ViewBag.RegisterUser = result;
           

            if (result != null)
            {
                Program.isFirstLogin = false;
                if (model.Email.Equals(result.Email) && model.password.Equals(result.Password) && result.Role!=4)
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
                else { 
                    Program.isFirstLogin =false;
                   
                    return RedirectToAction("Login"); 
                }

            }

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> RegisterUser(BlogUsers model)
        {

            var userExists = await _fetchOptions.checkUserExist(model);
            if (userExists == null)
            {
                model.Registration_epoch = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                Console.WriteLine(model);
                var data = await _authenticationOptions.RegisterUser(model);
                Program.isRegistered = true;
                Program.isFirstLogin = true;
                Program.isLoggedIn = false;
                return RedirectToAction("Login");
            }
            else
            {
                Program.isRegistered = true;
                Program.isLoggedIn = false;
                Program.isFirstLogin = true;
                return RedirectToAction("Register");
            }


        }

        
        public async Task<IActionResult> StorePost(Posts postModel, IFormFile image)
        {
            if (image == null || image.Length == 0) { return BadRequest("No file uploaded."); }
            else
            {


                // Construct the URL to access the image
                var fileName = Path.GetTempFileName();

                // Save the uploaded file to the temporary location
                using (var stream = new FileStream(fileName, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                Console.WriteLine(fileName);
            }
            postModel.Author_id = Program.authenticatedUser.User_id;
            postModel.Publication_epoch  = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            postModel.Last_updated_epoch = postModel.Publication_epoch;
            //await _storeOptions.StoreNewProject(postModel);
        
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeletePost(int id)
        {
            Posts post = await _fetchOptions.FetchYourPostsAsync(id);
            if (Program.isAdmin || Program.authenticatedUser!=null && post.Author_id == Program.authenticatedUser.User_id)
            {
                Console.WriteLine($"deleted {id}");
                await _deleteOptions.DeletePostFromDb(id);
                return RedirectToAction("Index");
            }
            else
            {
                return View("PageNotFound");
            }

        }
        public async Task<IActionResult> EditPost(int id)
        {
            Posts post = await _fetchOptions.FetchYourPostsAsync(id);
            if (post != null) { 
                if(Program.isAdmin ||Program.authenticatedUser!=null && post.Author_id == Program.authenticatedUser.User_id )
                {
                    ViewBag.isEdit = true;
                    ViewBag.post = post;
                    return View("NewPost");
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }

        }

        public async Task<IActionResult> UpdatePostInDb(Posts postModel, IFormFile image)
        {
            
            postModel.Last_updated_epoch = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            //await _updateOptions.updatePost(postModel);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> VisitPost(int id) 
        {
            var data = await _fetchOptions.FetchYourPostsAsync(id);
            var comments = await _fetchOptions.FetchAllCommentFromDb(id);
            ViewBag.post = data;
            ViewBag.comments = comments;
            return View("ViewPost");
        }

        public async Task<IActionResult> StoreComment(CommentsModel commentsModel)
        {
            commentsModel.comment_epoch = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            commentsModel.user_name = Program.authenticatedUser.Username;
            var result = await _storeOptions.StoreComment(commentsModel);
            Console.WriteLine(commentsModel);
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> DeleteComment(int id)
        {
            CommentsModel comments = await _fetchOptions.FetchTheComment(id);
            if (Program.isAdmin || Program.authenticatedUser != null && comments.user_id == Program.authenticatedUser.User_id)
            {
                Console.WriteLine($"deleted {id}");
                await _deleteOptions.DeleteTheComment(id);
                return Redirect(Request.Headers["Referer"].ToString());
            }
            else
            {
                return View("PageNotFound");
            }

        }

        public async Task<IActionResult> UpdateUsersInDB(BlogUsers model)
        {
            Console.WriteLine(model);
            var result = await _updateOptions.UpdateUserInDB(model);
            return RedirectToAction("AllUsers");
        }

        public async Task<IActionResult> AddUserAsAdmin()
        {

            ViewBag.isEdit = false;
            return View("AddUsers");
        }


        public async Task<IActionResult> SaveUserAsDbAdmin(BlogUsers model)
        {
            var userExists = await _fetchOptions.checkUserExist(model);
            if (userExists == null)
            {
                model.Registration_epoch = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                var data = await _authenticationOptions.RegisterUser(model);
                return RedirectToAction("AllUsers");
            }
            else
            {

                return RedirectToAction("AddUserAsAdmin");
            }

        }

        public async Task<IActionResult> UpdateUsersAsAdmin(int id)
        {
            ViewBag.isEdit = true;
            var registerModel = await _fetchOptions.getSingleUser(id);
            ViewBag.blogUser = registerModel;
            return View("AddUsers");
        }
    }
}

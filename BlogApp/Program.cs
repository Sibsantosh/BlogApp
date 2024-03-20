using BlogApp.Contracts;
using BlogApp.Models;

using BlogApp.Repositories;
using RazorLearning.Repositories;

namespace BlogApp
{
    public class Program
    {
        public static bool isLoggedIn { get; set; } = false;
        public static string userName { get; set; } = "";
        public static bool isAdmin { get; set; } = false;
        public static BlogUsers authenticatedUser { get; set; } = null;
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSingleton<IDatabaseConnection>( _ => new DatabaseConnection());
            builder.Services.AddSingleton<IFetchAllUsers, FetchAllUsers>();
            builder.Services.AddSingleton<ICheckUserExists, CheckUserExist>();
            builder.Services.AddSingleton<IRegisterUser, RegisterDatabaseUser>();
            builder.Services.AddSingleton<IFetchRecentPosts, FetchAllPosts>();
            builder.Services.AddSingleton<IFetchYourPosts, FetchYourPosts>();
            builder.Services.AddSingleton<Ilogin, LoginUser>();
            builder.Services.AddSingleton<BlogUsers>();
            builder.Services.AddSingleton<LoginModel>();
            builder.Services.AddSingleton<FetchAllUsersResponseModel>();
            builder.Services.AddSingleton<FetchPostsResponseModel>();
            builder.Services.AddSingleton<FetchYourPostResponseModel>();
            
            


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Blog}/{action=Index}");

            app.Run();
        }
    }
}

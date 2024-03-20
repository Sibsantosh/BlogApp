using BlogApp.Contracts;
using BlogApp.Models;

using BlogApp.Repositories;


namespace BlogApp
{
    public class Program
    {/*
        public static bool isLoggedIn { get; set; } = false;
        public static string userName { get; set; } = "";
        public static bool isAdmin { get; set; } = false;
        public static bool isFirstLogin { get; set; } = true;
        public static bool isRegistered { get; set; } = false;
        public static BlogUsers authenticatedUser { get; set; } = null;
        */
        public static bool isLoggedIn { get; set; } = true;
        public static string userName { get; set; } = "Sib";
        public static bool isAdmin { get; set; } = true;
        public static bool isFirstLogin { get; set; } = false;
        public static bool isRegistered { get; set; } = false;
        public static BlogUsers authenticatedUser { get; set; } = new BlogUsers { User_id = 1, Email ="sib@gmail.com", Password = "111111", Registration_epoch = 0, Role=3, Username="sib"};
        
        

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSingleton<IDatabaseConnection>( _ => new DatabaseConnection());
            builder.Services.AddSingleton<IFetchOptions, FetchOptions>();
            builder.Services.AddSingleton<IUpdateOptions, UpdateOptions>();
            builder.Services.AddSingleton<IDeleteOptions, DeleteOptions>();
            builder.Services.AddSingleton<IAuthenticationOption, AuthenticationOptions>();
            builder.Services.AddSingleton<IStoreOptions, StoreOptions>();
            builder.Services.AddSingleton<BlogUsers>();
            builder.Services.AddSingleton<LoginModel>();
            builder.Services.AddSingleton<CommentsModel>();
            builder.Services.AddSingleton<FetchAllUsersResponseModel>();
            builder.Services.AddSingleton<FetchPostsResponseModel>();
            builder.Services.AddSingleton<FetchYourPostResponseModel>();
            builder.Services.AddSingleton<CommentResponseModel>();
            
            


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

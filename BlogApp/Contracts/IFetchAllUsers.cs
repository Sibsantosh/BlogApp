using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Contracts
{
    public interface IFetchAllUsers
    {
        public Task<FetchAllUsersResponseModel> fetchAllUsers();
    }
}

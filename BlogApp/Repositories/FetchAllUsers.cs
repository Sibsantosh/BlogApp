using BlogApp.Constants;
using BlogApp.Contracts;
using BlogApp.Models;
using Dapper;
using System.Data;

namespace BlogApp.Repositories
{
    public class FetchAllUsers : IFetchAllUsers
    {
        private readonly IDbConnection _connection;
        private FetchAllUsersResponseModel _responseModel;
        public FetchAllUsers(IDatabaseConnection connection, FetchAllUsersResponseModel responseModel)
        {
            _connection = connection.connectDatabase();
            _responseModel = responseModel;
        }

        public async Task<FetchAllUsersResponseModel> fetchAllUsers()
        {
            string fetchAllUsersQuery = ConstantStrings.fetchAllUsers();
            var result = await _connection.QueryAsync<BlogUsers>(fetchAllUsersQuery);
            var list = result.ToList();
            foreach ( var item in list )
            {
                Console.WriteLine(item);
            }
            _responseModel.usersList = list;
            return _responseModel ;

        }
    }
}

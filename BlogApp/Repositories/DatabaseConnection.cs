using BlogApp.Constants;
using BlogApp.Contracts;
using System.Data.SqlClient;
using System.Data;


namespace BlogApp.Repositories
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly string connectionString;
        public DatabaseConnection()
        {
            this.connectionString = ConstantStrings.GetConnectionString();
        }

        public IDbConnection connectDatabase()
        {
            Console.WriteLine("Db connected");
            return new SqlConnection(this.connectionString);
        }
    }
}

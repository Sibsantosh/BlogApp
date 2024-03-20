using System.Data;

namespace BlogApp.Contracts
{
    public interface IDatabaseConnection
    {
        public IDbConnection connectDatabase();
    }
}

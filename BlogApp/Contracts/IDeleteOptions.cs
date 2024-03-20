namespace BlogApp.Contracts
{
    public interface IDeleteOptions
    {
        public Task<int> DeleteTheComment(int id);
        public Task<int> DeletePostFromDb(int id);
        public Task<int> DeleteUser(int id);
    }
}

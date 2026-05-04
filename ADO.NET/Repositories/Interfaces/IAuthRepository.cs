namespace ADO.NET.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> ValidateUser (string username, string password);
    }
}

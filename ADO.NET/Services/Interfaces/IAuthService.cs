namespace ADO.NET.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> IsLogin (string userName, string password);
    }
}

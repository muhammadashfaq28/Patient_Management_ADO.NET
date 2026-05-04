using ADO.NET.Repositories.Interfaces;
using ADO.NET.Services.Interfaces;

namespace ADO.NET.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly JwtHelper _jwtHelper;
        public AuthService(IAuthRepository authRepository, JwtHelper jwtHelper)
        {
            _authRepository = authRepository;
            _jwtHelper = jwtHelper;
        }
        public async Task<string> IsLogin (string username, string password)
        {
            var isValid = await _authRepository.ValidateUser(username, password);

            if (!isValid)
                return null;

            return _jwtHelper.GenerateToken(username);
         
        }
    }
}

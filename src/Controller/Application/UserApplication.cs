using Controller.Application.Interfaces;
using UseCase.Dtos;
using UseCase.UseCase.Interfaces;

namespace Controller.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserUseCase _userUseCase;
        public UserApplication(IUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        public async Task CreateUserAsync(UserRequest userRequest)
        {
            await _userUseCase.CreateUserAsync(userRequest);
        }

        public async Task<UserResponse?> AuthenticateUserAsync(UserAuthenticateRequest user, CancellationToken cancellationToken)
        {
            return await _userUseCase.AuthenticateUserAsync(user);
        }
    }
}
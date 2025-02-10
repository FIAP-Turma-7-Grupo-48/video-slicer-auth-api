using UseCase.Dtos;

namespace Controller.Application.Interfaces
{
    public interface IUserApplication
    {
        public Task CreateUserAsync(UserRequest userRequest);
        public Task<UserResponse> AuthenticateUserAsync(UserAuthenticateRequest user, CancellationToken cancellationToken);
    }
}
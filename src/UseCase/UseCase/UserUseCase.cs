using Core.Notifications;
using Domain.Entities.UserAggregate;
using Domain.Helpers;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using UseCase.Dtos;
using UseCase.UseCase.Interfaces;

namespace UseCase.UseCase
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IUserMongoRepository _userRepository;
        private readonly IUserService _userService;
        private readonly NotificationContext _notificationContext;
        public UserUseCase(
            IUserMongoRepository userRepository,
            IUserService userService,
            NotificationContext notificationContext)
        {
            _userRepository = userRepository;
            _userService = userService;
            _notificationContext = notificationContext;
        }

        public async Task CreateUserAsync(UserRequest userRequest)
        {
            var userExist = await _userRepository.GetUserAsync(userRequest.Email);

            if(userExist != null)
            {
                _notificationContext.AssertArgumentNotNull(userExist, $"O usuário já foi cadastrado");
                return;
            }

            if (_notificationContext.HasErrors)
            {
                return;
            }

            await _userRepository.CreateUserAsync(new User
            {
                Name = userRequest.Name,
                Email = userRequest.Email,
                Password = PasswordHelper.HashPassword(userRequest.Password),
            });
        }

        public async Task<UserResponse> AuthenticateUserAsync(UserRequest userRequest)
        {
            var user = await _userRepository.GetUserAsync(userRequest.Email);

            if (user == null)
            {
                _notificationContext.AssertArgumentNotNull(user, $"O usuário {userRequest.Name} não existe.").ToString();
                return new UserResponse { Token = string.Empty, Notification = new List<string> { $"O usuário {userRequest.Name} não existe." } };
            }

            var isPassValid = PasswordHelper.VerifyPassword(user.Password, userRequest.Password);

            if (!isPassValid)
            {
                _notificationContext.AssertArgumentNotNull(user, $"Senha {userRequest.Password} inválida.").ToString();
                return new UserResponse { Token = string.Empty, Notification = new List<string> { $"Senha {userRequest.Password} inválida." } };
            }

            string token = _userService.GenerateTokenAsync(userRequest.Name, userRequest.Email, default);

            return new UserResponse { Token = token, Notification = new List<string> { $"Token criado com sucesso." } };
        }
    }
}
using Core.Notifications;
using Domain.Entities.UserAggregate;
using Domain.Helpers;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Moq;
using UseCase.Dtos;
using UseCase.UseCase;
using Xunit;

namespace UnitTest.UseCase
{
    public class UserUseCaseTest
    {
        UserUseCase _userUseCase;

        Mock<IUserMongoRepository> _userRepository;
        Mock<IUserService> _userService;

        Mock<NotificationContext> _notificationContext;
        public UserUseCaseTest()
        {
            _userRepository = new Mock<IUserMongoRepository>();
            _userService = new Mock<IUserService>();
            _notificationContext = new Mock<NotificationContext>();

            _userUseCase = new UserUseCase(_userRepository.Object,
                                           _userService.Object,    
                                           _notificationContext.Object
                                           );
        }

        [Fact]
        public async void DevePermitirCriarUsuarios()
        {
            UserRequest userRequest = new UserRequest
            {
                Name = "teste",
                Email = "test@gmail.com",
                Password = "aA2124(*&o0"
            };

            User user = null;

            _userRepository.Setup(x => x.GetUserAsync(It.IsAny<string>())).Returns(Task.FromResult(user));
            _userRepository.Setup(x => x.CreateUserAsync(It.IsAny<User>())).Returns(Task.FromResult(user));


            await _userUseCase.CreateUserAsync(userRequest);

            _userRepository.Verify(p => p.GetUserAsync(It.IsAny<string>()), Times.Exactly(1));
            _userRepository.Verify(p => p.CreateUserAsync(It.IsAny<User>()), Times.Exactly(1));
        }

        [Fact]
        public async void DevePermitirNaoCriarUsuarioJaCadastrado()
        {
            UserRequest userRequest = new UserRequest
            {
                Name = "teste",
                Email = "test@gmail.com",
                Password = "aA2124(*&o0"
            };

            User user = new() {
                Name = "teste",
                Email = "test@gmail.com",
                Password = "aA2124(*&o0"
            };

            _notificationContext.Object.AddNotification("O usuário já foi cadastrado");

            _userRepository.Setup(x => x.GetUserAsync(It.IsAny<string>())).Returns(Task.FromResult(user));
            _userRepository.Setup(x => x.CreateUserAsync(It.IsAny<User>())).Returns(Task.FromResult(user));

            await _userUseCase.CreateUserAsync(userRequest);


            Assert.True(_notificationContext.Object.HasErrors);
            _userRepository.Verify(p => p.GetUserAsync(It.IsAny<string>()), Times.Exactly(1));
            _userRepository.Verify(p => p.CreateUserAsync(It.IsAny<User>()), Times.Exactly(0));
        }

        [Fact]
        public async void DevePermitirAutenticarUsuario()
        {
            UserAuthenticateRequest userRequest = new UserAuthenticateRequest
            {
                Email = "test@gmail.com",
                Password = "aA2124(*&o0"
            };

            User user = new()
            {
                Name = "teste",
                Email = "test@gmail.com",
                Password = PasswordHelper.HashPassword("aA2124(*&o0")
            };

            _userRepository.Setup(x => x.GetUserAsync(It.IsAny<string>())).Returns(Task.FromResult(user));
            _userService.Setup(x => x.GenerateTokenAsync(It.IsAny<string>(),
                                                         It.IsAny<string>(),
                                                         default)).Returns("12345awrfqwrgr03r");

            var result = await _userUseCase.AuthenticateUserAsync(userRequest);


            Assert.True(result.Token != null);
            _userRepository.Verify(p => p.GetUserAsync(It.IsAny<string>()), Times.Exactly(1));
            _userService.Verify(p => p.GenerateTokenAsync(It.IsAny<string>(),
                                                          It.IsAny<string>(),
                                                          default), Times.Exactly(1));
        }

        [Fact]
        public async void DevePermitirNaoAutenticarQuandoUsuarioInvalido()
        {
            UserAuthenticateRequest userRequest = new UserAuthenticateRequest
            {
                Email = "test@gmail.com",
                Password = "aA2124(*&o0"
            };

            User user = null;

            _userRepository.Setup(x => x.GetUserAsync(It.IsAny<string>())).Returns(Task.FromResult(user));
            _userService.Setup(x => x.GenerateTokenAsync(It.IsAny<string>(),
                                                         It.IsAny<string>(),
                                                         default)).Returns("12345awrfqwrgr03r");

            _notificationContext.Object.AddNotification($"Login e Senha Inválido.");

            var result = await _userUseCase.AuthenticateUserAsync(userRequest);


            Assert.Null(result);
            Assert.True(_notificationContext.Object.HasErrors);
            _userRepository.Verify(p => p.GetUserAsync(It.IsAny<string>()), Times.Exactly(1));
            _userService.Verify(p => p.GenerateTokenAsync(It.IsAny<string>(),
                                                          It.IsAny<string>(),
                                                          default), Times.Exactly(0));
        }

        [Fact]
        public async void DevePermitirNaoAutenticarQuandoSenhaInvalido()
        {
            UserAuthenticateRequest userRequest = new UserAuthenticateRequest
            {
                Email = "test@gmail.com",
                Password = "aA2124(*&o0$#"
            };

            User user = new()
            {
                Name = "teste",
                Email = "test@gmail.com",
                Password = PasswordHelper.HashPassword("aA2124(*&o0")
            };

            _userRepository.Setup(x => x.GetUserAsync(It.IsAny<string>())).Returns(Task.FromResult(user));
            _userService.Setup(x => x.GenerateTokenAsync(It.IsAny<string>(),
                                                         It.IsAny<string>(),
                                                         default)).Returns("12345awrfqwrgr03r");

            _notificationContext.Object.AddNotification($"Login e Senha Inválido.");

            var result = await _userUseCase.AuthenticateUserAsync(userRequest);


            Assert.Null(result);
            Assert.True(_notificationContext.Object.HasErrors);
            _userRepository.Verify(p => p.GetUserAsync(It.IsAny<string>()), Times.Exactly(1));
            _userService.Verify(p => p.GenerateTokenAsync(It.IsAny<string>(),
                                                          It.IsAny<string>(),
                                                          default), Times.Exactly(0));
        }
    }
}
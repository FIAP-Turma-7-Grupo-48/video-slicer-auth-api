using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCase.Dtos;

namespace UseCase.UseCase.Interfaces
{
    public interface IUserUseCase
    {
        public Task CreateUserAsync(UserRequest userRequest);
        public Task<UserResponse?> AuthenticateUserAsync(UserAuthenticateRequest userRequest);
    }
}
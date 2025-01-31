using Domain.Entities.UserAggregate;
using Domain.ValueObjects;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IUserMongoRepository
    {
        public Task<User> GetUserAsync(string email);
        public Task CreateUserAsync(User user);
    }
}
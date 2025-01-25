using Domain.Entities.UserAggregate;
using Domain.ValueObjects;
using Infrastructure.Repositories.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class UserMongoRepository : IUserMongoRepository
    {
        private readonly IMongoCollection<User> _users;
        public UserMongoRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("UserDb");
            _users = database.GetCollection<User>("Users");
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _users.Find(user => user.Email == email).FirstOrDefaultAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }
    }
}
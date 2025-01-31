namespace Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        public string GenerateTokenAsync(string name, string email, CancellationToken cancellationToken);
    }
}
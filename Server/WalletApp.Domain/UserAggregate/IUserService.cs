namespace WalletApp.Domain.UserAggregate;

public interface IUserService
{
    public Task<User> GetByIdAsync(Guid id);
    public Task<User> GetByCredentialsAsync(string userNameOrEmail, string password);
    public Task<User> CreateAsync(string userName, string email, string password);
}
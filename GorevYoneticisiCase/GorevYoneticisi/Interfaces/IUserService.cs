using GorevYoneticisi.Model;

namespace GorevYoneticisi.Interfaces
{
    public interface IUserService
    {
        Task<string> createUser(string username, string password, string passwordAgain);

        Task<TokenInfo> login(string username, string password);
    }
}

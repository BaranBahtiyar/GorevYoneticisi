
using GorevYoneticisi.Model;

namespace Deneme.Interface
{
    public interface ITokenService
    {
        public Task<TokenInfo> GenerateToken(Users user);
    }
}

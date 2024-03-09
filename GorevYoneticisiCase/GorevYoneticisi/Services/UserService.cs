using Deneme.Interface;
using GorevYoneticisi.Interfaces;
using GorevYoneticisi.Model;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GorevYoneticisi.Services
{
    public class UserService : IUserService
    {
        public IRepository<Users> _userRepo;

        public IRepository<TokenInfo> _tokenRepo;

        readonly ITokenService tokenService;

        public UserService(IRepository<Users> userRepo, IRepository<TokenInfo> tokenRepo, ITokenService tokenService)
        {
            _userRepo = userRepo;
            _tokenRepo = tokenRepo;
            this.tokenService = tokenService;
        }

        public async Task<string> createUser(string username, string password, string passwordAgain)
        {
            if(username == null || password == null || passwordAgain == null) throw new ArgumentNullException("Eksik veri girildi.");

            if(!password.Equals(passwordAgain)) throw new Exception("Şifreler uyuşmuyor.");

            Users? user = _userRepo.Get(user => user.UserName == username && user.Password == password);

            if(user != null)
            {
                throw new Exception("Girilen bilgilere ait kullanıcı bulunmaktadır");
            }

            Users newUser = new Users
            {
                UserName = username,
                Password = password,
            };

            _userRepo.Ekle(newUser);

            Users? addedUser = _userRepo.Get(user => user.UserName == username && user.Password == password);

            TokenInfo token = await tokenService.GenerateToken(addedUser);

            _tokenRepo.Ekle(token);

            return "Kullanıcı başarıyla eklendi.";
        }

        public async Task<TokenInfo> login(string username, string password)
        {
            if (username == null || password == null) throw new ArgumentNullException("Kullanıcı adı veya şifre eksik girildi.");

            Users? user = _userRepo.Get(user => user.UserName == username && user.Password == password );

            if (user == null) throw new Exception("Girilen bilgilera ait bir kullanıcı bulunmamaktadır.");

            TokenInfo tokenInfo = _tokenRepo.Get(token => token.userid == user.Id);

            return tokenInfo;


        }
    }
}

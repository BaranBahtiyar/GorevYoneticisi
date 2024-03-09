using Deneme.Interface;
using Deneme.Services;
using GorevYoneticisi.Interfaces;
using GorevYoneticisi.Model;
using GorevYoneticisi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GorevYoneticisi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService _userService;

        readonly IRaporService _raporService;

        readonly ITokenService _tokenService;

        public IRepository<TokenInfo> _tokenRepo;

        public IRepository<Users> _userRepo;

        private static int id;

        private static string jwtToken;

        public UserController(IUserService userService, IRaporService raporService, IRepository<TokenInfo> tokenRepo, ITokenService tokenService, IRepository<Users> userRepo)
        {
            _userService = userService;
            _raporService = raporService;
            _tokenRepo = tokenRepo;
            _tokenService = tokenService;
            _userRepo = userRepo;
        }

        [HttpPost("Create User")]
        [AllowAnonymous]

        public async Task<ActionResult<string>> createUser([FromQuery] string username, [FromQuery] string password, [FromQuery] string passwordagain)
        {
            var result = await _userService.createUser(username, password, passwordagain);
            return result;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenInfo>> Login([FromQuery] string username, [FromQuery] string password)
        {
            var result = await _userService.login(username, password);
            id = result.userid;

            if (result.ExpiryTime < DateTime.Now)
            {
                Users user = _userRepo.Get(k => k.Id == result.userid);

                TokenInfo token = await _tokenService.GenerateToken(user);

                result.ExpiryTime = token.ExpiryTime;
                result.Token = token.Token;

                _tokenRepo.Guncelle(result);

                jwtToken = result.Token;

                return Ok(new { Warning = "Token süresi geçtiği için yeni token oluşturuldu.", TokenInformation = result });
            }

            jwtToken = result.Token;

            return result;
        }

        [HttpPost("CreateReport")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> CreateReport([FromQuery] string raporTipi, [FromQuery] string raporIcerigi, [FromQuery] string JWTToken)
        {

            if(JWTToken == null)
            {
                throw new Exception("Rapor eklemek için Token bilgisi giriniz.");
            }

            else if (jwtToken != JWTToken)
            {
                throw new Exception("Token bilgileri uyuşmamaktadır. Lütfen giriş yapın");
            }

            var result = await _raporService.raporEkle(id, raporTipi, raporIcerigi); 
            return result;
        }

        [HttpPost("ShowReport")]
        [AllowAnonymous]
        public async Task<ActionResult<List<RaporKayit>>> ShowReport([FromQuery] string JWTToken)
        {
            if (JWTToken == null)
            {
                throw new Exception("Rapor görüntülemek için Token bilgisi giriniz.");
            }

            else if (jwtToken != JWTToken)
            {
                throw new Exception("Token bilgileri uyuşmamaktadır. Lütfen giriş yapın");
            }

            var result = await _raporService.raporlar(id);
            return result;
        }

        [HttpPost("ShowReportByType")]
        [AllowAnonymous]
        public async Task<ActionResult<List<RaporKayit>>> ShowReportByType([FromQuery] string raporTipi, [FromQuery] string JWTToken)
        {
            if (JWTToken == null)
            {
                throw new Exception("Rapor görüntülemek için Token bilgisi giriniz.");
            }

            else if (jwtToken != JWTToken)
            {
                throw new Exception("Token bilgileri uyuşmamaktadır. Lütfen giriş yapın");
            }

            var result = await _raporService.raporByTip(id, raporTipi);
            return result;
        }


    }
}

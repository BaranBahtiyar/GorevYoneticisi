using System.ComponentModel.DataAnnotations.Schema;

namespace GorevYoneticisi.Model
{
    public class TokenInfo
    {
        public int Id { get; set; }

        public int userid { get; set; }

        public string? Token { get; set; }

        public DateTime ExpiryTime { get; set; }
    }
}

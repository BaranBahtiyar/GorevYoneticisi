using GorevYoneticisi.Model;
using Microsoft.EntityFrameworkCore;

namespace GorevYoneticisi.Utility
{
    public class DBContextGY : DbContext
    {
        public DBContextGY(DbContextOptions options) : base(options) { }

        public DbSet<Users> Users { get; set; }

        public DbSet<TokenInfo> TokenInfo { get; set; }
        
        public DbSet<RaporKayit> RaporKayit { get; set; }



    }
}

using GorevYoneticisi.Interfaces;
using GorevYoneticisi.Model;

namespace GorevYoneticisi.Services
{
    public class RaporService : IRaporService
    {
        readonly IRepository<RaporKayit> _raporRepo;

        public RaporService(IRepository<RaporKayit> raporRepo)
        {
            _raporRepo = raporRepo;
        }

        public Task<List<RaporKayit>> raporByTip(int id, string raporTipi)
        {

            if(raporTipi.Trim() == null)
            {
                throw new ArgumentNullException("Rapor Tipi Giriniz.");
            }

            List<RaporKayit> kayitlar = _raporRepo.GetAll().Where(kayit => kayit.userId == id && kayit.raporTipi == raporTipi.ToLower()).ToList();

            return Task.FromResult(kayitlar);
        }

        public Task<string> raporEkle(int id, string raporTipi, string raporAciklama)
        {

            if (raporTipi.Trim() == null && raporAciklama.Trim() == null)
            {
                throw new ArgumentNullException("Boş veri kaydedemezsiniz.");
            }

            if (raporTipi.Trim() == null)
            {
                throw new ArgumentNullException("Rapor Tipi Giriniz.");
            }

            else if (raporAciklama.Trim() == null)
            {
                throw new ArgumentNullException("Rapor Açıklaması Giriniz.");
            }

            RaporKayit yeniRapor = new RaporKayit
            {
                userId = id,
                raporTipi = raporTipi.ToLower(),
                raporIcerigi = raporAciklama
            };

            _raporRepo.Ekle(yeniRapor);

            return Task.FromResult("Rapor Eklendi.");
        }

        public Task<List<RaporKayit>> raporlar(int id)
        {
            List<RaporKayit> raporlar = _raporRepo.GetAll().Where(kayit => kayit.userId == id).ToList();

            return Task.FromResult(raporlar);
        }
    }
}

using GorevYoneticisi.Model;

namespace GorevYoneticisi.Interfaces
{
    public interface IRaporService
    {
        Task<string> raporEkle(int id, string raporTipi, string raporAciklama);

        Task<List<RaporKayit>> raporlar(int id);

        Task<List<RaporKayit>> raporByTip(int id, string raporTipi);
    }
}

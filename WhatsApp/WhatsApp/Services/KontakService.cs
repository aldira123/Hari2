using WhatsApp.Models;
using WhatsApp.ViewModels;

namespace WhatsApp.Services
{
    public class KontakService : IKontakService 
    {
        List<Kontak> _kontaks;

        public KontakService()
        {
            _kontaks = new List<Kontak>()
            {
             new Kontak("Aldira", "089639971606"),
             new Kontak("Fitri", "089987621623"),
             new Kontak("Haniifah", "089987621623"),
           };
        }

        public int Add(KontakViewModel request)
        {
            throw new NotImplementedException();
        }

        public List<KontakViewModel> GetKontaks()
        {
            List<KontakViewModel> kontakViewModel = new List<KontakViewModel>();

            foreach (var item in _kontaks)
            {
                kontakViewModel.Add(new KontakViewModel(item.Nama, item.Nomor));
            }

            return kontakViewModel;
        }

    }
}


using WhatsApp.ViewModels;

namespace WhatsApp.Services
{
    public interface IKontakService
    {
        List<KontakViewModel> GetKontaks();
        int Add(KontakViewModel request);
    }
}

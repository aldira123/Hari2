using WhatsApp.ViewModels;

namespace WhatsApp.Services
{
    public interface IFileService
    {
        Task<List<KontakViewModel>> Read();
        Task Write(KontakViewModel request);
    }
}

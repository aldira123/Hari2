using WhatsApp.ViewModels;

namespace WhatsApp.Services
{
    public class FileService : IFileService
    {
        private const string FILE_NAME = "_whatsApp.txt";
        public async Task<List<KontakViewModel>> Read()
        {
            if(!File.Exists(System.AppContext.BaseDirectory + FILE_NAME)){
                return new List<KontakViewModel>();
            }
            var res = await File.ReadAllLinesAsync(System.AppContext.BaseDirectory + FILE_NAME);
            if(res == null)
                return new List<KontakViewModel>();

            List<KontakViewModel> kontak = new List<KontakViewModel>();
            foreach (var item in res)
            {
                var dataSplit = item.Split(":");
                kontak.Add(new KontakViewModel(dataSplit[0],dataSplit[1]));
            }

            return kontak;
        }

        public async Task Write(KontakViewModel request)
        {
            if(!File.Exists(System.AppContext.BaseDirectory + FILE_NAME)){
                using (var fileStream = File.Create(System.AppContext.BaseDirectory + FILE_NAME)){
                    fileStream.Close();
                }
            }
            using(var fileStream = File.AppendText(System.AppContext.BaseDirectory + FILE_NAME)){
                await fileStream.WriteLineAsync($"{request.Nama}:{request.Nomor}");
            }
        }
    }
}

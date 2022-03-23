namespace WhatsApp.Models
{
    public class KontakViewModel
    {
        public string Nama { get; set; }
        public string Nomor { get; set; }

        public KontakViewModel()
        {

        }
        public KontakViewModel(string nama, string nomor)
        {
            Nama = nama;
            Nomor = nomor;
        }
    }
}

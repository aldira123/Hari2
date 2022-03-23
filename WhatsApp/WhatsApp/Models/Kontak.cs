namespace WhatsApp.Models
{
    public class Kontak
    {
        public string Nama { get; set; }
        public string Nomor { get; set; }

        public Kontak()
        {

        }
        public Kontak(string nama, string nomor)
        {
            Nama = nama;
            Nomor = nomor;
        }
    }
}

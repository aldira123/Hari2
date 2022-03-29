using eCommerce.Datas.Entities;
namespace eCommerce.ViewModels
{
    public class KategoriViewModel
    {
        public KategoriViewModel()
        {
            NamaKategori = string.Empty;
            DeskripsiKategori = string.Empty;
            Icon = string.Empty;
        }

        public KategoriViewModel(Kategori item)
        {
            IdKategori = item.IdKategori;
            NamaKategori = item.NamaKategori;
            DeskripsiKategori = item.DeskripsiKategori;
            Icon = item.Icon;
        }

        public int IdKategori { get; set; }
        public string NamaKategori { get; set; }
        public string DeskripsiKategori { get; set; }
        public string? Icon { get; set; }
        public string IconSrc {
        get {
            return (string.IsNullOrEmpty(Icon) ? "images/default.png" : Icon );
         }
        }
        public IFormFile? IconFile { get; set; }

        public Kategori ConvertToDbModel()
        {
            return new Kategori
            {
                IdKategori = this.IdKategori,
                NamaKategori = this.NamaKategori,
                DeskripsiKategori = this.DeskripsiKategori,
                Icon = this.Icon ?? string.Empty,
            };
        }

    }
}
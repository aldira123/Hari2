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

        public int IdKategori { get; set; }
        public string NamaKategori { get; set; }
        public string DeskripsiKategori { get; set; }
         public string Icon { get; set; }

        public Kategori ConvertToDbModel()
        {
            return new Kategori
            {
                IdKategori = this.IdKategori,
                NamaKategori = this.NamaKategori,
                DeskripsiKategori = this.DeskripsiKategori,
                Icon = this.Icon,
            };
        }

    }
}
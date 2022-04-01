using eCommerce.Datas.Entities;
namespace eCommerce.ViewModels
{
    public class KategoriCustomerViewModel
    {
        public KategoriCustomerViewModel()
        {
            NamaKategori = string.Empty;
            DeskripsiKategori = string.Empty;
            Icon = string.Empty;
        }

        public KategoriCustomerViewModel(Kategori item)
        {
            NamaKategori = item.NamaKategori;
            DeskripsiKategori = item.DeskripsiKategori;
            Icon = item.Icon;
        }

        public string NamaKategori { get; set; }
        public string DeskripsiKategori { get; set; }
        public string? Icon { get; set; }
    }
}
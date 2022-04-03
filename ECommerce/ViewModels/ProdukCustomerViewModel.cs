using System.ComponentModel.DataAnnotations;
using eCommerce.Datas.Entities;

namespace eCommerce.ViewModels{
public class ProdukCustomerViewModel
{
    public ProdukCustomerViewModel()
    {  
        Kategories = new List<KategoriViewModel>();
    }
    public ProdukCustomerViewModel(int id, string nama, string deskripsi, decimal harga)
    {
        IdProduk = id;
        NamaProduk = nama;
        DeskripsiProduk = deskripsi;
        HargaProduk = harga;
        Stok = 100;
        Kategories = new List<KategoriViewModel>();
    }

    public int IdProduk { get; set; }
    public string NamaProduk { get; set; } = null!;
    public string DeskripsiProduk { get; set; } = null!;
    public int Stok { get; set; }

    public decimal HargaProduk { get; set; }
    public string? Gambar { get; set; } 
    public int JumlahBarang {get; set;}
    public List<KategoriViewModel> Kategories { get; set; }

    public Produk ConvertToDbModel(){
        return new Produk() {
            IdProduk = this.IdProduk,
            NamaProduk = this.NamaProduk,
            DeskripsiProduk = this.DeskripsiProduk,
            HargaProduk = this.HargaProduk,
            Gambar = this.Gambar ?? string.Empty,
            Stok = this.Stok,
        };
    }
}
}

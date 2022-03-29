using System.ComponentModel.DataAnnotations;
using eCommerce.Datas.Entities;

namespace eCommerce.ViewModels{
public class ProdukViewModel
{
    public ProdukViewModel()
    {  
    }
    public ProdukViewModel(int id, string nama, string deskripsi, decimal harga)
    {
        IdProduk = id;
        NamaProduk = nama;
        DeskripsiProduk = deskripsi;
        HargaProduk = harga;
        Stok = 100;
    }
    public int IdProduk { get; set; }
    [Required]
    public string NamaProduk { get; set; } = null!;
    public string DeskripsiProduk { get; set; } = null!;
    [Required]
    public decimal HargaProduk { get; set; }
    public int Stok { get; set; }
    public string? Gambar { get; set; }
    public int KategoriId { get; set; }
    public string? NamaKategori { get; set; }

    public Produk ConvertToDbModel(){
        return new Produk() {
            IdProduk = this.IdProduk,
            NamaProduk = this.NamaProduk,
            DeskripsiProduk = this.DeskripsiProduk,
            HargaProduk = this.HargaProduk,
            Gambar = this.Gambar ?? string.Empty,
            Stok = this.Stok
        };
    }
}
}

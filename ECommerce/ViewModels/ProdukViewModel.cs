using System.ComponentModel.DataAnnotations;
using eCommerce.Datas.Entities;

namespace eCommerce.ViewModels{
public class ProdukViewModel
{
    public ProdukViewModel()
    {  
        Kategories = new List<KategoriViewModel>();
    }
    public ProdukViewModel(int id, string nama, string deskripsi, decimal harga)
    {
        IdProduk = id;
        NamaProduk = nama;
        DeskripsiProduk = deskripsi;
        HargaProduk = harga;
        KategoriId = Array.Empty<int>();
        Kategories = new List<KategoriViewModel>();
    }

    public int IdProduk { get; set; }
    [Required]
    public string NamaProduk { get; set; } = null!;
    public string DeskripsiProduk { get; set; } = null!;
    [Required]
    public decimal HargaProduk { get; set; }
    public int Stok {get; set;}

    public string? Gambar { get; set; }
    public string GambarSrc {
        get {
            return (string.IsNullOrEmpty(Gambar) ? "images/default.png" : Gambar );
        }
    }
    public IFormFile? GambarFile { get; set; }
    public int[] KategoriId { get; set; }
    public List<KategoriViewModel> Kategories { get; set; }

    public Produk ConvertToDbModel(){
        return new Produk() {
            IdProduk = this.IdProduk,
            NamaProduk = this.NamaProduk,
            DeskripsiProduk = this.DeskripsiProduk,
            HargaProduk = this.HargaProduk,
            Gambar = this.Gambar ?? string.Empty,
        };
    }
}
}

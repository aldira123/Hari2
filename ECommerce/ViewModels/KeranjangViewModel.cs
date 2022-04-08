using eCommerce.Datas.Entities;
namespace eCommerce.ViewModels
{
    public partial class KeranjangViewModel
    {
        public KeranjangViewModel()
        {
            
        }

        public KeranjangViewModel(Keranjang item)
        {
            IdKeranjang = item.IdKeranjang;
            IdProduk = item.IdProduk;
            IdCustomer = item.IdCustomer;
            JumlahBarang = item.JumlahBarang;
            Subtotal = item.Subtotal;
        }


        public int IdKeranjang { get; set; }
        public int IdProduk { get; set; }
        public string NamaProduk{get; set;}
        public string Image{get; set;}
        public int IdCustomer { get; set; }
        public int JumlahBarang { get; set; }
        public decimal Subtotal { get; set; }
        public decimal HargaBarang { get; set; }
        public int Alamat{get;set;}

        public Keranjang ConvertToDbModel()
        {
            return new Keranjang
            {
                IdKeranjang = this.IdKeranjang,
                IdProduk = this.IdProduk,
                IdCustomer = this.IdCustomer,
                JumlahBarang = this.JumlahBarang,
                Subtotal = this.Subtotal,
            };
        }
        


    }
}
using eCommerce.Datas.Entities;
namespace eCommerce.ViewModels;

public class AlamatViewModel
{
    public AlamatViewModel()
    {
        Kecamatan = string.Empty;
        Kelurahan = string.Empty;
        Rt = string.Empty;
        Rw = string.Empty;
        KodePos = string.Empty;
        Detail = string.Empty;
    }

    public AlamatViewModel(Alamat item)
    {
        IdAlamat = item.IdAlamat;
        IdCustomer = item.IdCustomer;
        Kecamatan = item.Kecamatan;
        Kelurahan = item.Kelurahan;
        Rt = item.Rt;
        Rw = item.Rw;
        KodePos = item.KodePos;
        Detail = item.Detail;

    }

    public int IdAlamat { get; set; }
    public int IdCustomer { get; set; }
    public string Kecamatan { get; set; } = null!;
    public string Kelurahan { get; set; } = null!;
    public string Rt { get; set; } = null!;
    public string Rw { get; set; } = null!;
    public string KodePos { get; set; } = null!;
    public string Detail { get; set; } = null!;

    public Alamat ConvertToDbModel()
    {
        return new Alamat
        {
        IdAlamat = this.IdAlamat,
        IdCustomer = this.IdCustomer,
        Kecamatan = this.Kecamatan,
        Kelurahan = this.Kelurahan,
        Rt = this.Rt,
        Rw = this.Rw,
        KodePos = this.KodePos,
        Detail = this.Detail,
    };
}

}

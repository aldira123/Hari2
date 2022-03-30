using eCommerce.Datas.Entities;
namespace eCommerce.ViewModels
{
    public class AdminViewModel
    {
        public AdminViewModel()
        {
            Nama = string.Empty;
                NoHp = string.Empty;;
                Username = string.Empty;;
                Password = string.Empty;;
                Email = string.Empty;
        }

        public AdminViewModel(Admin item)
        {
            IdAdmin = item.IdAdmin;
                Nama = item.Nama;
                NoHp = item.NoHp;
                Username = item.Username;
                Password = item.Password;
                Email = item.Email;
        }

        public int IdAdmin { get; set; }
        public string Nama { get; set; } = null!;
        public string? NoHp { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
         public string? Email { get; set; }

        public Admin ConvertToDbModel()
        {
            return new Admin
            {
                IdAdmin = this.IdAdmin,
                Nama = this.Nama,
                NoHp = this.NoHp,
                Username = this.Username,
                Password = this.Password,
                Email = this.Email,
            };
        }

    }
}
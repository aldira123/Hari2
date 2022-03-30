using System.ComponentModel.DataAnnotations;
using eCommerce.Datas.Entities;
namespace eCommerce.ViewModels
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            Nama = string.Empty;
                NoHp = string.Empty;
                Username = string.Empty;
                Password = string.Empty;
                Email = string.Empty;
        }

        public RegisterViewModel(Customer item)
        {
                IdCustomer = item.IdCustomer;
                Nama = item.Nama;
                NoHp = item.NoHp;
                Username = item.Username;
                Password = item.Password;
                Email = item.Email;
        }

       public int IdCustomer { get; set; }
       [Required]
        public string Nama { get; set; } = null!;
        public string NoHp { get; set; } = null!;
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ConfirmPassword { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;

        public Customer ConvertToDbModel()
        {
            return new Customer
            {
                IdCustomer= this.IdCustomer,
                Nama = this.Nama,
                NoHp = this.NoHp,
                Username = this.Username,
                Password = this.Password,
                Email = this.Email,
            };
        }

    }
}
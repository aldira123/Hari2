using System;
using System.Collections.Generic;

namespace eCommerce.Datas.Entities
{
    public partial class Admin
    {
        public int IdAdmin { get; set; }
        public string Nama { get; set; } = null!;
        public string? NoHp { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
         public string? Email { get; set; }
    }
}

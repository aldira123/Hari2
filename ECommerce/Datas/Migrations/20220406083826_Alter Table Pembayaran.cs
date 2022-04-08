using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Datas.Migrations
{
    public partial class AlterTablePembayaran : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "catatan",
                table: "pembayaran",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                collation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "catatan",
                table: "pembayaran");
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Datas.Migrations
{
    public partial class AddTableDetailOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__keranjang",
                table: "order");

            migrationBuilder.DropIndex(
                name: "FK__keranjang",
                table: "order");

            migrationBuilder.DropColumn(
                name: "id_keranjang",
                table: "order");

            migrationBuilder.CreateTable(
                name: "detail_order",
                columns: table => new
                {
                    id_detail = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_order = table.Column<int>(type: "int(11)", nullable: false),
                    id_produk = table.Column<int>(type: "int(11)", nullable: false),
                    harga = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    jumlah_barang = table.Column<int>(type: "int(11)", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_detail);
                    table.ForeignKey(
                        name: "detail_order_FK_1",
                        column: x => x.id_order,
                        principalTable: "order",
                        principalColumn: "id_order");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "detail_order_FK_1",
                table: "detail_order",
                column: "id_order");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detail_order");

            migrationBuilder.AddColumn<int>(
                name: "id_keranjang",
                table: "order",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "FK__keranjang",
                table: "order",
                column: "id_keranjang");

            migrationBuilder.AddForeignKey(
                name: "FK__keranjang",
                table: "order",
                column: "id_keranjang",
                principalTable: "keranjang",
                principalColumn: "id_keranjang");
        }
    }
}

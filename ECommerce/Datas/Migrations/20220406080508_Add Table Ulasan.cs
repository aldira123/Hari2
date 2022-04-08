using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Datas.Migrations
{
    public partial class AddTableUlasan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Noresi",
                table: "pengiriman",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ulasan",
                columns: table => new
                {
                    id_ulasan = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_order = table.Column<int>(type: "int(11)", nullable: false),
                    id_customer = table.Column<int>(type: "int(11)", nullable: false),
                    komentar = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gambar = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rating = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_ulasan);
                    table.ForeignKey(
                        name: "FK__customer_Ulasan",
                        column: x => x.id_customer,
                        principalTable: "customer",
                        principalColumn: "id_customer");
                    table.ForeignKey(
                        name: "FK__Order_Ulasan",
                        column: x => x.id_order,
                        principalTable: "order",
                        principalColumn: "id_order");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "FK__customer_Ulasan",
                table: "ulasan",
                column: "id_customer");

            migrationBuilder.CreateIndex(
                name: "FK__Order_Ulasan",
                table: "ulasan",
                column: "id_order");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ulasan");

            migrationBuilder.DropColumn(
                name: "Noresi",
                table: "pengiriman");
        }
    }
}

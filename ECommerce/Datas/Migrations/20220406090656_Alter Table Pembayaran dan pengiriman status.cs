using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Datas.Migrations
{
    public partial class AlterTablePembayarandanpengirimanstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pembayaran_status_order",
                table: "pembayaran");

            migrationBuilder.DropForeignKey(
                name: "FK_pengiriman_status_order",
                table: "pengiriman");

            migrationBuilder.DropIndex(
                name: "FK_pengiriman_status_order",
                table: "pengiriman");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "pengiriman",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(int),
                oldType: "int(11)")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Noresi",
                table: "pengiriman",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.AddColumn<int>(
                name: "StatusOrderIdStatus",
                table: "pengiriman",
                type: "int(11)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "pembayaran",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(int),
                oldType: "int(11)")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "StatusOrderIdStatus",
                table: "pembayaran",
                type: "int(11)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_pengiriman_StatusOrderIdStatus",
                table: "pengiriman",
                column: "StatusOrderIdStatus");

            migrationBuilder.CreateIndex(
                name: "IX_pembayaran_StatusOrderIdStatus",
                table: "pembayaran",
                column: "StatusOrderIdStatus");

            migrationBuilder.AddForeignKey(
                name: "FK_pembayaran_status_order_StatusOrderIdStatus",
                table: "pembayaran",
                column: "StatusOrderIdStatus",
                principalTable: "status_order",
                principalColumn: "id_status");

            migrationBuilder.AddForeignKey(
                name: "FK_pengiriman_status_order_StatusOrderIdStatus",
                table: "pengiriman",
                column: "StatusOrderIdStatus",
                principalTable: "status_order",
                principalColumn: "id_status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pembayaran_status_order_StatusOrderIdStatus",
                table: "pembayaran");

            migrationBuilder.DropForeignKey(
                name: "FK_pengiriman_status_order_StatusOrderIdStatus",
                table: "pengiriman");

            migrationBuilder.DropIndex(
                name: "IX_pengiriman_StatusOrderIdStatus",
                table: "pengiriman");

            migrationBuilder.DropIndex(
                name: "IX_pembayaran_StatusOrderIdStatus",
                table: "pembayaran");

            migrationBuilder.DropColumn(
                name: "StatusOrderIdStatus",
                table: "pengiriman");

            migrationBuilder.DropColumn(
                name: "StatusOrderIdStatus",
                table: "pembayaran");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "pengiriman",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.UpdateData(
                table: "pengiriman",
                keyColumn: "Noresi",
                keyValue: null,
                column: "Noresi",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Noresi",
                table: "pengiriman",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "pembayaran",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "FK_pengiriman_status_order",
                table: "pengiriman",
                column: "status");

            migrationBuilder.AddForeignKey(
                name: "FK_pembayaran_status_order",
                table: "pembayaran",
                column: "status",
                principalTable: "status_order",
                principalColumn: "id_status");

            migrationBuilder.AddForeignKey(
                name: "FK_pengiriman_status_order",
                table: "pengiriman",
                column: "status",
                principalTable: "status_order",
                principalColumn: "id_status");
        }
    }
}

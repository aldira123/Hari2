using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Datas.Migrations
{
    public partial class AlterEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_pembayaran_status_order",
                table: "pembayaran");

            migrationBuilder.DropIndex(
                name: "IX_pembayaran_StatusOrderIdStatus",
                table: "pembayaran");

            migrationBuilder.DropColumn(
                name: "StatusOrderIdStatus",
                table: "pengiriman");

            migrationBuilder.DropColumn(
                name: "StatusOrderIdStatus",
                table: "pembayaran");

            migrationBuilder.RenameColumn(
                name: "Noresi",
                table: "pengiriman",
                newName: "no_resi");

            migrationBuilder.AlterColumn<string>(
                name: "no_resi",
                table: "pengiriman",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "tanggal_bayar",
                table: "pembayaran",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "pembayaran",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "no_resi",
                table: "pengiriman",
                newName: "Noresi");

            migrationBuilder.AlterColumn<string>(
                name: "Noresi",
                table: "pengiriman",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.AddColumn<int>(
                name: "StatusOrderIdStatus",
                table: "pengiriman",
                type: "int(11)",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "tanggal_bayar",
                table: "pembayaran",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.UpdateData(
                table: "pembayaran",
                keyColumn: "status",
                keyValue: null,
                column: "status",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "pembayaran",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

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
                name: "FK_pembayaran_status_order",
                table: "pembayaran",
                column: "status");

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
    }
}

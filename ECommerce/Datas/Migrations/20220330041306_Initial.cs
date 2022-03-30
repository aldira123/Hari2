using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Datas.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "admin",
                columns: table => new
                {
                    id_admin = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nama = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    no_hp = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_admin);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id_customer = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nama = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    no_hp = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    foto_profile = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_customer);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "kategori",
                columns: table => new
                {
                    id_kategori = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nama_kategori = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deskripsi_kategori = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    icon = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_kategori);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "produk",
                columns: table => new
                {
                    id_produk = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nama_produk = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deskripsi_produk = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    harga_produk = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    stok = table.Column<int>(type: "int(11)", nullable: false),
                    gambar = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_produk);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "status_order",
                columns: table => new
                {
                    id_status = table.Column<int>(type: "int(11)", nullable: false),
                    nama = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deskripsi = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_status);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "alamat",
                columns: table => new
                {
                    id_alamat = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_customer = table.Column<int>(type: "int(11)", nullable: false),
                    kecamatan = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValueSql: "''", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    kelurahan = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValueSql: "''", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rt = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, defaultValueSql: "''", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rw = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, defaultValueSql: "''", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    kode_pos = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false, defaultValueSql: "''", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    detail = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, defaultValueSql: "''", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_alamat);
                    table.ForeignKey(
                        name: "FK__customer",
                        column: x => x.id_customer,
                        principalTable: "customer",
                        principalColumn: "id_customer");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "keranjang",
                columns: table => new
                {
                    id_keranjang = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_produk = table.Column<int>(type: "int(11)", nullable: false),
                    id_customer = table.Column<int>(type: "int(11)", nullable: false),
                    jumlah_barang = table.Column<int>(type: "int(11)", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_keranjang);
                    table.ForeignKey(
                        name: "FK__customer_keranjang",
                        column: x => x.id_customer,
                        principalTable: "customer",
                        principalColumn: "id_customer");
                    table.ForeignKey(
                        name: "FK__produk_keranjang",
                        column: x => x.id_produk,
                        principalTable: "produk",
                        principalColumn: "id_produk");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "produk_kategori",
                columns: table => new
                {
                    id_produk_kategori = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_produk = table.Column<int>(type: "int(11)", nullable: false),
                    id_kategori = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_produk_kategori);
                    table.ForeignKey(
                        name: "FK__kategori_produk",
                        column: x => x.id_kategori,
                        principalTable: "kategori",
                        principalColumn: "id_kategori");
                    table.ForeignKey(
                        name: "FK__produk",
                        column: x => x.id_produk,
                        principalTable: "produk",
                        principalColumn: "id_produk");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id_order = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_keranjang = table.Column<int>(type: "int(11)", nullable: false),
                    tgl_transaksi = table.Column<DateOnly>(type: "date", nullable: false),
                    jumlah_bayar = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    id_alamat = table.Column<int>(type: "int(11)", nullable: false),
                    id_customer = table.Column<int>(type: "int(11)", nullable: false),
                    status = table.Column<int>(type: "int(11)", nullable: false),
                    catatan = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_order);
                    table.ForeignKey(
                        name: "FK__alamat",
                        column: x => x.id_alamat,
                        principalTable: "alamat",
                        principalColumn: "id_alamat");
                    table.ForeignKey(
                        name: "FK__customer_order",
                        column: x => x.id_customer,
                        principalTable: "customer",
                        principalColumn: "id_customer");
                    table.ForeignKey(
                        name: "FK__keranjang",
                        column: x => x.id_keranjang,
                        principalTable: "keranjang",
                        principalColumn: "id_keranjang");
                    table.ForeignKey(
                        name: "FK_order_status_order",
                        column: x => x.status,
                        principalTable: "status_order",
                        principalColumn: "id_status");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "pembayaran",
                columns: table => new
                {
                    id_pembayaran = table.Column<int>(type: "int(11)", nullable: false),
                    metode_pembayaran = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tanggal_bayar = table.Column<DateOnly>(type: "date", nullable: false),
                    id_order = table.Column<int>(type: "int(11)", nullable: false),
                    id_customer = table.Column<int>(type: "int(11)", nullable: false),
                    jumlah_bayar = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    tujuan = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pajak = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    status = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_pembayaran);
                    table.ForeignKey(
                        name: "FK__customer_pembayaran",
                        column: x => x.id_customer,
                        principalTable: "customer",
                        principalColumn: "id_customer");
                    table.ForeignKey(
                        name: "FK__order_pembayaran",
                        column: x => x.id_order,
                        principalTable: "order",
                        principalColumn: "id_order");
                    table.ForeignKey(
                        name: "FK_pembayaran_status_order",
                        column: x => x.status,
                        principalTable: "status_order",
                        principalColumn: "id_status");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "pengiriman",
                columns: table => new
                {
                    id_pengiriman = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_order = table.Column<int>(type: "int(11)", nullable: false),
                    kurir = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValueSql: "''", collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ongkir = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    id_alamat = table.Column<int>(type: "int(11)", nullable: false),
                    status = table.Column<int>(type: "int(11)", nullable: false),
                    keterangan = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_pengiriman);
                    table.ForeignKey(
                        name: "FK__alamat_pengiriman",
                        column: x => x.id_alamat,
                        principalTable: "alamat",
                        principalColumn: "id_alamat");
                    table.ForeignKey(
                        name: "FK__order_pengiriman",
                        column: x => x.id_order,
                        principalTable: "order",
                        principalColumn: "id_order");
                    table.ForeignKey(
                        name: "FK_pengiriman_status_order",
                        column: x => x.status,
                        principalTable: "status_order",
                        principalColumn: "id_status");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "FK__customer",
                table: "alamat",
                column: "id_customer");

            migrationBuilder.CreateIndex(
                name: "FK__customer_keranjang",
                table: "keranjang",
                column: "id_customer");

            migrationBuilder.CreateIndex(
                name: "FK__produk_keranjang",
                table: "keranjang",
                column: "id_produk");

            migrationBuilder.CreateIndex(
                name: "FK__alamat",
                table: "order",
                column: "id_alamat");

            migrationBuilder.CreateIndex(
                name: "FK__customer_order",
                table: "order",
                column: "id_customer");

            migrationBuilder.CreateIndex(
                name: "FK__keranjang",
                table: "order",
                column: "id_keranjang");

            migrationBuilder.CreateIndex(
                name: "FK_order_status_order",
                table: "order",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "FK__customer_pembayaran",
                table: "pembayaran",
                column: "id_customer");

            migrationBuilder.CreateIndex(
                name: "FK__order_pembayaran",
                table: "pembayaran",
                column: "id_order");

            migrationBuilder.CreateIndex(
                name: "FK_pembayaran_status_order",
                table: "pembayaran",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "FK__alamat_pengiriman",
                table: "pengiriman",
                column: "id_alamat");

            migrationBuilder.CreateIndex(
                name: "FK__order_pengiriman",
                table: "pengiriman",
                column: "id_order");

            migrationBuilder.CreateIndex(
                name: "FK_pengiriman_status_order",
                table: "pengiriman",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "FK__kategori_produk",
                table: "produk_kategori",
                column: "id_kategori");

            migrationBuilder.CreateIndex(
                name: "FK__produk",
                table: "produk_kategori",
                column: "id_produk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin");

            migrationBuilder.DropTable(
                name: "pembayaran");

            migrationBuilder.DropTable(
                name: "pengiriman");

            migrationBuilder.DropTable(
                name: "produk_kategori");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "kategori");

            migrationBuilder.DropTable(
                name: "alamat");

            migrationBuilder.DropTable(
                name: "keranjang");

            migrationBuilder.DropTable(
                name: "status_order");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "produk");
        }
    }
}

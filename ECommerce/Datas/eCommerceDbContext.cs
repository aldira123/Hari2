using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using eCommerce.Datas.Entities;

namespace eCommerce.Datas
{
    public partial class eCommerceDbContext : DbContext
    {
        public eCommerceDbContext()
        {
        }

        public eCommerceDbContext(DbContextOptions<eCommerceDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Alamat> Alamats { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Kategori> Kategoris { get; set; } = null!;
        public virtual DbSet<Keranjang> Keranjangs { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Pembayaran> Pembayarans { get; set; } = null!;
        public virtual DbSet<Pengiriman> Pengirimen { get; set; } = null!;
        public virtual DbSet<Produk> Produks { get; set; } = null!;
        public virtual DbSet<ProdukKategori> ProdukKategoris { get; set; } = null!;
        public virtual DbSet<StatusOrder> StatusOrders { get; set; } = null!;
        public virtual DbSet<DetailOrder> DetailOrders { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.IdAdmin)
                    .HasName("PRIMARY");

                entity.ToTable("admin");

                entity.Property(e => e.IdAdmin)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_admin");

                entity.Property(e => e.Nama)
                    .HasMaxLength(50)
                    .HasColumnName("nama");

                entity.Property(e => e.NoHp)
                    .HasMaxLength(15)
                    .HasColumnName("no_hp");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                    entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");
            });

            modelBuilder.Entity<Alamat>(entity =>
            {
                entity.HasKey(e => e.IdAlamat)
                    .HasName("PRIMARY");

                entity.ToTable("alamat");

                entity.HasIndex(e => e.IdCustomer, "FK__customer");

                entity.Property(e => e.IdAlamat)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_alamat");

                entity.Property(e => e.Detail)
                    .HasMaxLength(255)
                    .HasColumnName("detail")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.IdCustomer)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_customer");

                entity.Property(e => e.Kecamatan)
                    .HasMaxLength(50)
                    .HasColumnName("kecamatan")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Kelurahan)
                    .HasMaxLength(50)
                    .HasColumnName("kelurahan")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.KodePos)
                    .HasMaxLength(5)
                    .HasColumnName("kode_pos")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Rt)
                    .HasMaxLength(10)
                    .HasColumnName("rt")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Rw)
                    .HasMaxLength(10)
                    .HasColumnName("rw")
                    .HasDefaultValueSql("''");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Alamats)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__customer");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.IdCustomer)
                    .HasName("PRIMARY");

                entity.ToTable("customer");

                entity.Property(e => e.IdCustomer)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_customer");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.FotoProfile)
                    .HasMaxLength(255)
                    .HasColumnName("foto_profile");

                entity.Property(e => e.Nama)
                    .HasMaxLength(50)
                    .HasColumnName("nama");

                entity.Property(e => e.NoHp)
                    .HasMaxLength(50)
                    .HasColumnName("no_hp");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Kategori>(entity =>
            {
                entity.HasKey(e => e.IdKategori)
                    .HasName("PRIMARY");

                entity.ToTable("kategori");

                entity.Property(e => e.IdKategori)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_kategori");

                entity.Property(e => e.DeskripsiKategori)
                    .HasMaxLength(255)
                    .HasColumnName("deskripsi_kategori");

                entity.Property(e => e.Icon)
                    .HasMaxLength(255)
                    .HasColumnName("icon");

                entity.Property(e => e.NamaKategori)
                    .HasMaxLength(50)
                    .HasColumnName("nama_kategori");
            });

            modelBuilder.Entity<Keranjang>(entity =>
            {
                entity.HasKey(e => e.IdKeranjang)
                    .HasName("PRIMARY");

                entity.ToTable("keranjang");

                entity.HasIndex(e => e.IdCustomer, "FK__customer_keranjang");

                entity.HasIndex(e => e.IdProduk, "FK__produk_keranjang");

                entity.Property(e => e.IdKeranjang)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_keranjang");

                entity.Property(e => e.IdCustomer)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_customer");

                entity.Property(e => e.IdProduk)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_produk");

                entity.Property(e => e.JumlahBarang)
                    .HasColumnType("int(11)")
                    .HasColumnName("jumlah_barang");

                entity.Property(e => e.Subtotal)
                    .HasPrecision(10, 2)
                    .HasColumnName("subtotal");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Keranjangs)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__customer_keranjang");

                entity.HasOne(d => d.IdProdukNavigation)
                    .WithMany(p => p.Keranjangs)
                    .HasForeignKey(d => d.IdProduk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__produk_keranjang");
            });

            modelBuilder.Entity<DetailOrder>(entity => {
                
                entity.ToTable("detail_order");
                entity.HasKey(e => e.IdDetail)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.IdOrder, "detail_order_FK_1");

                entity.Property(e => e.IdDetail)
                .HasColumnType("int(11)")
                .HasColumnName("id_detail");

                entity.Property(e => e.IdOrder)
                .HasColumnType("int(11)")
                .HasColumnName("id_order");

                entity.Property(e => e.IdProduk)
                .HasColumnType("int(11)")
                .HasColumnName("id_produk");

                 entity.Property(e => e.Harga)
                    .HasPrecision(10, 2)
                    .HasColumnName("harga");
                
                entity.Property(e => e.JumlahBarang)
                .HasColumnType("int(11)")
                .HasColumnName("jumlah_barang");
                
                entity.Property(e => e.SubTotal).HasColumnName("subtotal")
                .HasPrecision(10,2);
                
                entity.HasOne(d => d.Order)
                    .WithMany(p => p.DetailOrders)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("detail_order_FK_1");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PRIMARY");

                entity.ToTable("order");

                entity.HasIndex(e => e.IdAlamat, "FK__alamat");

                entity.HasIndex(e => e.IdCustomer, "FK__customer_order");

                entity.HasIndex(e => e.Status, "FK_order_status_order");

                entity.Property(e => e.IdOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_order");

                entity.Property(e => e.Catatan)
                    .HasMaxLength(255)
                    .HasColumnName("catatan");

                entity.Property(e => e.IdAlamat)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_alamat");

                entity.Property(e => e.IdCustomer)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_customer");

                entity.Property(e => e.JumlahBayar)
                    .HasPrecision(10, 2)
                    .HasColumnName("jumlah_bayar");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status");

                entity.Property(e => e.TglTransaksi)
                .HasColumnType("datetime")
                .HasColumnName("tgl_transaksi");

                entity.HasOne(d => d.IdAlamatNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdAlamat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__alamat");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__customer_order");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_order_status_order");
            });

            modelBuilder.Entity<Pembayaran>(entity =>
            {
                entity.HasKey(e => e.IdPembayaran)
                    .HasName("PRIMARY");

                entity.ToTable("pembayaran");

                entity.HasIndex(e => e.IdCustomer, "FK__customer_pembayaran");

                entity.HasIndex(e => e.IdOrder, "FK__order_pembayaran");

                entity.HasIndex(e => e.Status, "FK_pembayaran_status_order");

                entity.Property(e => e.IdPembayaran)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id_pembayaran");

                entity.Property(e => e.IdCustomer)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_customer");

                entity.Property(e => e.IdOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_order");

                entity.Property(e => e.JumlahBayar)
                    .HasPrecision(10, 2)
                    .HasColumnName("jumlah_bayar");

                entity.Property(e => e.MetodePembayaran)
                    .HasMaxLength(50)
                    .HasColumnName("metode_pembayaran");

                entity.Property(e => e.Pajak)
                    .HasPrecision(10, 2)
                    .HasColumnName("pajak");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status");

                entity.Property(e => e.TanggalBayar).HasColumnName("tanggal_bayar");

                entity.Property(e => e.Tujuan)
                    .HasMaxLength(50)
                    .HasColumnName("tujuan");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Pembayarans)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__customer_pembayaran");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Pembayarans)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__order_pembayaran");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Pembayarans)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pembayaran_status_order");
            });

            modelBuilder.Entity<Pengiriman>(entity =>
            {
                entity.HasKey(e => e.IdPengiriman)
                    .HasName("PRIMARY");

                entity.ToTable("pengiriman");

                entity.HasIndex(e => e.IdAlamat, "FK__alamat_pengiriman");

                entity.HasIndex(e => e.IdOrder, "FK__order_pengiriman");

                entity.HasIndex(e => e.Status, "FK_pengiriman_status_order");

                entity.Property(e => e.IdPengiriman)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_pengiriman");

                entity.Property(e => e.IdAlamat)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_alamat");

                entity.Property(e => e.IdOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_order");

                entity.Property(e => e.Keterangan)
                    .HasMaxLength(255)
                    .HasColumnName("keterangan");

                entity.Property(e => e.Kurir)
                    .HasMaxLength(50)
                    .HasColumnName("kurir")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Ongkir)
                    .HasPrecision(10, 2)
                    .HasColumnName("ongkir");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status");

                entity.HasOne(d => d.IdAlamatNavigation)
                    .WithMany(p => p.Pengirimen)
                    .HasForeignKey(d => d.IdAlamat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__alamat_pengiriman");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Pengirimen)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__order_pengiriman");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Pengirimen)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pengiriman_status_order");
            });

            modelBuilder.Entity<Produk>(entity =>
            {
                entity.HasKey(e => e.IdProduk)
                    .HasName("PRIMARY");

                entity.ToTable("produk");

                entity.Property(e => e.IdProduk)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_produk");

                entity.Property(e => e.DeskripsiProduk)
                    .HasMaxLength(255)
                    .HasColumnName("deskripsi_produk");

                entity.Property(e => e.Gambar)
                    .HasMaxLength(255)
                    .HasColumnName("gambar");

                entity.Property(e => e.HargaProduk)
                    .HasPrecision(10, 2)
                    .HasColumnName("harga_produk");

                entity.Property(e => e.NamaProduk)
                    .HasMaxLength(50)
                    .HasColumnName("nama_produk");

                entity.Property(e => e.Stok)
                    .HasColumnType("int(11)")
                    .HasColumnName("stok");
            });

            modelBuilder.Entity<ProdukKategori>(entity =>
            {
                entity.HasKey(e => e.IdProdukKategori)
                    .HasName("PRIMARY");

                entity.ToTable("produk_kategori");

                entity.HasIndex(e => e.IdKategori, "FK__kategori_produk");

                entity.HasIndex(e => e.IdProduk, "FK__produk");

                entity.Property(e => e.IdProdukKategori)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_produk_kategori");

                entity.Property(e => e.IdKategori)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_kategori");

                entity.Property(e => e.IdProduk)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_produk");

                entity.HasOne(d => d.IdKategoriNavigation)
                    .WithMany(p => p.ProdukKategoris)
                    .HasForeignKey(d => d.IdKategori)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__kategori_produk");

                entity.HasOne(d => d.IdProdukNavigation)
                    .WithMany(p => p.ProdukKategoris)
                    .HasForeignKey(d => d.IdProduk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__produk");
            });

            modelBuilder.Entity<StatusOrder>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("PRIMARY");

                entity.ToTable("status_order");

                entity.Property(e => e.IdStatus)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id_status");

                entity.Property(e => e.Deskripsi)
                    .HasMaxLength(255)
                    .HasColumnName("deskripsi");

                entity.Property(e => e.Nama)
                    .HasMaxLength(50)
                    .HasColumnName("nama");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

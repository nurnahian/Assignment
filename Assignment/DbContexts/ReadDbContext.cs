using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Assignment.Models.Read;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assignment.DbContexts
{
    public partial class ReadDbContext : DbContext
    {
        public ReadDbContext()
        {
        }

        public ReadDbContext(DbContextOptions<ReadDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblItem> TblItem { get; set; }
        public virtual DbSet<TblPartner> TblPartner { get; set; }
        public virtual DbSet<TblPartnerType> TblPartnerType { get; set; }
        public virtual DbSet<TblPurchase> TblPurchase { get; set; }
        public virtual DbSet<TblPurchaseDetails> TblPurchaseDetails { get; set; }
        public virtual DbSet<TblSales> TblSales { get; set; }
        public virtual DbSet<TblSalesDetails> TblSalesDetails { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-9HPLJID\\SQLEXPRESS;Initial Catalog = ibosDb;Connect Timeout=30;Encrypt=False;Trusted_Connection=True;ApplicationIntent=ReadWrite;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblItem>(entity =>
            {
                entity.HasKey(e => e.IntItemId)
                    .HasName("PK__tblItem__FA6F1B1267549237");

                entity.ToTable("tblItem");

                entity.Property(e => e.IntItemId).HasColumnName("intItemId");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.NumStockQuantity)
                    .HasColumnName("numStockQuantity")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.StrItemName)
                    .HasColumnName("strItemName")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblPartner>(entity =>
            {
                entity.HasKey(e => e.IntPartnerId)
                    .HasName("PK__tblPartn__279F30387396EA85");

                entity.ToTable("tblPartner");

                entity.Property(e => e.IntPartnerId).HasColumnName("intPartnerId");

                entity.Property(e => e.IntPartnerTypeId).HasColumnName("intPartnerTypeId");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.StrPartnerName)
                    .HasColumnName("strPartnerName")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblPartnerType>(entity =>
            {
                entity.HasKey(e => e.IntPartnerTypeId)
                    .HasName("PK__tblPartn__3530195364956B28");

                entity.ToTable("tblPartnerType");

                entity.Property(e => e.IntPartnerTypeId).HasColumnName("intPartnerTypeId");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.StrPartnerTypeName)
                    .HasColumnName("strPartnerTypeName")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblPurchase>(entity =>
            {
                entity.HasKey(e => e.IntPurchaseId)
                    .HasName("PK__tblPurch__39AFE605FC977C1D");

                entity.ToTable("tblPurchase");

                entity.Property(e => e.IntPurchaseId).HasColumnName("intPurchaseId");

                entity.Property(e => e.DtePurchaseDate)
                    .HasColumnName("dtePurchaseDate")
                    .HasColumnType("date");

                entity.Property(e => e.IntSupplierId).HasColumnName("intSupplierId");

                entity.Property(e => e.IsActive).HasColumnName("isActive");
            });

            modelBuilder.Entity<TblPurchaseDetails>(entity =>
            {
                entity.HasKey(e => e.IntDetailsId)
                    .HasName("PK__tblPurch__0A1B5AF365F57D0E");

                entity.ToTable("tblPurchaseDetails");

                entity.Property(e => e.IntDetailsId).HasColumnName("intDetailsId");

                entity.Property(e => e.IntItemId).HasColumnName("intItemId");

                entity.Property(e => e.IntPurchaseId).HasColumnName("intPurchaseId");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.NumItemQuantity)
                    .HasColumnName("numItemQuantity")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.NumUnitPrice)
                    .HasColumnName("numUnitPrice")
                    .HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<TblSales>(entity =>
            {
                entity.HasKey(e => e.IntSalesId)
                    .HasName("PK__tblSales__754F6C55287B36F5");

                entity.ToTable("tblSales");

                entity.Property(e => e.IntSalesId).HasColumnName("intSalesId");

                entity.Property(e => e.DteSalesDate)
                    .HasColumnName("dteSalesDate")
                    .HasColumnType("date");

                entity.Property(e => e.IntCustomerId).HasColumnName("intCustomerId");

                entity.Property(e => e.IsActive).HasColumnName("isActive");
            });

            modelBuilder.Entity<TblSalesDetails>(entity =>
            {
                entity.HasKey(e => e.IntDetailsId)
                    .HasName("PK__tblSales__0A1B5AF32854409F");

                entity.ToTable("tblSalesDetails");

                entity.Property(e => e.IntDetailsId).HasColumnName("intDetailsId");

                entity.Property(e => e.IntItemId).HasColumnName("intItemId");

                entity.Property(e => e.IntSalesId).HasColumnName("intSalesId");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.NumItemQuantity)
                    .HasColumnName("numItemQuantity")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.NumUnitPrice)
                    .HasColumnName("numUnitPrice")
                    .HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblUser");

                entity.Property(e => e.UserEmail).HasMaxLength(255);

                entity.Property(e => e.UserPass).HasMaxLength(255);

                entity.Property(e => e.UserRole).HasMaxLength(30);

                entity.Property(e => e.Userid).ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

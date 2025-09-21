using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIApp.Models;

public partial class CareerQuizDbContext : DbContext
{
    public CareerQuizDbContext()
    {
    }

    public CareerQuizDbContext(DbContextOptions<CareerQuizDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CauHoi> CauHois { get; set; }

    public virtual DbSet<CauTraLoi> CauTraLois { get; set; }

    public virtual DbSet<DanhGium> DanhGia { get; set; }

    public virtual DbSet<DaoTaoNganhNghe> DaoTaoNganhNghes { get; set; }

    public virtual DbSet<KetQuaChiTiet> KetQuaChiTiets { get; set; }

    public virtual DbSet<KyNang> KyNangs { get; set; }

    public virtual DbSet<KyNangCaNhan> KyNangCaNhans { get; set; }

    public virtual DbSet<LichSuTest> LichSuTests { get; set; }

    public virtual DbSet<MucLuong> MucLuongs { get; set; }

    public virtual DbSet<NganhNghe> NganhNghes { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<ThiTruongViecLam> ThiTruongViecLams { get; set; }

    public virtual DbSet<ThongKeTruyCap> ThongKeTruyCaps { get; set; }

    public virtual DbSet<YeuCauHoTro> YeuCauHoTros { get; set; }

    public virtual DbSet<YeuCauKyNang> YeuCauKyNangs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost,1433;User Id=SA;Initial Catalog=CareerQuizDB;Password=<YourStrongPassword>;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CauHoi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CauHoi__3214EC275F73F64C");

            entity.ToTable("CauHoi");

            entity.HasIndex(e => e.ThuTu, "IX_CauHoi_ThuTu");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.LoaiCauHoi).HasMaxLength(50);
            entity.Property(e => e.NoiDung).HasMaxLength(500);
            entity.Property(e => e.TrangThai).HasDefaultValue(true);
        });

        modelBuilder.Entity<CauTraLoi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CauTraLo__3214EC2756AB923E");

            entity.ToTable("CauTraLoi");

            entity.HasIndex(e => e.CauHoiId, "IX_CauTraLoi_CauHoiID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CauHoiId).HasColumnName("CauHoiID");
            entity.Property(e => e.NoiDung).HasMaxLength(500);

            entity.HasOne(d => d.CauHoi).WithMany(p => p.CauTraLois)
                .HasForeignKey(d => d.CauHoiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CauTraLoi_CauHoi");
        });

        modelBuilder.Entity<DanhGium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DanhGia__3214EC275009774E");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.NgayDanhGia)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

            entity.HasOne(d => d.NguoiDung).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.NguoiDungId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DanhGia_NguoiDung");
        });

        modelBuilder.Entity<DaoTaoNganhNghe>(entity =>
        {
            entity.HasKey(e => e.NganhNgheId).HasName("PK__DaoTaoNg__55A1EA8BAE4BCC67");

            entity.ToTable("DaoTaoNganhNghe");

            entity.Property(e => e.NganhNgheId)
                .ValueGeneratedNever()
                .HasColumnName("NganhNgheID");
            entity.Property(e => e.ChiPhi).HasMaxLength(100);
            entity.Property(e => e.HocOnline).HasDefaultValue(false);
            entity.Property(e => e.ThoiGianHoc).HasMaxLength(100);

            entity.HasOne(d => d.NganhNghe).WithOne(p => p.DaoTaoNganhNghe)
                .HasForeignKey<DaoTaoNganhNghe>(d => d.NganhNgheId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DaoTaoNganhNghe_NganhNghe");
        });

        modelBuilder.Entity<KetQuaChiTiet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KetQuaCh__3214EC275EDC0FC4");

            entity.ToTable("KetQuaChiTiet");

            entity.HasIndex(e => e.LichSuTestId, "IX_KetQuaChiTiet_LichSuTestID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.LichSuTestId).HasColumnName("LichSuTestID");
            entity.Property(e => e.NganhNgheId).HasColumnName("NganhNgheID");
            entity.Property(e => e.PhanTramPhuHop).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.LichSuTest).WithMany(p => p.KetQuaChiTiets)
                .HasForeignKey(d => d.LichSuTestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KetQuaChiTiet_LichSuTest");

            entity.HasOne(d => d.NganhNghe).WithMany(p => p.KetQuaChiTiets)
                .HasForeignKey(d => d.NganhNgheId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KetQuaChiTiet_NganhNghe");
        });

        modelBuilder.Entity<KyNang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KyNang__3214EC27B75A7E6E");

            entity.ToTable("KyNang");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MoTa).HasMaxLength(500);
            entity.Property(e => e.TenKyNang).HasMaxLength(100);
        });

        modelBuilder.Entity<KyNangCaNhan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KyNangCa__3214EC27C8FD536A");

            entity.ToTable("KyNangCaNhan");

            entity.HasIndex(e => e.LichSuTestId, "IX_KyNangCaNhan_LichSuTestID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.KyNangId).HasColumnName("KyNangID");
            entity.Property(e => e.LichSuTestId).HasColumnName("LichSuTestID");

            entity.HasOne(d => d.KyNang).WithMany(p => p.KyNangCaNhans)
                .HasForeignKey(d => d.KyNangId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KyNangCaNhan_KyNang");

            entity.HasOne(d => d.LichSuTest).WithMany(p => p.KyNangCaNhans)
                .HasForeignKey(d => d.LichSuTestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KyNangCaNhan_LichSuTest");
        });

        modelBuilder.Entity<LichSuTest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LichSuTe__3214EC27D268DFAB");

            entity.ToTable("LichSuTest");

            entity.HasIndex(e => e.NgayLamTest, "IX_LichSuTest_NgayLamTest");

            entity.HasIndex(e => e.NguoiDungId, "IX_LichSuTest_NguoiDungID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.NgayLamTest)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

            entity.HasOne(d => d.NguoiDung).WithMany(p => p.LichSuTests)
                .HasForeignKey(d => d.NguoiDungId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LichSuTest_NguoiDung");
        });

        modelBuilder.Entity<MucLuong>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MucLuong__3214EC27F35F004D");

            entity.ToTable("MucLuong");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DonViTienTe)
                .HasMaxLength(10)
                .HasDefaultValue("VND");
            entity.Property(e => e.KinhNghiem).HasMaxLength(50);
            entity.Property(e => e.MoTa).HasMaxLength(100);
            entity.Property(e => e.MucLuongMax).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.MucLuongMin).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.NganhNgheId).HasColumnName("NganhNgheID");

            entity.HasOne(d => d.NganhNghe).WithMany(p => p.MucLuongs)
                .HasForeignKey(d => d.NganhNgheId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MucLuong_NganhNghe");
        });

        modelBuilder.Entity<NganhNghe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NganhNgh__3214EC27A1C4F6B9");

            entity.ToTable("NganhNghe");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AnhMinhHoa).HasMaxLength(255);
            entity.Property(e => e.IconCss)
                .HasMaxLength(100)
                .HasColumnName("IconCSS");
            entity.Property(e => e.MucDoHot).HasDefaultValue(0);
            entity.Property(e => e.TenNganhNghe).HasMaxLength(255);
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NguoiDun__3214EC276E927710");

            entity.ToTable("NguoiDung");

            entity.HasIndex(e => e.Email, "IX_NguoiDung_Email");

            entity.HasIndex(e => e.Email, "UQ__NguoiDun__A9D105349F208FAE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AnhDaiDien).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Ho).HasMaxLength(50);
            entity.Property(e => e.LaAdmin).HasDefaultValue(false);
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Ten).HasMaxLength(50);
        });

        modelBuilder.Entity<ThiTruongViecLam>(entity =>
        {
            entity.HasKey(e => e.NganhNgheId).HasName("PK__ThiTruon__55A1EA8B870D9D7C");

            entity.ToTable("ThiTruongViecLam");

            entity.Property(e => e.NganhNgheId)
                .ValueGeneratedNever()
                .HasColumnName("NganhNgheID");
            entity.Property(e => e.CanhTranh).HasMaxLength(20);
            entity.Property(e => e.NhuCauTuyenDung).HasMaxLength(20);
            entity.Property(e => e.XuHuong).HasMaxLength(100);

            entity.HasOne(d => d.NganhNghe).WithOne(p => p.ThiTruongViecLam)
                .HasForeignKey<ThiTruongViecLam>(d => d.NganhNgheId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ThiTruongViecLam_NganhNghe");
        });

        modelBuilder.Entity<ThongKeTruyCap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ThongKeT__3214EC2738F9F8CB");

            entity.ToTable("ThongKeTruyCap");

            entity.HasIndex(e => e.Ngay, "IX_ThongKeTruyCap_Ngay");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SoLuongBaiTest).HasDefaultValue(0);
            entity.Property(e => e.SoLuongNguoiDungMoi).HasDefaultValue(0);
            entity.Property(e => e.SoLuongTruyCap).HasDefaultValue(0);
        });

        modelBuilder.Entity<YeuCauHoTro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__YeuCauHo__3214EC27C8D38EB1");

            entity.ToTable("YeuCauHoTro");

            entity.HasIndex(e => e.TrangThai, "IX_YeuCauHoTro_TrangThai");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.NgayGui)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");
            entity.Property(e => e.TieuDe).HasMaxLength(255);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .HasDefaultValue("Chua x? lý");
        });

        modelBuilder.Entity<YeuCauKyNang>(entity =>
        {
            entity.HasKey(e => new { e.NganhNgheId, e.KyNangId }).HasName("PK__YeuCauKy__69291A9887C5C8E4");

            entity.ToTable("YeuCauKyNang");

            entity.Property(e => e.NganhNgheId).HasColumnName("NganhNgheID");
            entity.Property(e => e.KyNangId).HasColumnName("KyNangID");

            entity.HasOne(d => d.KyNang).WithMany(p => p.YeuCauKyNangs)
                .HasForeignKey(d => d.KyNangId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_YeuCauKyNang_KyNang");

            entity.HasOne(d => d.NganhNghe).WithMany(p => p.YeuCauKyNangs)
                .HasForeignKey(d => d.NganhNgheId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_YeuCauKyNang_NganhNghe");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

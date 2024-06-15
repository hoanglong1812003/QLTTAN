using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DoAnCoSo.Data;

public partial class QuanLyTrungTamAnhNguContext : DbContext
{
    public QuanLyTrungTamAnhNguContext()
    {
    }

    public QuanLyTrungTamAnhNguContext(DbContextOptions<QuanLyTrungTamAnhNguContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Baikiemtra> Baikiemtras { get; set; }

    public virtual DbSet<Baitap> Baitaps { get; set; }

    public virtual DbSet<Bangluong> Bangluongs { get; set; }

    public virtual DbSet<CtHocphi> CtHocphis { get; set; }

    public virtual DbSet<CtKhuyenmai> CtKhuyenmais { get; set; }

    public virtual DbSet<CtLapdat> CtLapdats { get; set; }

    public virtual DbSet<Giangvien> Giangviens { get; set; }

    public virtual DbSet<Hoadonchitieu> Hoadonchitieus { get; set; }

    public virtual DbSet<Hocvien> Hocviens { get; set; }

    public virtual DbSet<Khoahoc> Khoahocs { get; set; }

    public virtual DbSet<Khuyenmai> Khuyenmais { get; set; }

    public virtual DbSet<Lichday> Lichdays { get; set; }

    public virtual DbSet<Lichhoc> Lichhocs { get; set; }

    public virtual DbSet<Lophoc> Lophocs { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Phieudangkikhoahoc> Phieudangkikhoahocs { get; set; }

    public virtual DbSet<Phieudiemdanh> Phieudiemdanhs { get; set; }

    public virtual DbSet<Phieulatdat> Phieulatdats { get; set; }

    public virtual DbSet<Phieuthuhocphi> Phieuthuhocphis { get; set; }

    public virtual DbSet<Taikhoan> Taikhoans { get; set; }

    public virtual DbSet<Thietbi> Thietbis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-HF8HOQU\\SQLEXPRESS;Initial Catalog=QuanLyTrungTamAnhNgu;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Baikiemtra>(entity =>
        {
            entity.HasKey(e => e.Mabaikt).IsClustered(false);

            entity.ToTable("BAIKIEMTRA");

            entity.HasIndex(e => e.Magv, "CHAMBAIKIEMTRA_FK");

            entity.HasIndex(e => e.Mahv, "LAMBAIKIEMTRA_FK");

            entity.Property(e => e.Mabaikt)
                .ValueGeneratedNever()
                .HasColumnName("MABAIKT");
            entity.Property(e => e.Danhgia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DANHGIA");
            entity.Property(e => e.Ketqua).HasColumnName("KETQUA");
            entity.Property(e => e.Magv).HasColumnName("MAGV");
            entity.Property(e => e.Mahv).HasColumnName("MAHV");
            entity.Property(e => e.Ngaykt)
                .HasColumnType("datetime")
                .HasColumnName("NGAYKT");
            entity.Property(e => e.Tenbaikt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TENBAIKT");
            entity.Property(e => e.Tgbatdau)
                .HasColumnType("datetime")
                .HasColumnName("TGBATDAU");
            entity.Property(e => e.Tgketthuc)
                .HasColumnType("datetime")
                .HasColumnName("TGKETTHUC");

            entity.HasOne(d => d.MagvNavigation).WithMany(p => p.Baikiemtras)
                .HasForeignKey(d => d.Magv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BAIKIEMT_CHAMBAIKI_GIANGVIE");

            entity.HasOne(d => d.MahvNavigation).WithMany(p => p.Baikiemtras)
                .HasForeignKey(d => d.Mahv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BAIKIEMT_LAMBAIKIE_HOCVIEN");
        });

        modelBuilder.Entity<Baitap>(entity =>
        {
            entity.HasKey(e => e.Mabt).IsClustered(false);

            entity.ToTable("BAITAP");

            entity.HasIndex(e => e.Magv, "CHAMBAITAP_FK");

            entity.HasIndex(e => e.Mahv, "LAMBAITAP_FK");

            entity.Property(e => e.Mabt)
                .ValueGeneratedNever()
                .HasColumnName("MABT");
            entity.Property(e => e.Danhgia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DANHGIA");
            entity.Property(e => e.Ketqua).HasColumnName("KETQUA");
            entity.Property(e => e.Magv).HasColumnName("MAGV");
            entity.Property(e => e.Mahv).HasColumnName("MAHV");
            entity.Property(e => e.Tenbt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TENBT");
            entity.Property(e => e.Tgbatdau)
                .HasColumnType("datetime")
                .HasColumnName("TGBATDAU");
            entity.Property(e => e.Tgketthuc)
                .HasColumnType("datetime")
                .HasColumnName("TGKETTHUC");

            entity.HasOne(d => d.MagvNavigation).WithMany(p => p.Baitaps)
                .HasForeignKey(d => d.Magv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BAITAP_CHAMBAITA_GIANGVIE");

            entity.HasOne(d => d.MahvNavigation).WithMany(p => p.Baitaps)
                .HasForeignKey(d => d.Mahv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BAITAP_LAMBAITAP_HOCVIEN");
        });

        modelBuilder.Entity<Bangluong>(entity =>
        {
            entity.HasKey(e => e.Mabang).IsClustered(false);

            entity.ToTable("BANGLUONG");

            entity.HasIndex(e => e.Magv, "CHAMCONG_FK");

            entity.HasIndex(e => e.Manv, "TINHLUONG_FK");

            entity.Property(e => e.Mabang)
                .ValueGeneratedNever()
                .HasColumnName("MABANG");
            entity.Property(e => e.Ghichu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GHICHU");
            entity.Property(e => e.Hesoluong)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HESOLUONG");
            entity.Property(e => e.Magv).HasColumnName("MAGV");
            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Tongsotiet)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TONGSOTIET");
            entity.Property(e => e.Tongtienluong)
                .HasColumnType("money")
                .HasColumnName("TONGTIENLUONG");

            entity.HasOne(d => d.MagvNavigation).WithMany(p => p.Bangluongs)
                .HasForeignKey(d => d.Magv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BANGLUON_CHAMCONG_GIANGVIE");

            entity.HasOne(d => d.ManvNavigation).WithMany(p => p.Bangluongs)
                .HasForeignKey(d => d.Manv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BANGLUON_TINHLUONG_NHANVIEN");
        });

        modelBuilder.Entity<CtHocphi>(entity =>
        {
            entity.HasKey(e => new { e.Makh, e.Maphieu });

            entity.ToTable("CT_HOCPHI");

            entity.HasIndex(e => e.Maphieu, "CT_HOCPHI2_FK");

            entity.HasIndex(e => e.Makh, "CT_HOCPHI_FK");

            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Maphieu).HasColumnName("MAPHIEU");
            entity.Property(e => e.Mota)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MOTA");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.CtHocphis)
                .HasForeignKey(d => d.Makh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CT_HOCPH_CT_HOCPHI_KHOAHOC");

            entity.HasOne(d => d.MaphieuNavigation).WithMany(p => p.CtHocphis)
                .HasForeignKey(d => d.Maphieu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CT_HOCPH_CT_HOCPHI_PHIEUTHU");
        });

        modelBuilder.Entity<CtKhuyenmai>(entity =>
        {
            entity.HasKey(e => new { e.Makm, e.Makh });

            entity.ToTable("CT_KHUYENMAI");

            entity.HasIndex(e => e.Makh, "CT_KHUYENMAI2_FK");

            entity.HasIndex(e => e.Makm, "CT_KHUYENMAI_FK");

            entity.Property(e => e.Makm).HasColumnName("MAKM");
            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Ngaybatdau)
                .HasColumnType("datetime")
                .HasColumnName("NGAYBATDAU");
            entity.Property(e => e.Ngayketthuc)
                .HasColumnType("datetime")
                .HasColumnName("NGAYKETTHUC");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.CtKhuyenmais)
                .HasForeignKey(d => d.Makh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CT_KHUYE_CT_KHUYEN_KHOAHOC");

            entity.HasOne(d => d.MakmNavigation).WithMany(p => p.CtKhuyenmais)
                .HasForeignKey(d => d.Makm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CT_KHUYE_CT_KHUYEN_KHUYENMA");
        });

        modelBuilder.Entity<CtLapdat>(entity =>
        {
            entity.HasKey(e => new { e.Matb, e.Sophieu });

            entity.ToTable("CT_LAPDAT");

            entity.HasIndex(e => e.Sophieu, "CT_LAPDAT2_FK");

            entity.HasIndex(e => e.Matb, "CT_LAPDAT_FK");

            entity.Property(e => e.Matb).HasColumnName("MATB");
            entity.Property(e => e.Sophieu).HasColumnName("SOPHIEU");
            entity.Property(e => e.Ghichu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GHICHU");
            entity.Property(e => e.Ngaylapdat)
                .HasColumnType("datetime")
                .HasColumnName("NGAYLAPDAT");

            entity.HasOne(d => d.MatbNavigation).WithMany(p => p.CtLapdats)
                .HasForeignKey(d => d.Matb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CT_LAPDA_CT_LAPDAT_THIETBI");

            entity.HasOne(d => d.SophieuNavigation).WithMany(p => p.CtLapdats)
                .HasForeignKey(d => d.Sophieu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CT_LAPDA_CT_LAPDAT_PHIEULAT");
        });

        modelBuilder.Entity<Giangvien>(entity =>
        {
            entity.HasKey(e => e.Magv).IsClustered(false);

            entity.ToTable("GIANGVIEN");

            entity.HasIndex(e => e.Username, "DANGNHAPGV2_FK");

            entity.Property(e => e.Magv)
                .ValueGeneratedNever()
                .HasColumnName("MAGV");
            entity.Property(e => e.Diachi)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Gioitinh)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("GIOITINH");
            entity.Property(e => e.Ngaysinh)
                .HasColumnType("datetime")
                .HasColumnName("NGAYSINH");
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.Socccd)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("SOCCCD");
            entity.Property(e => e.Tengv)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TENGV");
            entity.Property(e => e.Trinhdo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TRINHDO");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Giangviens)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK_GIANGVIE_DANGNHAPG_TAIKHOAN");
        });

        modelBuilder.Entity<Hoadonchitieu>(entity =>
        {
            entity.HasKey(e => e.Mahd).IsClustered(false);

            entity.ToTable("HOADONCHITIEU");

            entity.HasIndex(e => e.Manv, "LAPHOADON_FK");

            entity.Property(e => e.Mahd)
                .ValueGeneratedNever()
                .HasColumnName("MAHD");
            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Noidung)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOIDUNG");
            entity.Property(e => e.Sotien)
                .HasColumnType("money")
                .HasColumnName("SOTIEN");

            entity.HasOne(d => d.ManvNavigation).WithMany(p => p.Hoadonchitieus)
                .HasForeignKey(d => d.Manv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HOADONCH_LAPHOADON_NHANVIEN");
        });

        modelBuilder.Entity<Hocvien>(entity =>
        {
            entity.HasKey(e => e.Mahv).IsClustered(false);

            entity.ToTable("HOCVIEN");

            entity.HasIndex(e => e.Username, "DANGNHAPHV2_FK");

            entity.Property(e => e.Mahv)
                .ValueGeneratedNever()
                .HasColumnName("MAHV");
            entity.Property(e => e.Diachi)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Gioitinh)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("GIOITINH");
            entity.Property(e => e.Ngaysinh)
                .HasColumnType("datetime")
                .HasColumnName("NGAYSINH");
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.Socccd)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("SOCCCD");
            entity.Property(e => e.Tenhv).HasColumnName("TENHV");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Hocviens)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK_HOCVIEN_DANGNHAPH_TAIKHOAN");
        });

        modelBuilder.Entity<Khoahoc>(entity =>
        {
            entity.HasKey(e => e.Makh).IsClustered(false);

            entity.ToTable("KHOAHOC");

            entity.Property(e => e.Makh)
                .ValueGeneratedNever()
                .HasColumnName("MAKH");
            entity.Property(e => e.Dongia)
                .HasColumnType("money")
                .HasColumnName("DONGIA");
            entity.Property(e => e.Noidung)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOIDUNG");
            entity.Property(e => e.Tenkh)
                .HasMaxLength(59)
                .IsUnicode(false)
                .HasColumnName("TENKH");
        });

        modelBuilder.Entity<Khuyenmai>(entity =>
        {
            entity.HasKey(e => e.Makm).IsClustered(false);

            entity.ToTable("KHUYENMAI");

            entity.Property(e => e.Makm)
                .ValueGeneratedNever()
                .HasColumnName("MAKM");
            entity.Property(e => e.Noidung)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOIDUNG");
        });

        modelBuilder.Entity<Lichday>(entity =>
        {
            entity.HasKey(e => e.Mald).IsClustered(false);

            entity.ToTable("LICHDAY");

            entity.HasIndex(e => e.Malop, "DAYLOP_FK");

            entity.HasIndex(e => e.Magv, "DAY_FK");

            entity.Property(e => e.Mald)
                .ValueGeneratedNever()
                .HasColumnName("MALD");
            entity.Property(e => e.Caday)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CADAY");
            entity.Property(e => e.Ghichu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GHICHU");
            entity.Property(e => e.Magv).HasColumnName("MAGV");
            entity.Property(e => e.Malop).HasColumnName("MALOP");
            entity.Property(e => e.Ngayday)
                .HasColumnType("datetime")
                .HasColumnName("NGAYDAY");
            entity.Property(e => e.Tuan)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TUAN");

            entity.HasOne(d => d.MagvNavigation).WithMany(p => p.Lichdays)
                .HasForeignKey(d => d.Magv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LICHDAY_DAY_GIANGVIE");

            entity.HasOne(d => d.MalopNavigation).WithMany(p => p.Lichdays)
                .HasForeignKey(d => d.Malop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LICHDAY_DAYLOP_LOPHOC");
        });

        modelBuilder.Entity<Lichhoc>(entity =>
        {
            entity.HasKey(e => e.Malh).IsClustered(false);

            entity.ToTable("LICHHOC");

            entity.HasIndex(e => e.Malop, "HOCLOP_FK");

            entity.HasIndex(e => e.Mahv, "HOC_FK");

            entity.Property(e => e.Malh)
                .ValueGeneratedNever()
                .HasColumnName("MALH");
            entity.Property(e => e.Cahoc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CAHOC");
            entity.Property(e => e.Ghichu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GHICHU");
            entity.Property(e => e.Mahv).HasColumnName("MAHV");
            entity.Property(e => e.Malop).HasColumnName("MALOP");
            entity.Property(e => e.Ngayhoc)
                .HasColumnType("datetime")
                .HasColumnName("NGAYHOC");
            entity.Property(e => e.Tuan)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("TUAN");

            entity.HasOne(d => d.MahvNavigation).WithMany(p => p.Lichhocs)
                .HasForeignKey(d => d.Mahv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LICHHOC_HOC_HOCVIEN");

            entity.HasOne(d => d.MalopNavigation).WithMany(p => p.Lichhocs)
                .HasForeignKey(d => d.Malop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LICHHOC_HOCLOP_LOPHOC");
        });

        modelBuilder.Entity<Lophoc>(entity =>
        {
            entity.HasKey(e => e.Malop).IsClustered(false);

            entity.ToTable("LOPHOC");

            entity.HasIndex(e => e.Makh, "THUOC_FK");

            entity.Property(e => e.Malop)
                .ValueGeneratedNever()
                .HasColumnName("MALOP");
            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Ngayketthuc)
                .HasColumnType("datetime")
                .HasColumnName("NGAYKETTHUC");
            entity.Property(e => e.Ngaynhaphoc)
                .HasColumnType("datetime")
                .HasColumnName("NGAYNHAPHOC");
            entity.Property(e => e.Tenlop)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TENLOP");
            entity.Property(e => e.Tgtao)
                .HasColumnType("datetime")
                .HasColumnName("TGTAO");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Lophocs)
                .HasForeignKey(d => d.Makh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOPHOC_THUOC_KHOAHOC");
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.Manv).IsClustered(false);

            entity.ToTable("NHANVIEN");

            entity.HasIndex(e => e.Username, "DANGNHAPNV2_FK");

            entity.Property(e => e.Manv)
                .ValueGeneratedNever()
                .HasColumnName("MANV");
            entity.Property(e => e.Chucvu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CHUCVU");
            entity.Property(e => e.Diachi)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Gioitinh)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("GIOITINH");
            entity.Property(e => e.Ngaysinh)
                .HasColumnType("datetime")
                .HasColumnName("NGAYSINH");
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.Socccd)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("SOCCCD");
            entity.Property(e => e.Tennv)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TENNV");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Nhanviens)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK_NHANVIEN_DANGNHAPN_TAIKHOAN");
        });

        modelBuilder.Entity<Phieudangkikhoahoc>(entity =>
        {
            entity.HasKey(e => e.Madki).IsClustered(false);

            entity.ToTable("PHIEUDANGKIKHOAHOC");

            entity.HasIndex(e => e.Mahv, "DANGKI_FK");

            entity.HasIndex(e => e.Makh, "KHOAHOC_FK");

            entity.Property(e => e.Madki)
                .ValueGeneratedNever()
                .HasColumnName("MADKI");
            entity.Property(e => e.Mahv).HasColumnName("MAHV");
            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Ngaydki)
                .HasColumnType("datetime")
                .HasColumnName("NGAYDKI");

            entity.HasOne(d => d.MahvNavigation).WithMany(p => p.Phieudangkikhoahocs)
                .HasForeignKey(d => d.Mahv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PHIEUDAN_DANGKI_HOCVIEN");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Phieudangkikhoahocs)
                .HasForeignKey(d => d.Makh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PHIEUDAN_KHOAHOC_KHOAHOC");
        });

        modelBuilder.Entity<Phieudiemdanh>(entity =>
        {
            entity.HasKey(e => e.Madd).IsClustered(false);

            entity.ToTable("PHIEUDIEMDANH");

            entity.HasIndex(e => e.Malh, "DIEMDANHHOCVIEN_FK");

            entity.HasIndex(e => e.Magv, "KIEMTRA_FK");

            entity.Property(e => e.Madd)
                .ValueGeneratedNever()
                .HasColumnName("MADD");
            entity.Property(e => e.Ghichu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GHICHU");
            entity.Property(e => e.Magv).HasColumnName("MAGV");
            entity.Property(e => e.Malh).HasColumnName("MALH");
            entity.Property(e => e.Trangthai)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("TRANGTHAI");

            entity.HasOne(d => d.MagvNavigation).WithMany(p => p.Phieudiemdanhs)
                .HasForeignKey(d => d.Magv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PHIEUDIE_KIEMTRA_GIANGVIE");

            entity.HasOne(d => d.MalhNavigation).WithMany(p => p.Phieudiemdanhs)
                .HasForeignKey(d => d.Malh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PHIEUDIE_DIEMDANHH_LICHHOC");
        });

        modelBuilder.Entity<Phieulatdat>(entity =>
        {
            entity.HasKey(e => e.Sophieu).IsClustered(false);

            entity.ToTable("PHIEULATDAT");

            entity.HasIndex(e => e.Mahd, "GOM_FK");

            entity.HasIndex(e => e.Manv, "LAP_FK");

            entity.Property(e => e.Sophieu)
                .ValueGeneratedNever()
                .HasColumnName("SOPHIEU");
            entity.Property(e => e.Mahd).HasColumnName("MAHD");
            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Noidung)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOIDUNG");
            entity.Property(e => e.Soluong).HasColumnName("SOLUONG");
            entity.Property(e => e.Thanhtien)
                .HasColumnType("money")
                .HasColumnName("THANHTIEN");

            entity.HasOne(d => d.MahdNavigation).WithMany(p => p.Phieulatdats)
                .HasForeignKey(d => d.Mahd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PHIEULAT_GOM_HOADONCH");

            entity.HasOne(d => d.ManvNavigation).WithMany(p => p.Phieulatdats)
                .HasForeignKey(d => d.Manv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PHIEULAT_LAP_NHANVIEN");
        });

        modelBuilder.Entity<Phieuthuhocphi>(entity =>
        {
            entity.HasKey(e => e.Maphieu).IsClustered(false);

            entity.ToTable("PHIEUTHUHOCPHI");

            entity.HasIndex(e => e.Mahv, "DONG_FK");

            entity.HasIndex(e => e.Manv, "THU_FK");

            entity.Property(e => e.Maphieu)
                .ValueGeneratedNever()
                .HasColumnName("MAPHIEU");
            entity.Property(e => e.Mahv).HasColumnName("MAHV");
            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Ngaythu)
                .HasColumnType("datetime")
                .HasColumnName("NGAYTHU");
            entity.Property(e => e.Sotien)
                .HasColumnType("money")
                .HasColumnName("SOTIEN");

            entity.HasOne(d => d.MahvNavigation).WithMany(p => p.Phieuthuhocphis)
                .HasForeignKey(d => d.Mahv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PHIEUTHU_DONG_HOCVIEN");

            entity.HasOne(d => d.ManvNavigation).WithMany(p => p.Phieuthuhocphis)
                .HasForeignKey(d => d.Manv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PHIEUTHU_THU_NHANVIEN");
        });

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.HasKey(e => e.Username).IsClustered(false);

            entity.ToTable("TAIKHOAN");

            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USERNAME");
            entity.Property(e => e.Loaiuser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LOAIUSER");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
        });

        modelBuilder.Entity<Thietbi>(entity =>
        {
            entity.HasKey(e => e.Matb).IsClustered(false);

            entity.ToTable("THIETBI");

            entity.Property(e => e.Matb)
                .ValueGeneratedNever()
                .HasColumnName("MATB");
            entity.Property(e => e.Dvt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DVT");
            entity.Property(e => e.Giatien)
                .HasColumnType("money")
                .HasColumnName("GIATIEN");
            entity.Property(e => e.Tentb)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TENTB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

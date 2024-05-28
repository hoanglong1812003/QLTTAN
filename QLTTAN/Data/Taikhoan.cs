using System;
using System.Collections.Generic;

namespace QLTTAN.Data;

public partial class Taikhoan
{
    public string Username { get; set; } = null!;

    public int Mahv { get; set; }

    public int Manv { get; set; }

    public int Magv { get; set; }

    public string? Password { get; set; }

    public string? Loaiuser { get; set; }

    public virtual ICollection<Giangvien> Giangviens { get; set; } = new List<Giangvien>();

    public virtual ICollection<Hocvien> Hocviens { get; set; } = new List<Hocvien>();

    public virtual Giangvien MagvNavigation { get; set; } = null!;

    public virtual Hocvien MahvNavigation { get; set; } = null!;

    public virtual Nhanvien ManvNavigation { get; set; } = null!;

    public virtual ICollection<Nhanvien> Nhanviens { get; set; } = new List<Nhanvien>();
}

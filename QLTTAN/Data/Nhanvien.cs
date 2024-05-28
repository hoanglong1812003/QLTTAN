using System;
using System.Collections.Generic;

namespace QLTTAN.Data;

public partial class Nhanvien
{
    public int Manv { get; set; }

    public string? Username { get; set; }

    public string? Tennv { get; set; }

    public DateTime? Ngaysinh { get; set; }

    public string? Gioitinh { get; set; }

    public string? Diachi { get; set; }

    public string? Socccd { get; set; }

    public string? Sdt { get; set; }

    public string? Email { get; set; }

    public string? Chucvu { get; set; }

    public virtual ICollection<Bangluong> Bangluongs { get; set; } = new List<Bangluong>();

    public virtual ICollection<Hoadonchitieu> Hoadonchitieus { get; set; } = new List<Hoadonchitieu>();

    public virtual ICollection<Phieulatdat> Phieulatdats { get; set; } = new List<Phieulatdat>();

    public virtual ICollection<Phieuthuhocphi> Phieuthuhocphis { get; set; } = new List<Phieuthuhocphi>();

    public virtual ICollection<Taikhoan> Taikhoans { get; set; } = new List<Taikhoan>();

    public virtual Taikhoan? UsernameNavigation { get; set; }
}

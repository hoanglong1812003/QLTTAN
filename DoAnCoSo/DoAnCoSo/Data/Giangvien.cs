using System;
using System.Collections.Generic;

namespace DoAnCoSo.Data;

public partial class Giangvien
{
    public int Magv { get; set; }

    public string? Username { get; set; }

    public string? Tengv { get; set; }

    public DateTime? Ngaysinh { get; set; }

    public string? Diachi { get; set; }

    public string? Gioitinh { get; set; }

    public string? Sdt { get; set; }

    public string? Socccd { get; set; }

    public string? Email { get; set; }

    public string? Trinhdo { get; set; }

    public virtual ICollection<Baikiemtra> Baikiemtras { get; set; } = new List<Baikiemtra>();

    public virtual ICollection<Baitap> Baitaps { get; set; } = new List<Baitap>();

    public virtual ICollection<Bangluong> Bangluongs { get; set; } = new List<Bangluong>();

    public virtual ICollection<Lichday> Lichdays { get; set; } = new List<Lichday>();

    public virtual ICollection<Phieudiemdanh> Phieudiemdanhs { get; set; } = new List<Phieudiemdanh>();

    public virtual Taikhoan? UsernameNavigation { get; set; }
}

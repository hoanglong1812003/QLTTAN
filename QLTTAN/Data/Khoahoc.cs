using System;
using System.Collections.Generic;

namespace QLTTAN.Data;

public partial class Khoahoc
{
    public int Makh { get; set; }

    public string? Tenkh { get; set; }

    public string? Noidung { get; set; }

    public decimal? Dongia { get; set; }

    public virtual ICollection<CtHocphi> CtHocphis { get; set; } = new List<CtHocphi>();

    public virtual ICollection<CtKhuyenmai> CtKhuyenmais { get; set; } = new List<CtKhuyenmai>();

    public virtual ICollection<Lophoc> Lophocs { get; set; } = new List<Lophoc>();

    public virtual ICollection<Phieudangkikhoahoc> Phieudangkikhoahocs { get; set; } = new List<Phieudangkikhoahoc>();
}

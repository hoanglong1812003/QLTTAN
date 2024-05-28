using System;
using System.Collections.Generic;

namespace QLTTAN.Data;

public partial class Hoadonchitieu
{
    public int Mahd { get; set; }

    public int Manv { get; set; }

    public string? Noidung { get; set; }

    public decimal? Sotien { get; set; }

    public virtual Nhanvien ManvNavigation { get; set; } = null!;

    public virtual ICollection<Phieulatdat> Phieulatdats { get; set; } = new List<Phieulatdat>();
}

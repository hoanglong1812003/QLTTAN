using System;
using System.Collections.Generic;

namespace DoAnCoSo.Data;

public partial class Lichday
{
    public int Mald { get; set; }

    public int Malop { get; set; }

    public int Magv { get; set; }

    public string? Tuan { get; set; }

    public string? Caday { get; set; }

    public DateTime? Ngayday { get; set; }

    public string? Ghichu { get; set; }

    public virtual Giangvien MagvNavigation { get; set; } = null!;

    public virtual Lophoc MalopNavigation { get; set; } = null!;
}

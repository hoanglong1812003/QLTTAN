using System;
using System.Collections.Generic;

namespace QLTTAN.Data;

public partial class Phieudiemdanh
{
    public int Madd { get; set; }

    public int Magv { get; set; }

    public int Malh { get; set; }

    public string? Trangthai { get; set; }

    public string? Ghichu { get; set; }

    public virtual Giangvien MagvNavigation { get; set; } = null!;

    public virtual Lichhoc MalhNavigation { get; set; } = null!;
}

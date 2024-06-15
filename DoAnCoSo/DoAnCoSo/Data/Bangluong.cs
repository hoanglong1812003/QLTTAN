using System;
using System.Collections.Generic;

namespace DoAnCoSo.Data;

public partial class Bangluong
{
    public int Mabang { get; set; }

    public int Manv { get; set; }

    public int Magv { get; set; }

    public string? Tongsotiet { get; set; }

    public string? Hesoluong { get; set; }

    public decimal? Tongtienluong { get; set; }

    public string? Ghichu { get; set; }

    public virtual Giangvien MagvNavigation { get; set; } = null!;

    public virtual Nhanvien ManvNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace DoAnCoSo.Data;

public partial class Khuyenmai
{
    public int Makm { get; set; }

    public string? Noidung { get; set; }

    public virtual ICollection<CtKhuyenmai> CtKhuyenmais { get; set; } = new List<CtKhuyenmai>();
}

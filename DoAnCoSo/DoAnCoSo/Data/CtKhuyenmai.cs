using System;
using System.Collections.Generic;

namespace DoAnCoSo.Data;

public partial class CtKhuyenmai
{
    public int Makm { get; set; }

    public int Makh { get; set; }

    public DateTime? Ngaybatdau { get; set; }

    public DateTime? Ngayketthuc { get; set; }

    public virtual Khoahoc? MakhNavigation { get; set; } = null!;

    public virtual Khuyenmai? MakmNavigation { get; set; } = null!;
}

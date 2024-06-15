using System;
using System.Collections.Generic;

namespace DoAnCoSo.Data;

public partial class CtLapdat
{
    public int Matb { get; set; }

    public int Sophieu { get; set; }

    public DateTime? Ngaylapdat { get; set; }

    public string? Ghichu { get; set; }

    public virtual Thietbi MatbNavigation { get; set; } = null!;

    public virtual Phieulatdat SophieuNavigation { get; set; } = null!;
}

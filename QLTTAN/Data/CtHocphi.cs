using System;
using System.Collections.Generic;

namespace QLTTAN.Data;

public partial class CtHocphi
{
    public int Makh { get; set; }

    public int Maphieu { get; set; }

    public string? Mota { get; set; }

    public virtual Khoahoc MakhNavigation { get; set; } = null!;

    public virtual Phieuthuhocphi MaphieuNavigation { get; set; } = null!;
}

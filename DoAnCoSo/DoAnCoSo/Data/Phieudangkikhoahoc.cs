using System;
using System.Collections.Generic;

namespace DoAnCoSo.Data;

public partial class Phieudangkikhoahoc
{
    public int Madki { get; set; }

    public int Mahv { get; set; }

    public int Makh { get; set; }

    public DateTime? Ngaydki { get; set; }

    public virtual Hocvien MahvNavigation { get; set; } = null!;

    public virtual Khoahoc MakhNavigation { get; set; } = null!;
}

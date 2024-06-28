using System;
using System.Collections.Generic;

namespace DoAnCoSo.Data;

public partial class Phieuthuhocphi
{
    public int Maphieu { get; set; }

    public int Mahv { get; set; }

    public int Manv { get; set; }

    public DateTime? Ngaythu { get; set; }

    public decimal? Sotien { get; set; }

    public virtual ICollection<CtHocphi> CtHocphis { get; set; } = new List<CtHocphi>();

    public virtual Hocvien? MahvNavigation { get; set; } = null!;

    public virtual Nhanvien? ManvNavigation { get; set; } = null!;
}

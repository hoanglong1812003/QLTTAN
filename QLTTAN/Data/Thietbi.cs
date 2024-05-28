using System;
using System.Collections.Generic;

namespace QLTTAN.Data;

public partial class Thietbi
{
    public int Matb { get; set; }

    public string? Tentb { get; set; }

    public decimal? Giatien { get; set; }

    public string? Dvt { get; set; }

    public virtual ICollection<CtLapdat> CtLapdats { get; set; } = new List<CtLapdat>();
}

using System;
using System.Collections.Generic;

namespace DoAnCoSo.Data;

public partial class Baikiemtra
{
    public int Mabaikt { get; set; }

    public int Magv { get; set; }

    public int Mahv { get; set; }

    public string? Tenbaikt { get; set; }

    public DateTime? Ngaykt { get; set; }

    public DateTime? Tgbatdau { get; set; }

    public DateTime? Tgketthuc { get; set; }

    public float? Ketqua { get; set; }

    public string? Danhgia { get; set; }

    public string? FilePath { get; set; }

    public virtual Giangvien MagvNavigation { get; set; } = null!;

    public virtual Hocvien MahvNavigation { get; set; } = null!;
}

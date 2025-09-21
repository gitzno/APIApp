using System;
using System.Collections.Generic;

namespace APIApp.Models;

public partial class ThiTruongViecLam
{
    public int NganhNgheId { get; set; }

    public string NhuCauTuyenDung { get; set; } = null!;

    public string CanhTranh { get; set; } = null!;

    public string? XuHuong { get; set; }

    public virtual NganhNghe NganhNghe { get; set; } = null!;
}

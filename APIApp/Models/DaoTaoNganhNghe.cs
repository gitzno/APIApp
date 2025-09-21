using System;
using System.Collections.Generic;

namespace APIApp.Models;

public partial class DaoTaoNganhNghe
{
    public int NganhNgheId { get; set; }

    public string? ThoiGianHoc { get; set; }

    public string? ChiPhi { get; set; }

    public bool? HocOnline { get; set; }

    public virtual NganhNghe NganhNghe { get; set; } = null!;
}

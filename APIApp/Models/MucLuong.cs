using System;
using System.Collections.Generic;

namespace APIApp.Models;

public partial class MucLuong
{
    public int Id { get; set; }

    public int NganhNgheId { get; set; }

    public string KinhNghiem { get; set; } = null!;

    public decimal MucLuongMin { get; set; }

    public decimal MucLuongMax { get; set; }

    public string? DonViTienTe { get; set; }

    public string? MoTa { get; set; }

    public virtual NganhNghe NganhNghe { get; set; } = null!;
}

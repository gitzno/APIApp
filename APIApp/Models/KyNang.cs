using System;
using System.Collections.Generic;

namespace APIApp.Models;

public partial class KyNang
{
    public int Id { get; set; }

    public string TenKyNang { get; set; } = null!;

    public string? MoTa { get; set; }

    public virtual ICollection<KyNangCaNhan> KyNangCaNhans { get; set; } = new List<KyNangCaNhan>();

    public virtual ICollection<YeuCauKyNang> YeuCauKyNangs { get; set; } = new List<YeuCauKyNang>();
}

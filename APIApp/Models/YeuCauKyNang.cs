using System;
using System.Collections.Generic;

namespace APIApp.Models;

public partial class YeuCauKyNang
{
    public int NganhNgheId { get; set; }

    public int KyNangId { get; set; }

    public int MucDoQuanTrong { get; set; }

    public virtual KyNang KyNang { get; set; } = null!;

    public virtual NganhNghe NganhNghe { get; set; } = null!;
}

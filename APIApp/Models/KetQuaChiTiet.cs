using System;
using System.Collections.Generic;

namespace APIApp.Models;

public partial class KetQuaChiTiet
{
    public int Id { get; set; }

    public int LichSuTestId { get; set; }

    public int NganhNgheId { get; set; }

    public decimal PhanTramPhuHop { get; set; }

    public virtual LichSuTest LichSuTest { get; set; } = null!;

    public virtual NganhNghe NganhNghe { get; set; } = null!;
}

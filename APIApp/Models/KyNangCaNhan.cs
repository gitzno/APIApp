using System;
using System.Collections.Generic;

namespace APIApp.Models;

public partial class KyNangCaNhan
{
    public int Id { get; set; }

    public int LichSuTestId { get; set; }

    public int KyNangId { get; set; }

    public int DiemSo { get; set; }

    public virtual KyNang KyNang { get; set; } = null!;

    public virtual LichSuTest LichSuTest { get; set; } = null!;
}

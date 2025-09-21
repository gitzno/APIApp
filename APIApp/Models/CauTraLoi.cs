using System;
using System.Collections.Generic;

namespace APIApp.Models;

public partial class CauTraLoi
{
    public int Id { get; set; }

    public int CauHoiId { get; set; }

    public string NoiDung { get; set; } = null!;

    public int DiemSo { get; set; }

    public virtual CauHoi CauHoi { get; set; } = null!;
}

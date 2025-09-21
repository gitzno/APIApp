using System;
using System.Collections.Generic;

namespace APIApp.Models;

public partial class CauHoi
{
    public int Id { get; set; }

    public string NoiDung { get; set; } = null!;

    public string LoaiCauHoi { get; set; } = null!;

    public int ThuTu { get; set; }

    public bool? TrangThai { get; set; }

    public virtual ICollection<CauTraLoi> CauTraLois { get; set; } = new List<CauTraLoi>();
}

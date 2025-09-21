using System;
using System.Collections.Generic;

namespace APIApp.Models;

public partial class LichSuTest
{
    public int Id { get; set; }

    public int NguoiDungId { get; set; }

    public DateTime? NgayLamTest { get; set; }

    public virtual ICollection<KetQuaChiTiet> KetQuaChiTiets { get; set; } = new List<KetQuaChiTiet>();

    public virtual ICollection<KyNangCaNhan> KyNangCaNhans { get; set; } = new List<KyNangCaNhan>();

    public virtual NguoiDung NguoiDung { get; set; } = null!;
}

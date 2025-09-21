using System;
using System.Collections.Generic;

namespace APIApp.Models;

public partial class NganhNghe
{
    public int Id { get; set; }

    public string TenNganhNghe { get; set; } = null!;

    public string? MoTa { get; set; }

    public string? AnhMinhHoa { get; set; }

    public int? MucDoHot { get; set; }

    public string? IconCss { get; set; }

    public virtual DaoTaoNganhNghe? DaoTaoNganhNghe { get; set; }

    public virtual ICollection<KetQuaChiTiet> KetQuaChiTiets { get; set; } = new List<KetQuaChiTiet>();

    public virtual ICollection<MucLuong> MucLuongs { get; set; } = new List<MucLuong>();

    public virtual ThiTruongViecLam? ThiTruongViecLam { get; set; }

    public virtual ICollection<YeuCauKyNang> YeuCauKyNangs { get; set; } = new List<YeuCauKyNang>();
}

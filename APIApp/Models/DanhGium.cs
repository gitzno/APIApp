using System;
using System.Collections.Generic;

namespace APIApp.Models;

public partial class DanhGium
{
    public int Id { get; set; }

    public int NguoiDungId { get; set; }

    public int DiemSo { get; set; }

    public string? NoiDungDanhGia { get; set; }

    public DateTime? NgayDanhGia { get; set; }

    public virtual NguoiDung NguoiDung { get; set; } = null!;
}

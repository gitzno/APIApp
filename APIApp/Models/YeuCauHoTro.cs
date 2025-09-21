using System;
using System.Collections.Generic;

namespace APIApp.Models;

public partial class YeuCauHoTro
{
    public int Id { get; set; }

    public int? NguoiDungId { get; set; }

    public string HoTen { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string TieuDe { get; set; } = null!;

    public string NoiDung { get; set; } = null!;

    public DateTime? NgayGui { get; set; }

    public string? TrangThai { get; set; }
}

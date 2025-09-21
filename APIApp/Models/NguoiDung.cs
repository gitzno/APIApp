using System;
using System.Collections.Generic;

namespace APIApp.Models;

public partial class NguoiDung
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string Ho { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public string? AnhDaiDien { get; set; }

    public DateTime? NgayTao { get; set; }

    public bool? LaAdmin { get; set; }

    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    public virtual ICollection<LichSuTest> LichSuTests { get; set; } = new List<LichSuTest>();
}

using System;
using System.Collections.Generic;

namespace APIApp.Models;

public partial class ThongKeTruyCap
{
    public int Id { get; set; }

    public DateOnly Ngay { get; set; }

    public int? SoLuongTruyCap { get; set; }

    public int? SoLuongNguoiDungMoi { get; set; }

    public int? SoLuongBaiTest { get; set; }
}

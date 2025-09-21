docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrongPassword>" -e "MSSQL_PID=Evaluation" -p 1433:1433 --name sqlpreview --hostname sqlpreview -d mcr.microsoft.com/mssql/server:2025-latest

USE CareerQuizDB;
GO

-- Bảng Người dùng
CREATE TABLE NguoiDung (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    MatKhau NVARCHAR(255) NOT NULL,
    Ho NVARCHAR(50) NOT NULL,
    Ten NVARCHAR(50) NOT NULL,
    AnhDaiDien NVARCHAR(255) NULL,
    NgayTao DATETIME DEFAULT GETDATE(),
    LaAdmin BIT DEFAULT 0
);
GO

-- Bảng Lịch sử làm bài test
CREATE TABLE LichSuTest (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NguoiDungID INT NOT NULL,
    NgayLamTest DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_LichSuTest_NguoiDung FOREIGN KEY (NguoiDungID) REFERENCES NguoiDung(ID)
);
GO

-- Bảng Câu hỏi
CREATE TABLE CauHoi (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NoiDung NVARCHAR(500) NOT NULL,
    LoaiCauHoi NVARCHAR(50) NOT NULL, -- Tính cách, Kỹ năng, Sở thích, v.v.
    ThuTu INT NOT NULL,
    TrangThai BIT DEFAULT 1
);
GO

-- Bảng Câu trả lời
CREATE TABLE CauTraLoi (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    CauHoiID INT NOT NULL,
    NoiDung NVARCHAR(500) NOT NULL,
    DiemSo INT NOT NULL, -- Điểm số cho câu trả lời này
    CONSTRAINT FK_CauTraLoi_CauHoi FOREIGN KEY (CauHoiID) REFERENCES CauHoi(ID)
);
GO

-- Bảng Ngành nghề
CREATE TABLE NganhNghe (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenNganhNghe NVARCHAR(255) NOT NULL,
    MoTa NVARCHAR(MAX) NULL,
    AnhMinhHoa NVARCHAR(255) NULL,
    MucDoHot INT DEFAULT 0, -- Độ hot của ngành nghề (1-10)
    IconCSS NVARCHAR(100) NULL -- Class CSS cho icon (fas fa-laptop-code, etc.)
);
GO

-- Bảng Kỹ năng
CREATE TABLE KyNang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenKyNang NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(500) NULL
);
GO

-- Bảng Yêu cầu kỹ năng cho ngành nghề
CREATE TABLE YeuCauKyNang (
    NganhNgheID INT NOT NULL,
    KyNangID INT NOT NULL,
    MucDoQuanTrong INT NOT NULL, -- Mức độ quan trọng (1-10)
    PRIMARY KEY (NganhNgheID, KyNangID),
    CONSTRAINT FK_YeuCauKyNang_NganhNghe FOREIGN KEY (NganhNgheID) REFERENCES NganhNghe(ID),
    CONSTRAINT FK_YeuCauKyNang_KyNang FOREIGN KEY (KyNangID) REFERENCES KyNang(ID)
);
GO

-- Bảng Mức lương theo ngành nghề
CREATE TABLE MucLuong (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NganhNgheID INT NOT NULL,
    KinhNghiem NVARCHAR(50) NOT NULL, -- Fresher, Junior, Middle, Senior,...
    MucLuongMin DECIMAL(12,2) NOT NULL,
    MucLuongMax DECIMAL(12,2) NOT NULL,
    DonViTienTe NVARCHAR(10) DEFAULT 'VND',
    MoTa NVARCHAR(100) NULL, -- Khởi điểm, Trung bình, Kinh nghiệm
    CONSTRAINT FK_MucLuong_NganhNghe FOREIGN KEY (NganhNgheID) REFERENCES NganhNghe(ID)
);
GO

-- Bảng thông tin thị trường việc làm cho ngành nghề
CREATE TABLE ThiTruongViecLam (
    NganhNgheID INT NOT NULL PRIMARY KEY,
    NhuCauTuyenDung NVARCHAR(20) NOT NULL, -- Cao, Trung bình, Thấp
    CanhTranh NVARCHAR(20) NOT NULL, -- Cao, Trung bình, Thấp
    XuHuong NVARCHAR(100) NULL, -- Ví dụ: Tăng 15%/năm
    CONSTRAINT FK_ThiTruongViecLam_NganhNghe FOREIGN KEY (NganhNgheID) REFERENCES NganhNghe(ID)
);
GO

-- Bảng thông tin đào tạo cho ngành nghề
CREATE TABLE DaoTaoNganhNghe (
    NganhNgheID INT NOT NULL PRIMARY KEY,
    ThoiGianHoc NVARCHAR(100) NULL, -- Ví dụ: 6-12 tháng
    ChiPhi NVARCHAR(100) NULL, -- Ví dụ: 5-15 triệu
    HocOnline BIT DEFAULT 0, -- Có học online được không
    CONSTRAINT FK_DaoTaoNganhNghe_NganhNghe FOREIGN KEY (NganhNgheID) REFERENCES NganhNghe(ID)
);
GO

-- Bảng kết quả chi tiết (lưu % phù hợp với từng ngành nghề)
CREATE TABLE KetQuaChiTiet (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    LichSuTestID INT NOT NULL,
    NganhNgheID INT NOT NULL,
    PhanTramPhuHop DECIMAL(5,2) NOT NULL,
    CONSTRAINT FK_KetQuaChiTiet_LichSuTest FOREIGN KEY (LichSuTestID) REFERENCES LichSuTest(ID),
    CONSTRAINT FK_KetQuaChiTiet_NganhNghe FOREIGN KEY (NganhNgheID) REFERENCES NganhNghe(ID)
);
GO

-- Bảng kết quả kỹ năng cá nhân (cho biểu đồ radar)
CREATE TABLE KyNangCaNhan (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    LichSuTestID INT NOT NULL,
    KyNangID INT NOT NULL,
    DiemSo INT NOT NULL, -- Điểm từ 0-100
    CONSTRAINT FK_KyNangCaNhan_LichSuTest FOREIGN KEY (LichSuTestID) REFERENCES LichSuTest(ID),
    CONSTRAINT FK_KyNangCaNhan_KyNang FOREIGN KEY (KyNangID) REFERENCES KyNang(ID)
);
GO

-- Bảng Yêu cầu hỗ trợ
CREATE TABLE YeuCauHoTro (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NguoiDungID INT NULL, -- Có thể NULL nếu khách vãng lai
    HoTen NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    TieuDe NVARCHAR(255) NOT NULL,
    NoiDung NVARCHAR(MAX) NOT NULL,
    NgayGui DATETIME DEFAULT GETDATE(),
    TrangThai NVARCHAR(20) DEFAULT 'Chưa xử lý' -- Chưa xử lý, Đang xử lý, Đã xử lý
);
GO

-- Bảng Đánh giá từ người dùng
CREATE TABLE DanhGia (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NguoiDungID INT NOT NULL,
    DiemSo INT NOT NULL, -- Điểm đánh giá từ 1-5
    NoiDungDanhGia NVARCHAR(MAX) NULL,
    NgayDanhGia DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_DanhGia_NguoiDung FOREIGN KEY (NguoiDungID) REFERENCES NguoiDung(ID)
);
GO

-- Bảng Thống kê truy cập
CREATE TABLE ThongKeTruyCap (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Ngay DATE NOT NULL,
    SoLuongTruyCap INT DEFAULT 0,
    SoLuongNguoiDungMoi INT DEFAULT 0,
    SoLuongBaiTest INT DEFAULT 0
);
GO

-- Tạo indexes để tối ưu hiệu suất
CREATE INDEX IX_NguoiDung_Email ON NguoiDung(Email);
CREATE INDEX IX_LichSuTest_NguoiDungID ON LichSuTest(NguoiDungID);
CREATE INDEX IX_LichSuTest_NgayLamTest ON LichSuTest(NgayLamTest);
CREATE INDEX IX_CauHoi_ThuTu ON CauHoi(ThuTu);
CREATE INDEX IX_CauTraLoi_CauHoiID ON CauTraLoi(CauHoiID);
CREATE INDEX IX_YeuCauHoTro_TrangThai ON YeuCauHoTro(TrangThai);
CREATE INDEX IX_ThongKeTruyCap_Ngay ON ThongKeTruyCap(Ngay);
CREATE INDEX IX_KetQuaChiTiet_LichSuTestID ON KetQuaChiTiet(LichSuTestID);
CREATE INDEX IX_KyNangCaNhan_LichSuTestID ON KyNangCaNhan(LichSuTestID);
GO

-- Chèn dữ liệu mẫu
-- Thêm dữ liệu mẫu cho bảng Ngành nghề
INSERT INTO NganhNghe (TenNganhNghe, MoTa, MucDoHot, IconCSS) VALUES
(N'Lập trình viên Full-stack', N'Phát triển cả frontend và backend của ứng dụng web', 9, 'fas fa-laptop-code'),
(N'Nhà phân tích dữ liệu', N'Phân tích và diễn giải dữ liệu phức tạp để đưa ra quyết định kinh doanh', 8, 'fas fa-chart-line'),
(N'Chuyên gia bảo mật', N'Bảo vệ hệ thống và dữ liệu khỏi các mối đe dọa an ninh mạng', 9, 'fas fa-shield-alt'),
(N'Lập trình viên Mobile', N'Phát triển ứng dụng cho thiết bị di động', 7, 'fas fa-mobile-alt'),
(N'Kỹ sư Trí tuệ nhân tạo', N'Phát triển và triển khai các giải pháp AI và machine learning', 10, 'fas fa-brain'),
(N'Kỹ sư DevOps', N'Kết hợp giữa phát triển phần mềm và vận hành hệ thống', 8, 'fas fa-server'),
(N'Quản trị cơ sở dữ liệu', N'Thiết kế, triển khai và bảo trì hệ thống cơ sở dữ liệu', 7, 'fas fa-database'),
(N'Kiểm thử phần mềm', N'Đảm bảo chất phần mềm thông qua kiểm thử', 6, 'fas fa-vial');
GO

-- Thêm dữ liệu mẫu cho bảng Kỹ năng
INSERT INTO KyNang (TenKyNang, MoTa) VALUES
(N'Kỹ thuật', N'Khả năng viết mã và phát triển phần mềm'),
(N'Giải quyết vấn đề', N'Khả năng phân tích và giải quyết các vấn đề phức tạp'),
(N'Sáng tạo', N'Khả năng nghĩ ra các giải pháp và ý tưởng mới'),
(N'Giao tiếp', N'Khả năng truyền đạt thông tin hiệu quả'),
(N'Làm việc nhóm', N'Khả năng hợp tác và làm việc với người khác'),
(N'Lãnh đạo', N'Khả năng dẫn dắt và quản lý nhóm'),
(N'Phân tích dữ liệu', N'Khả năng phân tích và diễn giải dữ liệu'),
(N'Thiết kế giao diện', N'Khả năng thiết kế giao diện người dùng trực quan và dễ sử dụng');
GO

-- Thêm dữ liệu mẫu cho bảng Yêu cầu kỹ năng
INSERT INTO YeuCauKyNang (NganhNgheID, KyNangID, MucDoQuanTrong) VALUES
(1, 1, 9), (1, 2, 8), (1, 4, 7), (1, 5, 8),
(2, 2, 9), (2, 7, 9), (2, 4, 8), (2, 5, 7),
(3, 1, 8), (3, 2, 9), (3, 6, 7), (3, 4, 7),
(4, 1, 8), (4, 3, 7), (4, 8, 9), (4, 5, 7),
(5, 1, 9), (5, 2, 9), (5, 3, 8), (5, 7, 9);
GO

-- Thêm dữ liệu mẫu cho bảng Mức lương
INSERT INTO MucLuong (NganhNgheID, KinhNghiem, MucLuongMin, MucLuongMax, MoTa) VALUES
(1, N'Fresher', 8000000, 12000000, N'Khởi điểm'),
(1, N'Junior', 12000000, 18000000, N'Trung bình'),
(1, N'Middle', 18000000, 25000000, N'Trung bình'),
(1, N'Senior', 25000000, 40000000, N'Kinh nghiệm'),
(2, N'Fresher', 9000000, 13000000, N'Khởi điểm'),
(2, N'Junior', 13000000, 20000000, N'Trung bình'),
(2, N'Middle', 20000000, 28000000, N'Trung bình'),
(2, N'Senior', 28000000, 45000000, N'Kinh nghiệm'),
(3, N'Fresher', 10000000, 15000000, N'Khởi điểm'),
(3, N'Junior', 15000000, 22000000, N'Trung bình'),
(3, N'Middle', 22000000, 32000000, N'Trung bình'),
(3, N'Senior', 32000000, 50000000, N'Kinh nghiệm');
GO

-- Thêm dữ liệu thị trường việc làm
INSERT INTO ThiTruongViecLam (NganhNgheID, NhuCauTuyenDung, CanhTranh, XuHuong) VALUES
(1, N'Cao', N'Trung bình', N'Tăng 15%/năm'),
(2, N'Cao', N'Cao', N'Tăng 20%/năm'),
(3, N'Rất cao', N'Thấp', N'Tăng 25%/năm'),
(4, N'Cao', N'Trung bình', N'Tăng 18%/năm'),
(5, N'Rất cao', N'Trung bình', N'Tăng 30%/năm');
GO

-- Thêm dữ liệu đào tạo
INSERT INTO DaoTaoNganhNghe (NganhNgheID, ThoiGianHoc, ChiPhi, HocOnline) VALUES
(1, N'6-12 tháng', N'5-15 triệu', 1),
(2, N'4-8 tháng', N'7-12 triệu', 1),
(3, N'8-15 tháng', N'10-20 triệu', 1),
(4, N'5-10 tháng', N'6-14 triệu', 1),
(5, N'9-18 tháng', N'12-25 triệu', 1);
GO

-- Thêm dữ liệu mẫu cho bảng Câu hỏi
INSERT INTO CauHoi (NoiDung, LoaiCauHoi, ThuTu, TrangThai) VALUES
(N'Bạn thích làm việc theo nhóm hay độc lập?', N'Tính cách', 1, 1),
(N'Bạn có thích lập trình không?', N'Sở thích', 2, 1),
(N'Bạn giỏi trong việc giải quyết vấn đề?', N'Kỹ năng', 3, 1),
(N'Bạn thích môi trường làm việc như thế nào?', N'Tính cách', 4, 1),
(N'Kỹ năng giao tiếp của bạn như thế nào?', N'Kỹ năng', 5, 1),
(N'Bạn có hứng thú với trí tuệ nhân tạo?', N'Sở thích', 6, 1),
(N'Bạn có khả năng làm việc dưới áp lực?', N'Kỹ năng', 7, 1),
(N'Bạn thích làm việc với dữ liệu hay giao diện?', N'Sở thích', 8, 1);
GO

-- Thêm dữ liệu mẫu cho bảng Câu trả lời
INSERT INTO CauTraLoi (CauHoiID, NoiDung, DiemSo) VALUES
(1, N'Theo nhóm', 8),
(1, N'Độc lập', 6),
(1, N'Cả hai', 9),
(1, N'Không chắc', 5),
(2, N'Rất thích', 9),
(2, N'Bình thường', 7),
(2, N'Không thích', 4),
(2, N'Chưa thử bao giờ', 5),
(3, N'Rất giỏi', 9),
(3, N'Khá giỏi', 7),
(3, N'Bình thường', 5),
(3, N'Không giỏi lắm', 3),
(4, N'Năng động, sáng tạo', 8),
(4, N'Ổn định, lâu dài', 6),
(4, N'Linh hoạt, tự do', 9),
(4, N'Có cấu trúc rõ ràng', 7),
(5, N'Rất tốt', 9),
(5, N'Khá tốt', 7),
(5, N'Trung bình', 5),
(5, N'Cần cải thiện', 3);
GO

-- Thêm dữ liệu mẫu người dùng
INSERT INTO NguoiDung (Email, MatKhau, Ho, Ten, AnhDaiDien) VALUES
('nguyenvana@example.com', 'hashed_password_1', N'Nguyễn', N'Văn A', NULL),
('tranthib@example.com', 'hashed_password_2', N'Trần', N'Thị B', NULL),
('levanC@example.com', 'hashed_password_3', N'Lê', N'Văn C', NULL);
GO

-- Thêm dữ liệu mẫu lịch sử test
INSERT INTO LichSuTest (NguoiDungID, NgayLamTest) VALUES
(1, GETDATE()),
(2, DATEADD(day, -2, GETDATE())),
(3, DATEADD(day, -5, GETDATE()));
GO

-- Thêm dữ liệu mẫu kết quả chi tiết
INSERT INTO KetQuaChiTiet (LichSuTestID, NganhNgheID, PhanTramPhuHop) VALUES
(1, 1, 92.0),
(1, 2, 88.0),
(1, 3, 85.0),
(1, 4, 82.0),
(1, 5, 78.0);
GO

-- Thêm dữ liệu mẫu kỹ năng cá nhân
INSERT INTO KyNangCaNhan (LichSuTestID, KyNangID, DiemSo) VALUES
(1, 1, 85),
(1, 2, 90),
(1, 3, 75),
(1, 4, 65),
(1, 5, 70),
(1, 6, 60);
GO

PRINT 'Cơ sở dữ liệu CareerQuizDB đã được tạo thành công và nạp dữ liệu mẫu!';

--1. Truy vấn đăng nhập người dùng
-- Kiểm tra thông tin đăng nhập
SELECT ID, Ho, Ten, Email, AnhDaiDien 
FROM NguoiDung 
WHERE Email = 'nguyenvana@example.com' AND MatKhau = 'hashed_password_1';
-- Lấy tất cả câu hỏi và câu trả lời
SELECT 
    ch.ID AS CauHoiID,
    ch.NoiDung AS CauHoi,
    ch.LoaiCauHoi,
    ch.ThuTu,
    ct.ID AS TraLoiID,
    ct.NoiDung AS TraLoi,
    ct.DiemSo
FROM CauHoi ch
INNER JOIN CauTraLoi ct ON ch.ID = ct.CauHoiID
WHERE ch.TrangThai = 1
ORDER BY ch.ThuTu, ct.ID;
-- Lưu lịch sử test
INSERT INTO LichSuTest (NguoiDungID, NgayLamTest) 
OUTPUT INSERTED.ID
VALUES (1, GETDATE());

-- Lưu kết quả chi tiết (% phù hợp với từng ngành)
INSERT INTO KetQuaChiTiet (LichSuTestID, NganhNgheID, PhanTramPhuHop)
VALUES 
(1, 1, 92.0),
(1, 2, 88.0),
(1, 3, 85.0);

-- Lưu điểm kỹ năng cá nhân (cho biểu đồ radar)
INSERT INTO KyNangCaNhan (LichSuTestID, KyNangID, DiemSo)
VALUES 
(1, 1, 85),
(1, 2, 90),
(1, 3, 75);
-- Lấy thông tin kết quả test
SELECT 
    ls.ID AS TestID,
    ls.NgayLamTest,
    nd.Ho + ' ' + nd.Ten AS HoTen,
    kq.NganhNgheID,
    nn.TenNganhNghe,
    nn.IconCSS,
    kq.PhanTramPhuHop
FROM LichSuTest ls
INNER JOIN NguoiDung nd ON ls.NguoiDungID = nd.ID
INNER JOIN KetQuaChiTiet kq ON ls.ID = kq.LichSuTestID
INNER JOIN NganhNghe nn ON kq.NganhNgheID = nn.ID
WHERE ls.ID = 1
ORDER BY kq.PhanTramPhuHop DESC;
-- Lấy thông tin chi tiết một ngành nghề
SELECT 
    nn.ID,
    nn.TenNganhNghe,
    nn.MoTa,
    nn.MucDoHot,
    nn.IconCSS,
    tt.NhuCauTuyenDung,
    tt.CanhTranh,
    tt.XuHuong,
    dt.ThoiGianHoc,
    dt.ChiPhi,
    dt.HocOnline,
    kn.TenKyNang,
    yk.MucDoQuanTrong
FROM NganhNghe nn
LEFT JOIN ThiTruongViecLam tt ON nn.ID = tt.NganhNgheID
LEFT JOIN DaoTaoNganhNghe dt ON nn.ID = dt.NganhNgheID
LEFT JOIN YeuCauKyNang yk ON nn.ID = yk.NganhNgheID
LEFT JOIN KyNang kn ON yk.KyNangID = kn.ID
WHERE nn.ID = 1;
-- Lấy thông tin mức lương theo ngành nghề
SELECT 
    nn.TenNganhNghe,
    ml.KinhNghiem,
    ml.MoTa,
    ml.MucLuongMin,
    ml.MucLuongMax,
    ml.DonViTienTe
FROM MucLuong ml
INNER JOIN NganhNghe nn ON ml.NganhNgheID = nn.ID
WHERE nn.ID = 1
ORDER BY 
    CASE 
        WHEN ml.MoTa = N'Khởi điểm' THEN 1
        WHEN ml.MoTa = N'Trung bình' THEN 2
        WHEN ml.MoTa = N'Kinh nghiệm' THEN 3
        ELSE 4
    END;
-- Lấy dữ liệu kỹ năng cá nhân cho biểu đồ radar
SELECT 
    kn.TenKyNang,
    kc.DiemSo
FROM KyNangCaNhan kc
INNER JOIN KyNang kn ON kc.KyNangID = kn.ID
WHERE kc.LichSuTestID = 1
ORDER BY kn.ID;
-- Lấy lịch sử làm test
SELECT 
    ls.ID AS TestID,
    ls.NgayLamTest,
    COUNT(kq.ID) AS SoNganhNghe,
    MAX(kq.PhanTramPhuHop) AS DiemCaoNhat
FROM LichSuTest ls
LEFT JOIN KetQuaChiTiet kq ON ls.ID = kq.LichSuTestID
WHERE ls.NguoiDungID = 1
GROUP BY ls.ID, ls.NgayLamTest
ORDER BY ls.NgayLamTest DESC;
-- Thống kê số lượng test theo ngày
SELECT 
    CONVERT(DATE, NgayLamTest) AS Ngay,
    COUNT(*) AS SoLuotTest
FROM LichSuTest
WHERE NgayLamTest >= DATEADD(day, -30, GETDATE())
GROUP BY CONVERT(DATE, NgayLamTest)
ORDER BY Ngay DESC;

-- Thống kê ngành nghề phổ biến
SELECT 
    nn.TenNganhNghe,
    COUNT(kq.ID) AS SoLuot,
    AVG(kq.PhanTramPhuHop) AS DiemTrungBinh
FROM KetQuaChiTiet kq
INNER JOIN NganhNghe nn ON kq.NganhNgheID = nn.ID
GROUP BY nn.ID, nn.TenNganhNghe
ORDER BY SoLuot DESC;
-- Tìm kiếm ngành nghề theo từ khóa
SELECT 
    ID,
    TenNganhNghe,
    MoTa,
    MucDoHot
FROM NganhNghe
WHERE 
    TenNganhNghe LIKE N'%lập trình%' 
    OR MoTa LIKE N'%lập trình%'
ORDER BY MucDoHot DESC;
-- Kiểm tra email đã tồn tại chưa
SELECT COUNT(*) FROM NguoiDung WHERE Email = 'email_moi@example.com';

-- Đăng ký người dùng mới
INSERT INTO NguoiDung (Email, MatKhau, Ho, Ten)
VALUES ('email_moi@example.com', 'hashed_password', N'Nguyễn', N'Văn B');

-- Lấy thông tin người dùng vừa đăng ký
SELECT ID, Ho, Ten, Email FROM NguoiDung WHERE Email = 'email_moi@example.com';
-- Cập nhật thông tin người dùng
UPDATE NguoiDung 
SET Ho = N'Nguyễn', Ten = N'Văn C', AnhDaiDien = 'avatar.png'
WHERE ID = 1;

-- Đổi mật khẩu
UPDATE NguoiDung 
SET MatKhau = 'hashed_password_moi'
WHERE ID = 1 AND MatKhau = 'hashed_password_cu';


use master;
DROP DATABASE IF EXISTS QuanLyCHXeMay;
CREATE DATABASE QuanLyCHXeMay;
GO
USE QuanLyCHXeMay;
GO
/* ============================================================
   1. QUẢN LÝ NGƯỜI DÙNG & PHÂN QUYỀN
   ============================================================ */
-- Bảng khách hàng
CREATE TABLE KhachHang (
    MaKH CHAR(10) PRIMARY KEY,         -- Mã khách hàng
    HoTenKH NVARCHAR(100) NOT NULL,    -- Họ tên khách hàng
    NgaySinh DATE,                     -- Ngày sinh
    GioiTinh NVARCHAR(10),             -- Giới tính
    Sdt VARCHAR(15),                   -- Số điện thoại
    Email VARCHAR(100),                -- Địa chỉ email
    DiaChi NVARCHAR(255),              -- Địa chỉ
    NgayTao DATETIME DEFAULT GETDATE(), -- Ngày tạo
    NgayCapNhat DATETIME DEFAULT GETDATE() -- Ngày cập nhật
);

ALTER TABLE KhachHang
ADD 
    SoCCCD VARCHAR(20) NULL UNIQUE,  -- Số CCCD 
    LoaiGiayTo NVARCHAR(50) NULL,    -- Giấy tờ
    AnhGiayTo VARBINARY(MAX) NULL;    -- ẢNh 

-- Bảng nhân viên
CREATE TABLE NhanVien (
    MaNV CHAR(10) PRIMARY KEY,         -- Mã nhân viên
    HoTenNV NVARCHAR(100) NOT NULL,    -- Họ tên nhân viên
    NgaySinh DATE NOT NULL,            -- Ngày sinh
    GioiTinh NVARCHAR(10),             -- Giới tính
    Sdt VARCHAR(15) UNIQUE,            -- Số điện thoại
    Email VARCHAR(100) UNIQUE,         -- Địa chỉ email
    DiaChi NVARCHAR(255),              -- Địa chỉ
    ChucVu NVARCHAR(50),               -- Chức vụ (Quản lý, Thu ngân, Kỹ thuật, Bán hàng)
    LuongCoBan DECIMAL(15,2),          -- Lương cơ bản
    TinhTrangLamViec NVARCHAR(20),     -- Còn làm, nghỉ làm, thử việc
    CCCD VARCHAR(20) UNIQUE,           -- Căn cước công dân
    TrinhDoHocVan NVARCHAR(100),       -- Trình độ học vấn
    AnhNhanVien VARBINARY(MAX),        -- URL ảnh nhân viên (lưu trên cloud)
    NgayTao DATETIME DEFAULT GETDATE(), -- Ngày tạo
    NgayCapNhat DATETIME DEFAULT GETDATE() -- Ngày cập nhật
);

ALTER TABLE NhanVien
ADD NgayVaoLam DATETIME NULL DEFAULT GETDATE();
-- Bảng tài khoản
CREATE TABLE TaiKhoan (
    MaTaiKhoan CHAR(10) PRIMARY KEY,     
    TenDangNhap VARCHAR(50) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL,

    LoaiTaiKhoan NVARCHAR(20) NOT NULL DEFAULT 'NhanVien',  -- 'NhanVien' hoặc 'KhachHang'

    TrangThaiTaiKhoan NVARCHAR(20) DEFAULT 'Hoạt động',

    MaNV CHAR(10) NULL,   -- Tài khoản nhân viên
    MaKH CHAR(10) NULL,   -- Tài khoản khách hàng

    NgayTao DATETIME DEFAULT GETDATE(),
    NgayCapNhat DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
);


CREATE TABLE NhaCungCap (
    MaNCC CHAR(10) PRIMARY KEY,
    TenNCC NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(255) NULL,
    Sdt VARCHAR(15) NULL,
    Email VARCHAR(100) NULL,
    GhiChu NVARCHAR(255) NULL
);

CREATE TABLE HangXe (
    MaHang CHAR(10) PRIMARY KEY,
    TenHang NVARCHAR(50) NOT NULL,
    MoTa NVARCHAR(255) NULL,
    Logo NVARCHAR(500) NULL
);

CREATE TABLE DongXe (
    MaDong CHAR(10) PRIMARY KEY,
    TenDong NVARCHAR(50) NOT NULL,
    MaHang CHAR(10) NOT NULL,
    PhanKhoi INT NULL,
    LoaiXe NVARCHAR(30) NULL,
    MoTa NVARCHAR(255) NULL,
    FOREIGN KEY (MaHang) REFERENCES HangXe(MaHang)
);

CREATE TABLE MauSac (
    MaMau CHAR(10) PRIMARY KEY,
    TenMau NVARCHAR(30) NOT NULL,
    GhiChu NVARCHAR(255) NULL
);

CREATE TABLE LoaiXe (
    ID_Loai CHAR(10) PRIMARY KEY,
    MaHang CHAR(10) NOT NULL,
    MaDong CHAR(10) NOT NULL,
    MaMau CHAR(10) NOT NULL,
    NamSX INT NULL,
    GhiChu NVARCHAR(255) NULL,
    UNIQUE (MaHang, MaDong, MaMau, NamSX),
    FOREIGN KEY (MaHang) REFERENCES HangXe(MaHang),
    FOREIGN KEY (MaDong) REFERENCES DongXe(MaDong),
    FOREIGN KEY (MaMau) REFERENCES MauSac(MaMau)
);

CREATE TABLE XeMay (
    ID_Xe CHAR(10) PRIMARY KEY,
    BienSo VARCHAR(15) UNIQUE NULL,
    ID_Loai CHAR(10) NOT NULL,
    MaNCC CHAR(10) NULL,
    NgayMua DATE NULL,
    GiaMua DECIMAL(15,2) NULL,
    NgayDangKy DATE NULL,
    HetHanDangKy DATE NULL,
    HetHanBaoHiem DATE NULL,
    KmDaChay INT NULL,
    ThongTinXang NVARCHAR(100) NULL,
    AnhXe VARBINARY(MAX) NULL,
	MucDichSuDung NVARCHAR(20) CHECK (MucDichSuDung IN (N'Cho thuê', N'Bán')),
	GiaNhap DECIMAL(15,2),
	SoLuong INT,
	SoLuongBanRa INT,
    TrangThai NVARCHAR(20) DEFAULT N'Sẵn sàng'
        CHECK (TrangThai IN (N'Sẵn sàng', N'Đang thuê', N'Đã bán', N'Đang bảo trì')),
    FOREIGN KEY (ID_Loai) REFERENCES LoaiXe(ID_Loai),
    FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC)
);

--
CREATE TABLE ThongTinGiaXe (
    ID_ThongTinGia INT IDENTITY(1,1) PRIMARY KEY,
    ID_Xe CHAR(10) NOT NULL,
    PhanLoai NVARCHAR(10) NOT NULL CHECK (PhanLoai IN (N'Bán', N'Thuê')),
    GiaBan DECIMAL(15,2),
    GiaThueNgay DECIMAL(15,2),
    TienDatCoc DECIMAL(15,2),
    NgayCapNhat DATE DEFAULT GETDATE(),
    GhiChu NVARCHAR(255),
    
    CONSTRAINT FK_ThongTinGiaXe_XeMay FOREIGN KEY (ID_Xe) REFERENCES XeMay(ID_Xe),
    CONSTRAINT UQ_ThongTinGiaXe_Xe_PhanLoai UNIQUE(ID_Xe, PhanLoai),
    CONSTRAINT CK_ThongTinGiaXe_Gia CHECK (
        (PhanLoai = N'Bán' AND GiaBan IS NOT NULL AND GiaThueNgay IS NULL AND TienDatCoc IS NULL) 
        OR (PhanLoai = N'Thuê' AND GiaThueNgay IS NOT NULL AND GiaBan IS NULL)
    )
);

-- Bảng lịch sử giá 
CREATE TABLE LichSuGia (
    ID_LichSuGia INT IDENTITY PRIMARY KEY,
    ID_Xe CHAR(10) NOT NULL,
    PhanLoai NVARCHAR(10) NOT NULL CHECK (PhanLoai IN ('Bán', 'Thuê')),
    GiaCu DECIMAL(15,2),
    GiaMoi DECIMAL(15,2),
    NgayThayDoi DATE DEFAULT GETDATE(),
    LyDoThayDoi NVARCHAR(255),
    MaTaiKhoan CHAR(10),
    GhiChu NVARCHAR(255),
    FOREIGN KEY (ID_Xe) REFERENCES XeMay(ID_Xe),
    FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan)
);

/* ============================================================
   3. QUẢN LÝ PHỤ TÙNG
   ============================================================ */
-- Bảng phụ tùng
CREATE TABLE PhuTung (
    MaPhuTung CHAR(10) PRIMARY KEY,     -- Mã phụ tùng
    TenPhuTung NVARCHAR(100) NOT NULL,  -- Tên phụ tùng (ví dụ: Lốp xe, Phanh)
    MaHangXe CHAR(10),                  -- Hãng xe tương thích (nếu cần)
    MaDongXe CHAR(10),                  -- Dòng xe tương thích (nếu cần)
    GiaMua DECIMAL(15,2),               -- Giá mua
    GiaBan DECIMAL(15,2),               -- Giá bán
    DonViTinh NVARCHAR(20),             -- Đơn vị (cái, bộ,...)
    GhiChu NVARCHAR(255),               -- Ghi chú
    FOREIGN KEY (MaHangXe) REFERENCES HangXe(MaHang),
    FOREIGN KEY (MaDongXe) REFERENCES DongXe(MaDong)
);

-- Bảng kho phụ tùng
CREATE TABLE KhoPhuTung (
    ID_KhoPhuTung INT IDENTITY PRIMARY KEY, -- Mã kho phụ tùng
    MaPhuTung CHAR(10) NOT NULL,            -- Mã phụ tùng
    SoLuongTon INT DEFAULT 0,               -- Số lượng tồn kho
    NgayCapNhat DATE DEFAULT GETDATE(),     -- Ngày cập nhật
    GhiChu NVARCHAR(255),                   -- Ghi chú
    UNIQUE (MaPhuTung),                     -- Tránh trùng lặp
    FOREIGN KEY (MaPhuTung) REFERENCES PhuTung(MaPhuTung)
);

-- Bảng ghi chú bảo trì xe
CREATE TABLE BaoTriXe (
    ID_BaoTri INT IDENTITY PRIMARY KEY, -- Mã bảo trì
    ID_Xe CHAR(10) NOT NULL,            -- Mã xe
    MaTaiKhoan CHAR(10),                -- Nhân viên kỹ thuật
    GhiChuBaoTri NVARCHAR(255),         -- Ghi chú bảo trì
    FOREIGN KEY (ID_Xe) REFERENCES XeMay(ID_Xe),
    FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan)
);

-- Bảng chi tiết bảo trì
CREATE TABLE ChiTietBaoTri (
    ID_ChiTiet INT IDENTITY PRIMARY KEY,-- Mã chi tiết
    ID_BaoTri INT NOT NULL,             -- Mã bảo trì
    MaPhuTung CHAR(10) NOT NULL,        -- Mã phụ tùng
    SoLuong INT NOT NULL,               -- Số lượng sử dụng
    GiaSuDung DECIMAL(15,2),            -- Giá sử dụng (SoLuong * GiaBan)
    GhiChu NVARCHAR(255),               -- Ghi chú
    FOREIGN KEY (ID_BaoTri) REFERENCES BaoTriXe(ID_BaoTri),
    FOREIGN KEY (MaPhuTung) REFERENCES PhuTung(MaPhuTung)
);

/* ============================================================
   4. QUẢN LÝ GIAO DỊCH
   ============================================================ */
-- Bảng giao dịch bán
CREATE TABLE GiaoDichBan (
    MaGDBan INT IDENTITY PRIMARY KEY,   -- Mã giao dịch bán
    MaKH CHAR(10) NOT NULL,             -- Mã khách hàng
    ID_Xe CHAR(10) NOT NULL,            -- Mã xe
    NgayBan DATE NOT NULL,              -- Ngày bán
    GiaBan DECIMAL(15,2) NOT NULL,      -- Giá bán
    TrangThaiThanhToan NVARCHAR(20),    -- Đã thanh toán / Chưa thanh toán
    HinhThucThanhToan NVARCHAR(50),     -- Hình thức thanh toán
    MaTaiKhoan CHAR(10),                -- Nhân viên thu ngân
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (ID_Xe) REFERENCES XeMay(ID_Xe),
    FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan)
);

-- Bảng giao dịch thuê
CREATE TABLE GiaoDichThue (
    MaGDThue INT IDENTITY PRIMARY KEY,  -- Mã giao dịch thuê
    ID_Xe CHAR(10) NOT NULL,            -- Mã xe
    MaKH CHAR(10) NOT NULL,             -- Mã khách hàng
    NgayBatDau DATE NOT NULL,           -- Ngày bắt đầu thuê
    NgayKetThuc DATE NOT NULL,          -- Ngày kết thúc thuê
    GiaThueNgay DECIMAL(15,2) NOT NULL, -- Giá thuê 1 ngày
    TongGia DECIMAL(15,2) NOT NULL,     -- Tổng giá
    TrangThai NVARCHAR(20),             -- Đang thuê / Đã thuê / Hủy
    TrangThaiThanhToan NVARCHAR(20),    -- Đã thanh toán / Chưa thanh toán
    HinhThucThanhToan NVARCHAR(50),     -- Hình thức thanh toán
    SoTienCoc DECIMAL(15,2),            -- Số tiền cọc
    GiayToGiuLai NVARCHAR(100),         -- Giấy tờ giữ lại
    MaTaiKhoan CHAR(10),                -- Nhân viên thu ngân
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (ID_Xe) REFERENCES XeMay(ID_Xe),
    FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan)
);

-- Thêm các cột còn thiếu
ALTER TABLE GiaoDichThue ADD TrangThaiDuyet NVARCHAR(20) DEFAULT N'Chờ duyệt';
ALTER TABLE GiaoDichThue ADD NguoiDuyet CHAR(10) NULL;
ALTER TABLE GiaoDichThue ADD NgayDuyet DATETIME NULL;
ALTER TABLE GiaoDichThue ADD GhiChuDuyet NVARCHAR(255) NULL;
-- Thêm các cột theo dõi thực tế vào GiaoDichThue
ALTER TABLE GiaoDichThue ADD NgayGiaoXeThucTe DATETIME NULL;
ALTER TABLE GiaoDichThue ADD NgayTraXeThucTe DATETIME NULL;
ALTER TABLE GiaoDichThue ADD GhiChuGiaoXe NVARCHAR(255) NULL;
ALTER TABLE GiaoDichThue ADD KmBatDau INT NULL;
ALTER TABLE GiaoDichTraThue ADD KmKetThuc INT NULL;
ALTER TABLE GiaoDichTraThue ADD SoNgayTraSom INT NULL DEFAULT 0;
ALTER TABLE GiaoDichTraThue ADD TienHoanTraSom DECIMAL(15,2) NULL DEFAULT 0;
-- 3. Cập nhật lại constraint cho TrangThai
ALTER TABLE GiaoDichThue DROP CONSTRAINT IF EXISTS CK_GiaoDichThue_TrangThai;
ALTER TABLE GiaoDichThue ADD CONSTRAINT CK_GiaoDichThue_TrangThai 
    CHECK (TrangThai IN (N'Chờ xác nhận', N'Chờ giao xe', N'Đang thuê', N'Đã thuê', N'Hủy'));

-- 4. Thêm index để tăng tốc truy vấn
CREATE INDEX IX_GiaoDichThue_XeNgay 
    ON GiaoDichThue(ID_Xe, NgayBatDau, NgayKetThuc, TrangThai);

CREATE INDEX IX_GiaoDichThue_TrangThaiDuyet 
    ON GiaoDichThue(TrangThaiDuyet, TrangThai);

-- 5. Thêm trigger tự động cập nhật trạng thái xe
GO
CREATE OR ALTER TRIGGER trg_UpdateXeStatus_AfterGiaoXe
ON GiaoDichThue
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Khi giao xe (TrangThai chuyển sang "Đang thuê")
    IF UPDATE(TrangThai)
    BEGIN
        UPDATE XeMay
        SET TrangThai = N'Đang thuê'
        FROM XeMay xe
        INNER JOIN inserted i ON xe.ID_Xe = i.ID_Xe
        WHERE i.TrangThai = N'Đang thuê';
        
        -- Khi trả xe (TrangThai chuyển sang "Đã thuê")
        UPDATE XeMay
        SET TrangThai = N'Sẵn sàng'
        FROM XeMay xe
        INNER JOIN inserted i ON xe.ID_Xe = i.ID_Xe
        WHERE i.TrangThai = N'Đã thuê';
    END
END
-- Bảng giao dịch trả thuê
CREATE TABLE GiaoDichTraThue (
    MaGDTraThue INT IDENTITY PRIMARY KEY, -- Mã giao dịch trả thuê
    MaGDThue INT NOT NULL,                -- Mã giao dịch thuê
    NgayTraXe DATE NOT NULL,              -- Ngày trả xe
    TinhTrangXe NVARCHAR(100),            -- Tình trạng xe khi trả
    ChiPhiPhatSinh DECIMAL(15,2),         -- Chi phí phát sinh
    GhiChu NVARCHAR(255),                 -- Ghi chú
    MaTaiKhoan CHAR(10),                  -- Nhân viên bán hàng
    FOREIGN KEY (MaGDThue) REFERENCES GiaoDichThue(MaGDThue),
    FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan)
);

-- Bảng hợp đồng mua xe
CREATE TABLE HopDongMua (
    MaHDM INT IDENTITY PRIMARY KEY,     -- Mã hợp đồng mua
    MaGDBan INT NOT NULL,               -- Mã giao dịch bán
    MaKH CHAR(10) NOT NULL,             -- Mã khách hàng
    MaTaiKhoan CHAR(10) NOT NULL,       -- Mã nhân viên (Quản lý hoặc Bán hàng)
    ID_Xe CHAR(10) NOT NULL,            -- Mã xe
    NgayLap DATE NOT NULL,              -- Ngày lập hợp đồng
    GiaBan DECIMAL(15,2) NOT NULL,      -- Giá bán
    DieuKhoan NVARCHAR(500),            -- Điều khoản
    GhiChu NVARCHAR(255),               -- Ghi chú
    TrangThaiHopDong NVARCHAR(20) CHECK (TrangThaiHopDong IN (N'Đang hiệu lực', N'Hết hạn', N'Hủy')), -- Trạng thái hợp đồng
    FileHopDong VARCHAR(500),           -- URL file PDF hợp đồng
    FOREIGN KEY (MaGDBan) REFERENCES GiaoDichBan(MaGDBan),
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan),
    FOREIGN KEY (ID_Xe) REFERENCES XeMay(ID_Xe)
);

drop table HopDongMua
-- Bảng hợp đồng thuê xe
CREATE TABLE HopDongThue (
    MaHDT INT IDENTITY PRIMARY KEY,     -- Mã hợp đồng thuê
    MaGDThue INT NOT NULL,              -- Mã giao dịch thuê
    MaKH CHAR(10) NOT NULL,             -- Mã khách hàng
    MaTaiKhoan CHAR(10) NOT NULL,       -- Mã nhân viên (Quản lý hoặc Bán hàng)
    ID_Xe CHAR(10) NOT NULL,            -- Mã xe
    NgayLap DATE NOT NULL,              -- Ngày lập hợp đồng
    NgayBatDau DATE NOT NULL,           -- Ngày bắt đầu thuê
    NgayKetThuc DATE NOT NULL,          -- Ngày kết thúc thuê
    GiaThue DECIMAL(15,2) NOT NULL,     -- Giá thuê
    TienDatCoc DECIMAL(15,2),           -- Tiền đặt cọc
    DieuKhoan NVARCHAR(500),            -- Điều khoản
    GhiChu NVARCHAR(255),               -- Ghi chú
    TrangThaiHopDong NVARCHAR(20) CHECK (TrangThaiHopDong IN ('Đang hiệu lực', 'Hết hạn', 'Hủy')), -- Trạng thái hợp đồng
    FileHopDong VARCHAR(500),           -- URL file PDF hợp đồng
    FOREIGN KEY (MaGDThue) REFERENCES GiaoDichThue(MaGDThue),
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan),
    FOREIGN KEY (ID_Xe) REFERENCES XeMay(ID_Xe)
);

-- Bảng thanh toán
CREATE TABLE ThanhToan (
    MaThanhToan INT IDENTITY PRIMARY KEY, -- Mã thanh toán
    MaGDBan INT NULL,                    -- Mã giao dịch bán
    MaGDThue INT NULL,                   -- Mã giao dịch thuê
    SoTien DECIMAL(15,2) NOT NULL,       -- Số tiền thanh toán
    NgayThanhToan DATE NOT NULL,         -- Ngày thanh toán
    PhuongThuc NVARCHAR(50),             -- Phương thức thanh toán
    GhiChu NVARCHAR(255),                -- Ghi chú
    FOREIGN KEY (MaGDBan) REFERENCES GiaoDichBan(MaGDBan),
    FOREIGN KEY (MaGDThue) REFERENCES GiaoDichThue(MaGDThue),
    CONSTRAINT CHK_ThanhToan_OneGiaoDich CHECK (
        (MaGDBan IS NOT NULL AND MaGDThue IS NULL) OR 
        (MaGDBan IS NULL AND MaGDThue IS NOT NULL)
    )
);

/* ============================================================
   5. KHUYẾN MÃI & HÓA ĐƠN
   ============================================================ */
-- Bảng khuyến mãi
CREATE TABLE KhuyenMai (
    MaKM CHAR(10) PRIMARY KEY,          -- Mã khuyến mãi
    TenKM NVARCHAR(100) NOT NULL,       -- Tên chương trình
    MoTa NVARCHAR(255),                 -- Mô tả
    NgayBatDau DATE NOT NULL,           -- Ngày bắt đầu
    NgayKetThuc DATE NOT NULL,          -- Ngày kết thúc
    PhanTramGiam DECIMAL(5,2) NOT NULL, -- % giảm giá
    DieuKienApDung NVARCHAR(255),       -- Điều kiện
    LoaiApDung NVARCHAR(20) CHECK (LoaiApDung IN ('XeBan', 'XeThue', 'PhuTung')), -- Loại áp dụng
    GhiChu NVARCHAR(255)                -- Ghi chú
);

-- Bảng hóa đơn
CREATE TABLE HoaDon (
    MaHD INT IDENTITY PRIMARY KEY,      -- Mã hóa đơn
    MaKH CHAR(10) NOT NULL,             -- Mã khách hàng
    MaTaiKhoan CHAR(10) NOT NULL,       -- Mã nhân viên thu ngân
    NgayLap DATE NOT NULL,              -- Ngày lập hóa đơn
    TongTien DECIMAL(15,2) NOT NULL,    -- Tổng tiền trước khuyến mãi
    MaKM CHAR(10) NULL,                 -- Mã khuyến mãi
    SoTienGiam DECIMAL(15,2) DEFAULT 0, -- Số tiền giảm
    ThanhTien DECIMAL(15,2) NOT NULL,   -- Số tiền thanh toán
    HinhThucThanhToan NVARCHAR(50),     -- Hình thức thanh toán
    GhiChu NVARCHAR(255),               -- Ghi chú
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan),
    FOREIGN KEY (MaKM) REFERENCES KhuyenMai(MaKM)
);

-- Bảng chi tiết hóa đơn
CREATE TABLE ChiTietHoaDon (
    MaChiTiet INT IDENTITY PRIMARY KEY, -- Mã chi tiết hóa đơn
    MaHD INT NOT NULL,                  -- Mã hóa đơn
    MaGDBan INT NULL,                   -- Mã giao dịch bán
    MaGDThue INT NULL,                  -- Mã giao dịch thuê
    SoTien DECIMAL(15,2) NOT NULL,      -- Số tiền
    GhiChu NVARCHAR(255),               -- Ghi chú
    FOREIGN KEY (MaHD) REFERENCES HoaDon(MaHD),
    FOREIGN KEY (MaGDBan) REFERENCES GiaoDichBan(MaGDBan),
    FOREIGN KEY (MaGDThue) REFERENCES GiaoDichThue(MaGDThue),
    CONSTRAINT CHK_ChiTietHoaDon_OneGiaoDich CHECK (
        (MaGDBan IS NOT NULL AND MaGDThue IS NULL) OR 
        (MaGDBan IS NULL AND MaGDThue IS NOT NULL)
    )
);
-- ============================================================
-- SỬA TRIGGER: Xử lý trạng thái xe theo quy trình đầy đủ
-- ============================================================
GO
DROP TRIGGER IF EXISTS trg_UpdateXeStatus_AfterGiaoXe;
GO

CREATE TRIGGER trg_UpdateXeStatus_OnGiaoDichThue
ON GiaoDichThue
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- =====================================================
    -- 1. KHI TẠO ĐƠN MỚI (INSERT) - Giữ nguyên xe Sẵn sàng
    -- =====================================================
    -- Không cần xử lý gì, xe vẫn ở trạng thái "Sẵn sàng"
    
    -- =====================================================
    -- 2. KHI DUYỆT ĐƠN (TrangThaiDuyet = 'Đã duyệt')
    -- =====================================================
    IF UPDATE(TrangThaiDuyet)
    BEGIN
        -- Duyệt đơn: Chuyển TrangThai GD sang "Chờ giao xe"
        UPDATE gd
        SET TrangThai = N'Chờ giao xe'
        FROM GiaoDichThue gd
        INNER JOIN inserted i ON gd.MaGDThue = i.MaGDThue
        WHERE i.TrangThaiDuyet = N'Đã duyệt'
          AND gd.TrangThai = N'Chờ xác nhận';
        
        -- Từ chối đơn: Chuyển TrangThai GD sang "Hủy"
        UPDATE gd
        SET TrangThai = N'Hủy'
        FROM GiaoDichThue gd
        INNER JOIN inserted i ON gd.MaGDThue = i.MaGDThue
        WHERE i.TrangThaiDuyet = N'Từ chối';
        
        -- Lưu ý: Xe vẫn giữ trạng thái "Sẵn sàng" khi duyệt
        -- Chỉ chuyển sang "Đang thuê" khi GIAO XE
    END
    
    -- =====================================================
    -- 3. KHI GIAO XE (TrangThai = 'Đang thuê')
    -- =====================================================
    IF UPDATE(TrangThai)
    BEGIN
        -- Giao xe: Chuyển xe sang "Đang thuê"
        UPDATE xe
        SET TrangThai = N'Đang thuê'
        FROM XeMay xe
        INNER JOIN inserted i ON xe.ID_Xe = i.ID_Xe
        WHERE i.TrangThai = N'Đang thuê'
          AND i.TrangThaiDuyet = N'Đã duyệt';
        
        -- Trả xe: Chuyển xe về "Sẵn sàng"
        UPDATE xe
        SET TrangThai = N'Sẵn sàng'
        FROM XeMay xe
        INNER JOIN inserted i ON xe.ID_Xe = i.ID_Xe
        WHERE i.TrangThai = N'Đã thuê';
        
        -- Hủy đơn: Trả xe về "Sẵn sàng" (nếu chưa giao)
        UPDATE xe
        SET TrangThai = N'Sẵn sàng'
        FROM XeMay xe
        INNER JOIN inserted i ON xe.ID_Xe = i.ID_Xe
        WHERE i.TrangThai = N'Hủy'
          AND xe.TrangThai != N'Sẵn sàng';
    END
END
/* ============================================================
   DỮ LIỆU MẪU ĐẦY ĐỦ - HỆ THỐNG QUẢN LÝ XE MÁY
   ============================================================ */

-- Tắt kiểm tra khóa ngoại tạm thời
SET NOCOUNT ON;
GO

/* ============================================================
   1. KHÁCH HÀNG
   ============================================================ */
INSERT INTO KhachHang (MaKH, HoTenKH, NgaySinh, GioiTinh, Sdt, Email, DiaChi, SoCCCD, LoaiGiayTo)
VALUES 
('KH001', N'Nguyễn Văn An', '1990-05-15', N'Nam', '0901234567', 'nva@gmail.com', N'123 Nguyễn Huệ, Q1, TP.HCM', '001090012345', N'CCCD'),
('KH002', N'Trần Thị Bình', '1992-08-20', N'Nữ', '0902345678', 'ttb@gmail.com', N'456 Lê Lợi, Q1, TP.HCM', '001092023456', N'CCCD'),
('KH003', N'Lê Văn Cường', '1988-03-10', N'Nam', '0903456789', 'lvc@gmail.com', N'789 Trần Hưng Đạo, Q5, TP.HCM', '001088034567', N'CCCD'),
('KH004', N'Phạm Thị Dung', '1995-11-25', N'Nữ', '0904567890', 'ptd@gmail.com', N'321 Điện Biên Phủ, Q3, TP.HCM', '001095045678', N'CCCD'),
('KH005', N'Hoàng Văn Em', '1993-07-18', N'Nam', '0905678901', 'hve@gmail.com', N'654 Võ Văn Tần, Q3, TP.HCM', '001093056789', N'CCCD'),
('KH006', N'Đỗ Thị Phương', '1991-02-14', N'Nữ', '0906789012', 'dtp@gmail.com', N'987 Hai Bà Trưng, Q1, TP.HCM', '001091067890', N'CCCD'),
('KH007', N'Vũ Văn Giang', '1989-09-30', N'Nam', '0907890123', 'vvg@gmail.com', N'147 Pasteur, Q3, TP.HCM', '001089078901', N'CCCD'),
('KH008', N'Bùi Thị Hà', '1994-12-05', N'Nữ', '0908901234', 'bth@gmail.com', N'258 Cách Mạng Tháng 8, Q10, TP.HCM', '001094089012', N'CCCD'),
('KH009', N'Ngô Văn Inh', '1987-04-22', N'Nam', '0909012345', 'nvi@gmail.com', N'369 Lý Thường Kiệt, Q10, TP.HCM', '001087090123', N'CCCD'),
('KH010', N'Đinh Thị Kim', '1996-06-08', N'Nữ', '0900123456', 'dtk@gmail.com', N'741 Nguyễn Thị Minh Khai, Q3, TP.HCM', '001096001234', N'CCCD');

/* ============================================================
   2. NHÂN VIÊN
   ============================================================ */
INSERT INTO NhanVien (MaNV, HoTenNV, NgaySinh, GioiTinh, Sdt, Email, DiaChi, ChucVu, LuongCoBan, TinhTrangLamViec, CCCD, TrinhDoHocVan)
VALUES 
('NV001', N'Nguyễn Văn Quản', '1985-01-15', N'Nam', '0911111111', 'nvq@company.com', N'100 Lê Duẩn, Q1, TP.HCM', N'Quản lý', 20000000, N'Còn làm', '001085011111', N'Đại học'),
('NV002', N'Trần Thị Lan', '1990-06-20', N'Nữ', '0922222222', 'ttl@company.com', N'200 Nguyễn Văn Linh, Q7, TP.HCM', N'Thu ngân', 10000000, N'Còn làm', '001090022222', N'Cao đẳng'),
('NV003', N'Lê Văn Minh', '1992-03-10', N'Nam', '0933333333', 'lvm@company.com', N'300 Võ Văn Kiệt, Q5, TP.HCM', N'Kỹ thuật', 12000000, N'Còn làm', '001092033333', N'Trung cấp'),
('NV004', N'Phạm Thị Nga', '1993-08-25', N'Nữ', '0944444444', 'ptn@company.com', N'400 Cộng Hòa, Tân Bình, TP.HCM', N'Bán hàng', 9000000, N'Còn làm', '001093044444', N'Cao đẳng'),
('NV005', N'Hoàng Văn Phong', '1991-11-05', N'Nam', '0955555555', 'hvp@company.com', N'500 Hoàng Văn Thụ, Tân Bình, TP.HCM', N'Bán hàng', 9000000, N'Còn làm', '001091055555', N'Cao đẳng'),
('NV006', N'Đỗ Thị Quyên', '1994-02-18', N'Nữ', '0966666666', 'dtq@company.com', N'600 3 Tháng 2, Q10, TP.HCM', N'Thu ngân', 10000000, N'Còn làm', '001094066666', N'Cao đẳng'),
('NV007', N'Vũ Văn Sơn', '1988-07-12', N'Nam', '0977777777', 'vvs@company.com', N'700 Lạc Long Quân, Q11, TP.HCM', N'Kỹ thuật', 12000000, N'Còn làm', '001088077777', N'Trung cấp'),
('NV008', N'Bùi Thị Tâm', '1995-09-30', N'Nữ', '0988888888', 'btt@company.com', N'800 Phan Văn Trị, Gò Vấp, TP.HCM', N'Bán hàng', 9000000, N'Thử việc', '001095088888', N'Cao đẳng');

/* ============================================================
   3. TÀI KHOẢN
   ============================================================ */
INSERT INTO TaiKhoan (MaTaiKhoan, TenDangNhap, Password, LoaiTaiKhoan, TrangThaiTaiKhoan, MaNV, MaKH)
VALUES 
('TK001', 'quanly01', 'password123', N'NhanVien', N'Hoạt động', 'NV001', NULL),
('TK002', 'thungan01', 'password123', N'NhanVien', N'Hoạt động', 'NV002', NULL),
('TK003', 'kythuat01', 'password123', N'NhanVien', N'Hoạt động', 'NV003', NULL),
('TK004', 'banhang01', 'password123', N'NhanVien', N'Hoạt động', 'NV004', NULL),
('TK005', 'banhang02', 'password123', N'NhanVien', N'Hoạt động', 'NV005', NULL),
('TK006', 'thungan02', 'password123', N'NhanVien', N'Hoạt động', 'NV006', NULL),
('TK007', 'kythuat02', 'password123', N'NhanVien', N'Hoạt động', 'NV007', NULL),
('TK008', 'banhang03', 'password123', N'NhanVien', N'Hoạt động', 'NV008', NULL),
('TK009', 'kh001', 'password123', N'KhachHang', N'Hoạt động', NULL, 'KH001'),
('TK010', 'kh002', 'password123', N'KhachHang', N'Hoạt động', NULL, 'KH002');

/* ============================================================
   4. NHÀ CUNG CẤP
   ============================================================ */
INSERT INTO NhaCungCap (MaNCC, TenNCC, DiaChi, Sdt, Email, GhiChu)
VALUES 
('NCC001', N'Công ty TNHH Honda Việt Nam', N'Khu CN Vĩnh Lộc, Bình Chánh, TP.HCM', '0281234567', 'info@honda.vn', N'Nhà phân phối chính hãng'),
('NCC002', N'Yamaha Motor Việt Nam', N'KCN Nhơn Trạch, Đồng Nai', '0282345678', 'info@yamaha-motor.vn', N'Nhà phân phối chính hãng'),
('NCC003', N'Suzuki Việt Nam', N'Đường số 9, KCN Việt Nam - Singapore, Bình Dương', '0283456789', 'info@suzuki.vn', N'Nhà phân phối chính hãng'),
('NCC004', N'SYM Việt Nam', N'Lô E2a-3, Đường D1, KCN Phú Mỹ, Bà Rịa - Vũng Tàu', '0284567890', 'info@sym.vn', N'Nhà phân phối chính hãng'),
('NCC005', N'Piaggio Việt Nam', N'KCN Tân Thuận, Q7, TP.HCM', '0285678901', 'info@piaggio.vn', N'Nhà phân phối chính hãng');

/* ============================================================
   5. HÃNG XE
   ============================================================ */
INSERT INTO HangXe (MaHang, TenHang, MoTa, Logo)
VALUES 
('HX001', 'Honda', N'Hãng xe Nhật Bản, nổi tiếng về độ bền', 'honda_logo.png'),
('HX002', 'Yamaha', N'Hãng xe Nhật Bản, thiết kế thể thao', 'yamaha_logo.png'),
('HX003', 'Suzuki', N'Hãng xe Nhật Bản', 'suzuki_logo.png'),
('HX004', 'SYM', N'Hãng xe Đài Loan', 'sym_logo.png'),
('HX005', 'Piaggio', N'Hãng xe Italia, sang trọng', 'piaggio_logo.png');

/* ============================================================
   6. DÒNG XE
   ============================================================ */
INSERT INTO DongXe (MaDong, TenDong, MaHang, PhanKhoi, LoaiXe, MoTa)
VALUES 
-- Honda
('DX001', 'Wave', 'HX001', 110, N'Xe số', N'Dòng xe tiết kiệm nhiên liệu'),
('DX002', 'Vision', 'HX001', 110, N'Xe tay ga', N'Xe tay ga phổ thông'),
('DX003', 'Air Blade', 'HX001', 125, N'Xe tay ga', N'Xe tay ga thể thao'),
('DX004', 'SH', 'HX001', 160, N'Xe tay ga', N'Xe tay ga cao cấp'),
-- Yamaha
('DX005', 'Exciter', 'HX002', 155, N'Xe số', N'Xe côn tay thể thao'),
('DX006', 'Jupiter', 'HX002', 115, N'Xe số', N'Xe số phổ thông'),
('DX007', 'Janus', 'HX002', 125, N'Xe tay ga', N'Xe tay ga retro'),
('DX008', 'FreeGo', 'HX002', 125, N'Xe tay ga', N'Xe tay ga đa dụng'),
-- Suzuki
('DX009', 'Raider', 'HX003', 150, N'Xe số', N'Xe số thể thao'),
('DX010', 'Axelo', 'HX003', 125, N'Xe tay ga', N'Xe tay ga thời trang'),
-- SYM
('DX011', 'Attila', 'HX004', 125, N'Xe tay ga', N'Xe tay ga phổ thông'),
('DX012', 'Elizabeth', 'HX004', 125, N'Xe tay ga', N'Xe tay ga dành cho nữ'),
-- Piaggio
('DX013', 'Liberty', 'HX005', 125, N'Xe tay ga', N'Xe tay ga Italia'),
('DX014', 'Medley', 'HX005', 150, N'Xe tay ga', N'Xe tay ga cao cấp');

/* ============================================================
   7. MÀU SẮC
   ============================================================ */
INSERT INTO MauSac (MaMau, TenMau, GhiChu)
VALUES 
('MS001', N'Đỏ', N'Màu đỏ'),
('MS002', N'Xanh dương', N'Màu xanh dương'),
('MS003', N'Đen', N'Màu đen'),
('MS004', N'Trắng', N'Màu trắng'),
('MS005', N'Xám', N'Màu xám'),
('MS006', N'Vàng', N'Màu vàng'),
('MS007', N'Xanh lá', N'Màu xanh lá'),
('MS008', N'Nâu', N'Màu nâu'),
('MS009', N'Bạc', N'Màu bạc'),
('MS010', N'Xanh ngọc', N'Màu xanh ngọc');

/* ============================================================
   8. LOẠI XE
   ============================================================ */
INSERT INTO LoaiXe (ID_Loai, MaHang, MaDong, MaMau, NamSX, GhiChu)
VALUES 
-- Honda Wave
('LX001', 'HX001', 'DX001', 'MS001', 2023, N'Wave đỏ 2023'),
('LX002', 'HX001', 'DX001', 'MS003', 2023, N'Wave đen 2023'),
('LX003', 'HX001', 'DX001', 'MS002', 2024, N'Wave xanh 2024'),
-- Honda Vision
('LX004', 'HX001', 'DX002', 'MS004', 2023, N'Vision trắng 2023'),
('LX005', 'HX001', 'DX002', 'MS001', 2024, N'Vision đỏ 2024'),
-- Honda Air Blade
('LX006', 'HX001', 'DX003', 'MS003', 2023, N'Air Blade đen 2023'),
('LX007', 'HX001', 'DX003', 'MS002', 2024, N'Air Blade xanh 2024'),
-- Honda SH
('LX008', 'HX001', 'DX004', 'MS005', 2023, N'SH xám 2023'),
('LX009', 'HX001', 'DX004', 'MS003', 2024, N'SH đen 2024'),
-- Yamaha Exciter
('LX010', 'HX002', 'DX005', 'MS002', 2023, N'Exciter xanh 2023'),
('LX011', 'HX002', 'DX005', 'MS001', 2024, N'Exciter đỏ 2024'),
-- Yamaha Jupiter
('LX012', 'HX002', 'DX006', 'MS003', 2023, N'Jupiter đen 2023'),
-- Yamaha Janus
('LX013', 'HX002', 'DX007', 'MS006', 2023, N'Janus vàng 2023'),
('LX014', 'HX002', 'DX007', 'MS010', 2024, N'Janus xanh ngọc 2024'),
-- Yamaha FreeGo
('LX015', 'HX002', 'DX008', 'MS002', 2024, N'FreeGo xanh 2024'),
-- Suzuki Raider
('LX016', 'HX003', 'DX009', 'MS003', 2023, N'Raider đen 2023'),
-- Suzuki Axelo
('LX017', 'HX003', 'DX010', 'MS009', 2023, N'Axelo bạc 2023'),
-- SYM Attila
('LX018', 'HX004', 'DX011', 'MS003', 2024, N'Attila đen 2024'),
-- SYM Elizabeth
('LX019', 'HX004', 'DX012', 'MS004', 2024, N'Elizabeth trắng 2024'),
-- Piaggio Liberty
('LX020', 'HX005', 'DX013', 'MS009', 2024, N'Liberty bạc 2024');

/* ============================================================
   9. XE MÁY
   ============================================================ */
INSERT INTO XeMay (ID_Xe, BienSo, ID_Loai, MaNCC, NgayMua, GiaMua, NgayDangKy, HetHanDangKy, HetHanBaoHiem, KmDaChay, ThongTinXang, MucDichSuDung, GiaNhap, SoLuong, SoLuongBanRa, TrangThai)
VALUES 
-- Xe cho thuê
('XE001', '59A1-12345', 'LX001', 'NCC001', '2023-01-15', 25000000, '2023-01-20', '2025-01-20', '2024-12-31', 1500, N'Xăng A95', N'Cho thuê', 25000000, 1, 0, N'Sẵn sàng'),
('XE002', '59B2-23456', 'LX002', 'NCC001', '2023-02-10', 26000000, '2023-02-15', '2025-02-15', '2024-12-31', 2000, N'Xăng A95', N'Cho thuê', 26000000, 1, 0, N'Sẵn sàng'),
('XE003', '59C3-34567', 'LX004', 'NCC001', '2023-03-05', 30000000, '2023-03-10', '2025-03-10', '2024-12-31', 1800, N'Xăng A95', N'Cho thuê', 30000000, 1, 0, N'Sẵn sàng'),
('XE004', '59D4-45678', 'LX006', 'NCC001', '2023-04-20', 40000000, '2023-04-25', '2025-04-25', '2024-12-31', 1200, N'Xăng A95', N'Cho thuê', 40000000, 1, 0, N'Sẵn sàng'),
('XE005', '59E5-56789', 'LX010', 'NCC002', '2023-05-15', 45000000, '2023-05-20', '2025-05-20', '2024-12-31', 1000, N'Xăng A95', N'Cho thuê', 45000000, 1, 0, N'Sẵn sàng'),
-- Xe bán
('XE006', NULL, 'LX003', 'NCC001', '2024-01-10', 27000000, NULL, NULL, NULL, 0, N'Xăng A95', N'Bán', 27000000, 5, 0, N'Sẵn sàng'),
('XE007', NULL, 'LX005', 'NCC001', '2024-01-15', 32000000, NULL, NULL, NULL, 0, N'Xăng A95', N'Bán', 32000000, 3, 0, N'Sẵn sàng'),
('XE008', NULL, 'LX007', 'NCC001', '2024-02-01', 42000000, NULL, NULL, NULL, 0, N'Xăng A95', N'Bán', 42000000, 4, 0, N'Sẵn sàng'),
('XE009', NULL, 'LX009', 'NCC001', '2024-02-10', 85000000, NULL, NULL, NULL, 0, N'Xăng A95', N'Bán', 85000000, 2, 0, N'Sẵn sàng'),
('XE010', NULL, 'LX011', 'NCC002', '2024-02-15', 48000000, NULL, NULL, NULL, 0, N'Xăng A95', N'Bán', 48000000, 3, 0, N'Sẵn sàng'),
('XE011', NULL, 'LX013', 'NCC002', '2024-03-01', 28000000, NULL, NULL, NULL, 0, N'Xăng A95', N'Bán', 28000000, 4, 0, N'Sẵn sàng'),
('XE012', NULL, 'LX015', 'NCC002', '2024-03-10', 38000000, NULL, NULL, NULL, 0, N'Xăng A95', N'Bán', 38000000, 3, 0, N'Sẵn sàng'),
('XE013', NULL, 'LX018', 'NCC004', '2024-03-15', 22000000, NULL, NULL, NULL, 0, N'Xăng A95', N'Bán', 22000000, 5, 0, N'Sẵn sàng'),
('XE014', NULL, 'LX019', 'NCC004', '2024-03-20', 20000000, NULL, NULL, NULL, 0, N'Xăng A95', N'Bán', 20000000, 4, 0, N'Sẵn sàng'),
('XE015', NULL, 'LX020', 'NCC005', '2024-04-01', 55000000, NULL, NULL, NULL, 0, N'Xăng A95', N'Bán', 55000000, 2, 0, N'Sẵn sàng');

/* ============================================================
   10. THÔNG TIN GIÁ XE
   ============================================================ */
INSERT INTO ThongTinGiaXe (ID_Xe, PhanLoai, GiaBan, GiaThueNgay, TienDatCoc, NgayCapNhat, GhiChu)
VALUES 
-- Xe cho thuê
('XE001', N'Thuê', NULL, 150000, 3000000, '2024-01-01', N'Giá thuê Wave'),
('XE002', N'Thuê', NULL, 150000, 3000000, '2024-01-01', N'Giá thuê Wave'),
('XE003', N'Thuê', NULL, 200000, 5000000, '2024-01-01', N'Giá thuê Vision'),
('XE004', N'Thuê', NULL, 250000, 8000000, '2024-01-01', N'Giá thuê Air Blade'),
('XE005', N'Thuê', NULL, 300000, 10000000, '2024-01-01', N'Giá thuê Exciter'),
-- Xe bán
('XE006', N'Bán', 30000000, NULL, NULL, '2024-01-10', N'Giá bán Wave 2024'),
('XE007', N'Bán', 35000000, NULL, NULL, '2024-01-15', N'Giá bán Vision 2024'),
('XE008', N'Bán', 46000000, NULL, NULL, '2024-02-01', N'Giá bán Air Blade 2024'),
('XE009', N'Bán', 95000000, NULL, NULL, '2024-02-10', N'Giá bán SH 2024'),
('XE010', N'Bán', 52000000, NULL, NULL, '2024-02-15', N'Giá bán Exciter 2024'),
('XE011', N'Bán', 31000000, NULL, NULL, '2024-03-01', N'Giá bán Janus 2023'),
('XE012', N'Bán', 42000000, NULL, NULL, '2024-03-10', N'Giá bán FreeGo 2024'),
('XE013', N'Bán', 25000000, NULL, NULL, '2024-03-15', N'Giá bán Attila 2024'),
('XE014', N'Bán', 23000000, NULL, NULL, '2024-03-20', N'Giá bán Elizabeth 2024'),
('XE015', N'Bán', 62000000, NULL, NULL, '2024-04-01', N'Giá bán Liberty 2024');

/* ============================================================
   11. PHỤ TÙNG
   ============================================================ */
INSERT INTO PhuTung (MaPhuTung, TenPhuTung, MaHangXe, MaDongXe, GiaMua, GiaBan, DonViTinh, GhiChu)
VALUES 
('PT001', N'Lốp trước Michelin', NULL, NULL, 250000, 350000, N'Cái', N'Lốp phổ thông'),
('PT002', N'Lốp sau Michelin', NULL, NULL, 280000, 400000, N'Cái', N'Lốp phổ thông'),
('PT003', N'Má phanh trước', 'HX001', NULL, 80000, 120000, N'Bộ', N'Honda Wave/Vision'),
('PT004', N'Má phanh sau', 'HX001', NULL, 70000, 100000, N'Bộ', N'Honda Wave/Vision'),
('PT005', N'Nhớt Castrol 10W40', NULL, NULL, 90000, 130000, N'Lít', N'Nhớt tổng hợp'),
('PT006', N'Bugi NGK', NULL, NULL, 35000, 50000, N'Cái', N'Bugi chuẩn'),
('PT007', N'Dây curoa Honda', 'HX001', 'DX002', 150000, 220000, N'Cái', N'Vision'),
('PT008', N'Đèn pha LED', NULL, NULL, 280000, 400000, N'Cái', N'Đèn LED tiết kiệm'),
('PT009', N'Gương chiếu hậu', NULL, NULL, 80000, 120000, N'Cái', N'Gương chuẩn'),
('PT010', N'Yên xe Vision', 'HX001', 'DX002', 450000, 650000, N'Cái', N'Yên chính hãng'),
('PT011', N'Lốp Dunlop 80/90-14', NULL, NULL, 300000, 420000, N'Cái', N'Lốp cao cấp'),
('PT012', N'Ắc quy GS 12V-5Ah', NULL, NULL, 280000, 400000, N'Cái', N'Ắc quy khô'),
('PT013', N'Lọc gió', NULL, NULL, 45000, 70000, N'Cái', N'Lọc gió thường'),
('PT014', N'Dây thắng trước', NULL, NULL, 65000, 95000, N'Cái', N'Dây thắng'),
('PT015', N'Xích nhông Exciter', 'HX002', 'DX005', 850000, 1200000, N'Bộ', N'Xích nhông chính hãng');

/* ============================================================
   12. KHO PHỤ TÙNG
   ============================================================ */
INSERT INTO KhoPhuTung (MaPhuTung, SoLuongTon, NgayCapNhat, GhiChu)
VALUES 
('PT001', 50, '2024-01-01', N'Tồn kho đủ'),
('PT002', 45, '2024-01-01', N'Tồn kho đủ'),
('PT003', 30, '2024-01-01', N'Tồn kho đủ'),
('PT004', 28, '2024-01-01', N'Tồn kho đủ'),
('PT005', 100, '2024-01-01', N'Tồn kho nhiều'),
('PT006', 80, '2024-01-01', N'Tồn kho nhiều'),
('PT007', 25, '2024-01-01', N'Tồn kho đủ'),
('PT008', 35, '2024-01-01', N'Tồn kho đủ'),
('PT009', 60, '2024-01-01', N'Tồn kho nhiều'),
('PT010', 20, '2024-01-01', N'Tồn kho ít'),
('PT011', 40, '2024-01-01', N'Tồn kho đủ'),
('PT012', 30, '2024-01-01', N'Tồn kho đủ'),
('PT013', 70, '2024-01-01', N'Tồn kho nhiều'),
('PT014', 55, '2024-01-01', N'Tồn kho đủ'),
('PT015', 15, '2024-01-01', N'Tồn kho ít');

/* ============================================================
   13. BẢO TRÌ XE
   ============================================================ */
INSERT INTO BaoTriXe (ID_Xe, MaTaiKhoan, GhiChuBaoTri)
VALUES 
('XE001', 'TK003', N'Bảo dưỡng định kỳ 1500km - Thay nhớt, kiểm tra phanh'),
('XE002', 'TK003', N'Thay lốp sau do mòn'),
('XE003', 'TK007', N'Bảo dưỡng định kỳ - Thay dây curoa'),
('XE004', 'TK003', N'Sửa chữa đèn pha bị hỏng'),
('XE005', 'TK007', N'Bảo dưỡng định kỳ 1000km - Thay nhớt, điều chỉnh xích');

/* ============================================================
   14. CHI TIẾT BẢO TRÌ
   ============================================================ */
INSERT INTO ChiTietBaoTri (ID_BaoTri, MaPhuTung, SoLuong, GiaSuDung, GhiChu)
VALUES 
-- Bảo trì 1: XE001
(1, 'PT005', 1, 130000, N'Nhớt Castrol'),
(1, 'PT003', 1, 120000, N'Má phanh trước'),
-- Bảo trì 2: XE002
(2, 'PT002', 1, 400000, N'Lốp sau Michelin'),
-- Bảo trì 3: XE003
(3, 'PT007', 1, 220000, N'Dây curoa Vision'),
(3, 'PT005', 1, 130000, N'Nhớt Castrol'),
-- Bảo trì 4: XE004
(4, 'PT008', 1, 400000, N'Đèn pha LED'),
-- Bảo trì 5: XE005
(5, 'PT005', 1, 130000, N'Nhớt Castrol'),
(5, 'PT015', 1, 1200000, N'Xích nhông Exciter');

/* ============================================================
   15. GIAO DỊCH BÁN
   ============================================================ */
INSERT INTO GiaoDichBan (MaKH, ID_Xe, NgayBan, GiaBan, TrangThaiThanhToan, HinhThucThanhToan, MaTaiKhoan)
VALUES 
('KH001', 'XE006', '2024-03-15', 30000000, N'Đã thanh toán', N'Chuyển khoản', 'TK002'),
('KH003', 'XE007', '2024-03-20', 35000000, N'Đã thanh toán', N'Tiền mặt', 'TK006'),
('KH005', 'XE008', '2024-04-05', 46000000, N'Đã thanh toán', N'Chuyển khoản', 'TK002'),
('KH007', 'XE011', '2024-04-10', 31000000, N'Đã thanh toán', N'Tiền mặt', 'TK006'),
('KH009', 'XE013', '2024-04-15', 25000000, N'Đã thanh toán', N'Chuyển khoản', 'TK002');

/* ============================================================
   16. GIAO DỊCH THUÊ
   ============================================================ */
INSERT INTO GiaoDichThue (ID_Xe, MaKH, NgayBatDau, NgayKetThuc, GiaThueNgay, TongGia, TrangThai, TrangThaiThanhToan, HinhThucThanhToan, SoTienCoc, GiayToGiuLai, MaTaiKhoan, TrangThaiDuyet, NguoiDuyet, NgayDuyet, NgayGiaoXeThucTe, KmBatDau)
VALUES 
-- Đã hoàn thành
('XE001', 'KH002', '2024-03-01', '2024-03-08', 150000, 1050000, N'Đã thuê', N'Đã thanh toán', N'Tiền mặt', 3000000, N'CCCD', 'TK002', N'Đã duyệt', 'TK001', '2024-02-28 10:00:00', '2024-03-01 08:00:00', 1500),
('XE002', 'KH004', '2024-03-10', '2024-03-15', 150000, 750000, N'Đã thuê', N'Đã thanh toán', N'Chuyển khoản', 3000000, N'CCCD', 'TK006', N'Đã duyệt', 'TK001', '2024-03-09 14:00:00', '2024-03-10 09:00:00', 2000),
('XE003', 'KH006', '2024-03-15', '2024-03-22', 200000, 1400000, N'Đã thuê', N'Đã thanh toán', N'Tiền mặt', 5000000, N'CCCD', 'TK002', N'Đã duyệt', 'TK001', '2024-03-14 11:00:00', '2024-03-15 08:30:00', 1800),
-- Đang thuê
('XE004', 'KH008', '2024-04-20', '2024-04-30', 250000, 2500000, N'Đang thuê', N'Đã thanh toán', N'Chuyển khoản', 8000000, N'CCCD', 'TK006', N'Đã duyệt', 'TK001', '2024-04-19 15:00:00', '2024-04-20 08:00:00', 1200),
('XE005', 'KH010', '2024-04-22', '2024-05-02', 300000, 3000000, N'Đang thuê', N'Đã thanh toán', N'Tiền mặt', 10000000, N'CCCD', 'TK002', N'Đã duyệt', 'TK001', '2024-04-21 10:00:00', '2024-04-22 09:00:00', 1000),
-- Chờ giao xe
('XE001', 'KH002', '2024-04-25', '2024-04-28', 150000, 450000, N'Chờ giao xe', N'Đã thanh toán', N'Chuyển khoản', 3000000, N'CCCD', 'TK006', N'Đã duyệt', 'TK001', '2024-04-24 09:00:00', NULL, NULL),
-- Chờ duyệt
('XE002', 'KH004', '2024-04-26', '2024-04-30', 150000, 600000, N'Chờ xác nhận', N'Chưa thanh toán', N'Tiền mặt', 3000000, N'CCCD', 'TK002', N'Chờ duyệt', NULL, NULL, NULL, NULL);

/* ============================================================
   17. GIAO DỊCH TRẢ THUÊ
   ============================================================ */
INSERT INTO GiaoDichTraThue (MaGDThue, NgayTraXe, TinhTrangXe, ChiPhiPhatSinh, GhiChu, MaTaiKhoan, KmKetThuc, SoNgayTraSom, TienHoanTraSom)
VALUES 
(1, '2024-03-08', N'Bình thường, không hư hỏng', 0, N'Trả xe đúng hạn', 'TK004', 2300, 0, 0),
(2, '2024-03-15', N'Có trầy xước nhẹ ở thân xe', 200000, N'Phí sửa chữa trầy xước', 'TK005', 2850, 0, 0),
(3, '2024-03-20', N'Bình thường', 0, N'Trả sớm 2 ngày', 'TK004', 2450, 2, 400000);

/* ============================================================
   18. HỢP ĐỒNG THUÊ
   ============================================================ */
INSERT INTO HopDongThue (MaGDThue, MaKH, MaTaiKhoan, ID_Xe, NgayLap, NgayBatDau, NgayKetThuc, GiaThue, TienDatCoc, DieuKhoan, GhiChu, TrangThaiHopDong, FileHopDong)
VALUES 
(1, 'KH002', 'TK001', 'XE001', '2024-02-28', '2024-03-01', '2024-03-08', 1050000, 3000000, N'Khách hàng chịu trách nhiệm bảo quản xe. Trả phí nếu có hư hỏng.', N'Hợp đồng chuẩn', N'Hết hạn', 'HD_THUE_001.pdf'),
(2, 'KH004', 'TK001', 'XE002', '2024-03-09', '2024-03-10', '2024-03-15', 750000, 3000000, N'Khách hàng chịu trách nhiệm bảo quản xe. Trả phí nếu có hư hỏng.', N'Hợp đồng chuẩn', N'Hết hạn', 'HD_THUE_002.pdf'),
(3, 'KH006', 'TK001', 'XE003', '2024-03-14', '2024-03-15', '2024-03-22', 1400000, 5000000, N'Khách hàng chịu trách nhiệm bảo quản xe. Trả phí nếu có hư hỏng.', N'Hợp đồng chuẩn', N'Hết hạn', 'HD_THUE_003.pdf'),
(4, 'KH008', 'TK001', 'XE004', '2024-04-19', '2024-04-20', '2024-04-30', 2500000, 8000000, N'Khách hàng chịu trách nhiệm bảo quản xe. Trả phí nếu có hư hỏng.', N'Hợp đồng chuẩn', N'Đang hiệu lực', 'HD_THUE_004.pdf'),
(5, 'KH010', 'TK001', 'XE005', '2024-04-21', '2024-04-22', '2024-05-02', 3000000, 10000000, N'Khách hàng chịu trách nhiệm bảo quản xe. Trả phí nếu có hư hỏng.', N'Hợp đồng chuẩn', N'Đang hiệu lực', 'HD_THUE_005.pdf'),
(6, 'KH002', 'TK001', 'XE001', '2024-04-24', '2024-04-25', '2024-04-28', 450000, 3000000, N'Khách hàng chịu trách nhiệm bảo quản xe. Trả phí nếu có hư hỏng.', N'Hợp đồng chuẩn', N'Đang hiệu lực', 'HD_THUE_006.pdf');

/* ============================================================
   19. THANH TOÁN
   ============================================================ */
INSERT INTO ThanhToan (MaGDBan, MaGDThue, SoTien, NgayThanhToan, PhuongThuc, GhiChu)
VALUES 
-- Thanh toán bán
(1, NULL, 30000000, '2024-03-15', N'Chuyển khoản', N'Thanh toán đầy đủ'),
(2, NULL, 35000000, '2024-03-20', N'Tiền mặt', N'Thanh toán đầy đủ'),
(3, NULL, 46000000, '2024-04-05', N'Chuyển khoản', N'Thanh toán đầy đủ'),
(4, NULL, 31000000, '2024-04-10', N'Tiền mặt', N'Thanh toán đầy đủ'),
(5, NULL, 25000000, '2024-04-15', N'Chuyển khoản', N'Thanh toán đầy đủ'),
-- Thanh toán thuê
(NULL, 1, 1050000, '2024-03-01', N'Tiền mặt', N'Thanh toán tiền thuê'),
(NULL, 2, 750000, '2024-03-10', N'Chuyển khoản', N'Thanh toán tiền thuê'),
(NULL, 3, 1400000, '2024-03-15', N'Tiền mặt', N'Thanh toán tiền thuê'),
(NULL, 4, 2500000, '2024-04-20', N'Chuyển khoản', N'Thanh toán tiền thuê'),
(NULL, 5, 3000000, '2024-04-22', N'Tiền mặt', N'Thanh toán tiền thuê'),
(NULL, 6, 450000, '2024-04-25', N'Chuyển khoản', N'Thanh toán tiền thuê');

/* ============================================================
   20. KHUYẾN MÃI
   ============================================================ */
INSERT INTO KhuyenMai (MaKM, TenKM, MoTa, NgayBatDau, NgayKetThuc, PhanTramGiam, DieuKienApDung, LoaiApDung, GhiChu)
VALUES 
('KM001', N'Khuyến mãi tết 2024', N'Giảm giá cho khách hàng mua xe dịp tết', '2024-01-20', '2024-02-15', 5.00, N'Áp dụng cho tất cả xe bán', 'XeBan', N'Chương trình tết'),
('KM002', N'Ưu đãi sinh nhật công ty', N'Giảm giá thuê xe nhân dịp sinh nhật 5 năm', '2024-03-01', '2024-03-31', 10.00, N'Áp dụng thuê từ 5 ngày trở lên', 'XeThue', N'Chương trình sinh nhật'),
('KM003', N'Mua phụ tùng giảm giá', N'Giảm 15% cho phụ tùng chính hãng', '2024-04-01', '2024-04-30', 15.00, N'Áp dụng cho phụ tùng Honda, Yamaha', 'PhuTung', N'Khuyến mãi tháng 4'),
('KM004', N'Ưu đãi mua SH', N'Giảm giá đặc biệt cho dòng SH', '2024-02-01', '2024-02-29', 3.00, N'Chỉ áp dụng cho SH 2024', 'XeBan', N'Khuyến mãi SH'),
('KM005', N'Thuê xe mùa du lịch', N'Giảm giá thuê xe dịp du lịch hè', '2024-05-01', '2024-06-30', 8.00, N'Thuê từ 7 ngày', 'XeThue', N'Chương trình hè');

/* ============================================================
   21. HÓA ĐƠN
   ============================================================ */
INSERT INTO HoaDon (MaKH, MaTaiKhoan, NgayLap, TongTien, MaKM, SoTienGiam, ThanhTien, HinhThucThanhToan, GhiChu)
VALUES 
-- Hóa đơn bán xe (có KM tết)
('KH001', 'TK002', '2024-03-15', 30000000, 'KM001', 1500000, 28500000, N'Chuyển khoản', N'Áp dụng KM tết'),
('KH003', 'TK006', '2024-03-20', 35000000, NULL, 0, 35000000, N'Tiền mặt', N'Không có KM'),
('KH005', 'TK002', '2024-04-05', 46000000, NULL, 0, 46000000, N'Chuyển khoản', N'Không có KM'),
('KH007', 'TK006', '2024-04-10', 31000000, NULL, 0, 31000000, N'Tiền mặt', N'Không có KM'),
('KH009', 'TK002', '2024-04-15', 25000000, NULL, 0, 25000000, N'Chuyển khoản', N'Không có KM'),
-- Hóa đơn thuê xe (có KM sinh nhật)
('KH002', 'TK002', '2024-03-01', 1050000, 'KM002', 105000, 945000, N'Tiền mặt', N'Áp dụng KM sinh nhật'),
('KH004', 'TK006', '2024-03-10', 750000, NULL, 0, 750000, N'Chuyển khoản', N'Không có KM'),
('KH006', 'TK002', '2024-03-15', 1400000, 'KM002', 140000, 1260000, N'Tiền mặt', N'Áp dụng KM sinh nhật'),
('KH008', 'TK006', '2024-04-20', 2500000, NULL, 0, 2500000, N'Chuyển khoản', N'Không có KM'),
('KH010', 'TK002', '2024-04-22', 3000000, NULL, 0, 3000000, N'Tiền mặt', N'Không có KM');

/* ============================================================
   22. CHI TIẾT HÓA ĐƠN
   ============================================================ */
INSERT INTO ChiTietHoaDon (MaHD, MaGDBan, MaGDThue, SoTien, GhiChu)
VALUES 
-- Chi tiết hóa đơn bán
(1, 1, NULL, 28500000, N'Bán xe Wave 2024'),
(2, 2, NULL, 35000000, N'Bán xe Vision 2024'),
(3, 3, NULL, 46000000, N'Bán xe Air Blade 2024'),
(4, 4, NULL, 31000000, N'Bán xe Janus 2023'),
(5, 5, NULL, 25000000, N'Bán xe Attila 2024'),
-- Chi tiết hóa đơn thuê
(6, NULL, 1, 945000, N'Thuê xe Wave 7 ngày'),
(7, NULL, 2, 750000, N'Thuê xe Wave 5 ngày'),
(8, NULL, 3, 1260000, N'Thuê xe Vision 7 ngày'),
(9, NULL, 4, 2500000, N'Thuê xe Air Blade 10 ngày'),
(10, NULL, 5, 3000000, N'Thuê xe Exciter 10 ngày');

/* ============================================================
   23. LỊCH SỬ GIÁ
   ============================================================ */
INSERT INTO LichSuGia (ID_Xe, PhanLoai, GiaCu, GiaMoi, NgayThayDoi, LyDoThayDoi, MaTaiKhoan, GhiChu)
VALUES 
('XE006', 'Bán', 28000000, 30000000, '2024-02-01', N'Điều chỉnh giá theo thị trường', 'TK001', N'Tăng giá do nhu cầu cao'),
('XE007', 'Bán', 33000000, 35000000, '2024-02-15', N'Điều chỉnh giá theo thị trường', 'TK001', N'Tăng giá do nhu cầu cao'),
('XE001', 'Thuê', 120000, 150000, '2024-01-15', N'Tăng giá thuê dịp tết', 'TK001', N'Tăng giá theo mùa'),
('XE004', 'Thuê', 220000, 250000, '2024-03-01', N'Điều chỉnh giá thuê', 'TK001', N'Tăng giá do xe mới bảo trì'),
('XE009', 'Bán', 90000000, 95000000, '2024-03-10', N'Tăng giá do khan hàng', 'TK001', N'SH khan hàng');

/* ============================================================
   24. CẬP NHẬT TRẠNG THÁI XE SAU GIAO DỊCH
   ============================================================ */
-- Cập nhật xe đã bán
UPDATE XeMay SET TrangThai = N'Đã bán', SoLuongBanRa = 1 WHERE ID_Xe = 'XE006';
UPDATE XeMay SET TrangThai = N'Đã bán', SoLuongBanRa = 1 WHERE ID_Xe = 'XE007';
UPDATE XeMay SET TrangThai = N'Đã bán', SoLuongBanRa = 1 WHERE ID_Xe = 'XE008';
UPDATE XeMay SET TrangThai = N'Đã bán', SoLuongBanRa = 1 WHERE ID_Xe = 'XE011';
UPDATE XeMay SET TrangThai = N'Đã bán', SoLuongBanRa = 1 WHERE ID_Xe = 'XE013';

-- Cập nhật xe đang thuê
UPDATE XeMay SET TrangThai = N'Đang thuê' WHERE ID_Xe IN ('XE004', 'XE005');

-- Cập nhật kho phụ tùng sau bảo trì
UPDATE KhoPhuTung SET SoLuongTon = SoLuongTon - 2 WHERE MaPhuTung = 'PT005'; -- Dùng 2 lít nhớt
UPDATE KhoPhuTung SET SoLuongTon = SoLuongTon - 1 WHERE MaPhuTung = 'PT003'; -- Dùng 1 má phanh
UPDATE KhoPhuTung SET SoLuongTon = SoLuongTon - 1 WHERE MaPhuTung = 'PT002'; -- Dùng 1 lốp
UPDATE KhoPhuTung SET SoLuongTon = SoLuongTon - 1 WHERE MaPhuTung = 'PT007'; -- Dùng 1 dây curoa
UPDATE KhoPhuTung SET SoLuongTon = SoLuongTon - 1 WHERE MaPhuTung = 'PT008'; -- Dùng 1 đèn pha
UPDATE KhoPhuTung SET SoLuongTon = SoLuongTon - 1 WHERE MaPhuTung = 'PT015'; -- Dùng 1 xích nhông

/* ============================================================
   25. KIỂM TRA DỮ LIỆU
   ============================================================ */
PRINT N'=== TỔNG HỢP DỮ LIỆU ===';
PRINT N'Số khách hàng: ' + CAST((SELECT COUNT(*) FROM KhachHang) AS VARCHAR);
PRINT N'Số nhân viên: ' + CAST((SELECT COUNT(*) FROM NhanVien) AS VARCHAR);
PRINT N'Số tài khoản: ' + CAST((SELECT COUNT(*) FROM TaiKhoan) AS VARCHAR);
PRINT N'Số nhà cung cấp: ' + CAST((SELECT COUNT(*) FROM NhaCungCap) AS VARCHAR);
PRINT N'Số hãng xe: ' + CAST((SELECT COUNT(*) FROM HangXe) AS VARCHAR);
PRINT N'Số dòng xe: ' + CAST((SELECT COUNT(*) FROM DongXe) AS VARCHAR);
PRINT N'Số loại xe: ' + CAST((SELECT COUNT(*) FROM LoaiXe) AS VARCHAR);
PRINT N'Số xe máy: ' + CAST((SELECT COUNT(*) FROM XeMay) AS VARCHAR);
PRINT N'Số phụ tùng: ' + CAST((SELECT COUNT(*) FROM PhuTung) AS VARCHAR);
PRINT N'Số giao dịch bán: ' + CAST((SELECT COUNT(*) FROM GiaoDichBan) AS VARCHAR);
PRINT N'Số giao dịch thuê: ' + CAST((SELECT COUNT(*) FROM GiaoDichThue) AS VARCHAR);
PRINT N'Số hóa đơn: ' + CAST((SELECT COUNT(*) FROM HoaDon) AS VARCHAR);
PRINT N'Số khuyến mãi: ' + CAST((SELECT COUNT(*) FROM KhuyenMai) AS VARCHAR);
PRINT N'';
PRINT N'=== HOÀN TẤT THÊM DỮ LIỆU ===';
GO

USE QuanLyCHXeMay;
GO

-- ============================================================
-- XÓA DỮ LIỆU THEO THỨ TỰ (Bảng con -> Bảng cha)
-- ============================================================

-- 1. Xóa chi tiết trước
DELETE FROM ChiTietBaoTri;
DELETE FROM ChiTietHoaDon;

-- 2. Xóa các bảng giao dịch phụ thuộc
DELETE FROM HopDongThue;
DELETE FROM HopDongMua;
DELETE FROM GiaoDichTraThue;
DELETE FROM ThanhToan;

-- 3. Xóa hóa đơn
DELETE FROM HoaDon;

-- 4. Xóa giao dịch chính
DELETE FROM GiaoDichThue;
DELETE FROM GiaoDichBan;

-- 5. Xóa bảo trì
DELETE FROM BaoTriXe;

-- 6. Xóa kho và phụ tùng
DELETE FROM KhoPhuTung;
DELETE FROM PhuTung;

-- 7. Xóa thông tin xe
DELETE FROM LichSuGia;
DELETE FROM ThongTinGiaXe;
DELETE FROM XeMay;

-- 8. Xóa loại xe và các bảng liên quan
DELETE FROM LoaiXe;
DELETE FROM MauSac;
DELETE FROM DongXe;
DELETE FROM HangXe;

-- 9. Xóa nhà cung cấp
DELETE FROM NhaCungCap;

-- 10. Xóa khuyến mãi
DELETE FROM KhuyenMai;

-- 11. Xóa tài khoản
DELETE FROM TaiKhoan;

-- 12. Xóa người dùng (cuối cùng)
DELETE FROM NhanVien;
DELETE FROM KhachHang;

GO

PRINT N'✅ Đã xóa toàn bộ dữ liệu thành công!';
GO
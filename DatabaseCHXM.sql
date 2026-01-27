/* ============================================================
   1. QUẢN LÝ NGƯỜI DÙNG & PHÂN QUYỀN
   ============================================================ */

-- Bảng khách hàng 
CREATE TABLE KhachHang (
    MaKH CHAR(10) PRIMARY KEY, -- Mã khách hàng
    HoTenKH NVARCHAR(100) NOT NULL, -- Họ tên khách hàng
    NgaySinh DATE, -- Ngày sinh
    GioiTinh NVARCHAR(10), -- Giới tính
    Sdt VARCHAR(15), -- Số điện thoại
    Email VARCHAR(100), -- Địa chỉ email
    DiaChi NVARCHAR(255), -- Địa chỉ
    NgayTao DATETIME DEFAULT GETDATE(), -- Ngày tạo
    NgayCapNhat DATETIME DEFAULT GETDATE(), -- Ngày cập nhật
    SoCCCD VARCHAR(20) NULL UNIQUE, -- Số CCCD
    LoaiGiayTo NVARCHAR(50) NULL, -- Giấy tờ
    AnhGiayTo VARBINARY(MAX) NULL -- Ảnh giấy tờ
);

-- Bảng nhân viên 
CREATE TABLE NhanVien (
    MaNV CHAR(10) PRIMARY KEY, -- Mã nhân viên
    HoTenNV NVARCHAR(100) NOT NULL, -- Họ tên nhân viên
    NgaySinh DATE NOT NULL, -- Ngày sinh
    GioiTinh NVARCHAR(10), -- Giới tính
    Sdt VARCHAR(15) UNIQUE, -- Số điện thoại
    Email VARCHAR(100) UNIQUE, -- Địa chỉ email
    DiaChi NVARCHAR(255), -- Địa chỉ
    ChucVu NVARCHAR(50), -- Chức vụ (Quản lý, Thu ngân, Kỹ thuật, Bán hàng)
    LuongCoBan DECIMAL(15,2), -- Lương cơ bản
    TinhTrangLamViec NVARCHAR(20), -- Còn làm, nghỉ làm, thử việc
    CCCD VARCHAR(20) UNIQUE, -- Căn cước công dân
    TrinhDoHocVan NVARCHAR(100), -- Trình độ học vấn
    AnhNhanVien VARBINARY(MAX), -- URL ảnh nhân viên (lưu trên cloud)
    NgayTao DATETIME DEFAULT GETDATE(), -- Ngày tạo
    NgayCapNhat DATETIME DEFAULT GETDATE(), -- Ngày cập nhật
    NgayVaoLam DATETIME NULL DEFAULT GETDATE() -- Ngày vào làm
);

-- Bảng tài khoản
CREATE TABLE TaiKhoan (
    MaTaiKhoan CHAR(10) PRIMARY KEY,
    TenDangNhap VARCHAR(50) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL,
    LoaiTaiKhoan NVARCHAR(20) NOT NULL DEFAULT 'NhanVien', -- 'NhanVien' hoặc 'KhachHang'
    TrangThaiTaiKhoan NVARCHAR(20) DEFAULT 'Hoạt động',
    MaNV CHAR(10) NULL, -- Tài khoản nhân viên
    MaKH CHAR(10) NULL, -- Tài khoản khách hàng
    NgayTao DATETIME DEFAULT GETDATE(),
    NgayCapNhat DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
);

/* ============================================================
   2. QUẢN LÝ XE MÁY
   ============================================================ */

-- Bảng nhà cung cấp
CREATE TABLE NhaCungCap (
    MaNCC CHAR(10) PRIMARY KEY,
    TenNCC NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(255) NULL,
    Sdt VARCHAR(15) NULL,
    Email VARCHAR(100) NULL,
    GhiChu NVARCHAR(255) NULL
);

-- Bảng hãng xe
CREATE TABLE HangXe (
    MaHang CHAR(10) PRIMARY KEY,
    TenHang NVARCHAR(50) NOT NULL,
    MoTa NVARCHAR(255) NULL,
    Logo NVARCHAR(500) NULL
);

-- Bảng dòng xe
CREATE TABLE DongXe (
    MaDong CHAR(10) PRIMARY KEY,
    TenDong NVARCHAR(50) NOT NULL,
    MaHang CHAR(10) NOT NULL,
    PhanKhoi INT NULL,
    LoaiXe NVARCHAR(30) NULL,
    MoTa NVARCHAR(255) NULL,
    FOREIGN KEY (MaHang) REFERENCES HangXe(MaHang)
);

-- Bảng màu sắc
CREATE TABLE MauSac (
    MaMau CHAR(10) PRIMARY KEY,
    TenMau NVARCHAR(30) NOT NULL,
    GhiChu NVARCHAR(255) NULL
);

-- Bảng loại xe
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

-- Bảng xe máy
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

-- Bảng thông tin giá xe
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
    MaPhuTung CHAR(10) PRIMARY KEY, -- Mã phụ tùng
    TenPhuTung NVARCHAR(100) NOT NULL, -- Tên phụ tùng (ví dụ: Lốp xe, Phanh)
    MaHangXe CHAR(10), -- Hãng xe tương thích (nếu cần)
    MaDongXe CHAR(10), -- Dòng xe tương thích (nếu cần)
    GiaMua DECIMAL(15,2), -- Giá mua
    GiaBan DECIMAL(15,2), -- Giá bán
    DonViTinh NVARCHAR(20), -- Đơn vị (cái, bộ,...)
    GhiChu NVARCHAR(255), -- Ghi chú
    FOREIGN KEY (MaHangXe) REFERENCES HangXe(MaHang),
    FOREIGN KEY (MaDongXe) REFERENCES DongXe(MaDong)
);

-- Bảng kho phụ tùng
CREATE TABLE KhoPhuTung (
    ID_KhoPhuTung INT IDENTITY PRIMARY KEY, -- Mã kho phụ tùng
    MaPhuTung CHAR(10) NOT NULL, -- Mã phụ tùng
    SoLuongTon INT DEFAULT 0, -- Số lượng tồn kho
    NgayCapNhat DATE DEFAULT GETDATE(), -- Ngày cập nhật
    GhiChu NVARCHAR(255), -- Ghi chú
    UNIQUE (MaPhuTung), -- Tránh trùng lặp
    FOREIGN KEY (MaPhuTung) REFERENCES PhuTung(MaPhuTung)
);

-- Bảng ghi chú bảo trì xe
CREATE TABLE BaoTriXe (
    ID_BaoTri INT IDENTITY PRIMARY KEY, -- Mã bảo trì
    ID_Xe CHAR(10) NOT NULL, -- Mã xe
    MaTaiKhoan CHAR(10), -- Nhân viên kỹ thuật
    GhiChuBaoTri NVARCHAR(255), -- Ghi chú bảo trì
    FOREIGN KEY (ID_Xe) REFERENCES XeMay(ID_Xe),
    FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan)
);

-- Bảng chi tiết bảo trì
CREATE TABLE ChiTietBaoTri (
    ID_ChiTiet INT IDENTITY PRIMARY KEY,-- Mã chi tiết
    ID_BaoTri INT NOT NULL, -- Mã bảo trì
    MaPhuTung CHAR(10) NOT NULL, -- Mã phụ tùng
    SoLuong INT NOT NULL, -- Số lượng sử dụng
    GiaSuDung DECIMAL(15,2), -- Giá sử dụng (SoLuong * GiaBan)
    GhiChu NVARCHAR(255), -- Ghi chú
    FOREIGN KEY (ID_BaoTri) REFERENCES BaoTriXe(ID_BaoTri),
    FOREIGN KEY (MaPhuTung) REFERENCES PhuTung(MaPhuTung)
);

/* ============================================================
   4. QUẢN LÝ GIAO DỊCH
   ============================================================ */

-- Bảng khuyến mãi 
CREATE TABLE KhuyenMai (
    MaKM CHAR(10) PRIMARY KEY,
    TenKM NVARCHAR(100) NOT NULL, -- Tên chương trình
    MoTa NVARCHAR(500), -- Mô tả chi tiết
    NgayBatDau DATETIME NOT NULL, -- Ngày bắt đầu
    NgayKetThuc DATETIME NOT NULL, -- Ngày kết thúc
    LoaiKhuyenMai NVARCHAR(20) NOT NULL
        CHECK (LoaiKhuyenMai IN (N'Giảm %', N'Giảm tiền')),
    PhanTramGiam DECIMAL(5,2) NULL, -- % giảm (0-100)
    SoTienGiam DECIMAL(15,2) NULL, -- Số tiền giảm cố định
    GiaTriGiamToiDa DECIMAL(15,2) NULL, -- Giới hạn giảm tối đa (cho giảm %)
    LoaiApDung NVARCHAR(20) DEFAULT N'Tất cả'
        CHECK (LoaiApDung IN (N'Tất cả', N'Xe bán', N'Xe thuê', N'Phụ tùng')),
    TrangThai NVARCHAR(20) DEFAULT N'Hoạt động'
        CHECK (TrangThai IN (N'Hoạt động', N'Tạm dừng', N'Hết hạn', N'Hủy')),
    MaTaiKhoan CHAR(10) NULL,
    NgayTao DATETIME DEFAULT GETDATE(),
    NgayCapNhat DATETIME DEFAULT GETDATE(),
    GhiChu NVARCHAR(500),
    FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan),
    CONSTRAINT CK_KhuyenMai_GiaTriGiam CHECK (
        (LoaiKhuyenMai = N'Giảm %' AND PhanTramGiam IS NOT NULL AND PhanTramGiam > 0 AND PhanTramGiam <= 100) OR
        (LoaiKhuyenMai = N'Giảm tiền' AND SoTienGiam IS NOT NULL AND SoTienGiam > 0)
    ),
    CONSTRAINT CK_KhuyenMai_NgayHopLe CHECK (NgayKetThuc >= NgayBatDau)
);

-- Bảng giao dịch bán 
CREATE TABLE GiaoDichBan (
    MaGDBan INT IDENTITY PRIMARY KEY, -- Mã giao dịch bán
    MaKH CHAR(10) NOT NULL, -- Mã khách hàng
    ID_Xe CHAR(10) NOT NULL, -- Mã xe
    NgayBan DATE NOT NULL, -- Ngày bán
    GiaBan DECIMAL(15,2) NOT NULL, -- Giá bán
    TrangThaiThanhToan NVARCHAR(20), -- Đã thanh toán / Chưa thanh toán
    HinhThucThanhToan NVARCHAR(50), -- Hình thức thanh toán
    MaTaiKhoan CHAR(10), -- Nhân viên thu ngân
    MaKM CHAR(10) NULL, -- Mã khuyến mãi (thêm từ ALTER)
    SoTienGiam DECIMAL(15,2) DEFAULT 0, -- Số tiền giảm (thêm từ ALTER)
    TongGiaPhuTung DECIMAL(15,2) DEFAULT 0, -- Tổng giá phụ tùng (thêm từ ALTER)
    TongGiamPhuTung DECIMAL(15,2) DEFAULT 0, -- Tổng giảm phụ tùng (thêm từ ALTER)
    TongThanhToan DECIMAL(15,2) DEFAULT 0, -- Tổng thanh toán (thêm từ ALTER)
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (ID_Xe) REFERENCES XeMay(ID_Xe),
    FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan),
    FOREIGN KEY (MaKM) REFERENCES KhuyenMai(MaKM) -- FK từ ALTER
);

-- Bảng chi tiết phụ tùng bán
CREATE TABLE ChiTietPhuTungBan (
    ID_ChiTiet INT IDENTITY PRIMARY KEY,
    MaGDBan INT NOT NULL,
    MaPhuTung CHAR(10) NOT NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(15,2) NOT NULL,
    ThanhTien DECIMAL(15,2) NOT NULL,
    MaKM CHAR(10) NULL,
    SoTienGiam DECIMAL(15,2) DEFAULT 0,
    FOREIGN KEY (MaGDBan) REFERENCES GiaoDichBan(MaGDBan),
    FOREIGN KEY (MaPhuTung) REFERENCES PhuTung(MaPhuTung),
    FOREIGN KEY (MaKM) REFERENCES KhuyenMai(MaKM)
);

-- Bảng giao dịch thuê 
CREATE TABLE GiaoDichThue (
    MaGDThue INT IDENTITY PRIMARY KEY, -- Mã giao dịch thuê
    ID_Xe CHAR(10) NOT NULL, -- Mã xe
    MaKH CHAR(10) NOT NULL, -- Mã khách hàng
    NgayBatDau DATE NOT NULL, -- Ngày bắt đầu thuê
    NgayKetThuc DATE NOT NULL, -- Ngày kết thúc thuê
    GiaThueNgay DECIMAL(15,2) NOT NULL, -- Giá thuê 1 ngày
    TongGia DECIMAL(15,2) NOT NULL, -- Tổng giá
    TrangThai NVARCHAR(20), -- Đang thuê / Đã thuê / Hủy
    TrangThaiThanhToan NVARCHAR(20), -- Đã thanh toán / Chưa thanh toán
    HinhThucThanhToan NVARCHAR(50), -- Hình thức thanh toán
    SoTienCoc DECIMAL(15,2), -- Số tiền cọc
    GiayToGiuLai NVARCHAR(100), -- Giấy tờ giữ lại
    MaTaiKhoan CHAR(10), -- Nhân viên thu ngân
    TrangThaiDuyet NVARCHAR(20) DEFAULT N'Chờ duyệt', -- (thêm từ ALTER)
    NguoiDuyet CHAR(10) NULL, -- (thêm từ ALTER)
    NgayDuyet DATETIME NULL, -- (thêm từ ALTER)
    GhiChuDuyet NVARCHAR(255) NULL, -- (thêm từ ALTER)
    NgayGiaoXeThucTe DATETIME NULL, -- (thêm từ ALTER)
    NgayTraXeThucTe DATETIME NULL, -- (thêm từ ALTER)
    GhiChuGiaoXe NVARCHAR(255) NULL, -- (thêm từ ALTER)
    KmBatDau INT NULL, -- (thêm từ ALTER)
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (ID_Xe) REFERENCES XeMay(ID_Xe),
    FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan),
    CONSTRAINT CK_GiaoDichThue_TrangThai CHECK (TrangThai IN (N'Chờ xác nhận', N'Chờ giao xe', N'Đang thuê', N'Đã thuê', N'Hủy'))
);

-- Tạo index cho GiaoDichThue
CREATE INDEX IX_GiaoDichThue_XeNgay
    ON GiaoDichThue(ID_Xe, NgayBatDau, NgayKetThuc, TrangThai);
	

CREATE INDEX IX_GiaoDichThue_TrangThaiDuyet
    ON GiaoDichThue(TrangThaiDuyet, TrangThai);

-- Bảng giao dịch trả thuê 
CREATE TABLE GiaoDichTraThue (
    MaGDTraThue INT IDENTITY PRIMARY KEY, -- Mã giao dịch trả thuê
    MaGDThue INT NOT NULL, -- Mã giao dịch thuê
    NgayTraXe DATE NOT NULL, -- Ngày trả xe
    TinhTrangXe NVARCHAR(100), -- Tình trạng xe khi trả
    ChiPhiPhatSinh DECIMAL(15,2), -- Chi phí phát sinh
    GhiChu NVARCHAR(255), -- Ghi chú
    MaTaiKhoan CHAR(10), -- Nhân viên bán hàng
    TienHoanCoc DECIMAL(15,2), -- (thêm từ ALTER)
    TienPhat DECIMAL(15,2), -- (thêm từ ALTER)
    KmKetThuc INT NULL, -- (thêm từ ALTER)
    SoNgayTraSom INT NULL DEFAULT 0, -- (thêm từ ALTER)
    TienHoanTraSom DECIMAL(15,2) NULL DEFAULT 0, -- (thêm từ ALTER)
    FOREIGN KEY (MaGDThue) REFERENCES GiaoDichThue(MaGDThue),
    FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan)
);
-- Hop dong mua
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

-- Bảng hợp đồng thuê xe
CREATE TABLE HopDongThue (
    MaHDT INT IDENTITY PRIMARY KEY, -- Mã hợp đồng thuê
    MaGDThue INT NOT NULL, -- Mã giao dịch thuê
    MaKH CHAR(10) NOT NULL, -- Mã khách hàng
    MaTaiKhoan CHAR(10) NOT NULL, -- Mã nhân viên (Quản lý hoặc Bán hàng)
    ID_Xe CHAR(10) NOT NULL, -- Mã xe
    NgayLap DATE NOT NULL, -- Ngày lập hợp đồng
    NgayBatDau DATE NOT NULL, -- Ngày bắt đầu thuê
    NgayKetThuc DATE NOT NULL, -- Ngày kết thúc thuê
    GiaThue DECIMAL(15,2) NOT NULL, -- Giá thuê
    TienDatCoc DECIMAL(15,2), -- Tiền đặt cọc
    DieuKhoan NVARCHAR(500), -- Điều khoản
    GhiChu NVARCHAR(255), -- Ghi chú
    TrangThaiHopDong NVARCHAR(20) CHECK (TrangThaiHopDong IN ('Đang hiệu lực', 'Hết hạn', 'Hủy')), -- Trạng thái hợp đồng
    FileHopDong VARCHAR(500), -- URL file PDF hợp đồng
    FOREIGN KEY (MaGDThue) REFERENCES GiaoDichThue(MaGDThue),
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan),
    FOREIGN KEY (ID_Xe) REFERENCES XeMay(ID_Xe)
);

-- Bảng thanh toán
CREATE TABLE ThanhToan (
    MaThanhToan INT IDENTITY PRIMARY KEY, -- Mã thanh toán
    MaGDBan INT NULL, -- Mã giao dịch bán
    MaGDThue INT NULL, -- Mã giao dịch thuê
    SoTien DECIMAL(15,2) NOT NULL, -- Số tiền thanh toán
    NgayThanhToan DATE NOT NULL, -- Ngày thanh toán
    PhuongThuc NVARCHAR(50), -- Phương thức thanh toán
    GhiChu NVARCHAR(255), -- Ghi chú
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

-- Bảng hóa đơn
CREATE TABLE HoaDon (
    MaHD INT IDENTITY PRIMARY KEY, -- Mã hóa đơn
    MaKH CHAR(10) NOT NULL, -- Mã khách hàng
    MaTaiKhoan CHAR(10) NOT NULL, -- Mã nhân viên thu ngân
    NgayLap DATE NOT NULL, -- Ngày lập hóa đơn
    TongTien DECIMAL(15,2) NOT NULL, -- Tổng tiền trước khuyến mãi
    MaKM CHAR(10) NULL, -- Mã khuyến mãi
    SoTienGiam DECIMAL(15,2) DEFAULT 0, -- Số tiền giảm
    ThanhTien DECIMAL(15,2) NOT NULL, -- Số tiền thanh toán
    HinhThucThanhToan NVARCHAR(50), -- Hình thức thanh toán
    GhiChu NVARCHAR(255), -- Ghi chú
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan),
    FOREIGN KEY (MaKM) REFERENCES KhuyenMai(MaKM)
);

-- Bảng chi tiết hóa đơn
CREATE TABLE ChiTietHoaDon (
    MaChiTiet INT IDENTITY PRIMARY KEY, -- Mã chi tiết hóa đơn
    MaHD INT NOT NULL, -- Mã hóa đơn
    MaGDBan INT NULL, -- Mã giao dịch bán
    MaGDThue INT NULL, -- Mã giao dịch thuê
    SoTien DECIMAL(15,2) NOT NULL, -- Số tiền
    GhiChu NVARCHAR(255), -- Ghi chú
    FOREIGN KEY (MaHD) REFERENCES HoaDon(MaHD),
    FOREIGN KEY (MaGDBan) REFERENCES GiaoDichBan(MaGDBan),
    FOREIGN KEY (MaGDThue) REFERENCES GiaoDichThue(MaGDThue),
    CONSTRAINT CHK_ChiTietHoaDon_OneGiaoDich CHECK (
        (MaGDBan IS NOT NULL AND MaGDThue IS NULL) OR
        (MaGDBan IS NULL AND MaGDThue IS NOT NULL)
    )
);

/* ============================================================
   TRIGGERS, FUNCTIONS, PROCEDURES
   ============================================================ */

-- Trigger cập nhật trạng thái xe trên GiaoDichThue
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
GO

-- Function kiểm tra khuyến mãi áp dụng
CREATE OR ALTER FUNCTION dbo.fn_KiemTraKhuyenMaiApDung(
    @MaKM CHAR(10),
    @NgayGiaoDich DATETIME,
    @LoaiApDung NVARCHAR(20)
)
RETURNS BIT
AS
BEGIN
    DECLARE @KetQua BIT = 0;
   
    SELECT @KetQua = 1
    FROM KhuyenMai
    WHERE MaKM = @MaKM
        AND TrangThai = N'Hoạt động'
        AND @NgayGiaoDich BETWEEN NgayBatDau AND NgayKetThuc
        AND (LoaiApDung = N'Tất cả' OR LoaiApDung = @LoaiApDung);
   
    RETURN ISNULL(@KetQua, 0);
END;
GO

-- Function tính giá trị giảm
CREATE OR ALTER FUNCTION dbo.fn_TinhGiaTriGiam(
    @MaKM CHAR(10),
    @GiaTriDonHang DECIMAL(15,2)
)
RETURNS DECIMAL(15,2)
AS
BEGIN
    DECLARE @GiaTriGiam DECIMAL(15,2) = 0;
    DECLARE @LoaiKM NVARCHAR(20);
    DECLARE @PhanTramGiam DECIMAL(5,2);
    DECLARE @SoTienGiam DECIMAL(15,2);
    DECLARE @GiaTriGiamToiDa DECIMAL(15,2);
   
    SELECT
        @LoaiKM = LoaiKhuyenMai,
        @PhanTramGiam = PhanTramGiam,
        @SoTienGiam = SoTienGiam,
        @GiaTriGiamToiDa = GiaTriGiamToiDa
    FROM KhuyenMai
    WHERE MaKM = @MaKM;
   
    IF @LoaiKM = N'Giảm %'
    BEGIN
        SET @GiaTriGiam = @GiaTriDonHang * @PhanTramGiam / 100;
       
        -- Áp dụng giới hạn tối đa
        IF @GiaTriGiamToiDa IS NOT NULL AND @GiaTriGiam > @GiaTriGiamToiDa
            SET @GiaTriGiam = @GiaTriGiamToiDa;
    END
    ELSE IF @LoaiKM = N'Giảm tiền'
    BEGIN
        SET @GiaTriGiam = @SoTienGiam;
    END
   
    -- Không giảm quá giá trị đơn hàng
    IF @GiaTriGiam > @GiaTriDonHang
        SET @GiaTriGiam = @GiaTriDonHang;
   
    RETURN ISNULL(@GiaTriGiam, 0);
END;
GO

-- Procedure thêm giao dịch bán kèm phụ tùng
CREATE PROCEDURE sp_ThemGiaoDichBanKemPhuTung
    @MaKH CHAR(10),
    @ID_Xe CHAR(10),
    @NgayBan DATETIME,
    @GiaBan DECIMAL(15,2),
    @TrangThaiThanhToan NVARCHAR(50),
    @HinhThucThanhToan NVARCHAR(50),
    @MaTaiKhoan CHAR(10),
    @MaKM_Xe CHAR(10) = NULL,
    @SoTienGiam_Xe DECIMAL(15,2) = 0,
    @DanhSachPhuTung NVARCHAR(MAX),
    @MaGDBan INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
   
    BEGIN TRY
        -- 1. Kiểm tra tồn kho phụ tùng
        IF @DanhSachPhuTung IS NOT NULL AND @DanhSachPhuTung != '[]'
        BEGIN
            DECLARE @PhuTungKhongDu NVARCHAR(MAX);
           
            SELECT @PhuTungKhongDu = STRING_AGG(pt.TenPhuTung + ' (Tồn: ' + CAST(kpt.SoLuongTon AS VARCHAR) + ', Cần: ' + JSON_VALUE(js.value, '$.SoLuong') + ')', ', ')
            FROM OPENJSON(@DanhSachPhuTung) js
            INNER JOIN PhuTung pt ON pt.MaPhuTung = JSON_VALUE(js.value, '$.MaPhuTung')
            LEFT JOIN KhoPhuTung kpt ON kpt.MaPhuTung = pt.MaPhuTung
            WHERE ISNULL(kpt.SoLuongTon, 0) < CAST(JSON_VALUE(js.value, '$.SoLuong') AS INT);
           
            IF @PhuTungKhongDu IS NOT NULL
            BEGIN
                ROLLBACK TRANSACTION;
                RAISERROR(N'Số lượng tồn kho phụ tùng không đủ: %s', 16, 1, @PhuTungKhongDu);
                RETURN;
            END
        END
       
        -- 1.1. Kiểm tra số lượng xe
        DECLARE @SoLuongXe INT;
        SELECT @SoLuongXe = SoLuong FROM XeMay WHERE ID_Xe = @ID_Xe;
       
        IF @SoLuongXe IS NULL OR @SoLuongXe <= 0
        BEGIN
            ROLLBACK TRANSACTION;
            RAISERROR(N'Xe không còn tồn kho!', 16, 1);
            RETURN;
        END
       
        -- 2. Thêm giao dịch bán
        INSERT INTO GiaoDichBan (MaKH, ID_Xe, NgayBan, GiaBan, TrangThaiThanhToan, HinhThucThanhToan, MaTaiKhoan, MaKM, SoTienGiam)
        VALUES (@MaKH, @ID_Xe, @NgayBan, @GiaBan, @TrangThaiThanhToan, @HinhThucThanhToan, @MaTaiKhoan, @MaKM_Xe, @SoTienGiam_Xe);
       
        SET @MaGDBan = SCOPE_IDENTITY();
       
        -- 3. Thêm phụ tùng (nếu có)
        IF @DanhSachPhuTung IS NOT NULL AND @DanhSachPhuTung != '[]'
        BEGIN
            -- 3.1. Thêm chi tiết phụ tùng
            INSERT INTO ChiTietPhuTungBan (MaGDBan, MaPhuTung, SoLuong, DonGia, ThanhTien, MaKM, SoTienGiam)
            SELECT
                @MaGDBan,
                JSON_VALUE(value, '$.MaPhuTung'),
                CAST(JSON_VALUE(value, '$.SoLuong') AS INT),
                CAST(JSON_VALUE(value, '$.DonGia') AS DECIMAL(15,2)),
                CAST(JSON_VALUE(value, '$.SoLuong') AS INT) * CAST(JSON_VALUE(value, '$.DonGia') AS DECIMAL(15,2)),
                CASE WHEN JSON_VALUE(value, '$.MaKM') = '' OR JSON_VALUE(value, '$.MaKM') IS NULL
                     THEN NULL
                     ELSE JSON_VALUE(value, '$.MaKM') END,
                ISNULL(CAST(JSON_VALUE(value, '$.SoTienGiam') AS DECIMAL(15,2)), 0)
            FROM OPENJSON(@DanhSachPhuTung);
           
            -- 3.2. Trừ tồn kho phụ tùng
            UPDATE kpt
            SET kpt.SoLuongTon = kpt.SoLuongTon - CAST(JSON_VALUE(js.value, '$.SoLuong') AS INT),
                kpt.NgayCapNhat = GETDATE()
            FROM KhoPhuTung kpt
            INNER JOIN OPENJSON(@DanhSachPhuTung) js ON kpt.MaPhuTung = JSON_VALUE(js.value, '$.MaPhuTung');
        END
       
        -- 4. Cập nhật tổng tiền
        UPDATE GiaoDichBan
        SET TongGiaPhuTung = ISNULL((SELECT SUM(ThanhTien) FROM ChiTietPhuTungBan WHERE MaGDBan = @MaGDBan), 0),
            TongGiamPhuTung = ISNULL((SELECT SUM(SoTienGiam) FROM ChiTietPhuTungBan WHERE MaGDBan = @MaGDBan), 0),
            TongThanhToan = (GiaBan - SoTienGiam) + ISNULL((SELECT SUM(ThanhTien - SoTienGiam) FROM ChiTietPhuTungBan WHERE MaGDBan = @MaGDBan), 0)
        WHERE MaGDBan = @MaGDBan;
       
        -- 5. TRỪ SỐ LƯỢNG XE và CẬP NHẬT TRẠNG THÁI
        UPDATE XeMay
        SET SoLuong = SoLuong - 1,
            SoLuongBanRa = ISNULL(SoLuongBanRa, 0) + 1,
            TrangThai = CASE
                WHEN (SoLuong - 1) <= 0 THEN N'Đã bán' -- Hết hàng
                ELSE N'Sẵn sàng' -- Còn hàng
            END
        WHERE ID_Xe = @ID_Xe;
       
        COMMIT TRANSACTION;
       
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END
GO


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

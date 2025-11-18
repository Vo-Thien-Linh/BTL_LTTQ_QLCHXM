USE QuanLyCHXeMay;
GO

-- Thêm cột MucDichSuDung (đã có)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('XeMay') AND name = 'MucDichSuDung')
BEGIN
    ALTER TABLE XeMay
    ADD MucDichSuDung NVARCHAR(20) CHECK (MucDichSuDung IN (N'Cho thuê', N'Bán'));
    
    -- Set giá trị mặc định cho dữ liệu cũ
    UPDATE XeMay SET MucDichSuDung = N'Bán' WHERE MucDichSuDung IS NULL;
END
GO

-- Thêm cột GiaNhap (giá vốn khi nhập về)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('XeMay') AND name = 'GiaNhap')
BEGIN
    ALTER TABLE XeMay
    ADD GiaNhap DECIMAL(15,2);
    
    -- Set giá nhập = giá mua cho dữ liệu cũ
    UPDATE XeMay SET GiaNhap = GiaMua WHERE GiaNhap IS NULL;
END
GO

-- Thêm cột SoLuong (số lượng tồn kho)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('XeMay') AND name = 'SoLuong')
BEGIN
    ALTER TABLE XeMay
    ADD SoLuong INT DEFAULT 1;
    
    -- Set số lượng = 1 cho các xe hiện có
    UPDATE XeMay SET SoLuong = 1 WHERE SoLuong IS NULL;
END
GO

-- Thêm cột SoLuongBanRa (thống kê số lượng đã bán)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('XeMay') AND name = 'SoLuongBanRa')
BEGIN
    ALTER TABLE XeMay
    ADD SoLuongBanRa INT DEFAULT 0;
    
    -- Set số lượng bán = 0 cho các xe hiện có
    UPDATE XeMay SET SoLuongBanRa = 0 WHERE SoLuongBanRa IS NULL;
END
GO

-- Kiểm tra kết quả
SELECT TOP 5 ID_Xe, BienSo, MucDichSuDung, GiaNhap, GiaMua, SoLuong, SoLuongBanRa, TrangThai
FROM XeMay;
GO

PRINT 'Đã thêm các cột mới thành công!';

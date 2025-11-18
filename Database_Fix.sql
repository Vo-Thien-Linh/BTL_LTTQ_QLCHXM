-- Script sửa database XeMay để khớp với code
-- Chạy script này trong SQL Server Management Studio

USE QLCuaHangXeMay;
GO

-- 1. Thêm cột MucDichSuDung nếu chưa có
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('XeMay') AND name = 'MucDichSuDung')
BEGIN
    ALTER TABLE XeMay 
    ADD MucDichSuDung NVARCHAR(20);
    PRINT 'Đã thêm cột MucDichSuDung';
END
ELSE
BEGIN
    PRINT 'Cột MucDichSuDung đã tồn tại';
END
GO

-- 2. Xóa constraint cũ của TrangThai nếu có
DECLARE @ConstraintName NVARCHAR(200);
SELECT @ConstraintName = name 
FROM sys.check_constraints 
WHERE parent_object_id = OBJECT_ID('XeMay') 
AND COL_NAME(parent_object_id, parent_column_id) = 'TrangThai';

IF @ConstraintName IS NOT NULL
BEGIN
    EXEC('ALTER TABLE XeMay DROP CONSTRAINT ' + @ConstraintName);
    PRINT 'Đã xóa constraint cũ: ' + @ConstraintName;
END
GO

-- 3. Thêm constraint mới cho TrangThai (CÓ DẤU)
ALTER TABLE XeMay 
ADD CONSTRAINT CK_XeMay_TrangThai 
CHECK (TrangThai IN (N'Sẵn sàng', N'Đang thuê', N'Đã bán', N'Đang bảo trì'));
PRINT 'Đã thêm constraint mới cho TrangThai';
GO

-- 4. Xóa constraint cũ của MucDichSuDung nếu có
DECLARE @ConstraintName2 NVARCHAR(200);
SELECT @ConstraintName2 = name 
FROM sys.check_constraints 
WHERE parent_object_id = OBJECT_ID('XeMay') 
AND COL_NAME(parent_object_id, parent_column_id) = 'MucDichSuDung';

IF @ConstraintName2 IS NOT NULL
BEGIN
    EXEC('ALTER TABLE XeMay DROP CONSTRAINT ' + @ConstraintName2);
    PRINT 'Đã xóa constraint cũ của MucDichSuDung: ' + @ConstraintName2;
END
GO

-- 5. Thêm constraint cho MucDichSuDung (CÓ DẤU)
ALTER TABLE XeMay 
ADD CONSTRAINT CK_XeMay_MucDichSuDung 
CHECK (MucDichSuDung IN (N'Cho thuê', N'Bán'));
PRINT 'Đã thêm constraint cho MucDichSuDung';
GO

-- 6. Cập nhật dữ liệu cũ nếu có (optional - set mặc định)
UPDATE XeMay 
SET MucDichSuDung = N'Cho thuê' 
WHERE MucDichSuDung IS NULL;
PRINT 'Đã cập nhật dữ liệu cũ';
GO

-- 7. Kiểm tra kết quả
PRINT '=== KIỂM TRA KẾT QUẢ ===';
SELECT 
    c.name AS 'Tên cột',
    t.name AS 'Kiểu dữ liệu',
    c.max_length AS 'Độ dài',
    c.is_nullable AS 'Cho phép NULL'
FROM sys.columns c
JOIN sys.types t ON c.user_type_id = t.user_type_id
WHERE c.object_id = OBJECT_ID('XeMay')
AND c.name IN ('TrangThai', 'MucDichSuDung');

SELECT 
    name AS 'Constraint Name',
    definition AS 'Định nghĩa'
FROM sys.check_constraints
WHERE parent_object_id = OBJECT_ID('XeMay');

PRINT 'Hoàn thành!';
GO

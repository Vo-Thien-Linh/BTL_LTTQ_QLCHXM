-- Script cập nhật database để thêm cột NgayBaoTri vào bảng BaoTriXe
-- Chạy script này để đảm bảo mỗi phiếu bảo trì có thời gian riêng

USE [QLCuaHangXeMay]
GO

-- Kiểm tra và thêm cột NgayBaoTri nếu chưa tồn tại
IF NOT EXISTS (SELECT * FROM sys.columns 
               WHERE object_id = OBJECT_ID(N'[dbo].[BaoTriXe]') 
               AND name = 'NgayBaoTri')
BEGIN
    ALTER TABLE [dbo].[BaoTriXe]
    ADD [NgayBaoTri] DATETIME NULL DEFAULT GETDATE();
    
    PRINT 'Đã thêm cột NgayBaoTri vào bảng BaoTriXe';
    
    -- Cập nhật dữ liệu cũ (nếu có) với thời gian hiện tại
    UPDATE [dbo].[BaoTriXe]
    SET [NgayBaoTri] = GETDATE()
    WHERE [NgayBaoTri] IS NULL;
    
    PRINT 'Đã cập nhật NgayBaoTri cho các bản ghi cũ';
END
ELSE
BEGIN
    PRINT 'Cột NgayBaoTri đã tồn tại trong bảng BaoTriXe';
END
GO

-- Kiểm tra cấu trúc bảng sau khi cập nhật
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'BaoTriXe'
ORDER BY ORDINAL_POSITION;
GO

PRINT 'Hoàn tất cập nhật cơ sở dữ liệu!';
PRINT 'Mỗi phiếu bảo trì giờ đây sẽ có ID_BaoTri và NgayBaoTri riêng biệt.';
GO

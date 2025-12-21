-- Script chuẩn hóa mã khách hàng
-- Giảm số chữ số 0 trong mã khách hàng từ KH00000XXX xuống KH0XXX (4 chữ số)

USE QLCHXM
GO

-- Xem các mã khách hàng hiện tại
SELECT MaKH, LEN(MaKH) as ChieuDai
FROM KhachHang
ORDER BY MaKH;

-- Cập nhật các mã khách hàng về format mới (KH + 4 chữ số)
-- Chuyển từ KH00000011 thành KH0011

UPDATE KhachHang
SET MaKH = 'KH' + RIGHT('0000' + CAST(CAST(SUBSTRING(MaKH, 3, LEN(MaKH) - 2) AS INT) AS VARCHAR), 4)
WHERE MaKH LIKE 'KH%';

-- Kiểm tra lại sau khi cập nhật
SELECT MaKH, LEN(MaKH) as ChieuDai
FROM KhachHang
ORDER BY MaKH;

-- Kiểm tra mã lớn nhất
SELECT TOP 1 MaKH
FROM KhachHang
ORDER BY CAST(SUBSTRING(MaKH, 3, LEN(MaKH) - 2) AS INT) DESC;

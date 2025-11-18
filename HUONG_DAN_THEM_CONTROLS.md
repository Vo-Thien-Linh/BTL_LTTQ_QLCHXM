# Hướng dẫn thêm Controls cho Giá Nhập và Số Lượng

## 1. Mở FormThemXe trong Designer

1. Double-click vào `FormThemXe.cs` trong Solution Explorer
2. Nhấn **View** → **Designer** (hoặc Shift+F7)

## 2. Thêm Controls vào GroupBox1

### Thêm Label và TextBox cho Giá Nhập:
1. Kéo **Label** từ Toolbox vào form, đặt bên dưới "Giá mua"
   - Properties:
     - **(Name)**: `label19`
     - **Text**: `Giá nhập`
     - **Location**: `271, 280`
     - **Size**: `60, 16`

2. Kéo **TextBox** từ Toolbox
   - Properties:
     - **(Name)**: `txtGiaNhap`
     - **Location**: `418, 277`
     - **Size**: `129, 22`
     - **TabIndex**: `16`

### Thêm Label và NumericUpDown cho Số Lượng:
1. Kéo **Label** từ Toolbox, đặt bên dưới "Giá nhập"
   - Properties:
     - **(Name)**: `label20`
     - **Text**: `Số lượng`
     - **Location**: `271, 310`
     - **Size**: `60, 16`

2. Kéo **NumericUpDown** từ Toolbox
   - Properties:
     - **(Name)**: `nudSoLuong`
     - **Location**: `418, 307`
     - **Size**: `129, 22`
     - **TabIndex**: `17`
     - **Minimum**: `1`
     - **Maximum**: `1000`
     - **Value**: `1`

## 3. Sắp xếp lại các controls phía dưới

- Di chuyển các controls bên dưới xuống thêm 60px để tạo chỗ cho 2 trường mới

## 4. Lưu và đóng Designer

## 5. Cập nhật Code-Behind (đã tự động)

Sau khi làm xong, form sẽ có:
- ✅ Giá mua (giá bán)
- ✅ Giá nhập (giá vốn)
- ✅ Số lượng tồn kho

---

**LƯU Ý**: Nếu làm thủ công Designer phức tạp, bạn có thể cho tôi biết để tôi tạo code tự động!

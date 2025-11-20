# Hướng dẫn cài đặt iTextSharp cho xuất PDF

## Cách 1: Sử dụng NuGet Package Manager (Khuyến nghị)

1. Mở Solution trong Visual Studio
2. Right-click vào project **UI** → chọn **Manage NuGet Packages**
3. Chọn tab **Browse**
4. Tìm kiếm: `iTextSharp`
5. Chọn package **iTextSharp** (version 5.5.13.3)
6. Click **Install**

## Cách 2: Sử dụng Package Manager Console

1. Trong Visual Studio, mở **Tools** → **NuGet Package Manager** → **Package Manager Console**
2. Chọn Default project: **UI**
3. Chạy lệnh:
```
Install-Package iTextSharp -Version 5.5.13.3
```

## Cách 3: Sử dụng Command Line

Mở PowerShell tại thư mục `f:\LTTQ\UI` và chạy:
```powershell
nuget install iTextSharp -Version 5.5.13.3 -OutputDirectory packages
```

Sau đó add reference vào project UI:
- Right-click **References** trong project UI
- Chọn **Add Reference**
- Browse đến: `f:\LTTQ\UI\packages\iTextSharp.5.5.13.3\lib\itextsharp.dll`
- Click OK

## Kiểm tra cài đặt

Sau khi cài đặt xong, file `packages.config` trong UI project sẽ có dòng:
```xml
<package id="iTextSharp" version="5.5.13.3" targetFramework="net472" />
```

## Lưu ý

- iTextSharp 5.5.13.3 là phiên bản miễn phí cuối cùng (LGPL license)
- Phiên bản mới hơn (iText 7+) là thương mại
- Package hỗ trợ .NET Framework 4.0 trở lên

using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.IO;

namespace UI
{
    /// <summary>
    /// Helper class để tạo file PDF cho hợp đồng mua xe máy
    /// Yêu cầu: iTextSharp 5.5.13.3
    /// </summary>
    public class PDFHelper
    {
        // Font Unicode cho tiếng Việt
        private static BaseFont baseFont;
        private static Font titleFont;
        private static Font headerFont;
        private static Font normalFont;
        private static Font boldFont;
        private static Font smallFont;

        static PDFHelper()
        {
            try
            {
                // Sử dụng Arial Unicode MS để hỗ trợ tiếng Việt
                string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                titleFont = new Font(baseFont, 18, Font.BOLD, BaseColor.BLUE);
                headerFont = new Font(baseFont, 14, Font.BOLD, BaseColor.BLACK);
                boldFont = new Font(baseFont, 11, Font.BOLD, BaseColor.BLACK);
                normalFont = new Font(baseFont, 11, Font.NORMAL, BaseColor.BLACK);
                smallFont = new Font(baseFont, 10, Font.NORMAL, BaseColor.DARK_GRAY);
            }
            catch
            {
                // Fallback nếu không tìm thấy Arial
                baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                titleFont = new Font(baseFont, 18, Font.BOLD, BaseColor.BLUE);
                headerFont = new Font(baseFont, 14, Font.BOLD, BaseColor.BLACK);
                boldFont = new Font(baseFont, 11, Font.BOLD, BaseColor.BLACK);
                normalFont = new Font(baseFont, 11, Font.NORMAL, BaseColor.BLACK);
                smallFont = new Font(baseFont, 10, Font.NORMAL, BaseColor.DARK_GRAY);
            }
        }

        /// <summary>
        /// Xuất danh sách hợp đồng ra file PDF
        /// </summary>
        public static void ExportDanhSachHopDong(DataTable data, string filePath)
        {
            Document document = new Document(PageSize.A4.Rotate(), 25, 25, 30, 30);

            try
            {
                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();

                // Tiêu đề
                Paragraph title = new Paragraph("DANH SÁCH HỢP ĐỒNG MUA XE MÁY", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 10f;
                document.Add(title);

                // Ngày xuất
                Paragraph date = new Paragraph($"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", smallFont);
                date.Alignment = Element.ALIGN_RIGHT;
                date.SpacingAfter = 20f;
                document.Add(date);

                // Tạo bảng
                PdfPTable table = new PdfPTable(9);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 5f, 7f, 10f, 10f, 15f, 10f, 12f, 10f, 10f });

                // Header
                AddTableHeader(table, "Mã HĐ");
                AddTableHeader(table, "Mã GD");
                AddTableHeader(table, "Mã KH");
                AddTableHeader(table, "Họ tên KH");
                AddTableHeader(table, "Biển số");
                AddTableHeader(table, "Ngày lập");
                AddTableHeader(table, "Giá bán");
                AddTableHeader(table, "Nhân viên");
                AddTableHeader(table, "Trạng thái");

                // Data rows
                foreach (DataRow row in data.Rows)
                {
                    AddTableCell(table, row["MaHDM"]?.ToString() ?? "");
                    AddTableCell(table, row["MaGDBan"]?.ToString() ?? "");
                    AddTableCell(table, row["MaKH"]?.ToString() ?? "");
                    AddTableCell(table, row["HoTenKH"]?.ToString() ?? "");
                    AddTableCell(table, row["BienSo"]?.ToString() ?? "");
                    AddTableCell(table, row["NgayLap"] != DBNull.Value
                        ? Convert.ToDateTime(row["NgayLap"]).ToString("dd/MM/yyyy")
                        : "");
                    AddTableCell(table, row["GiaBan"] != DBNull.Value
                        ? Convert.ToDecimal(row["GiaBan"]).ToString("N0")
                        : "");
                    AddTableCell(table, row["TenNhanVien"]?.ToString() ?? "");
                    AddTableCell(table, row["TrangThaiHopDong"]?.ToString() ?? "");
                }

                document.Add(table);

                // Footer
                Paragraph footer = new Paragraph($"\nTổng số hợp đồng: {data.Rows.Count}", boldFont);
                footer.SpacingBefore = 20f;
                document.Add(footer);
            }
            finally
            {
                document.Close();
            }
        }

        /// <summary>
        /// Xuất chi tiết hợp đồng ra file PDF
        /// </summary>
        public static void ExportChiTietHopDong(DataRow hopDong, string filePath)
        {
            Document document = new Document(PageSize.A4, 40, 40, 30, 30);

            try
            {
                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();

                // ==================== HEADER ====================
                PdfPTable headerTable = new PdfPTable(2);
                headerTable.WidthPercentage = 100;
                headerTable.SetWidths(new float[] { 50f, 50f });
                headerTable.SpacingAfter = 15f;

                // Bên trái - Thông tin cửa hàng
                PdfPCell leftCell = new PdfPCell();
                leftCell.Border = Rectangle.NO_BORDER;
                Font shopNameFont = new Font(baseFont, 16, Font.BOLD, new BaseColor(0, 102, 204));
                leftCell.AddElement(new Paragraph("CỬA HÀNG XE MÁY ABC", shopNameFont));
                leftCell.AddElement(new Paragraph("Địa chỉ: 123 Đường Lê Lợi, Quận 1, TP.HCM", smallFont));
                leftCell.AddElement(new Paragraph("Điện thoại: 0123.456.789 | Hotline: 0987.654.321", smallFont));
                leftCell.AddElement(new Paragraph("Email: info@cuahangxemay.vn", smallFont));
                leftCell.AddElement(new Paragraph("MST: 0123456789", smallFont));
                leftCell.PaddingTop = 5;
                headerTable.AddCell(leftCell);

                // Bên phải - Mã hợp đồng và ngày lập
                PdfPCell rightCell = new PdfPCell();
                rightCell.Border = Rectangle.NO_BORDER;
                rightCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                rightCell.AddElement(new Paragraph($"Số hợp đồng: {hopDong["MaHDM"]}", boldFont));
                rightCell.AddElement(new Paragraph($"Mã GD: {hopDong["MaGDBan"]}", normalFont));
                rightCell.AddElement(new Paragraph($"Ngày lập: {Convert.ToDateTime(hopDong["NgayLap"]):dd/MM/yyyy}", normalFont));
                rightCell.AddElement(new Paragraph($"Giờ: {Convert.ToDateTime(hopDong["NgayLap"]):HH:mm:ss}", smallFont));
                rightCell.PaddingTop = 5;
                headerTable.AddCell(rightCell);

                document.Add(headerTable);

                // Đường kẻ ngang
                PdfPTable lineTable = new PdfPTable(1);
                lineTable.WidthPercentage = 100;
                lineTable.SpacingAfter = 10f;
                PdfPCell lineCell = new PdfPCell();
                lineCell.Border = Rectangle.BOTTOM_BORDER;
                lineCell.BorderWidth = 1.5f;
                lineCell.BorderColor = BaseColor.GRAY;
                lineCell.FixedHeight = 0f;
                lineTable.AddCell(lineCell);
                document.Add(lineTable);

                // ==================== TIÊU ĐỀ ====================
                Font titleBigFont = new Font(baseFont, 20, Font.BOLD, new BaseColor(0, 51, 153));
                Paragraph title = new Paragraph("HỢP ĐỒNG MUA BÁN XE MÁY", titleBigFont);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 10f;
                document.Add(title);

                // Căn cứ pháp lý
                Font italicFont = new Font(baseFont, 10, Font.ITALIC, BaseColor.BLACK);
                Paragraph legal = new Paragraph("Căn cứ Bộ luật Dân sự số 91/2015/QH13", italicFont);
                legal.Alignment = Element.ALIGN_CENTER;
                legal.SpacingAfter = 15f;
                document.Add(legal);

                // ==================== BÊN BÁN (BÊN A) ====================
                Font sectionTitleFont = new Font(baseFont, 13, Font.BOLD, new BaseColor(0, 102, 204));
                Paragraph benBan = new Paragraph("BÊN BÁN (BÊN A):", sectionTitleFont);
                benBan.SpacingBefore = 10f;
                benBan.SpacingAfter = 8f;
                document.Add(benBan);

                // Bảng thông tin bên bán
                PdfPTable benBanTable = new PdfPTable(2);
                benBanTable.WidthPercentage = 100;
                benBanTable.SetWidths(new float[] { 30f, 70f });
                benBanTable.SpacingAfter = 10f;

                AddSimpleInfoRow(benBanTable, "Tên cửa hàng:", "CỬA HÀNG XE MÁY ABC");
                AddSimpleInfoRow(benBanTable, "Địa chỉ:", "123 Đường Lê Lợi, Quận 1, TP.HCM");
                AddSimpleInfoRow(benBanTable, "Đại diện:", hopDong["TenNhanVien"]?.ToString() ?? "Giám đốc");
                AddSimpleInfoRow(benBanTable, "Điện thoại:", "0123.456.789");
                AddSimpleInfoRow(benBanTable, "MST:", "0123456789");
                document.Add(benBanTable);

                // ==================== BÊN MUA (BÊN B) ====================
                Paragraph benMua = new Paragraph("BÊN MUA (BÊN B):", sectionTitleFont);
                benMua.SpacingBefore = 15f;
                benMua.SpacingAfter = 8f;
                document.Add(benMua);

                PdfPTable benMuaTable = new PdfPTable(2);
                benMuaTable.WidthPercentage = 100;
                benMuaTable.SetWidths(new float[] { 30f, 70f });
                benMuaTable.SpacingAfter = 10f;

                string tenKH = hopDong["HoTenKH"]?.ToString() ?? "";
                string sdtKH = hopDong["Sdt"]?.ToString() ?? "";
                string khachHangInfo = $"{tenKH}, SĐT: {sdtKH}";
                
                AddSimpleInfoRow(benMuaTable, "Khách hàng:", khachHangInfo);
                AddSimpleInfoRow(benMuaTable, "CCCD/CMND:", hopDong["SoCCCD"]?.ToString() ?? "");
                AddSimpleInfoRow(benMuaTable, "Địa chỉ:", hopDong["DiaChi"]?.ToString() ?? "");
                AddSimpleInfoRow(benMuaTable, "Mã khách hàng:", hopDong["MaKH"]?.ToString() ?? "");
                document.Add(benMuaTable);

                // ==================== THÔNG TIN XE MÁY ====================
                Paragraph thongTinXe = new Paragraph("THÔNG TIN XE MÁY:", sectionTitleFont);
                thongTinXe.SpacingBefore = 15f;
                thongTinXe.SpacingAfter = 8f;
                document.Add(thongTinXe);

                // Tạo bảng 2 cột cho thông tin xe
                PdfPTable xeTable = new PdfPTable(2);
                xeTable.WidthPercentage = 100;
                xeTable.SetWidths(new float[] { 30f, 70f });
                xeTable.SpacingAfter = 10f;

                AddSimpleInfoRow(xeTable, "Tên xe:", hopDong["TenXe"]?.ToString() ?? "");
                AddSimpleInfoRow(xeTable, "Biển số:", hopDong["BienSo"]?.ToString() ?? "Chưa đăng ký");
                AddSimpleInfoRow(xeTable, "Hãng xe:", hopDong["TenHangXe"]?.ToString() ?? "");
                AddSimpleInfoRow(xeTable, "Dòng xe:", hopDong["TenDongXe"]?.ToString() ?? "");
                AddSimpleInfoRow(xeTable, "Màu sắc:", hopDong["TenMauSac"]?.ToString() ?? "");
                AddSimpleInfoRow(xeTable, "Phân khối:", (hopDong["PhanKhoi"] != DBNull.Value ? hopDong["PhanKhoi"].ToString() + " cc" : ""));
                AddSimpleInfoRow(xeTable, "Thông tin xăng:", hopDong["ThongTinXang"]?.ToString() ?? "");
                AddSimpleInfoRow(xeTable, "Năm sản xuất:", hopDong["NamSanXuat"]?.ToString() ?? "");
                
                document.Add(xeTable);

                // ==================== ĐIỀU KHOẢN HỢP ĐỒNG ====================
                Paragraph dieuKhoan = new Paragraph("ĐIỀU KHOẢN HỢP ĐỒNG:", sectionTitleFont);
                dieuKhoan.SpacingBefore = 15f;
                dieuKhoan.SpacingAfter = 8f;
                document.Add(dieuKhoan);

                string dieuKhoanText = hopDong["DieuKhoan"]?.ToString();
                if (string.IsNullOrEmpty(dieuKhoanText))
                {
                    // Điều khoản mặc định
                    dieuKhoanText =
                        "Điều 1: Bên A cam kết xe máy nêu trên là tài sản hợp pháp, không tranh chấp, không nằm trong diện cầm cố, thế chấp.\n\n" +
                        "Điều 2: Bên B có trách nhiệm thanh toán đầy đủ số tiền ghi trong hợp đồng theo thỏa thuận.\n\n" +
                        "Điều 3: Bên A có trách nhiệm giao xe và toàn bộ giấy tờ liên quan đến xe cho Bên B ngay sau khi ký hợp đồng và nhận đủ tiền.\n\n" +
                        "Điều 4: Hai bên cam kết thực hiện đúng các điều khoản đã thỏa thuận. Nếu có tranh chấp, hai bên sẽ giải quyết trên tinh thần hòa giải. Nếu không thỏa thuận được sẽ đưa ra cơ quan pháp luật giải quyết.\n\n" +
                        "Điều 5: Hợp đồng có hiệu lực kể từ ngày ký và được lập thành 02 bản có giá trị pháp lý như nhau, mỗi bên giữ 01 bản.";
                }

                Paragraph dieuKhoanContent = new Paragraph(dieuKhoanText, normalFont);
                dieuKhoanContent.Alignment = Element.ALIGN_JUSTIFIED;
                dieuKhoanContent.Leading = 16f;
                document.Add(dieuKhoanContent);

                // Ghi chú (nếu có)
                if (!string.IsNullOrEmpty(hopDong["GhiChu"]?.ToString()))
                {
                    Paragraph ghiChu = new Paragraph("\nGHI CHÚ:", boldFont);
                    ghiChu.SpacingBefore = 10f;
                    document.Add(ghiChu);
                    document.Add(new Paragraph(hopDong["GhiChu"].ToString(), normalFont));
                }

                // ==================== CHỮ KÝ ====================
                document.Add(new Paragraph("\n\n"));

                PdfPTable signTable = new PdfPTable(2);
                signTable.WidthPercentage = 100;
                signTable.SetWidths(new float[] { 50f, 50f });
                signTable.SpacingBefore = 20f;

                // Bên bán
                PdfPCell signCell1 = new PdfPCell();
                signCell1.Border = Rectangle.NO_BORDER;
                signCell1.HorizontalAlignment = Element.ALIGN_CENTER;

                Paragraph dai_dien_a = new Paragraph("ĐẠI DIỆN BÊN BÁN (BÊN A)", boldFont);
                dai_dien_a.Alignment = Element.ALIGN_CENTER;
                signCell1.AddElement(dai_dien_a);

                Paragraph ky_ten_a = new Paragraph("(Ký, ghi rõ họ tên và đóng dấu)", smallFont);
                ky_ten_a.Alignment = Element.ALIGN_CENTER;
                signCell1.AddElement(ky_ten_a);

                signCell1.AddElement(new Paragraph("\n\n\n\n", normalFont));

                Paragraph ten_a = new Paragraph(hopDong["TenNhanVien"]?.ToString() ?? "_______________", normalFont);
                ten_a.Alignment = Element.ALIGN_CENTER;
                signCell1.AddElement(ten_a);

                signTable.AddCell(signCell1);

                // Bên mua
                PdfPCell signCell2 = new PdfPCell();
                signCell2.Border = Rectangle.NO_BORDER;
                signCell2.HorizontalAlignment = Element.ALIGN_CENTER;

                Paragraph dai_dien_b = new Paragraph("ĐẠI DIỆN BÊN MUA (BÊN B)", boldFont);
                dai_dien_b.Alignment = Element.ALIGN_CENTER;
                signCell2.AddElement(dai_dien_b);

                Paragraph ky_ten_b = new Paragraph("(Ký và ghi rõ họ tên)", smallFont);
                ky_ten_b.Alignment = Element.ALIGN_CENTER;
                signCell2.AddElement(ky_ten_b);

                signCell2.AddElement(new Paragraph("\n\n\n\n", normalFont));

                Paragraph ten_b = new Paragraph(hopDong["HoTenKH"]?.ToString() ?? "_______________", normalFont);
                ten_b.Alignment = Element.ALIGN_CENTER;
                signCell2.AddElement(ten_b);

                signTable.AddCell(signCell2);

                document.Add(signTable);

                // ==================== FOOTER ====================
                document.Add(new Paragraph("\n"));
                PdfPTable footerLineTable = new PdfPTable(1);
                footerLineTable.WidthPercentage = 100;
                PdfPCell footerLineCell = new PdfPCell();
                footerLineCell.Border = Rectangle.BOTTOM_BORDER;
                footerLineCell.BorderWidth = 0.5f;
                footerLineCell.BorderColor = BaseColor.LIGHT_GRAY;
                footerLineCell.FixedHeight = 0f;
                footerLineTable.AddCell(footerLineCell);
                document.Add(footerLineTable);

                Paragraph footer = new Paragraph(
                    $"Hợp đồng được lập lúc {Convert.ToDateTime(hopDong["NgayLap"]):HH:mm:ss} ngày {Convert.ToDateTime(hopDong["NgayLap"]):dd/MM/yyyy} - " +
                    $"Trạng thái: {hopDong["TrangThaiHopDong"]}",
                    smallFont
                );
                footer.Alignment = Element.ALIGN_CENTER;
                footer.SpacingBefore = 5f;
                document.Add(footer);

            }
            finally
            {
                document.Close();
            }
        }

        // Helper method đơn giản - Thêm hàng thông tin
        private static void AddSimpleInfoRow(PdfPTable table, string label, string value)
        {
            PdfPCell labelCell = new PdfPCell(new Phrase(label, boldFont));
            labelCell.BackgroundColor = new BaseColor(240, 248, 255);
            labelCell.Padding = 8;
            labelCell.Border = Rectangle.BOX;
            labelCell.BorderColor = BaseColor.LIGHT_GRAY;
            table.AddCell(labelCell);

            PdfPCell valueCell = new PdfPCell(new Phrase(value ?? "", normalFont));
            valueCell.Padding = 8;
            valueCell.Border = Rectangle.BOX;
            valueCell.BorderColor = BaseColor.LIGHT_GRAY;
            table.AddCell(valueCell);
        }

        // Helper method - Chuyển số thành chữ
        private static string ConvertNumberToWords(long number)
        {
            if (number == 0) return "Không đồng";

            string[] units = { "", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] levels = { "", "nghìn", "triệu", "tỷ" };

            string result = "";
            int level = 0;

            while (number > 0)
            {
                int temp = (int)(number % 1000);
                if (temp > 0)
                {
                    string tempStr = ConvertThreeDigits(temp, units);
                    result = tempStr + " " + levels[level] + " " + result;
                }
                number /= 1000;
                level++;
            }

            result = result.Trim() + " đồng";
            result = char.ToUpper(result[0]) + result.Substring(1);
            return result;
        }

        private static string ConvertThreeDigits(int number, string[] units)
        {
            int hundred = number / 100;
            int ten = (number % 100) / 10;
            int unit = number % 10;

            string result = "";

            if (hundred > 0)
            {
                result = units[hundred] + " trăm";
                if (ten == 0 && unit > 0)
                    result += " lẻ";
            }

            if (ten > 1)
            {
                result += " " + units[ten] + " mươi";
                if (unit == 1)
                    result += " mốt";
                else if (unit > 0)
                    result += " " + units[unit];
            }
            else if (ten == 1)
            {
                result += " mười";
                if (unit > 0)
                    result += " " + units[unit];
            }
            else if (unit > 0 && hundred > 0)
            {
                result += " " + units[unit];
            }
            else if (unit > 0)
            {
                result = units[unit];
            }

            return result.Trim();
        }

        /// <summary>
        /// Xuất hóa đơn mua xe (bao gồm xe và phụ tùng nếu có)
        /// </summary>
        public static void ExportHoaDonMuaXe(DataRow giaoDich, DataTable chiTietPhuTung, string filePath)
        {
            Document document = new Document(PageSize.A4, 40, 40, 30, 30);

            try
            {
                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();

                // ==================== HEADER ====================
                PdfPTable headerTable = new PdfPTable(2);
                headerTable.WidthPercentage = 100;
                headerTable.SetWidths(new float[] { 50f, 50f });
                headerTable.SpacingAfter = 15f;

                // Bên trái - Thông tin cửa hàng
                PdfPCell leftCell = new PdfPCell();
                leftCell.Border = Rectangle.NO_BORDER;
                Font shopNameFont = new Font(baseFont, 16, Font.BOLD, new BaseColor(0, 102, 204));
                leftCell.AddElement(new Paragraph("CỬA HÀNG XE MÁY ABC", shopNameFont));
                leftCell.AddElement(new Paragraph("Địa chỉ: 123 Đường Lê Lợi, Quận 1, TP.HCM", smallFont));
                leftCell.AddElement(new Paragraph("Điện thoại: 0123.456.789 | Hotline: 0987.654.321", smallFont));
                leftCell.AddElement(new Paragraph("Email: info@cuahangxemay.vn", smallFont));
                leftCell.AddElement(new Paragraph("MST: 0123456789", smallFont));
                leftCell.PaddingTop = 5;
                headerTable.AddCell(leftCell);

                // Bên phải - Mã hóa đơn và ngày
                PdfPCell rightCell = new PdfPCell();
                rightCell.Border = Rectangle.NO_BORDER;
                rightCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                rightCell.AddElement(new Paragraph($"HÓA ĐƠN BÁN HÀNG", new Font(baseFont, 14, Font.BOLD, BaseColor.RED)));
                rightCell.AddElement(new Paragraph($"Số: {giaoDich["MaGDBan"]}", boldFont));
                rightCell.AddElement(new Paragraph($"Ngày: {Convert.ToDateTime(giaoDich["NgayBan"]):dd/MM/yyyy}", normalFont));
                rightCell.AddElement(new Paragraph($"Giờ: {Convert.ToDateTime(giaoDich["NgayBan"]):HH:mm:ss}", smallFont));
                rightCell.PaddingTop = 5;
                headerTable.AddCell(rightCell);

                document.Add(headerTable);

                // Đường kẻ ngang
                PdfPTable lineTable = new PdfPTable(1);
                lineTable.WidthPercentage = 100;
                lineTable.SpacingAfter = 10f;
                PdfPCell lineCell = new PdfPCell();
                lineCell.Border = Rectangle.BOTTOM_BORDER;
                lineCell.BorderWidth = 1.5f;
                lineCell.BorderColor = BaseColor.GRAY;
                lineCell.FixedHeight = 0f;
                lineTable.AddCell(lineCell);
                document.Add(lineTable);

                // ==================== THÔNG TIN KHÁCH HÀNG ====================
                Font sectionTitleFont = new Font(baseFont, 12, Font.BOLD, new BaseColor(0, 102, 204));
                Paragraph khachHangTitle = new Paragraph("THÔNG TIN KHÁCH HÀNG", sectionTitleFont);
                khachHangTitle.SpacingBefore = 5f;
                khachHangTitle.SpacingAfter = 8f;
                document.Add(khachHangTitle);

                PdfPTable khachHangTable = new PdfPTable(2);
                khachHangTable.WidthPercentage = 100;
                khachHangTable.SetWidths(new float[] { 30f, 70f });
                khachHangTable.SpacingAfter = 10f;

                AddInvoiceInfoRow(khachHangTable, "Họ tên:", giaoDich["HoTenKH"]?.ToString() ?? "");
                AddInvoiceInfoRow(khachHangTable, "Mã KH:", giaoDich["MaKH"]?.ToString() ?? "");
                AddInvoiceInfoRow(khachHangTable, "Số điện thoại:", giaoDich["SdtKhachHang"]?.ToString() ?? "");
                
                if (giaoDich.Table.Columns.Contains("EmailKhachHang") && giaoDich["EmailKhachHang"] != DBNull.Value)
                    AddInvoiceInfoRow(khachHangTable, "Email:", giaoDich["EmailKhachHang"].ToString());
                
                if (giaoDich.Table.Columns.Contains("DiaChiKhachHang") && giaoDich["DiaChiKhachHang"] != DBNull.Value)
                    AddInvoiceInfoRow(khachHangTable, "Địa chỉ:", giaoDich["DiaChiKhachHang"].ToString());
                
                document.Add(khachHangTable);

                // ==================== CHI TIẾT SẢN PHẨM ====================
                Paragraph chiTietTitle = new Paragraph("CHI TIẾT SẢN PHẨM", sectionTitleFont);
                chiTietTitle.SpacingBefore = 10f;
                chiTietTitle.SpacingAfter = 8f;
                document.Add(chiTietTitle);

                // Bảng chi tiết
                PdfPTable productTable = new PdfPTable(5);
                productTable.WidthPercentage = 100;
                productTable.SetWidths(new float[] { 10f, 40f, 15f, 17f, 18f });
                productTable.SpacingAfter = 10f;

                // Header bảng
                AddProductTableHeader(productTable, "STT");
                AddProductTableHeader(productTable, "Tên sản phẩm");
                AddProductTableHeader(productTable, "Số lượng");
                AddProductTableHeader(productTable, "Đơn giá");
                AddProductTableHeader(productTable, "Thành tiền");

                decimal tongTien = 0;
                int stt = 1;

                // Thêm xe
                string tenXe = giaoDich["TenXe"]?.ToString() ?? "";
                string bienSo = giaoDich["BienSo"]?.ToString();
                if (!string.IsNullOrEmpty(bienSo))
                    tenXe += $"\n(Biển số: {bienSo})";

                decimal giaXe = giaoDich["GiaBan"] != DBNull.Value ? Convert.ToDecimal(giaoDich["GiaBan"]) : 0;
                
                // Nếu có phụ tùng, giá xe = giá bán - tổng tiền phụ tùng
                decimal tongTienPhuTung = 0;
                if (chiTietPhuTung != null && chiTietPhuTung.Rows.Count > 0)
                {
                    foreach (DataRow row in chiTietPhuTung.Rows)
                    {
                        tongTienPhuTung += row["ThanhTien"] != DBNull.Value ? Convert.ToDecimal(row["ThanhTien"]) : 0;
                    }
                }
                
                decimal giaXeThucTe = giaXe - tongTienPhuTung;
                
                AddProductTableRow(productTable, stt.ToString(), tenXe, "1", giaXeThucTe.ToString("N0"), giaXeThucTe.ToString("N0"));
                tongTien += giaXeThucTe;
                stt++;

                // Thêm phụ tùng (nếu có)
                if (chiTietPhuTung != null && chiTietPhuTung.Rows.Count > 0)
                {
                    foreach (DataRow row in chiTietPhuTung.Rows)
                    {
                        string tenPhuTung = row["TenPhuTung"]?.ToString() ?? "";
                        int soLuong = row["SoLuong"] != DBNull.Value ? Convert.ToInt32(row["SoLuong"]) : 0;
                        decimal donGia = row["DonGia"] != DBNull.Value ? Convert.ToDecimal(row["DonGia"]) : 0;
                        decimal thanhTien = row["ThanhTien"] != DBNull.Value ? Convert.ToDecimal(row["ThanhTien"]) : 0;

                        AddProductTableRow(productTable, stt.ToString(), tenPhuTung, soLuong.ToString(), 
                            donGia.ToString("N0"), thanhTien.ToString("N0"));
                        tongTien += thanhTien;
                        stt++;
                    }
                }

                document.Add(productTable);

                // ==================== TỔNG TIỀN ====================
                // Lấy giá trị từ database
                decimal giaBanXe = giaoDich["GiaBan"] != DBNull.Value ? Convert.ToDecimal(giaoDich["GiaBan"]) : 0;
                decimal soTienGiamXe = giaoDich["SoTienGiam"] != DBNull.Value ? Convert.ToDecimal(giaoDich["SoTienGiam"]) : 0;
                decimal tongGiaPhuTung = giaoDich["TongGiaPhuTung"] != DBNull.Value ? Convert.ToDecimal(giaoDich["TongGiaPhuTung"]) : 0;
                decimal tongGiamPhuTung = giaoDich["TongGiamPhuTung"] != DBNull.Value ? Convert.ToDecimal(giaoDich["TongGiamPhuTung"]) : 0;
                decimal tongThanhToan = giaoDich["TongThanhToan"] != DBNull.Value ? Convert.ToDecimal(giaoDich["TongThanhToan"]) : 0;
                
                // Tính tạm tính (trước khi giảm)
                decimal tamTinh = giaBanXe + tongGiaPhuTung;
                decimal tongGiam = soTienGiamXe + tongGiamPhuTung;

                PdfPTable tongTienTable = new PdfPTable(2);
                tongTienTable.WidthPercentage = 100;
                tongTienTable.SetWidths(new float[] { 70f, 30f });
                tongTienTable.SpacingBefore = 10f;
                tongTienTable.SpacingAfter = 5f;

                // Tạm tính
                PdfPCell tamTinhLabelCell = new PdfPCell(new Phrase("Tạm tính:", new Font(baseFont, 11, Font.NORMAL, BaseColor.BLACK)));
                tamTinhLabelCell.Border = Rectangle.NO_BORDER;
                tamTinhLabelCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tamTinhLabelCell.PaddingRight = 10;
                tamTinhLabelCell.PaddingBottom = 5;
                tongTienTable.AddCell(tamTinhLabelCell);

                PdfPCell tamTinhValueCell = new PdfPCell(new Phrase(tamTinh.ToString("N0") + " VNĐ", new Font(baseFont, 11, Font.NORMAL, BaseColor.BLACK)));
                tamTinhValueCell.Border = Rectangle.NO_BORDER;
                tamTinhValueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tamTinhValueCell.PaddingBottom = 5;
                tongTienTable.AddCell(tamTinhValueCell);

                // Giảm giá (nếu có)
                if (tongGiam > 0)
                {
                    // Hiển thị tên khuyến mãi nếu có
                    string labelGiamGia = "Giảm giá (khuyến mãi):";
                    if (giaoDich.Table.Columns.Contains("TenKM") && giaoDich["TenKM"] != DBNull.Value && !string.IsNullOrEmpty(giaoDich["TenKM"].ToString()))
                    {
                        labelGiamGia = $"Giảm giá ({giaoDich["TenKM"]}):";
                    }

                    PdfPCell giamGiaLabelCell = new PdfPCell(new Phrase(labelGiamGia, new Font(baseFont, 11, Font.NORMAL, new BaseColor(255, 69, 0))));
                    giamGiaLabelCell.Border = Rectangle.NO_BORDER;
                    giamGiaLabelCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    giamGiaLabelCell.PaddingRight = 10;
                    giamGiaLabelCell.PaddingBottom = 5;
                    tongTienTable.AddCell(giamGiaLabelCell);

                    PdfPCell giamGiaValueCell = new PdfPCell(new Phrase("- " + tongGiam.ToString("N0") + " VNĐ", new Font(baseFont, 11, Font.BOLD, new BaseColor(255, 69, 0))));
                    giamGiaValueCell.Border = Rectangle.NO_BORDER;
                    giamGiaValueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    giamGiaValueCell.PaddingBottom = 5;
                    tongTienTable.AddCell(giamGiaValueCell);
                }

                // Đường kẻ ngang
                PdfPCell lineCell1 = new PdfPCell();
                lineCell1.Border = Rectangle.NO_BORDER;
                lineCell1.Colspan = 2;
                lineCell1.FixedHeight = 0f;
                lineCell1.BorderWidthBottom = 1f;
                lineCell1.BorderColorBottom = BaseColor.LIGHT_GRAY;
                tongTienTable.AddCell(lineCell1);

                // Thành tiền (tổng cộng)
                PdfPCell thanhTienLabelCell = new PdfPCell(new Phrase("THÀNH TIỀN:", new Font(baseFont, 12, Font.BOLD, BaseColor.BLACK)));
                thanhTienLabelCell.Border = Rectangle.NO_BORDER;
                thanhTienLabelCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                thanhTienLabelCell.PaddingRight = 10;
                thanhTienLabelCell.PaddingTop = 5;
                tongTienTable.AddCell(thanhTienLabelCell);

                PdfPCell thanhTienValueCell = new PdfPCell(new Phrase(tongThanhToan.ToString("N0") + " VNĐ", new Font(baseFont, 12, Font.BOLD, new BaseColor(220, 20, 60))));
                thanhTienValueCell.Border = Rectangle.BOX;
                thanhTienValueCell.BorderWidth = 1f;
                thanhTienValueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                thanhTienValueCell.Padding = 5;
                thanhTienValueCell.BackgroundColor = new BaseColor(255, 250, 205);
                tongTienTable.AddCell(thanhTienValueCell);

                document.Add(tongTienTable);

                // Số tiền bằng chữ
                string tienBangChu = ConvertNumberToWords((long)tongThanhToan);
                Paragraph bangChu = new Paragraph($"Bằng chữ: {tienBangChu}", new Font(baseFont, 10, Font.ITALIC, BaseColor.BLACK));
                bangChu.SpacingAfter = 10f;
                document.Add(bangChu);

                // ==================== THÔNG TIN THANH TOÁN ====================
                Paragraph thanhToanTitle = new Paragraph("THÔNG TIN THANH TOÁN", sectionTitleFont);
                thanhToanTitle.SpacingBefore = 10f;
                thanhToanTitle.SpacingAfter = 8f;
                document.Add(thanhToanTitle);

                PdfPTable thanhToanTable = new PdfPTable(2);
                thanhToanTable.WidthPercentage = 100;
                thanhToanTable.SetWidths(new float[] { 30f, 70f });
                thanhToanTable.SpacingAfter = 15f;

                AddInvoiceInfoRow(thanhToanTable, "Hình thức thanh toán:", giaoDich["HinhThucThanhToan"]?.ToString() ?? "");
                AddInvoiceInfoRow(thanhToanTable, "Trạng thái:", giaoDich["TrangThaiThanhToan"]?.ToString() ?? "");
                AddInvoiceInfoRow(thanhToanTable, "Nhân viên bán hàng:", giaoDich["TenNhanVien"]?.ToString() ?? "");

                document.Add(thanhToanTable);

                // ==================== CHỮ KÝ ====================
                document.Add(new Paragraph("\n"));

                PdfPTable signTable = new PdfPTable(3);
                signTable.WidthPercentage = 100;
                signTable.SetWidths(new float[] { 33f, 34f, 33f });
                signTable.SpacingBefore = 20f;

                // Người mua hàng
                PdfPCell signCell1 = new PdfPCell();
                signCell1.Border = Rectangle.NO_BORDER;
                signCell1.HorizontalAlignment = Element.ALIGN_CENTER;
                Paragraph nguoiMua = new Paragraph("NGƯỜI MUA HÀNG", boldFont);
                nguoiMua.Alignment = Element.ALIGN_CENTER;
                signCell1.AddElement(nguoiMua);
                signCell1.AddElement(new Paragraph("(Ký và ghi rõ họ tên)", smallFont) { Alignment = Element.ALIGN_CENTER });
                signCell1.AddElement(new Paragraph("\n\n\n", normalFont));
                signCell1.AddElement(new Paragraph(giaoDich["HoTenKH"]?.ToString() ?? "", normalFont) { Alignment = Element.ALIGN_CENTER });
                signTable.AddCell(signCell1);

                // Người bán hàng
                PdfPCell signCell2 = new PdfPCell();
                signCell2.Border = Rectangle.NO_BORDER;
                signCell2.HorizontalAlignment = Element.ALIGN_CENTER;
                Paragraph nguoiBan = new Paragraph("NGƯỜI BÁN HÀNG", boldFont);
                nguoiBan.Alignment = Element.ALIGN_CENTER;
                signCell2.AddElement(nguoiBan);
                signCell2.AddElement(new Paragraph("(Ký và ghi rõ họ tên)", smallFont) { Alignment = Element.ALIGN_CENTER });
                signCell2.AddElement(new Paragraph("\n\n\n", normalFont));
                signCell2.AddElement(new Paragraph(giaoDich["TenNhanVien"]?.ToString() ?? "", normalFont) { Alignment = Element.ALIGN_CENTER });
                signTable.AddCell(signCell2);

                // Thủ quỹ
                PdfPCell signCell3 = new PdfPCell();
                signCell3.Border = Rectangle.NO_BORDER;
                signCell3.HorizontalAlignment = Element.ALIGN_CENTER;
                Paragraph thuQuy = new Paragraph("THỦ QUỸ", boldFont);
                thuQuy.Alignment = Element.ALIGN_CENTER;
                signCell3.AddElement(thuQuy);
                signCell3.AddElement(new Paragraph("(Ký và ghi rõ họ tên)", smallFont) { Alignment = Element.ALIGN_CENTER });
                signCell3.AddElement(new Paragraph("\n\n\n", normalFont));
                signCell3.AddElement(new Paragraph("_______________", normalFont) { Alignment = Element.ALIGN_CENTER });
                signTable.AddCell(signCell3);

                document.Add(signTable);

                // ==================== FOOTER ====================
                document.Add(new Paragraph("\n"));
                PdfPTable footerLineTable = new PdfPTable(1);
                footerLineTable.WidthPercentage = 100;
                PdfPCell footerLineCell = new PdfPCell();
                footerLineCell.Border = Rectangle.BOTTOM_BORDER;
                footerLineCell.BorderWidth = 0.5f;
                footerLineCell.BorderColor = BaseColor.LIGHT_GRAY;
                footerLineCell.FixedHeight = 0f;
                footerLineTable.AddCell(footerLineCell);
                document.Add(footerLineTable);

                Paragraph footer = new Paragraph(
                    "Cảm ơn quý khách đã mua hàng tại cửa hàng!\n" +
                    "Mọi thắc mắc xin liên hệ: 0123.456.789 hoặc email: info@cuahangxemay.vn",
                    smallFont
                );
                footer.Alignment = Element.ALIGN_CENTER;
                footer.SpacingBefore = 5f;
                document.Add(footer);

            }
            finally
            {
                document.Close();
            }
        }

        // Xuất danh sách hợp đồng thuê ra file PDF
        public static void ExportDanhSachHopDongThue(DataTable data, string filePath)
        {
            Document document = new Document(PageSize.A4.Rotate(), 25, 25, 30, 30);

            try
            {
                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();

                // Tiêu đề
                Paragraph title = new Paragraph("DANH SÁCH HỢP ĐỒNG THUÊ XE MÁY", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 10f;
                document.Add(title);

                // Ngày xuất
                Paragraph date = new Paragraph($"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", smallFont);
                date.Alignment = Element.ALIGN_RIGHT;
                date.SpacingAfter = 20f;
                document.Add(date);

                // Tạo bảng
                PdfPTable table = new PdfPTable(10);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 5f, 7f, 10f, 10f, 15f, 10f, 10f, 10f, 10f, 10f });

                // Header
                AddTableHeader(table, "Mã HĐ");
                AddTableHeader(table, "Mã GD");
                AddTableHeader(table, "Mã KH");
                AddTableHeader(table, "Họ tên KH");
                AddTableHeader(table, "Biển số");
                AddTableHeader(table, "Ngày lập");
                AddTableHeader(table, "Ngày bắt đầu");
                AddTableHeader(table, "Ngày kết thúc");
                AddTableHeader(table, "Giá thuê");
                AddTableHeader(table, "Trạng thái");

                // Data rows
                foreach (DataRow row in data.Rows)
                {
                    AddTableCell(table, row["MaHDT"]?.ToString() ?? "");
                    AddTableCell(table, row["MaGDThue"]?.ToString() ?? "");
                    AddTableCell(table, row["MaKH"]?.ToString() ?? "");
                    AddTableCell(table, row["HoTenKH"]?.ToString() ?? "");
                    AddTableCell(table, row["BienSo"]?.ToString() ?? "");
                    AddTableCell(table, row["NgayLap"] != DBNull.Value
                        ? Convert.ToDateTime(row["NgayLap"]).ToString("dd/MM/yyyy")
                        : "");
                    AddTableCell(table, row["NgayBatDau"] != DBNull.Value
                        ? Convert.ToDateTime(row["NgayBatDau"]).ToString("dd/MM/yyyy")
                        : "");
                    AddTableCell(table, row["NgayKetThuc"] != DBNull.Value
                        ? Convert.ToDateTime(row["NgayKetThuc"]).ToString("dd/MM/yyyy")
                        : "");
                    AddTableCell(table, row["GiaThue"] != DBNull.Value
                        ? Convert.ToDecimal(row["GiaThue"]).ToString("N0")
                        : "");
                    AddTableCell(table, row["TrangThaiHopDong"]?.ToString() ?? "");
                }

                document.Add(table);

                // Footer
                Paragraph footer = new Paragraph($"\nTổng số hợp đồng: {data.Rows.Count}", boldFont);
                footer.SpacingBefore = 20f;
                document.Add(footer);
            }
            finally
            {
                document.Close();
            }
        }

        /// Xuất chi tiết hợp đồng thuê ra file PDF
        public static void ExportChiTietHopDongThue(DataRow hopDong, string filePath)
        {
            Document document = new Document(PageSize.A4, 40, 40, 30, 30);

            try
            {
                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();

                // ==================== HEADER ====================
                PdfPTable headerTable = new PdfPTable(2);
                headerTable.WidthPercentage = 100;
                headerTable.SetWidths(new float[] { 50f, 50f });
                headerTable.SpacingAfter = 15f;

                // Bên trái - Thông tin cửa hàng
                PdfPCell leftCell = new PdfPCell();
                leftCell.Border = Rectangle.NO_BORDER;
                Font shopNameFont = new Font(baseFont, 16, Font.BOLD, new BaseColor(0, 102, 204));
                //leftCell.AddElement(new Paragraph("CỬA HÀNG ", shopNameFont));
                //leftCell.AddElement(new Paragraph("Địa chỉ: ", smallFont));
                //leftCell.AddElement(new Paragraph("Điện thoại:  | Hotline: ", smallFont));
                //leftCell.AddElement(new Paragraph("Email: ", smallFont));
                //leftCell.AddElement(new Paragraph("MST: ", smallFont));
                leftCell.PaddingTop = 5;
                headerTable.AddCell(leftCell);

                // Bên phải - Mã hợp đồng và ngày lập
                PdfPCell rightCell = new PdfPCell();
                rightCell.Border = Rectangle.NO_BORDER;
                //rightCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                //rightCell.AddElement(new Paragraph($"Số hợp đồng: {hopDong["MaHDT"]}", boldFont));
                //rightCell.AddElement(new Paragraph($"Mã GD: {hopDong["MaGDThue"]}", normalFont));
                //rightCell.AddElement(new Paragraph($"Ngày lập: {Convert.ToDateTime(hopDong["NgayLap"]):dd/MM/yyyy}", normalFont));
                //rightCell.AddElement(new Paragraph($"Giờ: {Convert.ToDateTime(hopDong["NgayLap"]):HH:mm:ss}", smallFont));
                rightCell.PaddingTop = 5;
                headerTable.AddCell(rightCell);

                document.Add(headerTable);

                // Đường kẻ ngang
                PdfPTable lineTable = new PdfPTable(1);
                lineTable.WidthPercentage = 100;
                lineTable.SpacingAfter = 10f;
                PdfPCell lineCell = new PdfPCell();
                lineCell.Border = Rectangle.BOTTOM_BORDER;
                lineCell.BorderWidth = 1.5f;
                lineCell.BorderColor = BaseColor.GRAY;
                lineCell.FixedHeight = 0f;
                lineTable.AddCell(lineCell);
                document.Add(lineTable);

                // ==================== TIÊU ĐỀ ====================
                Font titleBigFont = new Font(baseFont, 20, Font.BOLD, new BaseColor(0, 51, 153));
                Paragraph title = new Paragraph("HỢP ĐỒNG CHO THUÊ XE MÁY", titleBigFont);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 10f;
                document.Add(title);

                // Căn cứ pháp lý
                Font italicFont = new Font(baseFont, 10, Font.ITALIC, BaseColor.BLACK);
                Paragraph legal = new Paragraph("Căn cứ Bộ luật Dân sự số 91/2015/QH13", italicFont);
                legal.Alignment = Element.ALIGN_CENTER;
                legal.SpacingAfter = 15f;
                document.Add(legal);

                // ==================== BÊN CHO THUÊ (BÊN A) ====================
                Font sectionTitleFont = new Font(baseFont, 13, Font.BOLD, new BaseColor(0, 102, 204));
                Paragraph benChoThue = new Paragraph("BÊN CHO THUÊ (BÊN A):", sectionTitleFont);
                benChoThue.SpacingBefore = 10f;
                benChoThue.SpacingAfter = 8f;
                document.Add(benChoThue);

                // Bảng thông tin bên cho thuê
                PdfPTable benChoThueTable = new PdfPTable(2);
                benChoThueTable.WidthPercentage = 100;
                benChoThueTable.SetWidths(new float[] { 30f, 70f });
                benChoThueTable.SpacingAfter = 10f;

                AddSimpleInfoRow(benChoThueTable, "Tên cửa hàng:", "CỬA HÀNG XE MÁY ABC");
                AddSimpleInfoRow(benChoThueTable, "Địa chỉ:", "123 Đường Lê Lợi, Quận 1, TP.HCM");
                AddSimpleInfoRow(benChoThueTable, "Đại diện:", hopDong["TenNhanVien"]?.ToString() ?? "Giám đốc");
                AddSimpleInfoRow(benChoThueTable, "Điện thoại:", "0123.456.789");
                AddSimpleInfoRow(benChoThueTable, "MST:", "0123456789");
                document.Add(benChoThueTable);

                // ==================== BÊN THUÊ (BÊN B) ====================
                Paragraph benThue = new Paragraph("BÊN THUÊ (BÊN B):", sectionTitleFont);
                benThue.SpacingBefore = 15f;
                benThue.SpacingAfter = 8f;
                document.Add(benThue);

                PdfPTable benThueTable = new PdfPTable(2);
                benThueTable.WidthPercentage = 100;
                benThueTable.SetWidths(new float[] { 30f, 70f });
                benThueTable.SpacingAfter = 10f;

                string tenKH = hopDong["HoTenKH"]?.ToString() ?? "";
                string sdtKH = hopDong["Sdt"]?.ToString() ?? "";
                string khachHangInfo = $"{tenKH}, SĐT: {sdtKH}";
                
                AddSimpleInfoRow(benThueTable, "Khách hàng:", khachHangInfo);
                AddSimpleInfoRow(benThueTable, "CCCD/CMND:", hopDong["SoCCCD"]?.ToString() ?? "");
                AddSimpleInfoRow(benThueTable, "Địa chỉ:", hopDong["DiaChi"]?.ToString() ?? "");
                AddSimpleInfoRow(benThueTable, "Mã khách hàng:", hopDong["MaKH"]?.ToString() ?? "");
                document.Add(benThueTable);

                // ==================== THÔNG TIN XE MÁY ====================
                Paragraph thongTinXe = new Paragraph("THÔNG TIN XE MÁY:", sectionTitleFont);
                thongTinXe.SpacingBefore = 15f;
                thongTinXe.SpacingAfter = 8f;
                document.Add(thongTinXe);

                // Tạo bảng 2 cột cho thông tin xe
                PdfPTable xeTable = new PdfPTable(2);
                xeTable.WidthPercentage = 100;
                xeTable.SetWidths(new float[] { 30f, 70f });
                xeTable.SpacingAfter = 10f;

                AddSimpleInfoRow(xeTable, "Tên xe:", hopDong["TenXe"]?.ToString() ?? "");
                AddSimpleInfoRow(xeTable, "Biển số:", hopDong["BienSo"]?.ToString() ?? "Chưa đăng ký");
                AddSimpleInfoRow(xeTable, "Hãng xe:", hopDong["TenHangXe"]?.ToString() ?? "");
                AddSimpleInfoRow(xeTable, "Dòng xe:", hopDong["TenDongXe"]?.ToString() ?? "");
                AddSimpleInfoRow(xeTable, "Màu sắc:", hopDong["TenMauSac"]?.ToString() ?? "");
                AddSimpleInfoRow(xeTable, "Phân khối:", (hopDong["PhanKhoi"] != DBNull.Value ? hopDong["PhanKhoi"].ToString() + " cc" : ""));
                AddSimpleInfoRow(xeTable, "Loại xe:", hopDong["LoaiXe"]?.ToString() ?? "");
                AddSimpleInfoRow(xeTable, "Thông tin xăng:", hopDong["ThongTinXang"]?.ToString() ?? "");
                AddSimpleInfoRow(xeTable, "Năm sản xuất:", hopDong["NamSanXuat"]?.ToString() ?? "");
                
                document.Add(xeTable);

                // ==================== THỜI GIAN THUÊ ====================
                Paragraph thoiGianThue = new Paragraph("THỜI GIAN VÀ GIÁ THUÊ:", sectionTitleFont);
                thoiGianThue.SpacingBefore = 15f;
                thoiGianThue.SpacingAfter = 8f;
                document.Add(thoiGianThue);

                PdfPTable thoiGianTable = new PdfPTable(2);
                thoiGianTable.WidthPercentage = 100;
                thoiGianTable.SetWidths(new float[] { 30f, 70f });
                thoiGianTable.SpacingAfter = 10f;

                DateTime ngayBatDau = Convert.ToDateTime(hopDong["NgayBatDau"]);
                DateTime ngayKetThuc = Convert.ToDateTime(hopDong["NgayKetThuc"]);
                int soNgayThue = (ngayKetThuc - ngayBatDau).Days;

                AddSimpleInfoRow(thoiGianTable, "Ngày bắt đầu:", ngayBatDau.ToString("dd/MM/yyyy"));
                AddSimpleInfoRow(thoiGianTable, "Ngày kết thúc:", ngayKetThuc.ToString("dd/MM/yyyy"));
                AddSimpleInfoRow(thoiGianTable, "Số ngày thuê:", soNgayThue.ToString() + " ngày");
                AddSimpleInfoRow(thoiGianTable, "Giá thuê:", Convert.ToDecimal(hopDong["GiaThue"]).ToString("N0") + " VNĐ");

                decimal tienDatCoc = hopDong["TienDatCoc"] != DBNull.Value 
                    ? Convert.ToDecimal(hopDong["TienDatCoc"]) : 0;
                AddSimpleInfoRow(thoiGianTable, "Tiền đặt cọc:", tienDatCoc.ToString("N0") + " VNĐ");

                document.Add(thoiGianTable);

                // ==================== ĐIỀU KHOẢN HỢP ĐỒNG ====================
                Paragraph dieuKhoan = new Paragraph("ĐIỀU KHOẢN HỢP ĐỒNG:", sectionTitleFont);
                dieuKhoan.SpacingBefore = 15f;
                dieuKhoan.SpacingAfter = 8f;
                document.Add(dieuKhoan);

                string dieuKhoanText = hopDong["DieuKhoan"]?.ToString();
                if (string.IsNullOrEmpty(dieuKhoanText))
                {
                    // Điều khoản mặc định cho thuê xe
                    dieuKhoanText =
                        "Điều 1: Bên A cam kết xe máy nêu trên là tài sản hợp pháp, đang hoạt động tốt, có đầy đủ giấy tờ.\n\n" +
                        "Điều 2: Bên B cam kết sử dụng xe đúng mục đích, tuân thủ luật giao thông, không cho người khác thuê lại.\n\n" +
                        "Điều 3: Bên B phải trả xe đúng hạn, nếu trả trễ sẽ tính phí 150% giá thuê/ngày cho mỗi ngày trễ hạn.\n\n" +
                        "Điều 4: Bên B chịu trách nhiệm bồi thường 100% giá trị xe nếu xe bị mất cắp hoặc hư hỏng nặng.\n\n" +
                        "Điều 5: Xe phải trả về với đủ xăng như lúc nhận và trong tình trạng tốt.\n\n" +
                        "Điều 6: Hai bên cam kết thực hiện đúng các điều khoản đã thỏa thuận. Nếu có tranh chấp, hai bên sẽ giải quyết trên tinh thần hòa giải.\n\n" +
                        "Điều 7: Hợp đồng có hiệu lực kể từ ngày ký và được lập thành 02 bản có giá trị pháp lý như nhau, mỗi bên giữ 01 bản.";
                }

                Paragraph dieuKhoanContent = new Paragraph(dieuKhoanText, normalFont);
                dieuKhoanContent.Alignment = Element.ALIGN_JUSTIFIED;
                dieuKhoanContent.Leading = 16f;
                document.Add(dieuKhoanContent);

                // Ghi chú (nếu có)
                if (!string.IsNullOrEmpty(hopDong["GhiChu"]?.ToString()))
                {
                    Paragraph ghiChu = new Paragraph("\nGHI CHÚ:", boldFont);
                    ghiChu.SpacingBefore = 10f;
                    document.Add(ghiChu);
                    document.Add(new Paragraph(hopDong["GhiChu"].ToString(), normalFont));
                }

                // ==================== CHỮ KÝ ====================
                document.Add(new Paragraph("\n\n"));

                PdfPTable signTable = new PdfPTable(2);
                signTable.WidthPercentage = 100;
                signTable.SetWidths(new float[] { 50f, 50f });
                signTable.SpacingBefore = 20f;

                // Bên cho thuê
                PdfPCell signCell1 = new PdfPCell();
                signCell1.Border = Rectangle.NO_BORDER;
                signCell1.HorizontalAlignment = Element.ALIGN_CENTER;

                Paragraph dai_dien_a = new Paragraph("ĐẠI DIỆN BÊN CHO THUÊ (BÊN A)", boldFont);
                dai_dien_a.Alignment = Element.ALIGN_CENTER;
                signCell1.AddElement(dai_dien_a);

                Paragraph ky_ten_a = new Paragraph("(Ký, ghi rõ họ tên và đóng dấu)", smallFont);
                ky_ten_a.Alignment = Element.ALIGN_CENTER;
                signCell1.AddElement(ky_ten_a);

                signCell1.AddElement(new Paragraph("\n\n\n\n", normalFont));

                Paragraph ten_a = new Paragraph(hopDong["TenNhanVien"]?.ToString() ?? "_______________", normalFont);
                ten_a.Alignment = Element.ALIGN_CENTER;
                signCell1.AddElement(ten_a);

                signTable.AddCell(signCell1);

                // Bên thuê
                PdfPCell signCell2 = new PdfPCell();
                signCell2.Border = Rectangle.NO_BORDER;
                signCell2.HorizontalAlignment = Element.ALIGN_CENTER;

                Paragraph dai_dien_b = new Paragraph("ĐẠI DIỆN BÊN THUÊ (BÊN B)", boldFont);
                dai_dien_b.Alignment = Element.ALIGN_CENTER;
                signCell2.AddElement(dai_dien_b);

                Paragraph ky_ten_b = new Paragraph("(Ký và ghi rõ họ tên)", smallFont);
                ky_ten_b.Alignment = Element.ALIGN_CENTER;
                signCell2.AddElement(ky_ten_b);

                signCell2.AddElement(new Paragraph("\n\n\n\n", normalFont));

                Paragraph ten_b = new Paragraph(hopDong["HoTenKH"]?.ToString() ?? "_______________", normalFont);
                ten_b.Alignment = Element.ALIGN_CENTER;
                signCell2.AddElement(ten_b);

                signTable.AddCell(signCell2);

                document.Add(signTable);

                // ==================== FOOTER ====================
                document.Add(new Paragraph("\n"));
                PdfPTable footerLineTable = new PdfPTable(1);
                footerLineTable.WidthPercentage = 100;
                PdfPCell footerLineCell = new PdfPCell();
                footerLineCell.Border = Rectangle.BOTTOM_BORDER;
                footerLineCell.BorderWidth = 0.5f;
                footerLineCell.BorderColor = BaseColor.LIGHT_GRAY;
                footerLineCell.FixedHeight = 0f;
                footerLineTable.AddCell(footerLineCell);
                document.Add(footerLineTable);

                Paragraph footer = new Paragraph(
                    $"Hợp đồng được lập lúc {Convert.ToDateTime(hopDong["NgayLap"]):HH:mm:ss} ngày {Convert.ToDateTime(hopDong["NgayLap"]):dd/MM/yyyy} - " +
                    $"Trạng thái: {hopDong["TrangThaiHopDong"]}",
                    smallFont
                );
                footer.Alignment = Element.ALIGN_CENTER;
                footer.SpacingBefore = 5f;
                document.Add(footer);

            }
            finally
            {
                document.Close();
            }
        }

        /// Xuất hóa đơn thuê xe
        public static void ExportHoaDonThueXe(DataRow giaoDich, string filePath)
        {
            Document document = new Document(PageSize.A4, 40, 40, 30, 30);

            try
            {
                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();

                // ==================== HEADER ====================
                PdfPTable headerTable = new PdfPTable(2);
                headerTable.WidthPercentage = 100;
                headerTable.SetWidths(new float[] { 50f, 50f });
                headerTable.SpacingAfter = 15f;

                // Bên trái - Thông tin cửa hàng
                PdfPCell leftCell = new PdfPCell();
                leftCell.Border = Rectangle.NO_BORDER;
                Font shopNameFont = new Font(baseFont, 16, Font.BOLD, new BaseColor(0, 102, 204));
                leftCell.AddElement(new Paragraph("CỬA HÀNG XE MÁY WINGS MOTORBIKE", shopNameFont));
                leftCell.AddElement(new Paragraph("Địa chỉ: 450-451 Lê Văn Việt, Phường Tăng Nhơn Phú, TP.Hồ Chí Minh", smallFont));
                leftCell.AddElement(new Paragraph("Điện thoại: 0386624219 ", smallFont));
                leftCell.AddElement(new Paragraph("Email: Vothienlinh2@gmail.com", smallFont));
                leftCell.AddElement(new Paragraph("MST: 0123456789", smallFont));
                leftCell.PaddingTop = 5;
                headerTable.AddCell(leftCell);

                // Bên phải - Mã hóa đơn và ngày
                PdfPCell rightCell = new PdfPCell();
                rightCell.Border = Rectangle.NO_BORDER;
                rightCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                rightCell.AddElement(new Paragraph($"HÓA ĐƠN THUÊ XE", new Font(baseFont, 14, Font.BOLD, BaseColor.RED)));
                rightCell.AddElement(new Paragraph($"Số: {giaoDich["MaGDThue"]}", boldFont));
                rightCell.AddElement(new Paragraph($"Ngày: {Convert.ToDateTime(giaoDich["NgayBatDau"]):dd/MM/yyyy}", normalFont));
                rightCell.AddElement(new Paragraph($"Giờ: {DateTime.Now:HH:mm:ss}", smallFont));
                rightCell.PaddingTop = 5;
                headerTable.AddCell(rightCell);

                document.Add(headerTable);

                // Đường kẻ ngang
                PdfPTable lineTable = new PdfPTable(1);
                lineTable.WidthPercentage = 100;
                lineTable.SpacingAfter = 10f;
                PdfPCell lineCell = new PdfPCell();
                lineCell.Border = Rectangle.BOTTOM_BORDER;
                lineCell.BorderWidth = 1.5f;
                lineCell.BorderColor = BaseColor.GRAY;
                lineCell.FixedHeight = 0f;
                lineTable.AddCell(lineCell);
                document.Add(lineTable);

                // ==================== THÔNG TIN KHÁCH HÀNG ====================
                Font sectionTitleFont = new Font(baseFont, 12, Font.BOLD, new BaseColor(0, 102, 204));
                Paragraph khachHangTitle = new Paragraph("THÔNG TIN KHÁCH HÀNG", sectionTitleFont);
                khachHangTitle.SpacingBefore = 5f;
                khachHangTitle.SpacingAfter = 8f;
                document.Add(khachHangTitle);

                PdfPTable khachHangTable = new PdfPTable(2);
                khachHangTable.WidthPercentage = 100;
                khachHangTable.SetWidths(new float[] { 30f, 70f });
                khachHangTable.SpacingAfter = 10f;

                AddInvoiceInfoRow(khachHangTable, "Họ tên:", giaoDich["HoTenKH"]?.ToString() ?? "");
                AddInvoiceInfoRow(khachHangTable, "Mã KH:", giaoDich["MaKH"]?.ToString() ?? "");
                AddInvoiceInfoRow(khachHangTable, "Số điện thoại:", giaoDich["SdtKhachHang"]?.ToString() ?? "");
                
                if (giaoDich.Table.Columns.Contains("EmailKhachHang") && giaoDich["EmailKhachHang"] != DBNull.Value)
                    AddInvoiceInfoRow(khachHangTable, "Email:", giaoDich["EmailKhachHang"].ToString());
                
                if (giaoDich.Table.Columns.Contains("DiaChiKhachHang") && giaoDich["DiaChiKhachHang"] != DBNull.Value)
                    AddInvoiceInfoRow(khachHangTable, "Địa chỉ:", giaoDich["DiaChiKhachHang"].ToString());
                
                document.Add(khachHangTable);

                // ==================== THÔNG TIN THUÊ XE ====================
                Paragraph thueXeTitle = new Paragraph("THÔNG TIN THUÊ XE", sectionTitleFont);
                thueXeTitle.SpacingBefore = 10f;
                thueXeTitle.SpacingAfter = 8f;
                document.Add(thueXeTitle);

                PdfPTable thueXeTable = new PdfPTable(2);
                thueXeTable.WidthPercentage = 100;
                thueXeTable.SetWidths(new float[] { 30f, 70f });
                thueXeTable.SpacingAfter = 10f;

                string tenXe = giaoDich["TenXe"]?.ToString() ?? "";
                string bienSo = giaoDich["BienSo"]?.ToString();
                if (!string.IsNullOrEmpty(bienSo))
                    tenXe += $" (Biển số: {bienSo})";

                AddInvoiceInfoRow(thueXeTable, "Xe thuê:", tenXe);

                DateTime ngayBatDau = Convert.ToDateTime(giaoDich["NgayBatDau"]);
                DateTime ngayKetThuc = Convert.ToDateTime(giaoDich["NgayKetThuc"]);
                int soNgayThue = (ngayKetThuc - ngayBatDau).Days;

                AddInvoiceInfoRow(thueXeTable, "Từ ngày:", ngayBatDau.ToString("dd/MM/yyyy"));
                AddInvoiceInfoRow(thueXeTable, "Đến ngày:", ngayKetThuc.ToString("dd/MM/yyyy"));
                AddInvoiceInfoRow(thueXeTable, "Số ngày thuê:", soNgayThue.ToString() + " ngày");

                document.Add(thueXeTable);

                // ==================== CHI TIẾT THANH TOÁN ====================
                Paragraph chiTietTitle = new Paragraph("CHI TIẾT THANH TOÁN", sectionTitleFont);
                chiTietTitle.SpacingBefore = 10f;
                chiTietTitle.SpacingAfter = 8f;
                document.Add(chiTietTitle);

                // Bảng chi tiết
                PdfPTable productTable = new PdfPTable(4);
                productTable.WidthPercentage = 100;
                productTable.SetWidths(new float[] { 40f, 20f, 20f, 20f });
                productTable.SpacingAfter = 10f;

                // Header bảng
                AddProductTableHeader(productTable, "Nội dung");
                AddProductTableHeader(productTable, "Đơn giá");
                AddProductTableHeader(productTable, "Số lượng");
                AddProductTableHeader(productTable, "Thành tiền");

                decimal giaThueNgay = giaoDich["GiaThueNgay"] != DBNull.Value 
                    ? Convert.ToDecimal(giaoDich["GiaThueNgay"]) : 0;
                decimal tongGia = giaThueNgay * soNgayThue;

                // Thêm dòng thuê xe
                PdfPCell contentCell = new PdfPCell(new Phrase("Thuê xe máy", normalFont));
                contentCell.HorizontalAlignment = Element.ALIGN_LEFT;
                contentCell.Padding = 5;
                contentCell.BorderColor = BaseColor.LIGHT_GRAY;
                productTable.AddCell(contentCell);

                PdfPCell priceCell = new PdfPCell(new Phrase(giaThueNgay.ToString("N0") + " VNĐ/ngày", normalFont));
                priceCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                priceCell.Padding = 5;
                priceCell.BorderColor = BaseColor.LIGHT_GRAY;
                productTable.AddCell(priceCell);

                PdfPCell qtyCell = new PdfPCell(new Phrase(soNgayThue.ToString(), normalFont));
                qtyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                qtyCell.Padding = 5;
                qtyCell.BorderColor = BaseColor.LIGHT_GRAY;
                productTable.AddCell(qtyCell);

                PdfPCell totalCell = new PdfPCell(new Phrase(tongGia.ToString("N0") + " VNĐ", normalFont));
                totalCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                totalCell.Padding = 5;
                totalCell.BorderColor = BaseColor.LIGHT_GRAY;
                productTable.AddCell(totalCell);

                document.Add(productTable);

                // ==================== TỔNG TIỀN ====================
                decimal tongTienTruocGiam = giaoDich["TongTienTruocGiam"] != DBNull.Value 
                    ? Convert.ToDecimal(giaoDich["TongTienTruocGiam"]) : tongGia;
                decimal soTienGiam = giaoDich["SoTienGiam"] != DBNull.Value 
                    ? Convert.ToDecimal(giaoDich["SoTienGiam"]) : 0;
                decimal tongThanhToan = giaoDich["TongThanhToan"] != DBNull.Value 
                    ? Convert.ToDecimal(giaoDich["TongThanhToan"]) : tongGia;
                decimal soTienCoc = giaoDich["SoTienCoc"] != DBNull.Value 
                    ? Convert.ToDecimal(giaoDich["SoTienCoc"]) : 0;

                PdfPTable tongTienTable = new PdfPTable(2);
                tongTienTable.WidthPercentage = 100;
                tongTienTable.SetWidths(new float[] { 70f, 30f });
                tongTienTable.SpacingBefore = 10f;
                tongTienTable.SpacingAfter = 5f;

                // Tạm tính
                PdfPCell tamTinhLabelCell = new PdfPCell(new Phrase("Tạm tính:", new Font(baseFont, 11, Font.NORMAL, BaseColor.BLACK)));
                tamTinhLabelCell.Border = Rectangle.NO_BORDER;
                tamTinhLabelCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tamTinhLabelCell.PaddingRight = 10;
                tamTinhLabelCell.PaddingBottom = 5;
                tongTienTable.AddCell(tamTinhLabelCell);

                PdfPCell tamTinhValueCell = new PdfPCell(new Phrase(tongTienTruocGiam.ToString("N0") + " VNĐ", new Font(baseFont, 11, Font.NORMAL, BaseColor.BLACK)));
                tamTinhValueCell.Border = Rectangle.NO_BORDER;
                tamTinhValueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tamTinhValueCell.PaddingBottom = 5;
                tongTienTable.AddCell(tamTinhValueCell);

                // Giảm giá (nếu có)
                if (soTienGiam > 0)
                {
                    string labelGiamGia = "Giảm giá (khuyến mãi):";
                    if (giaoDich.Table.Columns.Contains("TenKM") && giaoDich["TenKM"] != DBNull.Value && !string.IsNullOrEmpty(giaoDich["TenKM"].ToString()))
                    {
                        labelGiamGia = $"Giảm giá ({giaoDich["TenKM"]}):";
                    }

                    PdfPCell giamGiaLabelCell = new PdfPCell(new Phrase(labelGiamGia, new Font(baseFont, 11, Font.NORMAL, new BaseColor(255, 69, 0))));
                    giamGiaLabelCell.Border = Rectangle.NO_BORDER;
                    giamGiaLabelCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    giamGiaLabelCell.PaddingRight = 10;
                    giamGiaLabelCell.PaddingBottom = 5;
                    tongTienTable.AddCell(giamGiaLabelCell);

                    PdfPCell giamGiaValueCell = new PdfPCell(new Phrase("- " + soTienGiam.ToString("N0") + " VNĐ", new Font(baseFont, 11, Font.BOLD, new BaseColor(255, 69, 0))));
                    giamGiaValueCell.Border = Rectangle.NO_BORDER;
                    giamGiaValueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    giamGiaValueCell.PaddingBottom = 5;
                    tongTienTable.AddCell(giamGiaValueCell);
                }

                // Đường kẻ ngang
                PdfPCell lineCell1 = new PdfPCell();
                lineCell1.Border = Rectangle.NO_BORDER;
                lineCell1.Colspan = 2;
                lineCell1.FixedHeight = 0f;
                lineCell1.BorderWidthBottom = 1f;
                lineCell1.BorderColorBottom = BaseColor.LIGHT_GRAY;
                tongTienTable.AddCell(lineCell1);

                // Thành tiền (tổng cộng)
                PdfPCell thanhTienLabelCell = new PdfPCell(new Phrase("TỔNG TIỀN THUÊ:", new Font(baseFont, 12, Font.BOLD, BaseColor.BLACK)));
                thanhTienLabelCell.Border = Rectangle.NO_BORDER;
                thanhTienLabelCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                thanhTienLabelCell.PaddingRight = 10;
                thanhTienLabelCell.PaddingTop = 5;
                tongTienTable.AddCell(thanhTienLabelCell);

                PdfPCell thanhTienValueCell = new PdfPCell(new Phrase(tongThanhToan.ToString("N0") + " VNĐ", new Font(baseFont, 12, Font.BOLD, new BaseColor(220, 20, 60))));
                thanhTienValueCell.Border = Rectangle.BOX;
                thanhTienValueCell.BorderWidth = 1f;
                thanhTienValueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                thanhTienValueCell.Padding = 5;
                thanhTienValueCell.BackgroundColor = new BaseColor(255, 250, 205);
                tongTienTable.AddCell(thanhTienValueCell);

                // Tiền cọc
                PdfPCell cocLabelCell = new PdfPCell(new Phrase("Tiền đặt cọc:", new Font(baseFont, 11, Font.NORMAL, BaseColor.BLACK)));
                cocLabelCell.Border = Rectangle.NO_BORDER;
                cocLabelCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cocLabelCell.PaddingRight = 10;
                cocLabelCell.PaddingTop = 10;
                tongTienTable.AddCell(cocLabelCell);

                PdfPCell cocValueCell = new PdfPCell(new Phrase(soTienCoc.ToString("N0") + " VNĐ", new Font(baseFont, 11, Font.NORMAL, BaseColor.BLACK)));
                cocValueCell.Border = Rectangle.NO_BORDER;
                cocValueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cocValueCell.PaddingTop = 10;
                tongTienTable.AddCell(cocValueCell);

                document.Add(tongTienTable);

                // Số tiền bằng chữ
                string tienBangChu = ConvertNumberToWords((long)tongThanhToan);
                Paragraph bangChu = new Paragraph($"Bằng chữ: {tienBangChu}", new Font(baseFont, 10, Font.ITALIC, BaseColor.BLACK));
                bangChu.SpacingAfter = 10f;
                document.Add(bangChu);

                // ==================== THÔNG TIN THANH TOÁN ====================
                Paragraph thanhToanTitle = new Paragraph("THÔNG TIN THANH TOÁN", sectionTitleFont);
                thanhToanTitle.SpacingBefore = 10f;
                thanhToanTitle.SpacingAfter = 8f;
                document.Add(thanhToanTitle);

                PdfPTable thanhToanTable = new PdfPTable(2);
                thanhToanTable.WidthPercentage = 100;
                thanhToanTable.SetWidths(new float[] { 30f, 70f });
                thanhToanTable.SpacingAfter = 15f;

                AddInvoiceInfoRow(thanhToanTable, "Hình thức thanh toán:", giaoDich["HinhThucThanhToan"]?.ToString() ?? "");
                AddInvoiceInfoRow(thanhToanTable, "Trạng thái:", giaoDich["TrangThaiThanhToan"]?.ToString() ?? "");
                AddInvoiceInfoRow(thanhToanTable, "Nhân viên:", giaoDich["TenNhanVien"]?.ToString() ?? "");

                document.Add(thanhToanTable);

                // ==================== LƯU Ý ====================
                Paragraph luuY = new Paragraph("LƯU Ý QUAN TRỌNG:", new Font(baseFont, 11, Font.BOLD, new BaseColor(244, 67, 54)));
                luuY.SpacingBefore = 10f;
                document.Add(luuY);

                string luuYText = 
                    "• Khách hàng cần mang theo giấy tờ tùy thân khi nhận xe.\n" +
                    "• Xe phải được trả về đúng hạn, trong tình trạng tốt và đủ xăng.\n" +
                    "• Trả xe muộn sẽ bị phạt 150% giá thuê/ngày cho mỗi ngày trễ hạn.\n" +
                    "• Tiền cọc sẽ được hoàn trả sau khi kiểm tra xe khi trả xe.";

                Paragraph luuYContent = new Paragraph(luuYText, new Font(baseFont, 9, Font.NORMAL, BaseColor.DARK_GRAY));
                luuYContent.SpacingAfter = 15f;
                document.Add(luuYContent);

                // ==================== CHỮ KÝ ====================
                document.Add(new Paragraph("\n"));

                PdfPTable signTable = new PdfPTable(3);
                signTable.WidthPercentage = 100;
                signTable.SetWidths(new float[] { 33f, 34f, 33f });
                signTable.SpacingBefore = 20f;

                // Người thuê
                PdfPCell signCell1 = new PdfPCell();
                signCell1.Border = Rectangle.NO_BORDER;
                signCell1.HorizontalAlignment = Element.ALIGN_CENTER;
                Paragraph nguoiThue = new Paragraph("NGƯỜI THUÊ XE", boldFont);
                nguoiThue.Alignment = Element.ALIGN_CENTER;
                signCell1.AddElement(nguoiThue);
                signCell1.AddElement(new Paragraph("(Ký và ghi rõ họ tên)", smallFont) { Alignment = Element.ALIGN_CENTER });
                signCell1.AddElement(new Paragraph("\n\n\n", normalFont));
                signCell1.AddElement(new Paragraph(giaoDich["HoTenKH"]?.ToString() ?? "", normalFont) { Alignment = Element.ALIGN_CENTER });
                signTable.AddCell(signCell1);

                // Nhân viên cho thuê
                PdfPCell signCell2 = new PdfPCell();
                signCell2.Border = Rectangle.NO_BORDER;
                signCell2.HorizontalAlignment = Element.ALIGN_CENTER;
                Paragraph nhanVien = new Paragraph("NHÂN VIÊN CHO THUÊ", boldFont);
                nhanVien.Alignment = Element.ALIGN_CENTER;
                signCell2.AddElement(nhanVien);
                signCell2.AddElement(new Paragraph("(Ký và ghi rõ họ tên)", smallFont) { Alignment = Element.ALIGN_CENTER });
                signCell2.AddElement(new Paragraph("\n\n\n", normalFont));
                signCell2.AddElement(new Paragraph(giaoDich["TenNhanVien"]?.ToString() ?? "", normalFont) { Alignment = Element.ALIGN_CENTER });
                signTable.AddCell(signCell2);

                // Thủ quỹ
                PdfPCell signCell3 = new PdfPCell();
                signCell3.Border = Rectangle.NO_BORDER;
                signCell3.HorizontalAlignment = Element.ALIGN_CENTER;
                Paragraph thuQuy = new Paragraph("THỦ QUỸ", boldFont);
                thuQuy.Alignment = Element.ALIGN_CENTER;
                signCell3.AddElement(thuQuy);
                signCell3.AddElement(new Paragraph("(Ký và ghi rõ họ tên)", smallFont) { Alignment = Element.ALIGN_CENTER });
                signCell3.AddElement(new Paragraph("\n\n\n", normalFont));
                signCell3.AddElement(new Paragraph("_______________", normalFont) { Alignment = Element.ALIGN_CENTER });
                signTable.AddCell(signCell3);

                document.Add(signTable);

                // ==================== FOOTER ====================
                document.Add(new Paragraph("\n"));
                PdfPTable footerLineTable = new PdfPTable(1);
                footerLineTable.WidthPercentage = 100;
                PdfPCell footerLineCell = new PdfPCell();
                footerLineCell.Border = Rectangle.BOTTOM_BORDER;
                footerLineCell.BorderWidth = 0.5f;
                footerLineCell.BorderColor = BaseColor.LIGHT_GRAY;
                footerLineCell.FixedHeight = 0f;
                footerLineTable.AddCell(footerLineCell);
                document.Add(footerLineTable);

                Paragraph footer = new Paragraph(
                    "Cảm ơn quý khách đã sử dụng dịch vụ cho thuê xe của chúng tôi!\n" +
                    "Mọi thắc mắc xin liên hệ: 0386624219 hoặc email: Vothienlinh2@gmail.com ",
                    smallFont
                );
                footer.Alignment = Element.ALIGN_CENTER;
                footer.SpacingBefore = 5f;
                document.Add(footer);

            }
            finally
            {
                document.Close();
            }
        }

        // Helper methods cho hóa đơn
        private static void AddInvoiceInfoRow(PdfPTable table, string label, string value)
        {
            PdfPCell labelCell = new PdfPCell(new Phrase(label, boldFont));
            labelCell.Border = Rectangle.NO_BORDER;
            labelCell.Padding = 3;
            table.AddCell(labelCell);

            PdfPCell valueCell = new PdfPCell(new Phrase(value ?? "", normalFont));
            valueCell.Border = Rectangle.NO_BORDER;
            valueCell.Padding = 3;
            table.AddCell(valueCell);
        }

        private static void AddProductTableHeader(PdfPTable table, string text)
        {
            Font whiteFont = new Font(baseFont, 11, Font.BOLD, BaseColor.WHITE);
            PdfPCell cell = new PdfPCell(new Phrase(text, whiteFont));
            cell.BackgroundColor = new BaseColor(33, 150, 243);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 8;
            table.AddCell(cell);
        }

        private static void AddProductTableRow(PdfPTable table, string stt, string tenSP, string soLuong, string donGia, string thanhTien)
        {
            // STT
            PdfPCell sttCell = new PdfPCell(new Phrase(stt, normalFont));
            sttCell.HorizontalAlignment = Element.ALIGN_CENTER;
            sttCell.Padding = 5;
            sttCell.BorderColor = BaseColor.LIGHT_GRAY;
            table.AddCell(sttCell);

            // Tên sản phẩm
            PdfPCell tenCell = new PdfPCell(new Phrase(tenSP, normalFont));
            tenCell.HorizontalAlignment = Element.ALIGN_LEFT;
            tenCell.Padding = 5;
            tenCell.BorderColor = BaseColor.LIGHT_GRAY;
            table.AddCell(tenCell);

            // Số lượng
            PdfPCell slCell = new PdfPCell(new Phrase(soLuong, normalFont));
            slCell.HorizontalAlignment = Element.ALIGN_CENTER;
            slCell.Padding = 5;
            slCell.BorderColor = BaseColor.LIGHT_GRAY;
            table.AddCell(slCell);

            // Đơn giá
            PdfPCell dgCell = new PdfPCell(new Phrase(donGia + " VNĐ", normalFont));
            dgCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            dgCell.Padding = 5;
            dgCell.BorderColor = BaseColor.LIGHT_GRAY;
            table.AddCell(dgCell);

            // Thành tiền
            PdfPCell ttCell = new PdfPCell(new Phrase(thanhTien + " VNĐ", normalFont));
            ttCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            ttCell.Padding = 5;
            ttCell.BorderColor = BaseColor.LIGHT_GRAY;
            table.AddCell(ttCell);
        }

        // Helper methods cho danh sách
        private static void AddTableHeader(PdfPTable table, string text)
        {
            Font whiteFont = new Font(baseFont, 11, Font.BOLD, BaseColor.WHITE);
            PdfPCell cell = new PdfPCell(new Phrase(text, whiteFont));
            cell.BackgroundColor = new BaseColor(33, 150, 243);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 8;
            table.AddCell(cell);
        }

        private static void AddTableCell(PdfPTable table, string text)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, normalFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 5;
            table.AddCell(cell);
        }
    }
}

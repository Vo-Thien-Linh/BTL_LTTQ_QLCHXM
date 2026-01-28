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
                
                AddSimpleInfoRow(benMuaTable, "Họ và tên:", hopDong["HoTenKH"]?.ToString() ?? "");
                AddSimpleInfoRow(benMuaTable, "CCCD/CMND:", hopDong["SoCCCD"]?.ToString() ?? "");
                AddSimpleInfoRow(benMuaTable, "Địa chỉ:", hopDong["DiaChi"]?.ToString() ?? "");
                AddSimpleInfoRow(benMuaTable, "Điện thoại:", hopDong["Sdt"]?.ToString() ?? "");
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
                xeTable.SetWidths(new float[] { 50f, 50f });
                xeTable.SpacingAfter = 10f;

                AddSimpleInfoRow(xeTable, "Hãng xe:", hopDong["TenHangXe"]?.ToString() ?? "");
                AddSimpleInfoRow(xeTable, "Dòng xe:", hopDong["TenDongXe"]?.ToString() ?? "");
                AddSimpleInfoRow(xeTable, "Màu sắc:", hopDong["TenMauSac"]?.ToString() ?? "");
                AddSimpleInfoRow(xeTable, "Năm sản xuất:", hopDong["NamSanXuat"]?.ToString() ?? "");
                AddSimpleInfoRow(xeTable, "Biển số:", hopDong["BienSo"]?.ToString() ?? "Chưa đăng ký");
                AddSimpleInfoRow(xeTable, "Số khung:", hopDong["SoKhung"]?.ToString() ?? "");
                AddSimpleInfoRow(xeTable, "Số máy:", hopDong["SoMay"]?.ToString() ?? "");

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

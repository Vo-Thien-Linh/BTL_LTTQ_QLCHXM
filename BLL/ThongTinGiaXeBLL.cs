using System;
using System.Data;
using DAL;

namespace BLL
{
    public class ThongTinGiaXeBLL
    {
        private ThongTinGiaXeDAL thongTinGiaXeDAL = new ThongTinGiaXeDAL();

        /// <summary>
        /// Lấy danh sách xe cho khách hàng
        /// </summary>
        public DataTable GetXeForCustomer(string phanLoai = null)
        {
            try
            {
                return thongTinGiaXeDAL.GetXeForCustomer(phanLoai);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách xe: " + ex.Message);
            }
        }

        /// <summary>
        /// Tìm kiếm xe theo từ khóa
        /// </summary>
        public DataTable SearchXeForCustomer(string keyword, string phanLoai = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return GetXeForCustomer(phanLoai);
                }

                return thongTinGiaXeDAL.SearchXeForCustomer(keyword, phanLoai);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm kiếm xe: " + ex.Message);
            }
        }
    }
}
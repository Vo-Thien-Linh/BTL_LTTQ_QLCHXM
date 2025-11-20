using System;
using System.Data;
using DAL;
using DTO;

namespace BLL
{
    public class HopDongMuaBLL
    {
        private HopDongMuaDAL hopDongMuaDAL;

        public HopDongMuaBLL()
        {
            hopDongMuaDAL = new HopDongMuaDAL();
        }

        /// <summary>
        /// Thêm hợp đồng mua mới
        /// </summary>
        public int InsertHopDongMua(HopDongMuaDTO hopDong, out string errorMessage)
        {
            errorMessage = "";

            // Validate
            if (hopDong.MaGDBan <= 0)
            {
                errorMessage = "Mã giao dịch bán không hợp lệ!";
                return 0;
            }

            if (string.IsNullOrEmpty(hopDong.MaKH))
            {
                errorMessage = "Mã khách hàng không được để trống!";
                return 0;
            }

            if (string.IsNullOrEmpty(hopDong.ID_Xe))
            {
                errorMessage = "Mã xe không được để trống!";
                return 0;
            }

            if (hopDong.GiaBan <= 0)
            {
                errorMessage = "Giá bán phải lớn hơn 0!";
                return 0;
            }

            return hopDongMuaDAL.InsertHopDongMua(hopDong, out errorMessage);
        }

        /// <summary>
        /// Lấy tất cả hợp đồng mua
        /// </summary>
        public DataTable GetAllHopDongMua()
        {
            return hopDongMuaDAL.GetAllHopDongMua();
        }

        /// <summary>
        /// Lấy hợp đồng theo mã giao dịch bán
        /// </summary>
        public DataTable GetHopDongByMaGDBan(int maGDBan)
        {
            return hopDongMuaDAL.GetHopDongByMaGDBan(maGDBan);
        }

        /// <summary>
        /// Cập nhật trạng thái hợp đồng
        /// </summary>
        public bool UpdateTrangThaiHopDong(int maHDM, string trangThai)
        {
            if (maHDM <= 0)
                return false;

            if (string.IsNullOrEmpty(trangThai))
                return false;

            return hopDongMuaDAL.UpdateTrangThaiHopDong(maHDM, trangThai);
        }
    }
}

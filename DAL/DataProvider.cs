using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DAL
{
    public class DataProvider
    {
        // Đọc chuỗi kết nối từ app.config
        public static string connectionString = ConfigurationManager.ConnectionStrings["QLCuaHangXeMayConn"].ConnectionString;

        /// <summary>
        /// Tạo và trả về SqlConnection mới
        /// </summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Thực thi câu lệnh INSERT, UPDATE, DELETE
        /// </summary>
        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // ✅ DEBUG
                    System.Diagnostics.Debug.WriteLine("=== ExecuteNonQuery ===");
                    System.Diagnostics.Debug.WriteLine($"Connection State: {conn.State}");
                    System.Diagnostics.Debug.WriteLine($"Query: {query}");

                    SqlCommand cmd = new SqlCommand(query, conn);

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);

                        // ✅ DEBUG Parameters
                        System.Diagnostics.Debug.WriteLine("Parameters:");
                        foreach (SqlParameter param in parameters)
                        {
                            System.Diagnostics.Debug.WriteLine($"  {param.ParameterName} = [{param.Value}]");
                        }
                    }

                    result = cmd.ExecuteNonQuery();

                    System.Diagnostics.Debug.WriteLine($"Rows affected: {result}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ ExecuteNonQuery Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
            }

            return result;
        }

        /// <summary>
        /// Thực thi câu lệnh trả về giá trị đơn (COUNT, SUM, MAX...)
        /// </summary>
        public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            object result = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // ✅ DEBUG
                    System.Diagnostics.Debug.WriteLine("=== ExecuteScalar ===");
                    System.Diagnostics.Debug.WriteLine($"Connection State: {conn.State}");
                    System.Diagnostics.Debug.WriteLine($"Query: {query}");

                    SqlCommand cmd = new SqlCommand(query, conn);

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);

                        // ✅ DEBUG Parameters
                        System.Diagnostics.Debug.WriteLine("Parameters:");
                        foreach (SqlParameter param in parameters)
                        {
                            System.Diagnostics.Debug.WriteLine($"  {param.ParameterName} = [{param.Value}]");
                        }
                    }

                    result = cmd.ExecuteScalar();

                    System.Diagnostics.Debug.WriteLine($"Result: {result}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ ExecuteScalar Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
            }

            return result;
        }

        /// <summary>
        /// Thực thi câu lệnh SELECT trả về DataTable
        /// </summary>
        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // ✅ DEBUG
                    System.Diagnostics.Debug.WriteLine("=== ExecuteQuery ===");
                    System.Diagnostics.Debug.WriteLine($"Connection State: {conn.State}");
                    System.Diagnostics.Debug.WriteLine($"Database: {conn.Database}");
                    System.Diagnostics.Debug.WriteLine($"Query: {query}");

                    SqlCommand cmd = new SqlCommand(query, conn);

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);

                        // ✅ DEBUG Parameters
                        System.Diagnostics.Debug.WriteLine("Parameters:");
                        foreach (SqlParameter param in parameters)
                        {
                            System.Diagnostics.Debug.WriteLine($"  {param.ParameterName} = [{param.Value}] (Type: {param.SqlDbType})");
                        }
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    System.Diagnostics.Debug.WriteLine($"✅ Rows returned: {dt.Rows.Count}");

                    // ✅ DEBUG: Hiển thị dữ liệu trả về (nếu có)
                    if (dt.Rows.Count > 0)
                    {
                        System.Diagnostics.Debug.WriteLine("First row data:");
                        foreach (DataColumn col in dt.Columns)
                        {
                            System.Diagnostics.Debug.WriteLine($"  {col.ColumnName} = [{dt.Rows[0][col.ColumnName]}]");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine($"❌ SQL Error: {sqlEx.Message}");
                System.Diagnostics.Debug.WriteLine($"Error Number: {sqlEx.Number}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {sqlEx.StackTrace}");

                MessageBox.Show($"Lỗi SQL: {sqlEx.Message}\n\nVui lòng kiểm tra kết nối database!",
                    "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);

                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ ExecuteQuery Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");

                MessageBox.Show($"Lỗi: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                throw;
            }

            return dt;
        }

        /// <summary>
        /// Test kết nối database
        /// </summary>
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    System.Diagnostics.Debug.WriteLine("✅ Kết nối database thành công!");
                    System.Diagnostics.Debug.WriteLine($"Database: {conn.Database}");
                    System.Diagnostics.Debug.WriteLine($"Server: {conn.DataSource}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi kết nối: {ex.Message}");
                MessageBox.Show($"Không thể kết nối database!\n\n{ex.Message}",
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
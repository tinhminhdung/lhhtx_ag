using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using System.Data.SqlClient;
using System.Data;

namespace Framework
{
    public class FService_LaiSuatAGLANG
    {
        //public bool INSERT(EService_LaiSuatAGLANG obj)
        //{
        //    bool flag;
        //    SqlConnection connection = Database.Connection();
        //    SqlCommand command = new SqlCommand("S_Service_LaiSuatAGLANG_Insert", connection)
        //    {
        //        CommandType = CommandType.StoredProcedure
        //    };
        //    SqlTransaction transaction = connection.BeginTransaction();
        //    command.Transaction = transaction;
        //    command.Parameters.Add(new SqlParameter("@IDLaiSuatAGLANG", obj.IDLaiSuatAGLANG));
        //    command.Parameters.Add(new SqlParameter("@IDSanPham", obj.IDSanPham));
        //    command.Parameters.Add(new SqlParameter("@IDThanhVienBan", obj.IDThanhVienBan));
        //    command.Parameters.Add(new SqlParameter("@IDThanhVienHuongHH", obj.IDThanhVienHuongHH));
        //    command.Parameters.Add(new SqlParameter("@LaiSuat", obj.LaiSuat));
        //    command.Parameters.Add(new SqlParameter("@NgayNhan", obj.NgayNhan));
        //    command.Parameters.Add(new SqlParameter("@SoTienDauTu", obj.SoTienDauTu));
        //    command.Parameters.Add(new SqlParameter("@KieuLaiSuat", obj.KieuLaiSuat));
        //    command.Parameters.Add(new SqlParameter("@SoNgayDaChay", obj.SoNgayDaChay));

        //    try
        //    {
        //        command.ExecuteNonQuery();
        //        transaction.Commit();
        //        flag = true;
        //    }
        //    catch
        //    {
        //        transaction.Rollback();
        //        flag = false;
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //    return flag;
        //}



        public List<EService_LaiSuatAGLANG> CATEGORY_PHANTRANG1(string Sql)
        {
            string sql = @"SELECT * FROM Service_LaiSuatAGLANG where 1=1  " + Sql + " order by NgayNhan desc ";
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<EService_LaiSuatAGLANG>(comm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<EService_LaiSuatAGLANG> CATEGORY_PHANTRANG2(string Sql, int PageIndex, int Tongpage)
        {
            int StartRecord = PageIndex * Tongpage;
            int EndRecord = StartRecord + Tongpage + 1;
            string sql = @"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY NgayNhan DESC) AS rowindex ,* 
                                FROM  Service_LaiSuatAGLANG 
        		                where 1=1 " + Sql + " ";
            sql += ") AS A WHERE  ( A.rowindex >  " + StartRecord.ToString() + " AND A.rowindex < " + EndRecord + ")";
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<EService_LaiSuatAGLANG>(comm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }


        #region Name Text
        public List<EService_LaiSuatAGLANG> Name_Text(string Name_Text)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(Name_Text, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<EService_LaiSuatAGLANG>(comm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion
    }
}

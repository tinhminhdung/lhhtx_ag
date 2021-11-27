using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Entity;
using System.Data.SqlClient;

namespace Framwork
{
    public class FCartDetail
    {
        #region[GetById]
        public List<Entity.CartDetail> GetById(string Id)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_CartDetail_GetById", conn);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.Add(new SqlParameter("@Id", Id));
            try
            {
                return Database.Bind_List_Reader<CartDetail>(comm);
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

        #region[GetByAll]
        public List<Entity.CartDetail> GetByAll()
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_CartDetail_GetByAll", conn);
            comm.CommandType = CommandType.StoredProcedure;

            try
            {
                return Database.Bind_List_Reader<CartDetail>(comm);
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

        #region[Insert]


        //#region Insert
        //public static int Insert(string Name, string Address, string Phone, string Email, string Contents, string Money, string lang, string Status, string Phuongthucthanhtoan, string Hinhthucvanchuyen, string StatusGiaoDich, string IDThanhVien)
        //{
        //    using (SqlCommand dbCmd = new SqlCommand("insert Carts values(@Name,@Address,@Phone,@Email,@Contents,getdate(),@Money,@lang,@Status,@Phuongthucthanhtoan,@Hinhthucvanchuyen,@StatusGiaoDich,@IDThanhVien)", Database.Connection()))
        //    {
        //        dbCmd.CommandType = CommandType.Text;
        //        dbCmd.Parameters.Add(new SqlParameter("@Name", Name));
        //        dbCmd.Parameters.Add(new SqlParameter("@Address", Address));
        //        dbCmd.Parameters.Add(new SqlParameter("@Phone", Phone));
        //        dbCmd.Parameters.Add(new SqlParameter("@Email", Email));
        //        dbCmd.Parameters.Add(new SqlParameter("@Contents", Contents));
        //        dbCmd.Parameters.Add(new SqlParameter("@Money", Money));
        //        dbCmd.Parameters.Add(new SqlParameter("@lang", lang));
        //        dbCmd.Parameters.Add(new SqlParameter("@Status", Status));
        //        dbCmd.Parameters.Add(new SqlParameter("@Phuongthucthanhtoan", Phuongthucthanhtoan));
        //        dbCmd.Parameters.Add(new SqlParameter("@Hinhthucvanchuyen", Hinhthucvanchuyen));
        //        dbCmd.Parameters.Add(new SqlParameter("@StatusGiaoDich", StatusGiaoDich));
        //        dbCmd.Parameters.Add(new SqlParameter("@IDThanhVien", IDThanhVien));

        //        dbCmd.ExecuteNonQuery();
        //    }
        //    System.Web.HttpContext.Current.Cache.Remove("Carts");
        //    using (SqlCommand dbCmd = new SqlCommand("select isnull(max(ID),0) as maxid from Carts", Database.Connection()))
        //        return Convert.ToInt32(Database.GetData(dbCmd).Rows[0]["maxid"]);
        //}
        //#endregion
        public static int Insert(CartDetail obj)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_CartDetail_Insert", conn);
            comm.CommandType = CommandType.StoredProcedure;
            SqlTransaction tran = conn.BeginTransaction();
            comm.Transaction = tran;
            comm.Parameters.Add(new SqlParameter("@ID_Cart", obj.ID_Cart));
            comm.Parameters.Add(new SqlParameter("@ipid", obj.ipid));
            comm.Parameters.Add(new SqlParameter("@Price", obj.Price));
            comm.Parameters.Add(new SqlParameter("@Quantity", obj.Quantity));
            comm.Parameters.Add(new SqlParameter("@Money", obj.Money));
            comm.Parameters.Add(new SqlParameter("@Mausac", obj.Mausac));
            comm.Parameters.Add(new SqlParameter("@Kichco", obj.Kichco));
            comm.Parameters.Add(new SqlParameter("@GiaThanhVien", obj.GiaThanhVien));
            comm.Parameters.Add(new SqlParameter("@Diemcoin", obj.Diemcoin));
            comm.Parameters.Add(new SqlParameter("@HoaHongTheoLevel", obj.HoaHongTheoLevel));
            comm.Parameters.Add(new SqlParameter("@IDThanhVien", obj.IDThanhVien));
            comm.Parameters.Add(new SqlParameter("@IDNhaCungCap", obj.IDNhaCungCap));
            comm.Parameters.Add(new SqlParameter("@TrangThaiAgLang", obj.TrangThaiAgLang));
            comm.Parameters.Add(new SqlParameter("@TrangThaiNhaCungCap", obj.TrangThaiNhaCungCap));
            comm.Parameters.Add(new SqlParameter("@LyDoHuyHang", obj.LyDoHuyHang));
            comm.Parameters.Add(new SqlParameter("@TrangThaiNguoiMuaHang", obj.TrangThaiNguoiMuaHang));
            comm.Parameters.Add(new SqlParameter("@LyDoTraHang", obj.LyDoTraHang));
            comm.Parameters.Add(new SqlParameter("@TrangThaiKhieuKien", obj.TrangThaiKhieuKien));
            comm.Parameters.Add(new SqlParameter("@SentMail", obj.SentMail));
            comm.Parameters.Add(new SqlParameter("@NoiDungGiaoHang", obj.NoiDungGiaoHang));
            comm.Parameters.Add(new SqlParameter("@TienTuViNao", obj.TienTuViNao));

            comm.Parameters.Add(new SqlParameter("@TongTienThanhToan", obj.TongTienThanhToan));
            comm.Parameters.Add(new SqlParameter("@TangThanhVienFree", obj.TangThanhVienFree));
            comm.Parameters.Add(new SqlParameter("@ChietKhauChoDaiLy", obj.ChietKhauChoDaiLy));
            comm.Parameters.Add(new SqlParameter("@TongDiemDemDiChia", obj.TongDiemDemDiChia));
            comm.Parameters.Add(new SqlParameter("@ThanhVienFree_DaiLy", obj.ThanhVienFree_DaiLy));
            comm.Parameters.Add(new SqlParameter("@CongDiemVechoAg", obj.CongDiemVechoAg));
            comm.Parameters.Add(new SqlParameter("@ThanhToanNCC", obj.ThanhToanNCC));
            comm.Parameters.Add(new SqlParameter("@ChietKhauVip", obj.ChietKhauVip));
            comm.Parameters.Add(new SqlParameter("@SanPhamChienLuoc", obj.SanPhamChienLuoc));
            try
            {
                comm.ExecuteNonQuery();
                tran.Commit();
                System.Web.HttpContext.Current.Cache.Remove("CartDetail");
                using (SqlCommand dbCmd = new SqlCommand("select isnull(max(ID),0) as maxid from CartDetail", Database.Connection()))
                    return Convert.ToInt32(Database.GetData(dbCmd).Rows[0]["maxid"]);
            }
            catch
            {
                tran.Rollback();
                return 0;
            }
            finally
            {
                conn.Close();
            }

        }
        #endregion

        #region[Update]
        public bool Update(CartDetail obj)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_CartDetail_Update", conn);
            comm.CommandType = CommandType.StoredProcedure;
            SqlTransaction tran = conn.BeginTransaction();
            comm.Transaction = tran;
            comm.Parameters.Add(new SqlParameter("@ID", obj.ID));
            comm.Parameters.Add(new SqlParameter("@ID_Cart", obj.ID_Cart));
            comm.Parameters.Add(new SqlParameter("@ipid", obj.ipid));
            comm.Parameters.Add(new SqlParameter("@Price", obj.Price));
            comm.Parameters.Add(new SqlParameter("@Quantity", obj.Quantity));
            comm.Parameters.Add(new SqlParameter("@Money", obj.Money));

            try
            {
                comm.ExecuteNonQuery();
                tran.Commit();
                return true;
            }
            catch
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                conn.Close();
            }

        }
        #endregion

        #region[Delete]
        public void Delete(string Id)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_CartDetail_Delete", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add(new SqlParameter("@Id", Id));
            SqlTransaction tran = conn.BeginTransaction();
            comm.Transaction = tran;
            try
            {
                comm.ExecuteNonQuery();
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
            }
            finally
            {
                conn.Close();
            }

        }
        #endregion

        #region Detail_NhaCungCap
        public List<Entity.CartDetail> Detail_NhaCungCap(string ID_Cart, string IDNhaCungCap)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_CartDetail_NhaCungCap", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add(new SqlParameter("@ID_Cart", ID_Cart));
            comm.Parameters.Add(new SqlParameter("@IDNhaCungCap", IDNhaCungCap));
            try
            {
                return Database.Bind_List_Reader<CartDetail>(comm);
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

        #region Detail_ID_Cart
        public List<Entity.CartDetail> Detail_ID_Cart(string ID_Cart)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_CartDetail_List_inCart", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add(new SqlParameter("@ID_Cart", ID_Cart));
            try
            {
                return Database.Bind_List_Reader<CartDetail>(comm);
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

        #region DetailID
        public List<Entity.CartDetail> DetailID(string Id)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_CartDetail_GetById", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add(new SqlParameter("@Id", Id));
            try
            {
                return Database.Bind_List_Reader<CartDetail>(comm);
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

        #region Delete_ID_Cart
        public void Delete_ID_Cart(string ID_Cart)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_CartDetail_Delete_by_CartID", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add(new SqlParameter("@ID_Cart", ID_Cart));
            SqlTransaction tran = conn.BeginTransaction();
            comm.Transaction = tran;
            try
            {
                comm.ExecuteNonQuery();
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
            }
            finally
            {
                conn.Close();
            }

        }
        #endregion

        #region CartDetail_List_Cart_Pro
        public void CartDetail_List_Cart_Pro(DataTable dts, string ID_Cart)
        {
            SqlDataAdapter VSda;
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("select pc.*,p.Name from CartDetail as pc left join  products  as p ON pc.ipid = p.ipid where pc.ID_Cart=@ID_Cart", conn);
            comm.Parameters.Add(new SqlParameter("@ID_Cart", ID_Cart));
            comm.CommandType = CommandType.Text;
            try
            {
                VSda = new SqlDataAdapter(comm);
                VSda.Fill(dts);
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
        public void CartDetail_IDThanhVien(DataTable dts, string ID_Cart, string sql)
        {
            string sqls = "select DISTINCT pc.IDThanhVien from CartDetail as pc left join Carts  as p ON pc.ID_Cart = p.ID   " + sql + " and pc.TrangThaiKhieuKien=0 and pc.TrangThaiNhaCungCap=1 and pc.TrangThaiNguoiMuaHang=1 ";

            SqlDataAdapter VSda;
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(sqls, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                VSda = new SqlDataAdapter(comm);
                VSda.Fill(dts);
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
        public void CartDetail_IDThanhViens(DataTable dts, string IDThanhVien)
        {
            string sqls = "select  pc.*,p.Name,p.Create_Date from CartDetail as pc left join Carts  as p ON pc.ID_Cart = p.ID  where p.IDThanhVien=" + IDThanhVien + " and  pc.TrangThaiKhieuKien=0 and pc.TrangThaiNhaCungCap=1 and pc.TrangThaiNguoiMuaHang=1 order by pc.ID desc";

            SqlDataAdapter VSda;
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(sqls, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                VSda = new SqlDataAdapter(comm);
                VSda.Fill(dts);
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
        public List<Entity.CartDetail> Name_Text(string Text)
        {
            List<Entity.CartDetail> list;
            SqlConnection connection = Database.Connection();
            SqlCommand command = new SqlCommand(Text, connection)
            {
                CommandType = CommandType.Text
            };
            try
            {
                list = Database.Bind_List_Reader<CartDetail>(command);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                connection.Close();
            }
            return list;
        }
    }
}
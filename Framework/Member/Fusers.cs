using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;

namespace Framework
{
    public class Fusers
    {
        #region AdminNCC
        public List<users> EXelNCC(string IDThanhVien, string keyword, string ChiNhanh, string Status, string ddltheoLead, string ddlcapdo, string sql1)
        {
            if (!Status.Equals("-1"))
            {
                sql1 += " and istatus=" + Status + " ";
            }
            if (!IDThanhVien.Equals("0"))
            {
                sql1 += " and iuser_id=" + IDThanhVien + " ";
            }
            //if (!ChiNhanh.Equals("0"))
            //{
            //    sql1 += " and IDChiNhanh=" + ChiNhanh + " ";
            //}

            //if (!ddltheoLead.Equals("-1"))
            //{
            //    sql1 += " and Leader=" + ddltheoLead + " ";
            //}
            if (!ddlcapdo.Equals("-1"))
            {
                sql1 += " and LevelThanhVien=" + ddlcapdo + " ";
            }
            string sql = @"SELECT  *  FROM  users  where (vuserun like '%" + keyword + "%'  or vlname like N'%" + keyword + "%' or vaddress like '%" + keyword + "%' or vphone like '%" + keyword + "%'  or vemail like '%" + keyword + "%')   " + sql1 + " ";
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<users>(comm);
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

        public List<TongSo> CATEGORY_PHANTRANG_NCC1(string IDThanhVien, string keyword, string ChiNhanh, string Status, string ddltheoLead, string ddlcapdo, string sql1, string sapxep)
        {
            if (!Status.Equals("-1"))
            {
                sql1 += " and istatus=" + Status + " ";
            }
            if (!IDThanhVien.Equals("0"))
            {
                sql1 += " and iuser_id=" + IDThanhVien + " ";
            }
            if (!ChiNhanh.Equals("0"))
            {
                sql1 += " and IDChiNhanh=" + ChiNhanh + " ";
            }

            //if (!ddltheoLead.Equals("-1"))
            //{
            //    sql1 += " and Leader=" + ddltheoLead + " ";
            //}
            if (!ddlcapdo.Equals("-1"))
            {
                sql1 += " and LevelThanhVien=" + ddlcapdo + " ";
            }


            string sql = @"SELECT  COUNT(*) as Tong  FROM  users  where (vuserun like '%" + keyword + "%'  or vlname like N'%" + keyword + "%' or vaddress like '%" + keyword + "%' or vphone like '%" + keyword + "%'  or vemail like '%" + keyword + "%')   " + sql1 + " ";// " + sapxep + "
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<TongSo>(comm);
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
        public List<users> CATEGORY_PHANTRANG_NCC2(string IDThanhVien, string keyword, string ChiNhanh, string Status, string ddltheoLead, string ddlcapdo, string sql1, string sapxep, int PageIndex, int Tongpage)
        {
            int StartRecord = PageIndex * Tongpage;
            int EndRecord = StartRecord + Tongpage + 1;

            if (!Status.Equals("-1"))
            {
                sql1 += " and istatus=" + Status + " ";
            }
            if (!IDThanhVien.Equals("0"))
            {
                sql1 += " and iuser_id=" + IDThanhVien + " ";
            }
            if (!ChiNhanh.Equals("0"))
            {
                sql1 += " and IDChiNhanh=" + ChiNhanh + " ";
            }
            //if (!ddltheoLead.Equals("-1"))
            //{
            //    sql1 += " and Leader=" + ddltheoLead + " ";
            //}
            if (!ddlcapdo.Equals("-1"))
            {
                sql1 += " and LevelThanhVien=" + ddlcapdo + " ";
            }

            string sql = @"SELECT *  FROM  (SELECT ROW_NUMBER() OVER (" + sapxep + ") AS rowindex ,*   FROM  users  where (vuserun like '%" + keyword + "%'  or vlname like N'%" + keyword + "%' or vaddress like '%" + keyword + "%' or vphone like '%" + keyword + "%'  or vemail like '%" + keyword + "%')  " + sql1 + " ";
            sql += ") AS A WHERE  ( A.rowindex >  " + StartRecord.ToString() + " AND A.rowindex < " + EndRecord + ")";
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<users>(comm);
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



        #region Admin

        public List<users> EXel(string IDThanhVien, string keyword, string ChiNhanh, string Status, string ddltheoLead, string ddlcapdo, string sql1, string sapxep)
        {
            if (!Status.Equals("-1"))
            {
                sql1 += " and DuyetTienDanap=" + Status + " ";
            }
            if (!IDThanhVien.Equals("0"))
            {
                sql1 += " and iuser_id=" + IDThanhVien + " ";
            }
            if (!ChiNhanh.Equals("0"))
            {
                sql1 += " and IDChiNhanh=" + ChiNhanh + " ";
            }

            if (!ddltheoLead.Equals("-1"))
            {
                sql1 += " and Leader=" + ddltheoLead + " ";
            }
            if (!ddlcapdo.Equals("-1"))
            {
                sql1 += " and LevelThanhVien=" + ddlcapdo + " ";
            }

            string sql = @"SELECT  *  FROM  users  where (vuserun like '%" + keyword + "%'  or vlname like N'%" + keyword + "%' or vaddress like '%" + keyword + "%' or vphone like '%" + keyword + "%'  or vemail like '%" + keyword + "%')   " + sql1 + " " + sapxep + "";
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<users>(comm);
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

        public List<TongSo> CATEGORY_PHANTRANG1(string IDThanhVien, string keyword, string ChiNhanh, string Status, string ddltheoLead, string ddlcapdo, string sql1)
        {
            if (!Status.Equals("-1"))
            {
                sql1 += " and DuyetTienDanap=" + Status + " ";
            }
            if (!IDThanhVien.Equals("0"))
            {
                sql1 += " and iuser_id=" + IDThanhVien + " ";
            }
            if (!ChiNhanh.Equals("0"))
            {
                sql1 += " and IDChiNhanh=" + ChiNhanh + " ";
            }

            if (!ddltheoLead.Equals("-1"))
            {
                sql1 += " and Leader=" + ddltheoLead + " ";
            }
            if (!ddlcapdo.Equals("-1"))
            {
                sql1 += " and LevelThanhVien=" + ddlcapdo + " ";
            }


            string sql = @"SELECT  COUNT(*) as Tong  FROM  users  where (vuserun like '%" + keyword + "%'  or vlname like N'%" + keyword + "%' or vaddress like '%" + keyword + "%' or vphone like '%" + keyword + "%'  or vemail like '%" + keyword + "%')   " + sql1 + " ";
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<TongSo>(comm);
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
        public List<users> CATEGORY_PHANTRANG2(string IDThanhVien, string keyword, string ChiNhanh, string Status, string ddltheoLead, string ddlcapdo, string sql1, string sapxep, int PageIndex, int Tongpage)
        {
            int StartRecord = PageIndex * Tongpage;
            int EndRecord = StartRecord + Tongpage + 1;

            if (!Status.Equals("-1"))
            {
                sql1 += " and DuyetTienDanap=" + Status + " ";
            }
            if (!IDThanhVien.Equals("0"))
            {
                sql1 += " and iuser_id=" + IDThanhVien + " ";
            }
            if (!ChiNhanh.Equals("0"))
            {
                sql1 += " and IDChiNhanh=" + ChiNhanh + " ";
            }
            if (!ddltheoLead.Equals("-1"))
            {
                sql1 += " and Leader=" + ddltheoLead + " ";
            }
            if (!ddlcapdo.Equals("-1"))
            {
                sql1 += " and LevelThanhVien=" + ddlcapdo + " ";
            }
            string sql = @"SELECT *  FROM  (SELECT ROW_NUMBER() OVER (" + sapxep + ") AS rowindex ,*  FROM  users  where (vuserun like '%" + keyword + "%'  or vlname like N'%" + keyword + "%' or vaddress like '%" + keyword + "%' or vphone like '%" + keyword + "%'  or vemail like '%" + keyword + "%')  " + sql1 + " ";
            sql += ") AS A WHERE  ( A.rowindex >  " + StartRecord.ToString() + " AND A.rowindex < " + EndRecord + ")";
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<users>(comm);
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
        #region Danhsachthanhvien
        public List<TongSo> ThanhVien_PHANTRANG1(string GioiThieu, string Status)
        {
            string sql1 = "";
            if (!Status.Equals("-1"))
            {
                sql1 += " and istatus=" + Status + " ";
            }
            string sql = @" SELECT COUNT(*) as Tong FROM  users  where GioiThieu=" + GioiThieu + " " + sql1 + " ";
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<TongSo>(comm);
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
        public List<users> ThanhVien_PHANTRANG2(string GioiThieu, string Status, int PageIndex, int Tongpage)
        {
            int StartRecord = PageIndex * Tongpage;
            int EndRecord = StartRecord + Tongpage + 1;
            string sql1 = "";
            if (!Status.Equals("-1"))
            {
                sql1 += " and istatus=" + Status + " ";
            }
            string sql = @"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY iuser_id DESC) AS rowindex ,* 
                                FROM  users
        		                 where GioiThieu=" + GioiThieu + " " + sql1 + " ";
            sql += ") AS A WHERE  ( A.rowindex >  " + StartRecord.ToString() + " AND A.rowindex < " + EndRecord + ")";
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<users>(comm);
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

        #region S_Member_ChuyenDiem
        public bool S_Member_ChuyenDiem(string vuserun, string ID)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_Member_ChuyenDiem", conn);
            comm.CommandType = CommandType.StoredProcedure;
            SqlTransaction tran = conn.BeginTransaction();
            comm.Transaction = tran;
            comm.Parameters.Add(new SqlParameter("@vuserun", vuserun));
            comm.Parameters.Add(new SqlParameter("@ID", @ID));
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

        #region BULD PASSOWRD
        static string BuildPassword(string input)
        {
            //MD5 md5Hasher = MD5.Create();
            //byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            //StringBuilder sBuilder = new StringBuilder();
            //for (int i = 0; i < data.Length; i++)
            //{
            //    sBuilder.Append(data[i].ToString("x2"));
            //} return sBuilder.ToString();
            return input.ToString();
        }
        #endregion

        public List<users> CATEGORY_PHANTRANG(string icid, string lang, string Status, int PageIndex, ref int TotalRecords, int Tongpage)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("Danhsachthanhvien_List", conn);
            comm.CommandType = CommandType.StoredProcedure;
            SqlCommandBuilder.DeriveParameters(comm);
            comm.Parameters["@Nhom"].Value = icid;
            comm.Parameters["@lang"].Value = lang;
            comm.Parameters["@Status"].Value = Status;
            comm.Parameters["@PageIndex"].Value = PageIndex;
            comm.Parameters["@TotalRecord"].Direction = ParameterDirection.Output;
            comm.Parameters["@Tongpage"].Value = Tongpage;
            try
            {
                return Database.Bind_List_Reader_pages<users>(comm, ref TotalRecords);
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

        public DataTable Detailvphone(string vphone)
        {
            DataTable dts = new DataTable();
            SqlDataAdapter VSda;
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("select * from users where vphone=@vphone", conn);
            comm.CommandType = CommandType.Text;
            comm.Parameters.Add(new SqlParameter("@vphone", vphone));
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
            return dts;
        }

        #region DETAIL VUSERUN
        public List<users> DETAIL_VUSERUN(string vuserun)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_users_Vuserun", conn);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.Add(new SqlParameter("@vuserun", vuserun));
            try
            {
                return Database.Bind_List_Reader<users>(comm);
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

        #region DETAIL VEMAIL
        public List<users> DETAIL_VEMAIL(string vuserun, string vemail)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_users_vemail", conn);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.Add(new SqlParameter("@vuserun", vuserun));
            comm.Parameters.Add(new SqlParameter("@vemail", vemail));
            try
            {
                return Database.Bind_List_Reader<users>(comm);
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

        #region DETAIL VALIDATEKEY
        public List<users> DETAIL_VALIDATEKEY(string vvalidatekey)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_users_vvalidatekey", conn);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.Add(new SqlParameter("@vvalidatekey", vvalidatekey));
            try
            {
                return Database.Bind_List_Reader<users>(comm);
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

        #region DETAIL VUSERUN VUSERPWD
        public List<users> DETAIL_VUSERUN_VUSERPWD(string vuserun, string vuserpwd)
        {
            vuserpwd = BuildPassword(vuserpwd);
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_users_vuserun_vuserpwd", conn);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.Add(new SqlParameter("@vuserun", vuserun));
            comm.Parameters.Add(new SqlParameter("@vuserpwd", vuserpwd));
            try
            {
                return Database.Bind_List_Reader<users>(comm);
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

        #region UPDATE VUSERUN PASSWORD
        public bool UPDATE_VUSERUN_PASSWORD(string vuserun, string newpassword)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("Update_vuserun_password", conn);
            comm.CommandType = CommandType.StoredProcedure;

            SqlTransaction tran = conn.BeginTransaction();
            comm.Transaction = tran;
            comm.Parameters.Add(new SqlParameter("@vuserun", vuserun));
            comm.Parameters.Add(new SqlParameter("@vuserpwd", newpassword));
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

        #region UPDATE VUSERUN PASSWORD
        public bool DoiMatKhau(string vuserun)
        {
            string key = DateTime.Now.Ticks.ToString();
            key = key.Substring(key.Length - 8, 7);
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("update users set validatekey=" + key + "  where vuserun=" + vuserun + "", conn);
            comm.CommandType = CommandType.Text;

            SqlTransaction tran = conn.BeginTransaction();
            comm.Transaction = tran;
            comm.Parameters.Add(new SqlParameter("@vuserun", vuserun));
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

        # region UPDATE DATETIME
        public List<users> Update_validatekey_byemail(string vemail, string hash)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("update users set vvalidatekey=@vvalidatekey where vemail=@vemail", conn);
            comm.CommandType = CommandType.Text;
            comm.Parameters.Add(new SqlParameter("@vvalidatekey", hash));
            comm.Parameters.Add(new SqlParameter("@vemail", vemail));
            try
            {
                return Database.Bind_List_Reader<users>(comm);
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

        #region DETAIL VUSERUN VUSERPWD STATUS
        public List<users> DETAIL_VUSERUN_STATUS(string vuserun, string vuserpwd, string istatus)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_users_vuserun_vuserpwd_istatus", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add(new SqlParameter("@vuserun", vuserun));
            comm.Parameters.Add(new SqlParameter("@vuserpwd", vuserpwd));
            comm.Parameters.Add(new SqlParameter("@istatus", istatus));
            try
            {
                return Database.Bind_List_Reader<users>(comm);
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

        #region GET BY ID
        public List<users> GETBYID(string iuser_id)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_users_ID", conn);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.Add(new SqlParameter("@iuser_id", iuser_id));
            try
            {
                return Database.Bind_List_Reader<users>(comm);
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

        #region GET BY ALL
        public List<users> GETBYALL()
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_users_All", conn);
            comm.CommandType = CommandType.StoredProcedure;

            try
            {
                return Database.Bind_List_Reader<users>(comm);
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

        public static int INSERT2(users obj)
        {
            using (SqlCommand comm = new SqlCommand("S_users_Insert", Database.Connection()))
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@vuserun", obj.vuserun));
                comm.Parameters.Add(new SqlParameter("@vuserpwd", BuildPassword(obj.vuserpwd)));
                comm.Parameters.Add(new SqlParameter("@vfname", obj.vfname));
                comm.Parameters.Add(new SqlParameter("@vlname", obj.vlname));
                comm.Parameters.Add(new SqlParameter("@igender", obj.igender));
                comm.Parameters.Add(new SqlParameter("@dbirthday", obj.dbirthday));
                comm.Parameters.Add(new SqlParameter("@vidcard", obj.vidcard));
                comm.Parameters.Add(new SqlParameter("@vaddress", obj.vaddress));
                comm.Parameters.Add(new SqlParameter("@vphone", obj.vphone));
                comm.Parameters.Add(new SqlParameter("@vemail", obj.vemail));
                comm.Parameters.Add(new SqlParameter("@iregionid", obj.iregionid));
                comm.Parameters.Add(new SqlParameter("@vavatar", obj.vavatar));
                comm.Parameters.Add(new SqlParameter("@vavatartitle", obj.vavatartitle));
                comm.Parameters.Add(new SqlParameter("@dcreatedate", obj.dcreatedate));
                comm.Parameters.Add(new SqlParameter("@dlastvisited", obj.dlastvisited));
                comm.Parameters.Add(new SqlParameter("@vvalidatekey", obj.vvalidatekey));
                comm.Parameters.Add(new SqlParameter("@istatus", obj.istatus));
                comm.Parameters.Add(new SqlParameter("@lang", obj.lang));

                comm.Parameters.Add(new SqlParameter("@Type", obj.Type));
                comm.Parameters.Add(new SqlParameter("@IDChiNhanh", obj.IDChiNhanh));
                comm.Parameters.Add(new SqlParameter("@ChiNhanh", obj.ChiNhanh));
                comm.Parameters.Add(new SqlParameter("@GioiThieu", obj.GioiThieu));

                comm.Parameters.Add(new SqlParameter("@DuyetTienDanap", obj.DuyetTienDanap));
                comm.Parameters.Add(new SqlParameter("@TongTienDanapVND", obj.TongTienDanapVND));
                comm.Parameters.Add(new SqlParameter("@TongTienDanapCoin", obj.TongTienDanapCoin));
                comm.Parameters.Add(new SqlParameter("@LevelThanhVien", obj.LevelThanhVien));
                comm.Parameters.Add(new SqlParameter("@Leader", obj.Leader));
                comm.Parameters.Add(new SqlParameter("@TongTienCoinDuocCap", obj.TongTienCoinDuocCap));
                comm.Parameters.Add(new SqlParameter("@MTree", obj.MTree));
                comm.Parameters.Add(new SqlParameter("@ViTienHHGioiThieu", obj.ViTienHHGioiThieu));
                comm.Parameters.Add(new SqlParameter("@HoTro", obj.HoTro));
                comm.Parameters.Add(new SqlParameter("@VIAAFFILIATE", obj.VIAAFFILIATE));
                comm.Parameters.Add(new SqlParameter("@ViAgLang", obj.ViAgLang));
                comm.Parameters.Add(new SqlParameter("@ThanhVienAgLang", obj.ThanhVienAgLang));
                comm.Parameters.Add(new SqlParameter("@TienDangSoHuuBatDongSan", obj.TienDangSoHuuBatDongSan));
                comm.Parameters.Add(new SqlParameter("@Uutien", obj.Uutien));
                comm.Parameters.Add(new SqlParameter("@ViUuTien", obj.ViUuTien));
                comm.Parameters.Add(new SqlParameter("@ViQRCode", obj.ViQRCode));
                comm.Parameters.Add(new SqlParameter("@TrangThaiThamGiaQRCode", obj.TrangThaiThamGiaQRCode));
                comm.Parameters.Add(new SqlParameter("@AnhQRCode", obj.AnhQRCode));
                comm.Parameters.Add(new SqlParameter("@QRCodeChietKhauHH", obj.QRCodeChietKhauHH));
                comm.Parameters.Add(new SqlParameter("@QRCodeHHNguoiMua", obj.QRCodeHHNguoiMua));
                comm.Parameters.Add(new SqlParameter("@QRCodeHHHeThong", obj.QRCodeHHHeThong));
                comm.Parameters.Add(new SqlParameter("@GiayPhepKinhDoanh", obj.GiayPhepKinhDoanh));
                comm.Parameters.Add(new SqlParameter("@AnhChungMinhThuTruoc", obj.AnhChungMinhThuTruoc));
                comm.Parameters.Add(new SqlParameter("@AnhChungMinhThuSau", obj.AnhChungMinhThuSau));
                comm.Parameters.Add(new SqlParameter("@ViHoaHongMuaBan", obj.ViHoaHongMuaBan));
                comm.Parameters.Add(new SqlParameter("@ViHoaHongAFF", obj.ViHoaHongAFF));
                comm.Parameters.Add(new SqlParameter("@CauHinhDuyetDonTuDong", obj.CauHinhDuyetDonTuDong));
                comm.Parameters.Add(new SqlParameter("@TongSoSanPhamDaBan", obj.TongSoSanPhamDaBan));
                comm.Parameters.Add(new SqlParameter("@ViMuaHangAFF", obj.ViMuaHangAFF));
                comm.Parameters.Add(new SqlParameter("@ViFMotAnTheoAgland", obj.ViFMotAnTheoAgland));
                comm.Parameters.Add(new SqlParameter("@ViTangTienVip", obj.ViTangTienVip));
                comm.Parameters.Add(new SqlParameter("@TinhThanh", obj.TinhThanh));
                comm.Parameters.Add(new SqlParameter("@CuaHang", obj.CuaHang));
                comm.Parameters.Add(new SqlParameter("@TatChucNang", obj.TatChucNang));
                comm.Parameters.Add(new SqlParameter("@TrangThaiThongBao", obj.TrangThaiThongBao));

                comm.Parameters.Add(new SqlParameter("@DaBanDuocSanPham", obj.DaBanDuocSanPham));
                comm.Parameters.Add(new SqlParameter("@IDLuuTam", obj.IDLuuTam));
                comm.Parameters.Add(new SqlParameter("@TongTienDaMua", obj.TongTienDaMua));
                comm.ExecuteNonQuery();
            }
            System.Web.HttpContext.Current.Cache.Remove("users");
            using (SqlCommand dbCmd = new SqlCommand("select isnull(max(iuser_id),0) as maxid from users", Database.Connection()))
                return Convert.ToInt32(Database.GetData(dbCmd).Rows[0]["maxid"]);
        }

        #region INSERT
        public bool INSERT(users obj)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_users_Insert", conn);
            comm.CommandType = CommandType.StoredProcedure;
            SqlTransaction tran = conn.BeginTransaction();
            comm.Transaction = tran;
            comm.Parameters.Add(new SqlParameter("@vuserun", obj.vuserun));
            comm.Parameters.Add(new SqlParameter("@vuserpwd", BuildPassword(obj.vuserpwd)));
            comm.Parameters.Add(new SqlParameter("@vfname", obj.vfname));
            comm.Parameters.Add(new SqlParameter("@vlname", obj.vlname));
            comm.Parameters.Add(new SqlParameter("@igender", obj.igender));
            comm.Parameters.Add(new SqlParameter("@dbirthday", obj.dbirthday));
            comm.Parameters.Add(new SqlParameter("@vidcard", obj.vidcard));
            comm.Parameters.Add(new SqlParameter("@vaddress", obj.vaddress));
            comm.Parameters.Add(new SqlParameter("@vphone", obj.vphone));
            comm.Parameters.Add(new SqlParameter("@vemail", obj.vemail));
            comm.Parameters.Add(new SqlParameter("@iregionid", obj.iregionid));
            comm.Parameters.Add(new SqlParameter("@vavatar", obj.vavatar));
            comm.Parameters.Add(new SqlParameter("@vavatartitle", obj.vavatartitle));
            comm.Parameters.Add(new SqlParameter("@dcreatedate", obj.dcreatedate));
            comm.Parameters.Add(new SqlParameter("@dlastvisited", obj.dlastvisited));
            comm.Parameters.Add(new SqlParameter("@vvalidatekey", obj.vvalidatekey));
            comm.Parameters.Add(new SqlParameter("@istatus", obj.istatus));
            comm.Parameters.Add(new SqlParameter("@lang", obj.lang));

            comm.Parameters.Add(new SqlParameter("@Type", obj.Type));
            comm.Parameters.Add(new SqlParameter("@IDChiNhanh", obj.IDChiNhanh));
            comm.Parameters.Add(new SqlParameter("@ChiNhanh", obj.ChiNhanh));
            comm.Parameters.Add(new SqlParameter("@GioiThieu", obj.GioiThieu));

            comm.Parameters.Add(new SqlParameter("@DuyetTienDanap", obj.DuyetTienDanap));
            comm.Parameters.Add(new SqlParameter("@TongTienDanapVND", obj.TongTienDanapVND));
            comm.Parameters.Add(new SqlParameter("@TongTienDanapCoin", obj.TongTienDanapCoin));
            comm.Parameters.Add(new SqlParameter("@LevelThanhVien", obj.LevelThanhVien));
            comm.Parameters.Add(new SqlParameter("@Leader", obj.Leader));
            comm.Parameters.Add(new SqlParameter("@TongTienCoinDuocCap", obj.TongTienCoinDuocCap));
            comm.Parameters.Add(new SqlParameter("@MTree", obj.MTree));
            comm.Parameters.Add(new SqlParameter("@ViTienHHGioiThieu", obj.ViTienHHGioiThieu));
            comm.Parameters.Add(new SqlParameter("@HoTro", obj.HoTro));
            comm.Parameters.Add(new SqlParameter("@VIAAFFILIATE", obj.VIAAFFILIATE));
            comm.Parameters.Add(new SqlParameter("@ViAgLang", obj.ViAgLang));
            comm.Parameters.Add(new SqlParameter("@ThanhVienAgLang", obj.ThanhVienAgLang));
            comm.Parameters.Add(new SqlParameter("@TienDangSoHuuBatDongSan", obj.TienDangSoHuuBatDongSan));
            comm.Parameters.Add(new SqlParameter("@Uutien", obj.Uutien));
            comm.Parameters.Add(new SqlParameter("@ViUuTien", obj.ViUuTien));
            comm.Parameters.Add(new SqlParameter("@ViQRCode", obj.ViQRCode));
            comm.Parameters.Add(new SqlParameter("@TrangThaiThamGiaQRCode", obj.TrangThaiThamGiaQRCode));
            comm.Parameters.Add(new SqlParameter("@AnhQRCode", obj.AnhQRCode));
            comm.Parameters.Add(new SqlParameter("@QRCodeChietKhauHH", obj.QRCodeChietKhauHH));
            comm.Parameters.Add(new SqlParameter("@QRCodeHHNguoiMua", obj.QRCodeHHNguoiMua));
            comm.Parameters.Add(new SqlParameter("@QRCodeHHHeThong", obj.QRCodeHHHeThong));
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

        #region UPDATE
        public bool UPDATE(users obj)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_users_Update", conn);
            comm.CommandType = CommandType.StoredProcedure;

            SqlTransaction tran = conn.BeginTransaction();
            comm.Transaction = tran;
            comm.Parameters.Add(new SqlParameter("@iuser_id", obj.iuser_id));
            comm.Parameters.Add(new SqlParameter("@vuserun", obj.vuserun));
            comm.Parameters.Add(new SqlParameter("@vuserpwd", obj.vuserpwd));
            comm.Parameters.Add(new SqlParameter("@vfname", obj.vfname));
            comm.Parameters.Add(new SqlParameter("@vlname", obj.vlname));
            comm.Parameters.Add(new SqlParameter("@igender", obj.igender));
            comm.Parameters.Add(new SqlParameter("@dbirthday", obj.dbirthday));
            comm.Parameters.Add(new SqlParameter("@vidcard", obj.vidcard));
            comm.Parameters.Add(new SqlParameter("@vaddress", obj.vaddress));
            comm.Parameters.Add(new SqlParameter("@vphone", obj.vphone));
            comm.Parameters.Add(new SqlParameter("@vemail", obj.vemail));
            comm.Parameters.Add(new SqlParameter("@iregionid", obj.iregionid));
            comm.Parameters.Add(new SqlParameter("@vavatar", obj.vavatar));
            comm.Parameters.Add(new SqlParameter("@vavatartitle", obj.vavatartitle));
            comm.Parameters.Add(new SqlParameter("@dcreatedate", obj.dcreatedate));
            comm.Parameters.Add(new SqlParameter("@dlastvisited", obj.dlastvisited));
            comm.Parameters.Add(new SqlParameter("@vvalidatekey", obj.vvalidatekey));
            comm.Parameters.Add(new SqlParameter("@istatus", obj.istatus));
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

        #region DELETE
        public void DELETE(string iuser_id)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_users_Delete", conn);
            comm.CommandType = CommandType.StoredProcedure;
            SqlTransaction tran = conn.BeginTransaction();
            comm.Transaction = tran;
            comm.Parameters.Add(new SqlParameter("@iuser_id", iuser_id));
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

        #region UPDATE STATUS
        public bool UPDATE_STATUS(string iuser_id, string istatus)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("UPDATE [users] SET [istatus] = @istatus WHERE iuser_id = @iuser_id", conn);
            comm.CommandType = CommandType.Text;
            SqlTransaction tran = conn.BeginTransaction();
            comm.Transaction = tran;
            comm.Parameters.Add(new SqlParameter("@istatus", istatus));
            comm.Parameters.Add(new SqlParameter("@iuser_id", iuser_id));
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

        #region CATEGORY
        public List<users> CATEGORY(string istatus)
        {
            string sql = @"select * from users order by dcreatedate desc";
            if (!istatus.Equals("-1"))
            {
                sql = sql + "where istatus=" + istatus + "";
            }
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            SqlTransaction tran = conn.BeginTransaction();
            comm.Transaction = tran;
            if (!istatus.Equals("-1"))
            {
                comm.Parameters.Add(new SqlParameter("@istatus", istatus));

            }
            try
            {
                return Database.Bind_List_Reader<users>(comm);
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
        #region UPDATE
        public bool users_update(users obj)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("update users set vuserun=@vuserun,vfname=@vfname,vlname=@vlname,igender=@igender,dbirthday=@dbirthday,vaddress=@vaddress,vphone=@vphone,vavatartitle=@vavatartitle,vemail=@vemail where iuser_id=@iuser_id", conn);
            comm.CommandType = CommandType.Text;
            SqlTransaction tran = conn.BeginTransaction();
            comm.Transaction = tran;
            comm.Parameters.Add(new SqlParameter("@vuserun", obj.vuserun));
            comm.Parameters.Add(new SqlParameter("@vfname", obj.vfname));
            comm.Parameters.Add(new SqlParameter("@vlname", obj.vlname));
            comm.Parameters.Add(new SqlParameter("@igender", obj.igender));
            comm.Parameters.Add(new SqlParameter("@dbirthday", obj.dbirthday));
            comm.Parameters.Add(new SqlParameter("@vaddress", obj.vaddress));
            comm.Parameters.Add(new SqlParameter("@vphone", obj.vphone));
            comm.Parameters.Add(new SqlParameter("@vavatartitle", obj.vavatartitle));
            comm.Parameters.Add(new SqlParameter("@vemail", obj.vemail));
            comm.Parameters.Add(new SqlParameter("@iuser_id", obj.iuser_id));
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

        #region UPDATE
        public bool Active_vvalidatekey(string vuserun, string vvalidatekey)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("update users set vvalidatekey=@vvalidatekey where vuserun=@vuserun", conn);
            comm.CommandType = CommandType.Text;
            SqlTransaction tran = conn.BeginTransaction();
            comm.Transaction = tran;
            comm.Parameters.Add(new SqlParameter("@vvalidatekey", vvalidatekey));
            comm.Parameters.Add(new SqlParameter("@vuserun", vuserun));
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
        #region CATEGORY ADMIN
        public List<users> CATEGORY_ADMIN(string orderby, string searchkeyword, string[] searchfields, string lang, string istatus, string chinhanh)
        {
            int i;
            string shortbydate = "";
            if (orderby.Length < 1)
            {
                shortbydate = "order by dcreatedate desc";
            }
            else
            {
                shortbydate = "order by " + orderby;
            }
            string sql = @"select * from users where lang=@lang ";
            if (!istatus.Equals("-1"))
            {
                sql += " and istatus=@istatus";
            }
            if (!chinhanh.Equals("0"))
            {
                sql += " and IDChiNhanh=" + chinhanh + "";
            }
            if ((searchkeyword.Length > 0) && (searchfields.Length > 0))
            {
                sql = sql + " and ";
                string strsearch = "(";
                for (i = 0; i < searchfields.Length; i++)
                {
                    if (i < (searchfields.Length - 1))
                    {
                        strsearch = strsearch + searchfields[i] + " like N'%" + searchkeyword + "%' or ";
                    }
                    else
                    {
                        strsearch = strsearch + searchfields[i] + " like N'%" + searchkeyword + "%'";
                    }
                }
                strsearch = strsearch + ")";
                sql = sql + strsearch;
            }
            sql += " " + shortbydate + "";
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            comm.Parameters.Add(new SqlParameter("@lang", lang));
            if (!istatus.Equals("-1"))
            {
                comm.Parameters.Add(new SqlParameter("@istatus", istatus));
            }
            if ((searchkeyword.Length > 0) && (searchfields.Length > 0))
            {
                for (i = 0; i < searchfields.Length; i++)
                {
                    comm.Parameters.Add(new SqlParameter("@" + searchfields[i], searchkeyword));
                }
            }
            try
            {
                return Database.Bind_List_Reader<users>(comm);
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
        #region UPDATE
        public bool users_update_updatepassword(string vuserun, string newpassword)
        {
            newpassword = BuildPassword(newpassword);
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("update users set vuserpwd=@vuserpwd,vidcard=vidcard + 1 where vuserun=@vuserun", conn);
            comm.CommandType = CommandType.Text;
            SqlTransaction tran = conn.BeginTransaction();
            comm.Transaction = tran;
            comm.Parameters.Add(new SqlParameter("@vuserpwd", newpassword));
            comm.Parameters.Add(new SqlParameter("@vuserun", vuserun));
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

        #region UPDATE Lần lấy mật khẩu
        public bool users_update_SolanLaymatkhau(string vuserun)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("update users set vidcard=vidcard + 1 where vuserun=@vuserun", conn);
            comm.CommandType = CommandType.Text;
            SqlTransaction tran = conn.BeginTransaction();
            comm.Transaction = tran;
            comm.Parameters.Add(new SqlParameter("@vuserun", vuserun));
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
        #region UPDATE
        public bool users_update_updateavatar(users obj)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("update users set vavatar=@vavatar,vavatartitle=@vavatartitle ,GiayPhepKinhDoanh=@GiayPhepKinhDoanh,AnhChungMinhThuTruoc=@AnhChungMinhThuTruoc,AnhChungMinhThuSau=@AnhChungMinhThuSau  where vuserun=@vuserun", conn);
            comm.CommandType = CommandType.Text;
            SqlTransaction tran = conn.BeginTransaction();
            comm.Transaction = tran;
            comm.Parameters.Add(new SqlParameter("@vavatar", obj.vavatar));
            comm.Parameters.Add(new SqlParameter("@vavatartitle", obj.vavatartitle));
            comm.Parameters.Add(new SqlParameter("@vuserun", obj.vuserun));
            comm.Parameters.Add(new SqlParameter("@GiayPhepKinhDoanh", obj.GiayPhepKinhDoanh));
            comm.Parameters.Add(new SqlParameter("@AnhChungMinhThuTruoc", obj.AnhChungMinhThuTruoc));
            comm.Parameters.Add(new SqlParameter("@AnhChungMinhThuSau", obj.AnhChungMinhThuSau));
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

        public DataTable vvalidatekey(string vvalidatekey)
        {
            DataTable dts = new DataTable();
            SqlDataAdapter VSda;
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("select * from users where vvalidatekey=@vvalidatekey", conn);
            comm.CommandType = CommandType.Text;
            comm.Parameters.Add(new SqlParameter("@vvalidatekey", vvalidatekey));
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
            return dts;
        }

        public DataTable Detailvuserun(string vuserun)
        {
            DataTable dts = new DataTable();
            SqlDataAdapter VSda;
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("select * from users where vuserun=@vuserun", conn);
            comm.CommandType = CommandType.Text;
            comm.Parameters.Add(new SqlParameter("@vuserun", vuserun));
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
            return dts;
        }

        public DataTable Detailemail(string vemail)
        {
            DataTable dts = new DataTable();
            SqlDataAdapter VSda;
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("select * from users where vemail=@vemail", conn);
            comm.CommandType = CommandType.Text;
            comm.Parameters.Add(new SqlParameter("@vemail", vemail));
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
            return dts;
        }

        #region GET BY ID
        public List<users> Logiin(string vuserun, string vuserpwd, string istatus)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("select * from users where vuserun='" + vuserun + "' and vuserpwd='" + vuserpwd + "' and istatus=" + istatus + "", conn);
            comm.CommandType = CommandType.Text;
            comm.Parameters.Add(new SqlParameter("@vuserun", vuserun));
            comm.Parameters.Add(new SqlParameter("@vuserpwd", BuildPassword(vuserpwd)));
            comm.Parameters.Add(new SqlParameter("@istatus", istatus));
            try
            {
                return Database.Bind_List_Reader<users>(comm);
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

        public DataTable Detail_vuserun_vuserpwd(string vuserun, string vuserpwd, string istatus)
        {
            vuserpwd = BuildPassword(vuserpwd);
            DataTable dts = new DataTable();
            SqlDataAdapter VSda;
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("select * from users where vuserun=@vuserun and vuserpwd=@vuserpwd and istatus=@istatus", conn);
            comm.CommandType = CommandType.Text;
            comm.Parameters.Add(new SqlParameter("@vuserun", vuserun));
            comm.Parameters.Add(new SqlParameter("@vuserpwd", BuildPassword(vuserpwd)));
            comm.Parameters.Add(new SqlParameter("@istatus", istatus));
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
            return dts;
        }

        public DataTable Getdetailbyunpwd(string vuserun, string vuserpwd)
        {
            vuserpwd = BuildPassword(vuserpwd);
            DataTable dts = new DataTable();
            SqlDataAdapter VSda;
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("select * from users where vuserun=@vuserun and vuserpwd=@vuserpwd", conn);
            comm.CommandType = CommandType.Text;
            comm.Parameters.Add(new SqlParameter("@vuserun", vuserun));
            comm.Parameters.Add(new SqlParameter("@vuserpwd", vuserpwd));
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
            return dts;
        }

        #region Name StoredProcedure
        public List<users> Name_StoredProcedure(string Name_StoredProcedure)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(Name_StoredProcedure, conn);
            comm.CommandType = CommandType.StoredProcedure;
            try
            {
                return Database.Bind_List_Reader<users>(comm);
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

        #region Name Text
        public List<users> Name_Text(string Name_Text)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(Name_Text, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<users>(comm);
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

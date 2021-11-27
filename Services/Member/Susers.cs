using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework;
using Entity;

namespace Services
{
    public class Susers
    {
        private static Fusers db = new Fusers();


        #region CATEGORY_PHANTRANG_NCC
        public static List<users> CATEGORY_PHANTRANG_NCC2(string IDThanhVien, string keyword, string ChiNhanh, string Status, string ddltheoLead, string ddlcapdo, string sql1, string sapxep, int PageIndex, int Tongpage)
        {
            return db.CATEGORY_PHANTRANG_NCC2(IDThanhVien, keyword, ChiNhanh, Status, ddltheoLead, ddlcapdo, sql1, sapxep, PageIndex, Tongpage);
        }

        public static List<TongSo> CATEGORY_PHANTRANG_NCC1(string IDThanhVien, string keyword, string ChiNhanh, string Status, string ddltheoLead, string ddlcapdo, string sql1, string sapxep)
        {
            return db.CATEGORY_PHANTRANG_NCC1(IDThanhVien, keyword, ChiNhanh, Status, ddltheoLead, ddlcapdo, sql1, sapxep);
        }

        public static List<users> EXelNCC(string IDThanhVien, string keyword, string ChiNhanh, string Status, string ddltheoLead, string ddlcapdo, string sql1)
        {
            return db.EXelNCC(IDThanhVien, keyword, ChiNhanh, Status, ddltheoLead, ddlcapdo, sql1);
        }
        #endregion


        #region CATEGORY_PHANTRANG
        public static List<users> CATEGORY_PHANTRANG2(string IDThanhVien, string keyword, string ChiNhanh, string Status, string ddltheoLead, string ddlcapdo, string sql1,string sapxep, int PageIndex, int Tongpage)
        {
            return db.CATEGORY_PHANTRANG2(IDThanhVien, keyword, ChiNhanh, Status, ddltheoLead, ddlcapdo, sql1,sapxep, PageIndex, Tongpage);
        }

        public static List<TongSo> CATEGORY_PHANTRANG1(string IDThanhVien, string keyword, string ChiNhanh, string Status, string ddltheoLead, string ddlcapdo, string sql1)
        {
            return db.CATEGORY_PHANTRANG1(IDThanhVien, keyword, ChiNhanh, Status, ddltheoLead, ddlcapdo, sql1);
        }

        public static List<users> EXel(string IDThanhVien, string keyword, string ChiNhanh, string Status, string ddltheoLead, string ddlcapdo, string sql1, string sapxep)
        {
            return db.EXel(IDThanhVien, keyword, ChiNhanh, Status, ddltheoLead, ddlcapdo, sql1, sapxep);
        }
        #endregion

        #region ThanhVien
        public static List<users> ThanhVien_PHANTRANG2(string GioiThieu, string Status, int PageIndex, int Tongpage)
        {
            return db.ThanhVien_PHANTRANG2(GioiThieu, Status, PageIndex, Tongpage);
        }
        public static List<TongSo> ThanhVien_PHANTRANG1(string GioiThieu, string Status)
        {
            return db.ThanhVien_PHANTRANG1(GioiThieu, Status);
        }
        #endregion


        #region CATEGORY_PHANTRANG
        public static List<users> CATEGORY_PHANTRANG(string icid, string lang, string Status, int PageIndex, ref int TotalRecords, int Tongpage)
        {
            return db.CATEGORY_PHANTRANG(icid, lang, Status, PageIndex, ref TotalRecords, Tongpage);
        }
        #endregion

        #region S_Member_ChuyenDiem
        public static bool S_Member_ChuyenDiem(string vuserun, string ID)
        {
            return db.S_Member_ChuyenDiem(vuserun, ID);
        }
        #endregion
        #region UPDATE STATUS
        public static bool UPDATE_STATUS(string vuserun, string istatus)
        {
            return db.UPDATE_STATUS(vuserun, istatus);
        }
        #endregion

        #region GET BY ID
        public static List<users> GET_BY_ID(string ID)
        {
            return db.GETBYID(ID);
        }
        #endregion

        #region CATEGORY
        public static List<users> CATEGORY(string istatus)
        {
            return db.CATEGORY(istatus);
        }
        #endregion

        #region GET BY ALL
        public static List<users> GET_BY_ALL()
        {
            return db.GETBYALL();
        }
        #endregion



        #region INSERT
        public static bool INSERT(users data)
        {
            return db.INSERT(data);
        }
        #endregion

        #region UPDATE
        public static bool users_update(users data)
        {
            return db.users_update(data);
        }
        #endregion
        #region UPDATE
        public static bool UPDATE(users data)
        {
            return db.UPDATE(data);
        }
        #endregion

        #region users_update_updateavatar
        public static bool users_update_updateavatar(users data)
        {
            return db.users_update_updateavatar(data);
        }
        #endregion

        public static List<users> CATEGORY_ADMIN(string orderby, string searchkeyword, string[] searchfields, string lang, string Status, string chinhanh)
        {
            return db.CATEGORY_ADMIN(orderby, searchkeyword, searchfields, lang, Status, chinhanh);
        }

        #region DELETE
        public static void DELETE(string ID)
        {
            db.DELETE(ID);
        }
        #endregion

        #region Name_StoredProcedure
        public static List<users> Name_StoredProcedure(string Name_StoredProcedure)
        {
            return db.Name_StoredProcedure(Name_StoredProcedure);
        }
        #endregion

        #region Name_Text
        public static List<users> Name_Text(string Name_Text)
        {
            return db.Name_Text(Name_Text);
        }
        #endregion
    }
}

using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce
{
    public partial class AutoChuyenDiem : System.Web.UI.Page
    {
        DatalinqDataContext db = new DatalinqDataContext();
        DatalinqDataContext db2 = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            //List<Entity.Products> iitem = SProducts.GetByAll("VIE");
            //if (iitem.Count() > 0)
            //{
            //    foreach (var item in iitem)
            //    {
            //        SProducts.Name_Text("UPDATE [Products] SET ChietKhau='" + Commond.AddEdit_Giamgia(item.Price.ToString(), item.Giacongtynhapvao.ToString()) + "' WHERE ipid=" + item.ipid.ToString() + "");

            //        SProducts.Name_Text("UPDATE [Products] SET PhanTramChietKhauDaiLy='" + Commond.CapBacChietKhauDaiLy(Commond.AddEdit_Giamgia(item.Price.ToString(), item.Giacongtynhapvao.ToString())) + "' WHERE ipid=" + item.ipid.ToString() + "");

            //        SProducts.Name_Text("UPDATE [Products] SET PhanTramChietKhauThanhVien='" + Commond.CapBacChietKhauThanhVien(Commond.AddEdit_Giamgia(item.Price.ToString(), item.Giacongtynhapvao.ToString())) + "' WHERE ipid=" + item.ipid.ToString() + "");

            //    }
            //}

            LoadItems();

            //List<Entity.users> iitem = Susers.GET_BY_ALL();
            //if (iitem.Count() > 0)
            //{
            //    foreach (var item in iitem)
            //    {
            //        double ViMuaHangAFF = Convert.ToDouble(item.ViMuaHangAFF);
            //        double VIAAFFILIATE = Convert.ToDouble(item.VIAAFFILIATE);
            //        double TongTienCoinDuocCap = Convert.ToDouble(item.TongTienCoinDuocCap);
            //        double Conglai = 0;
            //        Conglai = ((TongTienCoinDuocCap) + (VIAAFFILIATE) + (ViMuaHangAFF));
            //        Susers.Name_Text("update users set TongTienCoinDuocCap=" + Conglai.ToString() + "  where iuser_id=" + item.iuser_id.ToString() + "");
            //        Susers.Name_Text("update users set ViMuaHangAFF=0 where iuser_id=" + item.iuser_id.ToString() + "");
            //        Susers.Name_Text("update users set VIAAFFILIATE=0  where iuser_id=" + item.iuser_id.ToString() + "");

            //        double ViHoaHongAFF = Convert.ToDouble(item.ViHoaHongAFF);
            //        double ViHoaHongMuaBan = Convert.ToDouble(item.ViHoaHongMuaBan);
            //        double Conglai2 = 0;
            //        Conglai2 = ((ViHoaHongMuaBan) + (ViHoaHongAFF));
            //        //( HOA HỒNG QUẢN LÝ )ViHoaHongAFF-->ViHoaHongMuaBan (HOA HỒNG MUA BÁN)
            //        Susers.Name_Text("update users set ViHoaHongMuaBan=" + Conglai2.ToString() + "  where iuser_id=" + item.iuser_id.ToString() + "");
            //        Susers.Name_Text("update users set ViHoaHongAFF=0  where iuser_id=" + item.iuser_id.ToString() + "");
            //    }
            //}
        }
        public void LoadItems()
        {
            string sql = "";
            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse("1500");
            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            List<user> iitem = db.ExecuteQuery<user>(@"SELECT * FROM users where 1=1 and DuyetTienDanap=1 order by dcreatedate desc").ToList();
            if (iitem.Count() > 0)
            {
                Tongsobanghi = iitem.Count();
            }
            int PageIndex = (pages - 1);
            int Tongpage = Tongsotrang;
            int StartRecord = PageIndex * Tongpage;
            int EndRecord = StartRecord + Tongpage + 1;

            List<user> dt = db.ExecuteQuery<user>(@"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY dcreatedate DESC) AS rowindex ,*  FROM  users  where 1=1 and DuyetTienDanap=1 ) AS A WHERE  ( A.rowindex >  " + StartRecord.ToString() + " AND A.rowindex < " + EndRecord + ")").ToList();
            if (dt.Count >= 1)
            {
                //foreach (var item in dt)
                //{
                //    #region Kiểm tra ngày hết hạn Update lại trạng thái kích hoạt nạp 480K
                //    Commond.CheckNgayHetHan(item.iuser_id.ToString());
                //    #endregion
                //}
                rp_pagelist.DataSource = dt;
                rp_pagelist.DataBind();
            }

            if (Tongsobanghi % Tongsotrang > 0)
            {
                Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
            }
            else
            {
                Tongsobanghi = Tongsobanghi / Tongsotrang;
            }
            ltpage.Text = Commond.Phantrang("/AutoChuyenDiem.aspx", Tongsobanghi, pages);
        }
    }
}

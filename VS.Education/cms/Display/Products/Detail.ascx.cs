using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;
using System.Data;

namespace VS.E_Commerce.cms.Display.Products
{
    public partial class Detail : System.Web.UI.UserControl
    {
        string pid = "-1";
        string cid = "-1";
        string hp = "";
        int iEmptyIndex = 0;
        string bReturn = "";
        private string language = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["language"] != null)
            {
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["language"] = this.language;
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            if (Request["pid"] != null && !Request["pid"].Equals(""))
            {
                pid = Request["pid"];
            }
            #region Requesthp
            if (Request["hp"] != null && !Request["hp"].Equals(""))
            {
                hp = Request["hp"].ToString();
            }
            iEmptyIndex = hp.IndexOf("?");
            if (iEmptyIndex != -1)
            {
                hp = hp.Substring(0, iEmptyIndex);
            }

            #endregion
            if (!IsPostBack)
            {
                product dt = db.products.SingleOrDefault(p => p.ipid == int.Parse(pid));
                if (dt != null)
                {
                    cid = dt.icid.ToString();
                    ltcatename.Text = Commond.Name(dt.icid.ToString());
                    pid = dt.ipid.ToString();
                    hdipid.Value = dt.ipid.ToString();

                    List<Entity.users> dt1 = Susers.GET_BY_ID(dt.IDThanhVien.ToString());
                    if (dt1.Count >= 1)
                    {
                        if (dt1[0].DuyetTienDanap.ToString() == "1")
                        {
                            if (dt.Status.ToString() == "1")
                            {
                                Panel1.Visible = true;
                            }
                        }
                    }


                    //ShowGiaThanh();

                    //  ltshop.Text = ShowShop(dt.IDThanhVien.ToString());
                    ShowNhaCungCap(dt.IDThanhVien.ToString());


                    ltname.Text = dt.Name;
                    ltdesc.Text = dt.Brief;
                    ltdetail.Text = dt.Contents;
                    if (!string.IsNullOrEmpty(dt.TrongLuong))
                    {
                        lttrongluong.Text = dt.TrongLuong;
                    }
                    else
                    {
                        lttrongluong.Text = "(Đang cập nhật)";
                    }

                    ltcode.Text = dt.Code.ToString();
                    //  ltpricebanbuon.Text = Commond.Setting("Giathanhvien") + AllQuery.MorePro.FormatMoney_VND(dt.GiaThanhVien.ToString());
                    ltprice.Text = AllQuery.MorePro.FormatMoney_VND(dt.Price.ToString());

                    ltthuongthanhvien.Text = AllQuery.MorePro.FormatMoney_TV(Commond.ThanhVienFree(dt.Price.ToString(), dt.GiaThanhVien.ToString()));
                    ltgiadaily.Text = AllQuery.MorePro.FormatMoney_VND(Commond.GiaDaiLy_FormatMoney(Commond.ThanhVienDaiLy(dt.Price.ToString(), dt.GiaThanhVien.ToString()), dt.Price.ToString()));
                    ltgiacuahang.Text = AllQuery.MorePro.FormatMoney_VND(Commond.GiaDaiLy_FormatMoney(Commond.ThanhVienCuaHang(dt.Price.ToString(), dt.GiaThanhVien.ToString()), dt.Price.ToString()));

                    #region Show ảnh đại diện và nhiều ảnh thum
                    if (dt != null)
                    {
                        bReturn += " <a href=\"" + dt.Images + "\"><img src=\"" + dt.Images + "\" alt=\"" + dt.Name + "\"  /></a>";
                        if (dt.Anh.ToString().Length > 5)
                        {
                            string[] strArray = dt.Anh.ToString().Split(new char[] { ',' });
                            for (int i = 0; i < strArray.Length; i++)
                            {
                                bReturn += "<a href=\"" + strArray[i].ToString() + "\"><img alt='" + dt.Name.ToString() + "'src=\"" + strArray[i].ToString().Replace("uploads", "uploads/_thumbs") + "\"/></a>";
                            }
                        }
                    }
                    ltshowimg.Text = bReturn;
                    #endregion
                    // PhanTramChietKhauDaiLy,PhanTramChietKhauThanhVien
                    List<Entity.Category_Product> table = SProducts.Name_Text_Rg("select top " + int.Parse(Commond.Setting("proother")) + " GiaCuaHang,KichHoatDaiLy,IDThanhVien,DiemMuaHang,GiaThanhVien,GiaThanhVienFree,GiaChietKhauDaiLy,ChietKhau,PhanTramChietKhauDaiLy,PhanTramChietKhauThanhVien,ipid,icid,TangName,Alt,Name,Images,ImagesSmall,Brief,Create_Date,Price,OldPrice,ID_Hang,sanxuat,Code,Giacongtynhapvao from products where icid= " + cid + " and ipid!= " + pid + "  and lang= '" + language + "'  and Status=1 order by Create_Date desc");
                    if (table.Count >= 1)
                    {
                        ltother.Text += Commond.LoadProductList_Home_Cate(table);
                    }
                }
                #region CookiesPro_sanphamdaxem
                string ckPro = "";
                try
                {
                    if (MoreAll.MoreAll.GetCookies("CookiesPro").ToString() != null)
                    {
                        ckPro = MoreAll.MoreAll.GetCookies("CookiesPro").ToString();
                    }
                    if (ckPro != "")
                    {
                        int[] arrId = ckPro.Split(',').Select(s => int.Parse(s)).ToArray();
                        if (!arrId.Contains(int.Parse(hdipid.Value.ToString())))
                        {
                            ckPro += "," + hdipid.Value.Trim().ToString();
                        }
                    }
                    else
                    {
                        ckPro += hdipid.Value.Trim().ToString();
                    }
                    MoreAll.MoreAll.SetCookie("CookiesPro", ckPro, 100);
                }
                catch (Exception)
                { }
                #endregion
                // ltShowSanPhamVuaXem.Text = ShowSanPhamVuaXem();
            }
            if (MoreAll.MoreAll.GetCookies("views").Equals("") || !MoreAll.MoreAll.GetCookies("views").Equals(this.pid))
            {
                SProducts.UpdateViewTimes(this.pid);
                MoreAll.MoreAll.SetCookie("views", this.pid);
            }
        }

        public void ShowNhaCungCap(string id)
        {
            user table = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(id));
            if (table != null)
            {
                List<Entity.CartDetail> dtcart = SCartDetail.Name_Text("select * from CartDetail where TrangThaiNhaCungCap=1 and TrangThaiNguoiMuaHang=1 and TrangThaiKhieuKien=0 and IDNhaCungCap=" + table.iuser_id.ToString() + "");
                if (dtcart.Count > 0)
                {
                    double num = 0.0;
                    for (int i = 0; i < dtcart.Count; i++)
                    {
                        num += Convert.ToDouble(dtcart[i].Quantity.ToString());
                    }
                    ltspdaban.Text = num.ToString();
                }
                else
                {
                    ltspdaban.Text = "0";
                }
                lttenshop1.Text = lttenshop.Text = "<a  target=\"_blank\" style='color:red' href='/shop/" + table.vuserun + "'> " + table.TenShop + "</a> ";
                ltdiachikhohang.Text = table.DiaChiKhoHang;
                ltngaythamgia.Text = table.dcreatedate.ToString();
            }
        }
        protected string ShowShop(string id)
        {
            string str = "";
            user dt = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(id));
            if (dt != null)
            {
                str += "<div class=\"row\">";
                str += "<div class=\"col-xs-12 col-sm-12 col-md-6 col-lg-3\">";
                str += " <div class=\"logoncc\">";
                if (dt.vavatar.Length > 0)
                {
                    str += "<img class='logoncc' src='/Uploads/DangKyKinhDoanh/" + dt.vavatar.ToString() + "' >";
                    // str += "<img src=\"/Resources/logo.jpg\" />";
                }
                else
                {
                    str += "<img src=\"/Resources/logo.jpg\" />";
                }

                str += "</div>";
                str += "</div>";
                str += "<div class=\"col-xs-12 col-sm-12 col-md-6 col-lg-9\">";
                str += "  <div class=\"Nameshop\">";
                str += dt.TenShop;
                str += "</div>";

                str += "<div class=\"xemsshop\">";
                str += " <a href='/shop/" + dt.vuserun + "'><i class=\"fa fa-shopping-basket\" aria-hidden=\"true\"></i> Xem Shop</a> ";
                str += "</div>";
                str += "</div>";
                str += "</div>";
            }
            return str;
        }
        protected string ChenquangCaoChinhGiua(string Noidung)
        {
            string str = "";
            #region Tag
            string[] strArray = Noidung.ToString().Split(new char[] { '.' });
            if (strArray.Count() > 0)
            {
                string tong = strArray.Count().ToString();
                Double Tongv = Convert.ToDouble(tong);
                Double TongT = Tongv / 2;
                Double lamtron = Math.Round(TongT, 0);

                for (int i = 0; i < strArray.Length; i++)
                {
                    if (i != 0 && i != 1 && i != int.Parse(tong))
                    {
                        if (i.ToString() == lamtron.ToString())
                        {
                            if (strArray[i].ToString().Contains("<img"))
                            {
                                str += strArray[i].ToString().Replace("/>", "/>" + MoreAll.Other.Giatri("txtcodeq")) + ".";
                            }
                        }
                    }
                    str += strArray[i].ToString() + ".";
                }
                Response.Write(Tongv + "<br>" + TongT + "<br>" + lamtron + "<br>");
            }
            #endregion
            return str;
        }
        protected string ShowSanPhamVuaXem()
        {
            string str = "";
            try
            {
                if (MoreAll.MoreAll.GetCookies("CookiesPro").ToString() != "")
                {
                    List<Entity.Category_Product> table = SProducts.Name_Text_Rg("select top 10 GiaCuaHang,KichHoatDaiLy,IDThanhVien,DiemMuaHang,GiaThanhVien,Alt,ipid,icid,TangName,Name,Images,ImagesSmall,Brief,Create_Date,Price,OldPrice,ID_Hang,sanxuat,Code,Giacongtynhapvao from  products where ipid in (" + MoreAll.MoreAll.GetCookies("CookiesPro").ToString() + ") and lang='" + language + "' and Status=1  order by Create_Date desc");
                    if (table.Count >= 1)
                    {
                        str += Commond.LoadProductList_NoiBatMenu(table);
                    }
                }
            }
            catch (Exception)
            { }
            return str;
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }

        protected void lnkaddtocart_Click(object sender, EventArgs e)
        {
            string Kichco = "0";
            string Mausac = "0";
            try { if (System.Web.HttpContext.Current.Session["Session_Size"].ToString() != null && !System.Web.HttpContext.Current.Session["Session_Size"].ToString().Equals("")) { Kichco = System.Web.HttpContext.Current.Session["Session_Size"].ToString(); } }
            catch (Exception) { }
            try { if (System.Web.HttpContext.Current.Session["Session_MauSac"].ToString() != null && !System.Web.HttpContext.Current.Session["Session_MauSac"].ToString().Equals("")) { Mausac = System.Web.HttpContext.Current.Session["Session_MauSac"].ToString(); } }
            catch (Exception) { }
            if (MoreAll.MoreAll.GetCookies("Members") == "")
            {
                Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                //GioHangs.ShoppingCart_AddProduct(hdipid.Value, Convert.ToInt32(txtsoluong.Text), Mausac, Kichco, "1");
            }
            else
            {
                SessionCarts.ShoppingCart_AddProduct_Sesion(hdipid.Value, Convert.ToInt32(txtsoluong.Text), Mausac, Kichco, "1");
            }
            System.Web.HttpContext.Current.Session["MuaTheoGiaThanhVien"] = null;
            System.Web.HttpContext.Current.Session["Session_Size"] = null;
            System.Web.HttpContext.Current.Session["Session_MauSac"] = null;
            System.Web.HttpContext.Current.Session["GSession_MauSac"] = null;
            System.Web.HttpContext.Current.Session["GSession_Size"] = null;
            Response.Redirect("/gio-hang.html");
        }

        //v01.vmms.vn
        #region MauSac_KichCo
        public string MauSacFull()// cách lấy ra mầu kiểu code mới
        {
            string show = "0";
            string str = "";
            List<Menu> ListMenu = new List<Menu>();
            var list = db.Trunggians.Where(s => s.Proid == int.Parse(hdipid.Value) && s.Trangthai == 1).OrderBy(s => s.ID).ToList();// tìm trong bảng trung gian có bao nhiêu ID 218
            for (int i = 0; i < list.Count; i++)
            {
                var table = db.Menus.Where(s => s.ID == int.Parse(list[i].Icolor.ToString()) && s.Status == 1 && s.capp == "CO").OrderBy(s => s.Orders).ToList();//so sánh bảng menu và bảng trung gian để lấy ra tên của mầu
                if (table.Count > 0)
                {
                    show = "1";
                    for (int j = 0; j < table.Count; j++)
                    {

                        ListMenu.Add(table[j]);
                    }
                }
            }
            if (show == "1")
            {
                ListMenu = ListMenu.OrderBy(s => s.Orders).ToList();
                str += "<div style=\" padding-top:10px;clear:both\"  class=\"price\">";
                str += "<div style=\"color:#353535\">Mầu sắc</div>";
                str += "<div class=\"Mausac\">";
                int k = 1;
                foreach (var item in ListMenu)
                {
                    if (k == 1)
                    {
                        System.Web.HttpContext.Current.Session["Session_MauSac"] = item.ID;
                        System.Web.HttpContext.Current.Session["GSession_MauSac"] = item.ID;
                    }
                    try
                    {
                        if (item.ID.ToString() == Session["GSession_MauSac"].ToString())
                        {
                            str += "<a href=\"javascript:void(0)\" class=\"Color active\" onclick=\"MauSac(" + item.ID + ",'" + item.Name + "')\"><img  src='" + item.Images + "'/></a>";
                        }
                        else
                        {
                            str += "<a href=\"javascript:void(0)\" class=\"Color\" onclick=\"MauSac(" + item.ID + ",'" + item.Name + "')\"><img  src='" + item.Images + "'/></a>";
                        }
                    }
                    catch (Exception)
                    {
                        str += "<a href=\"javascript:void(0)\" class=\"Color\" onclick=\"MauSac(" + item.ID + ",'" + item.Name + "')\"><img  src='" + item.Images + "'/></a>";
                    }
                    k++;
                }
                str += "</div>";
                str += "</div>";
            }
            return str;
        }
        public string KichCoFull()// cách lấy ra mầu kiểu code mới
        {
            string str = "";
            string show = "0";
            List<Menu> ListMenu = new List<Menu>();
            var list = db.Trunggians.Where(s => s.Proid == int.Parse(hdipid.Value) && s.Trangthai == 2).OrderBy(s => s.ID).ToList();// tìm trong bảng trung gian có bao nhiêu ID 218
            for (int i = 0; i < list.Count; i++)
            {
                var table = db.Menus.Where(s => s.ID == int.Parse(list[i].Icolor.ToString()) && s.Status == 1 && s.capp == "SI").OrderBy(s => s.Orders).ToList();//so sánh bảng menu và bảng trung gian để lấy ra tên của mầu
                if (table.Count > 0)
                {
                    show = "1";
                    for (int j = 0; j < table.Count; j++)
                    {
                        ListMenu.Add(table[j]);
                    }
                }
            }
            if (show == "1")
            {
                ListMenu = ListMenu.OrderBy(s => s.Orders).ToList();
                str += "<div style=\" padding-top:10px;clear:both\"  class=\"price\">";
                str += "<div style=\"color:#353535\">Kích cỡ</div>";
                str += "<div class=\"Kichhuoc\">";
                int k = 1;
                foreach (var item in ListMenu)
                {
                    if (k == 1)
                    {
                        System.Web.HttpContext.Current.Session["Session_Size"] = item.ID;
                        System.Web.HttpContext.Current.Session["GSession_Size"] = item.ID;
                    }
                    try
                    {
                        if (item.ID.ToString() == Session["GSession_Size"].ToString())
                        {
                            str += "<a href=\"javascript:void(0)\" class=\"size active\" onclick=\"KichCo(" + item.ID + ",'" + item.Name + "')\">" + item.Name + "</a>";
                        }
                        else
                        {
                            str += "<a href=\"javascript:void(0)\" class=\"size\" onclick=\"KichCo(" + item.ID + ",'" + item.Name + "')\">" + item.Name + "</a>";
                        }
                    }
                    catch (Exception)
                    {
                        str += "<a href=\"javascript:void(0)\" class=\"size\" onclick=\"KichCo(" + item.ID + ",'" + item.Name + "')\">" + item.Name + "</a>";
                    }
                    k++;
                }
                str += "</div>";
                str += "</div>";
            }
            return str;
        }
        #endregion

        //void ShowGiaThanh()
        //{
        //    List<GiaThanhVien> gi = db.GiaThanhViens.Where(p => p.IDSanPham == int.Parse(hdipid.Value)).ToList();
        //    if (gi.Count > 0)
        //    {
        //        rpgiathanhvien.DataSource = gi;
        //        rpgiathanhvien.DataBind();
        //    }
        //}
        //protected void rpgiathanhvien_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    switch (e.CommandName)
        //    {
        //        case "ChonGia":
        //            GiaThanhVien dt = db.GiaThanhViens.SingleOrDefault(p => p.ID == int.Parse(e.CommandArgument.ToString()));
        //            if (dt != null)
        //            {
        //                // Active
        //                System.Web.HttpContext.Current.Session["MuaTheoGiaThanhVien"] = dt.GiaDaiLy;
        //                System.Web.HttpContext.Current.Session["MuaTheoGiaThanhVienID"] = dt.ID.ToString();
        //                // hdgiamuommua.Value = dt.GiaDaiLy;
        //                ShowGiaThanh();
        //            }
        //            break;

        //    }
        //}
        //protected string Stylecss(string id)
        //{
        //    string act = "";
        //    if (Session["MuaTheoGiaThanhVienID"] != null)
        //    {
        //        act = Session["MuaTheoGiaThanhVienID"].ToString();
        //    }
        //    if (id.Equals(act))
        //    {
        //        return "Active";
        //    }
        //    return "";
        //}

    }
}
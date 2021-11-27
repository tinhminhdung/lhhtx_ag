using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;
using Advertisings;

namespace VS.E_Commerce.cms.Display
{
    public partial class Header : System.Web.UI.UserControl
    {
        string hp = "";
        int iEmptyIndex = 0;
        string nav = "";
        string Module = "";
        int _cid = -1;
        DatalinqDataContext db = new DatalinqDataContext();
        private string language = Captionlanguage.Language;
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

            if (!base.IsPostBack)
            {
                if (Request["e"] != null)
                {
                    if (Request["e"].ToString() == "load")
                    {
                        Module = Commond.RequestMenu(Request["hp"]);
                    }
                }
                if (MoreAll.MoreAll.GetCookies("Members") != "")
                {
                    Panel1.Visible = false;
                    Panel4.Visible = false;
                    Panel2.Visible = true;
                    Panel5.Visible = true;
                    ltstyle.Text = "<style>.ViewThanhVien{ display:block}</style>";
                }
                List<Entity.users> table = Susers.Name_Text("select * from users where vuserun=N'" + MoreAll.MoreAll.GetCookies("Members") + "' and istatus=1 ");
                if (table.Count > 0)
                {
                    if (table[0].DuyetTienDanap.ToString() == "1" && table[0].CuaHang.ToString() == "1")
                    {
                        ltcssstyle.Text = "<style>.giacuahang{ display:block !important}.AnGiathanhvienfree_daily{ display:block!important}</style>";
                    }
                    else if (table[0].DuyetTienDanap.ToString() == "0" && table[0].CuaHang.ToString() == "0")
                    {
                        ltcssstyle.Text = "<style>.giacuahang{ display:none!important}.AnGiathanhvienfree_daily{ display:block !important}</style>";
                    }
                    else if (table[0].DuyetTienDanap.ToString() == "1")
                    {
                        ltcssstyle.Text = "<style>.giacuahang{ display:none!important}.AnGiathanhvienfree_daily{ display:block !important}</style>";
                    }

                    //try
                    //{
                    hdid.Value = table[0].iuser_id.ToString();
                    ltquanly1.Text = ltquanly.Text = ShowProducts(table[0].iuser_id.ToString());
                    lichsumuahang1.Text = lichsumuahang.Text = ShowLichSuMuahang(table[0].iuser_id.ToString());

                    ltdanhsachthanhvien.Text = "<a href=\"/Danh-sach-thanh-vien.html?ID=" + table[0].iuser_id.ToString() + "\"><i class=\"fa fa-gift\"></i> Danh sách thành viên</a>";
                    ltdanhsachthanhvien1.Text = "<a href=\"/Danh-sach-thanh-vien.html?ID=" + table[0].iuser_id.ToString() + "\">Danh sách thành viên</a>";
                    ltxinchao.Text = this.ltwelcome.Text = this.ltwelcome.Text ="Xin chào: "+ table[0].vfname;

                    lttinnhan.Text = "<a href=\"/tin-nhan.html\"><i class=\"fa fa-bell\" style=\" color:red\"></i> Tin nhắn của tôi (" + ShowTongThongBao(table[0].iuser_id.ToString()) + ")</a>";
                    lttinnhan1.Text = "<a href=\"/tin-nhan.html\"> Tin nhắn của tôi (" + ShowTongThongBao(table[0].iuser_id.ToString()) + ")</a>";

                    //if (table[0].iuser_id.ToString() == Commond.SetThanhVienChuyenGia())
                    //{
                    //    ltchuyengia.Text = " <div><a href=\"/chuyen-gia.html\"> Lịch sử hh chuyên gia</a></div>";
                    //    ltchuyengia1.Text = " <div><a href=\"/chuyen-gia.html\"><i class=\"fa fa-credit-card\" style=\" color:red\"></i> Lịch sử hh chuyên gia</a></div>";
                    //}

                    if (table[0].ThanhVienAgLang.ToString() == "1")
                    {
                        //  ltThanhvienaglang.Text = "<div class=\"AGLANG\"><a href=\"/lai-suat-agland.html\"> <b class=\"Thanhvienaglang\">AG LAND</b></a> </div>";
                        // ltaglang.Text = "<li class=\"aglangmobile\"><a href=\"/lai-suat-agland.html\"><img class='iconaaa' src=\"/Resources/images/agland.png\" /><span style=\" color:#fff\">AG LAND </span></a></li>";
                        // lthhagland.Text += " <div><a href=\"/lai-suat-agland.html\">Lãi suất AG LAND</a></div>";
                        // lthhagland2.Text += " <div><a href=\"/lai-suat-agland.html\"><i class=\"fa fa-gift\"></i>Lãi suất AG LAND</a></div>";
                    }

                    if (table[0].Type.ToString() == "2")
                    {
                        lthhagland2.Text += "<div> <a href=\"/lich-su-duyet-vi-tam-giu.html\"><i class=\"fa fa-gift\"></i> L/S duyệt ví tạm giữ bán/h</a></div>";
                        lthhagland.Text += "<div> <a href=\"/lich-su-duyet-vi-tam-giu.html\"> L/S duyệt ví tạm giữ bán/h</a></div>";
                    }

                    // lthoahongQRCode.Text += " <div><a href=\"/Hoa-hong-QRcode.html\">Hoa hồng QRCode</a></div>";
                    //lthoahongQRCode2.Text += " <div><a href=\"/Hoa-hong-QRcode.html\"><i class=\"fa fa-gift\"></i> Hoa hồng QRCode</a></div>";
                    if (table[0].TrangThaiThamGiaQRCode.ToString() == "1")
                    {
                        lthoahongQRCode.Text += " <div> <a href=\"/lich-su-thanh-toan-qrcode.html\">L/sử thanh toán QRCode</a></div>";
                        lthoahongQRCode2.Text += " <div> <a href=\"/lich-su-thanh-toan-qrcode.html\"><i class=\"fa fa-clock-o\"></i> L/sử thanh toán QRCode</a></div>";
                    }

                    //if (table[0].vavatar.Length > 0)
                    //{
                    //    // ltlavata2.Text = 
                    //    ltimg.Text = " <img class=\"user-avatar-medium Aavatar\" src=\"/Uploads/avatar//" + table[0].vavatar.ToString() + "\">";
                    //}
                    //else
                    //{
                    //    //  ltlavata2.Text = 
                    //    ltimg.Text = " <i class=\"fa fa-user-o\" aria-hidden=\"true\"></i>";
                    //}
                    if (table[0].Type.ToString() == "2") // là nhà cung cấp
                    {
                        Panel3.Visible = true;
                        Panel6.Visible = true;
                    }
                    else
                    {
                        Panel3.Visible = false;
                        Panel6.Visible = false;
                    }
                    //}
                    //catch (Exception)
                    //{
                    //    MoreAll.MoreAll.SetCookie("Members", "", -1);
                    //    MoreAll.MoreAll.SetCookie("MembersID", "", -1);
                    //}

                }
                else
                {
                    ltcssstyle.Text = "<style>.giacuahang{ display:none!important}.AnGiathanhvienfree_daily{ display:block !important}</style>";
                    ltstyle.Text = "<style>.ViewThanhVien{ display:none}</style>";
                    this.ltwelcome.Text = "";
                    ltxinchao.Text = "";
                    //ltlavata2.Text =
                  //  ltimg.Text = " <i class=\"fa fa-user-o\" aria-hidden=\"true\"></i>";
                    Panel1.Visible = true;
                    Panel4.Visible = true;
                    Panel2.Visible = false;
                    Panel5.Visible = false;
                }
               // KichHoatThanhVien();
            }
        }

        //public void KichHoatThanhVien()
        //{
        //    if (MoreAll.MoreAll.GetCookies("Members") != "")
        //    {
        //        Double Cauhinhtien = Convert.ToDouble(Commond.Setting("txtTienkichhoatdaily"));
        //        List<Entity.users> iitem = Susers.Name_Text("select * from users where iuser_id=" + MoreAll.MoreAll.GetCookies("MembersID").ToString() + " and DuyetTienDanap=0 ");
        //        if (iitem.Count > 0)
        //        {
        //            List<LCart> dt1 = db.LCarts.Where(p => p.IDThanhVien == int.Parse(iitem[0].iuser_id.ToString())).ToList();
        //            if (dt1.Count > 0)
        //            {
        //                Double TongTienDonHangKichHoatThanhVien = 0;
        //                foreach (var item in dt1)
        //                {
        //                    TongTienDonHangKichHoatThanhVien = Convert.ToDouble(item.TongTienSPChienLuocKichHoatTV.ToString());
        //                    if (TongTienDonHangKichHoatThanhVien >= Cauhinhtien)// Kiểm tra đơn nào có tiền thì mới xử lý
        //                    {
        //                        List<CartDetail> dt2 = db.CartDetails.Where(p => p.IDThanhVien == int.Parse(iitem[0].iuser_id.ToString()) && p.ID_Cart == item.ID && p.TrangThaiNhaCungCap == 1 && p.TrangThaiNguoiMuaHang == 1 && p.TrangThaiKhieuKien == 0 && p.SanPhamChienLuoc == 1).ToList();
        //                        if (dt2.Count > 0)
        //                        {
        //                            Double TongTienDaDuyet = 0;
        //                            foreach (var item3 in dt2)
        //                            {
        //                                TongTienDaDuyet += Convert.ToDouble(item3.Money);
        //                            }
        //                            Double TongTienDaDuyet2 = TongTienDaDuyet / 1000;
        //                            if (TongTienDaDuyet2 >= Cauhinhtien)
        //                            {
        //                                Susers.Name_Text("update users set DuyetTienDanap=1 where iuser_id=" + iitem[0].iuser_id.ToString() + "");
        //                            }
        //                        }
        //                    }
        //                }

        //            }
        //        }
        //    }
        //}
        public string SubCart(string ID)
        {
            string submn = "0";
            var dt = db.S_TimSanPhamDaBan(int.Parse(ID.ToString())).ToList();
            for (int i = 0; i < dt.Count; i++)
            {
                submn = submn + "," + dt[i].ID_Cart.ToString();
            }
            return submn;
        }
        protected string ShowProducts(string id)
        {
            //string sql = "select * from Carts where ID in (" + SubCart(id) + ")";
            //sql = sql + " and  Status=0 ";
            //sql = sql + " order by Create_Date desc";
            //List<LCart> dt = db.ExecuteQuery<LCart>(@"" + sql + "").ToList();
            List<CartDetail> dt = db.ExecuteQuery<CartDetail>(@"select * from CartDetail where IDNhaCungCap=" + id + " and TrangThaiNhaCungCap=3").ToList();
            if (dt.Count > 0)
            {
                return dt.Count.ToString();
            }
            return "0";
        }

        public string ShowLichSuMuahang(string ID)
        {
            List<CartDetail> dt = db.ExecuteQuery<CartDetail>(@"select * from CartDetail where IDThanhVien=" + ID + " and TrangThaiNguoiMuaHang=3").ToList();
            //string sql = "select * from Carts where IDThanhVien=" + ID + "";

            //string sql = "select * from Carts where IDThanhVien=" + ID + "";
            //sql = sql + " and  Status=0 ";
            //sql = sql + " order by Create_Date desc";
            //List<LCart> dt = db.ExecuteQuery<LCart>(@"" + sql + "").ToList();
            if (dt.Count > 0)
            {
                return dt.Count.ToString();
            }
            return "0";
        }
        protected void lnkthoat_Click(object sender, EventArgs e)
        {
            MoreAll.MoreAll.SetCookie("Members", "", -1);
            MoreAll.MoreAll.SetCookie("MembersID", "", -1);
           // RemoveCache.Products();
            Response.Redirect("/");
        }
        protected string MenuPro()
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.capp_Lang_Parent_ID_Status(More.PR, language, "-1", "1");
            if (dt.Count > 0)
            {
                //str += "<ul>";
                foreach (Entity.Menu item in dt)
                {
                    if (More.TangNameicid(hp) == item.ID.ToString())
                    {
                        str += "<li><a class=\"active\" href='" + item.TangName.ToString() + ".html'>" + item.Name.ToString() + "</a>" + Menu_Pro(item.ID.ToString()) + "</li>";
                    }
                    else
                    {
                        str += "<li><a href='" + item.TangName.ToString() + ".html'>" + item.Name.ToString() + "</a>" + Menu_Pro(item.ID.ToString()) + "</li>";
                    }
                }
                //str += "</ul>";
            }
            return str.ToString();
        }
        protected string Menu_Pro(string id)
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.capp_Lang_Parent_ID_Status(More.PR, language, id, "1");
            if (dt.Count > 0)
            {
                str += "<ul>";
                foreach (Entity.Menu item in dt)
                {
                    str += "<li><a href='" + item.TangName.ToString() + ".html'>" + item.Name.ToString() + "</a>" + Menu_Pro(item.ID.ToString()) + "</li>";
                }
                str += "</ul>";
            }
            return str.ToString();
        }
        protected string ShowTongThongBao(string id)
        {
            string Tong1 = "0";
            DatalinqDataContext db = new DatalinqDataContext();
            List<Notification> dt1 = db.Notifications.Where(s => s.IDThanhVienNhanThongBao == int.Parse(id) && s.TrangThai == 0).ToList();
            if (dt1.Count > 0)
            {
                Tong1 = dt1.Count.ToString();
            }
            return Tong1;
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}
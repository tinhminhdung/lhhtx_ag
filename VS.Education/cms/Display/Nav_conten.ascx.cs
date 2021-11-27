using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;

namespace VS.E_Commerce.cms.Display
{
    public partial class Nav_conten : System.Web.UI.UserControl
    {
        string hp = "";
        int iEmptyIndex = 0;
        string nav = "";
        string u = "";
        int _cid = -1;
        string pid = "-1";
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

            if (Request["pid"] != null && !Request["pid"].Equals(""))
            {
                pid = Request["pid"];
            }

            try
            {
                if (Request["e"] != null)
                {
                    if (Request["e"].ToString() == "load")
                    {
                        u = Commond.RequestMenu(Request["hp"]);
                    }
                }
            }
            catch
            {
            }
            if (!IsPostBack)
            {
                Nav_Content();
            }
        }

        private void Nav_Content()
        {
            string strReturn = "";
            strReturn += "";
            switch (u)
            {
                case "99":
                    strReturn += LoadNavPage(hp);
                    break;
                case "1":// nhom tin tuc
                    if (int.TryParse(More.TangNameicid(hp), out _cid))
                    {
                        strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/tin-tuc-new.html\">" + label("l_news") + "</a></li>";
                        strReturn += LoadNav(_cid);
                    }
                    break;
                case "3":// nhom chan trang
                    if (int.TryParse(More.TangNameicid(hp), out _cid))
                    {
                        strReturn += LoadNav(_cid);
                    }
                    break;
                case "5":// nhom Album
                    if (int.TryParse(More.TangNameicid(hp), out _cid))
                    {
                        strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/thu-vien-anh.html\">" + label("Thuvienanh") + "</a></li>";
                        strReturn += LoadNav(_cid);
                    }
                    break;
                case "7":// nhom Video
                    if (int.TryParse(More.TangNameicid(hp), out _cid))
                    {
                        strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/thu-vien-video.html\">" + label("Thuvienvideo") + "</a></li>";
                        strReturn += LoadNav(_cid);
                    }
                    break;
                case "20"://// nhom san pham
                    if (int.TryParse(More.TangNameicid(hp), out _cid))
                    {
                        strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/san-pham.html\">" + label("lproducts") + "</a></li>";
                        strReturn += LoadNav(_cid);
                    }
                    break;
                // Chi tiet

                case "2":
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/tin-tuc-new.html\">" + label("l_news") + "</a></li>";
                    strReturn += LoadNavNews();
                    break;
                case "4":
                    strReturn += LoadNavNewsFooter();
                    break;
                case "6":
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/thu-vien-anh.html\">" + label("Thuvienanh") + "</a></li>";
                    strReturn += LoadNavAllbums();
                    break;
                case "8":
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/thu-vien-video.html\">" + label("Thuvienvideo") + "</a></li>";
                    strReturn += LoadNavVideos();
                    break;
                case "21":
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/san-pham.html\">" + label("lproducts") + "</a></li>";
                    strReturn += LoadNavProduts();
                    break;


            }
            if (Request["su"] != null && !Request["su"].Equals(""))
            {
                if (Request["su"].ToString() == "viewcart" || Request["su"].ToString() == "msg" || Request["su"].ToString() == "msg")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/gio-hang.html\">" + label("lt_cartbox") + "</a></li>";
                }
                if (Request["su"].ToString() == "contact")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/lien-he.html\">" + label("l_contact") + "</a></li>";
                }
                if (Request["su"].ToString() == "nws")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/tin-tuc.html\">" + label("l_news") + "</a></li>";
                }
                if (Request["su"].ToString() == "Thuvien")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/thu-vien-anh.html\">" + label("Thuvienanh") + "</a></li>";
                }
                if (Request["su"].ToString() == "Videos")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/thu-vien-video.html\">" + label("Thuvienvideo") + "</a></li>";
                }
                if (Request["su"].ToString() == "prd")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/san-pham.html\">" + label("lproducts") + "</a></li>";
                }
                if (Request["su"].ToString() == "Download")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/tai-du-lieu.html\">Download</a></li>";
                }
                if (Request["su"].ToString() == "GioiThieu")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/giai-phap.html\">" + label("giaiphap") + "</a></li>";
                }
                if (Request["su"].ToString() == "Search")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"#\">" + label("l_search") + "</a></li>";
                }
                if (Request["su"].ToString() == "Register")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/Dang-ky.html\">" + label("Thanhvien") + "</a></li>";
                }
                if (Request["su"].ToString() == "resetpassword")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/Doi-mat-khau.html\">" + label("lt_changepassword") + "</a></li>";
                }
                if (Request["su"].ToString() == "Infos")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/thong-tin-thanh-vien.html\">" + label("ttthanhvien") + "</a></li>";
                }
                if (Request["su"].ToString() == "changinfo")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/xem-thong-tin-thanh-vien.html\">" + label("capthanhvien") + "</a></li>";
                }
                if (Request["su"].ToString() == "changepass")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/thay-doi-mat-khau.html\">" + label("thaydoimk") + "</a></li>";
                }
                if (Request["su"].ToString() == "Login")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/Dang-nhap.html\">" + label("l_login") + "</a></li>";
                }
                if (Request["su"].ToString() == "Banchay")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/san-pham-ban-chay.html\">" + label("l_prdbestsell") + "</a></li>";
                }
                if (Request["su"].ToString() == "sanphamoi")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/san-pham-moi.html\">" + label("l_prdnews") + "</a></li>";
                }
                if (Request["su"].ToString() == "GoiY")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/san-pham-goi-y.html\">Sản phẩm gợi ý</a></li>";
                }
                if (Request["su"].ToString() == "BanChay")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/san-pham-ban-chay.html\">Sản phẩm bán chạy</a></li>";
                }
                if (Request["su"].ToString() == "KhuyenMai")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/san-pham-khuyen-mai.html\">Sản phẩm khuyến mãi</a></li>";
                }
                if (Request["su"].ToString() == "NoiBat")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/san-pham-noi-bat.html\">Sản phẩm nổi bật</a></li>";
                }
                if (Request["su"].ToString() == "quanlysanpham")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"#\">Quản lý sản phẩm thành viên</a></li>";
                }
                if (Request["su"].ToString() == "HoaHongGioiThieu")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"#\">Danh sách hoa hồng giới thiệu</a></li>";
                }
                if (Request["su"].ToString() == "HoaHongMua")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"#\">Danh sách hoa hồng mua</a></li>";
                }
                if (Request["su"].ToString() == "HoaHongBan")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"#\">Danh sách hoa hồng bán</a></li>";
                }
                if (Request["su"].ToString() == "ChuyenDiem")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"#\">Chuyển điểm thành viên</a></li>";
                }
                if (Request["su"].ToString() == "Lichsu" || Request["su"].ToString() == "Detailorders")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"#\">Lịch sử mua hàng</a></li>";
                }
                if (Request["su"].ToString() == "orderssold" || Request["su"].ToString() == "DonBanHang")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"#\">Danh sách đơn hàng đã bán</a></li>";
                }
                if (Request["su"].ToString() == "quanlygiadaily")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"#\">Quản lý giá đại lý</a></li>";
                }
                if (Request["su"].ToString() == "LichSuCapDiem")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"#\">Lịch sử cấp điểm</a></li>";
                }
                if (Request["su"].ToString() == "LichSuChuyenDiem")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"#\">Lịch sử chuyển điểm</a></li>";
                }
                if (Request["su"].ToString() == "Thanhvien")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"#\">Danh sách thành viên</a></li>";
                }
                if (Request["su"].ToString() == "LichSuRutTien")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"#\">Lịch sử rút điểm</a></li>";
                }
                if (Request["su"].ToString() == "RutTien")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"#\">Rút điểm</a></li>";
                }
                if (Request["su"].ToString() == "ViTienThanhVien")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"#\">Ví thành viên</a></li>";
                }
                if (Request["su"].ToString() == "LinkGoiThieu")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"#\">Giới thiệu link</a></li>";
                }
                if (Request["su"].ToString() == "LaiSuatagland")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"#\">Danh sách lãi suất AG LAND</a></li>";
                }
                if (Request["su"].ToString() == "TroThanhDaiLy")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/san-pham-dieu-kien-tro-thanh-dai-ly.html\">Sản phẩm điều kiện trở thành đại lý</a></li>";
                }
                if (Request["su"].ToString() == "ChienLuoc")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/san-pham-chien-luoc.html\">Sản phẩm chiến lược</a></li>";
                }
                if (Request["su"].ToString() == "ProDetail")
                {
                    strReturn += "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"/san-pham.html\">" + label("lproducts") + "</a></li>";
                    strReturn += LoadNavProduts();
                }


            }
            ltrnav.Text = strReturn;
        }

        private string LoadNavNews()
        {
            New dt = db.News.SingleOrDefault(p => p.TangName == hp);
            if (dt != null)
            {
                var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(dt.icid.ToString()));
                nav = "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
                if (item.Parent_ID != -1)
                {
                    LoadNav(Convert.ToInt32(item.Parent_ID));
                }
            }
            return nav;
        }

        private string LoadNavNewsFooter()
        {
            Nfooter dt = db.Nfooters.SingleOrDefault(p => p.TangName == hp);
            if (dt != null)
            {
                var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(dt.icid.ToString()));
                nav = "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
                if (item.Parent_ID != -1)
                {
                    LoadNav(Convert.ToInt32(item.Parent_ID));
                }
            }
            return nav;
        }

        private string LoadNavAllbums()
        {
            Album dt = db.Albums.SingleOrDefault(p => p.TangName == hp);
            if (dt != null)
            {
                var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(dt.Menu_ID.ToString()));
                nav = "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
                if (item.Parent_ID != -1)
                {
                    LoadNav(Convert.ToInt32(item.Parent_ID));
                }
            }
            return nav;
        }

        private string LoadNavVideos()
        {
            VideoClip dt = db.VideoClips.SingleOrDefault(p => p.TangName == hp);
            if (dt != null)
            {
                var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(dt.Menu_ID.ToString()));
                nav = "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
                if (item.Parent_ID != -1)
                {
                    LoadNav(Convert.ToInt32(item.Parent_ID));
                }
            }
            return nav;
        }

        private string LoadNavProduts()
        {
            product dt = db.products.SingleOrDefault(p => p.ipid == int.Parse(pid));
            if (dt != null)
            {
                var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(dt.icid.ToString()));
                nav = "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
                if (item.Parent_ID != -1)
                {
                    LoadNav(Convert.ToInt32(item.Parent_ID));
                }
            }
            return nav;
        }

        private string LoadNav(int ID)
        {
            var item = db.Menus.FirstOrDefault(s => s.ID == ID);
            if (item != null)
            {
                nav = "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
                if (item.Parent_ID != -1)
                {
                    LoadNav(Convert.ToInt32(item.Parent_ID));
                }
            }
            return nav;
        }
        private string LoadNavPage(string TangName)
        {
            var item = db.Menus.FirstOrDefault(s => s.TangName == TangName & s.capp == More.MN);
            if (item != null)
            {
                nav = "<li><span><i class=\"fa fa-angle-right\"></i></span><a href=\"" + item.TangName.ToString() + ".html\">" + item.Name + "</a></li>" + nav;
                if (item.Parent_ID != -1)
                {
                    LoadNav(Convert.ToInt32(item.Parent_ID));
                }
            }
            return nav;
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}
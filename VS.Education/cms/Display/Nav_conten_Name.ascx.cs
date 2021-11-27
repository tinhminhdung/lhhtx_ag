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
    public partial class Nav_conten_Name : System.Web.UI.UserControl
    {
        string hp = "";
        int iEmptyIndex = 0;
        string nav = "";
        string u = "";
        int _cid = -1;
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
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
                case "1":// nhom tin tuc
                    if (int.TryParse(More.TangNameicid(hp), out _cid)) { strReturn += LoadNav(_cid); }
                    break;
                case "3":// nhom chan trang
                    if (int.TryParse(More.TangNameicid(hp), out _cid)) { strReturn += LoadNav(_cid); }
                    break;
                case "5":// nhom Album
                    if (int.TryParse(More.TangNameicid(hp), out _cid)) { strReturn += LoadNav(_cid); }
                    break;
                case "7":// nhom Video
                    if (int.TryParse(More.TangNameicid(hp), out _cid)) { strReturn += LoadNav(_cid); }
                    break;
                case "20"://// nhom san pham
                    if (int.TryParse(More.TangNameicid(hp), out _cid)) { strReturn += LoadNav(_cid); }
                    break;
                case "99"://// nhom san pham
                    if (int.TryParse(More.TangNameicid(hp), out _cid)) { strReturn += LoadNav(_cid); }
                    break;
                // Chi tiet

                case "2":
                    strReturn += LoadNavNewsDt();
                    break;
                case "4":
                    strReturn += LoadNavNewsFooter();
                    break;
                case "6":
                    strReturn += LoadNavAllbums();
                    break;
                case "8":
                    strReturn += LoadNavVideos();
                    break;
                case "21":
                    strReturn += LoadNavProdutsDt();
                    break;

            }
            if (Request["su"] != null && !Request["su"].Equals(""))
            {
                if (Request["su"].ToString() == "viewcart" || Request["su"].ToString() == "msg" || Request["su"].ToString() == "msg")
                {
                    strReturn += "<h1>Giỏ hàng của bạn</h1>";
                }
                if (Request["su"].ToString() == "contact")
                {
                    strReturn += "<h1>Liên hệ</h1>";
                }
                if (Request["su"].ToString() == "nws")
                {
                    strReturn += "<h1>Tin tức</h1>";
                }
                if (Request["su"].ToString() == "Allbums")
                {
                    strReturn += "<h1>Thư viện ảnh</h1>";
                }
                if (Request["su"].ToString() == "Videos")
                {
                    strReturn += "<h1>Thư viện video</h1>";
                }
                if (Request["su"].ToString() == "prd")
                {
                    strReturn += "<h1>Sản phẩm</h1>";
                }
                if (Request["su"].ToString() == "Download")
                {
                    strReturn += "<h1>Download</h1>";
                }
                if (Request["su"].ToString() == "Gioithieu")
                {
                    strReturn += "<h1>Giới thiệu</h1>";
                }
                if (Request["su"].ToString() == "Search")
                {
                    strReturn += "<h1>Tìm kiếm</h1>";
                }
                if (Request["su"].ToString() == "Register")
                {
                    strReturn += "<h1>Đăng ký thành viên</h1>";
                }
                if (Request["su"].ToString() == "resetpassword")
                {
                    strReturn += "<h1>Đổi mật khẩu</h1>";
                }
                if (Request["su"].ToString() == "Infos")
                {
                    strReturn += "<h1>Thông tin thành viên</h1>";
                }
                if (Request["su"].ToString() == "changinfo")
                {
                    strReturn += "<h1>Cập nhật thông tin</h1>";
                }
                if (Request["su"].ToString() == "changepass")
                {
                    strReturn += "<h1>Thay đổi mật khẩu</h1>";
                }
                if (Request["su"].ToString() == "Login")
                {
                    strReturn += "<h1>Đăng nhập</h1>";
                }
                if (Request["su"].ToString() == "Page")
                {
                    strReturn += "<h1>Lỗi 404</h1>";
                }
            }
            ltrnav.Text = strReturn;
        }
        private string LoadNavNewsDt()
        {
            string str = "";
            New dt = db.News.SingleOrDefault(p => p.TangName == hp);
            if (dt != null)
            {
                var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(dt.icid.ToString()));
                nav = "<h2>" + item.Name + "</h2>";
            }
            return nav + str;
        }
        private string LoadNavProdutsDt()
        {
            string str = "";
            product dt = db.products.SingleOrDefault(p => p.TangName == hp);
            if (dt != null)
            {
                var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(dt.icid.ToString()));
                nav = "<h2>" + item.Name + "</h2>";
            }
            return nav + str;
        }

        private string LoadNavNewsFooter()
        {
            Nfooter dt = db.Nfooters.SingleOrDefault(p => p.TangName == hp);
            if (dt != null)
            {
                var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(dt.icid.ToString()));
                nav = "<h2>" + item.Name + "</h2>";
            }
            return nav;
        }

        private string LoadNavAllbums()
        {
            Album dt = db.Albums.SingleOrDefault(p => p.TangName == hp);
            if (dt != null)
            {
                var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(dt.Menu_ID.ToString()));
                nav = "<h2>" + item.Name + "</h2>";
            }
            return nav;
        }

        private string LoadNavVideos()
        {
            VideoClip dt = db.VideoClips.SingleOrDefault(p => p.TangName == hp);
            if (dt != null)
            {
                var item = db.Menus.FirstOrDefault(s => s.ID == int.Parse(dt.Menu_ID.ToString()));
                nav = "<h2>" + item.Name + "</h2>";
            }
            return nav;
        }

        private string LoadNav(int ID)
        {
            var item = db.Menus.FirstOrDefault(s => s.ID == ID);
            if (item != null)
            {
                nav = "<h1>" + item.Name + "</h1>";
            }
            return nav;
        }
    }
}
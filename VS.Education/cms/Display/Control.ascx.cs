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
    public partial class Control : System.Web.UI.UserControl
    {
        #region string
        #endregion
        string hp = "";
        int iEmptyIndex = 0;
        public string Moldul = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            DatalinqDataContext db = new DatalinqDataContext();
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
            #region Request Modul
            if (Request["e"] != null)
            {
                if (Request["e"].ToString() == "load")
                {
                    Moldul = Commond.RequestMenu(Request["hp"]);
                }
            }
            #endregion
            #endregion
            if (Request["su"] == null && Request["e"] == null)
            {
                phcontrol.Controls.Add(LoadControl("~/cms/Display/index.ascx"));
            }
            if (Request["su"] == "TroThanhDaiLy" || Request["su"] == "ChienLuoc" || Request["su"] == "ChienLuoc" || Request["su"] == "BanChay" || Request["su"] == "KhuyenMai" || Request["su"] == "NoiBat" || Request["su"] == "prd" || Request["su"] == "Search" || Moldul == "20")
            {
                phcontrol.Controls.Add(LoadControl("~/cms/Display/Control/ControlPro.ascx"));
            }
            if (Request["su"] == "msg" || Request["su"] == "viewcart")
            {
                phcontrol.Controls.Add(LoadControl("~/cms/Display/Control/ControlProCart.ascx"));
            }
            //if (Moldul == "21")
            //{
            //    phcontrol.Controls.Add(LoadControl("~/cms/Display/Products/Detail.ascx"));
            //}
            if (Request["su"] == "nws" || Request["su"] == "Page" || Moldul == "1" || Moldul == "2" || Moldul == "99")
            {
                phcontrol.Controls.Add(LoadControl("~/cms/Display/Control/ControlNews.ascx"));
            }

            if (Request["su"] == "ProDetail")
            {
                phcontrol.Controls.Add(LoadControl("~/cms/Display/Products/Detail.ascx"));
            }

            if (Request["su"] == "ShopNhaCC")
            {
                phcontrol.Controls.Add(LoadControl("~/cms/Display/ShopNhaCC/HomeShop.ascx"));
            }

            if (Request["su"] == "contact")
            {
                phcontrol.Controls.Add(LoadControl("~/cms/Display/contact/contact.ascx"));
            }
            if (Request["su"] == "ThanhToanQRCode")
            {
                phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/ThanhToanQRCode.ascx"));
            }
            if (Request["su"] == "Login" || Request["su"] == "Register")
            {
                phcontrol.Controls.Add(LoadControl("~/cms/Display/Control/ControlThanhVien.ascx"));
            }
            //|| Request["su"] == "resetpassword" || Request["su"] == "changinfo" || Request["su"] == "changepass" || Request["su"] == "Infos" ||
            if (Request["su"] == "LSDautu" || Request["su"] == "Dautu" || Request["su"] == "ViTienThanhVien" || Request["su"] == "ChuyenDiemViDiem" || Request["su"] == "Chuyengia" || Request["su"] == "TinNhan" || Request["su"] == "Lichsutruthue" || Request["su"] == "LaiSuatagland" || Request["su"] == "lichsuthanhtoanqrcode" || Request["su"] == "lichsuduyetvitamgiu" || Request["su"] == "LichSuCapDiem" || Request["su"] == "HoaHongQRcode" || Request["su"] == "LichSuMuaDiem" || Request["su"] == "MuaDiem" || Request["su"] == "RutTien" || Request["su"] == "LichSuRutTien" || Request["su"] == "quanlygiadaily" || Request["su"] == "Thanhvien" || Request["su"] == "LichSuChuyenDiem" || Request["su"] == "LinkGoiThieu" || Request["su"] == "Lichsu" || Request["su"] == "orderssold" || Request["su"] == "DonBanHang" || Request["su"] == "Detailorders" || Request["su"] == "HoaHongGioiThieu" || Request["su"] == "HoaHongMua" || Request["su"] == "HoaHongBan" || Request["su"] == "HoaHong" || Request["su"] == "ChuyenDiem" || Request["su"] == "quanlysanpham" || Request["su"] == "resetpassword" || Request["su"] == "changinfo" || Request["su"] == "changepass" || Request["su"] == "Infos")
            {
                phcontrol.Controls.Add(LoadControl("~/cms/Display/Control/ControlThanhVien_QuanLy.ascx"));
            }
        }
    }
}

//    {
//        #region string
//        private string language = Captionlanguage.Language;
//        #endregion
//        string hp = "";
//        int iEmptyIndex = 0;
//        public string Moldul = "";
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            DatalinqDataContext db = new DatalinqDataContext();
//            #region #
//            if (System.Web.HttpContext.Current.Session["language"] != null)
//            {
//                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
//            }
//            else
//            {
//                System.Web.HttpContext.Current.Session["language"] = this.language;
//                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
//            }
//            #endregion

//            // Điều khiển hay phcontrol.Controls như trang Global.edu.vn 
//            if (Request["su"] == null && Request["e"] == null)
//            {
//                phHome.Controls.Add(LoadControl("index.ascx"));
//            }

//            #region Requesthp
//            if (Request["hp"] != null && !Request["hp"].Equals(""))
//            {
//                hp = Request["hp"].ToString();
//            }
//            iEmptyIndex = hp.IndexOf("?");
//            if (iEmptyIndex != -1)
//            {
//                hp = hp.Substring(0, iEmptyIndex);
//            }

//            #endregion
//            #region Request_e
//            //try
//            // {
//            if (Request["e"] != null)
//            {
//                if (Request["e"].ToString() == "load")
//                {
//                    string request = Request["hp"] != null ? Request["hp"].ToString() : Request.Path;
//                    string t = Request["hp"].ToString() + ".html";
//                    if (!request.ToLower().Contains("index.aspx"))
//                    {

//                        Moldul = Commond.RequestMenu(Request["hp"]);
//                        switch (Moldul)
//                        {
//                            case "99":
//                                phcontrol.Controls.Add(LoadControl("Page/Detail.ascx"));
//                                break;

//                            //News
//                            case "1":
//                                // Menu Vitri = db.Menus.FirstOrDefault(l => l.TangName == hp);
//                                //if (Vitri.News == 0)
//                                phcontrol.Controls.Add(LoadControl("News/Category.ascx"));
//                                // else
//                                //  phcontrol.Controls.Add(LoadControl("News/DCategory.ascx"));
//                                break;

//                            case "2":
//                                phcontrol.Controls.Add(LoadControl("News/Detail.ascx"));
//                                break;

//                            //NewsFooter
//                            case "3":
//                                phcontrol.Controls.Add(LoadControl("NewsFooter/Category.ascx"));
//                                break;
//                            case "4":
//                                phcontrol.Controls.Add(LoadControl("NewsFooter/Detail.ascx"));
//                                break;

//                            //Allbum
//                            case "5":
//                                phcontrol.Controls.Add(LoadControl("Allbums/Category.ascx"));
//                                break;
//                            case "6":
//                                phcontrol.Controls.Add(LoadControl("Allbums/Detail.ascx"));
//                                break;

//                            //Video
//                            case "7":
//                                phcontrol.Controls.Add(LoadControl("Videos/Category.ascx"));
//                                break;
//                            case "8":
//                                phcontrol.Controls.Add(LoadControl("Videos/Detail.ascx"));
//                                break;

//                            //Download

//                            case "10":
//                                phcontrol.Controls.Add(LoadControl("Download/Detail.ascx"));
//                                break;

//                            //GioiThieu
//                            case "11":
//                                phcontrol.Controls.Add(LoadControl("GioiThieu/GioiThieu.ascx"));
//                                break;

//                            //Faq
//                            case "12":
//                                phcontrol.Controls.Add(LoadControl("Faq/Detail.ascx"));
//                                break;

//                            //Dichvu
//                            case "13":
//                                phcontrol.Controls.Add(LoadControl("Dichvu/Detail.ascx"));
//                                break;

//                            //Products
//                            case "23":
//                                phcontrol.Controls.Add(LoadControl("Products/Hangsanpham.ascx"));
//                                break;

//                            case "20":
//                                // Menu Vitris = db.Menus.FirstOrDefault(l => l.TangName == hp);
//                                // if (Vitris.Parent_ID == -1)
//                                // phcontrol.Controls.Add(LoadControl("Products/DCategory.ascx"));
//                                // else
//                                phcontrol.Controls.Add(LoadControl("Products/Category.ascx"));
//                                break;

//                            case "21":
//                                phcontrol.Controls.Add(LoadControl("Products/Detail.ascx"));
//                                break;
//                        }
//                    }
//                }

//            }
//            //}
//            //catch (Exception)
//            //{
//            //    Response.Redirect("/page-404.html");
//            //}
//            #endregion
//            #region Request_su
//            switch (Request["su"])
//            {
//                case "Page":
//                    phcontrol.Controls.Add(LoadControl("Page_404.ascx"));
//                    break;
//                //Products
//                case "khuyenmai":
//                    phcontrol.Controls.Add(LoadControl("Products/khuyenmai.ascx"));
//                    break;
//                case "prd":
//                    phcontrol.Controls.Add(LoadControl("Products/index.ascx"));
//                    break;
//                case "viewcart":
//                    phcontrol.Controls.Add(LoadControl("Products/Cart.ascx"));
//                    break;
//                case "msg":
//                    phcontrol.Controls.Add(LoadControl("Products/Message.ascx"));
//                    break;
//                case "Search":
//                    phcontrol.Controls.Add(LoadControl("Products/Search.ascx"));
//                    break;
//                //case "Search":
//                //    phcontrol.Controls.Add(LoadControl("News/Search.ascx"));
//                //    break;
//                //contact
//                case "contact":
//                    phcontrol.Controls.Add(LoadControl("contact/contact.ascx"));
//                    break;
//                //News
//                case "nws":
//                    phcontrol.Controls.Add(LoadControl("News/Index.ascx"));
//                    break;
//                //Download
//                case "Download":
//                    phcontrol.Controls.Add(LoadControl("Download/Category.ascx"));
//                    break;
//                //GioiThieu
//                case "GioiThieu":
//                    phcontrol.Controls.Add(LoadControl("GioiThieu/GioiThieu.ascx"));
//                    break;
//                //Faq
//                case "Faq":
//                    phcontrol.Controls.Add(LoadControl("Faq/Category.ascx"));
//                    break;
//                //GioiThieu
//                case "Dichvu":
//                    phcontrol.Controls.Add(LoadControl("Dichvu/Category.ascx"));
//                    break;
//                //Allbums
//                case "Thuvien":
//                    phcontrol.Controls.Add(LoadControl("Allbums/Index.ascx"));
//                    break;
//                //Videos
//                case "Videos":
//                    phcontrol.Controls.Add(LoadControl("Videos/Index.ascx"));
//                    break;
//                //Members
//                case "Register":
//                    phcontrol.Controls.Add(LoadControl("Members/Register.ascx"));
//                    break;
//                case "Infos":
//                    if (HttpContext.Current.Request.Cookies["Members"] != null)
//                    {
//                        phcontrol.Controls.Add(LoadControl("Members/Info.ascx"));
//                    }
//                    else
//                    {
//                        phcontrol.Controls.Add(LoadControl("Members/Login.ascx"));
//                    }
//                    break;
//                case "changepass":
//                    if (HttpContext.Current.Request.Cookies["Members"] != null)
//                    {
//                        this.phcontrol.Controls.Add(LoadControl("Members/Changepassword.ascx"));
//                    }
//                    else
//                    {
//                        phcontrol.Controls.Add(LoadControl("Members/Login.ascx"));
//                    }
//                    return;
//                case "changinfo":
//                    if (HttpContext.Current.Request.Cookies["Members"] != null)
//                    {
//                        this.phcontrol.Controls.Add(LoadControl("Members/Changinfo.ascx"));
//                    }
//                    else
//                    {
//                        phcontrol.Controls.Add(LoadControl("Members/Login.ascx"));
//                    }
//                    return;
//                case "resetpassword":
//                    phcontrol.Controls.Add(LoadControl("Members/Resetpassword.ascx"));
//                    break;
//                case "Login":
//                    phcontrol.Controls.Add(LoadControl("Members/Login.ascx"));
//                    break;

//                case "info":
//                    this.phcontrol.Controls.Add(base.LoadControl("Contents/Content.ascx"));
//                    break;
//                case "infoco":
//                    phcontrol.Controls.Add(LoadControl("Contents/Detail.ascx"));
//                    break;
//            }
//            #endregion

//        }
//        //protected string Nhom()
//        //{
//        //    string strReturn = "";
//        //    if (Moldul == "1")
//        //        strReturn = More.MenuDacap(More.TangNameicid(hp));
//        //    if (Moldul == "2")
//        //        strReturn = More.MenuDacap(More.TangNameNewsicid(hp));
//        //    return strReturn;
//        //}
//        //protected string CheckNhom()
//        //{
//        //    string strReturn = "";
//        //    DatalinqDataContext db = new DatalinqDataContext();
//        //    List<Entity.Menu> list = SMenu.Name_Text("SELECT * FROM Menu where News=1 and  Module=1");
//        //    if (list != null)
//        //    {
//        //        strReturn += list[0].ID.ToString();
//        //    }
//        //    return strReturn;
//        //}
//        protected string label(string id)
//        {
//            return Captionlanguage.GetLabel(id, this.language);
//        }
//    }
//}
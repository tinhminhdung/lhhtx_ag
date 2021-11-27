using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using System.Data;
using Services;
using Framework;
using Entity;

namespace VS.E_Commerce.cms.Display
{
    public partial class Lefmenu : System.Web.UI.UserControl
    {
        #region string
        private string language = Captionlanguage.Language;
        string hp = "";
        int iEmptyIndex = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            #region #
            if (System.Web.HttpContext.Current.Session["language"] != null)
            {
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["language"] = this.language;
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            #endregion
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
                if (Commond.Setting("CooKie").Equals("1"))
                {
                    ltMenuPro.Text = MoreAll.MoreAll.GetCache("MenuPro", HttpContext.Current.Cache["MenuPro"] != null ? "" : ShowMenu());
                    ltShowLoadProNoiBat.Text = MoreAll.MoreAll.GetCache("ShowLoadProNoiBat", HttpContext.Current.Cache["ShowLoadProNoiBat"] != null ? "" : ShowLoadProNoiBat());
                    //ltthuonghieu.Text = MoreAll.MoreAll.GetCache("LocThuongHieu", HttpContext.Current.Cache["LocThuongHieu"] != null ? "" : LocThuongHieu());
                    ltloctheogia.Text = MoreAll.MoreAll.GetCache("LocTheoGia", HttpContext.Current.Cache["LocTheoGia"] != null ? "" : LocTheoGia());
                }
                else
                {
                    ltMenuPro.Text = ShowMenu();
                    ltShowLoadProNoiBat.Text = ShowLoadProNoiBat();
                    //ltthuonghieu.Text = LocThuongHieu();
                    ltloctheogia.Text = LocTheoGia();
                }
            }
        }
        protected string ShowLoadProNoiBat()//Sản phẩm nooir bật
        {
            string str = "";
            List<Entity.Category_Product> table = SProducts.Name_Text_Rg("SELECT top 5 " + Commond.Sql_Product() + " FROM [products] WHERE  News=1 and  lang='" + language + "'  AND Status=1  order by Create_Date desc");
            if (table.Count >= 1)
            {
                str += Commond.LoadProductList_NoiBatMenu(table);
            }
            return str;
        }
        protected string ShowMenu()
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.capp_Lang_Parent_ID_Status(More.PR, language, "-1", "1");
            if (dt.Count > 0)
            {
                foreach (Entity.Menu item in dt)
                {
                    str += "<li class=\"nav-item\"><i class=\"fa fa-caret-right\"></i><a class=\"nav-link\"  href='/" + item.TangName.ToString() + ".html'>" + item.Name.ToString() + "</a> ";
                    str += MenuNews(item.ID.ToString());
                    str += "</li>";
                }
            }
            return str.ToString();
        }
        protected string MenuNews(string id)
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.capp_Lang_Parent_ID_Status(More.PR, language, id, "1");
            if (dt.Count > 0)
            {
                str += "<i class=\"fa fa-angle-down\"></i><ul class=\"dropdown-menu\">";
                foreach (Entity.Menu item in dt)
                {
                    str += "<li class=\"dropdown-submenu nav-item\"><i class=\"fa fa-caret-right\"></i><a  class=\"nav-link\" href='/" + item.TangName.ToString() + ".html'>" + item.Name.ToString() + "</a>" + MenuNews(item.ID.ToString()) + "</li>";
                }
                str += "</ul>";
            }
            return str.ToString();
        }
        protected string LocThuongHieu()
        {
            string str = "";
            try
            {
                List<Entity.Menu> dt = SMenu.Name_Text("SELECT * FROM [Menu]  where  capp='" + More.HA + "' and Lang='" + language + "'  and Status=1 order by Orders asc");
                if (dt.Count > 0)
                {
                    foreach (Entity.Menu item in dt)
                    {
                        str += "<li><a href=\"javascript:void(0)\"  rel=\"nofollow\" id=\"" + item.ID + "\" title=\"" + item.Name + "\" class=\"sort_list\"  onclick=\"choose_produce(this)\"> " + item.Name.ToString() + " </a></li>";
                    }
                }
            }
            catch (Exception)
            { }
            return str.ToString();
        }
        protected string LocTheoGia()
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.Name_Text("SELECT * FROM [Menu]  where  capp='" + More.KG + "' and Lang='" + language + "'  and Parent_ID=-1 and Status=1 order by Orders asc");
            if (dt.Count > 0)
            {
                foreach (Entity.Menu item in dt)
                {
                    str += "<li><a href=\"javascript:void(0)\"  rel=\"nofollow\" name=\"" + item.ID + "\" title=\"" + item.Name + "\" class=\"sort_list\"  onclick=\"choose_price(this)\"> " + item.Name.ToString() + " </a></li>";
                }
            }
            return str.ToString();
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}
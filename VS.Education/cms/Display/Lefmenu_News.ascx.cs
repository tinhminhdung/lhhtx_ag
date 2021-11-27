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
    public partial class Lefmenu_News : System.Web.UI.UserControl
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
                    ltShowtinxemnhieu.Text = MoreAll.MoreAll.GetCache("Showtinxemnhieu", HttpContext.Current.Cache["Showtinxemnhieu"] != null ? "" : Showtinxemnhieu());
                    ltMenuNews.Text = MoreAll.MoreAll.GetCache("MenuNews", HttpContext.Current.Cache["MenuNews"] != null ? "" : ShowMenu());
                }
                else
                {
                    ltShowtinxemnhieu.Text = Showtinxemnhieu();
                    ltMenuNews.Text = ShowMenu();
                }
            }
        }
        protected string Showtinxemnhieu()
        {
            string str = "";
            List<Entity.Category_News> dt = SNews.Name_Text_Rg("SELECT top 6 inid,TangName,Title,Images,ImagesSmall,Brief,Create_Date,alt FROM [News] WHERE Status=1  order by Views desc");
            if (dt.Count > 0)
            {
                str += Commond.LoadNewsListXemNhieu(dt, language);
            }
            return str;
        }
        protected string ShowMenu()
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.capp_Lang_Parent_ID_Status(More.NS, language, "-1", "1");
            if (dt.Count > 0)
            {
                foreach (Entity.Menu item in dt)
                {
                    str += "<li class=\"nav-item\"><a class=\"nav-link\"  href='/" + item.TangName.ToString() + ".html'>" + item.Name.ToString() + "</a> ";
                    str += MenuNewss(item.ID.ToString());
                    str += "</li>";
                }
            }
            return str.ToString();
        }
        protected string MenuNewss(string id)
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.capp_Lang_Parent_ID_Status(More.NS, language, id, "1");
            if (dt.Count > 0)
            {
                str += "<i class=\"fa fa-angle-down\"></i><ul class=\"dropdown-menu\">";
                foreach (Entity.Menu item in dt)
                {
                    str += "<li class=\"dropdown-submenu nav-item\"><a  class=\"nav-link\" href='/" + item.TangName.ToString() + ".html'>" + item.Name.ToString() + "</a>" + MenuNewss(item.ID.ToString()) + "</li>";
                }
                str += "</ul>";
            }
            return str.ToString();
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}
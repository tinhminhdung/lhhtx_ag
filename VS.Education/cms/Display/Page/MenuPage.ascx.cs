using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.Page
{
    public partial class MenuPage : System.Web.UI.UserControl
    {
        #region string
        private string language = Captionlanguage.Language;
        string hp = "";
        int iEmptyIndex = 0;
        #endregion
        public string Case = "";
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
                #region Module
                try
                {
                    if (Request["e"] != null)
                    {
                        if (Request["e"].ToString() == "load")
                        {
                            Case = Commond.RequestMenu(Request["hp"]);
                        }
                    }
                }
                catch (Exception)
                {
                }
                #endregion
                ltmenu.Text = ShowMenuPage();
            }
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }

        protected string ShowMenuPage()
        {
            string str = "";
            List<Entity.Menu_OK> dt2 = SMenu.Name_Text_Rg("SELECT capp,Create_Date,Description,Equals,ID,Images,Lang,Level,Link,Module,Name,Orders,Parent_ID,ShowID,Styleshow,TangName,Type,Url_Name,Views FROM Menu where capp='" + More.MN + "'  and  len([Level])= 5 and [Level] like '" + RequestMenuLevel(Request["hp"]) + "%'   and Views=1  and lang='" + language + "'  and status=1 order by level,Orders asc");
            if (dt2.Count > 0)
            {
                str += "<li class=\"header\"><a  href='/" + dt2[0].TangName.ToString() + ".html'>" + dt2[0].Name.ToString() + "</a></li>";
            }
            List<Entity.Menu_OK> dt = SMenu.Name_Text_Rg("SELECT capp,Create_Date,Description,Equals,ID,Images,Lang,Level,Link,Module,Name,Orders,Parent_ID,ShowID,Styleshow,TangName,Type,Url_Name,Views  FROM Menu where capp='" + More.MN + "'  and  len([Level]) >= 10 and [Level] like '" + RequestMenuLevel(Request["hp"]) + "%'   and Views=1  and lang='" + language + "'  and status=1 order by level,Orders asc");
            if (dt.Count > 0)
            {
                foreach (Entity.Menu_OK item in dt)
                {
                    if (More.MenuDacap(More.TangNameicid(hp)) == More.MenuDacap(item.ID.ToString()))
                    {
                        str += "<li  class=\"activer\"><a  href='/" + item.TangName.ToString() + ".html'>" + item.Name.ToString() + "</a></li>";
                    }
                    else
                    {
                        str += "<li><a  href='/" + item.TangName.ToString() + ".html'>" + item.Name.ToString() + "</a></li>";
                    }
                }
            }
            return str.ToString();
        }

        public string RequestMenuLevel(string Request)
        {
            string Modul = "";
            List<Entity.Menu_OK> str = SMenu.Name_Text_Rg("SELECT top 1 capp,Create_Date,Description,Equals,ID,Images,Lang,Level,Link,Module,Name,Orders,Parent_ID,ShowID,Styleshow,TangName,Type,Url_Name,Views FROM [Menu]  where TangName='" + Request + "'");
            if (str.Count > 0 || str != null)
            {
                Modul = str[0].Level.Substring(0, 5).ToString();
            }
            return Modul;
        }
		
		// // cách 2 cho menu sản phẩm như trang global.edu.vn
		//protected string ShowMenuPage()
        //{
        //    string str = "";
        //    Menu dt2 = db.Menus.SingleOrDefault(p => p.ID == int.Parse(More.MenuDacap(More.TangNameicid(hp))) && p.capp == More.PR);
        //    if (dt2 != null)
        //    {
        //        str += "<div class=\"filter-category \">";
        //        str += "<div class=\"categoryleft\">";
        //        str += "<div class=\"title\">" + dt2.Name.ToString() + "</div>";

        //        List<Entity.Menu> dt = SMenu.Name_Text("SELECT * FROM Menu where capp='" + More.PR + "' and Parent_ID=" + dt2.ID.ToString() + "   and status=1 order by level,Orders asc");
        //        if (dt.Count > 0)
        //        {
        //            str += "<ul>";
        //            foreach (Entity.Menu item in dt)
        //            {
        //                //if (More.MenuDacap(More.TangNameicid(hp)) == More.MenuDacap(item.ID.ToString()))
        //                //{
        //                //    str += "<li  class=\"activer\"><a  href='/" + item.TangName.ToString() + ".html'>" + item.Name.ToString() + "</a></li>";
        //                //}
        //                //else
        //                //{
        //                str += "<li><a  href='/" + item.TangName.ToString() + ".html'>" + item.Name.ToString() + "</a></li>";
        //                //}
        //            }
        //            str += "</ul>";
        //            str += "</div>";
        //            str += "</div>";
        //        }
        //    }
        //    return str.ToString();
        //}
    }
}
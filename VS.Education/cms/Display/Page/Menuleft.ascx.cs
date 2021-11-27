using Entity;
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
    public partial class Menuleft : System.Web.UI.UserControl
    {
        #region string
        private string language = Captionlanguage.Language;
        #endregion
        string hp = "";
        int iEmptyIndex = 0;
        DatalinqDataContext db = new DatalinqDataContext();
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

            if (!IsPostBack)
            {
                if (Commond.Setting("CooKie").Equals("1"))
                {
                    ltMenuleft.Text = MoreAll.MoreAll.GetCache("Menuleft", HttpContext.Current.Cache["Menuleft"] != null ? "" : ShowMenuleft());
                }
                else
                {
                    ltMenuleft.Text = ShowMenuleft();
                }

            }
        }
        protected string ShowMenuleft()
        {
            string str = "";
            List<Entity.Menu_OK> table = SMenu.Name_Text_Rg("SELECT capp,Create_Date,Description,Equals,ID,Images,Lang,Level,Link,Module,Name,Orders,Parent_ID,ShowID,Styleshow,TangName,Type,Url_Name,Views FROM Menu where capp='" + More.MN + "' and lang='" + language + "' and Views=3 and status=1 order by level,Orders asc");//Views là vị trí menu top
            //List<Entity.Menu> table = SMenu.Name_Text("SELECT * FROM Menu where capp='" + More.MN + "' and lang='" + language + "' and Views=3 and status=1 order by level,Orders asc");//Views là vị trí menu top
            table = table.Where(s => s.Level.Length == 5).ToList();
            if (table != null)
            {
                for (int i = 0; i < table.Count; i++)
                {
                    string Link = "";
                    if (table[i].ShowID == 2)
                    {
                        Link = "/" + table[i].TangName + ".html";
                    }
                    else if (table[i].ShowID == 3)
                    {
                        Link = table[i].Link;
                    }
                    else
                    {
                        if (table[i].Link == "/")
                        {
                            Link = table[i].Link;
                        }
                        else
                        {
                            Link = "/" + table[i].Link;
                        }
                    }


                    str += "<li class=\"lev-1 nav-item clearfix has-mega mega-menu\">";
                    if (table[i].Images.Length > 0)
                        str += "<div class=\"icon\"><img class='iicon' src=\"" + table[i].Images + "\" alt=\"" + table[i].Name + "\"></div>";
                    str += "<a href=\"" + Link + "\">";
                    str += "<span>" + table[i].Name + "</span>";
                    // str += "<p>Khuyến mại giá cực sốc</p>";
                    str += MenuDacap(table[i].Level.ToString());
                    str += "</a>";
                    str += submenu(table[i].Level.ToString());
                    str += "</li>";

                }
            }
            return (str);
        }
        protected string submenu(string id)
        {
            string str = "";
            List<Entity.Menu_OK> list = SMenu.Name_Text_Rg("SELECT  capp,Create_Date,Description,Equals,ID,Images,Lang,Level,Link,Module,Name,Orders,Parent_ID,ShowID,Styleshow,TangName,Type,Url_Name,Views FROM Menu where capp='" + More.MN + "' and lang='" + language + "' and Views=3 and status=1 and  len([Level]) = 10 and [Level] like '" + id + "%'  order by level,Orders asc");//Views là vị trí menu top
            if (list != null)
            {
                str += "<ul class=\"dropdown-menu mega-menu-content clearfix \"><li class=\"col-sm-12\">  <ul> ";
                foreach (Menu_OK item in list)
                {
                    string Link = "";
                    if (item.ShowID == 2)
                    {
                        Link = "/" + item.TangName + ".html";
                    }
                    else if (item.ShowID == 3)
                    {
                        Link = item.Link;
                    }
                    else
                    {
                        if (item.Link == "/")
                        {
                            Link = item.Link;
                        }
                        else
                        {
                            Link = "/" + item.Link;
                        }
                    }
                    str += "<li class=\"col-sm-3\"><ul class=\"mega-item\"> <li class=\"h3\"><a  href='" + Link + "'> <span> " + item.Name + " </span> </a></li>" + submenu3(item.Level.ToString()) + "</ul></li>";
                }
                str += "</ul></li></ul>";
            }

            return str.ToString();
        }

        protected string submenu3(string id)
        {
            string str = "";
            List<Entity.Menu_OK> list = SMenu.Name_Text_Rg("SELECT  capp,Create_Date,Description,Equals,ID,Images,Lang,Level,Link,Module,Name,Orders,Parent_ID,ShowID,Styleshow,TangName,Type,Url_Name,Views FROM Menu where capp='" + More.MN + "' and lang='" + language + "' and Views=3 and status=1 and  len([Level]) = 15 and [Level] like '" + id + "%'  order by level,Orders asc");//Views là vị trí menu top
            if (list != null)
            {
                str += "";
                foreach (Menu_OK item in list)
                {
                    string Link = "";
                    if (item.ShowID == 2)
                    {
                        Link = "/" + item.TangName + ".html";
                    }
                    else if (item.ShowID == 3)
                    {
                        Link = item.Link;
                    }
                    else
                    {
                        if (item.Link == "/")
                        {
                            Link = item.Link;
                        }
                        else
                        {
                            Link = "/" + item.Link;
                        }
                    }
                    str += "<li class=\"lev-2 nav-item\"><a class=\"nav-link\" href='" + Link + "'>" + item.Name.ToString() + "</a></li>";
                }
            }

            return str.ToString();
        }
        public string MenuDacap(string cat)
        {
            string str = "";
            List<Entity.Menu> list = SMenu.Name_Text("SELECT * FROM Menu where capp='" + More.MN + "' and lang='" + language + "' and Views=3 and status=1 and  len([Level]) >= 10 and [Level] like '" + cat + "%'  order by level,Orders asc");//Views là vị trí menu top
            if (list.Count > 0)
            {
                return " <i class=\"fa fa-angle-right\"></i>";
            }
            return str;
        }
    }
}
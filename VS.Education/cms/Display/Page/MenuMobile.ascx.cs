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
    public partial class MenuMobile : System.Web.UI.UserControl
    {
        #region string
        string currLevel = "";
        string cid = "-1";
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
                currLevel = GetCurrentLevel();
                if (Commond.Setting("CooKie").Equals("1"))
                {
                    ltrMainMenu.Text = MoreAll.MoreAll.GetCache("MenuMobile", HttpContext.Current.Cache["MenuMobile"] != null ? "" : ShowMenuTop());
                }
                else
                {
                    ltrMainMenu.Text = ShowMenuTop();
                }
            }
        }

        private string GetCurrentLevel()
        {
            string culevel = "";
            string curLink = "";
            var curURL = Request.RawUrl;
            curLink = Request["hp"] != null ? Request["hp"] : curURL.Substring(curURL.LastIndexOf("/") + 1);
            curLink = Request["hp"] != null ? curLink + ".html" : curLink;
            var curPage = db.Menus.FirstOrDefault(s => s.Link == curLink && s.Views == 1);
            var homePage = db.Menus.FirstOrDefault(s => s.Link == "/" && s.TangName.Contains("trang-chu"));
            if (homePage != null)
                culevel = homePage.Level;
            try
            {
                string pagtag = Request["hp"] != null ? Request["hp"] : curURL.Substring(curURL.LastIndexOf("/") + 1);
                var pagtytin = db.Menus.Where(u => u.TangName == pagtag).FirstOrDefault();
                if (pagtytin.Module == 2)
                {
                    New tbnew = db.News.SingleOrDefault(p => p.TangName == pagtag);
                    var data = db.Menus.Where(u => u.ID == tbnew.icid).FirstOrDefault();

                    var xx = db.Menus.Where(u => u.Level == data.Level.Substring(0, 5) && u.capp == "NS").FirstOrDefault();
                    string chuoi = xx.TangName + ".html";

                    var kq = db.Menus.Where(u => u.Link == chuoi).FirstOrDefault();
                    return kq.Level.Substring(0, 5);
                }
            }
            catch (Exception) { }
            try
            {
                string pagtag = Request["hp"] != null ? Request["hp"] : curURL.Substring(curURL.LastIndexOf("/") + 1);
                var pagtytin = db.Menus.Where(u => u.TangName == pagtag).FirstOrDefault();
                if (pagtytin.Module == 21)
                {
                    product tbnew = db.products.SingleOrDefault(p => p.TangName == pagtag);
                    var data = db.Menus.Where(u => u.ID == int.Parse(tbnew.icid.ToString())).FirstOrDefault();

                    var xx = db.Menus.Where(u => u.Level == data.Level.Substring(0, 5) && u.capp == "PR").FirstOrDefault();
                    string chuoi = xx.TangName + ".html";

                    var kq = db.Menus.Where(u => u.Link == chuoi).FirstOrDefault();
                    return kq.Level.Substring(0, 5);
                }
            }
            catch (Exception) { }

            try
            {
                string pagtag = Request["hp"] != null ? Request["hp"] : curURL.Substring(curURL.LastIndexOf("/") + 1);
                var pagtytin = db.Menus.Where(u => u.TangName == pagtag).FirstOrDefault();
                if (pagtytin.Module == 6)
                {
                    Album tbnew = db.Albums.SingleOrDefault(p => p.TangName == pagtag);
                    var data = db.Menus.Where(u => u.ID == tbnew.Menu_ID).FirstOrDefault();

                    var xx = db.Menus.Where(u => u.Level == data.Level.Substring(0, 5) && u.capp == "AB").FirstOrDefault();
                    string chuoi = xx.TangName + ".html";

                    var kq = db.Menus.Where(u => u.Link == chuoi).FirstOrDefault();
                    return kq.Level.Substring(0, 5);
                }
            }
            catch (Exception) { }

            try
            {
                string pagtag = Request["hp"] != null ? Request["hp"] : curURL.Substring(curURL.LastIndexOf("/") + 1);
                var pagtytin = db.Menus.Where(u => u.TangName == pagtag).FirstOrDefault();
                if (pagtytin.Module == 8)
                {
                    VideoClip tbnew = db.VideoClips.SingleOrDefault(p => p.TangName == pagtag);
                    var data = db.Menus.Where(u => u.ID == tbnew.Menu_ID).FirstOrDefault();

                    var xx = db.Menus.Where(u => u.Level == data.Level.Substring(0, 5) && u.capp == "VD").FirstOrDefault();
                    string chuoi = xx.TangName + ".html";

                    var kq = db.Menus.Where(u => u.Link == chuoi).FirstOrDefault();
                    return kq.Level.Substring(0, 5);
                }
            }
            catch (Exception) { }


            if (curPage != null)
            {
                return curPage.Level;
            }
            else
            {
                return culevel;
            }
        }
        protected string ShowMenuTop()
        {
            string strActive = "";
            string strRequest = Request.QueryString["e"];
            string strUrl = Request.Url.AbsolutePath.ToString().ToLower();
            int intHome = 0;
            switch (strRequest)
            {
                case "":
                    strActive = "/";
                    intHome += 1;
                    break;
            }

            string strMenu = "";
            List<Entity.Menu_OK> list = SMenu.Name_Text_Rg("SELECT capp,Create_Date,Description,Equals,ID,Images,Lang,Level,Link,Module,Name,Orders,Parent_ID,ShowID,Styleshow,TangName,Type,Url_Name,Views FROM Menu where capp='" + More.MN + "' and lang='" + language + "' and Views=3 and status=1 order by level,Orders asc").ToList();//Views là vị trí menu top
            if (list != null)
            {
                int ilevel = 0, iblevel = 0, k = 0, n = 1, ilength = 5, istartlevel = 0;
                string tmpLevel = "";
                for (int j = 0; j < list.Count; j++)
                {
                    string liClass = "";
                    tmpLevel = list[j].Level;
                    ilevel = (tmpLevel.Length / ilength) - istartlevel;
                    if (ilevel > iblevel)
                    {
                        strMenu += ilevel == 1 ? string.Format("") : "<ul>";
                    }
                    if (ilevel < iblevel)
                    {
                        for (k = 1; k <= (iblevel - ilevel); k++)
                        {
                            strMenu += "</ul>";
                            if (iblevel > 1) { strMenu += "</li>"; }
                        }
                        iblevel = ilevel;
                    }
                    string strName = list[j].Name;

                    if (Request["su"] == null && Request["e"] == null)
                    {
                        if (list[j].TangName == "trang-chu")
                        {
                            liClass = "active";
                        }
                    }
                    else
                    {
                        if (list[j].ShowID == 2)
                        {
                            if ((currLevel.Length > 0 && currLevel.Length >= list[j].Level.Length && list[j].Level == currLevel.Substring(0, list[j].Level.Length)) || (list[j].TangName.Length > 1 && (Request.Url.AbsolutePath.Contains(list[j].TangName) || Request.RawUrl.Contains(list[j].TangName))))
                            {
                                liClass = "active";
                            }
                        }
                        else
                        {
                            if (list[j].Link != "/")
                            {
                                if ((currLevel.Length > 0 && currLevel.Length >= list[j].Level.Length && list[j].Level == currLevel.Substring(0, list[j].Level.Length)) || (list[j].Link.Length > 1 && (Request.Url.AbsolutePath.Contains(list[j].Link) || Request.RawUrl.Contains(list[j].Link))))
                                {
                                    liClass = "active";
                                }
                            }

                        }
                    }
                    string lastClass = j == list.Count - 1 ? "last" : "";
                    string iconClass = SMenu.Menu_ExitstByLevel(tmpLevel).Count > 0 ? ilevel == 1 ? "itop" : "icon" : "";
                    if (iconClass.Length > 0)
                        liClass += " " + iconClass;
                    if (lastClass.Length > 0)
                        liClass += " " + lastClass;
                    if (liClass.Length > 0)
                        liClass = string.Format(" class=\"{0}\"", liClass.Trim());

                    string Link = "";
                    if (list[j].ShowID == 2)
                    {
                        Link = "/" + list[j].TangName + ".html";
                    }
                    else if (list[j].ShowID == 3)
                    {
                        Link = list[j].Link;
                    }
                    else
                    {
                        if (list[j].Link == "/")
                        {
                            Link = list[j].Link;
                        }
                        else
                        {
                            Link = "/" + list[j].Link;
                        }
                    }
                    string anh = "";
                    if (list[j].Images.Length > 0)
                    {
                        anh += "<div class='iconmn'><img src=\"" + list[j].Images + "\" /></div>";
                    }
                    if ((j < list.Count - 2) && list[j + 1].Level.StartsWith(list[j].Level) && list[j + 1].Level.Length == list[j].Level.Length + 5)
                    {
                        strMenu += "<li" + liClass + "><a href=\"" + Link + "\" >" + anh + list[j].Name + "</a>";
                    }
                    else
                    {
                        strMenu += "<li" + liClass + "><a href=\"" + Link + "\" target=\"" + list[j].Styleshow + "\">" + anh + list[j].Name + "</a>";
                    }
                    if (SMenu.Menu_ExitstByLevel(tmpLevel).Count == 0)
                    {
                        strMenu += "</li>";
                    }
                    iblevel = ilevel;
                    if (n == list.Count)
                    {
                        k = 0;
                        for (k = iblevel - 1; k == 1; k--)
                        {
                            strMenu += "</ul>";
                            if (iblevel > 1) { strMenu += "</ul></li>"; }
                        }
                    }
                    n++;
                }
            }
            list.Clear();
            list = null;
            return strMenu;

        }
    }
}
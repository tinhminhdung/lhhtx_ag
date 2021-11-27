using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;

namespace VS.E_Commerce.cms.Display.Allbums
{
    public partial class index : System.Web.UI.UserControl
    {
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
            if (!IsPostBack)
            {
                //List<Entity.MenuShow> dt = SMenu.Pages_Home(More.AB, language, "1");
                //rpcates.DataSource = dt;
                //rpcates.DataBind();
                ltshowalbum.Text = ShowListAlbum();
            }
        }

        //protected List<Entity.Album> AlbumInCate(string icid)
        //{
        //    return SAlbum.Name_Text("select top " + Commond.Setting("txtHomePagealbum") + " * from  Album where News=1 and Menu_ID in (" + More.Sub_Menu(More.AB, icid) + ") and Status=1   order by Create_Date desc");
        //}
        protected string ShowListAlbum()
        {
            string str = "";
            List<Entity.MenuShow> dt = SMenu.Pages_Home(More.AB, language, "1");
            if (dt.Count > 0)
            {
                foreach (Entity.MenuShow item in dt)
                {
                    str += "<div class=\"nhomnhe\">";
                    str += "<h2 class=\"title\"><a href=\"/" + item.TangName + ".html\">" + item.Name + "</a></h2>";
                    str += "</div>";
                    str += "<div style=\"clear: both\"></div>";
                    str += "<ul class=\"Album \">";
                    List<Entity.Album_RutGon> table = SAlbum.Name_Text_RG("select top " + Commond.Setting("txtHomePagealbum") + " ID,Title,Images,ImagesSmall,Create_Date,TangName,Anhnhieu from  Album where News=1 and Menu_ID in (" + More.Sub_Menu(More.AB, item.ID.ToString()) + ") and Status=1   order by Create_Date desc");
                    if (table.Count >= 1)
                    {
                        str += Commond.LoadALbum_Home(table);
                    }
                    str += "</ul>";
                    str += "<div style=\"clear: both\"></div>";
                }
            }
            return str;
        }
 
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}
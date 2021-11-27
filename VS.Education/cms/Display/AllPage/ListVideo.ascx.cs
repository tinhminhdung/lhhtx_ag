using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.AllPage
{
    public partial class ListVideo : System.Web.UI.UserControl
    {
        public int i = 1;
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
            if (!base.IsPostBack)
            {
                LoadListVideo();
            }
        }
        protected void LoadListVideo()
        {
            List<Entity.VideoClip> list = SVideoClip.Name_Text("select top 15 * from VideoClip where  lang='" + language + "'  and Status=1  order by Create_Date desc");
            if (list.Count > 0)
            {
                string kq = "";
                kq += "<ul id='list_player'>";
                for (int i = 0; i < list.Count(); i++)
                {
                    var urlVideo = list[i].Contents;
                    kq += "<li><a href='javascript:void(0)' class='videoItems' title='" + list[i].Title + "' name='https://www.youtube.com/embed/" + urlVideo.Replace("https://www.youtube.com/watch?v=", "").Replace("http://www.youtube.com/watch?v=", "").Replace("http://youtu.be/", "") + "'>" + list[i].Title + "</a></li>";
                }
                kq += "</ul>";
                ltrListVideo.Text = kq;
                var firstVideo = list[0].Contents;
                ltrIframeVideo.Text = "<iframe width='372' height='198' src='https://www.youtube.com/embed/" + firstVideo.Replace("https://www.youtube.com/watch?v=", "").Replace("http://www.youtube.com/watch?v=", "").Replace("http://youtu.be/", "") + "?autoplay=1' frameborder='0'  allow=\"autoplay; encrypted-media\" allowfullscreen></iframe>";
            }
        }
    }
}
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
    public partial class List_video_iframe : System.Web.UI.UserControl
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
            if (!base.IsPostBack)
            {
                LoadListVideo();
            }
        }
        protected void LoadListVideo()
        {
            List<Entity.VideoClip> list = SVideoClip.Name_Text("select top 10 * from VideoClip where  lang='" + language + "'  and Status=1  order by Create_Date desc");
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    var firstVideo = item.Contents;
                    ltrIframeVideo.Text += "<div  class=\"VideoName\">";
                    ltrIframeVideo.Text += "<div class=\"iframe\"><iframe width='270' height='200' src='https://www.youtube.com/embed/" + firstVideo.Replace("https://www.youtube.com/watch?v=", "").Replace("http://www.youtube.com/watch?v=", "") + "' frameborder='0' allowfullscreen></iframe></div>";
                    ltrIframeVideo.Text += "<div  class=\"Name\">" + item.Title + "</div>";
                    ltrIframeVideo.Text += "</div>";
                }
            }
        }
    }
}
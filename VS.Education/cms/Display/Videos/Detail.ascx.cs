using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;
using Entity;

namespace VS.E_Commerce.cms.Display.Videos
{
    public partial class Detail : System.Web.UI.UserControl
    {
        #region string
        string cid = "-1";
        string iVideo = "-1";
        private string language = Captionlanguage.Language;
        string hp = "";
        int iEmptyIndex = 0;
        DatalinqDataContext db = new DatalinqDataContext();
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
            LoadItems();
            if (!IsPostBack)
            {
                #region Detail_VideoClip
                VideoClip dt = db.VideoClips.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    ltcatename.Text = Commond.Name(dt.Menu_ID.ToString());
                    iVideo = dt.ID.ToString();
                    string url = dt.Contents.ToString();
                    string FormattedUrl = AllQuery.MoreVideoClip.GetYouTubeID(url);
                    lttitle.Text = dt.Title.ToString();
                    ltdesc.Text = dt.Brief;
                    string str = "";
                    #region LinkYoutube
                    if (dt.Contents.ToString().Length > 0)
                    {
                        try
                        {
                            ltcontent.Text = "<iframe width=\"" + AllQuery.MoreVideoClip.Width() + "\" height=\"" + AllQuery.MoreVideoClip.Height() + "\" src=\"https://www.youtube.com/embed/" + dt.Contents.ToString().Replace("http://youtu.be/", "") + "\" frameborder=\"0\" allowfullscreen></iframe>";
                        }
                        catch (Exception)
                        {
                        }
                    }

                    #endregion

                    //#region Other
                    //List<Entity.VideoClip> str1 = SVideoClip.NEWS_OTHER_FIRST(iVideo, int.Parse("7"), language);
                    //if (str1.Count > 0)
                    //{
                    //    rpcates.DataSource = str1;
                    //    rpcates.DataBind();
                    //}
                    //else
                    //{
                    //    rpcates.Visible = false;
                    //}
                    //List<Entity.VideoClip> str2 = SVideoClip.NEWS_OTHER_LAST(iVideo, int.Parse("7"), language);
                    //if (str2.Count > 0)
                    //{
                    //    rpcates.DataSource = str2;
                    //    rpcates.DataBind();
                    //}
                    //else
                    //{
                    //    rpcates.Visible = false;
                    //}
                    //#endregion

                    #region views
                    if (MoreAll.MoreAll.GetCookies("views").Equals("") || !MoreAll.MoreAll.GetCookies("views").Equals(this.iVideo))
                    {
                        SVideoClip.UPDATE_VIEWS_TIMES(this.iVideo);
                        MoreAll.MoreAll.SetCookie("views", this.iVideo);
                    }
                    #endregion
                  
                }
                #endregion
            }
        }
        void LoadItems()
        {
            List<Entity.VideoClip_RutGon> dt = SVideoClip.CATEGORY(More.Sub_Menu(More.VD, cid), language, "1");
            if (dt.Count > 0)
            {
                CollectionPager1.DataSource = dt;
                CollectionPager1.MaxPages = 10000;
                CollectionPager1.BindToControl = rpcates;
                CollectionPager1.PageSize = int.Parse(AllQuery.MoreVideoClip.Pagevideo());
                rpcates.DataSource = CollectionPager1.DataSourcePaged;
                rpcates.DataBind();
            }
            else lterr.Text = "<div class='Checkdata'>" + this.label("I_dulieuchuadccapnhat") + "</div>";
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}


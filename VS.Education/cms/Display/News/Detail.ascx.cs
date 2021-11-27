using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using MoreAll;

namespace VS.E_Commerce.cms.Display.News
{
    public partial class Detail1 : System.Web.UI.UserControl
    {
        #region string
        string cid = "";
        string nid = "";
        string hp = "";
        int iEmptyIndex = 0;
        private string language = Captionlanguage.Language;
        #endregion
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
            #endregion
            if (!IsPostBack)
            {
                #region Detail_ID
                New dt = db.News.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    nid = dt.inid.ToString();
                    cid = dt.icid.ToString();

                    ltcatename.Text = Commond.Name(dt.icid.ToString());
                    lttitle.Text = dt.Title;
                    ltdesc.Text = dt.Brief;
                    //this.ltTinlienquan.Text = AllQuery.MoreNews.AllRelated("nwsd", nid);
                    ltcontent.Text = dt.Contents;

                    //#region Tag
                    //string[] strArray = SNews.GET_DETAIL_BYID(nid).Tags.ToString().Split(new char[] { ';' });
                    //for (int i = 0; i < strArray.Length; i++)
                    //{
                    //    string text = this.lttag.Text;
                    //    this.lttag.Text = text + "<a rel='tag' href='/nwsd/" + strArray[i].ToString() + "/homepage.aspx'>" + strArray[i].ToString() + "</a>";
                    //    this.lttag.Text = this.lttag.Text + "&nbsp;&nbsp; , ";
                    //}
                    //#endregion


                    #region Facebook
                    if (Commond.Setting("NFacebook").Equals("1"))
                    {
                        ltFacebook.Text = "<div class='fb-like' data-href='" + MoreAll.MoreAll.RequestUrl(Request.Url.ToString()) + "' data-send='true' data-width='450' data-show-faces='false'></div>";
                    }
                    if (Commond.Setting("NFacebook").Equals("2"))
                    {
                        ltFacebook.Text = "<div class='fb-like' data-href='" + MoreAll.MoreAll.RequestUrl(Request.Url.ToString()) + "' data-send='true' data-width='450' data-show-faces='true' data-font='arial'></div>";
                    }
                    #endregion

                    #region Other
                    var dt2 = SNews.OTHERFIRST(nid, int.Parse(Commond.Setting("newsother")), language, cid);
                    if (dt2.Count > 0)
                    {
                        rpitems.DataSource = dt2;
                        rpitems.DataBind();
                    }
                    else
                    {
                        rpitems.Visible = false;
                    }
                    var dt1 = SNews.OTHERLAST(nid, int.Parse(Commond.Setting("newsother")), language, cid);
                    if (dt1.Count > 0)
                    {
                        rpitems2.DataSource = dt1;
                        rpitems2.DataBind();
                    }
                    else
                    {
                        rpitems2.Visible = false;
                    }
                    #endregion
                }

                #region views
                if (MoreAll.MoreAll.GetCookies("views").Equals("") || !MoreAll.MoreAll.GetCookies("views").Equals(this.nid))
                {
                    SNews.UPDATEVIEWS_TIMES(this.nid);
                    MoreAll.MoreAll.SetCookie("views", this.nid);
                }

                #endregion
                #endregion
            }
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
    }
}
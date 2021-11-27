using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;

namespace VS.E_Commerce.cms.Display.NewsFooter
{
    public partial class Detail : System.Web.UI.UserControl
    {
        #region string
        string oid = "-1";
        string fid = "-1";
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
                #region MyRegion
                Menu dt = db.Menus.SingleOrDefault(p => p.ID == int.Parse(More.TangNameicid(hp)));
                if (dt != null)
                {
                    ltcatename.Text = dt.Name.ToString();
                }
                #endregion

                #region Detail_ID
                Nfooter item = db.Nfooters.SingleOrDefault(p => p.TangName == hp);
                if (item != null)
                {
                    fid = item.inid.ToString();
                    lttitle.Text = item.Title.ToString();
                    ltcontent.Text = item.Contents.ToString();

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
                    var other = SNfooter.OTHERFIRST(oid, int.Parse(Commond.Setting("newsother")), language, fid);
                    if (other.Count > 0)
                    {
                        rpitems.DataSource = other;
                        rpitems.DataBind();
                    }
                    else
                    {
                        rpitems.Visible = false;
                    }
                    var others = SNfooter.OTHERLAST(oid, int.Parse(Commond.Setting("newsother")), language, fid);
                    if (others.Count > 0)
                    {
                        rpitems2.DataSource = others;
                        rpitems2.DataBind();
                    }
                    else
                    {
                        rpitems2.Visible = false;
                    }
                    #endregion
                }

                #region views
                if (MoreAll.MoreAll.GetCookies("views").Equals("") || !MoreAll.MoreAll.GetCookies("views").Equals(this.oid))
                {
                    SNfooter.UPDATEVIEWS_TIMES(this.oid);
                    MoreAll.MoreAll.SetCookie("views", this.oid);
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
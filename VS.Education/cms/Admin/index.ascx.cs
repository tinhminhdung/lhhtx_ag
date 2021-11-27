using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;
using System.Data;
using Framework;

namespace VS.E_Commerce.cms.Admin
{
    public partial class index : System.Web.UI.UserControl
    {
        private string lang = Captionlanguage.Language;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["lang"] != null)
            {
                this.lang = System.Web.HttpContext.Current.Session["lang"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["lang"] = this.lang;
                this.lang = System.Web.HttpContext.Current.Session["lang"].ToString();
            }
            if (!base.IsPostBack)
            {
                List<Entity.Setting> str = SSetting.GETBYALL(lang);
                if (str.Count >= 1)
                {
                    foreach (Entity.Setting its in str)
                    {
                        if (its.Properties == "searchkeyword")
                        {
                            this.ltkeyword.Text = its.Value;
                        }
                        if (its.Properties == "webname")
                        {
                            this.ltwebname.Text = its.Value;
                        }
                        if (its.Properties == "keyworddescription")
                        {
                            this.txtsitekeyworddescription.Text = its.Value;
                        }
                    }
                }
                this.lthomepage.Text = "Trang mặc định";

                #region MyRegion
                Fweb_statistic db = new Fweb_statistic();
                DataTable table = db.Load_WebStatistic();
                if (table.Rows.Count > 0)
                {
                    this.ltvisittime.Text = table.Rows[0]["sumvisitor"].ToString();
                }
                #endregion
            }
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
        }
    }
}
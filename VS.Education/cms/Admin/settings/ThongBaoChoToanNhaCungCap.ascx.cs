using Framework;
using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Admin.settings
{
    public partial class ThongBaoChoToanNhaCungCap : System.Web.UI.UserControl
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
            this.Page.Form.DefaultButton = btnsetup.UniqueID;
            if (!base.IsPostBack)
            {
                this.binddata();
            }
        }

        private void binddata()
        {
            try
            {
                string StatusOnOff = "";
                FSetting DB = new FSetting();
                List<Entity.Setting> str = DB.GETBYALL(lang);
                ltmsg.Text = string.Empty;
                if (str.Count >= 1)
                {
                    foreach (Entity.Setting its in str)
                    {
                        if (its.Properties == "noiDungNCC")
                        {
                            this.txtnoiDungNCC.Text = its.Value;
                        }

                        if (its.Properties == "NgayHienThi")
                        {
                            this.txtngayhienthi.Text = its.Value;
                        }


                        if (its.Properties == "StatusNCC")
                        {
                            StatusOnOff = its.Value;
                        }
                    }
                }
                if (StatusOnOff.Equals("0"))
                {
                    this.rdcommentoptioncheckcomments.Checked = false;
                    this.rdcommentoptionnotcheckcomments.Checked = true;
                }
                else if (StatusOnOff.Equals("1"))
                {
                    this.rdcommentoptioncheckcomments.Checked = true;
                    this.rdcommentoptionnotcheckcomments.Checked = false;
                }
            }
            catch (Exception) { }
        }

        protected void btnsetup_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int num = 0;
                if (this.rdcommentoptioncheckcomments.Checked)
                {
                    num = 1;
                }
                Entity.Setting obj = new Entity.Setting();
                obj.Lang = lang;
                obj.Properties = "noiDungNCC";
                obj.Value = txtnoiDungNCC.Text;
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "NgayCapNhapHienThiTBNCC";
                obj.Value = DateTime.Now.ToString();
                SSetting.UPDATE(obj);

                obj.Lang = lang;
                obj.Properties = "NgayHienThi";
                obj.Value = txtngayhienthi.Text;
                SSetting.UPDATE(obj);


                obj.Lang = lang;
                obj.Properties = "StatusNCC";
                obj.Value = num.ToString();
                SSetting.UPDATE(obj);
            }
            this.binddata();
            this.ltmsg.Text = "Thiết lập th\x00e0nh c\x00f4ng!";
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
        }
    }
}
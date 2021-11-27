using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using MoreAll;

namespace VS.E_Commerce.cms.Display.News.Comments
{
    public partial class Comment : System.Web.UI.UserControl
    {
        string nid = "-1";
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
            if (Request["nid"] != null && !Request["nid"].Equals(""))
            {
                nid = Request["nid"];
            }
            if (!base.IsPostBack)
            {
                if (Commond.Setting("Comment").Equals("1"))
                {
                    Panel1.Visible = true;
                }
                this.LoadCaptcha();
                this.loadcomments();
            }
        }

        protected void btnsend_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateUtilities.IsValidEmail(this.txtemail.Text))
                {
                    this.ltmsg.Text = "Email không hợp lệ. Hãy kiểm tra một lần nữa!";
                }
                else if (this.txtcommentcontent.Text.Trim().Length < 20)
                {
                    this.ltmsg.Text = "Thông báo bình luận không hợp lệ. Hãy kiểm tra một lần nữa!";
                }
                else if (!MoreAll.MoreAll.GetSession("captcha").Equals(this.txtcaptcha.Text.Trim()))
                {
                    this.ltmsg.Text = "Captcha không hợp lệ. Hãy kiểm tra một lần nữa!";
                }
                else
                {
                    this.ltmsg.Text = "";
                    Entity.Comments obj = new Entity.Comments();
                    #region MyRegion
                    obj.ID_Parent = int.Parse(this.nid.ToString());
                    obj.Name = this.txtfullname.Text;
                    obj.Add = "";
                    obj.Email = this.txtemail.Text;
                    obj.Title = this.txtcommenttitle.Text;
                    obj.Contens = txtcommentcontent.Text;
                    obj.Create_Date = DateTime.Now;
                    obj.Status = 1; ;
                    #endregion
                    if (Commond.Setting("Comment").Equals("1"))
                    {
                        SComments.INSERT(obj);
                    }
                    else if (Commond.Setting("Comment").Equals("0"))
                    {
                        #region MyRegion
                        obj.ID_Parent = int.Parse(this.nid.ToString());
                        obj.Name = this.txtfullname.Text;
                        obj.Add = "";
                        obj.Email = this.txtemail.Text;
                        obj.Title = this.txtcommenttitle.Text;
                        obj.Contens = txtcommentcontent.Text;
                        obj.Create_Date = DateTime.Now;
                        obj.Status = 0;
                        #endregion
                        SComments.INSERT(obj);
                    }
                    this.LoadCaptcha();
                    btnsend.Enabled = false;
                    btncancel.Enabled = false;
                    this.txtcommenttitle.Text = "Ti\x00eau đề";
                    this.txtemail.Text = "Email";
                    this.txtfullname.Text = "Họ t\x00ean";
                    this.txtcommentcontent.Text = "";
                    this.txtcaptcha.Text = "M\x00e3 x\x00e1c nhận";
                    this.ltmsg.Text = "C\x00e1m ơn bạn đ\x00e3 gửi phản hồi. Phản hồi của bạn sẽ được kiểm duyệt trước khi đăng.";
                }
            }
            catch (Exception) { }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            this.txtfullname.Text = "Họ t\x00ean";
            this.txtemail.Text = "Email";
            this.txtcommenttitle.Text = "Ti\x00eau đề";
            this.txtcommentcontent.Text = "";
            this.ltmsg.Text = "";
        }

        protected string FormatDate(object date)
        {
            return MoreAll.MoreAll.FormatDate(date);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            this.hdnumofitems.Value = (Convert.ToInt32(this.hdnumofitems.Value) + 10).ToString();
            this.loadcomments();
        }

        private void LoadCaptcha()
        {
            this.captchaImage.ImageUrl = "/cms/display/news/comments/Capchart.aspx";
        }

        private void loadcomments()
        {
            try
            {
                List<Entity.Comments> table = SComments.DETAIL_TOP(Convert.ToInt32(this.hdnumofitems.Value), this.nid.ToString(), "1", "Create_Date asc");
                this.rpitems.DataSource = table;
                this.rpitems.DataBind();
                if (table.Count < Convert.ToInt32(this.hdnumofitems.Value))
                {
                    this.lttotal1.Text = this.lttotal2.Text = table.Count.ToString();
                    this.ltviewitems.Text = table.Count.ToString();
                    this.lnkviewmorecomments.Visible = false;
                }
                else
                {
                    this.lttotal2.Text = this.lttotal1.Text = SComments.DETAIL_COUNT(this.nid.ToString(), "1", "").ToString();
                    this.ltviewitems.Text = this.hdnumofitems.Value;
                    this.lnkviewmorecomments.Visible = true;
                }
            }
            catch (Exception) { }
        }

        protected void refreshimg_Click(object sender, ImageClickEventArgs e)
        {
            this.LoadCaptcha();
        }
    }
}
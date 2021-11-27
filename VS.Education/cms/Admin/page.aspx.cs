using Framework;
using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Admin
{
    public partial class page : System.Web.UI.Page
    {
        string kichhoat = "0";
        private string language = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
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
            if (Request["kichhoat"] != null && !Request["kichhoat"].Equals(""))
            {
                kichhoat = Request["kichhoat"].ToString();
            }
            this.Page.Form.DefaultButton = btnsetup.UniqueID;
            if (!base.IsPostBack)
            {
                this.binddata();
                #region UpdatePanel
                this.Page.Form.Enctype = "multipart/form-data";
                #endregion
            }

        }
        public void binddata()
        {
            FSetting DB = new FSetting();
            List<Entity.Setting> str = DB.GETBYALL(language);
            ltmsg.Text = string.Empty;
            string Thongbao = "0";
            string Email = "0";

            string Loi = "0";
            string cauhinhsm = "0";
            string cauhinhsb = "0";
            string Cauhinhqcad = "0";


            if (str.Count >= 1)
            {
                foreach (Entity.Setting its in str)
                {
                    if (its.Properties == "website")
                    {
                        this.txtwebsite.Text = its.Value;
                    }
                    else if (its.Properties == "Show")
                    {
                        this.txtcontent.Text = its.Value;
                    }
                    else if (its.Properties == "Thongbao")
                    {
                        Thongbao = its.Value;
                    }
                    else if (its.Properties == "EmailTB")
                    {
                        Email = its.Value;
                    }
                    else if (its.Properties == "cauhinhs")
                    {
                        Loi = its.Value;
                    }
                    else if (its.Properties == "cauhinhsm")
                    {
                        cauhinhsm = its.Value;
                    }

                    else if (its.Properties == "cauhinhsb")
                    {
                        cauhinhsb = its.Value;
                    }

                    else if (its.Properties == "Cauhinhqcad")
                    {
                        Cauhinhqcad = its.Value;
                    }

                    else if (its.Properties == "Redirectwebsite")
                    {
                        this.txtRedirect.Text = its.Value;
                    }
                    else if (its.Properties == "txtcodeq")
                    {
                        this.txtcodeq.Text = its.Value;
                    }
                    

                }
            }

            if (Loi.Equals("0"))
            {
                this.RadioButton3.Checked = true;
                this.RadioButton4.Checked = false;
            }
            else if (Loi.Equals("1"))
            {
                this.RadioButton3.Checked = false;
                this.RadioButton4.Checked = true;
            }

            if (cauhinhsm.Equals("0"))
            {
                this.RadioButton5.Checked = true;
                this.RadioButton6.Checked = false;
            }
            else if (cauhinhsm.Equals("1"))
            {
                this.RadioButton5.Checked = false;
                this.RadioButton6.Checked = true;
            }


            if (cauhinhsb.Equals("0"))
            {
                this.RadioButton7.Checked = true;
                this.RadioButton8.Checked = false;
            }
            else if (cauhinhsb.Equals("1"))
            {
                this.RadioButton7.Checked = false;
                this.RadioButton8.Checked = true;
            }

            if (Cauhinhqcad.Equals("0"))
            {
                this.RadioButton9.Checked = true;
                this.RadioButton10.Checked = false;
            }
            else if (Cauhinhqcad.Equals("1"))
            {
                this.RadioButton9.Checked = false;
                this.RadioButton9.Checked = true;
            }

            if (Email.Equals("0"))
            {
                this.RadioButton1.Checked = true;
                this.RadioButton2.Checked = false;
            }
            else if (Email.Equals("1"))
            {
                this.RadioButton1.Checked = false;
                this.RadioButton2.Checked = true;
            }
            if (Thongbao.Equals("0"))
            {
                this.Thongbao1.Checked = true;
                this.Thongbao2.Checked = false;
                this.Thongbao3.Checked = false;
            }
            else if (Thongbao.Equals("1"))
            {
                this.Thongbao1.Checked = false;
                this.Thongbao2.Checked = true;
                this.Thongbao3.Checked = false;
            }
            else if (Thongbao.Equals("2"))
            {
                this.Thongbao1.Checked = false;
                this.Thongbao2.Checked = false;
                this.Thongbao3.Checked = true;
            }
            this.btnsetup.Text = this.label("l_update");
        }
        protected void btnsetup_Click(object sender, EventArgs e)
        {
            int cauhinhsm = 0;
            if (this.RadioButton6.Checked)
            {
                cauhinhsm = 1;
            }

            int cauhinhsb = 0;
            if (this.RadioButton8.Checked)
            {
                cauhinhsb = 1;
            }

            int Cauhinhqcad = 0;
            if (this.RadioButton10.Checked)
            {
                Cauhinhqcad = 1;
            }


            int loi = 0;
            if (this.RadioButton4.Checked)
            {
                loi = 1;
            }


            int Email = 0;
            if (this.RadioButton2.Checked)
            {
                Email = 1;
            }
            int Thongbao = 0;
            if (this.Thongbao2.Checked)
            {
                Thongbao = 1;
            }
            else if (this.Thongbao3.Checked)
            {
                Thongbao = 2;
            }

            if (Page.IsValid)
            {
                Entity.Setting obj = new Entity.Setting();
                obj.Lang = language;
                obj.Properties = "website";
                obj.Value = txtwebsite.Text;
                SSetting.UPDATE(obj);

                obj.Lang = language;
                obj.Properties = "Show";
                obj.Value = txtcontent.Text;
                SSetting.UPDATE(obj);

                obj.Lang = language;
                obj.Properties = "Thongbao";
                obj.Value = Thongbao.ToString();
                SSetting.UPDATE(obj);

                obj.Lang = language;
                obj.Properties = "EmailTB";
                obj.Value = Email.ToString();
                SSetting.UPDATE(obj);

                obj.Lang = language;
                obj.Properties = "cauhinhs";
                obj.Value = loi.ToString();
                SSetting.UPDATE(obj);
                

                obj.Lang = language;
                obj.Properties = "Cauhinhqcad";
                obj.Value = Cauhinhqcad.ToString();
                SSetting.UPDATE(obj);

                obj.Lang = language;
                obj.Properties = "cauhinhsm";
                obj.Value = cauhinhsm.ToString();
                SSetting.UPDATE(obj);

                obj.Lang = language;
                obj.Properties = "cauhinhsb";
                obj.Value = cauhinhsb.ToString();
                SSetting.UPDATE(obj);


                obj.Lang = language;
                obj.Properties = "Redirectwebsite";
                obj.Value = txtRedirect.Text;
                SSetting.UPDATE(obj);

                obj.Lang = language;
                obj.Properties = "txtcodeq";
                obj.Value = txtcodeq.Text;
                SSetting.UPDATE(obj);




                this.binddata();
                this.ltmsg.Text = "Thiết lập th\x00e0nh c\x00f4ng!";
            }
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }

        protected void btcauhinhnhanh_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int Email = 0;
                if (this.RadioButton2.Checked)
                {
                    Email = 1;
                }

                string a = Request.Url.Authority;
                string b = "www." + Request.Url.Authority;

                Entity.Setting obj = new Entity.Setting();

                obj.Lang = language;
                obj.Properties = "website";
                obj.Value = a + "','" + b;
                SSetting.UPDATE(obj);


                obj.Lang = language;
                obj.Properties = "cauhinhs";
                obj.Value = "0";
                SSetting.UPDATE(obj);


                obj.Lang = language;
                obj.Properties = "EmailTB";
                obj.Value = Email.ToString();
                SSetting.UPDATE(obj);

                 

                


                this.binddata();
                this.ltmsg.Text = "Thiết lập th\x00e0nh c\x00f4ng!";
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (txtSQLQuery.Text.Length > 10)
            {
                try
                {
                    using (SqlConnection dbConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                    {
                        using (SqlCommand dbCmd = new SqlCommand(txtSQLQuery.Text, dbConn))
                        {
                            dbCmd.CommandType = CommandType.Text;
                            dbConn.Open();
                            dbCmd.ExecuteNonQuery();
                            dbConn.Close();
                        }
                    }
                    lblAlert.Text = "Thành công.!!!!!!!!!!!!!!";
                }
                catch
                {
                    lblAlert.Text = "Lỗi.!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!";
                }
            }
        }
    }
}
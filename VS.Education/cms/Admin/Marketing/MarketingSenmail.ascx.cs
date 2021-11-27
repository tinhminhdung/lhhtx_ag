using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using MoreAll;
using System.Collections;
using Framework;

namespace VS.E_Commerce.cms.Admin.Marketing
{
    public partial class MarketingSenmail : System.Web.UI.UserControl
    {
        private string lang = Captionlanguage.Language;
        private string status = "-1";
        DatalinqDataContext db = new DatalinqDataContext();
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
            if (Request["st"] != null && !Request["st"].Equals(""))
            {
                ddlstatus.SelectedValue = Request["st"];
            }

            if (Request["Lead"] != null && !Request["Lead"].Equals(""))
            {
                ddltheoLead.SelectedValue = Request["Lead"];
            }
            if (Request["sao"] != null && !Request["sao"].Equals(""))
            {
                ddlcapdo.SelectedValue = Request["sao"];
            }

            if (Request["chinhanh"] != null && !Request["chinhanh"].Equals(""))
            {
                ddlchinhanh.SelectedValue = Request["chinhanh"];
            }
            if (Request["us"] != null && !Request["us"].Equals(""))
            {
                ddlorderby.SelectedValue = Request["us"];
            }
            if (Request["ds"] != null && !Request["ds"].Equals(""))
            {
                ddlordertype.SelectedValue = Request["ds"];
            }
            if (Request["kieuthanhvien"] != null && !Request["kieuthanhvien"].Equals(""))
            {
                ddlkieuthanhvien.SelectedValue = Request["kieuthanhvien"];
            }
            if (Request["AgLand"] != null && !Request["AgLand"].Equals(""))
            {
                ddlAgLand.SelectedValue = Request["AgLand"];
            }
            if (Request["uutien"] != null && !Request["uutien"].Equals(""))
            {
                ddluutien.SelectedValue = Request["uutien"];
            }
            if (Request["QRCode"] != null && !Request["QRCode"].Equals(""))
            {
                ddlQRCode.SelectedValue = Request["QRCode"];
            }
            if (Request["kw"] != null && !Request["kw"].Equals(""))
            {
                txtkeyword.Text = Request["kw"];
            }

            if (Request["Tu"] != null && !Request["Tu"].Equals(""))
            {
                txtNgayThangNam.Text = Request["Tu"];
            }
            if (Request["Den"] != null && !Request["Den"].Equals(""))
            {
                txtDenNgayThangNam.Text = Request["Den"];
            }
            this.ddlstatus.Items.Add(new ListItem("== Kích hoạt thành viên ==", "-1"));
            this.ddlstatus.Items.Add(new ListItem("Kích hoạt", "1"));
            this.ddlstatus.Items.Add(new ListItem("Chưa kích hoạt", "0"));
            WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlstatus, this.status);
            ShowChiNhanh();
            this.binddata();
            this.LoadItems();
        }
        protected void ShowChiNhanh()
        {
            int str = 0;
            List<Entity.Menu> dt = SMenu.LOAD_CATESPARENT_ID(More.DL, this.lang, "-1", "1");
            for (int i = 0; i < dt.Count; i++)
            {
                if (dt[i].Parent_ID.ToString() == "-1")
                {
                    ddlchinhanh.Items.Insert(str, new ListItem(dt[i].Name.ToString(), dt[i].ID.ToString()));
                }
            }
            this.ddlchinhanh.Items.Insert(0, new ListItem("== Lọc theo chi nhánh == ", "0"));
            this.ddlchinhanh.DataBind();
        }
        public static string LocDate_NgayThangNam(string date)
        {
            return " and ( day(dcreatedate)=" + Convert.ToDateTime(date).ToString("dd") + " and MONTH(dcreatedate)=" + Convert.ToDateTime(date).ToString("MM") + "  and  year(dcreatedate)=" + Convert.ToDateTime(date).ToString("yyyy") + " )";
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            this.txtto.Text = "";
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)Repeater1.Items[i].FindControl("chkid");
                HiddenField id = (HiddenField)Repeater1.Items[i].FindControl("hiID");
                if (chk.Checked)
                {
                    this.txtto.Text = this.txtto.Text + id.Value + ", ";
                }
            }
            this.txtto.Text = this.txtto.Text + "000";
            this.txtto.Text = this.txtto.Text.Replace(", 000", "");
            //string[] strArray = base.Request.Form["chk"].Split(new char[] { ',' });
            //if (strArray.Length > 0)
            //{
            //    for (int i = 0; i < strArray.Length; i++)
            //    {
            //        if (i < (strArray.Length - 1))
            //        {
            //            this.txtto.Text = this.txtto.Text + strArray[i] + ", ";
            //        }
            //        else
            //        {
            //            this.txtto.Text = this.txtto.Text + strArray[i];
            //        }
            //    }
            //}
            //else
            //{
            //    this.txtto.Text = "";
            //}
        }
        protected void btnsend_Click(object sender, EventArgs e)
        {

            if (this.txtto.Text.Trim().Length < 1)
            {
                this.lblmsg.Text = "Yêu cầu chọn sanh sách cần gửi thông báo!";
                this.lblmsg.Visible = true;
                this.txtto.Focus();
            }
            else if (this.txtcontent.Text.Trim().Length < 1)
            {
                this.lblmsg.Text = "Nhập nội dung thông báo !";
                this.lblmsg.Visible = true;
                this.txtcontent.Focus();
            }
            else
            {
                string bcc = "";
                ArrayList list = new ArrayList();
                string sendername = Email.email();
                string password = Email.password();
                int str2 = Convert.ToInt32(Email.port());
                string host = Email.host();
                string[] strArray = this.txtto.Text.Split(new char[] { ',' });
                if (strArray.Length > 0)
                {
                    for (int j = 0; j < strArray.Length; j++)
                    {
                        //list.Add(strArray[j].Trim());
                        Fusers item = new Fusers();
                        List<Entity.users> table = Susers.Name_Text("select * from users where vuserun='" + strArray[j].Trim() + "' ");
                        if (table.Count > 0)
                        {
                            Notification obj = new Notification();
                            obj.IDThanhVienNhanThongBao = int.Parse(table[0].iuser_id.ToString());
                            obj.NgayTao = DateTime.Now;
                            obj.NoiDung = txtcontent.Text;
                            obj.NguoiTao = MoreAll.MoreAll.GetCookies("UName").ToString();
                            obj.TrangThai = Convert.ToInt16("0");
                            db.Notifications.InsertOnSubmit(obj);
                            db.SubmitChanges();
                        }
                    }
                }

                this.txtto.Text = "";
                // this.txtcontent.Text = "";
                // this.txtsubject.Text = "";
                this.lblmsg.Text = "Gửi thông báo th\x00e0nh c\x00f4ng !";
                this.lblmsg.Visible = true;
            }

        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
        }

        protected void lkbsearch_Click(object sender, EventArgs e)
        {
            LoadItems();
        }

        public void LoadItems()
        {
            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse("200");
            if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
            {
                pages = Convert.ToInt16(Request.QueryString["page"].Trim());
            }
            string sql1 = "";

            if (ddlkieuthanhvien.SelectedValue != "-1")
            {
                if (ddlkieuthanhvien.SelectedValue == "1" || ddlkieuthanhvien.SelectedValue == "2")
                {
                    sql1 += " and Type=" + ddlkieuthanhvien.SelectedValue + " ";
                }
                else if (ddlkieuthanhvien.SelectedValue == "3")
                {
                    sql1 += " and Type=2 and TongSoSanPham!=0";
                }
            }
            if (ddlAgLand.SelectedValue != "-1")
            {
                sql1 += " and ThanhVienAgLang=" + ddlAgLand.SelectedValue + " ";
            }
            if (ddluutien.SelectedValue != "-1")
            {
                sql1 += " and Uutien=" + ddluutien.SelectedValue + " ";
            }
            if (ddlQRCode.SelectedValue != "-1")
            {
                sql1 += " and TrangThaiThamGiaQRCode=" + ddlQRCode.SelectedValue + " ";
            }

            if (!string.IsNullOrEmpty(txtNgayThangNam.Text) && string.IsNullOrEmpty(txtDenNgayThangNam.Text))
            {
                sql1 += LocDate_NgayThangNam(txtNgayThangNam.Text);
            }
            else if (!string.IsNullOrEmpty(txtNgayThangNam.Text) && !string.IsNullOrEmpty(txtDenNgayThangNam.Text))
            {
                sql1 += " and ( dcreatedate>='" + MoreAll.MoreAll.LocDate(txtNgayThangNam.Text) + "' and  dcreatedate<='" + MoreAll.MoreAll.LocDate(txtDenNgayThangNam.Text) + "' )";
            }

            List<Entity.TongSo> iitem = Susers.CATEGORY_PHANTRANG1("0", txtkeyword.Text.Replace("&nbsp;", ""), ddlchinhanh.SelectedValue, ddlstatus.SelectedValue, ddltheoLead.SelectedValue, ddlcapdo.SelectedValue, sql1);
            if (iitem.Count() > 0)
            {
                Tongsobanghi = iitem[0].Tong;

            }
            List<Entity.users> dt = Susers.CATEGORY_PHANTRANG2("0", txtkeyword.Text.Replace("&nbsp;", ""), ddlchinhanh.SelectedValue, ddlstatus.SelectedValue, ddltheoLead.SelectedValue, ddlcapdo.SelectedValue, sql1, "ORDER BY dcreatedate desc", (pages - 1), Tongsotrang);
            if (dt.Count >= 1)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
                lttongtb.Text = dt.Count.ToString();
            }

            if (Tongsobanghi % Tongsotrang > 0)
            {
                Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
            }
            else
            {
                Tongsobanghi = Tongsobanghi / Tongsotrang;
            }
            ltpage.Text = Commond.PhantrangAdmin("/admin.aspx?u=Marketing&su=MarketingSenmail&st=" + ddlstatus.SelectedValue + "&us=" + ddlorderby.SelectedValue + "&ds=" + ddlordertype.SelectedValue + "&kw=" + txtkeyword.Text + "&chinhanh=" + ddlchinhanh.SelectedValue + "&Lead=" + ddltheoLead.SelectedValue + "&sao=" + ddlcapdo.SelectedValue + "&kieuthanhvien=" + ddlkieuthanhvien.SelectedValue + "&AgLand=" + ddlAgLand.SelectedValue + "&uutien=" + ddluutien.SelectedValue + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "", Tongsobanghi, pages);
        }


        protected void ddlordertype_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
            LoadRequest();
        }
        protected void ddlorderby_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
            LoadRequest();
        }

        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }
        protected void ddlchinhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }
        protected void ddlcapdo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }

        protected void ddltheoLead_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }

        protected void ddluutien_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }
        protected void ddlAgLand_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }

        protected void ddlkieuthanhvien_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }
        protected void LoadRequest()
        {
            Response.Redirect("admin.aspx?u=Marketing&su=MarketingSenmail&st=" + ddlstatus.SelectedValue + "&us=" + ddlorderby.SelectedValue + "&ds=" + ddlordertype.SelectedValue + "&kw=" + txtkeyword.Text + "&chinhanh=" + ddlchinhanh.SelectedValue + "&Lead=" + ddltheoLead.SelectedValue + "&sao=" + ddlcapdo.SelectedValue + "&kieuthanhvien=" + ddlkieuthanhvien.SelectedValue + "&AgLand=" + ddlAgLand.SelectedValue + "&uutien=" + ddluutien.SelectedValue + "&QRCode=" + ddlQRCode.SelectedValue + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "");

        }

        protected void ddlQRCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }
        protected void txtNgayThangNam_TextChanged(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }

        protected void txtDenNgayThangNam_TextChanged(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }

        protected void btnhuy2_Click(object sender, EventArgs e)
        {
            this.txtto.Text = "";
            // this.txtcontent.Text = "";
            // this.txtsubject.Text = "";
            this.lblmsg.Text = "";
        }
        private void binddata()
        {
            List<Entity.Setting> str = SSetting.GETBYALL(lang);
            lblmsg.Text = "";
            if (str.Count >= 1)
            {
                foreach (Entity.Setting its in str)
                {
                    if (its.Properties == "Notificationcontent")
                    {
                        this.txtcontent.Text = its.Value;
                    }

                }
            }

        }
        protected void btcapnhatthongbao_Click(object sender, EventArgs e)
        {
            Entity.Setting obj = new Entity.Setting();
            obj.Lang = lang;
            obj.Properties = "Notificationcontent";
            obj.Value = txtcontent.Text;
            SSetting.UPDATE(obj);

            this.binddata();
            this.lblmsg.Text = "Thiết lập th\x00e0nh c\x00f4ng!";


        }
    }
}
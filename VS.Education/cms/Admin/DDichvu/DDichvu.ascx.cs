﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using System.IO;
using Services;

namespace VS.E_Commerce.cms.Admin.DDichvu
{
    public partial class DDichvu : System.Web.UI.UserControl
    {
        ServerInfoUtlitities utlitities = new ServerInfoUtlitities();
        private string status = "";
        private string lang = Captionlanguage.Language;
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
            if (Request["us"] != null && !Request["us"].Equals(""))
            {
                ddlorderby.SelectedValue = Request["us"];
            }
            if (Request["ds"] != null && !Request["ds"].Equals(""))
            {
                ddlordertype.SelectedValue = Request["ds"];
            }
            if (Request["kw"] != null && !Request["kw"].Equals(""))
            {
                txtkeyword.Text = Request["kw"];
            }
            if (!IsPostBack)
            {
                #region UpdatePanel
                this.Page.Form.Enctype = "multipart/form-data";
                ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnsave);
                #endregion
                if (Request["st"] != null && !Request["st"].Equals(""))
                {
                    ddlstatus.SelectedValue = Request["st"];
                    WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlstatus, this.status);
                }
                LoadItems();
                this.LoadView();
            }
        }

        void LoadItems()
        {
            try
            {
                string[] searchfields = new string[] { "Title", "Brief", "Contents", "search" };
                string orderby = this.ddlorderby.SelectedValue + " " + this.ddlordertype.SelectedValue;
                List<Entity.Dichvu> dt = SDichvu.CATEGORY_ADMIN(orderby, this.txtkeyword.Text.Replace("&nbsp;", ""), searchfields, lang, ddlstatus.SelectedValue);
                CollectionPager1.DataSource = dt;
                CollectionPager1.BindToControl = rpitems;
                CollectionPager1.MaxPages = 10000;
                CollectionPager1.PageSize = 10;
                rpitems.DataSource = CollectionPager1.DataSourcePaged;
                rpitems.DataBind();
            }
            catch (Exception) { }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            // try
            //{
            if (this.txtname.Text.Trim().Length < 1)
            {
                this.lbl_msg.Text = "Xin vui lòng kiểm tra dữ liệu đầu vào của bạn";
            }
            else
            {
                int status = 0;
                if (this.chkstatus.Checked)
                {
                    status = 1;
                }

                #region Date
                int Chekdata = 0;
                string cdate = DateTime.Now.ToString();
                string edate = DateTime.Now.AddYears(10).ToString();
                DateTime dcreatedate = Convert.ToDateTime(cdate.ToString());
                DateTime denddate = Convert.ToDateTime(edate.ToString());
                if (this.chkdaytype.Checked)
                {
                    Chekdata = 1;
                    dcreatedate = Convert.ToDateTime(this.txtfromday.Text);
                    denddate = dcreatedate.AddDays((double)Convert.ToInt32(this.txtindays.Text));
                }
                #endregion

                #region Img
                ServerInfoUtlitities utlitities = new ServerInfoUtlitities();
                int vkey = 1;
                string vimg = this.txtvimg.Text;
                string path = "";
                if (this.rdFromComputer.Checked)
                {
                    if ((this.flimage.FileName.Trim().Length > 0) && (this.flimage.PostedFile.ContentLength > 0))
                    {
                        path = Path.GetFileName(this.flimage.PostedFile.FileName);
                        string str6 = "";
                        str6 = Path.GetExtension(path).ToLower();
                        vimg = "/Uploads/All/" + DateTime.Now.Ticks.ToString() + str6;
                        flimage.PostedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + vimg);
                    }
                    else
                    {
                        if ((this.txtvimg.Text.Trim().Length > 0))
                        {
                            vimg = this.hdFileName.Value;
                        }
                    }
                    vkey = 0;
                }
                #endregion

                Entity.Dichvu obj = new Entity.Dichvu();
                string str2 = this.hdinsertupdate.Value.Trim();
                if (str2 != null)
                {
                    if (!(str2 == "update"))
                    {
                        if (str2 == "insert")
                        {
                            #region InsertMenu
                            int cong = 0;
                            string TangName = "";
                            ModulControl obm = new ModulControl();
                            obm.Name = txtname.Text;
                            obm.Module = 13;
                            List<Entity.ModulControls> curItem = SModulControls.Name_Text("SELECT top 1 * FROM ModulControls order by ID desc");
                            int tong = int.Parse(curItem[0].ID.ToString()); cong = tong + 1; var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txtname.Text)).FirstOrDefault(); TangName = hasTagName != null ? MoreAll.AddURL.SeoURL(txtname.Text) + "-" + cong : MoreAll.AddURL.SeoURL(txtname.Text);
                            obm.TangName = TangName;
                            db.ModulControls.InsertOnSubmit(obm);
                            db.SubmitChanges();
                            #endregion

                            #region MyRegion
                            obj.Title = txtname.Text;
                            obj.Brief = txtdesc.Text;
                            obj.Contents = txtcontent.Text;
                            obj.Keywords = txtauthor.Text;
                            obj.search = RewriteURLNew.NameToTag(txtname.Text + txtdesc.Text + txtcontent.Text + txtauthor.Text);
                            obj.Images = vimg;
                            obj.Equals = vkey;
                            obj.Chekdata = Chekdata;
                            obj.Create_Date = dcreatedate;
                            obj.Modified_Date = denddate;
                            obj.Views = 0;
                            obj.lang = this.lang;
                            obj.Status = status;
                            obj.Titleseo = this.txttitleseo.Text;
                            obj.Meta = this.txtmeta.Text;
                            obj.Keyword = this.txtKeywordS.Text;
                            obj.TangName = TangName;
                            #endregion
                            SDichvu.INSERT(obj);
                        }
                    }
                    else
                    {
                        #region DELETE
                        if (vimg.Equals(""))
                        {
                            vimg = this.hdimg.Value;
                        }
                        else
                        {
                            try
                            {
                                if ((this.txtvimg.Text.Trim().Length > 0))
                                {
                                    File.Delete(utlitities.APPL_PHYSICAL_PATH + this.hdimg.Value);
                                }
                                if ((this.flimage.FileName.Trim().Length > 0) && (this.flimage.PostedFile.ContentLength > 0))
                                {
                                    File.Delete(utlitities.APPL_PHYSICAL_PATH + this.hdimg.Value);
                                }
                            }
                            catch (Exception) { }
                        }
                        #endregion
                        #region UpdateMenu
                        string TagName = "";
                        List<Entity.Dichvu> item = SDichvu.GET_BY_ID(this.hdid.Value);
                        if (item.Count > 0)
                        {
                            ModulControl obk = db.ModulControls.SingleOrDefault(p => p.TangName == item[0].TangName);
                            obk.Name = txtname.Text;
                            obk.Module = 13;
                            List<ModulControl> list = (from p in db.ModulControls where p.TangName == obk.TangName orderby p.ID descending select p).ToList();
                            if (list.Count > 2)
                            {
                                var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txtname.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txtname.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txtname.Text);
                            }
                            else
                            {
                                if (MoreAll.AddURL.SeoURL(item[0].Title) != MoreAll.AddURL.SeoURL(txtname.Text)) { var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txtname.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txtname.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txtname.Text); } else { TagName = item[0].TangName; }
                            }
                            obk.TangName = TagName;
                            db.SubmitChanges();
                        }
                        #endregion

                        #region MyRegion
                        obj.ID = int.Parse(hdid.Value);
                        obj.Title = txtname.Text;
                        obj.Brief = txtdesc.Text;
                        obj.Contents = txtcontent.Text;
                        obj.Keywords = txtauthor.Text;
                        obj.search = RewriteURLNew.NameToTag(txtname.Text + txtdesc.Text + txtcontent.Text + txtauthor.Text);
                        obj.Images = vimg;
                        obj.Equals = vkey;
                        obj.Chekdata = Chekdata;
                        //obj.Create_Date = dcreatedate;
                        //obj.Modified_Date = denddate;
                        obj.lang = this.lang;
                        obj.Status = status;
                        obj.Titleseo = this.txttitleseo.Text;
                        obj.Meta = this.txtmeta.Text;
                        obj.Keyword = this.txtKeywordS.Text;
                        obj.TangName = TagName;
                        #endregion
                        SDichvu.UPDATE(obj);
                    }
                }
                LoadItems();
                MultiView1.ActiveViewIndex = 0;
                DeleteFormValue();
            }
            //}
            // catch (Exception) { }
        }

        void DeleteFormValue()
        {
            txtcontent.Text = "";
            txtdesc.Text = "";
            hdimg.Value = "";
            hdcid.Value = "";
            hdimg.Value = "";
            txtname.Text = "";
            txtcontent.Text = "";
            txtauthor.Text = "";
            ltimg.Text = "";
            txtvimg.Text = "";
            hdFileName.Value = "";
            lbl_msg.Text = "";
            lbl_msg.Text = "";
            this.txttitleseo.Text = "";
            this.txtmeta.Text = "";
            this.txtKeywordS.Text = "";
        }

        protected void Delete_Load(object sender, System.EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Xóa Thông Tin Vừa Chọn ?')";
        }

        protected void btthemmoi_Click(object sender, EventArgs e)
        {
            hdinsertupdate.Value = "insert";
            MultiView1.ActiveViewIndex = 1;
            DeleteFormValue();
            this.txtfromday.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
        }

        protected void btndisplay_Click(object sender, EventArgs e)
        {
            LoadItems(); LoadRequest();
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            DeleteFormValue();
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
        }

        protected void rpitems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            List<Entity.Dichvu> dt = new List<Entity.Dichvu>();
            switch (e.CommandName)
            {
                #region EditDetail
                case "EditDetail":
                    dt = SDichvu.GET_DETAIL_BYID(e.CommandArgument.ToString());
                    if (dt.Count > 0)
                    {
                        txtname.Text = dt[0].Title.ToString();
                        txtdesc.Text = dt[0].Brief.ToString();
                        txtcontent.Text = dt[0].Contents.ToString();
                        txtauthor.Text = dt[0].Keywords.ToString();
                        hdimg.Value = dt[0].Images.ToString();
                        ltimg.Text = "";
                        if (dt[0].Status.ToString().Trim().Equals("0"))
                        {
                            this.chkstatus.Checked = false;
                        }
                        else if (dt[0].Status.ToString().Equals("1"))
                        {
                            this.chkstatus.Checked = true;
                        }
                        #region Seowwebsite
                        txttitleseo.Text = dt[0].Titleseo.ToString().Trim();
                        txtmeta.Text = dt[0].Meta.ToString().Trim();
                        txtKeywordS.Text = dt[0].Keyword.ToString().Trim();
                        #endregion
                        #region Update
                        this.txtfromday.Text = Convert.ToDateTime(dt[0].Create_Date).ToString("MM/dd/yyyy HH:mm");
                        this.txtindays.Text = ((Convert.ToDateTime(dt[0].Modified_Date).Ticks - Convert.ToDateTime(dt[0].Create_Date).Ticks) / 0xc92a69c000L).ToString();
                        if (dt[0].Chekdata.ToString().Equals("1"))
                        {
                            this.chkdaytype.Checked = true;
                            this.pnadddate.Visible = true;
                        }
                        else
                        {
                            this.chkdaytype.Checked = false;
                            this.pnadddate.Visible = false;
                        }
                        #endregion
                        if (dt[0].Equals.ToString().Trim().Equals("1"))
                        {
                            this.rdFromLinks.Checked = true;
                            this.rdFromComputer.Checked = false;
                            this.LoadView();
                            this.txtvimg.Text = dt[0].Images.ToString();
                        }
                        else
                        {
                            this.rdFromComputer.Checked = true;
                            this.rdFromLinks.Checked = false;
                            this.LoadView();
                            this.hdFileName.Value = dt[0].Images.ToString();
                        }
                        MultiView1.ActiveViewIndex = 1;
                        hdinsertupdate.Value = "update";
                        hdid.Value = dt[0].ID.ToString();
                    }
                    break;
                #endregion

                #region ChangeStatus
                case "ChangeStatus":
                    string str = e.CommandName.Trim();
                    string str2 = e.CommandArgument.ToString().Trim();
                    string str4 = str;
                    if (str4 != null)
                    {
                        string str3;
                        str2 = e.CommandArgument.ToString().Trim().Substring(0, e.CommandArgument.ToString().IndexOf("|"));
                        if (e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf("|") + 1, (e.CommandArgument.ToString().Length - e.CommandArgument.ToString().IndexOf("|")) - 1) == "1")
                        {
                            str3 = "0";
                        }
                        else
                        {
                            str3 = "1";
                        }
                        SDichvu.UPDATE_STATUS(str2, str3);
                        this.LoadItems();
                        return;
                    }
                    return;
                #endregion

                #region updat_date
                case "updat_date":
                    SDichvu.UPDATE_DATETIME(e.CommandArgument.ToString(), Convert.ToDateTime(DateTime.Now.ToString()), Convert.ToDateTime(DateTime.Now.AddYears(20).ToString()));
                    this.LoadItems();
                    MultiView1.ActiveViewIndex = 0;
                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    return;
                case "Chekdata":
                    SDichvu.CHECKDATA(e.CommandArgument.ToString(), "0", Convert.ToDateTime(DateTime.Now.ToString()), Convert.ToDateTime(DateTime.Now.AddYears(20).ToString()));
                    this.LoadItems();
                    MultiView1.ActiveViewIndex = 0;
                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    return;
                #endregion

                #region Delete
                case "Delete":
                    dt = SDichvu.GET_DETAIL_BYID(e.CommandArgument.ToString());
                    if (dt.Count > 0)
                    {
                        try
                        {
                            File.Delete(utlitities.APPL_PHYSICAL_PATH + dt[0].Images.ToString());

                        }
                        catch (Exception) { }
                        try
                        {
                            SModulControls.Name_Text("DELETE FROM ModulControls WHERE TangName='" + dt[0].TangName + "'");
                        }
                        catch (Exception)
                        { }
                    }
                    SDichvu.DELETE(e.CommandArgument.ToString());
                    LoadItems();
                    break;
                #endregion
            }
        }

        protected void lnkcreatenew_Click(object sender, EventArgs e)
        {
            hdinsertupdate.Value = "insert";
            MultiView1.ActiveViewIndex = 1;
            DeleteFormValue();
            this.txtfromday.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
        }

        protected void ddlordertype_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems(); LoadRequest();
        }

        protected void ddlorderby_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems(); LoadRequest();
        }

        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems(); LoadRequest();
        }

        protected void btDeleteall_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < rpitems.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rpitems.Items[i].FindControl("chkid");
                    HiddenField id = (HiddenField)rpitems.Items[i].FindControl("hiID");
                    if (chk.Checked)
                    {
                        #region Delete
                        List<Entity.Dichvu> dlt = new List<Entity.Dichvu>();
                        dlt = SDichvu.GET_DETAIL_BYID(id.Value);
                        for (int j = 0; j < dlt.Count; j++)
                        {
                            try
                            {
                                File.Delete(utlitities.APPL_PHYSICAL_PATH + dlt[j].Images.ToString());

                            }
                            catch (Exception) { }

                            try
                            {
                                SModulControls.Name_Text("DELETE FROM ModulControls WHERE TangName='" + dlt[j].TangName + "'");
                            }
                            catch (Exception)
                            { }
                        }
                        #endregion
                        SDichvu.DELETE(id.Value);
                    }
                }
                LoadItems();
            }
            catch (Exception) { }
        }

        protected void rdFromComputer_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadView();
        }

        protected void rdFromLinks_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadView();
        }

        private void LoadView()
        {
            if (this.rdFromComputer.Checked)
            {
                this.MultiView2.SetActiveView(this.vwFromComputer);
            }
            else
            {
                this.MultiView2.SetActiveView(this.vwFromLinks);
            }
        }

        protected void lnksearch_Click(object sender, EventArgs e)
        {
            LoadItems();
            LoadRequest();
        }

        protected void bthienthi_Click(object sender, EventArgs e)
        {
            LoadItems();
            LoadRequest();
        }

        protected void btDeleteimages_Click(object sender, EventArgs e)
        {
            List<Entity.Dichvu> str5 = SDichvu.GET_BY_ID(hdid.Value);
            if (str5.Count > 0)
            {
                try
                {
                    ServerInfoUtlitities utlitities = new ServerInfoUtlitities();
                    File.Delete(utlitities.APPL_PHYSICAL_PATH + str5[0].Images.ToString());
                }
                catch (Exception) { }
            }
            this.hdimg.Value = "";
            SDichvu.UPDATE_IMG(hdid.Value);
            this.LoadItems();
            ltimg.Text = "";
            MultiView1.ActiveViewIndex = 1;
        }
        protected void LoadRequest()
        {
            Response.Redirect("admin.aspx?u=Dichvu&st=" + ddlstatus.SelectedValue + "&us=" + ddlorderby.SelectedValue + "&ds=" + ddlordertype.SelectedValue + "&kw=" + txtkeyword.Text + "");
        }
        protected void chkdaytype_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkdaytype.Checked)
            {
                this.pnadddate.Visible = true;
            }
            else
            {
                this.pnadddate.Visible = false;
            }
        }
    }
}
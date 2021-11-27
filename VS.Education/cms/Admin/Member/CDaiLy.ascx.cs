using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using System.IO;
using MoreAll;
using Entity;
using System.Configuration;
using TestWindowService;

namespace VS.E_Commerce.cms.Admin.Member
{
    public partial class CDaiLy : System.Web.UI.UserControl
    {
        string LogFile = ConfigurationManager.AppSettings.Get("LogFileCart");
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
            if (!base.IsPostBack)
            {
                #region UpdatePanel
                this.Page.Form.Enctype = "multipart/form-data";
                ScriptManager.GetCurrent(Page).RegisterPostBackControl(btn_InsertUpdate);
                #endregion
                if (!Commond.Setting("PageCateNews").Equals(""))
                {
                    ddlPage.SelectedValue = Commond.Setting("PageCateNews");
                }
                if (MoreAll.MoreAll.GetCookie("URole") != null)
                {
                    string strArray = MoreAll.MoreAll.GetCookie("URole").ToString().Trim();
                    if (strArray.Length > 0)
                    {
                        if (strArray.Contains("|10"))
                        {
                            this.LoadItems();
                        }
                        else if (!strArray.Contains("|10"))
                        {
                            Response.Redirect("/admin.aspx");
                        }
                    }
                }
            }
        }
        protected void btn_InsertUpdate_Click(object sender, EventArgs e)
        {
            // try
            //{
            if (this.txt_title.Text.Trim().Length < 1)
            {
                this.lblmsg.Text = "Xin vui lòng kiểm tra dữ liệu đầu vào của bạn";
            }
            else if (!ValidateUtilities.IsValidInt(this.txt_order.Text.Trim()))
            {
                this.lblmsg.Text = "Xin vui lòng kiểm tra dữ liệu đầu vào của bạn!";
            }
            else if (hdidthanhvien.Value.Trim().Length < 1)
            {
                this.lblmsg.Text = "Tên thành viên chưa đúng.";
            }
            else
            {
                string sgrnlevel = hidLevel.Value;
                ServerInfoUtlitities utlitities = new ServerInfoUtlitities();

                Entity.Menu obj = new Entity.Menu();
                string str5 = this.hd_insertupdate.Value.Trim();
                if (str5 != null)
                {
                    if (!(str5 == "update"))
                    {
                        if (str5 == "insert")
                        {
                            #region RewriteUrl
                            int cong = 0;
                            string TangName = "";
                            if (txtRewriteUrl.Text.Length > 0)
                            {
                                #region InsertMenu
                                List<Entity.ModulControls> curItem = SModulControls.Name_Text("SELECT top 1 * FROM ModulControls order by ID desc");
                                int tong = int.Parse(curItem[0].ID.ToString()); cong = tong + 1; var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txtRewriteUrl.Text)).FirstOrDefault(); TangName = hasTagName != null ? MoreAll.AddURL.SeoURL(txtRewriteUrl.Text) + "-" + cong : MoreAll.AddURL.SeoURL(txtRewriteUrl.Text);
                                #endregion
                            }
                            else
                            {
                                #region InsertMenu
                                List<Entity.ModulControls> curItem = SModulControls.Name_Text("SELECT top 1 * FROM ModulControls order by ID desc");
                                int tong = int.Parse(curItem[0].ID.ToString()); cong = tong + 1; var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txt_title.Text)).FirstOrDefault(); TangName = hasTagName != null ? MoreAll.AddURL.SeoURL(txt_title.Text) + "-" + cong : MoreAll.AddURL.SeoURL(txt_title.Text);
                                #endregion
                            }
                            ModulControl obm = new ModulControl();
                            obm.Name = txt_title.Text;
                            obm.Module = 30;
                            obm.TangName = TangName;
                            db.ModulControls.InsertOnSubmit(obm);
                            db.SubmitChanges();
                            #endregion

                            #region MyRegion
                            obj.Parent_ID = Convert.ToInt16(this.hd_id.Value.Trim());
                            obj.capp = More.DL;
                            obj.Type = int.Parse(hdidthanhvien.Value);
                            obj.Lang = lang;
                            obj.Name = txt_title.Text.Trim();
                            obj.Url_Name = RewriteURLNew.NameToTag(this.txt_title.Text.Trim());
                            obj.Link = "";
                            obj.Styleshow = "";
                            obj.Equals = 0;
                            obj.Images = txtImage.Text;
                            obj.Description = txtcontent.Text;
                            obj.Create_Date = DateTime.Now;
                            obj.Views = 0;
                            obj.ShowID = 0;
                            obj.Orders = Convert.ToInt16(txt_order.Text);
                            if (sgrnlevel.Length > 0)
                            {
                                obj.Level = sgrnlevel + "00000";
                            }
                            else
                            {
                                obj.Level = "00000";
                            }
                            obj.News = Convert.ToInt16(chknews.Checked ? "1" : "0");
                            obj.page_Home = Convert.ToInt16(chkTrangChu.Checked ? "1" : "0");
                            obj.Status = Convert.ToInt16(chck_Enable.Checked ? "1" : "0");
                            obj.Titleseo = txttitleseo.Text;
                            obj.Meta = txtmeta.Text;
                            obj.Keyword = txtKeyword.Text;
                            obj.Check_01 = 0;
                            obj.Check_02 = 0;
                            obj.Check_03 = 0;
                            obj.Check_04 = 0;
                            obj.Check_05 = 0;
                            obj.Noidung1 = "";
                            obj.Noidung2 = "";
                            obj.Noidung3 = "";
                            obj.Noidung4 = "";
                            obj.Noidung5 = "";
                            obj.Module = 30;
                            obj.TangName = TangName;
                            #endregion
                            SMenu.Insert(obj);
                        }
                    }
                    else
                    {

                        #region RewriteUrl
                        string TagName = "";
                        if (txtRewriteUrl.Text.Length > 0)
                        {
                            #region UpdateMenu
                            List<Entity.Menu> item = SMenu.GETBYID(hd_page_edit_id.Value);
                            if (item.Count > 0)
                            {
                                ModulControl obk = db.ModulControls.SingleOrDefault(p => p.TangName == item[0].TangName);
                                obk.Name = txt_title.Text;
                                obk.Module = 30;
                                List<ModulControl> list = (from p in db.ModulControls where p.TangName == obk.TangName orderby p.ID descending select p).ToList();
                                if (list.Count > 2)
                                {
                                    var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txtRewriteUrl.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txtRewriteUrl.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txtRewriteUrl.Text);
                                }
                                else
                                {
                                    if (MoreAll.AddURL.SeoURL(item[0].TangName) != MoreAll.AddURL.SeoURL(txtRewriteUrl.Text)) { var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txtRewriteUrl.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txtRewriteUrl.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txtRewriteUrl.Text); } else { TagName = item[0].TangName; }
                                }
                                obk.TangName = TagName;
                                db.SubmitChanges();
                            }
                            #endregion
                        }
                        else
                        {
                            #region UpdateMenu
                            List<Entity.Menu> item = SMenu.GETBYID(hd_page_edit_id.Value);
                            if (item.Count > 0)
                            {
                                ModulControl obk = db.ModulControls.SingleOrDefault(p => p.TangName == item[0].TangName);
                                obk.Name = txt_title.Text;
                                obk.Module = 30;
                                List<ModulControl> list = (from p in db.ModulControls where p.TangName == obk.TangName orderby p.ID descending select p).ToList();
                                if (list.Count > 2)
                                {
                                    var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txt_title.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txt_title.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txt_title.Text);
                                }
                                else
                                {
                                    if (MoreAll.AddURL.SeoURL(item[0].TangName) != MoreAll.AddURL.SeoURL(txt_title.Text)) { var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txt_title.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txt_title.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txt_title.Text); } else { TagName = item[0].TangName; }
                                }
                                obk.TangName = TagName;
                                db.SubmitChanges();
                            }
                            #endregion
                        }
                        #endregion
                        //
                        try
                        {
                            Menu iite = db.Menus.SingleOrDefault(p => p.ID == int.Parse(hd_page_edit_id.Value));
                            if (iite != null)
                            {
                                user thanhvienv = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(iite.Type.ToString()));
                                thanhvienv.ChiNhanh = 0;
                                db.SubmitChanges();
                            }
                        }
                        catch (Exception)
                        { }

                        #region MyRegion
                        obj.ID = Convert.ToInt16(hd_page_edit_id.Value);
                        obj.Parent_ID = Convert.ToInt16(this.hd_id.Value.Trim());
                        obj.capp = More.DL;
                        obj.Type = int.Parse(hdidthanhvien.Value);
                        obj.Lang = lang;
                        obj.Name = txt_title.Text.Trim();
                        obj.Url_Name = RewriteURLNew.NameToTag(this.txt_title.Text.Trim());
                        obj.Link = "";
                        obj.Styleshow = "";
                        obj.Equals = 0;
                        obj.Images = txtImage.Text;
                        obj.Description = txtcontent.Text;
                        obj.Create_Date = DateTime.Now;
                        obj.Views = 0;
                        obj.ShowID = 0;
                        obj.Orders = Convert.ToInt16(txt_order.Text);
                        obj.Level = sgrnlevel + "00000";
                        obj.News = Convert.ToInt16(chknews.Checked ? "1" : "0");
                        obj.page_Home = Convert.ToInt16(chkTrangChu.Checked ? "1" : "0");
                        obj.Status = Convert.ToInt16(chck_Enable.Checked ? "1" : "0");
                        obj.Titleseo = txttitleseo.Text;
                        obj.Meta = txtmeta.Text;
                        obj.Keyword = txtKeyword.Text;
                        obj.Check_01 = 0;
                        obj.Check_02 = 0;
                        obj.Check_03 = 0;
                        obj.Check_04 = 0;
                        obj.Check_05 = 0;
                        obj.Noidung1 = "";
                        obj.Noidung2 = "";
                        obj.Noidung3 = "";
                        obj.Noidung4 = "";
                        obj.Noidung5 = "";
                        obj.Module = 30;
                        obj.TangName = TagName;
                        #endregion
                        SMenu.UPDATE(obj);

                       
                    }
                }
                #region Sét cho thành viên là chi nhánh
                // Sét cho thành viên là chi nhánh
                user thanhvien = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(hdidthanhvien.Value));
                thanhvien.ChiNhanh = 1;
                db.SubmitChanges();
                #endregion

                this.LoadItems();
                this.pn_list.Visible = true;
                this.pn_insert.Visible = false;
                this.hd_insertupdate.Value = "";
                this.txt_title.Text = "";
                this.hd_insertupdate.Value = "insert";
                this.hd_id.Value = "-1";
                this.hdFileName.Value = "";
                txtImage.Text = "";
                hdimgnews.Value = "";
                ltimgs.Text = "";
                this.txtcontent.Text = "";
                txttitleseo.Text = "";
                txtmeta.Text = "";
                txtKeyword.Text = "";
                txtRewriteUrl.Text = "";
                ltshowurl.Text = "";
                this.lblmsg.Text = "";
                this.lbl_curpage.Text = "";
            }
            // }
            //catch (Exception) { }
        }

        private void btn_link_cancel_Click(object sender, EventArgs e)
        {
            this.pn_list.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.hd_insertupdate.Value = "";
            this.pn_list.Visible = true;
            this.pn_insert.Visible = false;
            this.hdFileName.Value = "";
            txtImage.Text = "";
            hdimgnews.Value = "";
            ltimgs.Text = "";
            this.txtcontent.Text = "";
            this.lblmsg.Text = "";
            txttitleseo.Text = "";
            txtmeta.Text = "";
            txtKeyword.Text = "";
            hidLevel.Value = "";
            lbl_curpage.Text = "";
            txtRewriteUrl.Text = "";
            ltshowurl.Text = "";
            hidLevel.Value = "";
        }

        protected void Delete_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Xóa bài viết này ?')";
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            this.pn_list.Visible = false;
            this.pn_insert.Visible = true;
            this.hd_insertupdate.Value = "insert";
            this.hd_page_edit_id.Value = "-1";
            this.hdFileName.Value = "";
            txtImage.Text = "";
            hdimgnews.Value = "";
            ltimgs.Text = "";
            this.txtcontent.Text = "";
            this.lblmsg.Text = "";
            this.txt_title.Text = "";
            hidLevel.Value = "";
            txtRewriteUrl.Text = "";
            ltshowurl.Text = "";
            if (hd_id.Value.Equals(""))
            {
                this.txt_order.Text = More.GetNextCateOrder(More.DL, this.lang, "-1").ToString();
            }
            else
            {
                this.txt_order.Text = More.GetNextCateOrder(More.DL, this.lang, hd_id.Value).ToString();
            }
            this.chknews.Checked = false;
            this.chkTrangChu.Checked = false;
        }

        protected void rp_pagelist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string str = e.CommandName.Trim();
            string str2 = e.CommandArgument.ToString().Trim();
            string str4 = str;

            switch (e.CommandName)
            {
                #region EditDetail
                case "EditDetail":
                    List<Entity.Menu> table = SMenu.GETBYID(str2);
                    if (table.Count > 0)
                    {
                        int _cid1 = -1;
                        if (int.TryParse(str2, out _cid1))
                        {
                            string nva1 = "";
                            this.lbl_curpage.Text = More.LoadNav(_cid1, ref nva1);
                        }

                        hidLevel.Value = table[0].Level.Substring(0, table[0].Level.Length - 5);

                        this.pn_list.Visible = false;
                        this.pn_insert.Visible = true;
                        this.hd_insertupdate.Value = "update";
                        this.hd_page_edit_id.Value = str2.Trim();
                        if (table.Count > 0)
                        {
                            hd_id.Value = table[0].Parent_ID.ToString().Trim();
                            this.hd_par_id.Value = table[0].Parent_ID.ToString().Trim();
                            this.hdid.Value = table[0].ID.ToString().Trim();
                            this.txt_title.Text = table[0].Name.ToString().Trim();
                            this.txt_order.Text = table[0].Orders.ToString().Trim();
                            this.txtcontent.Text = table[0].Description.ToString().Trim();
                            txtRewriteUrl.Text = table[0].TangName;

                            string ssl = "http://" + Request.Url.Host + "/";
                            if (Commond.Setting("SSL").Equals("1"))
                            {
                                ssl = "https://" + Request.Url.Host + "/";
                            }
                            ltshowurl.Text = ssl + table[0].TangName + ".html";
                            ltthongtin.Text = SearchThanhVienID(table[0].Type.ToString());

                            #region Seowwebsite

                            txttitleseo.Text = table[0].Titleseo.ToString().Trim();
                            txtmeta.Text = table[0].Meta.ToString().Trim();
                            txtKeyword.Text = table[0].Keyword.ToString().Trim();
                            #endregion

                            txtImage.Text = table[0].Images;
                            ltimgs.Text = MoreImage.Image(table[0].Images);
                            hdimgnews.Value = table[0].Images;
                            hdFileName.Value = table[0].Images.ToString();
                            chck_Enable.Checked = (table[0].Status == 1);
                            chknews.Checked = (table[0].News == 1);
                            chkTrangChu.Checked = (table[0].page_Home == 1);

                        }
                    }
                    return;
                #endregion
                case "Delete":
                    List<user> abt = db.users.Where(p => p.IDChiNhanh == int.Parse(str2)).ToList();
                    if (abt.Count > 0)
                    {
                        lthongbao.Text = "Không thể xóa Chi nhánh này khi đã có người đăng ký. Bạn chỉ có thể tắt trạng thái.!";
                    }
                    else
                    {
                        try
                        {
                            List<Entity.Menu> abk = SMenu.GETBYID(str2);
                            if (abk.Count > 0)
                            {
                                user thanhvien = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(abk[0].Type.ToString()));
                                thanhvien.ChiNhanh = 0;
                                db.SubmitChanges();
                            }
                        }
                        catch (Exception)
                        { }
                        SMenu.DELETE(str2);
                        this.LoadItems();
                        this.ltmsg.Text = "";
                    }
                    return;
                case "ListChildren":
                    this.hd_id.Value = str2;
                    var itemAdd = SMenu.Detail(str2);
                    hidLevel.Value = itemAdd[0].Level;
                    int _cid = -1;
                    if (int.TryParse(str2, out _cid))
                    {
                        string nva = "";
                        this.lbl_curpage.Text = More.LoadNav(_cid, ref nva);
                    }
                    if (hd_id.Value.Equals(""))
                    {
                        this.txt_order.Text = More.GetNextCateOrder(More.DL, this.lang, "-1").ToString();
                    }
                    else
                    {
                        this.txt_order.Text = More.GetNextCateOrder(More.DL, this.lang, hd_id.Value).ToString();
                    }
                    this.pn_list.Visible = false;
                    this.pn_insert.Visible = true;
                    this.hd_insertupdate.Value = "insert";
                    this.hd_page_edit_id.Value = "-1";
                    return;
                case "ChangeStatus":
                    string str3;
                    str2 = e.CommandArgument.ToString().Trim().Substring(0, e.CommandArgument.ToString().IndexOf("|"));
                    if (e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf("|") + 1, (e.CommandArgument.ToString().Length - e.CommandArgument.ToString().IndexOf("|")) - 1) == "1")
                    { str3 = "0"; }
                    else { str3 = "1"; }
                    SMenu.UPDATESTATUS(str2, str3);
                    this.LoadItems();
                    return;
                case "News":
                    {
                        string str33;
                        str2 = e.CommandArgument.ToString().Trim().Substring(0, e.CommandArgument.ToString().IndexOf("|"));
                        if (e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf("|") + 1, (e.CommandArgument.ToString().Length - e.CommandArgument.ToString().IndexOf("|")) - 1) == "1")
                        { str33 = "0"; }
                        else { str33 = "1"; }
                        SMenu.Updatemenu(str2, "News", str33);
                        this.LoadItems();
                    }
                    return;
                case "page_Home":
                    {
                        string str344;
                        str2 = e.CommandArgument.ToString().Trim().Substring(0, e.CommandArgument.ToString().IndexOf("|"));
                        if (e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf("|") + 1, (e.CommandArgument.ToString().Length - e.CommandArgument.ToString().IndexOf("|")) - 1) == "1")
                        { str344 = "0"; }
                        else { str344 = "1"; }
                        SMenu.Updatemenu(str2, "page_Home", str344);
                        this.LoadItems();
                    }
                    return;
                case "Tang":
                    SMenu.UPDATEVIEWS_T(str2);
                    this.LoadItems();
                    return;
                case "Giam":
                    SMenu.UPDATEVIEWS_G(str2);
                    this.LoadItems();
                    return;
            }
        }

        private void LoadItems()
        {
            try
            {
                if (this.hd_id.Value.Equals(""))
                {
                    this.hd_id.Value = "-1";
                }
                // List<Entity.Menu> table = SMenu.CATE_LOADALL_NEWS(More.DL, this.lang, this.hd_id.Value.Trim());
                List<Entity.Menu> table = SMenu.Name_Text("SELECT * FROM Menu where capp='" + More.DL + "' and lang='" + lang + "' order by level,Orders asc");
                CollectionPager1.DataSource = table;
                CollectionPager1.BindToControl = rp_pagelist;
                CollectionPager1.MaxPages = 10000;
                CollectionPager1.PageSize = int.Parse(ddlPage.SelectedValue);
                rp_pagelist.DataSource = CollectionPager1.DataSourcePaged;
                rp_pagelist.DataBind();
                RemoveCache.Menu();
                RemoveCache.News();
                if (this.hd_id.Value.Equals("-1"))
                {
                    this.lbl_curpage.Text = "";
                }
                else
                {
                    List<Entity.Menu> str = SMenu.Detail(this.hd_id.Value.Trim());
                    if (str.Count > 0)
                    {
                        this.hd_par_id.Value = str[0].Parent_ID.ToString().Trim();
                    }
                }
            }
            catch (Exception) { }
        }
        protected void btthemmoi_Click(object sender, EventArgs e)
        {
            this.pn_list.Visible = false;
            this.pn_insert.Visible = true;
            this.hd_insertupdate.Value = "insert";
            this.hd_page_edit_id.Value = "-1";
            this.hdFileName.Value = "";
            txtImage.Text = "";
            hdimgnews.Value = "";
            ltimgs.Text = "";
            this.txtcontent.Text = "";
            this.lblmsg.Text = "";
            this.chknews.Checked = false;
            this.chkTrangChu.Checked = false;
            hidLevel.Value = "";
            txtRewriteUrl.Text = "";
            ltshowurl.Text = "";
            if (hd_id.Value.Equals(""))
            {
                this.txt_order.Text = More.GetNextCateOrder(More.DL, this.lang, "-1").ToString();
            }
            else
            {
                this.txt_order.Text = More.GetNextCateOrder(More.DL, this.lang, hd_id.Value).ToString();
            }
        }

        //protected void txtOrders_TextChanged(object sender, EventArgs e)
        //{
        //    TextBox Orders = (TextBox)sender;
        //    var b = (HiddenField)Orders.FindControl("hiID");
        //    SMenu.Name_Text("UPDATE [Menu] SET Orders=" + Orders.Text + " WHERE ID=" + b.Value + " and capp='" + More.DL + "'");
        //    LoadItems();
        //    this.ltmsg.Text = "<span class=alert>Cập nhật thứ tự thành công.</span>";
        //}

        protected void txtTennhom_TextChanged(object sender, EventArgs e)
        {
            TextBox Nhom = (TextBox)sender;
            var b = (HiddenField)Nhom.FindControl("hiID");
            if (Nhom.Text.Length > 0)
            {
                #region UpdateMenu
                string TagName = "";
                List<Entity.Menu> item = SMenu.GETBYID(b.Value);
                if (item.Count > 0)
                {
                    Menu iitem = db.Menus.SingleOrDefault(p => p.TangName == item[0].TangName);
                    ModulControl obk = db.ModulControls.SingleOrDefault(p => p.TangName == item[0].TangName);
                    obk.Name = Nhom.Text;
                    obk.Module = 30;
                    List<ModulControl> list = (from p in db.ModulControls where p.TangName == obk.TangName orderby p.ID descending select p).ToList();
                    if (list.Count > 2)
                    {
                        var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(Nhom.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(Nhom.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(Nhom.Text);
                    }
                    else
                    {
                        if (MoreAll.AddURL.SeoURL(item[0].Name) != MoreAll.AddURL.SeoURL(Nhom.Text)) { var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(Nhom.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(Nhom.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(Nhom.Text); } else { TagName = item[0].TangName; }
                    }
                    obk.TangName = TagName;
                    db.SubmitChanges();


                    iitem.Name = Nhom.Text;
                    iitem.Module = 30;
                    iitem.TangName = TagName;
                    db.SubmitChanges();

                    LoadItems();
                    this.ltmsg.Text = "<span class=alert>Cập nhật tiêu đề thành công !!</span>";
                }
                #endregion
            }
            else
            {
                LoadItems();
                ltmsg.Text = "<span class=alert>Bạn chưa nhập tiêu đề !!</span>";
            }
        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            MoreAll.MoreAll.Update_setting("PageCateNews", ddlPage.SelectedValue);
            Response.Redirect(Request.RawUrl.ToString());
        }
        protected void bntcapnhat_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rp_pagelist.Items.Count; i++)
            {
                TextBox txtOrders = (TextBox)rp_pagelist.Items[i].FindControl("txtOrders");
                HiddenField id = (HiddenField)rp_pagelist.Items[i].FindControl("hiID");
                // Cập nhật thứ tự
                if (txtOrders.Text != "" && txtOrders.Text != "0")
                {

                    SMenu.Name_Text("UPDATE [Menu] SET Orders=" + txtOrders.Text + " WHERE ID=" + id.Value + " and capp='" + More.DL + "'");
                    ltmsg.Text = "<span class=alert>Cập nhật thành công !!</span>";
                }
            }
            LoadItems();
        }
        protected string ShowtThanhVien(string id)
        {
            string str = "";
            List<Entity.users> dt = Susers.GET_BY_ID(id);
            if (dt.Count >= 1)
            {
                str += "<span id=" + dt[0].iuser_id.ToString() + " style=\" color:red\">";
                if (dt[0].vfname.ToString().Length > 0)
                {
                    str += "<a target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&IDThanhVien=" + dt[0].iuser_id.ToString() + "\"><span style='color:red'>" + dt[0].vfname + " - (" + dt[0].vuserun + ")  - (Level:" + dt[0].LevelThanhVien + ")</span></a>";
                }
                str += "</span><br />";
                if (dt[0].vphone.ToString().Length > 0)
                {
                    str += dt[0].vphone + "<br />";
                }
                if (dt[0].vphone.ToString().Length > 0)
                {
                    str += "<span style=\" color:red;font-weight: bold;\">Ví Thương Mại:</span> " + dt[0].TongTienCoinDuocCap + "<br />";
                }
                if (dt[0].vphone.ToString().Length > 0)
                {
                    str += "<span style=\" color:red;font-weight: bold;\">Ví Quản lý:</span> " + dt[0].VIAAFFILIATE + "<br />";
                }
               
            }
            return str;
        }
        protected string ShowChiNhanh(string id)
        {
            var dt = db.S_Member_COUNT_ChiNhanh(Convert.ToInt32(id)).ToList();
            if (dt.Count() >= 0)
            {
                if (dt[0].Tong.ToString() != "")
                {
                    return "<a style=\"background:#ed1c24;padding: 4px;margin:10px;color:#fff;border-radius: 3px;font-weight: 600;\" target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&chinhanh=" + id + "\">" + dt[0].Tong.ToString() + "</a>";
                }
                else
                {
                    return "<a style=\"background:#00a9d2;padding: 4px;margin:10px;color:#fff;border-radius: 3px;font-weight: 600;\">0</a>";
                }
            }
            else
            {
                return "<a style=\"background:#00a9d2;padding: 4px;margin:10px;color:#fff;border-radius: 3px;font-weight: 600;\">0</a>";
            }
            //string str = "0";
            //List<Entity.users> dt = Susers.Name_Text("SELECT * FROM users where IDChiNhanh=" + id + " ");
            //if (dt.Count >= 1)
            //{
            //    return "<a style=\"background:#ed1c24;padding: 4px;margin:10px;color:#fff;border-radius: 3px;font-weight: 600;\" target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&chinhanh=" + id + "\">" + dt.Count.ToString() + "</a>";
            //}
            //return "<a style=\"background:#00a9d2;padding: 4px;margin:10px;color:#fff;border-radius: 3px;font-weight: 600;\">0</a>";
        }

        protected string ShowChiNhanhChuaKH(string id)
        {
            var dt = db.S_Member_COUNT_ChiNhanh_ChuaKichHoat(Convert.ToInt32(id)).ToList();
            if (dt.Count() >= 0)
            {
                if (dt[0].Tong.ToString() != "")
                {
                    return "<a style=\"background:#ed1c24;padding: 4px;margin:10px;color:#fff;border-radius: 3px;font-weight: 600;\" target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&chinhanh=" + id + "\">" + dt[0].Tong.ToString() + "</a>";
                }
                else
                {
                    return "<a style=\"background:#00a9d2;padding: 4px;margin:10px;color:#fff;border-radius: 3px;font-weight: 600;\">0</a>";
                }
            }
            else
            {
                return "<a style=\"background:#00a9d2;padding: 4px;margin:10px;color:#fff;border-radius: 3px;font-weight: 600;\">0</a>";
            }
        }

        protected string ShowChiNhanhDaKH(string id)
        {
            var dt = db.S_Member_COUNT_ChiNhanh_DaKichHoat(Convert.ToInt32(id)).ToList();
            if (dt.Count() >= 0)
            {
                if (dt[0].Tong.ToString() != "")
                {
                    return "<a style=\"background:#ed1c24;padding: 4px;margin:10px;color:#fff;border-radius: 3px;font-weight: 600;\" target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&chinhanh=" + id + "\">" + dt[0].Tong.ToString() + "</a>";
                }
                else
                {
                    return "<a style=\"background:#00a9d2;padding: 4px;margin:10px;color:#fff;border-radius: 3px;font-weight: 600;\">0</a>";
                }
            }
            else
            {
                return "<a style=\"background:#00a9d2;padding: 4px;margin:10px;color:#fff;border-radius: 3px;font-weight: 600;\">0</a>";
            }
        }
        protected void btkiemtra_Click(object sender, EventArgs e)
        {
            string ssl = "http://" + Request.Url.Host + "/";
            if (Commond.Setting("SSL").Equals("1"))
            {
                ssl = "https://" + Request.Url.Host + "/";
            }
            if (hd_insertupdate.Value.Equals("insert"))
            {
                #region RewriteUrl
                int cong = 0;
                string TangName = "";
                if (txtRewriteUrl.Text.Length > 0)
                {
                    #region InsertMenu
                    List<Entity.ModulControls> curItem = SModulControls.Name_Text("SELECT top 1 * FROM ModulControls order by ID desc");
                    int tong = int.Parse(curItem[0].ID.ToString()); cong = tong + 1; var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txtRewriteUrl.Text)).FirstOrDefault(); TangName = hasTagName != null ? MoreAll.AddURL.SeoURL(txtRewriteUrl.Text) + "-" + cong : MoreAll.AddURL.SeoURL(txtRewriteUrl.Text);
                    #endregion
                }
                else
                {
                    #region InsertMenu
                    List<Entity.ModulControls> curItem = SModulControls.Name_Text("SELECT top 1 * FROM ModulControls order by ID desc");
                    int tong = int.Parse(curItem[0].ID.ToString()); cong = tong + 1; var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txt_title.Text)).FirstOrDefault(); TangName = hasTagName != null ? MoreAll.AddURL.SeoURL(txt_title.Text) + "-" + cong : MoreAll.AddURL.SeoURL(txt_title.Text);
                    #endregion
                }
                ltshowurl.Text = ssl + TangName + ".html";
                #endregion
            }
            else
            {
                #region RewriteUrl
                string TagName = "";
                if (txtRewriteUrl.Text.Length > 0)
                {
                    #region UpdateMenu
                    List<Entity.Menu> item = SMenu.GETBYID(hd_page_edit_id.Value);
                    if (item.Count > 0)
                    {
                        ModulControl obk = db.ModulControls.SingleOrDefault(p => p.TangName == item[0].TangName);
                        obk.Name = txtRewriteUrl.Text;
                        obk.Module = 30;
                        List<ModulControl> list = (from p in db.ModulControls where p.TangName == obk.TangName orderby p.ID descending select p).ToList();
                        if (list.Count > 2)
                        {
                            var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txtRewriteUrl.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txtRewriteUrl.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txtRewriteUrl.Text);
                        }
                        else
                        {
                            if (MoreAll.AddURL.SeoURL(item[0].TangName) != MoreAll.AddURL.SeoURL(txtRewriteUrl.Text)) { var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txtRewriteUrl.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txtRewriteUrl.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txtRewriteUrl.Text); } else { TagName = item[0].TangName; }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region UpdateMenu
                    List<Entity.Menu> item = SMenu.GETBYID(hd_page_edit_id.Value);
                    if (item.Count > 0)
                    {
                        ModulControl obk = db.ModulControls.SingleOrDefault(p => p.TangName == item[0].TangName);
                        obk.Name = txt_title.Text;
                        obk.Module = 30;
                        List<ModulControl> list = (from p in db.ModulControls where p.TangName == obk.TangName orderby p.ID descending select p).ToList();
                        if (list.Count > 2)
                        {
                            var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txt_title.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txt_title.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txt_title.Text);
                        }
                        else
                        {
                            if (MoreAll.AddURL.SeoURL(item[0].TangName) != MoreAll.AddURL.SeoURL(txt_title.Text)) { var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txt_title.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txt_title.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txt_title.Text); } else { TagName = item[0].TangName; }
                        }
                    }
                    #endregion
                }
                ltshowurl.Text = ssl + TagName + ".html";
                #endregion
            }
        }
        protected string ShowIDChiNhanh(string id)
        {
            string str = "0";
            List<Entity.Menu> dt = SMenu.GETBYID(id);
            if (dt.Count >= 1)
            {
                str = dt[0].Type.ToString();// chính là ID của thành viên nào đang quản lý (Bảng thành viên)
            }
            return str;
        }

        protected void txtTenthanhvien_TextChanged(object sender, EventArgs e)
        {
            TextBox Tieude = (TextBox)sender;
            ltthongtin.Text = SearchThanhVien(Tieude.Text.Trim().Replace("&nbsp;", ""));
        }
        protected string SearchThanhVien(string keyword)
        {
            List<Entity.users> dt = Susers.Name_Text("select * from users where (vuserun ='" + keyword + "')");
            if (dt.Count >= 1)
            {
                hdidthanhvien.Value = dt[0].iuser_id.ToString();
                return dt[0].vfname.ToString() + " - " + dt[0].vphone.ToString();

            }
            return "";
        }
        protected string SearchThanhVienID(string id)
        {
            List<Entity.users> dt = Susers.Name_Text("select * from users where iuser_id=" + id + " ");
            if (dt.Count >= 1)
            {
                txtTenthanhvien.Text = dt[0].vuserun.ToString();
                hdidthanhvien.Value = dt[0].iuser_id.ToString();
                return dt[0].vfname.ToString() + " - " + dt[0].vphone.ToString();
            }
            return "";
        }
    }
}
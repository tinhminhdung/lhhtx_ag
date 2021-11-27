using Entity;
using Framework;
using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.Page
{
    public partial class index : System.Web.UI.Page
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
                //if (kichhoat == "1")
                //{
                //    Setting obj = new Setting();
                //    obj.Lang = lang;
                //    obj.Properties = "Email";
                //    obj.Value = "1";
                //    SSetting.UPDATE(obj);
                //    this.binddata();
                //    this.ltmsg.Text = "1 ---> Kích hoạt gửi Email!";
                //}
                //else if (kichhoat == "2")
                //{
                //    Setting obj = new Setting();
                //    obj.Lang = lang;
                //    obj.Properties = "Thongbao";
                //    obj.Value = "1";
                //    SSetting.UPDATE(obj);
                //    this.binddata();
                //    this.ltmsg.Text = "2---> Kích hoạt Thông báo  +++  lấy nội dung trong CKEditor !";
                //}
                //else if (kichhoat == "3")
                //{
                //    Setting obj = new Setting();
                //    obj.Lang = lang;
                //    obj.Properties = "Thongbao";
                //    obj.Value = "2";
                //    SSetting.UPDATE(obj);
                //    this.binddata();
                //    this.ltmsg.Text = "3---> Kích hoạt Thông báo  +++  lấy nội dung trong Code !";
                //}
                //else if (kichhoat == "4")
                //{
                //    Setting obj = new Setting();
                //    obj.Lang = lang;
                //    obj.Properties = "Thongbao";
                //    obj.Value = "0";
                //    SSetting.UPDATE(obj);
                //    this.binddata();
                //    this.ltmsg.Text = "4---> Tắt nội dung vi phạm!";
                //}
                //else if (kichhoat == "0")
                //{
                //    Setting obj = new Setting();
                //    obj.Lang = lang;
                //    obj.Properties = "Thongbao";
                //    obj.Value = "0";
                //    SSetting.UPDATE(obj);

                //    obj.Lang = lang;
                //    obj.Properties = "Email";
                //    obj.Value = "0";
                //    SSetting.UPDATE(obj);

                //    this.binddata();
                //    this.ltmsg.Text = "0 ---> Tắt thông báo và Gửi Email";
                //}
                //else if (kichhoat == "5")
                //{
                //    Setting obj = new Setting();
                //    obj.Lang = lang;
                //    obj.Properties = "Email";
                //    obj.Value = "0";
                //    SSetting.UPDATE(obj);
                //    this.binddata();
                //    this.ltmsg.Text = "5 ---> Tắt gửi Email!";
                //}

                #region UpdatePanel
                this.Page.Form.Enctype = "multipart/form-data";
                ScriptManager.GetCurrent(Page).RegisterPostBackControl(btn_InsertUpdate);
                ScriptManager.GetCurrent(Page).RegisterPostBackControl(btDeleteimages);
                #endregion
                this.btnCancel.Text = this.label("l_cancel");
            }
            this.ShowList();

        }
        public void binddata()
        {
            FSetting DB = new FSetting();
            List<Entity.Setting> str = DB.GETBYALL(language);
            ltmsg.Text = string.Empty;
            string Thongbao = "0";
            string Email = "0";

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
                    else if (its.Properties == "Email")
                    {
                        Email = its.Value;
                    }
                    else if (its.Properties == "Redirectwebsite")
                    {
                        this.txtRedirect.Text = its.Value;
                    }
                }
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
                obj.Properties = "Email";
                obj.Value = Email.ToString();
                SSetting.UPDATE(obj);

                obj.Lang = language;
                obj.Properties = "Redirectwebsite";
                obj.Value = txtRedirect.Text;
                SSetting.UPDATE(obj);

                this.binddata();
                this.ltmsg.Text = "Thiết lập th\x00e0nh c\x00f4ng!";
            }
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
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
            else
            {
                string sgrnlevel = hidLevel.Value;
                ServerInfoUtlitities utlitities = new ServerInfoUtlitities();
                #region image
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
                        vimg = "/Uploads/prods/" + DateTime.Now.Ticks.ToString() + str6;
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
                Entity.Menu obj = new Entity.Menu();
                string str5 = this.hd_insertupdate.Value.Trim();
                if (str5 != null)
                {
                    if (!(str5 == "update"))
                    {
                        if (str5 == "insert")
                        {
                            #region Menu
                            string TangName = "";
                            int cong = 0;
                            List<Entity.Menu> curItem = SMenu.Name_Text("SELECT top 1 * FROM Menu order by ID desc");
                            int tong = int.Parse(curItem[0].ID.ToString());
                            cong = tong + 1;
                            var hasTagName = db.Menus.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txt_title.Text)).FirstOrDefault();
                            TangName = hasTagName != null ? MoreAll.AddURL.SeoURL(txt_title.Text) + "-" + cong : MoreAll.AddURL.SeoURL(txt_title.Text);
                            #endregion
                            #region MyRegion
                            obj.Parent_ID = Convert.ToInt16(this.hd_id.Value.Trim());
                            obj.capp = More.VP;
                            obj.Type = 0;
                            obj.Lang = language;
                            obj.Name = txt_title.Text.Trim();
                            obj.Url_Name = RewriteURLNew.NameToTag(this.txt_title.Text.Trim());
                            obj.Link = "";
                            obj.Styleshow = "";
                            obj.Equals = Convert.ToInt16(vkey);
                            obj.Images = vimg;
                            obj.Description = txtlink.Text;
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
                            obj.Module = 1;
                            obj.TangName = TangName;
                            #endregion
                            SMenu.Insert(obj);
                        }
                    }
                    else
                    {
                        #region Delete
                        if (vimg.Equals(""))
                        {
                            vimg = this.hdFileName.Value;
                        }
                        else
                        {
                            try
                            {
                                if ((this.txtvimg.Text.Trim().Length > 0))
                                {
                                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/Uploads/All/" + this.hdFileName.Value);
                                }
                                if ((this.flimage.FileName.Trim().Length > 0) && (this.flimage.PostedFile.ContentLength > 0))
                                {
                                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/Uploads/All/" + this.hdFileName.Value);
                                }
                            }
                            catch (Exception) { }
                        }
                        #endregion
                        #region UpdateMenu
                        string TagName = "";
                        List<Entity.Menu> item = SMenu.GETBYID(hd_page_edit_id.Value);
                        if (item.Count > 0)
                        {
                            Menu obk = db.Menus.SingleOrDefault(p => p.TangName == item[0].TangName);
                            obk.Name = txt_title.Text;
                            obk.Module = 1;
                            List<Menu> list = (from p in db.Menus where p.TangName == obk.TangName orderby p.ID descending select p).ToList();
                            if (list.Count > 2)
                            {
                                var hasTagName = db.Menus.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txt_title.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txt_title.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txt_title.Text);
                            }
                            else
                            {
                                if (MoreAll.AddURL.SeoURL(item[0].Name) != MoreAll.AddURL.SeoURL(txt_title.Text)) { var hasTagName = db.Menus.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txt_title.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txt_title.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txt_title.Text); } else { TagName = item[0].TangName; }
                            }
                            obk.TangName = TagName;
                            db.SubmitChanges();
                        }

                        #endregion
                        #region MyRegion
                        obj.ID = Convert.ToInt16(hd_page_edit_id.Value);
                        obj.Parent_ID = Convert.ToInt16(this.hd_id.Value.Trim());
                        obj.capp = More.VP;
                        obj.Type = 0;
                        obj.Lang = language;
                        obj.Name = txt_title.Text.Trim();
                        obj.Url_Name = RewriteURLNew.NameToTag(this.txt_title.Text.Trim());
                        obj.Link = "";
                        obj.Styleshow = "";
                        obj.Equals = Convert.ToInt16(vkey);
                        obj.Images = vimg;
                        obj.Description = txtlink.Text;
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
                        obj.Module = 1;
                        obj.TangName = TagName;
                        #endregion
                        SMenu.UPDATE(obj);
                    }
                }
                this.ShowList();
                this.pn_list.Visible = true;
                this.pn_insert.Visible = false;
                this.hd_insertupdate.Value = "";
                this.txt_title.Text = "";
                this.hd_insertupdate.Value = "insert";
                this.hd_id.Value = "-1";
                this.txtvimg.Text = "";
                this.hdFileName.Value = "";
                ltimg.Text = "";
                this.txtcontent.Text = "";
                txttitleseo.Text = "";
                txtmeta.Text = "";
                txtKeyword.Text = "";
                pnseo.Visible = false;
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
            this.txtvimg.Text = "";
            this.hdFileName.Value = "";
            ltimg.Text = "";
            this.txtcontent.Text = "";
            this.lblmsg.Text = "";
            txttitleseo.Text = "";
            txtmeta.Text = "";
            txtKeyword.Text = "";
            hidLevel.Value = "";
            lbl_curpage.Text = "";
            pnseo.Visible = false;
            hidLevel.Value = "";
        }

        protected void Delete_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Xóa bài viết này ?')";
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            //this.lt_info.Text = " - " + this.label("l_createnew");
            this.btn_InsertUpdate.Text = this.label("l_insert");
            this.pn_list.Visible = false;
            this.pn_insert.Visible = true;
            this.hd_insertupdate.Value = "insert";
            this.hd_page_edit_id.Value = "-1";
            this.txtvimg.Text = "";
            this.hdFileName.Value = "";
            this.ltimg.Text = "";
            this.txtcontent.Text = "";
            this.lblmsg.Text = "";
            this.txt_title.Text = "";
            hidLevel.Value = "";
            if (hd_id.Value.Equals(""))
            {
                this.txt_order.Text = More.GetNextCateOrder(More.VP, this.language, "-1").ToString();
            }
            else
            {
                this.txt_order.Text = More.GetNextCateOrder(More.VP, this.language, hd_id.Value).ToString();
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

                        this.btn_InsertUpdate.Text = this.label("l_update");
                        // this.lt_info.Text = " - " + this.label("lt_edit");
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
                            this.txtlink.Text = table[0].Description.ToString().Trim();

                            #region Seowwebsite
                            if (table[0].Titleseo.ToString().Length > 0)
                            {
                                pnseo.Visible = true;
                            }
                            txttitleseo.Text = table[0].Titleseo.ToString().Trim();
                            txtmeta.Text = table[0].Meta.ToString().Trim();
                            txtKeyword.Text = table[0].Keyword.ToString().Trim();
                            #endregion

                            ltimg.Text = MoreImage.Image(table[0].Images.ToString());
                            hdFileName.Value = table[0].Images.ToString();
                            if (table[0].Status.ToString().Trim().Equals("0"))
                            {
                                this.chck_Enable.Checked = false;
                            }
                            else if (table[0].Status.ToString().Equals("1"))
                            {
                                this.chck_Enable.Checked = true;
                            }
                            this.chck_Enable.Checked = true;


                            if (table[0].News.ToString().Trim().Equals("0"))
                            {
                                this.chknews.Checked = false;
                            }
                            else if (table[0].News.ToString().Equals("1"))
                            {
                                this.chknews.Checked = true;
                            }
                            this.chknews.Checked = true;


                            if (table[0].page_Home.ToString().Trim().Equals("0"))
                            {
                                this.chkTrangChu.Checked = false;
                            }
                            else if (table[0].page_Home.ToString().Equals("1"))
                            {
                                this.chkTrangChu.Checked = true;
                            }
                            this.chkTrangChu.Checked = true;


                            if (table[0].Equals.ToString().Trim().Equals("1"))
                            {
                                this.rdFromLinks.Checked = true;
                                this.rdFromComputer.Checked = false;
                                this.LoadView();
                                this.txtvimg.Text = table[0].Images.ToString();
                            }
                            else
                            {
                                this.rdFromComputer.Checked = true;
                                this.rdFromLinks.Checked = false;
                                this.LoadView();
                                this.hdFileName.Value = table[0].Images.ToString();
                            }
                        }
                    }
                    return;
                #endregion
                case "Delete":
                    {
                        try
                        {
                            SMenu.DELETE(More.Sub_Menu(More.VP, str2));
                            SMenu.DELETE(str2);
                            this.ShowList();
                            this.ltmsg.Text = "";
                        }
                        catch (Exception)
                        {
                            this.ltmsg.Text = "<span class=alert> : " + label("lt_youmustdeleteallitemsinthiscategoryfirst") + "</span>";
                        }
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
                        this.txt_order.Text = More.GetNextCateOrder(More.VP, this.language, "-1").ToString();
                    }
                    else
                    {
                        this.txt_order.Text = More.GetNextCateOrder(More.VP, this.language, hd_id.Value).ToString();
                    }
                    this.btn_InsertUpdate.Text = this.label("l_insert");
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
                    this.ShowList();
                    return;
                case "News":
                    {
                        string str33;
                        str2 = e.CommandArgument.ToString().Trim().Substring(0, e.CommandArgument.ToString().IndexOf("|"));
                        if (e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf("|") + 1, (e.CommandArgument.ToString().Length - e.CommandArgument.ToString().IndexOf("|")) - 1) == "1")
                        { str33 = "0"; }
                        else { str33 = "1"; }
                        SMenu.Updatemenu(str2, "News", str33);
                        this.ShowList();
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
                        this.ShowList();
                    }
                    return;
                case "Tang":
                    SMenu.UPDATEVIEWS_T(str2);
                    this.ShowList();
                    return;
                case "Giam":
                    SMenu.UPDATEVIEWS_G(str2);
                    this.ShowList();
                    return;
            }
        }

        private void ShowList()
        {
            try
            {
                if (this.hd_id.Value.Equals(""))
                {
                    this.hd_id.Value = "-1";
                }
                List<Entity.Menu> table = SMenu.Name_Text("SELECT * FROM Menu where capp='" + More.VP + "' and lang='" + language + "' order by level,Orders asc");
                rp_pagelist.DataSource = table;
                rp_pagelist.DataBind();

                if (this.hd_id.Value.Equals("-1"))
                {
                    this.lbl_curpage.Text = this.label("l_rootcate");
                }
                else
                {
                    List<Entity.Menu> str = SMenu.Detail(this.hd_id.Value.Trim());
                    if (str.Count > 0)
                    {
                        //  this.lbl_curpage.Text = this.label("l_rootcate") + " : " + str[0].Name.ToString().Trim();
                        this.hd_par_id.Value = str[0].Parent_ID.ToString().Trim();
                    }
                }
            }
            catch (Exception) { }
        }

        protected void btxoa_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < rp_pagelist.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)rp_pagelist.Items[i].FindControl("chkid");
                    HiddenField id = (HiddenField)rp_pagelist.Items[i].FindControl("hiID");
                    if (chk.Checked)
                    {
                        SMenu.DELETE_PARENT(More.Sub_Menu(More.VP, id.Value));
                        SMenu.DELETE(id.Value);
                    }
                }
                ShowList();
            }
            catch (Exception)
            {
                this.ltmsg.Text = "<span class=alert> : " + label("lt_youmustdeleteallitemsinthiscategoryfirst") + "</span>";
            }
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

        protected void btDeleteimages_Click(object sender, EventArgs e)
        {
            List<Entity.Menu> table = SMenu.GETBYID(hdid.Value);
            if (table.Count > 0)
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/" + table[0].Images);
            }
            SMenu.Name_Text("update Menu set Images='' where ID=" + hdid.Value + " capp='" + More.VP + "'");
            this.txtvimg.Text = "";
            this.hdFileName.Value = "";
            this.ltimg.Text = "";
            this.txtcontent.Text = "";
            this.ShowList();
            this.pn_list.Visible = false;
            this.pn_insert.Visible = true;
        }

        protected void btthemmoi_Click(object sender, EventArgs e)
        {
            this.btn_InsertUpdate.Text = this.label("l_insert");
            this.pn_list.Visible = false;
            this.pn_insert.Visible = true;
            this.hd_insertupdate.Value = "insert";
            this.hd_page_edit_id.Value = "-1";
            this.txtvimg.Text = "";
            this.hdFileName.Value = "";
            ltimg.Text = "";
            this.txtcontent.Text = "";
            this.lblmsg.Text = "";
            this.chknews.Checked = false;
            this.chkTrangChu.Checked = false;
            hidLevel.Value = "";
            if (hd_id.Value.Equals(""))
            {
                this.txt_order.Text = More.GetNextCateOrder(More.VP, this.language, "-1").ToString();
            }
            else
            {
                this.txt_order.Text = More.GetNextCateOrder(More.VP, this.language, hd_id.Value).ToString();
            }
        }

        protected void btseo_Click(object sender, EventArgs e)
        {
            pnseo.Visible = true;
            System.Threading.Thread.Sleep(1000);
        }

        protected void txtOrders_TextChanged(object sender, EventArgs e)
        {
            TextBox Orders = (TextBox)sender;
            var b = (HiddenField)Orders.FindControl("hiID");
            SMenu.Name_Text("UPDATE [Menu] SET Orders=" + Orders.Text + " WHERE ID=" + b.Value + " and capp='" + More.VP + "'");
            ShowList();
            this.ltmsg.Text = "<span class=alert>: Cập nhật thứ tự thành công.</span>";
        }

        protected void btrunwwebsite_Click(object sender, EventArgs e)
        {
            string str = "";
            List<Entity.Menu> table = SMenu.Name_Text("SELECT * FROM Menu where capp='" + More.VP + "' and lang='" + language + "' order by level,Orders asc");
            if (table.Count > 0)
            {
                foreach (var item in table)
                {
                    Response.Write("<div><iframe width=500px height=100px style=\" overflow:hidden\" scrolling=no  frameborder=no src=\"" + item.Description + "\" marginheight=0 marginwidth=0></iframe></div>");
                    str += item.Description + "<br>";
                }
            }
            this.binddata();
            ltmsg1.Text = str;
        }
    }
}
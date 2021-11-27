using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;
using System.IO;
using Entity;
using Framework;
using System.Drawing.Imaging;

namespace VS.E_Commerce.cms.Admin.M_News
{
    public partial class MNews : System.Web.UI.UserControl
    {
        DatalinqDataContext db = new DatalinqDataContext();
        private string status = "";
        private string lang = Captionlanguage.Language;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region MyRegion
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
            #endregion
            this.Page.Form.DefaultButton = lnksearch.UniqueID;
            if (!IsPostBack)
            {
                #region UpdatePanel
                this.Page.Form.Enctype = "multipart/form-data";
                ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnsave);
                #endregion
                #region #
                try
                {
                    if (Request["st"] != null && !Request["st"].Equals(""))
                    {
                        ddlstatus.SelectedValue = Request["st"];
                        WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlstatus, this.status);
                    }

                    LoadCategories();
                    LoadItems();
                }
                catch (Exception) { }
                #endregion
            }
        }

        #region Menu
        protected void LoadCategories()
        {
            try
            {
                if (Request["id"] != null && !Request["id"].Equals(""))
                {
                    ddlcategories.SelectedValue = Request["id"];
                }
                int str = 0;
                List<Entity.Menu> dt = SMenu.LOAD_CATESPARENT_ID(More.NS, this.lang, "-1", "1");
                for (int i = 0; i < dt.Count; i++)
                {
                    if (dt[i].Parent_ID.ToString() == "-1")
                    {
                        ddlcategories.Items.Insert(str, new ListItem(dt[i].Name.ToString(), dt[i].ID.ToString()));
                        ddlcategoriesdetail.Items.Insert(str, new ListItem(dt[i].Name.ToString(), dt[i].ID.ToString()));
                        str = str + 1;
                        str = Categories(dt[i].ID.ToString(), str, "===");
                    }
                }
                this.ddlcategories.Items.Insert(0, new ListItem("Tất cả các mục", "-1"));
                this.ddlcategories.DataBind();
                this.ddlcategoriesdetail.DataBind();
            }
            catch (Exception) { }
        }
        protected int Categories(string id, int str, string j)
        {
            List<Entity.Menu> dt = SMenu.LOAD_CATESPARENT_ID(More.NS, this.lang, id, "1");
            for (int i = 0; i < dt.Count; i++)
            {
                if (dt[i].Parent_ID.ToString() == id)
                {
                    ddlcategories.Items.Insert(str, new ListItem(j + dt[i].Name.ToString(), dt[i].ID.ToString()));
                    ddlcategoriesdetail.Items.Insert(str, new ListItem(j + dt[i].Name.ToString(), dt[i].ID.ToString()));
                    str = str + 1;
                    str = Categories(dt[i].ID.ToString(), str, j + j);
                }
            }
            return str;
        }
        #endregion

        void LoadItems()
        {
            try
            {
                FNews DB = new FNews();
                string[] searchfields = new string[] { "Title", "Brief", "Contents", "search" };
                string orderby = this.ddlorderby.SelectedValue + " " + this.ddlordertype.SelectedValue;
                List<News> dt = DB.CATEGORYADMIN(orderby, this.txtkeyword.Text.Trim().Replace("&nbsp;", ""), searchfields, More.Sub_Menu(More.NS, ddlcategories.SelectedValue), lang, ddlstatus.SelectedValue);
                CollectionPager1.DataSource = dt;
                CollectionPager1.BindToControl = rpitems;
                CollectionPager1.MaxPages = 10000;
                CollectionPager1.PageSize = 10;
                rpitems.DataSource = CollectionPager1.DataSourcePaged;
                rpitems.DataBind();
                RemoveCache.News();
            }
            catch (Exception) { }
        }

        protected void lnkcreatenew_Click(object sender, EventArgs e)
        {
            hdinsertupdate.Value = "insert";
            MultiView1.ActiveViewIndex = 1;
            DeleteFormValue();
            this.txtfromday.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlcategoriesdetail, this.ddlcategories.SelectedValue);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            DeleteFormValue();
            base.Response.Redirect(base.Request.Url.ToString().Trim());
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //  try
            {
                WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlcategories, this.ddlcategoriesdetail.SelectedValue);
                if (this.txtname.Text.Trim().Length < 1)
                {
                    this.lbl_msg.Text = "Xin vui lòng kiểm tra dữ liệu đầu vào của bạn";
                }
                else
                {
                    #region Img
                    string vimg = this.txtvimg.Text;
                    ServerInfoUtlitities utlitities = new ServerInfoUtlitities();
                    int vkey = 1;
                    string path = "";
                    string small = "";
                    if (this.rdFromComputer.Checked)
                    {
                        if ((this.flimage.FileName.Trim().Length > 0) && (this.flimage.PostedFile.ContentLength > 0))
                        {
                            String pathcategorise = Database.ConverCodeUni(ddlcategoriesdetail.SelectedItem.Text.Trim());
                            String paths = "/Uploads/All/" + pathcategorise + "/";
                            if (Directory.Exists(Server.MapPath(paths)) != true)
                            {
                                Directory.CreateDirectory(Server.MapPath(paths));
                            }
                            path = Path.GetFileName(this.flimage.PostedFile.FileName);
                            string str6 = "";
                            str6 = Path.GetExtension(path).ToLower();
                            vimg = paths + DateTime.Now.Ticks.ToString() + str6;
                            flimage.PostedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + vimg);
                            small = paths + DateTime.Now.Ticks.ToString() + "SMall" + str6;
                            Database.ResizeIamgesFix(Server.MapPath(vimg), Server.MapPath(small), Convert.ToInt32(AllQuery.MoreNews.Height()), Convert.ToInt32(AllQuery.MoreNews.Width()));
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

                    #region date
                    string Chek = "0";
                    string cdate = DateTime.Now.ToString();
                    string edate = DateTime.Now.AddYears(10).ToString();
                    DateTime dcreatedate = Convert.ToDateTime(cdate.ToString());
                    DateTime denddate = Convert.ToDateTime(edate.ToString());
                    if (this.chkdaytype.Checked)
                    {
                        Chek = "1";
                        dcreatedate = Convert.ToDateTime(this.txtfromday.Text);
                        denddate = dcreatedate.AddDays((double)Convert.ToInt32(this.txtindays.Text));
                    }
                    #endregion
                    News obj = new News();
                    if (hdinsertupdate.Value.Equals("insert"))
                    {
                        #region RewriteUrl
                        int cong = 0;
                        string TangName = "";
                        ModulControl obm = new ModulControl();
                        obm.Name = txtname.Text;
                        obm.Module = 2;
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
                            int tong = int.Parse(curItem[0].ID.ToString()); cong = tong + 1; var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txtname.Text)).FirstOrDefault(); TangName = hasTagName != null ? MoreAll.AddURL.SeoURL(txtname.Text) + "-" + cong : MoreAll.AddURL.SeoURL(txtname.Text);
                            #endregion
                        }
                        obm.TangName = TangName;
                        db.ModulControls.InsertOnSubmit(obm);
                        db.SubmitChanges();
                        #endregion

                        #region MyRegion
                        obj.icid = int.Parse(this.ddlcategoriesdetail.SelectedValue);
                        obj.Title = this.txtname.Text;
                        obj.Brief = this.txtdesc.Text;
                        obj.Contents = this.txtcontent.Text;
                        obj.Keywords = this.txtauthor.Text;
                        obj.search = RewriteURLNew.NameToTag(this.txtname.Text + this.txtdesc.Text + this.txtcontent.Text);
                        //obj.Images = vimg;
                        //obj.ImagesSmall = small;

                        obj.Images = txtImage.Text;
                        obj.ImagesSmall = txtImage.Text.Replace("uploads", "uploads/_thumbs");

                        obj.Equals = vkey;
                        obj.Chekdata = int.Parse(Chek);
                        obj.Create_Date = dcreatedate;
                        obj.Modified_Date = denddate;
                        obj.Views = 0;
                        obj.Tags = this.txttags.Text;
                        obj.lang = this.lang;
                        obj.New = int.Parse(this.chknews.Checked ? "1" : "0");
                        obj.CheckBox1 = int.Parse(this.CheckBox1.Checked ? "1" : "0");
                        obj.CheckBox2 = int.Parse(this.CheckBox2.Checked ? "1" : "0");
                        obj.CheckBox3 = int.Parse(this.CheckBox3.Checked ? "1" : "0");
                        obj.CheckBox4 = int.Parse(this.CheckBox4.Checked ? "1" : "0");
                        obj.CheckBox5 = int.Parse(this.CheckBox5.Checked ? "1" : "0");
                        obj.CheckBox6 = int.Parse(this.CheckBox6.Checked ? "1" : "0");
                        obj.Status = int.Parse(this.chkstatus.Checked ? "1" : "0");
                        obj.Titleseo = this.txttitleseo.Text;
                        obj.Meta = this.txtmeta.Text;
                        obj.Keyword = this.txtKeywordS.Text;
                        obj.TangName = TangName;
                        obj.Alt = this.txAlt.Text;
                        SNews.News_INSERT(obj);
                        #endregion

                    }
                    else
                    {
                        #region RewriteUrl
                        string TagName = "";
                        if (txtRewriteUrl.Text.Length > 0)
                        {
                            #region UpdateMenu
                            List<News> item = SNews.GETBYID(this.hdid.Value);
                            if (item.Count > 0)
                            {
                                ModulControl obk = db.ModulControls.SingleOrDefault(p => p.TangName == item[0].TangName);
                                obk.Name = txtname.Text;
                                obk.Module = 2;
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
                            List<News> item = SNews.GETBYID(this.hdid.Value);
                            if (item.Count > 0)
                            {
                                ModulControl obk = db.ModulControls.SingleOrDefault(p => p.TangName == item[0].TangName);
                                obk.Name = txtname.Text;
                                obk.Module = 2;
                                List<ModulControl> list = (from p in db.ModulControls where p.TangName == obk.TangName orderby p.ID descending select p).ToList();
                                if (list.Count > 2)
                                {
                                    var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txtname.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txtname.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txtname.Text);
                                }
                                else
                                {
                                    if (MoreAll.AddURL.SeoURL(item[0].TangName) != MoreAll.AddURL.SeoURL(txtname.Text)) { var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txtname.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txtname.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txtname.Text); } else { TagName = item[0].TangName; }
                                }
                                obk.TangName = TagName;
                                db.SubmitChanges();
                            }
                            #endregion
                        }
                        #endregion

                        #region MyRegion
                        obj.inid = int.Parse(this.hdid.Value);
                        obj.icid = int.Parse(this.ddlcategoriesdetail.SelectedValue);
                        obj.Title = this.txtname.Text;
                        obj.Brief = this.txtdesc.Text;
                        obj.Contents = this.txtcontent.Text;
                        obj.Keywords = this.txtauthor.Text;
                        obj.search = RewriteURLNew.NameToTag(this.txtname.Text + this.txtdesc.Text + this.txtcontent.Text);
                        //obj.Images = vimg;
                        //obj.ImagesSmall = small;
                        obj.Images = txtImage.Text;
                        obj.ImagesSmall = txtImage.Text.Replace("uploads", "uploads/_thumbs");
                        obj.Equals = vkey;
                        obj.Chekdata = int.Parse(Chek);
                        obj.Create_Date = dcreatedate;
                        obj.Modified_Date = denddate;
                        obj.Views = 0;
                        obj.Tags = this.txttags.Text;
                        obj.lang = this.lang;
                        obj.New = int.Parse(this.chknews.Checked ? "1" : "0");
                        obj.CheckBox1 = int.Parse(this.CheckBox1.Checked ? "1" : "0");
                        obj.CheckBox2 = int.Parse(this.CheckBox2.Checked ? "1" : "0");
                        obj.CheckBox3 = int.Parse(this.CheckBox3.Checked ? "1" : "0");
                        obj.CheckBox4 = int.Parse(this.CheckBox4.Checked ? "1" : "0");
                        obj.CheckBox5 = int.Parse(this.CheckBox5.Checked ? "1" : "0");
                        obj.CheckBox6 = int.Parse(this.CheckBox6.Checked ? "1" : "0");
                        obj.Status = int.Parse(this.chkstatus.Checked ? "1" : "0");
                        obj.Titleseo = this.txttitleseo.Text;
                        obj.Meta = this.txtmeta.Text;
                        obj.Keyword = this.txtKeywordS.Text;
                        obj.TangName = TagName;
                        obj.Alt = this.txAlt.Text;
                        #endregion
                        SNews.News_UPDATE(obj);
                    }
                    LoadItems();
                    MultiView1.ActiveViewIndex = 0;
                    DeleteFormValue();
                }
            }
            //  catch (Exception) { }
        }

        public bool ThumbnailCallback()
        {
            return false;
        }

        void DeleteFormValue()
        {
            this.txtdesc.Text = "";
            this.txtcontent.Text = "";
            this.hdcid.Value = "";
            this.txtname.Text = "";
            this.txtcontent.Text = "";
            this.txtauthor.Text = "";
            this.ltimg.Text = "";
            this.txtvimg.Text = "";
            this.hdFileName.Value = "";
            this.lbl_msg.Text = "";
            this.hdimgMax.Value = "";
            this.hdimgsmallEdit.Value = "";
            this.hdimgMaxEdit.Value = "";
            this.txttags.Text = "";
            this.txttitleseo.Text = "";
            this.txtmeta.Text = "";
            this.txtKeywordS.Text = "";
            txtImage.Text = "";
            hdimgnews.Value = "";
            txtRewriteUrl.Text = "";
            ltshowurl.Text = "";
            this.txAlt.Text = "";
        }

        protected void Delete_Load(object sender, System.EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Xóa tin được lựa chọn ?.')";
        }

        protected void rpitems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "EditDetail":
                    List<News> dtdetail = SNews.GETBYID(e.CommandArgument.ToString());
                    if (dtdetail.Count > 0)
                    {
                        txtname.Text = dtdetail[0].Title;
                        txtdesc.Text = dtdetail[0].Brief;
                        txtcontent.Text = dtdetail[0].Contents;
                        txtauthor.Text = dtdetail[0].Keywords;
                        hdimgMaxEdit.Value = dtdetail[0].Images;
                        txttags.Text = dtdetail[0].Tags;
                        txtRewriteUrl.Text = dtdetail[0].TangName;
                        string ssl = "http://" + Request.Url.Host + "/";
                        if (Commond.Setting("SSL").Equals("1"))
                        {
                            ssl = "https://" + Request.Url.Host + "/";
                        }
                        ltshowurl.Text = ssl + dtdetail[0].TangName + ".html";
                        txAlt.Text = dtdetail[0].Alt;

                        #region Seowwebsite
                        txttitleseo.Text = dtdetail[0].Titleseo;
                        txtmeta.Text = dtdetail[0].Meta;
                        txtKeywordS.Text = dtdetail[0].Keyword;
                        #endregion


                        txtImage.Text = dtdetail[0].Images;
                        ltimgs.Text = MoreImage.Image(dtdetail[0].ImagesSmall);
                        hdimgnews.Value = dtdetail[0].Images;


                        hdimgsmallEdit.Value = dtdetail[0].ImagesSmall;

                        ltimg.Text = MoreImage.Image(dtdetail[0].ImagesSmall);
                        WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlcategoriesdetail, dtdetail[0].icid.ToString());
                        chknews.Checked = (dtdetail[0].New == 1);
                        chkstatus.Checked = (dtdetail[0].Status == 1);
                        CheckBox1.Checked = (dtdetail[0].CheckBox1 == 1);
                        CheckBox2.Checked = (dtdetail[0].CheckBox2 == 1);
                        CheckBox3.Checked = (dtdetail[0].CheckBox3 == 1);
                        CheckBox4.Checked = (dtdetail[0].CheckBox4 == 1);
                        CheckBox5.Checked = (dtdetail[0].CheckBox5 == 1);
                        CheckBox6.Checked = (dtdetail[0].CheckBox6 == 1);
                        #region Update
                        this.txtfromday.Text = Convert.ToDateTime(dtdetail[0].Create_Date).ToString("MM/dd/yyyy HH:mm");
                        this.txtindays.Text = ((Convert.ToDateTime(dtdetail[0].Modified_Date).Ticks - Convert.ToDateTime(dtdetail[0].Create_Date).Ticks) / 0xc92a69c000L).ToString();
                        if (dtdetail[0].Chekdata.ToString().Equals("1"))
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

                        if (dtdetail[0].Equals.ToString().Trim().Equals("1"))
                        {
                            this.rdFromLinks.Checked = true;
                            this.rdFromComputer.Checked = false;
                            this.LoadView();
                            this.txtvimg.Text = dtdetail[0].Images;
                        }
                        else
                        {
                            this.rdFromComputer.Checked = true;
                            this.rdFromLinks.Checked = false;
                            this.LoadView();
                            this.hdFileName.Value = dtdetail[0].Images;
                        }
                        MultiView1.ActiveViewIndex = 1;
                        hdinsertupdate.Value = "update";
                        hdid.Value = dtdetail[0].inid.ToString();
                        // giu gia tri cua nhom thông tin cu: su dung khi thay doi nhom thông tin.
                        hdcid.Value = dtdetail[0].icid.ToString();
                        ddlcategoriesdetail.SelectedValue = hdcid.Value;
                    }
                    break;
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
                        SNews.UPDATESTATUS(str3, str2);
                        this.LoadItems();
                        base.Response.Redirect(base.Request.Url.ToString().Trim());
                        return;
                    }
                    return;
                case "ChangeNews":
                    string str1 = e.CommandName.Trim();
                    string str21 = e.CommandArgument.ToString().Trim();
                    string str41 = str1;
                    if (str41 != null)
                    {
                        string str3;
                        str21 = e.CommandArgument.ToString().Trim().Substring(0, e.CommandArgument.ToString().IndexOf("|"));
                        if (e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf("|") + 1, (e.CommandArgument.ToString().Length - e.CommandArgument.ToString().IndexOf("|")) - 1) == "1")
                        {
                            str3 = "0";
                        }
                        else
                        {
                            str3 = "1";
                        }
                        SNews.UPDATE_News(str3, str21);
                        this.LoadItems();
                        base.Response.Redirect(base.Request.Url.ToString().Trim());
                        return;
                    }
                    return;
                case "CheckBox1":
                    string str11 = e.CommandName.Trim();
                    string str211 = e.CommandArgument.ToString().Trim();
                    string str411 = str11;
                    if (str411 != null)
                    {
                        string str3;
                        str211 = e.CommandArgument.ToString().Trim().Substring(0, e.CommandArgument.ToString().IndexOf("|"));
                        if (e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf("|") + 1, (e.CommandArgument.ToString().Length - e.CommandArgument.ToString().IndexOf("|")) - 1) == "1")
                        {
                            str3 = "0";
                        }
                        else
                        {
                            str3 = "1";
                        }
                        SNews.Name_Text("update News set CheckBox1= " + str3 + " where inid=" + str211 + "");
                        this.LoadItems();
                        base.Response.Redirect(base.Request.Url.ToString().Trim());
                        return;
                    }
                    return;
                case "CheckBox2":
                    string str112 = e.CommandName.Trim();
                    string str2112 = e.CommandArgument.ToString().Trim();
                    string str4112 = str112;
                    if (str4112 != null)
                    {
                        string str3;
                        str2112 = e.CommandArgument.ToString().Trim().Substring(0, e.CommandArgument.ToString().IndexOf("|"));
                        if (e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf("|") + 1, (e.CommandArgument.ToString().Length - e.CommandArgument.ToString().IndexOf("|")) - 1) == "1")
                        {
                            str3 = "0";
                        }
                        else
                        {
                            str3 = "1";
                        }
                        SNews.Name_Text("update News set CheckBox2= " + str3 + " where inid=" + str2112 + "");
                        this.LoadItems();
                        base.Response.Redirect(base.Request.Url.ToString().Trim());
                        return;
                    }
                    return;
                case "CheckBox3":
                    string str1123 = e.CommandName.Trim();
                    string str21123 = e.CommandArgument.ToString().Trim();
                    string str41123 = str1123;
                    if (str41123 != null)
                    {
                        string str3;
                        str21123 = e.CommandArgument.ToString().Trim().Substring(0, e.CommandArgument.ToString().IndexOf("|"));
                        if (e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf("|") + 1, (e.CommandArgument.ToString().Length - e.CommandArgument.ToString().IndexOf("|")) - 1) == "1")
                        {
                            str3 = "0";
                        }
                        else
                        {
                            str3 = "1";
                        }
                        SNews.Name_Text("update News set CheckBox3= " + str3 + " where inid=" + str21123 + "");
                        this.LoadItems();
                        base.Response.Redirect(base.Request.Url.ToString().Trim());
                        return;
                    }
                    return;
                case "CheckBox4":
                    string str11234 = e.CommandName.Trim();
                    string str211234 = e.CommandArgument.ToString().Trim();
                    string str411234 = str11234;
                    if (str411234 != null)
                    {
                        string str3;
                        str211234 = e.CommandArgument.ToString().Trim().Substring(0, e.CommandArgument.ToString().IndexOf("|"));
                        if (e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf("|") + 1, (e.CommandArgument.ToString().Length - e.CommandArgument.ToString().IndexOf("|")) - 1) == "1")
                        {
                            str3 = "0";
                        }
                        else
                        {
                            str3 = "1";
                        }
                        SNews.Name_Text("update News set CheckBox4= " + str3 + " where inid=" + str211234 + "");
                        this.LoadItems();
                        base.Response.Redirect(base.Request.Url.ToString().Trim());
                        return;
                    }
                    return;
                case "CheckBox5":
                    string str112345 = e.CommandName.Trim();
                    string str2112345 = e.CommandArgument.ToString().Trim();
                    string str4112345 = str112345;
                    if (str4112345 != null)
                    {
                        string str3;
                        str2112345 = e.CommandArgument.ToString().Trim().Substring(0, e.CommandArgument.ToString().IndexOf("|"));
                        if (e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf("|") + 1, (e.CommandArgument.ToString().Length - e.CommandArgument.ToString().IndexOf("|")) - 1) == "1")
                        {
                            str3 = "0";
                        }
                        else
                        {
                            str3 = "1";
                        }
                        SNews.Name_Text("update News set CheckBox5= " + str3 + " where inid=" + str2112345 + "");
                        this.LoadItems();
                        base.Response.Redirect(base.Request.Url.ToString().Trim());
                        return;
                    }
                    return;
                case "CheckBox6":
                    string str1123456 = e.CommandName.Trim();
                    string str21123456 = e.CommandArgument.ToString().Trim();
                    string str41123456 = str1123456;
                    if (str41123456 != null)
                    {
                        string str3;
                        str21123456 = e.CommandArgument.ToString().Trim().Substring(0, e.CommandArgument.ToString().IndexOf("|"));
                        if (e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf("|") + 1, (e.CommandArgument.ToString().Length - e.CommandArgument.ToString().IndexOf("|")) - 1) == "1")
                        {
                            str3 = "0";
                        }
                        else
                        {
                            str3 = "1";
                        }
                        SNews.Name_Text("update News set CheckBox6= " + str3 + " where inid=" + str21123456 + "");
                        this.LoadItems();
                        base.Response.Redirect(base.Request.Url.ToString().Trim());
                        return;
                    }
                    return;
                case "UpDate":
                    SNews.UPDATE_DATETIME(e.CommandArgument.ToString(), Convert.ToDateTime(DateTime.Now.ToString()), Convert.ToDateTime(DateTime.Now.AddYears(20).ToString()));
                    this.LoadItems();
                    MultiView1.ActiveViewIndex = 0;
                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    return;
                case "Chekdata":
                    SNews.CHECKDATA(e.CommandArgument.ToString(), "0", Convert.ToDateTime(DateTime.Now.ToString()), Convert.ToDateTime(DateTime.Now.AddYears(20).ToString()));
                    this.LoadItems();
                    MultiView1.ActiveViewIndex = 0;
                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    return;
                case "Delete":
                    List<News> str5 = SNews.GETBYID(e.CommandArgument.ToString());
                    if (str5.Count > 0)
                    {
                        try
                        {
                            SModulControls.Name_Text("DELETE FROM ModulControls WHERE TangName ='" + str5[0].TangName + "'");
                        }
                        catch (Exception)
                        { }
                    }
                    SComments.DELETE_Parent_ID(e.CommandArgument.ToString());
                    SNews_Related.DELETE_RELATED(e.CommandArgument.ToString());
                    SNews.News_DELETE(e.CommandArgument.ToString());
                    LoadItems();
                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    break;
            }
        }

        protected void ddlcategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
            LoadRequest();
        }

        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
            LoadRequest();
        }

        protected void ddlClassBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
            LoadRequest();
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
                        int j;
                        List<News> dlt = new List<News>();
                        dlt = SNews.GETBYID(id.Value);
                        for (j = 0; j < dlt.Count; j++)
                        {
                            //try
                            //{
                            //    ServerInfoUtlitities utlitities = new ServerInfoUtlitities();
                            //    File.Delete(utlitities.APPL_PHYSICAL_PATH + dlt[j].Images.ToString());
                            //    File.Delete(utlitities.APPL_PHYSICAL_PATH + dlt[j].ImagesSmall.ToString());
                            //}
                            //catch (Exception) { }
                            try
                            {
                                SModulControls.Name_Text("DELETE FROM ModulControls WHERE TangName='" + dlt[j].TangName + "'");
                            }
                            catch (Exception)
                            { }

                        }
                        SComments.DELETE_Parent_ID(id.Value);
                        SNews_Related.DELETE_RELATED(id.Value);
                        SNews.News_DELETE(id.Value);
                    }
                }
                LoadItems();
                base.Response.Redirect(base.Request.Url.ToString().Trim());
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

        protected void btthemmoi_Click(object sender, EventArgs e)
        {
            hdinsertupdate.Value = "insert";
            MultiView1.ActiveViewIndex = 1;
            DeleteFormValue();
            this.txtfromday.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlcategoriesdetail, this.ddlcategories.SelectedValue);
        }

        protected void btDeleteimages_Click(object sender, EventArgs e)
        {
            List<News> str5 = SNews.GETBYID(hdid.Value);
            if (str5.Count > 0)
            {
                try
                {
                    ServerInfoUtlitities utlitities = new ServerInfoUtlitities();
                    File.Delete(utlitities.APPL_PHYSICAL_PATH + str5[0].Images.ToString());
                    File.Delete(utlitities.APPL_PHYSICAL_PATH + str5[0].ImagesSmall.ToString());
                }
                catch (Exception) { }
            }
            this.hdimgMaxEdit.Value = "";
            this.hdimgsmallEdit.Value = "";


            SNews.News_UPDATEIMG(hdid.Value);
            this.LoadItems();
            ltimg.Text = "";
            MultiView1.ActiveViewIndex = 1;
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
        }

        protected void bthienthi_Click(object sender, EventArgs e)
        {
            LoadItems();
            LoadRequest();
        }

        protected string More_Related(string inid, string icid)
        {
            return ("<a href='admin.aspx?u=news&su=News_Related&inid=" + inid + "&icid=" + icid + "'><img src='Uploads/pic/web/icon/add.png' border=0 /></a>");
        }

        protected string Comments(string inid)
        {
            return ("<a href='admin.aspx?u=news&su=Comments&nid=" + inid + "'><img Title=\"Bình luận\" src=\"Uploads/pic/web/icon/comment.png\" border=0 /></a>");
        }
        protected void LoadRequest()
        {
            Response.Redirect("admin.aspx?u=news&su=Tintuc&id=" + ddlcategories.SelectedValue + "&st=" + ddlstatus.SelectedValue + "&us=" + ddlorderby.SelectedValue + "&ds=" + ddlordertype.SelectedValue + "&kw=" + txtkeyword.Text + "");
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
        protected void txtTieude_TextChanged(object sender, EventArgs e)
        {
            TextBox Tieude = (TextBox)sender;
            var b = (HiddenField)Tieude.FindControl("hiID");
            #region UpdateMenu
            string TagName = "";
            if (Tieude.Text.Length > 0)
            {
                List<News> item = SNews.GETBYID(b.Value);
                if (item.Count > 0)
                {
                    ModulControl obk = db.ModulControls.SingleOrDefault(p => p.TangName == item[0].TangName);
                    obk.Name = Tieude.Text;
                    obk.Module = 2;
                    List<ModulControl> list = (from p in db.ModulControls where p.TangName == obk.TangName orderby p.ID descending select p).ToList();
                    if (list.Count > 2)
                    {
                        var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(Tieude.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(Tieude.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(Tieude.Text);
                    }
                    else
                    {
                        if (MoreAll.AddURL.SeoURL(item[0].Title) != MoreAll.AddURL.SeoURL(Tieude.Text)) { var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(Tieude.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(Tieude.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(Tieude.Text); } else { TagName = item[0].TangName; }
                    }
                    obk.TangName = TagName;
                    db.SubmitChanges();
                    SNews.Name_Text("UPDATE [News] SET Title=N'" + Tieude.Text + "',TangName='" + TagName + "'  WHERE inid=" + b.Value + "");
                    LoadItems();
                    this.ltmsg.Text = "<span class=alert>Cập nhật tiêu đề thành công !!</span>";
                }
            }
            else
            {
                LoadItems();
                ltmsg.Text = "<span class=alert>Bạn chưa nhập tiêu đề !!</span>";
            }
            #endregion

        }

        protected void btkiemtra_Click(object sender, EventArgs e)
        {
            string ssl = "http://" + Request.Url.Host + "/";
            if (Commond.Setting("SSL").Equals("1"))
            {
                ssl = "https://" + Request.Url.Host + "/";
            }
            if (hdinsertupdate.Value.Equals("insert"))
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
                    int tong = int.Parse(curItem[0].ID.ToString()); cong = tong + 1; var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txtname.Text)).FirstOrDefault(); TangName = hasTagName != null ? MoreAll.AddURL.SeoURL(txtname.Text) + "-" + cong : MoreAll.AddURL.SeoURL(txtname.Text);
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
                    List<News> item = SNews.GETBYID(this.hdid.Value);
                    if (item.Count > 0)
                    {
                        ModulControl obk = db.ModulControls.SingleOrDefault(p => p.TangName == item[0].TangName);
                        obk.Name = txtname.Text;
                        obk.Module = 2;
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
                    List<News> item = SNews.GETBYID(this.hdid.Value);
                    if (item.Count > 0)
                    {
                        ModulControl obk = db.ModulControls.SingleOrDefault(p => p.TangName == item[0].TangName);
                        obk.Name = txtname.Text;
                        obk.Module = 2;
                        List<ModulControl> list = (from p in db.ModulControls where p.TangName == obk.TangName orderby p.ID descending select p).ToList();
                        if (list.Count > 2)
                        {
                            var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txtname.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txtname.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txtname.Text);
                        }
                        else
                        {
                            if (MoreAll.AddURL.SeoURL(item[0].TangName) != MoreAll.AddURL.SeoURL(txtname.Text)) { var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txtname.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txtname.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txtname.Text); } else { TagName = item[0].TangName; }
                        }
                    }
                    #endregion
                }
                ltshowurl.Text = ssl + TagName + ".html";
                #endregion
            }
        }
    }
}
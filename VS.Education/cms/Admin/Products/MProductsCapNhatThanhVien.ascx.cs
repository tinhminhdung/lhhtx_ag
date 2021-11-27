using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;
using Framwork;
using System.Drawing.Imaging;
using System.IO;
using Entity;

namespace VS.E_Commerce.cms.Admin.Products
{
    public partial class MProductsCapNhatThanhVien : System.Web.UI.UserControl
    {
        private string id = "-1";
        private string status = "";
        private string StatusThanhVien = "";
        private string IDThanhVien = "";
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
            if (Request["id"] != null && !Request["id"].Equals(""))
            {
                id = Request["id"];
            }
            if (Request["IDThanhVien"] != null && !Request["IDThanhVien"].Equals(""))
            {
                IDThanhVien = Request["IDThanhVien"];
            }
            if (Request["st"] != null && !Request["st"].Equals(""))
            {
                ddlstatus.SelectedValue = Request["st"];
                WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlstatus, this.status);
            }
            if (Request["ThanhVien"] != null && !Request["ThanhVien"].Equals(""))
            {
                ddltrangthaithanhvien.SelectedValue = Request["ThanhVien"];
                WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddltrangthaithanhvien, this.StatusThanhVien);
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
            this.Page.Form.DefaultButton = lnksearch.UniqueID;
            if (!IsPostBack)
            {
                Session["ipid"] = null; Session["icid"] = null;
                #region UpdatePanel
                this.Page.Form.Enctype = "multipart/form-data";
                ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnsave);
                ScriptManager.GetCurrent(Page).RegisterPostBackControl(btkiemtra);
                #endregion
                Showthanhvien();
                ShowMau();
                Showkichthuoc();
                ShowHang();
                LoadCategories();
                LoadItems();
            }
        }

        protected void Showthanhvien()
        {
            int str = 0;
            List<Entity.users> dt = Susers.Name_Text("select * from users  where istatus=1 order by vuserun desc");
            for (int i = 0; i < dt.Count; i++)
            {
                ddlthanhvien.Items.Insert(str, new ListItem(dt[i].vuserun.ToString() + " / " + dt[i].vfname.ToString(), dt[i].iuser_id.ToString()));
            }
            this.ddlthanhvien.Items.Insert(0, new ListItem("Chọn nhà cung cấp", "-1"));
            this.ddlthanhvien.Items.Insert(1, new ListItem("Admin", "0"));
            this.ddlthanhvien.DataBind();
        }
        protected void ShowHang()
        {
            int str = 0;
            List<Entity.Menu> dt = SMenu.LOAD_CATESPARENT_ID(More.HA, this.lang, "-1", "1");
            for (int i = 0; i < dt.Count; i++)
            {
                ddlthuonghieu.Items.Insert(str, new ListItem(dt[i].Name.ToString(), dt[i].ID.ToString()));
            }
            this.ddlthuonghieu.DataBind();
        }
        #region Menu
        protected void LoadCategories()
        {
            if (Request["id"] != null && !Request["id"].Equals(""))
            {
                ddlcategories.SelectedValue = Request["id"];
            }
            int str = 0;
            List<Entity.Menu> dt = SMenu.LOAD_CATESPARENT_ID(More.PR, this.lang, "-1", "1");
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
        protected int Categories(string id, int str, string j)
        {
            List<Entity.Menu> dt = SMenu.LOAD_CATESPARENT_ID(More.PR, this.lang, id, "1");
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
                string Vitri = "";
                if (ddlvitri.SelectedValue == "1")
                {
                    Vitri = "  and Quantity=0";
                }
                else if (ddlvitri.SelectedValue == "2")
                {
                    Vitri = "  and (Quantity between (0) and (10))";
                }
                else if (ddlvitri.SelectedValue == "3")
                {
                    Vitri = "  and (Quantity between (10) and (50))";
                }
                Fproducts DB = new Fproducts();
                string[] searchfields = new string[] { "Name", "Brief", "Contents", "search" };
                string orderby = this.ddlorderby.SelectedValue + " " + this.ddlordertype.SelectedValue;
                List<Entity.Products> dt = DB.CategoryAdmin(orderby, this.txtkeyword.Text.Trim().Replace("&nbsp;", ""), searchfields, More.Sub_Menu(More.PR, ddlcategories.SelectedValue), lang, ddlstatus.SelectedValue, ddltrangthaithanhvien.SelectedValue, Vitri, IDThanhVien);
                CollectionPager1.DataSource = dt;
                CollectionPager1.BindToControl = rpitems;
                CollectionPager1.MaxPages = 10000;
                CollectionPager1.PageSize = 100;
                rpitems.DataSource = CollectionPager1.DataSourcePaged;
                rpitems.DataBind();
                RemoveCache.Products();
            }
            catch (Exception) { }
        }

        protected void lnkcreatenew_Click(object sender, EventArgs e)
        {
            hdinsertupdate.Value = "insert";
            MultiView1.ActiveViewIndex = 1;
            DeleteFormValue();
            this.txtfromday.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlcategoriesdetail, this.ddlcategories.SelectedValue);
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            DeleteFormValue();
            LoadItems();
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            //try
            {
                WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlcategories, this.ddlcategoriesdetail.SelectedValue);
                if (this.txtname.Text.Trim().Length < 1)
                {
                    this.lbl_msg.Text = "Xin vui lòng kiểm tra dữ liệu đầu vào của bạn";
                }
                //else if (this.txtdiemmuahang.Text.Trim().Length < 1)
                //{
                //    this.lbl_msg.Text = "Xin vui lòng điền điểm mua hàng";
                //}
                else
                {
                    #region status
                    int status = 0;
                    if (this.chkstatus.Checked)
                    {
                        status = 1;
                    }
                    int news = 0;
                    if (this.Checknews.Checked)
                    {
                        news = 1;
                    }
                    int Home = 0;
                    if (this.CheckHome.Checked)
                    {
                        Home = 1;
                    }
                    #endregion
                    #region Check
                    int Check1 = 0;
                    if (this.Check_01.Checked)
                    {
                        Check1 = 1;
                    }
                    int Check2 = 0;
                    if (this.Check_02.Checked)
                    {
                        Check2 = 1;
                    }
                    int Check3 = 0;
                    if (this.Check_03.Checked)
                    {
                        Check3 = 1;
                    }
                    int Check4 = 0;
                    if (this.Check_04.Checked)
                    {
                        Check4 = 1;
                    }
                    int Check5 = 0;
                    if (this.Check_05.Checked)
                    {
                        Check5 = 1;
                    }
                    #endregion
                    #region Chekdata
                    int Chek = 0;
                    string cdate = DateTime.Now.ToString();
                    string edate = DateTime.Now.AddYears(10).ToString();
                    DateTime dcreatedate = Convert.ToDateTime(cdate.ToString());
                    DateTime denddate = Convert.ToDateTime(edate.ToString());

                    if (this.chkdaytype.Checked)
                    {
                        Chek = 1;
                        dcreatedate = Convert.ToDateTime(this.txtfromday.Text);
                        denddate = dcreatedate.AddDays((double)Convert.ToInt32(txtindays.Text));
                    }
                    #endregion
                    #region Img
                    string vimg = this.txtvimg.Text;
                    hdimgsmall.Value = vimg;
                    hdimgMax.Value = vimg;
                    ServerInfoUtlitities utlitities = new ServerInfoUtlitities();
                    int vkey = 1;
                    string path = "";
                    string small = "";
                    if (this.rdFromComputer.Checked)
                    {
                        if ((this.flimage.FileName.Trim().Length > 0) && (this.flimage.PostedFile.ContentLength > 0))
                        {
                            String pathcategorise = Database.ConverCodeUni(ddlcategoriesdetail.SelectedItem.Text.Trim());
                            String paths = "/Uploads/prods/" + pathcategorise + "/";
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
                            Database.ResizeIamgesFix(Server.MapPath(vimg), Server.MapPath(small), Convert.ToInt32(AllQuery.MorePro.Height()), Convert.ToInt32(AllQuery.MorePro.Width()));
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
                    if (hdinsertupdate.Value.Equals("insert"))
                    {
                        #region RewriteUrl
                        int cong = 0;
                        string TangName = "";
                        ModulControl obm = new ModulControl();
                        obm.Name = txtname.Text;
                        obm.Module = 21;
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

                        Entity.Products obj = new Entity.Products();
                        #region MyRegion
                        obj.icid = int.Parse(ddlcategoriesdetail.SelectedValue);
                        obj.ID_Hang = int.Parse(ddlthuonghieu.SelectedValue);
                        obj.sanxuat = int.Parse("0");
                        obj.Code = txtcode.Text;
                        obj.Name = txtname.Text;
                        obj.Brief = txtdesc.Text;
                        obj.Contents = txtcontent.Text;
                        obj.search = RewriteURLNew.NameToTag(txtname.Text + txtdesc.Text + txtcontent.Text);
                        //obj.Images = vimg;
                        //obj.ImagesSmall = small;
                        obj.Images = txtImage.Text;
                        obj.ImagesSmall = txtImage.Text.Replace("uploads", "uploads/_thumbs");

                        obj.Equals = vkey;
                        obj.Quantity = 1;// int.Parse(txtquantity.Text.Trim());
                        obj.Price = txtprice.Text;
                        obj.OldPrice = txtoldprice.Text;
                        obj.Views = 0;
                        obj.Chekdata = Chek;
                        obj.Create_Date = dcreatedate;
                        obj.Modified_Date = denddate;
                        obj.lang = this.lang;
                        obj.News = news;
                        obj.Home = Home;
                        obj.Check_01 = Check1;
                        obj.Check_02 = Check2;
                        obj.Check_03 = Check3;
                        obj.Check_04 = Check4;
                        obj.Check_05 = Check5;
                        obj.Status = int.Parse(chkstatus.Checked ? "1" : "0");
                        obj.Titleseo = txttitleseo.Text;
                        obj.Meta = txtmeta.Text;
                        obj.Keyword = txtKeywordS.Text;
                        obj.Anh = txtMImage.Text.TrimEnd(',');
                        obj.Noidung1 = "";
                        obj.Noidung2 = "";
                        obj.Noidung3 = "";
                        obj.Noidung4 = "";
                        obj.Noidung5 = "";
                        obj.TangName = TangName;
                        obj.RateCount = 0;
                        obj.RateSum = 0;
                        obj.Alt = txtAlt.Text;
                        obj.IDThanhVien = 0;
                        obj.DiemMuaHang = int.Parse("0");
                        obj.GiaThanhVien = txtGiaThanhVien.Text;
                        obj.Giacongtynhapvao = txtgiacongtynhapvao.Text;
                        #endregion
                        SProducts.Insert(obj);
                        #region Mau_Kichthuoc
                        try
                        {
                            product tbn = db.products.Where(s => s.lang == lang).OrderByDescending(s => s.ipid).FirstOrDefault();
                            string proid = tbn.ipid.ToString();
                            try
                            {
                                Trunggian del = db.Trunggians.Where(s => s.Proid == int.Parse(proid) && s.Trangthai == 1).FirstOrDefault();
                                db.Trunggians.DeleteOnSubmit(del);
                                db.SubmitChanges();
                            }
                            catch (Exception)
                            { }
                            InsertCenter(proid, ddlcategoriesdetail.SelectedValue, cblcat);
                        }
                        catch
                        {
                            lbl_msg.Text = "Lỗi";
                        }
                        //kich thuoc
                        try
                        {
                            product tbn = db.products.Where(s => s.lang == lang).OrderByDescending(s => s.ipid).FirstOrDefault();
                            string proid = tbn.ipid.ToString();
                            try
                            {
                                Trunggian del = db.Trunggians.Where(s => s.Proid == int.Parse(proid) && s.Trangthai == 2).FirstOrDefault();
                                db.Trunggians.DeleteOnSubmit(del);
                                db.SubmitChanges();
                            }
                            catch (Exception)
                            { }
                            InsertCenterkt(proid, ddlcategoriesdetail.SelectedValue, ckichthuoc);
                        }
                        catch
                        {
                            lbl_msg.Text = "Lỗi";
                        }
                        #endregion
                    }
                    else
                    {

                        #region RewriteUrl
                        string TagName = "";
                        if (txtRewriteUrl.Text.Length > 0)
                        {
                            #region UpdateMenu
                            List<Entity.Products> item = SProducts.GetById(this.hdid.Value);
                            if (item.Count > 0)
                            {
                                ModulControl obk = db.ModulControls.SingleOrDefault(p => p.TangName == item[0].TangName);
                                obk.Name = txtname.Text;
                                obk.Module = 21;
                                List<ModulControl> list = (from p in db.ModulControls where p.TangName == obk.TangName orderby p.ID descending select p).ToList();
                                if (list.Count > 2)
                                {
                                    var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txtRewriteUrl.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txtRewriteUrl.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txtRewriteUrl.Text);
                                }
                                else
                                {
                                    if (MoreAll.AddURL.SeoURL(item[0].TangName) != MoreAll.AddURL.SeoURL(txtRewriteUrl.Text))
                                    {
                                        var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txtRewriteUrl.Text)).FirstOrDefault();
                                        TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txtRewriteUrl.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txtRewriteUrl.Text);
                                    }
                                    else
                                    {
                                        TagName = item[0].TangName;
                                    }
                                }
                                obk.TangName = TagName;
                                db.SubmitChanges();
                            }
                            #endregion
                        }
                        else
                        {
                            #region UpdateMenu
                            List<Entity.Products> item = SProducts.GetById(this.hdid.Value);
                            if (item.Count > 0)
                            {
                                ModulControl obk = db.ModulControls.SingleOrDefault(p => p.TangName == item[0].TangName);
                                obk.Name = txtname.Text;
                                obk.Module = 21;
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


                        Entity.Products obj = new Entity.Products();
                        #region MyRegion
                        obj.ipid = int.Parse(hdid.Value);
                        obj.icid = int.Parse(ddlcategoriesdetail.SelectedValue);
                        obj.ID_Hang = int.Parse(ddlthuonghieu.SelectedValue);
                        obj.sanxuat = int.Parse("0");
                        obj.Code = txtcode.Text;
                        obj.Name = txtname.Text;
                        obj.Brief = txtdesc.Text;
                        obj.Contents = txtcontent.Text;
                        obj.search = RewriteURLNew.NameToTag(txtname.Text + txtdesc.Text + txtcontent.Text);
                        //obj.Images = vimg;
                        //obj.ImagesSmall = small;
                        obj.Images = txtImage.Text;
                        obj.ImagesSmall = txtImage.Text.Replace("uploads", "uploads/_thumbs");

                        obj.Equals = vkey;
                        obj.Quantity = 1;// int.Parse(txtquantity.Text.Trim());
                        obj.Price = txtprice.Text;
                        obj.OldPrice = txtoldprice.Text;
                        obj.Views = 0;
                        obj.Chekdata = Chek;
                        obj.Create_Date = dcreatedate;
                        obj.Modified_Date = denddate;
                        obj.lang = this.lang;
                        obj.News = news;
                        obj.Home = Home;
                        obj.Check_01 = Check1;
                        obj.Check_02 = Check2;
                        obj.Check_03 = Check3;
                        obj.Check_04 = Check4;
                        obj.Check_05 = Check5;
                        obj.Status = int.Parse(chkstatus.Checked ? "1" : "0");
                        obj.Titleseo = txttitleseo.Text;
                        obj.Meta = txtmeta.Text;
                        obj.Keyword = txtKeywordS.Text;
                        obj.Anh = txtMImage.Text.TrimEnd(',');
                        obj.Noidung1 = "";
                        obj.Noidung2 = "";
                        obj.Noidung3 = "";
                        obj.Noidung4 = "";
                        obj.Noidung5 = "";
                        obj.TangName = TagName;
                        obj.Alt = txtAlt.Text;
                        obj.IDThanhVien = int.Parse(hdthanhvien.Value);
                        obj.DiemMuaHang = int.Parse("0");
                        obj.GiaThanhVien = txtGiaThanhVien.Text;
                        obj.Giacongtynhapvao = txtgiacongtynhapvao.Text;
                        #endregion
                        SProducts.Update(obj);

                        #region Mau_Kichthuoc
                        try
                        {
                            string proid = hdid.Value;
                            try
                            {
                                var queryNews = from News in db.Trunggians where News.Proid == int.Parse(proid) && News.Trangthai == 1 select News;
                                foreach (var News in queryNews)
                                {
                                    db.Trunggians.DeleteOnSubmit(News);
                                }
                                db.SubmitChanges();
                            }
                            catch (Exception)
                            { }
                            InsertCenter(hdid.Value, ddlcategoriesdetail.SelectedValue, cblcat);
                        }
                        catch { }
                        //kich thuoc 
                        try
                        {
                            string proid = hdid.Value;
                            try
                            {
                                var queryNews = from News in db.Trunggians where News.Proid == int.Parse(proid) && News.Trangthai == 2 select News;
                                foreach (var News in queryNews)
                                {
                                    db.Trunggians.DeleteOnSubmit(News);
                                }
                                db.SubmitChanges();
                            }
                            catch (Exception)
                            { }
                            InsertCenterkt(hdid.Value, ddlcategoriesdetail.SelectedValue, ckichthuoc);
                        }
                        catch { }
                        #endregion
                    }
                    //Cách Dùng tab để thêm 
                    //
                    //List<Entity.Products> dtdetail = SProducts.Name_Text("select top 1 * from Products order by ipid desc");
                    //if (dtdetail.Count > 0)
                    //{
                    //    Double str = int.Parse(dtdetail[0].ipid.ToString());
                    //    Double Tong = str + 1;
                    //    Literal1.Text = Tong.ToString();
                    //}
                    //update products set Orders=SCOPE_IDENTITY() WHERE ipid=SCOPE_IDENTITY()
                    LoadItems();
                    MultiView1.ActiveViewIndex = 0;
                    DeleteFormValue();
                }
            }
            // catch (Exception) { }
        }

        public bool ThumbnailCallback()
        {
            return false;
        }

        void DeleteFormValue()
        {
            txtcode.Text = "";
            txtdesc.Text = "";
            txtcontent.Text = "";
            txtprice.Text = "";
            txtquantity.Text = "";
            hdcid.Value = "";
            txtname.Text = "";
            txtoldprice.Text = "";
            this.txtvimg.Text = "";
            this.hdFileName.Value = "";
            ltimg.Text = "";
            hdimgMax.Value = "";
            hdimgsmallEdit.Value = "";
            hdimgMaxEdit.Value = "";
            txttitleseo.Text = "";
            txtmeta.Text = "";
            txtKeywordS.Text = "";
            txtMImage.Text = "";
            txtRewriteUrl.Text = "";
            ltimgs.Text = "";
            txtImage.Text = "";
            hdimgnews.Value = "";
            ltshowurl.Text = "";
            txtAlt.Text = "";
            txtgiacongtynhapvao.Text = "";
            txtGiaThanhVien.Text = "";
            txtdiemmuahang.Text = "";
            hdthanhvien.Value = "0";
        }

        protected void btndisplay_Click(object sender, EventArgs e)
        {
            LoadItems();
            LoadRequest();
        }

        protected void Delete_Load(object sender, System.EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Xóa Thông tin sản phẩm?')";
        }

        protected void rpitems_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "update":
                    List<Entity.Products> dtdetail = SProducts.GetById(e.CommandArgument.ToString());
                    if (dtdetail.Count > 0)
                    {
                        txtcode.Text = dtdetail[0].Code.ToString();
                        txtname.Text = dtdetail[0].Name.ToString();
                        txtdesc.Text = dtdetail[0].Brief.ToString();
                        txtcontent.Text = dtdetail[0].Contents.ToString();
                        hdimgMaxEdit.Value = dtdetail[0].Images.ToString();
                        hdimgsmallEdit.Value = dtdetail[0].ImagesSmall.ToString();
                        ltimg.Text = MoreImage.Image(dtdetail[0].ImagesSmall.ToString());
                        this.txtquantity.Text = dtdetail[0].Quantity.ToString();
                        this.txtprice.Text = dtdetail[0].Price.ToString();
                        txtoldprice.Text = dtdetail[0].OldPrice.ToString();
                        txtRewriteUrl.Text = dtdetail[0].TangName.ToString();
                        txtAlt.Text = dtdetail[0].Alt;

                        txtGiaThanhVien.Text = dtdetail[0].GiaThanhVien;
                        txtdiemmuahang.Text = dtdetail[0].DiemMuaHang.ToString();
                        txtgiacongtynhapvao.Text = dtdetail[0].Giacongtynhapvao.ToString();

                        hdthanhvien.Value = dtdetail[0].IDThanhVien.ToString();
                        string ssl = "http://" + Request.Url.Host + "/";
                        if (Commond.Setting("SSL").Equals("1"))
                        {
                            ssl = "https://" + Request.Url.Host + "/";
                        }
                        ltshowurl.Text = ssl + dtdetail[0].TangName + ".html";

                        #region Seowwebsite
                        txttitleseo.Text = dtdetail[0].Titleseo.ToString().Trim();
                        txtmeta.Text = dtdetail[0].Meta.ToString().Trim();
                        txtKeywordS.Text = dtdetail[0].Keyword.ToString().Trim();
                        #endregion

                        txtImage.Text = dtdetail[0].Images;
                        ltimgs.Text = MoreImage.Image(dtdetail[0].ImagesSmall);
                        hdimgnews.Value = dtdetail[0].Images;
                        if (dtdetail[0].Anh.Length > 0)
                        {
                            txtMImage.Text = dtdetail[0].Anh;
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "LoadImage", @"<script type='text/javascript'>LoadStringImg('" + dtdetail[0].Anh + "','" + txtMImage.ClientID + "');</script>", false);
                        }
                        else
                        {
                            txtMImage.Text = "";
                        }

                        LoadListGroupNewskt(dtdetail[0].ipid.ToString());
                        LoadListGroupNews(dtdetail[0].ipid.ToString());
                        WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlthuonghieu, dtdetail[0].ID_Hang.ToString());
                        WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlcategoriesdetail, dtdetail[0].icid.ToString());
                        if (dtdetail[0].Status.ToString().Trim().Equals("0"))
                        {
                            this.chkstatus.Checked = false;
                        }
                        else if (dtdetail[0].Status.ToString().Equals("1"))
                        {
                            this.chkstatus.Checked = true;
                        }
                        if (dtdetail[0].News.ToString().Trim().Equals("0"))
                        {
                            this.Checknews.Checked = false;
                        }
                        else if (dtdetail[0].News.ToString().Equals("1"))
                        {
                            this.Checknews.Checked = true;
                        }

                        if (dtdetail[0].Home.ToString().Trim().Equals("0"))
                        {
                            this.CheckHome.Checked = false;
                        }
                        else if (dtdetail[0].Home.ToString().Equals("1"))
                        {
                            this.CheckHome.Checked = true;
                        }
                        #region Check

                        if (dtdetail[0].Check_01.ToString().Trim().Equals("0"))
                        {
                            this.Check_01.Checked = false;
                        }
                        else if (dtdetail[0].Check_01.ToString().Equals("1"))
                        {
                            this.Check_01.Checked = true;
                        }
                        if (dtdetail[0].Check_02.ToString().Trim().Equals("0"))
                        {
                            this.Check_02.Checked = false;
                        }
                        else if (dtdetail[0].Check_02.ToString().Equals("1"))
                        {
                            this.Check_02.Checked = true;
                        }
                        if (dtdetail[0].Check_03.ToString().Trim().Equals("0"))
                        {
                            this.Check_03.Checked = false;
                        }
                        else if (dtdetail[0].Check_03.ToString().Equals("1"))
                        {
                            this.Check_03.Checked = true;
                        }
                        if (dtdetail[0].Check_04.ToString().Trim().Equals("0"))
                        {
                            this.Check_04.Checked = false;
                        }
                        else if (dtdetail[0].Check_04.ToString().Equals("1"))
                        {
                            this.Check_04.Checked = true;
                        }
                        if (dtdetail[0].Check_05.ToString().Trim().Equals("0"))
                        {
                            this.Check_05.Checked = false;
                        }
                        else if (dtdetail[0].Check_05.ToString().Equals("1"))
                        {
                            this.Check_05.Checked = true;
                        }
                        #endregion

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
                            this.txtvimg.Text = dtdetail[0].Images.ToString();
                        }
                        else
                        {
                            this.rdFromComputer.Checked = true;
                            this.rdFromLinks.Checked = false;
                            this.LoadView();
                            this.hdFileName.Value = dtdetail[0].Images.ToString();
                        }
                        MultiView1.ActiveViewIndex = 1;
                        hdinsertupdate.Value = "update";
                        hdid.Value = dtdetail[0].ipid.ToString();
                    }
                    break;
                #region StatusCheck
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
                        SProducts.Update_Status(str2, str3);
                        this.LoadItems();
                        return;
                    }
                    break;
                case "ChangeHome":
                    string strh = e.CommandName.Trim();
                    string str2h = e.CommandArgument.ToString().Trim();
                    string str4h = strh;
                    if (str4h != null)
                    {
                        string str3;
                        str2h = e.CommandArgument.ToString().Trim().Substring(0, e.CommandArgument.ToString().IndexOf("|"));
                        if (e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf("|") + 1, (e.CommandArgument.ToString().Length - e.CommandArgument.ToString().IndexOf("|")) - 1) == "1")
                        {
                            str3 = "0";
                        }
                        else
                        {
                            str3 = "1";
                        }
                        SProducts.Update_Home(str2h, str3);
                        this.LoadItems();
                        return;
                    }
                    break;
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
                        SProducts.Update_News(str21, str3);
                        this.LoadItems();
                        return;
                    }
                    break;

                case "ChangeCheck_01":
                    string str01 = e.CommandName.Trim();
                    string str201 = e.CommandArgument.ToString().Trim();
                    string str401 = str01;
                    if (str401 != null)
                    {
                        string str3;
                        str201 = e.CommandArgument.ToString().Trim().Substring(0, e.CommandArgument.ToString().IndexOf("|"));
                        if (e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf("|") + 1, (e.CommandArgument.ToString().Length - e.CommandArgument.ToString().IndexOf("|")) - 1) == "1")
                        {
                            str3 = "0";
                        }
                        else
                        {
                            str3 = "1";
                        }
                        SProducts.Update_Check_01(str201, str3);
                        this.LoadItems();
                        return;
                    }
                    break;

                case "ChangeCheck_02":
                    string str02 = e.CommandName.Trim();
                    string str202 = e.CommandArgument.ToString().Trim();
                    string str402 = str02;
                    if (str402 != null)
                    {
                        string str3;
                        str202 = e.CommandArgument.ToString().Trim().Substring(0, e.CommandArgument.ToString().IndexOf("|"));
                        if (e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf("|") + 1, (e.CommandArgument.ToString().Length - e.CommandArgument.ToString().IndexOf("|")) - 1) == "1")
                        {
                            str3 = "0";
                        }
                        else
                        {
                            str3 = "1";
                        }
                        SProducts.Update_Check_02(str202, str3);
                        this.LoadItems();
                        return;
                    }
                    break;
                case "ChangeCheck_03":
                    string str03 = e.CommandName.Trim();
                    string str203 = e.CommandArgument.ToString().Trim();
                    string str403 = str03;
                    if (str403 != null)
                    {
                        string str3;
                        str203 = e.CommandArgument.ToString().Trim().Substring(0, e.CommandArgument.ToString().IndexOf("|"));
                        if (e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf("|") + 1, (e.CommandArgument.ToString().Length - e.CommandArgument.ToString().IndexOf("|")) - 1) == "1")
                        {
                            str3 = "0";
                        }
                        else
                        {
                            str3 = "1";
                        }
                        SProducts.Update_Check_03(str203, str3);
                        this.LoadItems();
                        return;
                    }
                    break;
                case "ChangeCheck_04":
                    string str04 = e.CommandName.Trim();
                    string str204 = e.CommandArgument.ToString().Trim();
                    string str404 = str04;
                    if (str404 != null)
                    {
                        string str3;
                        str204 = e.CommandArgument.ToString().Trim().Substring(0, e.CommandArgument.ToString().IndexOf("|"));
                        if (e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf("|") + 1, (e.CommandArgument.ToString().Length - e.CommandArgument.ToString().IndexOf("|")) - 1) == "1")
                        {
                            str3 = "0";
                        }
                        else
                        {
                            str3 = "1";
                        }
                        SProducts.Update_Check_04(str204, str3);
                        this.LoadItems();
                        return;
                    }
                    break;
                case "ChangeCheck_05":
                    string str05 = e.CommandName.Trim();
                    string str205 = e.CommandArgument.ToString().Trim();
                    string str405 = str05;
                    if (str405 != null)
                    {
                        string str3;
                        str205 = e.CommandArgument.ToString().Trim().Substring(0, e.CommandArgument.ToString().IndexOf("|"));
                        if (e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf("|") + 1, (e.CommandArgument.ToString().Length - e.CommandArgument.ToString().IndexOf("|")) - 1) == "1")
                        {
                            str3 = "0";
                        }
                        else
                        {
                            str3 = "1";
                        }
                        SProducts.Update_Check_05(str205, str3);
                        this.LoadItems();
                        return;
                    }
                    break;
                #endregion
                case "updat_date":
                    List<Entity.Products> str5 = SProducts.GetById(e.CommandArgument.ToString());
                    if (str5.Count > 0)
                    {
                        SProducts.Update_datetime(e.CommandArgument.ToString(), Convert.ToDateTime(DateTime.Now.ToString()), Convert.ToDateTime(DateTime.Now.AddYears(20).ToString()));
                        this.LoadItems();
                        MultiView1.ActiveViewIndex = 0;
                    }
                    return;
                case "Chekdata":
                    SProducts.Chekdata(e.CommandArgument.ToString(), "0", Convert.ToDateTime(DateTime.Now.ToString()), Convert.ToDateTime(DateTime.Now.AddYears(20).ToString()));
                    this.LoadItems();
                    MultiView1.ActiveViewIndex = 0;
                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    return;
                case "delete":
                    try
                    {
                        List<Entity.Products> table = SProducts.GetById(e.CommandArgument.ToString());
                        if (table.Count > 0)
                        {
                            try
                            {
                                SModulControls.Name_Text("DELETE FROM ModulControls WHERE TangName='" + table[0].TangName + "'");
                            }
                            catch (Exception)
                            { }
                        }
                        try
                        {
                            List<Trunggian> del = db.Trunggians.Where(s => s.Proid == int.Parse(e.CommandArgument.ToString())).ToList();
                            db.Trunggians.DeleteAllOnSubmit(del);
                            db.SubmitChanges();
                        }
                        catch (Exception)
                        { }
                        SProduct_images.Delete_Ipid(e.CommandArgument.ToString());
                        SProducts.Delete(e.CommandArgument.ToString());
                        LoadItems();
                    }
                    catch (Exception)
                    {
                        lterr.Text = "Sản phẩm đang tồn tại trong giỏ hàng của bạn .Yêu cầu xem lại trước khi xóa";
                    }
                    break;
            }
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
        }

        protected void btn_createnew_Click(object sender, EventArgs e)
        {
            hdinsertupdate.Value = "insert";
            MultiView1.ActiveViewIndex = 1;
            DeleteFormValue();
        }

        protected void btthemmoi_Click(object sender, EventArgs e)
        {
            hdinsertupdate.Value = "insert";
            MultiView1.ActiveViewIndex = 1;
            DeleteFormValue();
            this.txtfromday.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlcategoriesdetail, this.ddlcategories.SelectedValue);
        }

        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
            LoadRequest();
        }

        protected void ddltrangthaithanhvien_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
            LoadRequest();
        }

        protected void ddlcategories_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlvitri_SelectedIndexChanged(object sender, EventArgs e)
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
                        List<Entity.Products> dlt = SProducts.GetById(id.Value);
                        for (j = 0; j < dlt.Count; j++)
                        {
                            try
                            {
                                SModulControls.Name_Text("DELETE FROM ModulControls WHERE TangName='" + dlt[j].TangName + "'");
                            }
                            catch (Exception)
                            { }
                        }
                        try
                        {
                            List<Trunggian> del = db.Trunggians.Where(s => s.Proid == int.Parse(id.Value)).ToList();
                            db.Trunggians.DeleteAllOnSubmit(del);
                            db.SubmitChanges();
                        }
                        catch (Exception)
                        { }
                        SProduct_images.Delete_Ipid(id.Value);
                        SProducts.Delete(id.Value);
                    }
                }
                LoadItems();
            }
            catch (Exception)
            {
                lterr.Text = "Sản phẩm đang tồn tại trong giỏ hàng của bạn .Yêu cầu xem lại trước khi xóa";
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

        protected void lnksearch_Click(object sender, EventArgs e)
        {
            LoadItems();
            LoadRequest();
        }

        protected void btDeleteimages_Click(object sender, EventArgs e)
        {
            List<Entity.Products> str5 = SProducts.GetById(hdid.Value);
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

            SProducts.Update_Images(hdid.Value, "", "");
            this.LoadItems();
            ltimg.Text = "";
            MultiView1.ActiveViewIndex = 1;
        }

        protected void bthienthi_Click(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }

        protected void LoadRequest()
        {
            Response.Redirect("admin.aspx?u=pro&su=thanhvien&id=" + ddlcategories.SelectedValue + "&st=" + ddlstatus.SelectedValue + "&ThanhVien=" + ddltrangthaithanhvien.SelectedValue + "&us=" + ddlorderby.SelectedValue + "&ds=" + ddlordertype.SelectedValue + "&kw=" + txtkeyword.Text + "&IDThanhVien=" + IDThanhVien + "");
        }

        protected string MoreImages(string ipid, string icid)
        {
            return ("<a  style=\"color: red; font-weight: bold;\"  title=\"Thêm giá\" href='admin.aspx?u=pro&su=Gia&ipid=" + ipid + "&icid=" + icid + "'><i style=\"font-size: 18px; color: red;\" class=\"icon-plus\"></i>  Thêm giá bán đại lý</a>");
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
        protected void txtxQuantity_TextChanged(object sender, EventArgs e)
        {
            TextBox Quantity = (TextBox)sender;
            var b = (HiddenField)Quantity.FindControl("hiID");
            if (MoreAll.RegularExpressions.phone(b.Value) == true)
            {
                ltthongbao.Text = "Số lượng phải là số";
            }
            else
            {
                List<Entity.Products> strid = SProducts.GetById(b.Value);
                if (strid.Count > 0)
                {
                    SProducts.Name_Text("update products set Quantity='" + Quantity.Text + "' where ipid=" + b.Value + " ");
                }
                ltthongbao.Text = "Cập nhật số lượng thành công";
                LoadItems();
            }
        }

        public string Hethang(string Soluong)
        {
            if (Soluong == "0")
            {
                return "<span class=\"label-sale\">Hết hàng</span>";
            }
            return "";
        }

        #region KichThuoc_masac
        public string MoreMau(string ID)//show ra danh sách mầu sắc
        {
            //Trangthai == 1 mầu
            string str = "";
            List<Menu> listpro = new List<Menu>();
            var list = db.Trunggians.Where(s => s.Proid == int.Parse(ID) && s.Trangthai == 1).ToList();// tìm trong bảng trung gian có bao nhiêu ID 218
            for (int i = 0; i < list.Count; i++)
            {
                var table = db.Menus.Where(s => s.ID == int.Parse(list[i].Icolor.ToString()) && s.Status == 1 && s.capp == "CO").ToList();//so sánh bảng menu và bảng trung gian để lấy ra tên của mầu
                for (int j = 0; j < table.Count; j++)
                {
                    listpro.Add(table[j]);
                }
            }
            foreach (var item in listpro)
            {
                str += "<a href=\"javascript:void(0)\" class=\"Color\"><img  src='" + item.Images + "'/><div class=\"pl\"><img src=\"/Resources/images/activee.png\" style=' height: 16px !important;width: 18px !important;' /></div></a>";
            }
            return str;
        }
        public string MoreSize(string ID)//show ra danh sách kích cỡ
        {
            //Trangthai == 2 Size
            string str = "";
            List<Menu> listpro = new List<Menu>();
            var list = db.Trunggians.Where(s => s.Proid == int.Parse(ID) && s.Trangthai == 2).ToList();// tìm trong bảng trung gian có bao nhiêu ID 218
            for (int i = 0; i < list.Count; i++)
            {
                var table = db.Menus.Where(s => s.ID == int.Parse(list[i].Icolor.ToString()) && s.Status == 1 && s.capp == "SI").ToList();//so sánh bảng menu và bảng trung gian để lấy ra tên của mầu
                for (int j = 0; j < table.Count; j++)
                {
                    listpro.Add(table[j]);
                }
            }
            foreach (var item in listpro)
            {
                str += "<a href=\"javascript:void(0)\" class=\"size\">" + item.Name + "<div class=\"pl\"><img src=\"/Resources/images/activee.png\" /></div></a>";
            }
            return str;
        }

        private void ShowMau()
        {
            string Chuoi = "";
            List<Entity.Menu> list = SMenu.LOAD_CATESPARENT_ID(More.CO, this.lang, "-1", "1");
            cblcat.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                cblcat.Items.Add(new ListItem(Chuoi + "<img src=\"" + list[i].Images + "\">", list[i].ID.ToString()));
            }
            list.Clear();

        }

        private void InsertCenter(string proId, string cid, CheckBoxList cbl)
        {
            foreach (ListItem listItem in cbl.Items)
            {
                if (listItem.Selected == true)
                {
                    Trunggian obj = new Trunggian();
                    obj.Proid = int.Parse(proId);
                    obj.Icolor = int.Parse(listItem.Value);
                    obj.Trangthai = 1;
                    obj.icid = int.Parse(cid);
                    db.Trunggians.InsertOnSubmit(obj);
                    db.SubmitChanges();
                }
            }
        }

        private void LoadListGroupNews(string id)
        {
            //cblcat
            cblcat.Items.Clear();
            List<Entity.Menu> list = SMenu.LOAD_CATESPARENT_ID(More.CO, this.lang, "-1", "1");
            for (int i = 0; i < list.Count; i++)
            {
                ListItem li = new ListItem("<img src=\"" + list[i].Images + "\">", list[i].ID.ToString());
                List<Trunggian> lst = (from p in db.Trunggians where p.Proid == int.Parse(id) && p.Icolor == int.Parse(list[i].ID.ToString()) select p).ToList();
                li.Selected = lst.Count > 0;
                cblcat.Items.Add(li);
            }
        }

        private void UpdateCenter(string proId, string cid)
        {
            string selectedValue = "";
            foreach (ListItem listItem in cblcat.Items)
            {
                if (listItem.Selected == true)
                {
                    selectedValue += listItem.Value + ",";
                }
            }
            string[] center = selectedValue.Split(',');
            foreach (string t in center)
            {
                Trunggian obj = new Trunggian();
                Trunggian abc = db.Trunggians.SingleOrDefault(p => p.Proid == int.Parse(proId));
                obj.Proid = int.Parse(proId);
                obj.Icolor = int.Parse(t);
                obj.Trangthai = 1;
                obj.icid = int.Parse(cid);
                db.SubmitChanges();
            }
        }

        //kich thuoc
        private void Showkichthuoc()
        {
            string Chuoi = "";
            List<Entity.Menu> list = SMenu.LOAD_CATESPARENT_ID(More.SI, this.lang, "-1", "1");
            ckichthuoc.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                ckichthuoc.Items.Add(new ListItem(Chuoi + " " + list[i].Name, list[i].ID.ToString()));
            }
            list.Clear();
        }

        private void InsertCenterkt(string proId, string cid, CheckBoxList cbl)
        {
            foreach (ListItem listItem in cbl.Items)
            {
                if (listItem.Selected == true)
                {
                    Trunggian obj = new Trunggian();
                    obj.Proid = int.Parse(proId);
                    obj.Icolor = int.Parse(listItem.Value);
                    obj.Trangthai = 2;
                    obj.icid = int.Parse(cid);
                    db.Trunggians.InsertOnSubmit(obj);
                    db.SubmitChanges();
                }
            }
        }

        private void LoadListGroupNewskt(string id)
        {
            //cblcat
            ckichthuoc.Items.Clear();
            List<Entity.Menu> list = SMenu.LOAD_CATESPARENT_ID(More.SI, this.lang, "-1", "1");
            for (int i = 0; i < list.Count; i++)
            {
                ListItem li = new ListItem(list[i].Name, list[i].ID.ToString());
                List<Trunggian> lst = (from p in db.Trunggians where p.Proid == int.Parse(id) && p.Icolor == int.Parse(list[i].ID.ToString()) select p).ToList();
                li.Selected = lst.Count > 0;
                ckichthuoc.Items.Add(li);
            }
        }

        private void UpdateCenterkt(string proId, string cid)
        {
            string selectedValue = "";
            foreach (ListItem listItem in ckichthuoc.Items)
            {
                if (listItem.Selected == true)
                {
                    selectedValue += listItem.Value + ",";
                }
            }
            string[] center = selectedValue.Split(',');
            foreach (string t in center)
            {
                Trunggian obj = new Trunggian();
                Trunggian abc = db.Trunggians.SingleOrDefault(p => p.Proid == int.Parse(proId));
                obj.Proid = int.Parse(proId);
                obj.Icolor = int.Parse(t);
                obj.Trangthai = 2;
                obj.icid = int.Parse(cid);
                db.SubmitChanges();
            }
        }
        #endregion

        public string GetRating(string id)
        {
            string DataRate = "0";
            string RateCount = "0";
            float result = 0;
            product product = db.products.FirstOrDefault(s => s.Status == 1 && s.ipid == int.Parse(id));
            if (product != null)
            {
                if (product.RateCount != null && product.RateSum != null && product.RateCount != 0)
                {
                    result = (float)product.RateSum / (float)product.RateCount;
                    RateCount = product.RateCount.ToString();
                }
            }
            DataRate = result.ToString();
            return RateCount;
        }

        protected void txtTieude_TextChanged(object sender, EventArgs e)
        {
            TextBox Tieude = (TextBox)sender;

            var b = (HiddenField)Tieude.FindControl("hiID");
            #region UpdateMenu
            string TagName = "";
            if (Tieude.Text.Length > 0)
            {
                List<Entity.Products> item = SProducts.GetById(b.Value);
                if (item.Count > 0)
                {
                    ModulControl obk = db.ModulControls.SingleOrDefault(p => p.TangName == item[0].TangName);
                    obk.Name = txtname.Text;
                    obk.Module = 21;
                    List<ModulControl> list = (from p in db.ModulControls where p.TangName == obk.TangName orderby p.ID descending select p).ToList();
                    if (list.Count > 2)
                    {
                        var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(Tieude.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(Tieude.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(Tieude.Text);
                    }
                    else
                    {
                        if (MoreAll.AddURL.SeoURL(item[0].Name) != MoreAll.AddURL.SeoURL(Tieude.Text)) { var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(Tieude.Text)).FirstOrDefault(); TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(Tieude.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(Tieude.Text); } else { TagName = item[0].TangName; }
                    }
                    obk.TangName = TagName;
                    db.SubmitChanges();
                    SProducts.Name_Text("UPDATE [Products] SET Name=N'" + Tieude.Text + "',TangName='" + TagName + "'  WHERE ipid=" + b.Value + "");
                    LoadItems();
                    this.ltmsg.Text = "<span class=alert>Cập tiêu đề thành công !!</span>";
                }
            }
            else
            {
                LoadItems();
                ltmsg.Text = "<span class=alert>Bạn chưa nhập tiêu đề !!</span>";
            }
            #endregion

        }
        protected void txtgiacu_TextChanged(object sender, EventArgs e)
        {
            TextBox Tieude = (TextBox)sender;
            var b = (HiddenField)Tieude.FindControl("hiID");
            #region UpdateOldPrice
            if (Tieude.Text.Length > 0)
            {
                List<Entity.Products> item = SProducts.GetById(b.Value);
                if (item.Count > 0)
                {
                    SProducts.Name_Text("UPDATE [Products] SET OldPrice=" + Tieude.Text.Replace(".", "").Replace(",", "") + " WHERE ipid=" + b.Value + "");
                    LoadItems();
                    this.ltmsg.Text = "<span class=alert>Cập nhật giá cũ thành công !!</span>";
                }
            }
            else
            {
                LoadItems();
                ltmsg.Text = "<span class=alert>Bạn chưa nhập giá !!</span>";
            }
            #endregion

        }
        protected void txtgiaban_TextChanged(object sender, EventArgs e)
        {
            TextBox Tieude = (TextBox)sender;
            var b = (HiddenField)Tieude.FindControl("hiID");
            #region UpdatePrice
            if (Tieude.Text.Length > 0)
            {
                List<Entity.Products> item = SProducts.GetById(b.Value);
                if (item.Count > 0)
                {
                    SProducts.Name_Text("UPDATE [Products] SET Price=" + Tieude.Text.Replace(".", "").Replace(",", "") + " WHERE ipid=" + b.Value + "");
                    LoadItems();
                    this.ltmsg.Text = "<span class=alert>Cập nhật giá cũ thành công !!</span>";
                }
            }
            else
            {
                LoadItems();
                ltmsg.Text = "<span class=alert>Bạn chưa nhập giá !!</span>";
            }
            #endregion

        }
        protected void txtgiabanthanhvien_TextChanged(object sender, EventArgs e)
        {
            TextBox Tieude = (TextBox)sender;
            var b = (HiddenField)Tieude.FindControl("hiID");
            #region UpdatePrice
            if (Tieude.Text.Length > 0)
            {
                List<Entity.Products> item = SProducts.GetById(b.Value);
                if (item.Count > 0)
                {
                    SProducts.Name_Text("UPDATE [Products] SET GiaThanhVien=" + Tieude.Text.Replace(".", "").Replace(",", "") + " WHERE ipid=" + b.Value + "");
                    LoadItems();
                    this.ltmsg.Text = "<span class=alert>Cập nhật giá Thành viên thành công !!</span>";
                }
            }
            else
            {
                LoadItems();
                ltmsg.Text = "<span class=alert>Bạn chưa nhập giá Thành viên!!</span>";
            }
            #endregion
        }
        protected void txtgiacongtynhapvao_TextChanged(object sender, EventArgs e)
        {
            TextBox Tieude = (TextBox)sender;
            var b = (HiddenField)Tieude.FindControl("hiID");
            #region UpdatePrice
            if (Tieude.Text.Length > 0)
            {
                List<Entity.Products> item = SProducts.GetById(b.Value);
                if (item.Count > 0)
                {
                    SProducts.Name_Text("UPDATE [Products] SET Giacongtynhapvao=" + Tieude.Text.Replace(".", "").Replace(",", "") + " WHERE ipid=" + b.Value + "");
                    LoadItems();
                    this.ltmsg.Text = "<span class=alert>Cập nhật giá công ty nhập vào thành công !!</span>";
                }
            }
            else
            {
                LoadItems();
                ltmsg.Text = "<span class=alert>Bạn chưa nhập giá công ty nhập vào !!</span>";
            }
            #endregion
        }


        public string ShowThanhVien(string IDThanhVien)
        {
            if (IDThanhVien != "0")
            {
                user table = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(IDThanhVien));
                if (table != null)
                {
                    return "<a target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&IDThanhVien=" + table.iuser_id.ToString() + "\"><span style='color:red'>" + table.vfname + "</span></a>";
                }
            }
            return "Admin";
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
                if (txtMImage.Text.Length > 0)
                {
                    ltshowiavascrip.Text = "<script type='text/javascript'>$(function() { LoadStringImg('" + txtMImage.Text + "','" + txtMImage.ClientID + "');}); </script>";
                }
                else
                {
                    txtMImage.Text = "";
                }

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
                    List<Entity.Products> item = SProducts.GetById(this.hdid.Value);
                    if (item.Count > 0)
                    {
                        if (item[0].Anh.Length > 0)
                        {
                            txtMImage.Text = item[0].Anh;
                            ltshowiavascrip.Text = "<script type='text/javascript'>$(function() { LoadStringImg('" + item[0].Anh + "','" + txtMImage.ClientID + "');}); </script>";
                        }
                        else
                        {
                            txtMImage.Text = "";
                        }

                        ModulControl obk = db.ModulControls.SingleOrDefault(p => p.TangName == item[0].TangName);
                        obk.Name = txtRewriteUrl.Text;
                        obk.Module = 21;
                        List<ModulControl> list = (from p in db.ModulControls where p.TangName == obk.TangName orderby p.ID descending select p).ToList();
                        if (list.Count > 2)
                        {
                            var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txtRewriteUrl.Text)).FirstOrDefault();
                            TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txtRewriteUrl.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txtRewriteUrl.Text);
                        }
                        else
                        {
                            if (MoreAll.AddURL.SeoURL(item[0].TangName) != MoreAll.AddURL.SeoURL(txtRewriteUrl.Text))
                            {
                                var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txtRewriteUrl.Text)).FirstOrDefault();
                                TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txtRewriteUrl.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txtRewriteUrl.Text);
                            }
                            else
                            {
                                TagName = item[0].TangName;
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region UpdateMenu
                    List<Entity.Products> item = SProducts.GetById(this.hdid.Value);
                    if (item.Count > 0)
                    {
                        if (item[0].Anh.Length > 0)
                        {
                            txtMImage.Text = item[0].Anh;
                            ltshowiavascrip.Text = "<script type='text/javascript'>$(function() { LoadStringImg('" + item[0].Anh + "','" + txtMImage.ClientID + "');}); </script>";
                        }
                        else
                        {
                            txtMImage.Text = "";
                        }


                        ModulControl obk = db.ModulControls.SingleOrDefault(p => p.TangName == item[0].TangName);
                        obk.Name = txtname.Text;
                        obk.Module = 21;
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

        protected void lnkcapnhatthanhvien_Click(object sender, EventArgs e)
        {
            if (ddlthanhvien.SelectedValue != "-1")
            {
                for (int i = 0; i < rpitems.Items.Count; i++)
                {
                    HiddenField id = (HiddenField)rpitems.Items[i].FindControl("hiID");
                    SProducts.Name_Text("UPDATE [Products] SET IDThanhVien=" + ddlthanhvien.SelectedValue + " WHERE ipid=" + id.Value + "");
                    LoadItems();
                    this.ltmsg.Text = "<span class=alert>Cập nhật Thành viên thành công !!</span>";
                }
            }
            else
            {
                LoadItems();
                ltmsg.Text = "<span class=alert>Vui lòng chọn thành viên cần cập nhật !!</span>";
            }

        }

    }
}
using Entity;
using Framwork;
using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.QuanLyDangBai
{
    public partial class ThanhVienDangBai : System.Web.UI.UserControl
    {
        private string id = "-1";
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
            if (Request["id"] != null && !Request["id"].Equals(""))
            {
                id = Request["id"];
            }
            if (Request["st"] != null && !Request["st"].Equals(""))
            {
                ddlstatus.SelectedValue = Request["st"];
                WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlstatus, this.status);
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
                if (MoreAll.MoreAll.GetCookies("Members") != "")
                {
                    user table = db.users.SingleOrDefault(p => p.vuserun == MoreAll.MoreAll.GetCookies("Members").ToString());
                    if (table != null)
                    {
                        if (table.DuyetTienDanap == 0)
                        {
                            Response.Write("<script type=\"text/javascript\">alert('Bạn không thể sử dụng tính năng này. Yêu cầu kích hoạt tài khoản thành viên.');window.location.href='/vi-tien.html'; </script>");
                        }
                        if (table.TatChucNang == 1)
                        {
                            Response.Write("<script type=\"text/javascript\">alert('Bạn không thể sử dụng tính năng này.');window.location.href='/vi-tien.html'; </script>");
                        }
                        hdthanhvien.Value = table.iuser_id.ToString();
                        // ShowHang();

                        List<Entity.Products> dtdetail = SProducts.Name_Text("select * from Products where IDThanhVien=" + table.iuser_id.ToString() + " ");
                        if (dtdetail.Count > 0)
                        {
                            SCartDetail.Name_Text("UPDATE [users] SET TongSoSanPham=" + dtdetail.Count + " WHERE iuser_id =" + table.iuser_id.ToString() + "");
                        }
                        else
                        {
                            SCartDetail.Name_Text("UPDATE [users] SET TongSoSanPham=0 WHERE iuser_id =" + table.iuser_id.ToString() + "");
                        }

                        LoadCategories();
                        LoadItems();
                    }

                }
            }
        }
        //protected void ShowHang()
        //{
        //    int str = 0;
        //    List<Entity.Menu> dt = SMenu.LOAD_CATESPARENT_ID(More.HA, this.lang, "-1", "1");
        //    for (int i = 0; i < dt.Count; i++)
        //    {
        //        ddlthuonghieu.Items.Insert(str, new ListItem(dt[i].Name.ToString(), dt[i].ID.ToString()));
        //    }
        //    this.ddlthuonghieu.DataBind();
        //}
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
        //void LoadItems()
        //{
        //    try
        //    {
        //        Fproducts DB = new Fproducts();
        //        string[] searchfields = new string[] { "Name", "Brief", "Contents", "search" };
        //        string orderby = this.ddlorderby.SelectedValue + " " + this.ddlordertype.SelectedValue;
        //        List<Entity.Products> dt = DB.CategoryAdmin_ThanhVien(orderby, this.txtkeyword.Text.Trim().Replace("&nbsp;", ""), searchfields, More.Sub_Menu(More.PR, ddlcategories.SelectedValue), lang, ddlstatus.SelectedValue, hdthanhvien.Value, Vitri);
        //        CollectionPager1.DataSource = dt;
        //        CollectionPager1.BindToControl = rpitems;
        //        CollectionPager1.MaxPages = 10000;
        //        CollectionPager1.PageSize = 2;
        //        rpitems.DataSource = CollectionPager1.DataSourcePaged;
        //        rpitems.DataBind();
        //        //  RemoveCache.Products();
        //    }
        //    catch (Exception) { }
        //}

        public void LoadItems()
        {
            user table = db.users.SingleOrDefault(p => p.vuserun == MoreAll.MoreAll.GetCookies("Members").ToString());
            if (table != null)
            {
                string sapxep = "";
                string orderby = this.ddlorderby.SelectedValue + " " + this.ddlordertype.SelectedValue;
                string Vitri = "";
                Vitri += " and IDThanhVien =" + table.iuser_id.ToString() + " and Status!=5 ";
                if (txtkeyword.Text.Trim().Length > 0)
                {
                    Vitri += " and (Name LIKE N'" + Fproducts.SearchApproximate.Exec(txtkeyword.Text.Trim()) + "' OR Code LIKE N'" + Fproducts.SearchApproximate.Exec(txtkeyword.Text.Trim()) + "')";
                }
                if (orderby.Length < 1)
                {
                    sapxep = "order by Create_Date desc";
                }
                else
                {
                    sapxep = "order by " + orderby;
                }
                if (ddlstatus.SelectedValue != "-1")
                {
                    Vitri += " and Status =" + ddlstatus.SelectedValue + "";
                }

                int Tongsobanghi = 0;
                Int16 pages = 1;
                int Tongsotrang = int.Parse("20");
                if ((Request.QueryString["page"] != null) && (Request.QueryString["page"] != ""))
                {
                    pages = Convert.ToInt16(Request.QueryString["page"].Trim());
                }
                List<Entity.Products> iitem = SProducts.QuanLyThanhVien_locTongbanghi(Commond.SubMenu(More.PR, ddlcategories.SelectedValue), Vitri, lang);
                if (iitem.Count() > 0)
                {
                    Tongsobanghi = iitem.Count();
                }
                List<Entity.Products> dt = SProducts.QuanLyThanhVien_loc(Commond.SubMenu(More.PR, ddlcategories.SelectedValue), Vitri, lang, sapxep, (pages - 1), Tongsotrang);
                if (dt.Count >= 1)
                {
                    rpitems.DataSource = dt;
                    rpitems.DataBind();
                }
                else
                {
                    lterr.Text = "<div class='Checkdata'>" + this.label("I_dulieuchuadccapnhat") + "</div>";
                }
                if (Tongsobanghi % Tongsotrang > 0)
                {
                    Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
                }
                else
                {
                    Tongsobanghi = Tongsobanghi / Tongsotrang;
                }
                ltpage.Text = Commond.Phantrang_Sanpham("/quan-ly-san-pham.html", "&id=" + ddlcategories.SelectedValue + "&st=" + ddlstatus.SelectedValue + "&us=" + ddlorderby.SelectedValue + "&ds=" + ddlordertype.SelectedValue + "&kw=" + txtkeyword.Text + "", Tongsobanghi, pages);
            }
        }

        protected void lnkcreatenew_Click(object sender, EventArgs e)
        {
            hdinsertupdate.Value = "insert";
            MultiView1.ActiveViewIndex = 1;
            DeleteFormValue();
            WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlcategoriesdetail, this.ddlcategories.SelectedValue);
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            DeleteFormValue();
            LoadItems();
        }
        protected bool Test()
        {

            double Giacongtynhapvao = Convert.ToDouble(txtgiacongtynhapvao.Text);
            double GiaNY = Convert.ToDouble(txtprice.Text);
            if (GiaNY <= Giacongtynhapvao)
            {
                lbl_msg.Text = "Giá bán cho Ag phải nhỏ hơn giá bán niêm yết!";
                lbl_msg.Focus();
                return false;
            }
            return true;
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
                //else if (this.txtcode.Text.Trim().Length < 1)
                //{
                //    this.lbl_msg.Text = "Vui lòng điền mã sản phẩm";
                //}
                else if (ddlsanphanthuoc.SelectedValue == "0")
                {
                    this.lbl_msg.Text = "Vui lòng chọn kiểu sản phẩm";
                }
                else
                {
                    if (Test() == true)
                    {

                        int SPAG = 0;
                        if (this.checkAg.Checked)
                        {
                            SPAG = 1;
                        }

                        int Chek = 0;
                        string cdate = DateTime.Now.ToString();
                        string edate = DateTime.Now.AddYears(10).ToString();
                        DateTime dcreatedate = Convert.ToDateTime(cdate.ToString());
                        DateTime denddate = Convert.ToDateTime(edate.ToString());

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
                            obj.ID_Hang = int.Parse("0");// int.Parse(ddlthuonghieu.SelectedValue);
                            obj.sanxuat = int.Parse("0");
                            obj.Code = txtcode.Text;
                            obj.Name = txtname.Text;
                            obj.Brief = FilterKeywords(txtdesc.Text);
                            obj.Contents = FilterKeywords(txtcontent.Text);
                            obj.search = RewriteURLNew.NameSearch(txtname.Text);
                            //obj.Images = vimg;
                            //obj.ImagesSmall = small;
                            obj.Images = txtImage.Text;
                            obj.ImagesSmall = txtImage.Text.Replace("uploads", "uploads/_thumbs");

                            obj.Equals = 0;
                            obj.Quantity = 1;// int.Parse(txtquantity.Text.Trim());
                            obj.Price = txtprice.Text;
                            obj.OldPrice = txtgiacongtynhapvao.Text;
                            obj.Views = 0;
                            obj.Chekdata = Chek;
                            obj.Create_Date = dcreatedate;
                            obj.Modified_Date = denddate;
                            obj.lang = this.lang;
                            obj.News = 0;
                            obj.Home = 0;
                            obj.Check_01 = 0;
                            obj.Check_02 = 0;
                            obj.Check_03 = 0;
                            obj.Check_04 = 0;
                            obj.Check_05 = 0;
                            obj.Status = 0;// int.Parse(chkstatus.Checked ? "1" : "0");
                            obj.Titleseo = txttitleseo.Text;
                            obj.Meta = txtmeta.Text;
                            obj.Keyword = txtKeywordS.Text;
                            obj.Anh = txtMImage.Text.TrimEnd(',');
                            obj.Noidung1 = "";
                            obj.Noidung2 = txtMImageSS.Text.TrimEnd(',');
                            obj.Noidung3 = "0";
                            obj.Noidung4 = "";
                            obj.Noidung5 = "";
                            obj.TangName = TangName;
                            obj.RateCount = 0;
                            obj.RateSum = 0;
                            obj.Alt = txtAlt.Text;
                            obj.IDThanhVien = int.Parse(hdthanhvien.Value);
                            obj.DiemMuaHang = int.Parse("0");
                            obj.GiaThanhVien = txtgiacongtynhapvao.Text;
                            obj.Giacongtynhapvao = txtgiacongtynhapvao.Text;
                            obj.TrangThaiAgLang = int.Parse(ddlsanphanthuoc.SelectedValue);
                            obj.Phaply = int.Parse(ddlchon.SelectedValue);
                            obj.SanPhamAg = SPAG;
                            obj.TrongLuong = txttrongluong.Text;
                            obj.GiaThanhVienFree = "0";
                            obj.GiaChietKhauDaiLy = "0";
                            //obj.GiaThanhVienFree = "0";
                            obj.ChietKhau = Commond.AddEdit_Giamgia(txtprice.Text, txtgiacongtynhapvao.Text);

                            // obj.PhanTramChietKhauDaiLy = Convert.ToInt32(Commond.CapBacChietKhauDaiLy(Commond.AddEdit_Giamgia(txtprice.Text, txtgiacongtynhapvao.Text)));
                            // obj.PhanTramChietKhauThanhVien = Convert.ToInt32(Commond.CapBacChietKhauThanhVien(Commond.AddEdit_Giamgia(txtprice.Text, txtgiacongtynhapvao.Text)));

                            obj.PhanTramChietKhauDaiLy = Convert.ToInt32((Commond.AddEdit_Giamgia(txtprice.Text, txtgiacongtynhapvao.Text)));
                            obj.PhanTramChietKhauThanhVien = Convert.ToInt32((Commond.AddEdit_Giamgia(txtprice.Text, txtgiacongtynhapvao.Text)));
                            obj.KichHoatDaiLy = 0;
                            obj.GiaCuaHang = "0";
                            //  PhanTramChietKhauDaiLy,PhanTramChietKhauThanhVien
                            #endregion
                            SProducts.Insert(obj);
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
                                    if (obk != null)
                                    {
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
                                    else
                                    {
                                        if (MoreAll.AddURL.SeoURL(item[0].TangName) != MoreAll.AddURL.SeoURL(txtname.Text))
                                        {
                                            var hasTagName = db.ModulControls.Where(s => s.TangName == MoreAll.AddURL.SeoURL(txtname.Text)).FirstOrDefault();
                                            TagName = hasTagName != null ? MoreAll.AddURL.SeoURL(txtname.Text) + "-" + obk.ID : MoreAll.AddURL.SeoURL(txtname.Text);
                                        }
                                        else
                                        {
                                            TagName = item[0].TangName;
                                        }
                                        ModulControl obl = new ModulControl();
                                        obl.Name = txtname.Text;
                                        obl.Module = 21;
                                        obl.TangName = TagName;
                                        db.ModulControls.InsertOnSubmit(obl);
                                        db.SubmitChanges();
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
                            List<Entity.Products> items = SProducts.GetById(this.hdid.Value);

                            Entity.Products obj = new Entity.Products();
                            #region MyRegion
                            obj.ipid = int.Parse(hdid.Value);
                            obj.icid = int.Parse(ddlcategoriesdetail.SelectedValue);
                            obj.ID_Hang = int.Parse("0"); //int.Parse(ddlthuonghieu.SelectedValue);
                            obj.sanxuat = int.Parse("0");
                            obj.Code = txtcode.Text;
                            obj.Name = txtname.Text;
                            obj.Brief = FilterKeywords(txtdesc.Text);
                            obj.Contents = FilterKeywords(txtcontent.Text);
                            obj.search = RewriteURLNew.NameSearch(txtname.Text);
                            //obj.Images = vimg;
                            //obj.ImagesSmall = small;
                            obj.Images = txtImage.Text;
                            obj.ImagesSmall = txtImage.Text.Replace("uploads", "uploads/_thumbs");
                            obj.Equals = 0;
                            obj.Quantity = 1;// int.Parse(txtquantity.Text.Trim());
                            obj.Price = txtprice.Text;
                            obj.OldPrice = txtgiacongtynhapvao.Text;
                            obj.Views = 0;
                            obj.Chekdata = Chek;
                            obj.Create_Date = dcreatedate;
                            obj.Modified_Date = denddate;
                            obj.lang = this.lang;
                            obj.News = items[0].News;
                            obj.Home = items[0].Home;
                            obj.Check_01 = items[0].Check_01;
                            obj.Check_02 = items[0].Check_02;
                            obj.Check_03 = items[0].Check_03;
                            obj.Check_04 = items[0].Check_04;
                            obj.Check_05 = items[0].Check_05;
                            obj.Status = 4;// int.Parse(chkstatus.Checked ? "1" : "0");
                            obj.Titleseo = txttitleseo.Text;
                            obj.Meta = txtmeta.Text;
                            obj.Keyword = txtKeywordS.Text;
                            obj.Anh = txtMImage.Text.TrimEnd(',');
                            obj.Noidung1 = "";
                            obj.Noidung2 = txtMImageSS.Text.TrimEnd(',');
                            obj.Noidung3 = "0";
                            obj.Noidung4 = "";
                            obj.Noidung5 = "";
                            obj.TangName = TagName;
                            obj.Alt = txtAlt.Text;
                            obj.IDThanhVien = int.Parse(hdthanhvien.Value);
                            obj.DiemMuaHang = int.Parse("0");
                            obj.GiaThanhVien = txtgiacongtynhapvao.Text;
                            obj.Giacongtynhapvao = txtgiacongtynhapvao.Text;
                            obj.TrangThaiAgLang = int.Parse(ddlsanphanthuoc.SelectedValue);
                            obj.Phaply = int.Parse(ddlchon.SelectedValue);
                            obj.SanPhamAg = SPAG;
                            obj.TrongLuong = txttrongluong.Text;
                            obj.GiaThanhVienFree = "0";
                            obj.GiaChietKhauDaiLy = "0";
                           // obj.GiaThanhVienFree = hdGiaThanhVienFree.Value;
                            obj.ChietKhau = Commond.AddEdit_Giamgia(txtprice.Text, txtgiacongtynhapvao.Text);
                            obj.PhanTramChietKhauDaiLy = Convert.ToInt32((Commond.AddEdit_Giamgia(txtprice.Text, txtgiacongtynhapvao.Text)));
                            obj.PhanTramChietKhauThanhVien = Convert.ToInt32((Commond.AddEdit_Giamgia(txtprice.Text, txtgiacongtynhapvao.Text)));
                           // obj.PhanTramChietKhauDaiLy = Convert.ToInt32(Commond.CapBacChietKhauDaiLy(Commond.AddEdit_Giamgia(txtprice.Text, txtgiacongtynhapvao.Text)));
                           // obj.PhanTramChietKhauThanhVien = Convert.ToInt32(Commond.CapBacChietKhauThanhVien(Commond.AddEdit_Giamgia(txtprice.Text, txtgiacongtynhapvao.Text)));
                            obj.KichHoatDaiLy = 0;
                            obj.GiaCuaHang = "0";
                            #endregion
                            // //  PhanTramChietKhauDaiLy,PhanTramChietKhauThanhVien
                            SProducts.Update(obj);
                        }
                        LoadItems();
                        MultiView1.ActiveViewIndex = 0;
                        DeleteFormValue();
                        base.Response.Redirect("/quan-ly-san-pham.html");
                    }
                }
            }
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
            txttrongluong.Text = "";
            //this.txtvimg.Text = "";
            this.hdFileName.Value = "";
            // ltimg.Text = "";
            hdimgMax.Value = "";
            hdimgsmallEdit.Value = "";
            hdimgMaxEdit.Value = "";
            txttitleseo.Text = "";
            txtmeta.Text = "";
            txtKeywordS.Text = "";
            txtMImage.Text = "";
            txtMImageSS.Text = "";
            txtRewriteUrl.Text = "";
            ltimgs.Text = "";
            txtImage.Text = "";
            hdimgnews.Value = "";
            ltshowurl.Text = "";
            txtAlt.Text = "";
            txtgiacongtynhapvao.Text = "";
           // txtGiaThanhVien.Text = "";
            hdGiaThanhVienFree.Value = "";
             txtprice.Text = "";
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
                        //hdGiaThanhVienFree.Value = dtdetail[0].GiaThanhVienFree.ToString();

                        // ltimg.Text = MoreImage.Image(dtdetail[0].ImagesSmall.ToString());
                        this.txtquantity.Text = dtdetail[0].Quantity.ToString();
                        this.txtprice.Text = dtdetail[0].Price.ToString();

                        txtRewriteUrl.Text = dtdetail[0].TangName.ToString();
                        txtAlt.Text = dtdetail[0].Alt;
                        txttrongluong.Text = dtdetail[0].TrongLuong;
                       // txtGiaThanhVien.Text = dtdetail[0].GiaThanhVien;
                        //  txtdiemmuahang.Text = dtdetail[0].DiemMuaHang.ToString();
                        txtgiacongtynhapvao.Text = dtdetail[0].OldPrice.ToString();

                      //  txtprice.Text = dtdetail[0].GiaCuaHang.ToString();

                        hdthanhvien.Value = dtdetail[0].IDThanhVien.ToString();
                        ddlchon.SelectedValue = dtdetail[0].Phaply.ToString();
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
                            ltanh1.Text += "<script type='text/javascript'>jQuery(document).ready(function ($) {LoadStringImg('" + dtdetail[0].Anh + "','" + txtMImage.ClientID + "'); });</script>";
                            //  ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "LoadImage", @"<script type='text/javascript'>LoadStringImg('" + dtdetail[0].Anh + "','" + txtMImage.ClientID + "');</script>", false);
                        }
                        else
                        {
                            txtMImage.Text = "";
                        }

                        if (dtdetail[0].Noidung2.Length > 0)
                        {
                            txtMImageSS.Text = dtdetail[0].Noidung2;
                            ltanh2.Text += "<script type='text/javascript'>jQuery(document).ready(function ($) {LoadStringImgSS('" + dtdetail[0].Noidung2 + "','" + txtMImageSS.ClientID + "'); });</script>";
                        }
                        else
                        {
                            txtMImageSS.Text = "";
                        }


                        //WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlthuonghieu, dtdetail[0].ID_Hang.ToString());
                        WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlcategoriesdetail, dtdetail[0].icid.ToString());
                        WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlsanphanthuoc, dtdetail[0].TrangThaiAgLang.ToString());
                        WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlchon, dtdetail[0].Phaply.ToString());

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
                        List<Entity.CartDetail> tablek = SCartDetail.Name_Text("SELECT * from CartDetail where ipid=" + e.CommandArgument.ToString() + "");
                        if (tablek.Count > 0)
                        {
                            SProducts.Name_Text("update products set Status=5 where ipid=" + e.CommandArgument.ToString() + "");
                        }
                        else
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

                        }

                        LoadItems();
                        base.Response.Redirect("/quan-ly-san-pham.html");
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
            WebControlsUtilities.SetSelectedIndexInDropDownList(ref this.ddlcategoriesdetail, this.ddlcategories.SelectedValue);
        }

        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
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

                        List<Entity.CartDetail> tablek = SCartDetail.Name_Text("SELECT * from CartDetail where ipid=" + id.Value + "");
                        if (tablek.Count > 0)
                        {
                            SProducts.Name_Text("update products set Status=5 where ipid=" + id.Value + "");
                        }
                        else
                        {

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
                }
                LoadItems();
                base.Response.Redirect("/quan-ly-san-pham.html");
            }
            catch (Exception)
            {
                lterr.Text = "Sản phẩm đang tồn tại trong giỏ hàng của bạn .Yêu cầu xem lại trước khi xóa";
            }
        }



        protected void lnksearch_Click(object sender, EventArgs e)
        {
            LoadItems();
            LoadRequest();
        }



        protected void bthienthi_Click(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }

        protected void LoadRequest()
        {
            Response.Redirect("/quan-ly-san-pham.html?id=" + ddlcategories.SelectedValue + "&st=" + ddlstatus.SelectedValue + "&us=" + ddlorderby.SelectedValue + "&ds=" + ddlordertype.SelectedValue + "&kw=" + txtkeyword.Text + "");
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
        public static string FilterKeywords(string strText)
        {
            if (Check(strText))
            {
                strText = strText.Trim().Replace("'", "&#39;");
                strText = strText.Replace("script", string.Empty);
                //string str = strText.Replace("drop", string.Empty);
                //str = strText.Replace("--", string.Empty);
                //str = strText.Replace(";", string.Empty);
                //str = strText.Replace("exec", string.Empty);
                return strText;
            }
            return "";
        }
        public static bool Check(object String)
        {
            return ((String != null) && (String.ToString().Trim().Length > 0));
        }

        protected void rp_pagelist_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ddlNhom = (e.Item.FindControl("ddlNhom") as DropDownList);
                Label lblicid = (e.Item.FindControl("lblicid") as Label);
                List<Entity.Menu> list = SMenu.Name_Text("SELECT * FROM Menu where capp='" + More.PR + "' and lang='" + lang + "' and status=1 order by level,Orders asc");
                for (int i = 0; i < list.Count; i++)
                {
                    string space = "";
                    for (int j = 0; j < list[i].Level.Length / 5 - 1; j++) space += "-----";
                    ddlNhom.Items.Add(new ListItem(space + list[i].Name, list[i].ID.ToString()));
                }
                ddlNhom.SelectedValue = lblicid.Text;
                list.Clear();
                list = null;
            }
        }

        protected void ddlNhom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlNhom = (DropDownList)sender;
                var b = (HiddenField)ddlNhom.FindControl("hiID");
                if (ddlNhom.SelectedValue == "0")
                {
                    ltmsg.Text = "Yêu cầu chọn nhóm !!";
                    LoadItems();
                }
                else
                {
                    SProducts.Name_Text("update  products SET icid='" + ddlNhom.SelectedValue + "' where ipid=" + b.Value + "");
                    LoadItems();
                    ltmsg.Text = "Cập nhật cấp nhóm sản phẩm thành công !!";
                }
            }
            catch (Exception)
            {
                ltmsg.Text = "Yêu cầu chọn nhóm !!";
            }
            LoadItems();
        }
    }
}
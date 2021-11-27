using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using System.IO;
using Services;
using Entity;
using System.Drawing.Imaging;

namespace VS.E_Commerce.cms.Admin.Album
{
    public partial class Album_images : System.Web.UI.UserControl
    {
        private string lang = Captionlanguage.Language;
        private string iphoto = "-1";
        private string ialb = "-1";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["ialb"] != null && !Request["ialb"].Equals(""))
            {
                ialb = Request["ialb"];
            }
            if (Request["iphoto"] != null && !Request["iphoto"].Equals(""))
            {
                iphoto = Request["iphoto"];
            }
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
                this.UpdateList();
                LoadItemsProduct();
                this.btnCancel.Text = this.label("l_cancel");
            }
        }

        void LoadItemsProduct()
        {
            try
            {
                rpitems.DataSource = SAlbum.GET_DETAIL_BYID(iphoto);
                rpitems.DataBind();
            }
            catch (Exception) { }
        }

        protected void btn_InsertUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txt_title.Text.Trim().Length < 0)
                {
                    this.lblmsg.Text = "Xin vui lòng kiểm tra dữ liệu đầu vào của bạn";
                }
                else if (!ValidateUtilities.IsValidInt(this.txt_order.Text.Trim()))
                {
                    this.lblmsg.Text = "Xin vui lòng kiểm tra dữ liệu đầu vào của bạn!";
                }
                else
                {
                    int status = 0;
                    if (this.chck_Enable.Checked)
                    {
                        status = 1;
                    }
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
                            String paths = "/Uploads/Album/";
                            path = Path.GetFileName(this.flimage.PostedFile.FileName);
                            string str6 = "";
                            str6 = Path.GetExtension(path).ToLower();
                            vimg = paths + DateTime.Now.Ticks.ToString() + str6;
                            flimage.PostedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + vimg);
                            small = paths + DateTime.Now.Ticks.ToString() + "SMall" + str6;
                            Database.ResizeIamgesFix(Server.MapPath(vimg), Server.MapPath(small), Convert.ToInt32(AllQuery.MoreAllBum.Height()), Convert.ToInt32(AllQuery.MoreAllBum.Width()));
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
                    Album_Images obj = new Album_Images();
                    string str5 = this.hd_insertupdate.Value.Trim();
                    if (str5 != null)
                    {
                        if (!(str5 == "update"))
                        {
                            if (str5 == "insert")
                            {
                                #region MyRegion
                                obj.Menu_ALB = int.Parse(iphoto);
                                obj.Menu_ID = int.Parse(ialb);
                                obj.Title = this.txt_title.Text.Trim();
                                obj.Brief = txtNoiDung.Text;
                                obj.Images = vimg;
                                obj.ImagesSmall = small;
                                obj.Equals = vkey;
                                obj.Orders = int.Parse(this.txt_order.Text.Trim());
                                obj.Status = status;
                                #endregion
                                SAlbum_Images.ALBUM_IMAGES_INSERT(obj);
                            }
                        }
                        else
                        {
                            #region Delete
                            if (vimg.Equals(""))
                            {
                                vimg = this.hdimgMaxEdit.Value;
                            }
                            if (small.Equals(""))
                            {
                                small = this.hdimgsmallEdit.Value;
                            }
                            else
                            {
                                try
                                {
                                    if ((this.flimage.FileName.Trim().Length > 0) && (this.flimage.PostedFile.ContentLength > 0))
                                    {
                                        File.Delete(utlitities.APPL_PHYSICAL_PATH + this.hdimgMaxEdit.Value);
                                        File.Delete(utlitities.APPL_PHYSICAL_PATH + this.hdimgsmallEdit.Value);
                                    }
                                    if (this.txtvimg.Text.Trim().Length > 0)
                                    {
                                        File.Delete(utlitities.APPL_PHYSICAL_PATH + this.hdimgMaxEdit.Value);
                                        File.Delete(utlitities.APPL_PHYSICAL_PATH + this.hdimgsmallEdit.Value);
                                    }
                                }
                                catch (Exception) { }
                            }
                            #endregion

                            #region MyRegion
                            obj.ID = int.Parse(this.hd_page_edit_id.Value);
                            obj.Menu_ALB = int.Parse(iphoto);
                            obj.Menu_ID = int.Parse(ialb);
                            obj.Title = this.txt_title.Text.Trim();
                            obj.Brief = txtNoiDung.Text;
                            obj.Images = vimg;
                            obj.ImagesSmall = small;
                            obj.Equals = vkey;
                            obj.Orders = int.Parse(this.txt_order.Text.Trim());
                            obj.Status = status;
                            #endregion
                            SAlbum_Images.ALBUM_IMAGES_UPDATE(obj);
                        }
                    }
                    this.UpdateList();
                    this.pn_list.Visible = true;
                    this.pn_insert.Visible = false;
                    this.hd_insertupdate.Value = "";
                    this.txt_title.Text = "";
                    this.txt_order.Text = "1";
                    this.txtvimg.Text = "";
                    this.hdFileName.Value = "";
                    ltimg.Text = "";
                    this.lblmsg.Text = "";
                    txtNoiDung.Text = "";
                }
            }
            catch (Exception) { }
        }

        public bool ThumbnailCallback()
        {
            return false;
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
        }

        protected void Delete_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('xóa bài viết này ?')";
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            this.btn_InsertUpdate.Text = this.label("l_insert");
            this.pn_list.Visible = false;
            this.pn_insert.Visible = true;
            this.hd_insertupdate.Value = "insert";
            this.hd_page_edit_id.Value = "-1";
            this.txtvimg.Text = "";
            this.hdFileName.Value = "";
            ltimg.Text = "";
            this.txtNoiDung.Text = "";
        }

        protected void rp_pagelist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            List<Entity.Album_Images> dtdetail = new List<Entity.Album_Images>();
            switch (e.CommandName)
            {
                #region EditDetail
                case "EditDetail":
                    dtdetail = SAlbum_Images.GET_DETAIL_BYID(e.CommandArgument.ToString());
                    {
                        this.pn_list.Visible = false;
                        this.pn_insert.Visible = true;
                        this.hd_insertupdate.Value = "update";
                        this.hd_page_edit_id.Value = e.CommandArgument.ToString();
                        List<Album_Images> table = SAlbum_Images.GET_DETAIL_BYID(e.CommandArgument.ToString());
                        if (table.Count > 0)
                        {
                            this.hdid.Value = table[0].ID.ToString().Trim();
                            this.txt_title.Text = table[0].Title.ToString().Trim();
                            this.txt_order.Text = table[0].Orders.ToString().Trim();
                            this.txtNoiDung.Text = table[0].Brief.ToString().Trim();
                            hdimgMaxEdit.Value = table[0].Images;
                            hdimgsmallEdit.Value = table[0].ImagesSmall.ToString();
                            ltimg.Text = MoreImage.Image(table[0].ImagesSmall.ToString());
                            if (table[0].Status.ToString().Trim().Equals("0"))
                            {
                                this.chck_Enable.Checked = false;
                            }
                            else if (table[0].Status.ToString().Equals("1"))
                            {
                                this.chck_Enable.Checked = true;
                            }
                            this.chck_Enable.Checked = true;
                            if (table[0].Equals.ToString().Trim().Equals("1"))
                            {
                                this.rdFromLinks.Checked = true;
                                this.rdFromComputer.Checked = false;
                                this.LoadView();
                                this.txtvimg.Text = table[0].Images;
                            }
                            else
                            {
                                this.rdFromComputer.Checked = true;
                                this.rdFromLinks.Checked = false;
                                this.LoadView();
                                this.hdFileName.Value = table[0].Images;
                            }
                        }
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
                        SAlbum_Images.Name_Text("UPDATE [Album_Images] SET [status] = " + str3 + " WHERE ID=" + str2 + "");
                        this.UpdateList();
                        base.Response.Redirect(base.Request.Url.ToString().Trim());
                        return;
                    }
                    return;
                #endregion
                #region Delete
                case "Delete":
                    {
                        try
                        {
                            #region Xoa anh Va danh sach
                            List<Album_Images> str1 = SAlbum_Images.GET_DETAIL_BYID(e.CommandArgument.ToString());
                            for (int k = 0; k < str1.Count; k++)
                            {
                                try
                                {
                                    ServerInfoUtlitities utlitities = new ServerInfoUtlitities();
                                    File.Delete(utlitities.APPL_PHYSICAL_PATH + str1[k].Images.ToString());
                                    File.Delete(utlitities.APPL_PHYSICAL_PATH + str1[k].ImagesSmall.ToString());
                                }
                                catch (Exception) { }
                            }
                            #endregion
                            SAlbum_Images.ALBUM_IMAGES_DELETE(e.CommandArgument.ToString());
                            this.UpdateList();
                            this.ltmsg.Text = "";
                        }
                        catch (Exception)
                        {
                            this.ltmsg.Text = "<span class=alert> : " + label("lt_youmustdeleteallitemsinthiscategoryfirst") + "</span>";
                        }
                    }
                    break;
                #endregion
            }
        }
        private void UpdateList()
        {
            try
            {
                List<Album_Images> table = SAlbum_Images.GET_DETAIL_BY_MENUALB(iphoto);
                this.rp_pagelist.DataSource = table;
                this.rp_pagelist.DataBind();
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
                        #region Xoa danh sach tin va anh
                        List<Album_Images> str = SAlbum_Images.GET_BY_ID(id.Value);
                        for (int k = 0; k < str.Count; k++)
                        {
                            try
                            {
                                ServerInfoUtlitities utlitities = new ServerInfoUtlitities();
                                File.Delete(utlitities.APPL_PHYSICAL_PATH + str[k].Images.ToString());
                                File.Delete(utlitities.APPL_PHYSICAL_PATH + str[k].ImagesSmall.ToString());
                            }
                            catch (Exception) { }
                        }
                        #endregion
                        SAlbum_Images.ALBUM_IMAGES_DELETE(id.Value);
                    }
                }
                UpdateList();
            }
            catch (Exception)
            {
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
            List<Album_Images> str5 = SAlbum_Images.GET_BY_ID(hdid.Value);
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

            SAlbum_Images.UPDATE_IMG(hdid.Value, "", "");
            this.UpdateList();
            ltimg.Text = "";
            this.pn_list.Visible = false;
            this.pn_insert.Visible = true;
        }

        protected void cmd_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (Ra_Upload.UploadedFiles.Count > 0)
                {
                    //Tạo thư mục foder theo danh mục của tin
                    String paths = "/Uploads/Album/" + DateTime.Now.Ticks.ToString();
                    foreach (Telerik.Web.UI.UploadedFile files in Ra_Upload.UploadedFiles)
                    {
                        string upfile = files.FileName.ToString();
                        String getfile = Database.ConverCodeUnisecond(files.GetNameWithoutExtension().Trim());
                        if ((upfile != null) && (System.IO.File.Exists(Server.MapPath(paths) + getfile + files.GetExtension()) == false))
                        {
                            files.SaveAs(Server.MapPath(paths) + getfile + files.GetExtension(), true);//820x468 chuẩn
                            int imageHeight = Convert.ToInt32(AllQuery.MoreAllBum.Height());
                            int imageWidth = Convert.ToInt32(AllQuery.MoreAllBum.Width());
                            Database.ResizeIamgesFix(Server.MapPath(paths) + getfile + files.GetExtension(), Server.MapPath(paths) + getfile + "_smallz" + files.GetExtension(), imageHeight, imageWidth);
                            //Gọi dữ liệu ra
                            Album_Images opictures = new Album_Images();
                            opictures.Menu_ALB = int.Parse(iphoto);
                            opictures.Menu_ID = int.Parse(ialb);
                            opictures.Title = files.GetFieldValue("Title");
                            opictures.Brief = files.GetFieldValue("Desc");
                            if (Database.RMIamges(Server.MapPath(paths), getfile, files.GetExtension(), 980) == true)
                            {
                                if (System.IO.File.Exists(Server.MapPath(paths) + getfile + files.GetExtension()) == true)
                                {
                                    System.IO.File.Delete(Server.MapPath(paths) + getfile + files.GetExtension());
                                }
                                opictures.Images = paths + getfile + "_Max" + files.GetExtension();
                            }
                            else
                            {
                                opictures.Images = paths + getfile + files.GetExtension();
                            }
                            opictures.ImagesSmall = paths + getfile + "_smallz" + files.GetExtension();
                            opictures.Status = Convert.ToInt32(ChAction.Checked);
                            opictures.Equals = 0;
                            opictures.Orders = int.Parse("1");
                            SAlbum_Images.ALBUM_IMAGES_INSERT(opictures);

                        }
                    }
                    this.UpdateList();
                    this.pn_list.Visible = true;
                    this.pn_insert.Visible = false;
                    this.hd_insertupdate.Value = "";
                    this.txt_title.Text = "";
                    this.txt_order.Text = "1";
                    this.txtvimg.Text = "";
                    this.hdFileName.Value = "";
                    ltimg.Text = "";
                    this.lblmsg.Text = "";
                    txtNoiDung.Text = "";
                }
            }
            catch (Exception)
            { }
        }

        protected void btUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileUpload.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {
                    int iUploadedCnt = 0;
                    int iFailedCnt = 0;
                    HttpFileCollection hfc = Request.Files;
                    lblFileList.Text = "Chọn <b>" + hfc.Count + "</b> file(s)";

                    if (hfc.Count <= 15) // 10 FILES GIỚI HẠN.
                    {
                        for (int i = 0; i <= hfc.Count - 1; i++)
                        {
                            HttpPostedFile hpf = hfc[i];
                            if (hpf.ContentLength > 0)
                            {
                                if (!File.Exists(Server.MapPath("/Uploads/Album/Multiupload/") + Path.GetFileName(hpf.FileName)))
                                {
                                    DirectoryInfo objDir = new DirectoryInfo(Server.MapPath("/Uploads/Album/Multiupload/"));
                                    string sFileName = Path.GetFileName(hpf.FileName);
                                    string sFileExt = Path.GetExtension(hpf.FileName);

                                    // KIỂM TRA kiếm file trùng lặp.
                                    FileInfo[] objFI = objDir.GetFiles(sFileName.Replace(sFileExt, "") + ".*");

                                    if (objFI.Length > 0)
                                    {
                                        // ĐÁNH DẤU NẾU FILE cùng tên tồn tại (bỏ qua các extentions).
                                        foreach (FileInfo file in objFI)
                                        {
                                            string sFileName1 = objFI[0].Name;
                                            string sFileExt1 = Path.GetExtension(objFI[0].Name);

                                            if (sFileName1.Replace(sFileExt1, "") == sFileName.Replace(sFileExt, ""))
                                            {
                                                iFailedCnt += 1;  // HÔNG CHO PHÉP SAO CHÉP.
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string path = "";
                                        path = "/Uploads/Album/Multiupload/" + DateTime.Now.Ticks.ToString();
                                        String UploadedFile = Path.GetFileName(hpf.FileName);
                                        int ExtractPos = UploadedFile.LastIndexOf("/") + 1;
                                        String UploadedFileName = UploadedFile.Substring(ExtractPos, UploadedFile.Length - ExtractPos);
                                        hpf.SaveAs(Server.MapPath("/Uploads/Album/Multiupload/") + DateTime.Now.Ticks.ToString() + Path.GetFileName(hpf.FileName));
                                        String imageUrl = UploadedFileName;
                                        int imageHeight = Convert.ToInt32(AllQuery.MoreAllBum.Height());
                                        int imageWidth = Convert.ToInt32(AllQuery.MoreAllBum.Width());
                                        imageUrl = path + imageUrl;
                                        System.Drawing.Image fullSizeImg = System.Drawing.Image.FromFile(Server.MapPath(imageUrl));
                                        System.Drawing.Image.GetThumbnailImageAbort dummyCallBack = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                                        System.Drawing.Image thumbNailImg = fullSizeImg.GetThumbnailImage(imageWidth, imageHeight, dummyCallBack, IntPtr.Zero);
                                        DateTime MyDate = DateTime.Now;
                                        String MyString = ".png";
                                        thumbNailImg.Save(Request.PhysicalApplicationPath + path + "small" + MyString, ImageFormat.Png);
                                        thumbNailImg.Dispose();
                                        //  ltname.Text += "<img src='" + path + UploadedFileName + "' /></br>";
                                        //  ltname2.Text += "<img src='" + path + "small" + MyString + "' /></br>";

                                        // SAVE THE FILE IN A FOLDER.
                                        // SAVE THE FILE IN A FOLDER.
                                        //hpf.SaveAs(Server.MapPath("/Uploads/Multiupload/") + Path.GetFileName(hpf.FileName));
                                        //ltname.Text += "<img src='" + "/Uploads/Multiupload/" + Path.GetFileName(hpf.FileName) + "' /></br>";
                                        //iUploadedCnt += 1;
                                        Album_Images obj = new Album_Images();
                                        #region MyRegion
                                        obj.Menu_ALB = int.Parse(iphoto);
                                        obj.Menu_ID = int.Parse(ialb);
                                        obj.Title = "";
                                        obj.Brief = "";
                                        obj.Images = path + UploadedFileName;
                                        obj.ImagesSmall = path + "small" + MyString;
                                        obj.Status = Convert.ToInt32(ChAction.Checked);
                                        obj.Equals = 0;
                                        obj.Orders = int.Parse("1");
                                        #endregion
                                        SAlbum_Images.ALBUM_IMAGES_INSERT(obj);
                                        this.UpdateList();
                                        this.pn_list.Visible = true;
                                        this.pn_insert.Visible = false;
                                        this.hd_insertupdate.Value = "";
                                        this.txt_title.Text = "";
                                        this.txt_order.Text = "1";
                                        this.txtvimg.Text = "";
                                        this.hdFileName.Value = "";
                                        ltimg.Text = "";
                                        this.lblmsg.Text = "";
                                        txtNoiDung.Text = "";
                                    }
                                }
                            }
                        }
                        lblUploadStatus.Text = "<b>" + iUploadedCnt + "</b> file(s) Uploaded.";
                        lblFailedStatus.Text = "<b>" + iFailedCnt + "</b> trùng lặp file (s) không thể được tải lên.";
                    }
                    else lblUploadStatus.Text = "Được phép đăng tối đa 15 ảnh lên 1 lần";
                }
                else lblUploadStatus.Text = "Không có tập tin được lựa chọn.";
            }
            catch (Exception) { lblUploadStatus.Text = "Có lỗi xảy ra. Bạn hãy thử lại."; }
        }

        protected void btmuitil_Click(object sender, EventArgs e)
        {
            pnmuitil.Visible = true;
        }

        protected void ltclose_Click(object sender, EventArgs e)
        {
            pnmuitil.Visible = false;
            lblUploadStatus.Text = "";
            lblFailedStatus.Text = "";
        }
    }
}

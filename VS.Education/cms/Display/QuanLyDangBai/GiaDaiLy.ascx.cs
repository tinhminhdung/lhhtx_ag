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

namespace VS.E_Commerce.cms.Display.QuanLyDangBai
{
    public partial class GiaDaiLy : System.Web.UI.UserControl
    {
        private string lang = Captionlanguage.Language;
        private string ipid = "-1";
        private string icid = "-1";
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["ID"] != null && !Request["ID"].Equals(""))
            {
                ipid = Request["ID"];
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
                this.ddlsoluongtu.Items.Clear();
                for (int i = 2; i < 500; i++)
                {
                    this.ddlsoluongtu.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }

                this.ddlsoluongden.Items.Clear();
                for (int i = 2; i < 500; i++)
                {
                    this.ddlsoluongden.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                if (MoreAll.MoreAll.GetCookies("Members") != "")
                {
                    user table = db.users.SingleOrDefault(p => p.vuserun == MoreAll.MoreAll.GetCookies("Members").ToString());
                    if (table != null)
                    {
                        hdthanhvien.Value = table.iuser_id.ToString();
                        this.UpdateList();
                        LoadItemsProduct();
                    }
                }
                else
                {
                    Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                }
            }
        }
        void LoadItemsProduct()
        {
            try
            {
                List<Entity.Products> dt = SProducts.Name_Text("SELECT * FROM [products] WHERE ipid = " + ipid + " and IDThanhVien=" + hdthanhvien.Value + "");
                if (dt.Count > 0)
                {
                    rpitems.DataSource = dt;
                    rpitems.DataBind();
                }
                else
                {
                    Response.Redirect("/quan-ly-san-pham.html");
                }

            }

            catch (Exception) { }
        }

        protected void btn_InsertUpdate_Click(object sender, EventArgs e)
        {
            if (this.txtgia.Text.Trim().Length < 0 && this.txt_order.Text.Trim().Length < 0)
            {
                this.lblmsg.Text = "Xin vui lòng kiểm tra dữ liệu đầu vào của bạn";
            }
            else if (!ValidateUtilities.IsValidInt(this.txt_order.Text.Trim()))
            {
                this.lblmsg.Text = "Thứ tự phải là kiểu số !";
            }
            else
            {

                GiaThanhVien obj = new GiaThanhVien();
                obj.IDSanPham = int.Parse(ipid);
                obj.SoLuongTu = ddlsoluongtu.SelectedValue;
                obj.SoLuongDen = ddlsoluongden.SelectedValue;
                obj.GiaDaiLy = txtgia.Text;
                obj.NgayTao = DateTime.Now;
                obj.Oders = int.Parse(txt_order.Text);
                db.GiaThanhViens.InsertOnSubmit(obj);
                db.SubmitChanges();

                //string str5 = this.hd_insertupdate.Value.Trim();
                //if (str5 != null)
                //{
                //    if (!(str5 == "update"))
                //    {
                //        if (str5 == "insert")
                //        {
                //            GiaThanhVien obj = new GiaThanhVien();
                //            obj.SoLuong = txtsoluong.Text;
                //            obj.GiaDaiLy = txtgia.Text;
                //            obj.NgayTao = DateTime.Now;
                //            obj.Oders = int.Parse(txt_order.Text);
                //            db.GiaThanhViens.InsertOnSubmit(obj);
                //            db.SubmitChanges();
                //        }
                //    }
                //    else
                //    {
                //        GiaThanhVien abc = db.GiaThanhViens.SingleOrDefault(p => p.ID == int.Parse(this.hd_page_edit_id.Value));
                //        abc.SoLuong = txtsoluong.Text;
                //        abc.GiaDaiLy = txtgia.Text;
                //        abc.Oders = int.Parse(txt_order.Text);
                //        db.SubmitChanges();
                //    }
                //}
                this.UpdateList();
                this.pn_list.Visible = true;
                this.pn_insert.Visible = false;
                this.hd_insertupdate.Value = "insert";

                this.txt_order.Text = "1";
                this.hdFileName.Value = "";
                this.lblmsg.Text = "";
                txt_order.Text = "1";
            }
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
            this.pn_list.Visible = false;
            this.pn_insert.Visible = true;
            this.hd_insertupdate.Value = "insert";
            this.hd_page_edit_id.Value = "-1";
            this.hdFileName.Value = "";
        }

        protected void rp_pagelist_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string str = e.CommandName.Trim();
            string str2 = e.CommandArgument.ToString().Trim();
            string str4 = str;
            switch (e.CommandName)
            {
                case "Delete":
                    GiaThanhVien del = db.GiaThanhViens.Where(s => s.ID == int.Parse(str2)).FirstOrDefault();// xóa 1
                    if (del != null)
                    {
                        db.GiaThanhViens.DeleteOnSubmit(del);
                        db.SubmitChanges();
                    }
                    this.UpdateList();
                    this.ltmsg.Text = "";
                    break;
                case "EditDetail":
                    this.btn_InsertUpdate.Text = "Cập nhật";
                    this.pn_list.Visible = false;
                    this.pn_insert.Visible = true;
                    this.hd_insertupdate.Value = "update";
                    this.hd_page_edit_id.Value = str2.Trim();
                    GiaThanhVien table = db.GiaThanhViens.SingleOrDefault(p => p.ID == int.Parse(str2));
                    if (table != null)
                    {
                        this.hdid.Value = table.IDSanPham.ToString().Trim();
                        // this.txtsoluong.Text = table.SoLuong.ToString().Trim();
                        this.txtgia.Text = table.GiaDaiLy.ToString().Trim();
                        this.txt_order.Text = table.Oders.ToString().Trim();
                    }
                    break;
            }
        }

        private void UpdateList()
        {
            List<GiaThanhVien> table = db.GiaThanhViens.Where(s => s.IDSanPham == int.Parse(ipid)).OrderBy(s => s.Oders).ToList();
            if (table != null)
            {
                this.rp_pagelist.DataSource = table;
                this.rp_pagelist.DataBind();
            }
        }

        //protected void txtsoluong_TextChanged(object sender, EventArgs e)
        //{
        //    TextBox Tieude = (TextBox)sender;
        //    var b = (HiddenField)Tieude.FindControl("hiID");
        //    #region UpdatePrice
        //    if (Tieude.Text.Length > 0)
        //    {
        //        GiaThanhVien item = db.GiaThanhViens.SingleOrDefault(p => p.ID == int.Parse(b.Value));
        //        if (item != null)
        //        {
        //            item.SoLuong = Tieude.Text.Replace(".", "").Replace(",", "");
        //            db.SubmitChanges();
        //            UpdateList();
        //            this.ltmsg.Text = "<span class='alerts'>Cập nhật Số lượng thành công !!</span>";
        //        }
        //    }
        //    else
        //    {
        //        ltmsg.Text = "<span class='alerts'>Bạn chưa nhập số lượng !!</span>";
        //    }
        //    #endregion

        //}
        protected void txtgia_TextChanged(object sender, EventArgs e)
        {
            TextBox Tieude = (TextBox)sender;
            var b = (HiddenField)Tieude.FindControl("hiID");
            #region UpdatePrice
            if (Tieude.Text.Length > 0)
            {
                GiaThanhVien item = db.GiaThanhViens.SingleOrDefault(p => p.ID == int.Parse(b.Value));
                if (item != null)
                {
                    item.GiaDaiLy = Tieude.Text.Replace(".", "").Replace(",", "");
                    db.SubmitChanges();
                    UpdateList();
                    this.ltmsg.Text = "<span class='alerts'>Cập nhật Giá thành công !!</span>";
                }
            }
            else
            {
                ltmsg.Text = "<span class='alerts'>Bạn chưa nhập Giá !!</span>";
            }
            #endregion

        }
        protected void txtthutu_TextChanged(object sender, EventArgs e)
        {
            TextBox Tieude = (TextBox)sender;
            var b = (HiddenField)Tieude.FindControl("hiID");
            #region UpdatePrice
            if (Tieude.Text.Length > 0)
            {
                GiaThanhVien item = db.GiaThanhViens.SingleOrDefault(p => p.ID == int.Parse(b.Value));
                if (item != null)
                {
                    item.Oders = int.Parse(Tieude.Text.Replace(".", "").Replace(",", ""));
                    db.SubmitChanges();
                    UpdateList();
                    this.ltmsg.Text = "<span class='alerts'>Cập nhật Thứ Tự thành công !!</span>";
                }
            }
            else
            {
                ltmsg.Text = "<span class='alerts'>Bạn chưa nhập Thứ Tự !!</span>";
            }
            #endregion

        }
        protected void rp_pagelist_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            for (int i = 0; i < rp_pagelist.Items.Count; i++)
            {
                DropDownList ddlsoluongden = (DropDownList)rp_pagelist.Items[i].FindControl("ddlsoluongden");
                HiddenField id = (HiddenField)rp_pagelist.Items[i].FindControl("hdSoLuongDen");
                if (id.Value != "")
                {
                    ddlsoluongden.SelectedValue = id.Value;
                }

                DropDownList ddlsoluongtu = (DropDownList)rp_pagelist.Items[i].FindControl("ddlsoluongtu");
                HiddenField id1 = (HiddenField)rp_pagelist.Items[i].FindControl("hdSoLuongTu");
                if (id1.Value != "")
                {
                    ddlsoluongtu.SelectedValue = id1.Value;
                }
            }
        }
        protected void ddlsoluongden_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddltrangthai = (DropDownList)sender;
            var id = (HiddenField)ddltrangthai.FindControl("hiID");
            GiaThanhVien abc = db.GiaThanhViens.SingleOrDefault(p => p.ID == int.Parse(id.Value));
            abc.SoLuongDen = ddltrangthai.SelectedValue;
            db.SubmitChanges();
            lblmsg.Text = "Cập nhật Số Lượng Đến thành công !!";
            UpdateList();
        }

        protected void ddlsoluongtu_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddltrangthai = (DropDownList)sender;
            var id = (HiddenField)ddltrangthai.FindControl("hiID");
            GiaThanhVien abc = db.GiaThanhViens.SingleOrDefault(p => p.ID == int.Parse(id.Value));
            abc.SoLuongTu = ddltrangthai.SelectedValue;
            db.SubmitChanges();
            lblmsg.Text = "Cập nhật Số Lượng Từ thành công !!";
            UpdateList();
        }
    }
}
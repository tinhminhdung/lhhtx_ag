using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Services;
using Entity;
using System.Text;
using VS.E_Commerce.Application;

namespace VS.E_Commerce.cms.Admin.Member
{
    public partial class Members : System.Web.UI.UserControl
    {
        private string status = "-1";
        private string IDThanhVien = "0";
        private string lang = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        public string ShowThem = "0";
        public string ChucNang = "0";
        DateTime fDate, tDate;
        public string DuyetKichHoat = "0";

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
                status = Request["st"];
            }
            if (MoreAll.MoreAll.GetCookie("URole") != null)
            {
                string[] strArray = MoreAll.MoreAll.GetCookie("URole").ToString().Trim().Split(new char[] { '|' });
                if (strArray.Length > 0)
                {
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        if (strArray[i].ToString().Equals("23"))
                        {
                            ShowThem = "1";
                        }
                        if (strArray[i].ToString().Equals("24"))
                        {
                            ChucNang = "1";
                        }
                        if (strArray[i].ToString().Equals("25"))
                        {
                            DuyetKichHoat = "1";
                        }

                    }
                }
            }


            this.Page.Form.DefaultButton = lnksearch.UniqueID;
            if (!base.IsPostBack)
            {
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
                if (Request["khoa"] != null && !Request["khoa"].Equals(""))
                {
                    ddkhoathanhvien.SelectedValue = Request["khoa"];
                }
                if (Request["chinhanh"] != null && !Request["chinhanh"].Equals(""))
                {
                    ddlchinhanh.SelectedValue = Request["chinhanh"];
                }
                if (Request["thanhvienkh"] != null && !Request["thanhvienkh"].Equals(""))
                {
                    ddlthanhvien.SelectedValue = Request["thanhvienkh"];
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
                if (Request["IDThanhVien"] != null && !Request["IDThanhVien"].Equals(""))
                {
                    IDThanhVien = Request["IDThanhVien"];
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
                this.LoadItems();
            }
        }
        protected void btndisplay_Click(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }
        protected void lnksearch_Click(object sender, EventArgs e)
        {
            LoadItems();
            LoadRequest();
        }
        protected void Delete_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn có muốn xóa thành viên này?')";
        }
        protected bool EnableLock(string status)
        {
            return status.Equals("1");
        }

        protected bool EnablecUnLock(string status)
        {
            return status.Equals("1");
        }
        protected bool EnablecLock(string status)
        {
            return status.Equals("2");
        }
        protected bool EnableUnLock(string status)
        {
            return status.Equals("0");
        }
        protected string ShowNCC(string status)
        {
            if (status.Equals("2"))
            {
                return "display:block";
            }
            return "display:none";
        }

        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
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
        public void LoadItems()
        {
          


            int Tongsobanghi = 0;
            Int16 pages = 1;
            int Tongsotrang = int.Parse("30");
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

            if (Commond.Check(txtNgayThangNam.Text))
                fDate = Commond.ConvertStringToDate(txtNgayThangNam.Text, "dd/MM/yyyy");
            if (Commond.Check(txtDenNgayThangNam.Text))
                tDate = Commond.ConvertStringToDate(txtDenNgayThangNam.Text, "dd/MM/yyyy");

            if (txtNgayThangNam.Text != "" && txtDenNgayThangNam.Text != "")
            {
                sql1 += " AND dcreatedate IS NOT NULL AND ((DATEADD(dd,-31,dcreatedate)>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000' OR dcreatedate>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000') AND dcreatedate <='" + Commond.FormatDate(tDate.Date) + " 23:59:59.999')";
            }
            else if (txtNgayThangNam.Text == "" && txtDenNgayThangNam.Text != "")
            {
                sql1 += " AND dcreatedate IS NOT NULL AND dcreatedate <='" + Commond.FormatDate(tDate.Date) + " 23:59:59.999'";
            }
            else if (txtNgayThangNam.Text != "" && txtDenNgayThangNam.Text == "")
            {
                sql1 += " AND dcreatedate IS NOT NULL AND (DATEADD(dd,-31,dcreatedate)>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000' OR dcreatedate>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000')";
            }

            if (ddlthanhvien.SelectedValue == "0")
            {
                sql1 += " and DuyetTienDanap=0 ";
            }
            if (ddlthanhvien.SelectedValue == "1")
            {
                sql1 += " and DuyetTienDanap=1 ";
            }
            if (ddlthanhvien.SelectedValue == "2")
            {
                sql1 += " and CuaHang=1 ";
            }
            if (ddkhoathanhvien.SelectedValue == "0")
            {
                sql1 += " and istatus=0 ";
            }
            if (ddkhoathanhvien.SelectedValue == "1")
            {
                sql1 += " and istatus=1 ";
            }
            string sapxep = "";
            if (ddlordertype.SelectedValue == "desc")
            {
                sapxep = " ORDER BY dcreatedate desc  ";
            }
            else
            {
                sapxep = " ORDER BY dcreatedate asc  ";
            }

            List<Entity.TongSo> iitem = Susers.CATEGORY_PHANTRANG1(IDThanhVien, txtkeyword.Text.Trim().Replace("&nbsp;", ""), ddlchinhanh.SelectedValue, ddlstatus.SelectedValue, ddltheoLead.SelectedValue, ddlcapdo.SelectedValue, sql1);
            if (iitem.Count() > 0)
            {
                Tongsobanghi = iitem[0].Tong;
                lttongtb.Text = iitem[0].Tong.ToString();
            }
            List<Entity.users> dt = Susers.CATEGORY_PHANTRANG2(IDThanhVien, txtkeyword.Text.Trim().Replace("&nbsp;", ""), ddlchinhanh.SelectedValue, ddlstatus.SelectedValue, ddltheoLead.SelectedValue, ddlcapdo.SelectedValue, sql1, sapxep, (pages - 1), Tongsotrang);
            if (dt.Count >= 1)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }

            if (Tongsobanghi % Tongsotrang > 0)
            {
                Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
            }
            else
            {
                Tongsobanghi = Tongsobanghi / Tongsotrang;
            }
            ltpage.Text = Commond.PhantrangAdmin("/admin.aspx?u=Thanhvien&thanhvienkh=" + ddlthanhvien.SelectedValue + "&khoa=" + ddkhoathanhvien.SelectedValue + "&st=" + ddlstatus.SelectedValue + "&us=" + ddlorderby.SelectedValue + "&ds=" + ddlordertype.SelectedValue + "&kw=" + txtkeyword.Text + "&chinhanh=" + ddlchinhanh.SelectedValue + "&Lead=" + ddltheoLead.SelectedValue + "&sao=" + ddlcapdo.SelectedValue + "&kieuthanhvien=" + ddlkieuthanhvien.SelectedValue + "&AgLand=" + ddlAgLand.SelectedValue + "&uutien=" + ddluutien.SelectedValue + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "", Tongsobanghi, pages);
        }


        protected void TinPhanTram(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bắt đầu tính % Hoa Hồng cho: Thành viên giới thiệu, Lead, Chi nhánh')";
        }
        protected void Lock_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn c\x00f3 muốn kh\x00f3a t\x00e0i khoản n\x00e0y?')";
        }
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string str = e.CommandName.Trim();
            string str2 = e.CommandArgument.ToString().Trim();
            string str4 = str;
            switch (e.CommandName)
            {
                case "delete":
                    user abt = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(e.CommandArgument.ToString().Trim()));
                    if (abt.ChiNhanh == 1)
                    {
                        ltthongbao.Text = "Không thể xóa thành viên này khi đang là chi nhánh";
                    }
                    else
                    {
                        Susers.DELETE(e.CommandArgument.ToString().Trim());
                        this.LoadItems();
                        base.Response.Redirect(base.Request.Url.ToString().Trim());
                    }
                    return;

                case "lock":
                    this.UpdateStatus(e.CommandArgument.ToString().Trim(), "0");
                    this.LoadItems();
                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    return;
                case "unlock":
                    this.UpdateStatus(e.CommandArgument.ToString().Trim(), "1");
                    this.LoadItems();
                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    return;
                case "ChangeStatus":
                    user abc = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(e.CommandArgument.ToString().Trim()));
                    if (abc.Leader == 0)
                    {
                        Susers.Name_Text("update users set Leader=1 where iuser_id=" + e.CommandArgument.ToString().Trim() + "");
                    }
                    else if (abc.Leader == 1)
                    {
                        Susers.Name_Text("update users set Leader=0 where iuser_id=" + e.CommandArgument.ToString().Trim() + "");
                    }
                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    return;
                case "ChangeAGLANG":
                    user abc5 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(e.CommandArgument.ToString().Trim()));
                    if (abc5.ThanhVienAgLang == 0)
                    {
                        Susers.Name_Text("update users set ThanhVienAgLang=1 where iuser_id=" + e.CommandArgument.ToString().Trim() + "");
                    }
                    else if (abc5.ThanhVienAgLang == 1)
                    {
                        Susers.Name_Text("update users set ThanhVienAgLang=0 where iuser_id=" + e.CommandArgument.ToString().Trim() + "");
                    }
                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    return;

                case "ChangeCuaHang":
                    user abc56 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(e.CommandArgument.ToString().Trim()) && p.DuyetTienDanap == 1);
                    if (abc56 != null)
                    {
                        if (abc56.CuaHang == 0)
                        {
                            Susers.Name_Text("update users set CuaHang=1 where iuser_id=" + e.CommandArgument.ToString().Trim() + "");
                        }
                        else if (abc56.CuaHang == 1)
                        {
                            Susers.Name_Text("update users set CuaHang=0 where iuser_id=" + e.CommandArgument.ToString().Trim() + "");
                        }
                        base.Response.Redirect(base.Request.Url.ToString().Trim());
                    }
                    else
                    {
                        WebMsgBox.Show("Yêu cầu thành viên này phải kích hoạt trở thành đại lý trước khi kích hoạt cửa hàng.");
                    }
                    return;
                case "ChangeTatChucNang":
                    user abctcn = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(e.CommandArgument.ToString().Trim()));
                    if (abctcn.TatChucNang == 0)
                    {
                        Susers.Name_Text("update users set TatChucNang=1 where iuser_id=" + e.CommandArgument.ToString().Trim() + "");
                    }
                    else if (abctcn.TatChucNang == 1)
                    {
                        Susers.Name_Text("update users set TatChucNang=0 where iuser_id=" + e.CommandArgument.ToString().Trim() + "");
                    }
                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    return;
                case "ChangeQRCode":
                    user abc55 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(e.CommandArgument.ToString().Trim()));
                    if (abc55.TrangThaiThamGiaQRCode == 0)
                    {
                        Susers.Name_Text("update users set TrangThaiThamGiaQRCode=1 where iuser_id=" + e.CommandArgument.ToString().Trim() + "");
                    }
                    else if (abc55.TrangThaiThamGiaQRCode == 1)
                    {
                        Susers.Name_Text("update users set TrangThaiThamGiaQRCode=0 where iuser_id=" + e.CommandArgument.ToString().Trim() + "");
                    }
                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    return;
                case "ThanhVienNhaCungCap":
                    user abc1 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(e.CommandArgument.ToString().Trim()));
                    if (abc1.Type == 2)
                    {
                        Susers.Name_Text("update users set Type=1 where iuser_id=" + e.CommandArgument.ToString().Trim() + "");
                    }
                    else if (abc1.Type == 1)
                    {
                        Susers.Name_Text("update users set Type=2 where iuser_id=" + e.CommandArgument.ToString().Trim() + "");
                    }
                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    //this.LoadItems();
                    return;

                case "ChangeUutien":
                    user abc5v = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(e.CommandArgument.ToString().Trim()));
                    if (abc5v.Uutien == 0)
                    {
                        Susers.Name_Text("update users set Uutien=1 where iuser_id=" + e.CommandArgument.ToString().Trim() + "");
                    }
                    else if (abc5v.Uutien == 1)
                    {
                        Susers.Name_Text("update users set Uutien=0 where iuser_id=" + e.CommandArgument.ToString().Trim() + "");
                    }
                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    return;

                case "ChangeChiNhanh":
                    user abcv = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(e.CommandArgument.ToString().Trim()));
                    if (abcv.ChiNhanh == 1)
                    {
                        Susers.Name_Text("update users set ChiNhanh=0 where iuser_id=" + e.CommandArgument.ToString().Trim() + "");
                    }
                    else if (abcv.ChiNhanh == 0)
                    {
                        Susers.Name_Text("update users set ChiNhanh=1 where iuser_id=" + e.CommandArgument.ToString().Trim() + "");
                    }
                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    return;

                case "Changeiregionid":
                    user vvvg = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(e.CommandArgument.ToString().Trim()));
                    if (vvvg.iregionid == 1)
                    {
                        Susers.Name_Text("update users set iregionid=0 where iuser_id=" + e.CommandArgument.ToString().Trim() + "");
                    }
                    else if (vvvg.iregionid == 0)
                    {
                        Susers.Name_Text("update users set iregionid=1 where iuser_id=" + e.CommandArgument.ToString().Trim() + "");
                    }
                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    return;

                case "DuyetKichHoat":
                    //Susers.Name_Text("update users set DuyetTienDanap=1 where iuser_id=" + e.CommandArgument.ToString().Trim() + " ");
                    #region Cập nhật ngày kích hoạt 1 năm để kiểm soát
                    Commond.SetLichSuKichHoat(e.CommandArgument.ToString().Trim(), "Admin Kích");
                    #endregion

                    base.Response.Redirect(base.Request.Url.ToString().Trim());
                    return;

                case "CapNhatQRCode":
                    RepeaterItem item = e.Item;
                    TextBox txtChietkauQRCode = (TextBox)item.FindControl("txtChietkauQRCode");
                    TextBox txthhnguoimuaQRCode = (TextBox)item.FindControl("txthhnguoimuaQRCode");
                    // TextBox txthhhethongQRCode = (TextBox)item.FindControl("txthhhethongQRCode");
                    if (Test(txtChietkauQRCode.Text.Trim(), txthhnguoimuaQRCode.Text.Trim()) == true)
                    {
                        // Cập nhật thứ tự
                        if (txtChietkauQRCode.Text != "" && txtChietkauQRCode.Text != "0")//&& txthhnguoimuaQRCode.Text != "" && txthhnguoimuaQRCode.Text != "0" && txthhhethongQRCode.Text != "" && txthhhethongQRCode.Text != "0"
                        {
                            Double TongChietKhauNGuoiMua = Convert.ToDouble(txthhnguoimuaQRCode.Text.Trim());
                            Double TongChietKhau = 100 - TongChietKhauNGuoiMua;

                            #region LichSuQRCode
                            LichSuQRCode obj = new LichSuQRCode();
                            obj.IDThanhVien = int.Parse(e.CommandArgument.ToString().Trim());
                            obj.ChietKhauHH = txtChietkauQRCode.Text.Trim();
                            obj.HHNGuoiMua = txthhnguoimuaQRCode.Text.Trim();
                            obj.HHHeThong = TongChietKhau.ToString();// txthhhethongQRCode.Text.Trim();
                            obj.NguoiDuyet = "Admin : " + MoreAll.MoreAll.GetCookies("UName").ToString();
                            obj.NgayDuyet = DateTime.Now;
                            db.LichSuQRCodes.InsertOnSubmit(obj);
                            db.SubmitChanges();
                            #endregion
                            Susers.Name_Text("update users set QRCodeChietKhauHH='" + txtChietkauQRCode.Text + "',QRCodeHHNguoiMua='" + txthhnguoimuaQRCode.Text + "',QRCodeHHHeThong='" + TongChietKhau + "' WHERE iuser_id=" + e.CommandArgument.ToString().Trim() + "");

                            this.ltthongbao.Text = "<span class=alert>Cập nhật % hoa hồng QRCode thành công !!</span>";
                        }
                        else
                        {
                            ltthongbao.Text = "<span class=alert>Bạn chưa nhập % hoa hồng QRCode !!</span>";
                        }
                        base.Response.Redirect(base.Request.Url.ToString().Trim());
                    }
                    return;
            }
        }
        public static string TimLeader(string id)
        {
            string str = "0";
            List<Entity.users> dt = Susers.Name_Text("select top 1 * from users  where iuser_id=" + id + " ");
            if (dt.Count > 0)
            {
                if (dt[0].Leader.ToString() == "1")
                {
                    return dt[0].iuser_id.ToString();
                }
                else
                {
                    str = dt[0].GioiThieu.ToString();
                    return TimLeader(str);
                }
            }
            return str;
        }
        protected bool Test(string ChieKhau, string NguoiMua)
        {
            Double TongChieKhau = Convert.ToDouble(ChieKhau);
            Double TongNguoiMua = Convert.ToDouble(NguoiMua);
            //  Double TongHeThong = Convert.ToDouble(HeThong);

            Double Tong1 = Convert.ToDouble("0");
            Double Tong = Convert.ToDouble("100");

            if (TongChieKhau <= Tong1 || TongChieKhau >= Tong)
            {
                Response.Write("<script type=\"text/javascript\">alert('Giá trị nhập % chiết khấu trong khoảng từ 0 đến 99!');window.location.href='" + Request.Url.ToString().Trim() + "';</script>"); return false;
            }
            if (TongNguoiMua <= Tong1 || TongNguoiMua >= Tong)
            {
                Response.Write("<script type=\"text/javascript\">alert('Giá trị nhập % chiết khấu người mua trong khoảng từ 0 đến 99!');window.location.href='" + Request.Url.ToString().Trim() + "';</script>"); return false;
            }
            //if (TongHeThong <= Tong1 || TongHeThong >= Tong)
            //{
            //    Response.Write("<script type=\"text/javascript\">alert('Giá trị nhập % chiết khấu hệ thống trong khoảng từ 0 đến 99 !');window.location.href='" + Request.Url.ToString().Trim() + "';</script>"); return false;
            //}
            return true;
        }

        protected string Status(string status)
        {
            if (status.Equals("1"))
            {
                return "Đang hoạt động";
            }
            return "Đ\x00e3 kh\x00f3a";
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
        protected string Enablechon(string chon)
        {
            if (chon.Equals("1"))
            {
                return "<span style='font-size: 12px;'>Thành viên</span>";
            }
            return "<span style='font-size: 12px;'>Nội bộ</span>";
        }
        private void UpdateStatus(string un, string status)
        {
            Susers.UPDATE_STATUS(un, status);
        }
        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }

        protected string ShowThanhVien(string id)
        {
            string str = "";
            List<Entity.users> dt = Susers.GET_BY_ID(id);
            if (dt.Count >= 1)
            {
                str += "<span id=" + dt[0].iuser_id.ToString() + " style=\" color:red\">";
                if (dt[0].vfname.ToString().Length > 0)
                {
                    str += dt[0].vfname;
                }
                str += "</span>";
                if (dt[0].vphone.ToString().Length > 0)
                {
                    str += " - " + dt[0].vphone;
                }
            }
            return str;
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
        protected string ShowChiNhanh(string id)
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.GETBYID(id);
            if (dt.Count >= 1)
            {
                str = dt[0].Name;
            }
            return str;
        }

        //protected string ShowNameChiNhanh(string ChiNhanh, string id)
        //{
        //    if (ChiNhanh == "1")
        //    {
        //        List<Entity.Menu> dt = SMenu.Name_Text("SELECT * FROM Menu where capp='DL' and Type=" + id + "");
        //        if (dt.Count >= 1)
        //        {
        //            return dt[0].Name;
        //        }
        //    }
        //    return "";
        //}

        protected void btxoa_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < Repeater1.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)Repeater1.Items[i].FindControl("chkid");
                    HiddenField id = (HiddenField)Repeater1.Items[i].FindControl("hiID");
                    if (chk.Checked)
                    {
                        user abc = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(id.Value));
                        if (abc.ChiNhanh == 1)
                        {
                            ltthongbao.Text = "Không thể xóa thành viên này khi đang là chi nhánh";
                        }
                        else
                        {
                            Susers.DELETE(id.Value);
                            LoadItems();
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        protected void ddlchinhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }
        protected string Showsanpham(string kieu, string id, string TongSoSanPham)
        {
            string str = "";
            if (kieu == "2")
            {
                str = "<a href=\"/admin.aspx?u=pro&su=items&IDThanhVien=" + id + "\" target=\"_blank\">Có (" + TongSoSanPham + ") Sản phẩm</a><br>";
            }
            return str;
        }
        protected string ShowTongTienDanapCoin(string chon)
        {
            if (!chon.Equals("0"))
            {
                return chon + " Điểm";
            }
            return " <span style='color:#d55449'>Chưa kích hoạt</span> ";
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
                    str += "<a target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&IDThanhVien=" + dt[0].iuser_id.ToString() + "\"><span style='color:red'>" + dt[0].vfname + "</span></a>";
                }
                str += "</span>";
            }
            return str;
        }
        protected string ShowDienmDuocCap(string IDThanhVien)
        {
            var dt = db.S_CapDiemThanhViens_ThongKe(Convert.ToInt32(IDThanhVien)).ToList();
            if (dt.Count() >= 0)
            {
                if (dt[0].sodiem.ToString() != "")
                {
                    return dt[0].sodiem.ToString();
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }

        protected void lnkxuatExel_Click(object sender, EventArgs e)
        {
            string sql1 = "";

            if (ddlkieuthanhvien.SelectedValue != "-1")
            {
                sql1 += " and Type=" + ddlkieuthanhvien.SelectedValue + " ";
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

            if (Commond.Check(txtNgayThangNam.Text))
                fDate = Commond.ConvertStringToDate(txtNgayThangNam.Text, "dd/MM/yyyy");
            if (Commond.Check(txtDenNgayThangNam.Text))
                tDate = Commond.ConvertStringToDate(txtDenNgayThangNam.Text, "dd/MM/yyyy");

            if (txtNgayThangNam.Text != "" && txtDenNgayThangNam.Text != "")
            {
                sql1 += " AND dcreatedate IS NOT NULL AND ((DATEADD(dd,-31,dcreatedate)>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000' OR dcreatedate>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000') AND dcreatedate <='" + Commond.FormatDate(tDate.Date) + " 23:59:59.999')";
            }
            else if (txtNgayThangNam.Text == "" && txtDenNgayThangNam.Text != "")
            {
                sql1 += " AND dcreatedate IS NOT NULL AND dcreatedate <='" + Commond.FormatDate(tDate.Date) + " 23:59:59.999'";
            }
            else if (txtNgayThangNam.Text != "" && txtDenNgayThangNam.Text == "")
            {
                sql1 += " AND dcreatedate IS NOT NULL AND (DATEADD(dd,-31,dcreatedate)>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000' OR dcreatedate>='" + Commond.FormatDate(fDate.Date) + " 00:00:00.000')";
            }

            string sapxep = "";
            if (ddlordertype.SelectedValue == "desc")
            {
                sapxep = " ORDER BY dcreatedate desc  ";
            }
            else
            {
                sapxep = " ORDER BY dcreatedate asc  ";
            }



            string Namefile = "DanhSachThanhVien";
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename=" + Namefile + ".xls");
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
            StringBuilder sb = new StringBuilder();

            sb.Append(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            sb.Append("<table border='1' bgcolor='#ffffff' bordercolor='#dedede' cellspacing='0' cellpadding='0' style='font-size:12px; font-family:Arial; background:white;'>");
            sb.Append("<tr>");
            sb.Append("  <th style=\"width:50px; vertical-align:middle; height: 22px;\">");
            sb.Append("    <b>STT</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:520px; vertical-align:middle;\">");
            sb.Append("    <b>Tên Đăng Nhập</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Họ và tên</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:400px; vertical-align:middle;\">");
            sb.Append("    <b>Địa chỉ</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>Điện thoại</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Email</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>AG Land</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Kích Hoạt</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>QRCode</b>");
            sb.Append("  </th>");

            //sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            //sb.Append("    <b>Ví AFF</b>");
            //sb.Append("  </th>");
            //sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            //sb.Append("    <b>Ví Thương mại</b>");
            //sb.Append("  </th>");
            sb.Append("</tr>");
            List<Entity.users> dt = Susers.EXel(IDThanhVien, txtkeyword.Text.Replace("&nbsp;", ""), ddlchinhanh.SelectedValue, ddlstatus.SelectedValue, ddltheoLead.SelectedValue, ddlcapdo.SelectedValue, sql1, sapxep);
            // List<Entity.users> dt = Susers.Name_Text("SELECT  * FROM  users ORDER BY iuser_id DESC");
            if (dt.Count > 0)
            {
                int i = 1;
                foreach (var item in dt)
                {
                    sb.Append("<tr>");
                    sb.Append("    <td style=\"width:50px; vertical-align:middle; height: 22px; text-align: center;\">" + i++ + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + item.vuserun + "</td>");
                    sb.Append("    <td style=\"width:400px; vertical-align:middle; text-align: left;\">" + item.vfname + "</td>");
                    sb.Append("    <td style=\"width:250px; vertical-align:middle;text-align: left;\">" + item.vaddress + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + item.vphone + "</td>");
                    sb.Append("    <td style=\"width:520px; vertical-align:middle; text-align: left;\">" + item.vemail + "</td>");
                    if (item.ThanhVienAgLang == 0)
                    {
                        sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: left;\"></td>");
                    }
                    else
                    {
                        sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: left;\">Là thành viên AG Land</td>");
                    }
                    if (item.DuyetTienDanap == 0)
                    {
                        sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: left;\">Chưa kích hoạt</td>");
                    }
                    else
                    {
                        sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: left;\">Đã Kích Hoạt</td>");
                    }
                    if (item.TrangThaiThamGiaQRCode == 0)
                    {
                        sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: left;\">Chưa kích hoạt</td>");
                    }
                    else
                    {
                        sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: left;\">Đã Kích Hoạt</td>");
                    }
                    sb.Append("</tr>");
                }
            }
            sb.Append("</table>");
            Response.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }


        protected string Quantity(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                str += dt[0].Quantity.ToString();
            }
            return str.ToString();
        }
        protected string Code(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                str += dt[0].Code.ToString();
            }
            return str.ToString();
        }
        protected string Name(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                str += dt[0].Name.ToString();
            }
            return str.ToString();
        }


        // chi tiết giỏ hàng


        protected string Anh(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                str += dt[0].Images.ToString();
            }
            return str.ToString();
        }
        protected string Codes(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                str += dt[0].Code.ToString();
            }
            return str.ToString();
        }
        protected string GiaNhap1(string id)
        {
            string str = "0";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                if (dt[0].Giacongtynhapvao.Length > 0)
                {
                    str = AllQuery.MorePro.Detail_Price(dt[0].Giacongtynhapvao.ToString());
                }
            }
            return str.ToString();
        }
        protected string GiaNhap(string id, string quantity)
        {
            string str = "0";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                if (dt[0].Giacongtynhapvao.Length > 0)
                {
                    Double Tongtien = Convert.ToInt32(quantity) * Convert.ToDouble(dt[0].Giacongtynhapvao.ToString());
                    return AllQuery.MorePro.Detail_Price(Tongtien.ToString());
                }
            }
            return str.ToString();
        }
        protected string Kho(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                str = dt[0].Quantity.ToString();
            }
            return str.ToString();
        }
        protected string Kichthuoc(string id)
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.GETBYID(id);
            if (dt.Count > 0)
            {
                str += dt[0].Name.ToString();
            }
            return str.ToString();
        }
        protected string Mausac(string id)
        {
            string str = "";
            List<Entity.Menu> dt = SMenu.GETBYID(id);
            if (dt.Count > 0)
            {
                str += "<img src=\"" + dt[0].Images.ToString() + "\" style=\"width:28px; height:28px;border:solid 1px #d7d7d7\" />";
            }
            return str.ToString();
        }

        protected string FormatMoneyDiemMuaHang(string Money)
        {
            try
            {
                double TongCoin = Convert.ToDouble(Money.ToString());
                double Tong = (TongCoin) / 1000;
                return Tong.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        protected string HoaHongTheoLevel_TheoThoiDiemMuahang(string IDCart, string ID, string Tongd, string IDThanhVien)
        {
            #region Show % Hoa Hồng theo thời điểm mua hàng (Lịch sử)
            //Xem thành viên đang ở level nào rồi nhân với level đó
            List<Carts> table2 = SCarts.Carts_GetById(IDCart.ToString());
            if (table2.Count > 0)
            {
                // Nếu đã đặt hàng thành công xong rồi trạng thái status =1 thì sẽ phải lấy HoaHongTheoLevel trong bảng cartdetail để làm căn cứ lịch sử lúc mua tại thời điểm đó chỉ dc bằng đó %
                if (table2[0].Status == 1)
                {
                    List<CartDetail> table = db.CartDetails.Where(p => p.ID == int.Parse(ID.ToString())).ToList();
                    if (table != null)
                    {
                        string CapoLevelHoaHongs = (table[0].HoaHongTheoLevel.ToString());
                        double TongCoin = Convert.ToDouble(Tongd);

                        double HoaHongs = Convert.ToDouble(CapoLevelHoaHongs);
                        double Tong = (TongCoin * HoaHongs) / 100;
                        return Tong.ToString();
                    }
                }
                else
                {
                    // Nếu chưa đặt hàng thì trọc vào bảng thành viên lấy hoa hồng hiện tại thành viên đang ở level mấy  ra nhé
                    user tables = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(IDThanhVien.ToString()));// Type=1 là thành viên  hoặc là ng mua hàng , type=2 là nhà cung cấp
                    if (tables != null)
                    {
                        string CapoLevelHoaHongs = CapoLevelHoaHong(tables.LevelThanhVien.ToString());
                        double HoaHongs = Convert.ToDouble(CapoLevelHoaHongs);
                        double TongCoin = Convert.ToDouble(Tongd);
                        double Tong = (TongCoin * HoaHongs) / 100;
                        return Tong.ToString();
                    }
                }
            }
            #endregion
            return "";
        }
        protected string CapoLevelHoaHong(string level)
        {
            List<Entity.Menu> cdd = SMenu.Name_Text("SELECT * FROM Menu where capp='" + More.LV + "'  and Views=" + level + " and lang='VIE' order by level,Orders asc");
            if (cdd != null)
            {
                return cdd[0].Noidung1.ToString();// ID chính là thuộc cấp độ mấy do tiêu đề ghi
            }
            return "0";
        }

        protected string Soluong(string ID_Cart)
        {
            string totalvnd = "0";
            List<Entity.CartDetail> cartdetail = SCartDetail.Detail_ID_Cart(ID_Cart);
            if (cartdetail.Count > 0)
            {
                if (cartdetail.Count > 0)
                {
                    double num = 0.0;
                    for (int i = 0; i < cartdetail.Count; i++)
                    {
                        num += Convert.ToDouble(cartdetail[i].Quantity.ToString());
                    }
                    totalvnd = num.ToString();
                }
            }
            return totalvnd;
        }
        protected string ShowTongTienCoin(string ID_Cart)
        {
            string totalvnd = "0";
            List<Entity.CartDetail> cartdetail = SCartDetail.Detail_ID_Cart(ID_Cart);
            if (cartdetail.Count > 0)
            {
                if (cartdetail.Count > 0)
                {
                    double num = 0.0;
                    for (int i = 0; i < cartdetail.Count; i++)
                    {
                        num += Convert.ToDouble(cartdetail[i].Diemcoin.ToString());
                    }
                    totalvnd = num.ToString();
                }
            }
            return totalvnd;
        }
        protected string ShowTongThongBao(string id)
        {
            string Tong1 = "0";
            string Tong2 = "0";
            List<Notification> dt = db.Notifications.Where(s => s.IDThanhVienNhanThongBao == int.Parse(id) && s.TrangThai == 1).ToList();
            if (dt.Count > 0)
            {
                Tong1 = dt.Count.ToString();
            }
            List<Notification> dt1 = db.Notifications.Where(s => s.IDThanhVienNhanThongBao == int.Parse(id) && s.TrangThai == 0).ToList();
            if (dt1.Count > 0)
            {
                Tong2 = dt1.Count.ToString();
            }
            return Tong1 + "/" + Tong2;
        }
        protected void ddlLevelThanhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlLevelThanhVien = (DropDownList)sender;
            var id = (HiddenField)ddlLevelThanhVien.FindControl("hiID");
            List<Entity.users> list = Susers.Name_Text("select * from users where iuser_id=" + id.Value + "  ");
            if (list.Count > 0)
            {
                Susers.Name_Text("update users set LevelThanhVien=" + ddlLevelThanhVien.SelectedValue + " where iuser_id=" + id.Value + "");
                NangLevel.Lichsucapdo(id.Value, ddlLevelThanhVien.SelectedValue, MoreAll.MoreAll.GetCookies("UName").ToString());
                ltthongbao.Text = "<script type=\"text/javascript\">alert('Cập nhật cấp cấp độ (" + ddlLevelThanhVien.SelectedValue + ") thành công, Cho Thành Viên: (" + list[0].vfname + ").');window.location.href='" + Request.RawUrl.ToString() + "'; </script></div>";
            }
        }
        protected void rp_items_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ddlLevelThanhVien = (e.Item.FindControl("ddlLevelThanhVien") as DropDownList);
                HiddenField id = (e.Item.FindControl("hdLevelThanhVien") as HiddenField);
                if (id.Value != "")
                {
                    ddlLevelThanhVien.SelectedValue = id.Value;
                }
            }
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

        protected void txtTieude_TextChanged(object sender, EventArgs e)
        {
            TextBox Tieude = (TextBox)sender;
            var b = (HiddenField)Tieude.FindControl("hiID");
            if (Tieude.Text.Trim().Contains(","))
            {
                ltthongbao.Text = "<script type=\"text/javascript\">alert('Định dạng Số cổ phẩn đang sở hữu bị sai.');window.location.href='" + Request.RawUrl.ToString() + "'; </script></div>";
            }
            else
            {
                if (Tieude.Text.Length > 0)
                {
                    List<Entity.users> list = Susers.Name_Text("select * from users where iuser_id=" + b.Value + "  ");
                    if (list.Count > 0)
                    {
                        Susers.Name_Text("update users set TienDangSoHuuBatDongSan=" + Tieude.Text + " where iuser_id=" + b.Value + "");
                        ltthongbao.Text = "<script type=\"text/javascript\">alert('Cập nhật SỐ CỔ PHẦN ĐANG SỞ HỮU thành công, Cho Thành Viên: (" + list[0].vfname + ").');window.location.href='" + Request.RawUrl.ToString() + "'; </script></div>";
                    }
                }
                else
                {
                    ltthongbao.Text = "<script type=\"text/javascript\">alert('Vui lòng điền số cổ phẩn đang sở hữu ');window.location.href='" + Request.RawUrl.ToString() + "'; </script></div>";
                }
            }

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
            Response.Redirect("admin.aspx?u=Thanhvien&thanhvienkh=" + ddlthanhvien.SelectedValue + "&khoa=" + ddkhoathanhvien.SelectedValue + "&st=" + ddlstatus.SelectedValue + "&us=" + ddlorderby.SelectedValue + "&ds=" + ddlordertype.SelectedValue + "&kw=" + txtkeyword.Text + "&chinhanh=" + ddlchinhanh.SelectedValue + "&Lead=" + ddltheoLead.SelectedValue + "&sao=" + ddlcapdo.SelectedValue + "&kieuthanhvien=" + ddlkieuthanhvien.SelectedValue + "&AgLand=" + ddlAgLand.SelectedValue + "&uutien=" + ddluutien.SelectedValue + "&QRCode=" + ddlQRCode.SelectedValue + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "");

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

        protected void lnkxuatExelNhaCC_Click(object sender, EventArgs e)
        {
            string sql1 = "";
            string Namefile = "DanhSachNhaCungCap";
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename=" + Namefile + ".xls");
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
            StringBuilder sb = new StringBuilder();

            sb.Append(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            sb.Append("<table border='1' bgcolor='#ffffff' bordercolor='#dedede' cellspacing='0' cellpadding='0' style='font-size:12px; font-family:Arial; background:white;'>");
            sb.Append("<tr>");
            sb.Append("  <th style=\"width:50px; vertical-align:middle; height: 22px;\">");
            sb.Append("    <b>STT</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:250px; vertical-align:middle;\">");
            sb.Append("    <b>Tên Đăng Nhập</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Họ và tên</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:350px; vertical-align:middle;\">");
            sb.Append("    <b>Địa chỉ</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Điện thoại</b>");
            sb.Append("  </th>");
            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Email</b>");
            sb.Append("  </th>");

            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Ngày đăng ký</b>");
            sb.Append("  </th>");

            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Tổng số sản phẩm</b>");
            sb.Append("  </th>");

            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Số sản phẩm chưa duyệt</b>");
            sb.Append("  </th>");

            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Đã duyệt</b>");
            sb.Append("  </th>");

            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Duyệt tạm thời</b>");
            sb.Append("  </th>");

            sb.Append("  <th style=\"width:150px; vertical-align:middle;\">");
            sb.Append("    <b>Yêu cầu xem lại</b>");
            sb.Append("  </th>");

            sb.Append("  <th style=\"width:1050px; vertical-align:middle;\">");
            sb.Append("    <b>Nhóm, Tên sản phẩm, Loại nhà cung cấp</b>");
            sb.Append("  </th>");
            sb.Append("</tr>");

            string sapxep = "";
            if (ddlordertype.SelectedValue == "desc")
            {
                sapxep = " ORDER BY dcreatedate desc  ";
            }
            else
            {
                sapxep = " ORDER BY dcreatedate asc  ";
            }

            List<Entity.users> dt = Susers.Name_Text("SELECT * FROM users where TongSoSanPham!=0  " + sapxep + "");
            if (dt.Count > 0)
            {
                int i = 1;
                foreach (var item in dt)
                {
                    sb.Append("<tr>");
                    sb.Append("    <td style=\"width:50px; vertical-align:middle; height: 22px; text-align: center;\">" + i++ + "</td>");
                    sb.Append("    <td style=\"width:250px; vertical-align:middle;text-align: center;\">" + item.vuserun + "</td>");
                    sb.Append("    <td style=\"width:250px; vertical-align:middle; text-align: center;\">" + item.vfname + "</td>");
                    sb.Append("    <td style=\"width:350px; vertical-align:middle;text-align: center;\">" + item.vaddress + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: center;\">" + item.vphone + "</td>");
                    sb.Append("    <td style=\"width:250px; vertical-align:middle; text-align: center;\">" + item.vemail + "</td>");
                    sb.Append("    <td style=\"width:250px; vertical-align:middle; text-align: center;\">" + item.dcreatedate + "</td>");
                    sb.Append("    <td style=\"width:10px; vertical-align:middle; text-align: center;\">" + Commond.ShowTongSanPhamNhaCungCap(item.iuser_id.ToString()) + "</td>");
                    sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: center;\">" + Commond.ShowTrangThaiSanPham(0, item.iuser_id.ToString()) + "</td>");
                    sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: center;\">" + Commond.ShowTrangThaiSanPham(1, item.iuser_id.ToString()) + "</td>");
                    sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: center;\">" + Commond.ShowTrangThaiSanPham(2, item.iuser_id.ToString()) + "</td>");
                    sb.Append("<td style=\"width:150px; vertical-align:middle;text-align: center;\">" + Commond.ShowTrangThaiSanPham(3, item.iuser_id.ToString()) + "</td>");

                    sb.Append("<td style=\"width:1050px; vertical-align:middle;text-align: left;\">" + Commond.ShowSanPhamNCC(item.iuser_id.ToString()) + "</td>");
                    sb.Append("</tr>");
                }
            }
            sb.Append("</table>");
            Response.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }

        protected void txtduyettudongdonhang_TextChanged(object sender, EventArgs e)
        {
            TextBox Tieude = (TextBox)sender;
            var b = (HiddenField)Tieude.FindControl("hiID");
            if (Tieude.Text.Trim().Contains(","))
            {
                ltthongbao.Text = "<script type=\"text/javascript\">alert('Định dạng bị sai.');window.location.href='" + Request.RawUrl.ToString() + "'; </script></div>";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(Tieude.Text))
                {
                    List<Entity.users> list = Susers.Name_Text("select * from users where iuser_id=" + b.Value + "  ");
                    if (list.Count > 0)
                    {
                        Susers.Name_Text("update users set CauHinhDuyetDonTuDong=" + Tieude.Text + " where iuser_id=" + b.Value + "");
                        ltthongbao.Text = "<script type=\"text/javascript\">alert('Cập nhật duyệt đơn tự động thành công, Cho Thành Viên: (" + list[0].vfname + ").');window.location.href='" + Request.RawUrl.ToString() + "'; </script></div>";
                    }
                }
                else
                {
                    ltthongbao.Text = "<script type=\"text/javascript\">alert('Vui lòng kiểm tra lại.');window.location.href='" + Request.RawUrl.ToString() + "'; </script></div>";
                }
            }
        }

        protected void ddlthanhvien_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }

        protected void ddkhoathanhvien_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadItems();
            LoadRequest();
        }

        //protected void txtChietkauQRCode_TextChanged(object sender, EventArgs e)
        //{
        //    TextBox Nhom = (TextBox)sender;
        //    var b = (HiddenField)Nhom.FindControl("hiID");
        //    if (Nhom.Text.Length > 0)
        //    {
        //        user iitem = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(b.Value));
        //        iitem.QRCodeChietKhauHH = Nhom.Text;
        //        db.SubmitChanges();

        //        #region LichSuQRCode
        //        LichSuQRCode obj = new LichSuQRCode();
        //        obj.IDThanhVien = int.Parse(b.Value);
        //        obj.ChietKhauHH = Nhom.Text.Trim();
        //        obj.HHNGuoiMua = "";
        //        obj.HHHeThong = "";
        //        obj.NguoiDuyet = "Admin : " + MoreAll.MoreAll.GetCookies("UName").ToString();
        //        obj.NgayDuyet = DateTime.Now;
        //        db.LichSuQRCodes.InsertOnSubmit(obj);
        //        db.SubmitChanges();
        //        #endregion

        //        LoadItems();
        //        this.ltthongbao.Text = "<span class=alert>Cập nhật % hoa hồng chiết khấu QRCode thành công !!</span>";
        //    }
        //    else
        //    {
        //        ltthongbao.Text = "<span class=alert>Bạn chưa nhập % hoa hồng chiết khấu QRCode !!</span>";
        //    }
        //    //  -4 ,làm them khi thay đổi giá trị hoa hồng theo ngày , ai thay đổi, admin hay khách hàng..lưu lịch sử
        //}

        //protected void txthhnguoimuaQRCode_TextChanged(object sender, EventArgs e)
        //{
        //    TextBox Nhom = (TextBox)sender;
        //    var b = (HiddenField)Nhom.FindControl("hiID");
        //    if (Nhom.Text.Length > 0)
        //    {
        //        user iitem = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(b.Value));
        //        iitem.QRCodeHHNguoiMua = Nhom.Text;

        //        #region LichSuQRCode
        //        LichSuQRCode obj = new LichSuQRCode();
        //        obj.IDThanhVien = int.Parse(b.Value);
        //        obj.ChietKhauHH = "";
        //        obj.HHNGuoiMua = Nhom.Text.Trim();
        //        obj.HHHeThong = "";
        //        obj.NguoiDuyet = "Admin : " + MoreAll.MoreAll.GetCookies("UName").ToString();
        //        obj.NgayDuyet = DateTime.Now;
        //        db.LichSuQRCodes.InsertOnSubmit(obj);
        //        db.SubmitChanges();
        //        #endregion

        //        db.SubmitChanges();
        //        LoadItems();
        //        this.ltthongbao.Text = "<span class=alert>Cập nhật % hoa hồng người mua QRCode thành công !!</span>";
        //    }
        //    else
        //    {
        //        ltthongbao.Text = "<span class=alert>Bạn chưa nhập % hoa hồng người mua QRCode !!</span>";
        //    }
        //    //  -4 ,làm them khi thay đổi giá trị hoa hồng theo ngày , ai thay đổi, admin hay khách hàng..lưu lịch sử
        //}

        //protected void txthhhethongQRCode_TextChanged(object sender, EventArgs e)
        //{
        //    TextBox Nhom = (TextBox)sender;
        //    var b = (HiddenField)Nhom.FindControl("hiID");
        //    if (Nhom.Text.Length > 0)
        //    {
        //        user iitem = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(b.Value));
        //        iitem.QRCodeHHHeThong = Nhom.Text;

        //        #region LichSuQRCode
        //        LichSuQRCode obj = new LichSuQRCode();
        //        obj.IDThanhVien = int.Parse(b.Value);
        //        obj.ChietKhauHH = "";
        //        obj.HHNGuoiMua = "";
        //        obj.HHHeThong = Nhom.Text.Trim();
        //        obj.NguoiDuyet = "Admin : " + MoreAll.MoreAll.GetCookies("UName").ToString();
        //        obj.NgayDuyet = DateTime.Now;
        //        db.LichSuQRCodes.InsertOnSubmit(obj);
        //        db.SubmitChanges();
        //        #endregion

        //        db.SubmitChanges();
        //        LoadItems();

        //    }
        //    else
        //    {
        //        ltthongbao.Text = "<span class=alert>Bạn chưa nhập % hoa hồng hệ thống QRCode !!</span>";
        //    }
        //    //  -4 ,làm them khi thay đổi giá trị hoa hồng theo ngày , ai thay đổi, admin hay khách hàng..lưu lịch sử
        //}
        //protected void Linkbutton9_Click(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < Repeater1.Items.Count; i++)
        //    {
        //        TextBox txtChietkauQRCode = (TextBox)Repeater1.Items[i].FindControl("txtChietkauQRCode");
        //        TextBox txthhnguoimuaQRCode = (TextBox)Repeater1.Items[i].FindControl("txthhnguoimuaQRCode");
        //        TextBox txthhhethongQRCode = (TextBox)Repeater1.Items[i].FindControl("txthhhethongQRCode");

        //        HiddenField id = (HiddenField)Repeater1.Items[i].FindControl("hiID");
        //        // Cập nhật thứ tự
        //        if (txtChietkauQRCode.Text != "" && txtChietkauQRCode.Text != "0" && txthhnguoimuaQRCode.Text != "" && txthhnguoimuaQRCode.Text != "0" && txthhhethongQRCode.Text != "" && txthhhethongQRCode.Text != "0")
        //        {
        //            #region LichSuQRCode
        //            LichSuQRCode obj = new LichSuQRCode();
        //            obj.IDThanhVien = int.Parse(id.Value);
        //            obj.ChietKhauHH = txtChietkauQRCode.Text.Trim();
        //            obj.HHNGuoiMua = txthhnguoimuaQRCode.Text.Trim();
        //            obj.HHHeThong = txthhhethongQRCode.Text.Trim();
        //            obj.NguoiDuyet = "Admin : " + MoreAll.MoreAll.GetCookies("UName").ToString();
        //            obj.NgayDuyet = DateTime.Now;
        //            db.LichSuQRCodes.InsertOnSubmit(obj);
        //            db.SubmitChanges();
        //            #endregion
        //            Susers.Name_Text("update users set QRCodeChietKhauHH='" + txtChietkauQRCode.Text + "',QRCodeHHNguoiMua='" + txthhnguoimuaQRCode.Text + "',QRCodeHHHeThong='" + txthhhethongQRCode.Text + "' WHERE iuser_id=" + id.Value + "");

        //            this.ltthongbao.Text = "<span class=alert>Cập nhật % hoa hồng QRCode thành công !!</span>";
        //        }
        //        else
        //        {
        //            ltthongbao.Text = "<span class=alert>Bạn chưa nhập % hoa hồng QRCode !!</span>";
        //        }
        //    }
        //    LoadItems();
        //}
    }
}
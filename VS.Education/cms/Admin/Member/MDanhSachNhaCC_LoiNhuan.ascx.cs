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

namespace VS.E_Commerce.cms.Admin.Member
{
    public partial class MDanhSachNhaCC_LoiNhuan : System.Web.UI.UserControl
    {
        private string status = "-1";
        private string IDThanhVien = "0";
        private string lang = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        public string ShowThem = "0";
        DateTime fDate, tDate;
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
                this.ddlstatus.Items.Add(new ListItem("Tất cả các mục", "-1"));
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
            string sapxep = "";
            string orderby = this.ddlorderby.SelectedValue + " " + this.ddlordertype.SelectedValue;
            if (ddlkieuthanhvien.SelectedValue != "-1")
            {
                if (ddlkieuthanhvien.SelectedValue == "2")
                {
                    sql1 += " and Type=" + ddlkieuthanhvien.SelectedValue + " ";
                }
                else if (ddlkieuthanhvien.SelectedValue == "3")
                {
                    sql1 += " and Type=2 and TongSoSanPham!=0";
                }
            }
            else
            {
                sql1 += "  and TongSoSanPham!=0";
            }
            if (orderby.Length < 1)
            {
                sapxep = "order by dcreatedate desc";
            }
            else
            {
                sapxep = "order by " + orderby;
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

            List<Entity.TongSo> iitem = Susers.CATEGORY_PHANTRANG_NCC1(IDThanhVien, txtkeyword.Text.Replace("&nbsp;", ""), ddlchinhanh.SelectedValue, ddlstatus.SelectedValue, "", ddlcapdo.SelectedValue, sql1, sapxep);
            if (iitem.Count() > 0)
            {
                Tongsobanghi = iitem[0].Tong;
                lttongtb.Text = iitem[0].Tong.ToString();
            }
            List<Entity.users> dt = Susers.CATEGORY_PHANTRANG_NCC2(IDThanhVien, txtkeyword.Text.Replace("&nbsp;", ""), ddlchinhanh.SelectedValue, ddlstatus.SelectedValue, "", ddlcapdo.SelectedValue, sql1, sapxep, (pages - 1), Tongsotrang);
            if (dt.Count >= 1)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
                //foreach (var item in dt)
                //{
                //    Susers.Name_Text("Update users set TongSoSanPhamDaBan=" + ShowSanPhamDaBan(item.iuser_id.ToString()) + "  where iuser_id=" + item.iuser_id.ToString() + "");
                //}
            }

            if (Tongsobanghi % Tongsotrang > 0)
            {
                Tongsobanghi = (Tongsobanghi / Tongsotrang) + 1;
            }
            else
            {
                Tongsobanghi = Tongsobanghi / Tongsotrang;
            }
            ltpage.Text = Commond.PhantrangAdmin("/admin.aspx?u=LoiNhuanNCC&st=" + ddlstatus.SelectedValue + "&us=" + ddlorderby.SelectedValue + "&ds=" + ddlordertype.SelectedValue + "&kw=" + txtkeyword.Text + "&chinhanh=" + ddlchinhanh.SelectedValue + "&sao=" + ddlcapdo.SelectedValue + "&kieuthanhvien=" + ddlkieuthanhvien.SelectedValue + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "", Tongsobanghi, pages);
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
        protected string Showsanpham2(string kieu, string id, string TongSoSanPham)
        {
            string str = "";
            if (kieu == "2")
            {
                str = "<a href=\"/admin.aspx?u=pro&su=items&IDThanhVien=" + id + "\" target=\"_blank\">" + TongSoSanPham + " </a>";
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

        protected string ShowDiemGoc(string id)
        {
            string Total = "0";
            List<Entity.ELoiNhuanMuaBan> detail = SLoiNhuanMuaBan.Name_Text("select * from LoiNhuanMuaBan where IDThanhVienBan=" + id + "");
            if (detail.Count > 0)
            {
                double num = 0.0;
                for (int i = 0; i < detail.Count; i++)
                {
                    num += Convert.ToDouble(detail[i].SoDiemGoc.ToString());
                }
                Total = num.ToString();
            }
            return Total;
        }
        protected string ShowDiemConLai(string id)
        {
            string Total = "0";
            List<Entity.ELoiNhuanMuaBan> detail = SLoiNhuanMuaBan.Name_Text("select * from LoiNhuanMuaBan where IDThanhVienBan=" + id + "");
            if (detail.Count > 0)
            {
                double num = 0.0;
                for (int i = 0; i < detail.Count; i++)
                {
                    num += Convert.ToDouble(detail[i].SoDiemConLai.ToString());
                }
                Total = num.ToString();
            }
            return Total;
        }
        protected string ShowSoDiemDaChia(string id)
        {
            string Total = "0";
            List<Entity.ELoiNhuanMuaBan> detail = SLoiNhuanMuaBan.Name_Text("select * from LoiNhuanMuaBan where IDThanhVienBan=" + id + "");
            if (detail.Count > 0)
            {
                double num = 0.0;
                for (int i = 0; i < detail.Count; i++)
                {
                    num += Convert.ToDouble(detail[i].SoDiemDaChia.ToString());
                }
                Total = num.ToString();
            }
            return Total;
        }
        protected string ShowSanPhamDaBan(string id)
        {
            string Total = "0";
            List<Entity.CartDetail> detail = SCartDetail.Name_Text("select * from CartDetail where TrangThaiNhaCungCap=1 and TrangThaiNguoiMuaHang=1 and TrangThaiKhieuKien=0 and IDNhaCungCap=" + id + "");
            if (detail.Count > 0)
            {
                double num = 0.0;
                for (int i = 0; i < detail.Count; i++)
                {
                    num += Convert.ToDouble(detail[i].Quantity.ToString());
                }
                Total = num.ToString();
            }
            return Total;
        }

        protected void ddlcapdo_SelectedIndexChanged(object sender, EventArgs e)
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
            Response.Redirect("admin.aspx?u=LoiNhuanNCC&st=" + ddlstatus.SelectedValue + "&us=" + ddlorderby.SelectedValue + "&ds=" + ddlordertype.SelectedValue + "&kw=" + txtkeyword.Text + "&chinhanh=" + ddlchinhanh.SelectedValue + "&sao=" + ddlcapdo.SelectedValue + "&kieuthanhvien=" + ddlkieuthanhvien.SelectedValue + "&Tu=" + txtNgayThangNam.Text + "&Den=" + txtDenNgayThangNam.Text + "");

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
            sb.Append("</tr>");
            List<Entity.users> dt = Susers.Name_Text("SELECT * FROM users where TongSoSanPham!=0  order by iuser_id asc");
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
                    sb.Append("</tr>");
                }
            }
            sb.Append("</table>");
            Response.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }


    }
}
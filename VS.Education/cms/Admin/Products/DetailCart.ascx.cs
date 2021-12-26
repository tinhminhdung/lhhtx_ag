using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using MoreAll;
using Entity;
using System.Data;
using Framwork;
using FlexCel.XlsAdapter;
using FlexCel.Core;
using System.Text;
using System.Configuration;
using TestWindowService;

namespace VS.E_Commerce.cms.Admin.Products
{
    public partial class DetailCart : System.Web.UI.UserControl
    {
        private string status = "1";
        public int i = 1;
        protected bool Dung = false;
        private string IDSanPham = "";
        private string IDGioHang = "0";
        private string lang = Captionlanguage.Language;
        string ID = "";
        DatalinqDataContext db = new DatalinqDataContext();
        string URL = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            URL = Request.RawUrl.ToString();
            if (System.Web.HttpContext.Current.Session["lang"] != null)
            {
                this.lang = System.Web.HttpContext.Current.Session["lang"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["lang"] = this.lang;
                this.lang = System.Web.HttpContext.Current.Session["lang"].ToString();
            }
            if (Request["ID"] != null && !Request["ID"].Equals(""))
            {
                ID = Request["ID"];
            }
            ChitietDonhang(ID);
            if (!base.IsPostBack)
            { }
        }
        void ChitietDonhang(string id)
        {
            double Diemcoins = 0;
            double totalvnd = 0;
            hdIDGiohang.Value = id.ToString();
            List<Entity.CartDetail> table = SCartDetail.Detail_ID_Cart(id);
            if (table.Count > 0)
            {
                this.rpcartdetail.DataSource = table;
                this.rpcartdetail.DataBind();


                foreach (var item in table)
                {
                    if (item.NoiDungGiaoHang.Length > 5)
                    {
                        ltthongtin.Text += "<div class=\"alert alert-info\" role=\"alert\">Thông báo từ nhà cung cấp với sản phẩm : <b>" + Commond.ShowPro(item.ipid.ToString()) + " <br></b>";
                        ltthongtin.Text += "<div style='color:red'> " + item.NoiDungGiaoHang + "</div>";
                        ltthongtin.Text += "</div>";
                    }
                }


                double num2 = 0.0;
                double num = 0.0;
                for (int i = 0; i < table.Count; i++)
                {
                    num += Convert.ToDouble(table[i].Money.ToString());
                    num2 += Convert.ToDouble(table[i].Diemcoin.ToString());
                }
                totalvnd = num;
                Diemcoins = num2;
                double TongCoin = (Diemcoins);/// 100;
                /// 
                #region Show % Hoa Hồng theo thời gian mua hàng
                //Xem thành viên đang ở level nào rồi nhân với level đó
                List<Carts> table2 = SCarts.Carts_GetById(table[0].ID_Cart.ToString());
                if (table2.Count > 0)
                {
                    ////// Nếu đã đặt hàng thành công xong rồi trạng thái status =1 thì sẽ phải lấy HoaHongTheoLevel trong bảng cartdetail để làm căn cứ lịch sử lúc mua tại thời điểm đó chỉ dc bằng đó %
                    ////if (table2[0].Status == 1)
                    ////{
                    //string CapoLevelHoaHongs = (table[0].HoaHongTheoLevel.ToString());
                    //double HoaHongs = Convert.ToDouble(CapoLevelHoaHongs);

                    string CapoLevelHoaHongs = MoreAll.Other.Giatri("txtHoaHongGioiThieuTrucTiepmuahangVaF1");//dtcart[i].HoaHongTheoLevel.ToString();
                    double HoaHongs = Convert.ToDouble(MoreAll.Other.Giatri("txtHoaHongGioiThieuTrucTiepmuahangVaF1"));// Convert.ToDouble(dtcart[i].HoaHongTheoLevel.ToString());

                    double Tong = (TongCoin * HoaHongs) / 100;
                    this.ltCoin.Text = HoaHongs + "% của / Tổng " + Diemcoins + " điểm = " + Tong + " điểm ";
                    ////}
                    ////else
                    ////{
                    ////    // Nếu chưa đặt hàng thì trọc vào bảng thành viên lấy hoa hồng hiện tại thành viên đang ở level mấy  ra nhé
                    ////    user tables = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(table[0].IDThanhVien.ToString()));// Type=1 là thành viên  hoặc là ng mua hàng , type=2 là nhà cung cấp
                    ////    if (tables != null)
                    ////    {
                    ////        string CapoLevelHoaHongs = CapoLevelHoaHong(tables.LevelThanhVien.ToString());
                    ////        double HoaHongs = Convert.ToDouble(CapoLevelHoaHongs);
                    ////        double Tong = (TongCoin * HoaHongs) / 100;
                    ////        this.ltCoin.Text = HoaHongs + "% của / Tổng " + Diemcoins + " điểm = " + Tong + " điểm ";
                    ////    }
                    ////}
                }
                #endregion
                lttong.Text = AllQuery.MorePro.Detail_Price(num.ToString());
            }
            double TongDiemphaithanhtoan = (totalvnd) / 1000;
            lttongdiem.Text = TongDiemphaithanhtoan.ToString();
            List<Entity.Carts> dt1 = SCarts.Carts_GetById(id);
            if (dt1.Count > 0)
            {
                ltmadonhang.Text = dt1[0].ID.ToString();
                ltname.Text = ShowtThanhVien2(dt1[0].IDThanhVien.ToString(), dt1[0].Name.ToString());
                // ltname.Text = dt1[0].Name.ToString();
                ltdienthoai.Text = dt1[0].Phone.ToString();
                ltemail.Text = dt1[0].Email.ToString();
                ltdiachi.Text = dt1[0].Address.ToString();
                lthinhthucgiaohang.Text = dt1[0].Phuongthucthanhtoan.ToString();
                ltthanhtoan.Text = dt1[0].Hinhthucvanchuyen.ToString();
                ltghichu.Text = dt1[0].Contents;
                ltvietchu.Text = ConvertSoRaChu(dt1[0].Money);
                hdtongtien.Value = dt1[0].Money.ToString();
                hdid.Value = id;
            }
        }
        protected void Delete_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Xóa đơn hàng này ?')";
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.lang);
        }

        protected void Pass_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Xử lý ?')";
        }

        protected void UnPass_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Hủy bỏ ?')";
        }
        protected string Thanhtoan(string ID)
        {
            LCart abc = db.LCarts.SingleOrDefault(p => p.ID == int.Parse(ID));
            if (abc.StatusGiaoDich.ToString().Equals("1"))
            {
                return "color:#fff; padding:3px; font-weight:bold; background:#fe0505; border-radius:5px; width:250px";
            }
            return "color:#fff; padding:3px; font-weight:bold; background:#ff619c; border-radius:5px; width:250px";
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

        #region Hàm đổi số ra chữ
        private string Chu(string gNumber)
        {
            string result = "";
            switch (gNumber)
            {
                case "0":
                    result = "không";
                    break;
                case "1":
                    result = "một";
                    break;
                case "2":
                    result = "hai";
                    break;
                case "3":
                    result = "ba";
                    break;
                case "4":
                    result = "bốn";
                    break;
                case "5":
                    result = "năm";
                    break;
                case "6":
                    result = "sáu";
                    break;
                case "7":
                    result = "bảy";
                    break;
                case "8":
                    result = "tám";
                    break;
                case "9":
                    result = "chín";
                    break;
            }
            return result;
        }
        private string Donvi(string so)
        {
            string Kdonvi = "";
            if (so.Equals("1"))
                Kdonvi = "";
            if (so.Equals("2"))
                Kdonvi = "nghìn";
            if (so.Equals("3"))
                Kdonvi = "triệu";
            if (so.Equals("4"))
                Kdonvi = "tỷ";
            if (so.Equals("5"))
                Kdonvi = "nghìn tỷ";
            if (so.Equals("6"))
                Kdonvi = "triệu tỷ";
            if (so.Equals("7"))
                Kdonvi = "tỷ tỷ";

            return Kdonvi;
        }
        private string Tach(string tach3)
        {
            string Ktach = "";
            if (tach3.Equals("000"))
                return "";
            if (tach3.Length == 3)
            {
                string tr = tach3.Trim().Substring(0, 1).ToString().Trim();
                string ch = tach3.Trim().Substring(1, 1).ToString().Trim();
                string dv = tach3.Trim().Substring(2, 1).ToString().Trim();
                if (tr.Equals("0") && ch.Equals("0"))
                    Ktach = " không trăm lẻ " + Chu(dv.ToString().Trim()) + " ";
                if (!tr.Equals("0") && ch.Equals("0") && dv.Equals("0"))
                    Ktach = Chu(tr.ToString().Trim()).Trim() + " trăm ";
                if (!tr.Equals("0") && ch.Equals("0") && !dv.Equals("0"))
                    Ktach = Chu(tr.ToString().Trim()).Trim() + " trăm lẻ " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && dv.Equals("0"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && dv.Equals("5"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi lăm ";
                if (tr.Equals("0") && ch.Equals("1") && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = " không trăm mười " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && ch.Equals("1") && dv.Equals("0"))
                    Ktach = " không trăm mười ";
                if (tr.Equals("0") && ch.Equals("1") && dv.Equals("5"))
                    Ktach = " không trăm mười lăm ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi " + Chu(dv.Trim()).Trim() + " ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && dv.Equals("0"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi lăm ";
                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười " + Chu(dv.Trim()).Trim() + " ";
                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && dv.Equals("0"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười ";
                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười lăm ";
            }
            return Ktach;
        }
        public string ConvertSoRaChu(double gNum)
        {
            if (gNum == 0)
                return "Không đồng";
            string lso_chu = "";
            string tach_mod = "";
            string tach_conlai = "";
            double Num = Math.Round(gNum, 0);
            string gN = Convert.ToString(Num);
            int m = Convert.ToInt32(gN.Length / 3);
            int mod = gN.Length - m * 3;
            string dau = "[+]";
            // Dau [+ , - ]
            if (gNum < 0)
                dau = "[-]";
            dau = "";
            // Tach hang lon nhat
            if (mod.Equals(1))
                tach_mod = "00" + Convert.ToString(Num.ToString().Trim().Substring(0, 1)).Trim();
            if (mod.Equals(2))
                tach_mod = "0" + Convert.ToString(Num.ToString().Trim().Substring(0, 2)).Trim();
            if (mod.Equals(0))
                tach_mod = "000";
            // Tach hang con lai sau mod :
            if (Num.ToString().Length > 2)
                tach_conlai = Convert.ToString(Num.ToString().Trim().Substring(mod, Num.ToString().Length - mod)).Trim();
            ///don vi hang mod
            int im = m + 1;
            if (mod > 0)
                lso_chu = Tach(tach_mod).ToString().Trim() + " " + Donvi(im.ToString().Trim());
            /// Tach 3 trong tach_conlai
            int i = m;
            int _m = m;
            int j = 1;
            string tach3 = "";
            string tach3_ = "";
            while (i > 0)
            {
                tach3 = tach_conlai.Trim().Substring(0, 3).Trim();
                tach3_ = tach3;
                lso_chu = lso_chu.Trim() + " " + Tach(tach3.Trim()).Trim();
                m = _m + 1 - j;
                if (!tach3_.Equals("000"))
                    lso_chu = lso_chu.Trim() + " " + Donvi(m.ToString().Trim()).Trim();
                tach_conlai = tach_conlai.Trim().Substring(3, tach_conlai.Trim().Length - 3);

                i = i - 1;
                j = j + 1;
            }
            if (lso_chu.Trim().Substring(0, 1).Equals("k"))
                lso_chu = lso_chu.Trim().Substring(10, lso_chu.Trim().Length - 10).Trim();
            if (lso_chu.Trim().Substring(0, 1).Equals("l"))
                lso_chu = lso_chu.Trim().Substring(2, lso_chu.Trim().Length - 2).Trim();
            if (lso_chu.Trim().Length > 0)
                lso_chu = dau.Trim() + " " + lso_chu.Trim().Substring(0, 1).Trim().ToUpper() + lso_chu.Trim().Substring(1, lso_chu.Trim().Length - 1).Trim() + " đồng chẵn.";
            return lso_chu.ToString().Trim();
        }
        #endregion
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

        protected string ShowGiaThanhVien(string id)
        {
            if (id == "1")
            {
                return "<div style=\"background:red;padding: 4px;margin:10px;color:#fff;border-radius: 3px;font-weight: 600;\">Mua theo giá thành viên</div>";
            }
            return "";
        }
        protected bool ShowXyLyDonHang()
        {
            List<Entity.CartDetail> dtcart = SCartDetail.Name_Text("select * from CartDetail where TrangThaiNhaCungCap in (3,2) and ID_Cart=" + hdIDGiohang.Value + "");
            if (dtcart.Count > 0)
            {
                ltmess.Text = "<div class=\"thongbaosv\">Hiện trong đơn hàng này vẫn có 1 số sản phẩm chưa được duyệt. hoặc bị hủy. bạn cần phải trao đổi với nhà cung cấp , hoặc xóa sản phẩm đó trước khi bấm duyệt đơn hàng .</div>";
                return false;
            }
            return true;
        }

        protected string TimLevelB(string ID)
        {
            user iitems = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(ID.ToString()));
            if (iitems != null)
            {
                return iitems.LevelThanhVien.ToString();
            }
            return "0";
        }

        protected string CapoLevelID(string level)
        {
            List<Entity.Menu> cdd = SMenu.Name_Text("SELECT * FROM Menu where capp='" + More.LV + "'  and Views=" + level + " and lang='VIE' order by level,Orders asc");
            if (cdd != null)
            {
                return cdd[0].ID.ToString();// ID chính là thuộc cấp độ mấy do tiêu đề ghi
            }
            return "0";
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

        #region Cách tính cho gián tiếp --> cứ - được mà ko âm thì cho lấy ra số điểm để thưởng cho b gián tiếp
        //TinhDiemthuongGiantiep("2","5") // lv2 - lv5 = 6
        public string TinhDiemthuongGiantiep(string LevelA, string LevelB)
        {
            if (LevelA.Length > 0 && LevelB.Length > 0)
            {
                if (Convert.ToDouble(LevelA.ToString()) > Convert.ToDouble(LevelB.ToString()))
                {
                    double TLevelA = Convert.ToDouble(SetLevel(LevelA.ToString()));
                    double TLevelB = Convert.ToDouble(SetLevel(LevelB.ToString()));
                    double Tong = (TLevelA - TLevelB);
                    if (Tong != 0)
                    {
                        return Tong.ToString();
                    }
                }
            }
            return "0";
        }
        public string SetLevel(string id)
        {
            List<Entity.Menu> dt = SMenu.Name_Text("select * from Menu where capp='" + More.LV + "' and Views=" + id + " ");
            if (dt.Count > 0)
            {
                return dt[0].Noidung1.ToString();
            }
            return "0";
        }
        #endregion

        protected string TimThanhVienGioiThieu(string id)
        {
            string str = "0";
            List<Entity.users> dt = Susers.Name_Text("select * from users where iuser_id=" + id + "");
            if (dt.Count >= 1)
            {
                str = dt[0].ThanhVienAgLang.ToString();
            }
            return str;
        }

        #region ChiNhanh
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
            string str = "0";
            List<Entity.Menu> dt = SMenu.GETBYID(id);
            if (dt.Count >= 1)
            {
                if (dt[0].Name.ToString().Length > 0)
                {
                    str += dt[0].Name;
                }
            }
            return str;
        }
        #endregion

        #region Tìm nhà bán cung cấp hàng từ sản phẩm đang bán
        protected string ShowNhaCungCap(string id)
        {
            string str = "0";
            List<Entity.Products> dt = SProducts.Name_Text("select * from products  where ipid=" + id + "");
            if (dt.Count >= 1)
            {
                str = dt[0].IDThanhVien.ToString();
            }
            return str;
        }
        #endregion
        protected string HoaHongTheoLevel_TheoThoiDiemMuahang_News(string CapoLevelHoaHongs, string Tongd)
        {
            double TongCoin = Convert.ToDouble(Tongd);
            double HoaHongs = Convert.ToDouble(CapoLevelHoaHongs);
            double Tong = (TongCoin * HoaHongs) / 100;
            return Tong.ToString();
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

                        double HoaHongs = Convert.ToDouble("30");//CapoLevelHoaHongs
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
                        double HoaHongs = Convert.ToDouble("30");//CapoLevelHoaHongs
                        double TongCoin = Convert.ToDouble(Tongd);
                        double Tong = (TongCoin * HoaHongs) / 100;
                        return Tong.ToString();
                    }
                }
            }
            #endregion
            return "";
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
        public string ShowThanhVien(string IDThanhVien)
        {
            if (IDThanhVien != "0")
            {
                try
                {
                    user table = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(IDThanhVien));
                    if (table != null)
                    {
                        return "<a target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&IDThanhVien=" + table.iuser_id.ToString() + "\"><span style='color:red'>" + table.vfname + "</span></a>";
                    }
                }
                catch (Exception)
                { }
            }
            return "Admin";
        }

        public string ShowGiacongtynhapvao(string id, string quantity)
        {
            product table = db.products.SingleOrDefault(p => p.ipid == int.Parse(id));
            if (table != null)
            {
                Double Tongtien = Convert.ToInt32(quantity) * Convert.ToDouble(table.Giacongtynhapvao.ToString());
                return Tongtien.ToString();
            }
            return "0";
        }
        protected string SentMail(string id)
        {
            if (id != "0")
            {
                return "Đã Gửi Mail Thông Báo";
            }
            return "";
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
                    str += "<a target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&IDThanhVien=" + dt[0].iuser_id.ToString() + "\"><span style='color:red'>" + dt[0].vuserun + "/" + dt[0].vfname + "</span></a>";
                }
                str += "</span><br>";
                if (dt[0].vphone.ToString().Length > 0)
                {
                    str += dt[0].vphone;
                }
                str += "<br>";
                if (dt[0].vemail.ToString().Length > 0)
                {
                    str += dt[0].vemail;
                }
                return str;
            }
            return "<b>Chưa có nhà cung cấp</b>";
        }
        protected string ShowtThanhVien2(string id, string Name)
        {
            string str = "";
            List<Entity.users> dt = Susers.GET_BY_ID(id);
            if (dt.Count >= 1)
            {
                str += "<a target=\"_blank\" href=\"/admin.aspx?u=Thanhvien&IDThanhVien=" + dt[0].iuser_id.ToString() + "\"><span style='color:red'>" + Name + "</span></a>";
                return str;
            }
            return "<b>" + Name + " /(Thành viên này đã bị xóa)</b>";
        }
        protected string Showtrangthai(string status)
        {
            if (status.Equals("0"))
            {
                return "<span style='background:#ed1c24;padding: 4px;margin:10px;color:#fff;border-radius: 3px;font-weight: 600;'>Chưa duyệt</span>";
            }
            if (status.Equals("2"))
            {
                return "<span style='background:#ed1c24;padding: 4px;margin:10px;color:#fff;border-radius: 3px;font-weight: 600;'>Chưa duyệt</span>";
            }
            if (status.Equals("3"))
            {
                return "<span style='background:#ed1c24;padding: 4px;margin:10px;color:#fff;border-radius: 3px;font-weight: 600;'>Chưa duyệt</span>";
            }
            return "<span style='background:#2489da;padding: 4px;margin:10px;color:#fff;border-radius: 3px;font-weight: 600;'>Đã duyệt</span>";
        }

        #region Tìm giá trị lớn nhất trong level để thưởng cho các đời F1 đến F5
        public string MinAndMax(string c)
        {
            String intString = c.Replace("99999999999,", ""); ;//.Replace(",0", "").Replace("0,", "");
            int[] strArray = stringArrayToIntArray(intString);
            int max = strArray[0];
            if (strArray.Length > 0)
            {
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i] > max)
                    {
                        max = strArray[i];
                    }
                }
            }
            if (max.ToString() == "0")
            {
                return "8";
            }
            else
            {
                return max.ToString();
            }
            return "8";// Nếu trong toàn bộ đều có level =0 thì gán cho nó là 8
        }
        public static int[] stringArrayToIntArray(String intString)
        {
            String[] intStringSplit = intString.Trim().Split(new char[] { ',' });
            int[] result = new int[intStringSplit.Length]; //Used to store our ints

            for (int i = 0; i < intStringSplit.Length; i++)
            {
                result[i] = int.Parse(intStringSplit[i]);
            }
            return result;
        }
        #endregion;

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
        protected string ShowtrangthaiNCC(string status)
        {
            if (status.Equals("1"))
            {
                return "<span style='background:#1188dc;padding: 4px;margin:5px;color:#fff;border-radius: 3px;font-weight: 600;'>Đã duyệt</span>";
            }
            if (status.Equals("2"))
            {
                return "<span style='background:#ed1c24;padding: 4px;margin:5px;color:#fff;border-radius: 3px;font-weight: 600;'>Hủy đơn</span>";
            }
            if (status.Equals("3"))
            {
                return "<span style='background:#ff7919;padding: 4px;margin:5px;color:#fff;border-radius: 3px;font-weight: 600;'>Chờ xử lý</span>";
            }
            return "";
        }

        /// News

        protected void rpcartdetail_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string strv = e.CommandName.Trim();
            string str2 = e.CommandArgument.ToString().Trim();
            string str4 = strv;

            switch (e.CommandName)
            {
                case "ChapNhan":
                    List<CartDetail> ds = db.ExecuteQuery<CartDetail>(@"SELECT * FROM CartDetail where ID =" + str2 + " and TrangThaiNguoiMuaHang=3").ToList();
                    if (ds.Count > 0)
                    {
                        #region Gửi mail cho nhà cung cấp khi thành viên mua hủy sản phẩm.
                        List<Entity.users> dc = Susers.GET_BY_ID(ds[0].IDThanhVien.ToString());
                        if (dc.Count > 0)
                        {
                            string Emails = dc[0].vemail.ToString();
                            string Noidung = "";

                            Noidung += "<b> Chúc mừng đơn hàng của bạn đã được người mua chấp nhận thành công, và người mua đã nhận được hàng. ! </b><br /> Đơn hàng <a href=\"http://lienhiephoptac.vn/account/orders/" + ds[0].ID_Cart.ToString() + "\" target=\"_blank\"><b>#" + ds[0].ID_Cart.ToString() + "</b></a> . Vui lòng xem nội dung ở phía dưới.<br />";
                            Noidung += "<br />";

                            Noidung += "<b>Tên sản phẩm :  </b>" + Commond.ShowPro(ds[0].ipid.ToString()) + "<br />";
                            Noidung += "<b>Số lượng sản phẩm :  </b>" + ds[0].Quantity.ToString() + "<br />";
                            Noidung += "<b>Tổng số tiền :  </b>" + ds[0].Money.ToString() + "<br />";

                            Noidung += "---------------------------------";
                            Noidung += "<br />";

                            Noidung += "<b>Người mua hàng: </b>" + dc[0].vfname.ToString() + "<br />";
                            Noidung += "<b>Địa chỉ: </b>" + dc[0].vaddress.ToString() + "<br />";
                            Noidung += "<b>Điện thoại: </b>" + dc[0].vphone.ToString() + "<br />";
                            Noidung += "<b>Email: </b>" + dc[0].vemail.ToString() + "<br />";

                            Noidung += "<br />";
                            Noidung += "<br />";
                            Noidung += Commond.Setting("txtfooterEmail");
                            Noidung += "<br />";

                            try
                            {
                                new MailHelper().SendMail(ShowEmailNhaCungCap(ds[0].IDNhaCungCap.ToString()), "Chấp nhận đơn hàng từ webiste: " + Request.Url.Host + " - Mã đơn hàng #" + ds[0].ID_Cart.ToString() + " ", Noidung.ToString());
                                new MailHelper().SendMail(dc[0].vemail.ToString(), "Chấp nhận đơn hàng từ webiste: " + Request.Url.Host + " - Mã đơn hàng #" + ds[0].ID_Cart.ToString() + " ", Noidung.ToString());
                            }
                            catch { }
                        }
                        #endregion
                    }
                    SCartDetail.Name_Text("UPDATE [CartDetail] SET [TrangThaiNguoiMuaHang]=1,LyDoHuyHang='',LyDoTraHang='' WHERE ID =" + str2 + "");
                    ltmess.Text = "<script type=\"text/javascript\">alert('Duyệt đơn hàng thành công..');window.location.href='" + URL + "'; </script></div>";

                    return;
                case "TraHang":
                    #region TraHang

                    List<CartDetail> item = db.ExecuteQuery<CartDetail>(@"SELECT * FROM CartDetail where ID =" + str2 + " and TrangThaiNguoiMuaHang=3").ToList();
                    if (item.Count > 0)
                    {
                        #region Trừ vào ví của NCC
                        List<Entity.users> thanhtoan = Susers.Name_Text("select * from users where iuser_id=" + int.Parse(item[0].IDNhaCungCap.ToString()) + " ");
                        if (thanhtoan.Count > 0)
                        {
                            double Money = Convert.ToDouble(item[0].Money.ToString()) / 1000;
                            double TongTienCoinDuocCap = Convert.ToDouble(thanhtoan[0].TongTienCoinDuocCap.ToString());
                            double ConglaiCoin = ((TongTienCoinDuocCap) - (Money));
                            if (TongTienCoinDuocCap >= Money)
                            {
                                Susers.Name_Text("update users set TongTienCoinDuocCap='" + ConglaiCoin + "'  where iuser_id=" + thanhtoan[0].iuser_id.ToString() + "");
                            }
                            else
                            {
                                ltmess.Text = "<script type=\"text/javascript\">alert('Tài khoản nhà cung cấp không đủ để trả.');window.location.href='" + URL + "'; </script></div>";
                                return;
                                break;
                            }
                        }
                        #endregion

                        #region Thanh toán tiền cho ng mua
                        List<Entity.users> Nhanve = Susers.Name_Text("select * from users where iuser_id=" + int.Parse(item[0].IDThanhVien.ToString()) + " ");
                        if (Nhanve.Count > 0)
                        {
                            double Money = Convert.ToDouble(item[0].Money.ToString()) / 1000;
                            double TongTienCoinDuocCap = Convert.ToDouble(Nhanve[0].TongTienCoinDuocCap.ToString());
                            double ConglaiCoin = ((TongTienCoinDuocCap) + (Money));
                            Susers.Name_Text("update users set TongTienCoinDuocCap='" + ConglaiCoin + "'  where iuser_id=" + Nhanve[0].iuser_id.ToString() + "");
                        }
                        #endregion

                        SCartDetail.Name_Text("UPDATE [CartDetail] SET [TrangThaiNguoiMuaHang]=2 WHERE ID =" + str2 + "");
                        #region Gửi mail cho thành viên mua, NCC hoàn trả tiền
                        List<CartDetail> tablv = db.CartDetails.Where(s => s.ID == int.Parse(str2)).ToList();
                        if (tablv.Count > 0)
                        {
                            List<Entity.users> dc = Susers.GET_BY_ID(tablv[0].IDThanhVien.ToString());
                            if (dc.Count > 0)
                            {
                                string Emails = dc[0].vemail.ToString();
                                //Gửi email cho người mua
                                string Noidung = "";
                                Noidung += "Kính gửi: <b>" + dc[0].vfname.ToString() + "</b><br />";

                                Noidung += "Đơn hàng <a href=\"http://lienhiephoptac.vn/account/orders/" + tablv[0].ID_Cart.ToString() + "\" target=\"_blank\"><b>#" + tablv[0].ID_Cart.ToString() + "</b></a> đã được hoàn tiền . Quý khách vui lòng kiểm tra lại.";

                                Noidung += "<br />";
                                Noidung += "<br />";
                                Noidung += "<b>Tên sản phẩm  </b>: " + Commond.ShowPro(tablv[0].ipid.ToString()) + "<br />";
                                Noidung += "<b>Số lượng sản phẩm  </b>: " + tablv[0].Quantity.ToString() + "<br />";
                                Noidung += "<b>Tổng số tiền  </b>: " + tablv[0].Money.ToString() + "<br />";

                                Noidung += "---------------------------------";
                                Noidung += "<br />";

                                Noidung += "THÔNG TIN NGƯỜI MUA HÀNG:";
                                Noidung += "<br />";
                                Noidung += "<b>Người mua hàng: </b>" + dc[0].vfname.ToString() + "<br />";
                                Noidung += "<b>Địa chỉ: </b>" + dc[0].vaddress.ToString() + "<br />";
                                Noidung += "<b>Điện thoại: </b>" + dc[0].vphone.ToString() + "<br />";
                                Noidung += "<b>Email: </b>" + dc[0].vemail.ToString() + "<br />";

                                Noidung += "---------------------------------";
                                Noidung += "<br />";

                                Noidung += "THÔNG TIN NHÀ CUNG CẤP:";
                                Noidung += "<br />";
                                Noidung += ShowNameNCC(tablv[0].IDNhaCungCap.ToString()) + "<br />";
                                Noidung += "<br />";
                                Noidung += "<br />";
                                Noidung += Commond.Setting("txtfooterEmail");
                                Noidung += "<br />";
                                try
                                {
                                    new MailHelper().SendMail(ShowEmailNhaCungCap(tablv[0].IDNhaCungCap.ToString()), "Hoàn tiền từ webiste: " + Request.Url.Host + " - Mã đơn hàng #" + tablv[0].ID_Cart.ToString() + " ", Noidung.ToString());
                                    new MailHelper().SendMail(dc[0].vemail.ToString(), "Hoàn tiền từ webiste: " + Request.Url.Host + " - Mã đơn hàng #" + tablv[0].ID_Cart.ToString() + " ", Noidung.ToString());
                                }
                                catch {
                                    ltmess.Text = "<script type=\"text/javascript\">alert('Hoàn tiền lại cho người mua thành công. Hãy Nêu lý do hủy đơn vào ô lý do.');window.location.href='" + URL + "'; </script></div>";
                                }
                            }
                        }
                        #endregion
                        return;
                    }
                    #endregion

                    ltmess.Text = "<script type=\"text/javascript\">alert('Hoàn tiền lại cho người mua thành công. Hãy Nêu lý do hủy đơn vào ô lý do.');window.location.href='" + URL + "'; </script></div>";
                    return;
            }
        }

        public string ShowEmailNhaCungCap(string id)
        {
            string code = "0";
            List<Entity.users> dt = Susers.GET_BY_ID(id);
            if (dt.Count > 0)
            {
                code = dt[0].vemail.ToString();
            }
            return code;
        }
        public string ShowNameNhaCungCap(string id)
        {
            string code = "0";
            List<Entity.users> dt = Susers.GET_BY_ID(id);
            if (dt.Count > 0)
            {
                code = dt[0].vfname.ToString();
            }
            return code;
        }
        public string ShowNameNCC(string id)
        {
            string Noidung = "";
            List<Entity.users> dt = Susers.GET_BY_ID(id);
            if (dt.Count > 0)
            {
                Noidung += "<b>Tên nhà cung cấp: </b>" + dt[0].vfname.ToString() + "<br />";
                Noidung += "<b>Email: </b>" + dt[0].vemail.ToString() + "<br />";
                Noidung += "<b>Điện thoại: </b>" + dt[0].vphone.ToString() + "<br />";
            }
            return Noidung;
        }
        protected string ShowtrangthaiNMH(string status)
        {
            if (status.Equals("1"))
            {
                return "<span style='background:#939393;padding: 4px;margin:5px;color:#fff;border-radius: 3px;'>Chấp Nhận</span>";
            }
            if (status.Equals("2"))
            {
                return "<span style='background:#939393;padding: 4px;margin:5px;color:#fff;border-radius: 3px;'>Trả hàng</span>";
            }
            return "";
        }
        protected string ShowtrangthaiNCC(string status, string LyDoHuyHang)
        {
            if (status.Equals("1"))
            {
                return "<a style='background:#0b98ea;padding: 4px;margin:5px;color:#fff;border-radius: 3px;' title='Nhà cung cấp đã duyệt đơn hàng này.'>Đã duyệt</a>";
            }
            else if (status.Equals("3"))
            {
                return "<a style='background:#ffa903;padding: 4px;margin:5px;color:#fff;border-radius: 3px;' title='Nhà cung cấp chưa xử lý đơn hàng này'>Chưa xử lý</a>";
            }
            else if (status.Equals("2"))
            {
                string ld = "";
                if (LyDoHuyHang.Length > 0)
                {
                    ld = "<span class='lydohuyhang'>" + LyDoHuyHang + "</span>";
                }
                return "<a style='background:#ed1c24;padding: 4px;margin:5px;color:#fff;border-radius: 3px;'  title='Nhà cung cấp chưa đã hủy đơn hàng này'>Hủy đơn</a><br>" + ld + "";
            }
            else if (status.Equals("4"))
            {
                return "<a style='background:#669d0b;padding: 4px;margin:5px;color:#fff;border-radius: 3px;' title='Nhà cung cấp đang xử lý cho người mua'>Đang chờ xử lý</a>";
            }
            else if (status.Equals("5"))
            {
                return "<a style='background:#0872bb;padding: 4px;margin:5px;color:#fff;border-radius: 3px;' title='Nhà cung cấp đã hoàn lại tiền cho hàng đơn hàng này của bạn'>Hoàn tiền</a>";
            }
            else if (status.Equals("6"))
            {
                return "<a style='background:#0872bb;padding: 4px;margin:5px;color:#fff;border-radius: 3px;' title='Khiếu kiện lên Admin'>Khiếu kiện</a><a style='margin:5px;color: #a50606;float: left;;' title='Đang chờ Admin xử lý'>Đang chờ Admin xử lý</a>";
            }
            else if (status.Equals("7"))
            {
                return "<a style='background:#0872bb;padding: 4px;margin:5px;color:#fff;border-radius: 3px;' title='Admin Chấp nhận thanh toán'>Admin Chấp nhận TT</a>";
            }
            else if (status.Equals("8"))
            {
                return "<a style='background:#0872bb;padding: 4px;margin:5px;color:#fff;border-radius: 3px;' title='Admin Hoàn tiền'>Admin Hoàn tiền</a>";
            }
            return "";
        }
        protected string ShowtrangthaiKhieuKien(string status)
        {
            if (status.Equals("2"))
            {
                return "<a style='background:#0b98ea;padding: 4px;margin:5px;color:#fff;border-radius: 3px;' title='Chấp nhận thanh toán'>Chấp nhận thanh toán</a>";
            }
            if (status.Equals("3"))
            {
                return "<a style='background:#0b98ea;padding: 4px;margin:5px;color:#fff;border-radius: 3px;' title='Chấp nhận hoàn tiền'>Chấp nhận hoàn tiền</a>";
            }
            return "";
        }
        protected bool EnableLock(string id)
        {
            List<Carts> table2 = SCarts.Carts_GetById(id.ToString());
            if (table2.Count > 0)
            {
                if (table2[0].Status.ToString() == "1")
                {
                    return false;
                }
            }
            return true;
        }
        protected bool EnableLock_KhieuKien(string Enable)
        {
            // 1: Đã duyệt
            // 2: Hủy đơn hàng
            // 3: Chờ xử lý
            // 4: Bị người mua trả lại hàng
            // 5: Hoàn tiền
            // 6: Khiếu kiện gửi admin
            // 7: Admin chập nhận thanh toán
            // 8: Admin chập nhận Hoàn tiền
            if (Enable.ToString() == "1")
            {
                return true;
            }
            return false;
        }
        protected bool EnableLock_HuyDonHang(string Enable)
        {
            // 1: Đã duyệt
            // 2: Hủy đơn hàng
            // 3: Chờ xử lý
            // 4: Bị người mua trả lại hàng
            // 5: Hoàn tiền
            // 6: Khiếu kiện gửi admin
            if (Enable.ToString() == "2")
            {
                return true;
            }
            return false;
        }
        protected bool EnableLock_DuyetHang(string Enable)
        {
            // 1: Đã duyệt
            // 2: Hủy đơn hàng
            // 3: Chờ xử lý
            // 4: Bị người mua trả lại hàng
            // 5: Hoàn tiền
            // 6: Khiếu kiện gửi admin
            if (Enable.ToString() == "1" || Enable.ToString() == "2")
            {
                return false;
            }
            return true;
        }

        protected string NhaCungCapDaDuyet(string Enable)
        {
            // 1: Đã duyệt
            // 2: Hủy đơn hàng
            // 3: Chờ xử lý
            // 4: Bị người mua trả lại hàng
            // 5: Hoàn tiền
            // 6: Khiếu kiện gửi admin
            if (Enable.ToString() == "1")
            {
                return "display:block";
            }
            return "display:none";
        }
        protected void ChapNhan_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn muốn chấp nhận sản phẩm này. Đồng nghĩa là sẽ không có phát sinh tranh chấp nào.?')";
        }
        protected void KhieuKien_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn muốn chấp nhận giao dịch này, và tiền sẽ thanh toán cho nhà cung cấp?')";
        }
        protected void HoanTienMua_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Bạn muốn hủy và sẽ hoàn tiền cho người mua.?')";
        }
        protected void TraHang_Load(object sender, EventArgs e)
        {
            ((LinkButton)sender).Attributes["onclick"] = "return confirm('Hoàn tiền. Hủy đơn hàng')";
        }
        protected void txtLyDoTraHang_TextChanged(object sender, EventArgs e)
        {
            TextBox LyDo = (TextBox)sender;
            var b = (HiddenField)LyDo.FindControl("hdids");
            if (LyDo.Text.Length > 10)
            {
                List<Entity.CartDetail> item = SCartDetail.GetDetail(b.Value);
                if (item.Count > 0)
                {
                    List<Entity.CartDetail> list = SCartDetail.Name_Text("select * from CartDetail where id=" + b.Value + "  ");
                    if (list.Count > 0)
                    {
                        SCartDetail.Name_Text("UPDATE [CartDetail] SET [LyDoTraHang] =N'" + LyDo.Text + "' WHERE ID =" + b.Value + "");
                    }
                    ltmess.Text = "<script type=\"text/javascript\">alert('Nhập lý do hủy đơn thành công.');window.location.href='" + URL + "'; </script></div>";
                }
            }
            else
            {
                ltmess.Text = "<script type=\"text/javascript\">alert('Vui lòng nhập lý do hủy đơn trên 10 ký tự.');window.location.href='" + URL + "'; </script></div>";
            }
        }
       
    }
}

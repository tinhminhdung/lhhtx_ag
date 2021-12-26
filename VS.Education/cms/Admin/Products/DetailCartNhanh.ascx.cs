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
using TestWindowService;
using System.Configuration;

namespace VS.E_Commerce.cms.Admin.Products
{
    public partial class DetailCartNhanh : System.Web.UI.UserControl
    {
        private string status = "1";
        public int i = 1;
        protected bool Dung = false;
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
            //List<CartDetail> table = db.CartDetails.OrderByDescending(s => s.ID_Cart == Convert.ToInt32(id)).ToList();
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
                if (table[0].ThanhVienFree_DaiLy.ToString() == "0")
                {
                    ltthanhvien.Text = "<span style='background:#ffa903;padding: 4px;margin:5px;color:#fff;border-radius: 3px;'> Thành viên Free </span>";
                }
                else if (table[0].ThanhVienFree_DaiLy.ToString() == "2")
                {
                    ltthanhvien.Text = "<span style='background:#5f9902;padding: 4px;margin:5px;color:#fff;border-radius: 3px;'>  Cửa hàng </span> <img src=\"/Resources/images/cuahang.jpg\" style=\"width: 44px; \">";
                }
                else
                {
                    ltthanhvien.Text = "<span style='background:#5f9902;padding: 4px;margin:5px;color:#fff;border-radius: 3px;'>  Đại Lý </span>";
                }

                //double num2 = 0.0;
                //double num = 0.0;
                //for (int i = 0; i < table.Count; i++)
                //{
                //    num += Convert.ToDouble(table[i].Money.ToString());
                //    num2 += Convert.ToDouble(table[i].Diemcoin.ToString());
                //}
                //totalvnd = num;
                //Diemcoins = num2;
                //double TongCoin = (Diemcoins);/// 100;
                /// 
                //#region Show % Hoa Hồng theo thời gian mua hàng
                ////Xem thành viên đang ở level nào rồi nhân với level đó
                // List<Carts> table2 = SCarts.Carts_GetById(table[0].ID_Cart.ToString());
                // if (table2.Count > 0)
                // {

                // //    //// Nếu đã đặt hàng thành công xong rồi trạng thái status =1 thì sẽ phải lấy HoaHongTheoLevel trong bảng cartdetail để làm căn cứ lịch sử lúc mua tại thời điểm đó chỉ dc bằng đó %
                // //    //// if (table2[0].Status == 1)
                // //    //{
                // //    //    string CapoLevelHoaHongs = (table[0].HoaHongTheoLevel.ToString());
                // //    //    double HoaHongs = Convert.ToDouble(CapoLevelHoaHongs);
                // //    //    double Tong = (TongCoin * HoaHongs) / 100;
                // //    //    this.ltCoin.Text = HoaHongs + "% của / Tổng " + Diemcoins + " điểm = " + Tong + " điểm ";
                //}
                //    //else
                //    //{
                //    //    // Nếu chưa đặt hàng thì trọc vào bảng thành viên lấy hoa hồng hiện tại thành viên đang ở level mấy  ra nhé
                //    //    user tables = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(table[0].IDThanhVien.ToString()));// Type=1 là thành viên  hoặc là ng mua hàng , type=2 là nhà cung cấp
                //    //    if (tables != null)
                //    //    {
                //    //        string CapoLevelHoaHongs = CapoLevelHoaHong(tables.LevelThanhVien.ToString());
                //    //        double HoaHongs = Convert.ToDouble(CapoLevelHoaHongs);
                //    //        double Tong = (TongCoin * HoaHongs) / 100;
                //    //        this.ltCoin.Text = HoaHongs + "% của / Tổng " + Diemcoins + " điểm = " + Tong + " điểm ";
                //    //    }
                //    //}
                //}
                //#endregion
                // lttong.Text = AllQuery.MorePro.Detail_Price(table[0].TongTienThanhToan.ToString());
            }


            List<Entity.Carts> dt1 = SCarts.Carts_GetById(id);
            if (dt1.Count > 0)
            {
                if (ShowSuKienMua(dt1[0].ID.ToString()) == true)
                {
                    btduyet.Visible = true;
                }
                lttongdiem.Text = dt1[0].Money.ToString();
                ltngaydathang.Text = dt1[0].Create_Date.ToString();
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
        protected string LayTuViNao(string TienTuViNao)
        {
            if (TienTuViNao == "1")
            {
                return "<span style='background:#939393;padding: 4px;margin:5px;color:#fff;border-radius: 3px;'>Ví Mua Hàng</span>";
            }
            else if (TienTuViNao == "2")
            {
                return "<span style='background:#939393;padding: 4px;margin:5px;color:#fff;border-radius: 3px;'>Ví Thương mại</span>";
            }
            return "";
        }
        protected string ShowGiaThanhVien(string id)
        {
            if (id == "1")
            {
                return "<div style=\"background:red;padding: 4px;margin:10px;color:#fff;border-radius: 3px;font-weight: 600;\">Mua theo giá thành viên</div>";
            }
            return "";
        }

        protected string ThanhVienFree_DaiLy(string id, string Free, string Daily)
        {
            if (id == "0")
            {
                return "Free: " + Free;
            }
            return "Đại lý: " + Daily;
        }
        protected void btduyet_Click(object sender, EventArgs e)
        {
            string TimF1Agland = "0";
            Double TongTienDonHangKichHoatThanhVien = 0;
            Double ThanhVienKichHoat = 0;
            LCart dt1 = db.LCarts.SingleOrDefault(p => p.ID == int.Parse(hdIDGiohang.Value));
            if (dt1 != null)
            {
                TongTienDonHangKichHoatThanhVien = Convert.ToDouble(dt1.TongTienSPChienLuocKichHoatTV.ToString());
                List<Entity.CartDetail> dtcart = SCartDetail.Detail_ID_Cart(hdIDGiohang.Value);
                if (dtcart.Count > 0)
                {
                    List<CartDetail> abk = db.CartDetails.Where(p => p.ID_Cart == int.Parse(hdIDGiohang.Value) && p.IDNhaCungCap.ToString() == "0").ToList();
                    if (abk.Count > 0)
                    {
                        ltmess.Text = "<div class=\"thongbaosvv\">Không thể thực hiện khi chưa biết sản phẩm trong đơn hàng thuộc nhà cung cấp nào</div> ";
                    }
                    else
                    {
                        ThanhVienKichHoat = Convert.ToInt32(dtcart[0].ThanhVienFree_DaiLy.ToString());

                        #region Tính tiền trừ vào bảng User TongTienCoinDuocCap
                        //string str = "";
                        //user iiit = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(dt1.IDThanhVien.ToString()));//&& p.ChiNhanh == 0
                        //if (iiit != null)
                        //{
                        //    double ViHienTaiCoin = Convert.ToDouble(iiit.TongTienCoinDuocCap);
                        //    // Quy đổi VND ra số Coin
                        //    double SoTienPhaiThanhToanCoin = Convert.ToDouble(hdtongtien.Value);
                        //    double SoTienCoin = (SoTienPhaiThanhToanCoin) / 1000;

                        //    double ViAFF = Convert.ToDouble(iiit.ViMuaHangAFF);// lấy hoa hồng quản lý (AFF) đi mua hàng
                        //    // 0 : Vi AFF
                        //    // 1: Ví Thương mại
                        //    if (ViAFF >= SoTienCoin)// trừ tiền ở ví hoa hồng aff
                        //    {
                        //        double ConglaiCoin = ((ViAFF) - (SoTienCoin));
                        //        iiit.ViMuaHangAFF = ConglaiCoin.ToString();
                        //        db.SubmitChanges();
                        //        //TienTuViNao 1 là ví aff
                        //        SCartDetail.Name_Text("update CartDetail set TienTuViNao=1  where ID_Cart=" + hdIDGiohang.Value + "");
                        //        // Susers.Name_Text("update users set ViMuaHangAFF=" + ConglaiCoin.ToString() + "  where iuser_id=" + dt1.IDThanhVien.ToString() + "");
                        //        ltmess.Text = "<div class=\"thongbaos\">Bạn đã thanh toán thành công đơn hàng.</div> ";
                        //    }
                        //    else if (ViHienTaiCoin >= SoTienCoin)// trừ tiền ví thương mại
                        //    {
                        //        double ConglaiCoin = ((ViHienTaiCoin) - (SoTienCoin));
                        //        iiit.TongTienCoinDuocCap = ConglaiCoin.ToString();
                        //        db.SubmitChanges();
                        //        //TienTuViNao 2 là ví Thương mại
                        //        SCartDetail.Name_Text("update CartDetail set TienTuViNao=2  where ID_Cart=" + hdIDGiohang.Value + "");

                        //        // Susers.Name_Text("update users set TongTienCoinDuocCap=" + ConglaiCoin.ToString() + "  where iuser_id=" + dt1.IDThanhVien.ToString() + "");
                        //        ltmess.Text = "<div class=\"thongbaos\">Bạn đã thanh toán thành công đơn hàng.</div> ";
                        //    }

                        //    else
                        //    {
                        //        ltmess.Text = "<div class=\"thongbaos\">Số tiền trong ví không đủ để thanh toán.</div> ";
                        //        return;
                        //    }
                        //}
                        //else
                        //{
                        //    ltmess.Text = "<div class=\"thongbaos\">Số tiền trong ví không đủ để thanh toán</div> ";
                        //    return;
                        //}
                        #endregion

                        #region Xóa tiền ở bảng tạm
                        for (int p = 0; p < dtcart.Count; p++)
                        {
                            ViTamMuaHang delv = db.ViTamMuaHangs.Where(s => s.IDCartDetail == int.Parse(dtcart[p].ID.ToString()) && s.NCCDuyet == 1).FirstOrDefault();// xóa 1
                            if (delv != null)
                            {
                                db.ViTamMuaHangs.DeleteOnSubmit(delv);
                                db.SubmitChanges();
                            }
                        }
                        #endregion

                        #region Gửi mail cho nhà cung cấp khi thành viên mua hủy sản phẩm.
                        List<Entity.users> dc = Susers.GET_BY_ID(dtcart[0].IDThanhVien.ToString());
                        if (dc.Count > 0)
                        {
                            //Gửi email
                            string Noidung = "";
                            Noidung += "<b>Duyệt nhanh đơn hàng thành công. Đơn hàng đã được chấp nhận và người mua đã nhận được hàng.! </b><br /> Đơn hàng <a href=\"http://lienhiephoptac.vn/account/orders/" + dtcart[0].ID_Cart.ToString() + "\" target=\"_blank\"><b>#" + dtcart[0].ID_Cart.ToString() + "</b></a> .<br />";
                            Noidung += "<br />";
                            for (int k = 0; k < dtcart.Count; k++)
                            {
                                Noidung += "<b>Tên sản phẩm :  </b>" + Commond.ShowPro(dtcart[k].ipid.ToString()) + "<br />";
                                Noidung += "<b>Số lượng sản phẩm :  </b>" + dtcart[k].Quantity.ToString() + "<br />";
                                Noidung += "<b>Tổng số tiền :  </b>" + dtcart[k].Money.ToString() + "<br />";

                            }
                            Noidung += "---------------------------------";
                            Noidung += "<br />";
                            Noidung += "THÔNG TIN NGƯỜI MUA HÀNG";
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
                                new MailHelper().SendMail(ShowEmailNhaCungCap(dtcart[0].IDNhaCungCap.ToString()), "Duyệt nhanh đơn hàng từ webiste: " + Request.Url.Host + " - Mã đơn hàng #" + dtcart[0].ID_Cart.ToString() + " ", Noidung.ToString());
                                new MailHelper().SendMail(dc[0].vemail.ToString(), "Duyệt nhanh đơn hàng từ webiste: " + Request.Url.Host + " - Mã đơn hàng #" + dtcart[0].ID_Cart.ToString() + " ", Noidung.ToString());
                            }
                            catch { }
                        }
                        #endregion

                        #region Tính Hoa Hồng
                        for (int i = 0; i < dtcart.Count; i++)
                        {
                            #region Công tiền khi người mua đã thanh toán bằng ví khi mua sản phẩm --> cho nhà cung cấp sản phẩm đăng bán
                            string IDNhaCungCapBanHang = dtcart[i].IDNhaCungCap.ToString();
                            if (IDNhaCungCapBanHang != "0")
                            {
                                // Lấy giá gốc nhập vào rồi nhân cho số lượng
                                // Ví dụ: chính là lấy số 142000 chứ ko phải lấy 158000 nhé

                                ThemHoaHongNCC(dtcart[i].ipid.ToString(), "30", "Thanh toán tiền đơn hàng cho nhà Cung cấp", dtcart[i].IDThanhVien.ToString(), IDNhaCungCapBanHang, "0", dtcart[i].ThanhToanNCC.ToString(), hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                if (dtcart[i].ThanhVienFree_DaiLy.ToString() == "0")// 0 là free/ 1 là đại lý
                                {
                                    ThemHoaHong(dtcart[i].ipid.ToString(), "300", "Tặng cho thành viên Free", dtcart[i].IDThanhVien.ToString(), dtcart[i].IDThanhVien.ToString(), "0", dtcart[i].TangThanhVienFree.ToString(), hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                }
                                if (dtcart[i].CongDiemVechoAg.ToString() != "0")
                                {
                                    ThemHoaHong(dtcart[i].ipid.ToString(), "301", "Tiền đổ về công ty", dtcart[i].IDThanhVien.ToString(), "0", "0", dtcart[i].CongDiemVechoAg.ToString(), hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                }
                                try
                                {
                                    Commond.CongSanPhamDaBan(IDNhaCungCapBanHang, dtcart[i].Quantity.ToString());
                                }
                                catch (Exception)
                                { }
                            }
                            #endregion

                            //1: thưởng cho người mua hàng trực tiếp dc hưởng 30% theo level nhé
                            //2: thưởng cho người giới thiệu
                            //3: Gián tiếp : thưởng cho người giới thiệu là 10%
                            //4: Nâng level cho thành viên khi đủ điểm
                            string Plevel = "99999999999";
                            string TongLevel = "0";

                            //TH1 : Nếu thành viên và leader mua hàng mua hàng
                            user table = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(dtcart[i].IDThanhVien.ToString()));// Type=1 là thành viên  hoặc là ng mua hàng , type=2 là nhà cung cấp
                            if (table != null)
                            {
                                string TrangThaiAgLang = dtcart[i].TrangThaiAgLang.ToString();// 1 = sản phẩm , 2= bất động sản // TrangThaiAgLang này khi đặt hàng là lấy trạng thái từ sản phẩm đưa vào giỏ hàng để so sánh
                                // Kiểm tra nếu điểm thưởng mà nhỏ hơn 0 thì sẽ ko phát sinh hoa hồng nào nhé.
                                //Tiền Coin
                                //string CapoLevelHoaHongs = CapoLevelHoaHong(table.LevelThanhVien.ToString());
                                //double HoaHongs = Convert.ToDouble(CapoLevelHoaHongs);

                                string CapoLevelHoaHongs = MoreAll.Other.Giatri("txtHoaHongGioiThieuTrucTiepmuahangVaF1");//dtcart[i].HoaHongTheoLevel.ToString();
                                double HoaHongs = Convert.ToDouble(MoreAll.Other.Giatri("txtHoaHongGioiThieuTrucTiepmuahangVaF1"));// Convert.ToDouble(dtcart[i].HoaHongTheoLevel.ToString());

                                double Diemcoin = Convert.ToDouble(dtcart[i].TongDiemDemDiChia.ToString());
                                double TongCoin = (Diemcoin);/// 100;
                                double Tong = (TongCoin * HoaHongs) / 100;
                                if (TongCoin > 0)// Kiểm tra nếu điểm thưởng mà nhỏ hơn 0 thì sẽ ko phát sinh hoa hồng nào nhé.
                                {
                                    #region Đối với mua hàng trực tiếp & hoa hồng & Chi Nhánh

                                    #region Phát sinh hoa hồng giới thiệu Và thưởng thêm theo level
                                    // Ngày 7/01/2020 phát sinh yêu cầu về AGlang
                                    // Khi có đơn hàng nào thuộc Thành viên (TrangThaiAgLang) AgLang giới thiệu thì mới được hoa hồng , nếu ai ko thuộc thành viên aglang thì ko được ăn hoa hồng 
                                    // Về phía sản phẩm thì vẫn phát sinh hoa hồng bình thường.
                                    if (table != null)
                                    {
                                        string IDThanhVien = table.iuser_id.ToString();// ID thành viên mua

                                        #region Tính hoa hồng cho kiểu sản phẩm (TrangThaiAgLang=1)
                                        if (TrangThaiAgLang == "1")
                                        {

                                            #region Ví chuyên gia mua bán
                                            try
                                            {
                                                user ChuyenGiga = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(SetVi.SetThanhVienChuyenGia()));
                                                if (ChuyenGiga != null)
                                                {
                                                    string IDBanChuyenGia = SetVi.SetThanhVienChuyenGia();
                                                    double HHChuyengia = Convert.ToDouble(Commond.Setting("txtbanchuyengia"));
                                                    double TienHH = Convert.ToDouble(Diemcoin * HHChuyengia) / 100;
                                                    ThemHoaHong(dtcart[i].ipid.ToString(), "401", "Hoa hồng Mua Bán - Ban Đào tạo - Chuyên gia ", IDThanhVien.Trim().ToLower(), IDBanChuyenGia.ToString(), HHChuyengia.ToString(), TienHH.ToString(), hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                }
                                            }
                                            catch (Exception)
                                            { }
                                            #endregion

                                            #region Ví doanh số đồng hưởng
                                            try
                                            {
                                                string IDDongHuong = SetVi.SetViDongHuongDoanhSo();
                                                double HHDH = Convert.ToDouble(Commond.Setting("txtdoanhsodonghuongmuaban"));
                                                double TienHH = Convert.ToDouble(Diemcoin * HHDH) / 100;

                                                ThemHoaHong(dtcart[i].ipid.ToString(), "403", "Hoa hồng  - Danh số đồng hưởng", IDThanhVien.Trim().ToLower(), IDDongHuong.ToString(), HHDH.ToString(), TienHH.ToString(), hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                MDoanhSoDongHuong.DoanhSoDongHuongMuaBan(TienHH.ToString(), dtcart[i].ipid.ToString(), IDThanhVien.ToString(), hdIDGiohang.Value, "");
                                            }
                                            catch (Exception)
                                            { }
                                            #endregion


                                            #region Hoa Hồng Quản lý
                                            try
                                            {
                                                double txtthuongquanly = Convert.ToDouble(MoreAll.Other.Giatri("txtthuongquanly"));
                                                if (!MoreAll.Other.Giatri("txtthuongquanly").Equals("0"))
                                                {
                                                    double TongLeader = (TongCoin * txtthuongquanly) / 100;
                                                    ThemHoaHong(dtcart[i].ipid.ToString(), "405", "Hoa Hồng (Thưởng Quản Lý)", table.iuser_id.ToString(), SetVi.SetViThuongQuanLy(), txtthuongquanly.ToString(), TongLeader.ToString(), hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                }
                                            }
                                            catch (Exception)
                                            { }
                                            #endregion

                                            if (table.GioiThieu.ToString() != "0")
                                            {
                                                //double HoaHongGioiThieuF1 = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongGioiThieuF1"));
                                                #region Hoa Hồng Gián tiếp F1
                                                // double HoaHongF1 = (Tong * HoaHongGioiThieuF1) / 100;
                                                double HoaHongF1 = Tong;
                                                if (table.GioiThieu.ToString() != "0")
                                                {
                                                    ThemHoaHong(dtcart[i].ipid.ToString(), "7", "Hoa hồng giới thiệu F1 ", IDThanhVien.Trim().ToLower(), table.GioiThieu.ToString(), CapoLevelHoaHongs.ToString(), HoaHongF1.ToString(), hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                    // Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                    // Nếu người mua hàng có level = 4 tức =45% thì dừng ko cho F1 được hưởng nữa
                                                    try
                                                    {
                                                        if (table.LevelThanhVien.ToString() == "5")
                                                        {
                                                            Dung = false;
                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                        }
                                                        else
                                                        {
                                                            Dung = true;
                                                            Plevel = Plevel + "," + table.LevelThanhVien.ToString();
                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F1", "75", IDThanhVien, table.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(table.GioiThieu.ToString()), TimLevelB(IDThanhVien), hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                        }
                                                    }
                                                    catch (Exception)
                                                    { }
                                                    #endregion
                                                }
                                                #region Hoa Hồng Gián tiếp F2
                                                user tableTVTF2 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(table.GioiThieu.ToString()));
                                                if (tableTVTF2 != null)
                                                {
                                                    double HoaHongGioiThieuF2 = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongGioiThieuF2"));
                                                    double HoaHongF2 = (Diemcoin * HoaHongGioiThieuF2) / 100;
                                                    if (tableTVTF2.GioiThieu.ToString() != "0")
                                                    {
                                                        ThemHoaHong(dtcart[i].ipid.ToString(), "71", "Hoa hồng giới thiệu F2 ", IDThanhVien.Trim().ToLower(), tableTVTF2.GioiThieu.ToString(), HoaHongGioiThieuF2.ToString(), HoaHongF2.ToString(), hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                        // Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                        try
                                                        {
                                                            if (tableTVTF2.LevelThanhVien.ToString() == "5")
                                                            {
                                                                Dung = false;
                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                            }
                                                            else
                                                            {
                                                                Dung = true;
                                                                Plevel = Plevel + "," + tableTVTF2.LevelThanhVien.ToString();
                                                            }
                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                            if (Plevel.ToString() == "99999999999")
                                                            {

                                                            }
                                                            else
                                                            {
                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                            }
                                                            if (Dung == true)
                                                            {
                                                                if (TongLevel != "8")// 8 là ko tìm thấy giá trị nào cao hơn 0 ở hàm MinAndMax
                                                                {
                                                                    if (TongLevel != "45")// 45 là ko hưởng hoa hồng nữa
                                                                    {
                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F2", "76", IDThanhVien, tableTVTF2.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF2.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F2", "76", IDThanhVien, tableTVTF2.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF2.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                }

                                                                //#region Dừng nếu gặp lelvel5
                                                                //string leveeeel = TimLevelB(tableTVTF2.GioiThieu.ToString());
                                                                //if (leveeeel == "5")
                                                                //{
                                                                //    Plevel = "45";
                                                                //}
                                                                //#endregion

                                                            }
                                                        }
                                                        catch (Exception)
                                                        { }
                                                        #endregion
                                                    }

                                                    #region Hoa Hồng Gián tiếp F3
                                                    user tableTVTF3 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF2.GioiThieu.ToString()));
                                                    if (tableTVTF3 != null)
                                                    {
                                                        double HoaHongGioiThieuF3 = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongGioiThieuF3"));
                                                        double HoaHongF3 = (Diemcoin * HoaHongGioiThieuF3) / 100;
                                                        if (tableTVTF3.GioiThieu.ToString() != "0")
                                                        {
                                                            ThemHoaHong(dtcart[i].ipid.ToString(), "72", "Hoa hồng giới thiệu F3 ", IDThanhVien.Trim().ToLower(), tableTVTF3.GioiThieu.ToString(), HoaHongGioiThieuF3.ToString(), HoaHongF3.ToString(), hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                            // Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                            try
                                                            {
                                                                if (tableTVTF3.LevelThanhVien.ToString() == "5")
                                                                {
                                                                    Dung = false;
                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                }
                                                                else
                                                                {
                                                                    Dung = true;
                                                                    Plevel = Plevel + "," + tableTVTF3.LevelThanhVien.ToString();
                                                                }
                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                if (Plevel.ToString() == "99999999999")
                                                                {

                                                                }
                                                                else
                                                                {
                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                }
                                                                if (Dung == true)
                                                                {
                                                                    if (TongLevel != "8")
                                                                    {
                                                                        if (TongLevel != "45")
                                                                        {
                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F3", "77", IDThanhVien, tableTVTF3.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF3.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F3", "77", IDThanhVien, tableTVTF3.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF3.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                    }
                                                                }
                                                            }
                                                            catch (Exception)
                                                            { }
                                                            #endregion

                                                        }
                                                        #region Hoa Hồng Gián tiếp F4
                                                        user tableTVTF4 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF3.GioiThieu.ToString()));
                                                        if (tableTVTF4 != null)
                                                        {
                                                            double HoaHongGioiThieuF4 = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongGioiThieuF4"));
                                                            double HoaHongF4 = (Diemcoin * HoaHongGioiThieuF4) / 100;
                                                            if (tableTVTF4.GioiThieu.ToString() != "0")
                                                            {
                                                                ThemHoaHong(dtcart[i].ipid.ToString(), "73", "Hoa hồng giới thiệu F4 ", IDThanhVien.Trim().ToLower(), tableTVTF4.GioiThieu.ToString(), HoaHongGioiThieuF4.ToString(), HoaHongF4.ToString(), hdIDGiohang.Value, dtcart[i].ipid.ToString());

                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                // Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa

                                                                try
                                                                {
                                                                    if (tableTVTF4.LevelThanhVien.ToString() == "5")
                                                                    {
                                                                        Dung = false;
                                                                        Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                    }
                                                                    else
                                                                    {
                                                                        Dung = true;
                                                                        Plevel = Plevel + "," + tableTVTF4.LevelThanhVien.ToString();
                                                                    }
                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                    if (Plevel.ToString() == "99999999999")
                                                                    {

                                                                    }
                                                                    else
                                                                    {
                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                    }
                                                                    if (Dung == true)
                                                                    {
                                                                        if (TongLevel != "8")
                                                                        {
                                                                            if (TongLevel != "45")
                                                                            {
                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F4", "78", IDThanhVien, tableTVTF4.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF4.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F4", "78", IDThanhVien, tableTVTF4.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF4.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                        }
                                                                    }
                                                                }
                                                                catch (Exception)
                                                                { }
                                                                #endregion
                                                            }

                                                            #region Hoa Hồng Gián tiếp F5
                                                            user tableTVTF5 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF4.GioiThieu.ToString()));
                                                            if (tableTVTF5 != null)
                                                            {
                                                                double HoaHongGioiThieuF5 = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongGioiThieuF5"));
                                                                double HoaHongF5 = (Diemcoin * HoaHongGioiThieuF5) / 100;
                                                                if (tableTVTF5.GioiThieu.ToString() != "0")
                                                                {
                                                                    ThemHoaHong(dtcart[i].ipid.ToString(), "74", "Hoa hồng giới thiệu F5", IDThanhVien.Trim().ToLower(), tableTVTF5.GioiThieu.ToString(), HoaHongGioiThieuF5.ToString(), HoaHongF5.ToString(), hdIDGiohang.Value, dtcart[i].ipid.ToString());

                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                    // Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                    try
                                                                    {
                                                                        if (tableTVTF5.LevelThanhVien.ToString() == "5")
                                                                        {
                                                                            Dung = false;
                                                                            Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                        }
                                                                        else
                                                                        {
                                                                            Dung = true;
                                                                            Plevel = Plevel + "," + tableTVTF5.LevelThanhVien.ToString();
                                                                        }
                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                        if (Plevel.ToString() == "99999999999")
                                                                        {

                                                                        }
                                                                        else
                                                                        {
                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                        }
                                                                        if (Dung == true)
                                                                        {
                                                                            if (TongLevel != "8")
                                                                            {
                                                                                if (TongLevel != "45")
                                                                                {
                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F5", "79", IDThanhVien, tableTVTF5.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF5.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F5", "79", IDThanhVien, tableTVTF5.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF5.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                            }
                                                                        }
                                                                    }
                                                                    catch (Exception)
                                                                    { }
                                                                    #endregion
                                                                }

                                                                #region Hoa Hồng Gián tiếp F6
                                                                user tableTVTF6 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF5.GioiThieu.ToString()));
                                                                if (tableTVTF6 != null)
                                                                {
                                                                    if (tableTVTF6.GioiThieu.ToString() != "0")
                                                                    {
                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                        // Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                        try
                                                                        {
                                                                            if (tableTVTF6.LevelThanhVien.ToString() == "5")
                                                                            {
                                                                                Dung = false;
                                                                                Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                            }
                                                                            else
                                                                            {
                                                                                Dung = true;
                                                                                Plevel = Plevel + "," + tableTVTF6.LevelThanhVien.ToString();
                                                                            }
                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                            if (Plevel.ToString() == "99999999999")
                                                                            {

                                                                            }
                                                                            else
                                                                            {
                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                            }
                                                                            if (Dung == true)
                                                                            {
                                                                                if (TongLevel != "8")
                                                                                {
                                                                                    if (TongLevel != "45")
                                                                                    {
                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F6", "55", IDThanhVien, tableTVTF6.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF6.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F6", "55", IDThanhVien, tableTVTF6.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF6.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                }
                                                                            }
                                                                        }
                                                                        catch (Exception)
                                                                        { }
                                                                        #endregion
                                                                    }

                                                                    #region Hoa Hồng Gián tiếp F7
                                                                    user tableTVTF7 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF6.GioiThieu.ToString()));
                                                                    if (tableTVTF7 != null)
                                                                    {
                                                                        if (tableTVTF7.GioiThieu.ToString() != "0")
                                                                        {
                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                            // Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                            try
                                                                            {
                                                                                if (tableTVTF7.LevelThanhVien.ToString() == "5")
                                                                                {
                                                                                    Dung = false;
                                                                                    Plevel = "45";// gán cho số 45 là sẽ phải dừng. số 45 chỉ là số ví dụ quy đinh thôi (Có thể thay số khác cũng dc nhé)
                                                                                }
                                                                                else
                                                                                {
                                                                                    Dung = true;
                                                                                    Plevel = Plevel + "," + tableTVTF7.LevelThanhVien.ToString();
                                                                                }
                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                if (Plevel.ToString() == "99999999999")
                                                                                {

                                                                                }
                                                                                else
                                                                                {
                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                }
                                                                                if (Dung == true)
                                                                                {
                                                                                    if (TongLevel != "8")
                                                                                    {
                                                                                        if (TongLevel != "45")
                                                                                        {
                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F7", "55", IDThanhVien, tableTVTF7.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF7.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F7", "55", IDThanhVien, tableTVTF7.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF7.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                    }
                                                                                }
                                                                            }
                                                                            catch (Exception)
                                                                            { }
                                                                            #endregion
                                                                        }

                                                                        #region Hoa Hồng Gián tiếp F8
                                                                        user tableTVTF8 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF7.GioiThieu.ToString()));
                                                                        if (tableTVTF8 != null)
                                                                        {
                                                                            if (tableTVTF8.GioiThieu.ToString() != "0")
                                                                            {
                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                try
                                                                                {
                                                                                    if (tableTVTF8.LevelThanhVien.ToString() == "5")
                                                                                    {
                                                                                        Dung = false;
                                                                                        Plevel = "45";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        Dung = true;
                                                                                        Plevel = Plevel + "," + tableTVTF8.LevelThanhVien.ToString();
                                                                                    }
                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                    {

                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                    }
                                                                                    if (Dung == true)
                                                                                    {
                                                                                        if (TongLevel != "8")
                                                                                        {
                                                                                            if (TongLevel != "45")
                                                                                            {
                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F8", "55", IDThanhVien, tableTVTF8.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF8.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F8", "55", IDThanhVien, tableTVTF8.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF8.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                        }
                                                                                    }
                                                                                }
                                                                                catch (Exception)
                                                                                { }
                                                                                #endregion
                                                                            }

                                                                            #region Hoa Hồng Gián tiếp F9
                                                                            user tableTVTF9 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF8.GioiThieu.ToString()));
                                                                            if (tableTVTF9 != null)
                                                                            {
                                                                                if (tableTVTF9.GioiThieu.ToString() != "0")
                                                                                {
                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                    try
                                                                                    {
                                                                                        if (tableTVTF9.LevelThanhVien.ToString() == "5")
                                                                                        {
                                                                                            Dung = false;
                                                                                            Plevel = "45";
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            Dung = true;
                                                                                            Plevel = Plevel + "," + tableTVTF9.LevelThanhVien.ToString();
                                                                                        }
                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                        {

                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                        }
                                                                                        if (Dung == true)
                                                                                        {
                                                                                            if (TongLevel != "8")
                                                                                            {
                                                                                                if (TongLevel != "45")
                                                                                                {
                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F9", "55", IDThanhVien, tableTVTF9.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF9.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F9", "55", IDThanhVien, tableTVTF9.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF9.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                    catch (Exception)
                                                                                    { }
                                                                                    #endregion
                                                                                }

                                                                                #region Hoa Hồng Gián tiếp F10
                                                                                user tableTVTF10 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF9.GioiThieu.ToString()));
                                                                                if (tableTVTF10 != null)
                                                                                {
                                                                                    if (tableTVTF10.GioiThieu.ToString() != "0")
                                                                                    {
                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                        try
                                                                                        {
                                                                                            if (tableTVTF10.LevelThanhVien.ToString() == "5")
                                                                                            {
                                                                                                Dung = false;
                                                                                                Plevel = "45";
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                Dung = true;
                                                                                                Plevel = Plevel + "," + tableTVTF10.LevelThanhVien.ToString();
                                                                                            }
                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                            {

                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                            }
                                                                                            if (Dung == true)
                                                                                            {
                                                                                                if (TongLevel != "8")
                                                                                                {
                                                                                                    if (TongLevel != "45")
                                                                                                    {
                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F10", "55", IDThanhVien, tableTVTF10.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF10.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                    }
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F10", "55", IDThanhVien, tableTVTF10.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF10.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                        catch (Exception)
                                                                                        { }
                                                                                        #endregion
                                                                                    }

                                                                                    #region Hoa Hồng Gián tiếp F11
                                                                                    user tableTVTF11 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF10.GioiThieu.ToString()));
                                                                                    if (tableTVTF11 != null)
                                                                                    {
                                                                                        if (tableTVTF11.GioiThieu.ToString() != "0")
                                                                                        {
                                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                            try
                                                                                            {
                                                                                                if (tableTVTF11.LevelThanhVien.ToString() == "5")
                                                                                                {
                                                                                                    Dung = false;
                                                                                                    Plevel = "45";
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    Dung = true;
                                                                                                    Plevel = Plevel + "," + tableTVTF11.LevelThanhVien.ToString();
                                                                                                }
                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                {

                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                }
                                                                                                if (Dung == true)
                                                                                                {
                                                                                                    if (TongLevel != "8")
                                                                                                    {
                                                                                                        if (TongLevel != "45")
                                                                                                        {
                                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F11", "55", IDThanhVien, tableTVTF11.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF11.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                        }
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F11", "55", IDThanhVien, tableTVTF11.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF11.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            catch (Exception)
                                                                                            { }
                                                                                            #endregion
                                                                                        }
                                                                                        #region Hoa Hồng Gián tiếp F12
                                                                                        user tableTVTF12 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF11.GioiThieu.ToString()));
                                                                                        if (tableTVTF12 != null)
                                                                                        {
                                                                                            if (tableTVTF12.GioiThieu.ToString() != "0")
                                                                                            {
                                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                try
                                                                                                {
                                                                                                    if (tableTVTF12.LevelThanhVien.ToString() == "5")
                                                                                                    {
                                                                                                        Dung = false;
                                                                                                        Plevel = "45";
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        Dung = true;
                                                                                                        Plevel = Plevel + "," + tableTVTF12.LevelThanhVien.ToString();
                                                                                                    }
                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                    {

                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                    }
                                                                                                    if (Dung == true)
                                                                                                    {
                                                                                                        if (TongLevel != "8")
                                                                                                        {
                                                                                                            if (TongLevel != "45")
                                                                                                            {
                                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F12", "55", IDThanhVien, tableTVTF12.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF12.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                            }
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F12", "55", IDThanhVien, tableTVTF12.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF12.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                                catch (Exception)
                                                                                                { }
                                                                                                #endregion
                                                                                            }

                                                                                            #region Hoa Hồng Gián tiếp F13
                                                                                            user tableTVTF13 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF12.GioiThieu.ToString()));
                                                                                            if (tableTVTF13 != null)
                                                                                            {
                                                                                                if (tableTVTF13.GioiThieu.ToString() != "0")
                                                                                                {
                                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                    try
                                                                                                    {
                                                                                                        if (tableTVTF13.LevelThanhVien.ToString() == "5")
                                                                                                        {
                                                                                                            Dung = false;
                                                                                                            Plevel = "45";
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            Dung = true;
                                                                                                            Plevel = Plevel + "," + tableTVTF13.LevelThanhVien.ToString();
                                                                                                        }
                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                        {

                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                        }
                                                                                                        if (Dung == true)
                                                                                                        {
                                                                                                            if (TongLevel != "8")
                                                                                                            {
                                                                                                                if (TongLevel != "45")
                                                                                                                {
                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F13", "55", IDThanhVien, tableTVTF13.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF13.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                }
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F13", "55", IDThanhVien, tableTVTF13.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF13.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                    catch (Exception)
                                                                                                    { }
                                                                                                    #endregion
                                                                                                }

                                                                                                #region Hoa Hồng Gián tiếp F14
                                                                                                user tableTVTF14 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF13.GioiThieu.ToString()));
                                                                                                if (tableTVTF14 != null)
                                                                                                {
                                                                                                    if (tableTVTF14.GioiThieu.ToString() != "0")
                                                                                                    {
                                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                        try
                                                                                                        {
                                                                                                            if (tableTVTF14.LevelThanhVien.ToString() == "5")
                                                                                                            {
                                                                                                                Dung = false;
                                                                                                                Plevel = "45";
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                Dung = true;
                                                                                                                Plevel = Plevel + "," + tableTVTF14.LevelThanhVien.ToString();
                                                                                                            }
                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                            {

                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                            }
                                                                                                            if (Dung == true)
                                                                                                            {
                                                                                                                if (TongLevel != "8")
                                                                                                                {
                                                                                                                    if (TongLevel != "45")
                                                                                                                    {
                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F14", "55", IDThanhVien, tableTVTF14.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF14.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                    }
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F14", "55", IDThanhVien, tableTVTF14.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF14.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                        catch (Exception)
                                                                                                        { }
                                                                                                        #endregion
                                                                                                    }

                                                                                                    #region Hoa Hồng Gián tiếp F15
                                                                                                    user tableTVTF15 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF14.GioiThieu.ToString()));
                                                                                                    if (tableTVTF15 != null)
                                                                                                    {
                                                                                                        if (tableTVTF15.GioiThieu.ToString() != "0")
                                                                                                        {
                                                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                            try
                                                                                                            {
                                                                                                                if (tableTVTF15.LevelThanhVien.ToString() == "5")
                                                                                                                {
                                                                                                                    Dung = false;
                                                                                                                    Plevel = "45";
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    Dung = true;
                                                                                                                    Plevel = Plevel + "," + tableTVTF15.LevelThanhVien.ToString();
                                                                                                                }
                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                {

                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                }
                                                                                                                if (Dung == true)
                                                                                                                {
                                                                                                                    if (TongLevel != "8")
                                                                                                                    {
                                                                                                                        if (TongLevel != "45")
                                                                                                                        {
                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F15", "55", IDThanhVien, tableTVTF15.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF15.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                        }
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F15", "55", IDThanhVien, tableTVTF15.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF15.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                            catch (Exception)
                                                                                                            { }
                                                                                                            #endregion
                                                                                                        }

                                                                                                        #region Hoa Hồng Gián tiếp F16
                                                                                                        user tableTVTF16 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF15.GioiThieu.ToString()));
                                                                                                        if (tableTVTF16 != null)
                                                                                                        {
                                                                                                            if (tableTVTF16.GioiThieu.ToString() != "0")
                                                                                                            {
                                                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                try
                                                                                                                {
                                                                                                                    if (tableTVTF16.LevelThanhVien.ToString() == "5")
                                                                                                                    {
                                                                                                                        Dung = false;
                                                                                                                        Plevel = "45";
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        Dung = true;
                                                                                                                        Plevel = Plevel + "," + tableTVTF16.LevelThanhVien.ToString();
                                                                                                                    }
                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                    {

                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                    }
                                                                                                                    if (Dung == true)
                                                                                                                    {
                                                                                                                        if (TongLevel != "8")
                                                                                                                        {
                                                                                                                            if (TongLevel != "45")
                                                                                                                            {
                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F16", "55", IDThanhVien, tableTVTF16.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF16.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                            }
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F16", "55", IDThanhVien, tableTVTF16.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF16.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                                catch (Exception)
                                                                                                                { }
                                                                                                                #endregion
                                                                                                            }

                                                                                                            #region Hoa Hồng Gián tiếp F17
                                                                                                            user tableTVTF17 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF16.GioiThieu.ToString()));
                                                                                                            if (tableTVTF17 != null)
                                                                                                            {
                                                                                                                if (tableTVTF17.GioiThieu.ToString() != "0")
                                                                                                                {
                                                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                    try
                                                                                                                    {
                                                                                                                        if (tableTVTF17.LevelThanhVien.ToString() == "5")
                                                                                                                        {
                                                                                                                            Dung = false;
                                                                                                                            Plevel = "45";
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            Dung = true;
                                                                                                                            Plevel = Plevel + "," + tableTVTF17.LevelThanhVien.ToString();
                                                                                                                        }
                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                        {

                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                        }
                                                                                                                        if (Dung == true)
                                                                                                                        {
                                                                                                                            if (TongLevel != "8")
                                                                                                                            {
                                                                                                                                if (TongLevel != "45")
                                                                                                                                {
                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F17", "55", IDThanhVien, tableTVTF17.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF17.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                }
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F17", "55", IDThanhVien, tableTVTF17.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF17.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                    catch (Exception)
                                                                                                                    { }
                                                                                                                    #endregion
                                                                                                                }

                                                                                                                #region Hoa Hồng Gián tiếp F18
                                                                                                                user tableTVTF18 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF17.GioiThieu.ToString()));
                                                                                                                if (tableTVTF18 != null)
                                                                                                                {
                                                                                                                    if (tableTVTF18.GioiThieu.ToString() != "0")
                                                                                                                    {
                                                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                        try
                                                                                                                        {
                                                                                                                            if (tableTVTF18.LevelThanhVien.ToString() == "5")
                                                                                                                            {
                                                                                                                                Dung = false;
                                                                                                                                Plevel = "45";
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                Dung = true;
                                                                                                                                Plevel = Plevel + "," + tableTVTF18.LevelThanhVien.ToString();
                                                                                                                            }
                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                            {

                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                            }
                                                                                                                            if (Dung == true)
                                                                                                                            {
                                                                                                                                if (TongLevel != "8")
                                                                                                                                {
                                                                                                                                    if (TongLevel != "45")
                                                                                                                                    {
                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F18", "55", IDThanhVien, tableTVTF18.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF18.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                    }
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F18", "55", IDThanhVien, tableTVTF18.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF18.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                        catch (Exception)
                                                                                                                        { }
                                                                                                                        #endregion
                                                                                                                    }

                                                                                                                    #region Hoa Hồng Gián tiếp F19
                                                                                                                    user tableTVTF19 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF18.GioiThieu.ToString()));
                                                                                                                    if (tableTVTF19 != null)
                                                                                                                    {
                                                                                                                        if (tableTVTF19.GioiThieu.ToString() != "0")
                                                                                                                        {
                                                                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                            try
                                                                                                                            {
                                                                                                                                if (tableTVTF19.LevelThanhVien.ToString() == "5")
                                                                                                                                {
                                                                                                                                    Dung = false;
                                                                                                                                    Plevel = "45";
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    Dung = true;
                                                                                                                                    Plevel = Plevel + "," + tableTVTF19.LevelThanhVien.ToString();
                                                                                                                                }
                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                {

                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                }
                                                                                                                                if (Dung == true)
                                                                                                                                {
                                                                                                                                    if (TongLevel != "8")
                                                                                                                                    {
                                                                                                                                        if (TongLevel != "45")
                                                                                                                                        {
                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F19", "55", IDThanhVien, tableTVTF19.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF19.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F19", "55", IDThanhVien, tableTVTF19.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF19.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            }
                                                                                                                            catch (Exception)
                                                                                                                            { }
                                                                                                                            #endregion
                                                                                                                        }
                                                                                                                        ///22222222222222
                                                                                                                        #region Hoa Hồng Gián tiếp F20
                                                                                                                        user tableTVTF20 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF19.GioiThieu.ToString()));
                                                                                                                        if (tableTVTF20 != null)
                                                                                                                        {
                                                                                                                            if (tableTVTF20.GioiThieu.ToString() != "0")
                                                                                                                            {
                                                                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                try
                                                                                                                                {
                                                                                                                                    if (tableTVTF20.LevelThanhVien.ToString() == "5")
                                                                                                                                    {
                                                                                                                                        Dung = false;
                                                                                                                                        Plevel = "45";
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        Dung = true;
                                                                                                                                        Plevel = Plevel + "," + tableTVTF20.LevelThanhVien.ToString();
                                                                                                                                    }
                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                    {

                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                    }
                                                                                                                                    if (Dung == true)
                                                                                                                                    {
                                                                                                                                        if (TongLevel != "8")
                                                                                                                                        {
                                                                                                                                            if (TongLevel != "45")
                                                                                                                                            {
                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F20", "55", IDThanhVien, tableTVTF20.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF20.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F20", "55", IDThanhVien, tableTVTF20.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF20.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                }
                                                                                                                                catch (Exception)
                                                                                                                                { }
                                                                                                                                #endregion
                                                                                                                            }
                                                                                                                            #region Hoa Hồng Gián tiếp F21
                                                                                                                            user tableTVTF21 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF20.GioiThieu.ToString()));
                                                                                                                            if (tableTVTF21 != null)
                                                                                                                            {
                                                                                                                                if (tableTVTF21.GioiThieu.ToString() != "0")
                                                                                                                                {
                                                                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                    try
                                                                                                                                    {
                                                                                                                                        if (tableTVTF21.LevelThanhVien.ToString() == "5")
                                                                                                                                        {
                                                                                                                                            Dung = false;
                                                                                                                                            Plevel = "45";
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            Dung = true;
                                                                                                                                            Plevel = Plevel + "," + tableTVTF21.LevelThanhVien.ToString();
                                                                                                                                        }
                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                        {

                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                        }
                                                                                                                                        if (Dung == true)
                                                                                                                                        {
                                                                                                                                            if (TongLevel != "8")
                                                                                                                                            {
                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                {
                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F21", "55", IDThanhVien, tableTVTF21.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF21.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F21", "55", IDThanhVien, tableTVTF21.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF21.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                    catch (Exception)
                                                                                                                                    { }
                                                                                                                                    #endregion
                                                                                                                                }

                                                                                                                                #region Hoa Hồng Gián tiếp F22
                                                                                                                                user tableTVTF22 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF21.GioiThieu.ToString()));
                                                                                                                                if (tableTVTF22 != null)
                                                                                                                                {
                                                                                                                                    if (tableTVTF22.GioiThieu.ToString() != "0")
                                                                                                                                    {
                                                                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                        try
                                                                                                                                        {
                                                                                                                                            if (tableTVTF22.LevelThanhVien.ToString() == "5")
                                                                                                                                            {
                                                                                                                                                Dung = false;
                                                                                                                                                Plevel = "45";
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                Dung = true;
                                                                                                                                                Plevel = Plevel + "," + tableTVTF22.LevelThanhVien.ToString();
                                                                                                                                            }
                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                            {

                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                            }
                                                                                                                                            if (Dung == true)
                                                                                                                                            {
                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                {
                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                    {
                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F22", "55", IDThanhVien, tableTVTF22.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF22.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F22", "55", IDThanhVien, tableTVTF22.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF22.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                        catch (Exception)
                                                                                                                                        { }
                                                                                                                                        #endregion
                                                                                                                                    }

                                                                                                                                    #region Hoa Hồng Gián tiếp F23
                                                                                                                                    user tableTVTF23 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF22.GioiThieu.ToString()));
                                                                                                                                    if (tableTVTF23 != null)
                                                                                                                                    {
                                                                                                                                        if (tableTVTF23.GioiThieu.ToString() != "0")
                                                                                                                                        {
                                                                                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                            try
                                                                                                                                            {
                                                                                                                                                if (tableTVTF23.LevelThanhVien.ToString() == "5")
                                                                                                                                                {
                                                                                                                                                    Dung = false;
                                                                                                                                                    Plevel = "45";
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    Dung = true;
                                                                                                                                                    Plevel = Plevel + "," + tableTVTF23.LevelThanhVien.ToString();
                                                                                                                                                }
                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                {

                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                }
                                                                                                                                                if (Dung == true)
                                                                                                                                                {
                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                    {
                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                        {
                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F23", "55", IDThanhVien, tableTVTF23.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF23.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F23", "55", IDThanhVien, tableTVTF23.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF23.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                            catch (Exception)
                                                                                                                                            { }
                                                                                                                                            #endregion
                                                                                                                                        }

                                                                                                                                        #region Hoa Hồng Gián tiếp F24
                                                                                                                                        user tableTVTF24 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF23.GioiThieu.ToString()));
                                                                                                                                        if (tableTVTF24 != null)
                                                                                                                                        {
                                                                                                                                            if (tableTVTF24.GioiThieu.ToString() != "0")
                                                                                                                                            {
                                                                                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                try
                                                                                                                                                {
                                                                                                                                                    if (tableTVTF24.LevelThanhVien.ToString() == "5")
                                                                                                                                                    {
                                                                                                                                                        Dung = false;
                                                                                                                                                        Plevel = "45";
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        Dung = true;
                                                                                                                                                        Plevel = Plevel + "," + tableTVTF24.LevelThanhVien.ToString();
                                                                                                                                                    }
                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                                    {

                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                    }
                                                                                                                                                    if (Dung == true)
                                                                                                                                                    {
                                                                                                                                                        if (TongLevel != "8")
                                                                                                                                                        {
                                                                                                                                                            if (TongLevel != "45")
                                                                                                                                                            {
                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F24", "55", IDThanhVien, tableTVTF24.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF24.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F24", "55", IDThanhVien, tableTVTF24.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF24.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                                catch (Exception)
                                                                                                                                                { }
                                                                                                                                                #endregion
                                                                                                                                            }

                                                                                                                                            #region Hoa Hồng Gián tiếp F25
                                                                                                                                            user tableTVTF25 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF24.GioiThieu.ToString()));
                                                                                                                                            if (tableTVTF25 != null)
                                                                                                                                            {
                                                                                                                                                if (tableTVTF25.GioiThieu.ToString() != "0")
                                                                                                                                                {
                                                                                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                    try
                                                                                                                                                    {
                                                                                                                                                        if (tableTVTF25.LevelThanhVien.ToString() == "5")
                                                                                                                                                        {
                                                                                                                                                            Dung = false;
                                                                                                                                                            Plevel = "45";
                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            Dung = true;
                                                                                                                                                            Plevel = Plevel + "," + tableTVTF25.LevelThanhVien.ToString();
                                                                                                                                                        }
                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                                        {

                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                        }
                                                                                                                                                        if (Dung == true)
                                                                                                                                                        {
                                                                                                                                                            if (TongLevel != "8")
                                                                                                                                                            {
                                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                                {
                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F25", "55", IDThanhVien, tableTVTF25.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF25.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F25", "55", IDThanhVien, tableTVTF25.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF25.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                    catch (Exception)
                                                                                                                                                    { }
                                                                                                                                                    #endregion
                                                                                                                                                }

                                                                                                                                                #region Hoa Hồng Gián tiếp F26
                                                                                                                                                user tableTVTF26 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF25.GioiThieu.ToString()));
                                                                                                                                                if (tableTVTF26 != null)
                                                                                                                                                {
                                                                                                                                                    if (tableTVTF26.GioiThieu.ToString() != "0")
                                                                                                                                                    {
                                                                                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                        try
                                                                                                                                                        {
                                                                                                                                                            if (tableTVTF26.LevelThanhVien.ToString() == "5")
                                                                                                                                                            {
                                                                                                                                                                Dung = false;
                                                                                                                                                                Plevel = "45";
                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                Dung = true;
                                                                                                                                                                Plevel = Plevel + "," + tableTVTF26.LevelThanhVien.ToString();
                                                                                                                                                            }
                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                                            {

                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                            }
                                                                                                                                                            if (Dung == true)
                                                                                                                                                            {
                                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                                {
                                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                                    {
                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F26", "55", IDThanhVien, tableTVTF26.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF26.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F26", "55", IDThanhVien, tableTVTF26.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF26.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                        catch (Exception)
                                                                                                                                                        { }
                                                                                                                                                        #endregion
                                                                                                                                                    }

                                                                                                                                                    #region Hoa Hồng Gián tiếp F27
                                                                                                                                                    user tableTVTF27 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF26.GioiThieu.ToString()));
                                                                                                                                                    if (tableTVTF27 != null)
                                                                                                                                                    {
                                                                                                                                                        if (tableTVTF27.GioiThieu.ToString() != "0")
                                                                                                                                                        {
                                                                                                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                            try
                                                                                                                                                            {
                                                                                                                                                                if (tableTVTF27.LevelThanhVien.ToString() == "5")
                                                                                                                                                                {
                                                                                                                                                                    Dung = false;
                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    Dung = true;
                                                                                                                                                                    Plevel = Plevel + "," + tableTVTF27.LevelThanhVien.ToString();
                                                                                                                                                                }
                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                                {

                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                }
                                                                                                                                                                if (Dung == true)
                                                                                                                                                                {
                                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                                    {
                                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                                        {
                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F27", "55", IDThanhVien, tableTVTF27.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF27.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                    else
                                                                                                                                                                    {
                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F27", "55", IDThanhVien, tableTVTF27.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF27.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                            catch (Exception)
                                                                                                                                                            { }
                                                                                                                                                            #endregion
                                                                                                                                                        }

                                                                                                                                                        #region Hoa Hồng Gián tiếp F28
                                                                                                                                                        user tableTVTF28 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF27.GioiThieu.ToString()));
                                                                                                                                                        if (tableTVTF28 != null)
                                                                                                                                                        {
                                                                                                                                                            if (tableTVTF28.GioiThieu.ToString() != "0")
                                                                                                                                                            {
                                                                                                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                try
                                                                                                                                                                {
                                                                                                                                                                    if (tableTVTF28.LevelThanhVien.ToString() == "5")
                                                                                                                                                                    {
                                                                                                                                                                        Dung = false;
                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                    }
                                                                                                                                                                    else
                                                                                                                                                                    {
                                                                                                                                                                        Dung = true;
                                                                                                                                                                        Plevel = Plevel + "," + tableTVTF28.LevelThanhVien.ToString();
                                                                                                                                                                    }
                                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                                                    {

                                                                                                                                                                    }
                                                                                                                                                                    else
                                                                                                                                                                    {
                                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                    }
                                                                                                                                                                    if (Dung == true)
                                                                                                                                                                    {
                                                                                                                                                                        if (TongLevel != "8")
                                                                                                                                                                        {
                                                                                                                                                                            if (TongLevel != "45")
                                                                                                                                                                            {
                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F28", "55", IDThanhVien, tableTVTF28.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF28.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                        else
                                                                                                                                                                        {
                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F28", "55", IDThanhVien, tableTVTF28.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF28.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                                catch (Exception)
                                                                                                                                                                { }
                                                                                                                                                                #endregion
                                                                                                                                                            }
                                                                                                                                                            ///88888888888888
                                                                                                                                                            #region Hoa Hồng Gián tiếp F29
                                                                                                                                                            user tableTVTF29 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF28.GioiThieu.ToString()));
                                                                                                                                                            if (tableTVTF29 != null)
                                                                                                                                                            {
                                                                                                                                                                if (tableTVTF29.GioiThieu.ToString() != "0")
                                                                                                                                                                {
                                                                                                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                    try
                                                                                                                                                                    {
                                                                                                                                                                        if (tableTVTF29.LevelThanhVien.ToString() == "5")
                                                                                                                                                                        {
                                                                                                                                                                            Dung = false;
                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                        }
                                                                                                                                                                        else
                                                                                                                                                                        {
                                                                                                                                                                            Dung = true;
                                                                                                                                                                            Plevel = Plevel + "," + tableTVTF29.LevelThanhVien.ToString();
                                                                                                                                                                        }
                                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                                                        {

                                                                                                                                                                        }
                                                                                                                                                                        else
                                                                                                                                                                        {
                                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                        }
                                                                                                                                                                        if (Dung == true)
                                                                                                                                                                        {
                                                                                                                                                                            if (TongLevel != "8")
                                                                                                                                                                            {
                                                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                                                {
                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F29", "55", IDThanhVien, tableTVTF29.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF29.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                            else
                                                                                                                                                                            {
                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F29", "55", IDThanhVien, tableTVTF29.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF29.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                    catch (Exception)
                                                                                                                                                                    { }
                                                                                                                                                                    #endregion
                                                                                                                                                                }

                                                                                                                                                                #region Hoa Hồng Gián tiếp F30
                                                                                                                                                                user tableTVTF30 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF29.GioiThieu.ToString()));
                                                                                                                                                                if (tableTVTF30 != null)
                                                                                                                                                                {
                                                                                                                                                                    if (tableTVTF30.GioiThieu.ToString() != "0")
                                                                                                                                                                    {
                                                                                                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                        try
                                                                                                                                                                        {
                                                                                                                                                                            if (tableTVTF30.LevelThanhVien.ToString() == "5")
                                                                                                                                                                            {
                                                                                                                                                                                Dung = false;
                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                            }
                                                                                                                                                                            else
                                                                                                                                                                            {
                                                                                                                                                                                Dung = true;
                                                                                                                                                                                Plevel = Plevel + "," + tableTVTF30.LevelThanhVien.ToString();
                                                                                                                                                                            }
                                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                                                            {

                                                                                                                                                                            }
                                                                                                                                                                            else
                                                                                                                                                                            {
                                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                            }
                                                                                                                                                                            if (Dung == true)
                                                                                                                                                                            {
                                                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                                                {
                                                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                                                    {
                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F30", "55", IDThanhVien, tableTVTF30.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF30.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                                else
                                                                                                                                                                                {
                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F30", "55", IDThanhVien, tableTVTF30.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF30.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                        catch (Exception)
                                                                                                                                                                        { }
                                                                                                                                                                        #endregion
                                                                                                                                                                    }

                                                                                                                                                                    #region Hoa Hồng Gián tiếp F31
                                                                                                                                                                    user tableTVTF31 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF30.GioiThieu.ToString()));
                                                                                                                                                                    if (tableTVTF31 != null)
                                                                                                                                                                    {
                                                                                                                                                                        if (tableTVTF31.GioiThieu.ToString() != "0")
                                                                                                                                                                        {
                                                                                                                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                            try
                                                                                                                                                                            {
                                                                                                                                                                                if (tableTVTF31.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                {
                                                                                                                                                                                    Dung = false;
                                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                                }
                                                                                                                                                                                else
                                                                                                                                                                                {
                                                                                                                                                                                    Dung = true;
                                                                                                                                                                                    Plevel = Plevel + "," + tableTVTF31.LevelThanhVien.ToString();
                                                                                                                                                                                }
                                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                {

                                                                                                                                                                                }
                                                                                                                                                                                else
                                                                                                                                                                                {
                                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                }
                                                                                                                                                                                if (Dung == true)
                                                                                                                                                                                {
                                                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                                                    {
                                                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                                                        {
                                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F31", "55", IDThanhVien, tableTVTF31.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF31.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                    else
                                                                                                                                                                                    {
                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F31", "55", IDThanhVien, tableTVTF31.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF31.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                            catch (Exception)
                                                                                                                                                                            { }
                                                                                                                                                                            #endregion
                                                                                                                                                                        }

                                                                                                                                                                        #region Hoa Hồng Gián tiếp F32
                                                                                                                                                                        user tableTVTF32 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF31.GioiThieu.ToString()));
                                                                                                                                                                        if (tableTVTF32 != null)
                                                                                                                                                                        {
                                                                                                                                                                            if (tableTVTF32.GioiThieu.ToString() != "0")
                                                                                                                                                                            {
                                                                                                                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                try
                                                                                                                                                                                {
                                                                                                                                                                                    if (tableTVTF32.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                    {
                                                                                                                                                                                        Dung = false;
                                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                                    }
                                                                                                                                                                                    else
                                                                                                                                                                                    {
                                                                                                                                                                                        Dung = true;
                                                                                                                                                                                        Plevel = Plevel + "," + tableTVTF32.LevelThanhVien.ToString();
                                                                                                                                                                                    }
                                                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                    {

                                                                                                                                                                                    }
                                                                                                                                                                                    else
                                                                                                                                                                                    {
                                                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                    }
                                                                                                                                                                                    if (Dung == true)
                                                                                                                                                                                    {
                                                                                                                                                                                        if (TongLevel != "8")
                                                                                                                                                                                        {
                                                                                                                                                                                            if (TongLevel != "45")
                                                                                                                                                                                            {
                                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F32", "55", IDThanhVien, tableTVTF32.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF32.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                        else
                                                                                                                                                                                        {
                                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F32", "55", IDThanhVien, tableTVTF32.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF32.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                                catch (Exception)
                                                                                                                                                                                { }
                                                                                                                                                                                #endregion
                                                                                                                                                                            }

                                                                                                                                                                            #region Hoa Hồng Gián tiếp F33
                                                                                                                                                                            user tableTVTF33 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF32.GioiThieu.ToString()));
                                                                                                                                                                            if (tableTVTF33 != null)
                                                                                                                                                                            {
                                                                                                                                                                                if (tableTVTF33.GioiThieu.ToString() != "0")
                                                                                                                                                                                {
                                                                                                                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                    try
                                                                                                                                                                                    {
                                                                                                                                                                                        if (tableTVTF33.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                        {
                                                                                                                                                                                            Dung = false;
                                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                                        }
                                                                                                                                                                                        else
                                                                                                                                                                                        {
                                                                                                                                                                                            Dung = true;
                                                                                                                                                                                            Plevel = Plevel + "," + tableTVTF33.LevelThanhVien.ToString();
                                                                                                                                                                                        }
                                                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                        {

                                                                                                                                                                                        }
                                                                                                                                                                                        else
                                                                                                                                                                                        {
                                                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                        }
                                                                                                                                                                                        if (Dung == true)
                                                                                                                                                                                        {
                                                                                                                                                                                            if (TongLevel != "8")
                                                                                                                                                                                            {
                                                                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                                                                {
                                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F33", "55", IDThanhVien, tableTVTF33.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF33.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                }
                                                                                                                                                                                            }
                                                                                                                                                                                            else
                                                                                                                                                                                            {
                                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F33", "55", IDThanhVien, tableTVTF33.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF33.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                    catch (Exception)
                                                                                                                                                                                    { }
                                                                                                                                                                                    #endregion
                                                                                                                                                                                }
                                                                                                                                                                                //000000000000000000000
                                                                                                                                                                                #region Hoa Hồng Gián tiếp F34
                                                                                                                                                                                user tableTVTF34 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF33.GioiThieu.ToString()));
                                                                                                                                                                                if (tableTVTF34 != null)
                                                                                                                                                                                {
                                                                                                                                                                                    if (tableTVTF34.GioiThieu.ToString() != "0")
                                                                                                                                                                                    {
                                                                                                                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                        try
                                                                                                                                                                                        {
                                                                                                                                                                                            if (tableTVTF34.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                            {
                                                                                                                                                                                                Dung = false;
                                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                                            }
                                                                                                                                                                                            else
                                                                                                                                                                                            {
                                                                                                                                                                                                Dung = true;
                                                                                                                                                                                                Plevel = Plevel + "," + tableTVTF34.LevelThanhVien.ToString();
                                                                                                                                                                                            }
                                                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                            {

                                                                                                                                                                                            }
                                                                                                                                                                                            else
                                                                                                                                                                                            {
                                                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                            }
                                                                                                                                                                                            if (Dung == true)
                                                                                                                                                                                            {
                                                                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                                                                {
                                                                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                                                                    {
                                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F34", "55", IDThanhVien, tableTVTF34.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF34.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                    }
                                                                                                                                                                                                }
                                                                                                                                                                                                else
                                                                                                                                                                                                {
                                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F34", "55", IDThanhVien, tableTVTF34.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF34.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                }
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                        catch (Exception)
                                                                                                                                                                                        { }
                                                                                                                                                                                        #endregion
                                                                                                                                                                                    }

                                                                                                                                                                                    #region Hoa Hồng Gián tiếp F35
                                                                                                                                                                                    user tableTVTF35 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF34.GioiThieu.ToString()));
                                                                                                                                                                                    if (tableTVTF35 != null)
                                                                                                                                                                                    {
                                                                                                                                                                                        if (tableTVTF35.GioiThieu.ToString() != "0")
                                                                                                                                                                                        {
                                                                                                                                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                            try
                                                                                                                                                                                            {
                                                                                                                                                                                                if (tableTVTF35.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                {
                                                                                                                                                                                                    Dung = false;
                                                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                                                }
                                                                                                                                                                                                else
                                                                                                                                                                                                {
                                                                                                                                                                                                    Dung = true;
                                                                                                                                                                                                    Plevel = Plevel + "," + tableTVTF35.LevelThanhVien.ToString();
                                                                                                                                                                                                }
                                                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                {

                                                                                                                                                                                                }
                                                                                                                                                                                                else
                                                                                                                                                                                                {
                                                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                }
                                                                                                                                                                                                if (Dung == true)
                                                                                                                                                                                                {
                                                                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                                                                        {
                                                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F35", "55", IDThanhVien, tableTVTF35.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF35.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                        }
                                                                                                                                                                                                    }
                                                                                                                                                                                                    else
                                                                                                                                                                                                    {
                                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F35", "55", IDThanhVien, tableTVTF35.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF35.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                    }
                                                                                                                                                                                                }
                                                                                                                                                                                            }
                                                                                                                                                                                            catch (Exception)
                                                                                                                                                                                            { }
                                                                                                                                                                                            #endregion
                                                                                                                                                                                        }
                                                                                                                                                                                        #region Hoa Hồng Gián tiếp F36
                                                                                                                                                                                        user tableTVTF36 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF35.GioiThieu.ToString()));
                                                                                                                                                                                        if (tableTVTF36 != null)
                                                                                                                                                                                        {
                                                                                                                                                                                            if (tableTVTF36.GioiThieu.ToString() != "0")
                                                                                                                                                                                            {
                                                                                                                                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                try
                                                                                                                                                                                                {
                                                                                                                                                                                                    if (tableTVTF36.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                    {
                                                                                                                                                                                                        Dung = false;
                                                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                                                    }
                                                                                                                                                                                                    else
                                                                                                                                                                                                    {
                                                                                                                                                                                                        Dung = true;
                                                                                                                                                                                                        Plevel = Plevel + "," + tableTVTF36.LevelThanhVien.ToString();
                                                                                                                                                                                                    }
                                                                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                    {

                                                                                                                                                                                                    }
                                                                                                                                                                                                    else
                                                                                                                                                                                                    {
                                                                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                    }
                                                                                                                                                                                                    if (Dung == true)
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (TongLevel != "8")
                                                                                                                                                                                                        {
                                                                                                                                                                                                            if (TongLevel != "45")
                                                                                                                                                                                                            {
                                                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F36", "55", IDThanhVien, tableTVTF36.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF36.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                            }
                                                                                                                                                                                                        }
                                                                                                                                                                                                        else
                                                                                                                                                                                                        {
                                                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F36", "55", IDThanhVien, tableTVTF36.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF36.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                        }
                                                                                                                                                                                                    }
                                                                                                                                                                                                }
                                                                                                                                                                                                catch (Exception)
                                                                                                                                                                                                { }
                                                                                                                                                                                                #endregion
                                                                                                                                                                                            }
                                                                                                                                                                                            #region Hoa Hồng Gián tiếp F37
                                                                                                                                                                                            user tableTVTF37 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF36.GioiThieu.ToString()));
                                                                                                                                                                                            if (tableTVTF37 != null)
                                                                                                                                                                                            {
                                                                                                                                                                                                if (tableTVTF37.GioiThieu.ToString() != "0")
                                                                                                                                                                                                {
                                                                                                                                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                    try
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (tableTVTF37.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                        {
                                                                                                                                                                                                            Dung = false;
                                                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                                                        }
                                                                                                                                                                                                        else
                                                                                                                                                                                                        {
                                                                                                                                                                                                            Dung = true;
                                                                                                                                                                                                            Plevel = Plevel + "," + tableTVTF37.LevelThanhVien.ToString();
                                                                                                                                                                                                        }
                                                                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                        {

                                                                                                                                                                                                        }
                                                                                                                                                                                                        else
                                                                                                                                                                                                        {
                                                                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                        }
                                                                                                                                                                                                        if (Dung == true)
                                                                                                                                                                                                        {
                                                                                                                                                                                                            if (TongLevel != "8")
                                                                                                                                                                                                            {
                                                                                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F37", "55", IDThanhVien, tableTVTF37.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF37.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                }
                                                                                                                                                                                                            }
                                                                                                                                                                                                            else
                                                                                                                                                                                                            {
                                                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F37", "55", IDThanhVien, tableTVTF37.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF37.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                            }
                                                                                                                                                                                                        }
                                                                                                                                                                                                    }
                                                                                                                                                                                                    catch (Exception)
                                                                                                                                                                                                    { }
                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                }

                                                                                                                                                                                                #region Hoa Hồng Gián tiếp F38
                                                                                                                                                                                                user tableTVTF38 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF37.GioiThieu.ToString()));
                                                                                                                                                                                                if (tableTVTF38 != null)
                                                                                                                                                                                                {
                                                                                                                                                                                                    if (tableTVTF38.GioiThieu.ToString() != "0")
                                                                                                                                                                                                    {
                                                                                                                                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                        try
                                                                                                                                                                                                        {
                                                                                                                                                                                                            if (tableTVTF38.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                            {
                                                                                                                                                                                                                Dung = false;
                                                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                                                            }
                                                                                                                                                                                                            else
                                                                                                                                                                                                            {
                                                                                                                                                                                                                Dung = true;
                                                                                                                                                                                                                Plevel = Plevel + "," + tableTVTF38.LevelThanhVien.ToString();
                                                                                                                                                                                                            }
                                                                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                            {

                                                                                                                                                                                                            }
                                                                                                                                                                                                            else
                                                                                                                                                                                                            {
                                                                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                            }
                                                                                                                                                                                                            if (Dung == true)
                                                                                                                                                                                                            {
                                                                                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F38", "55", IDThanhVien, tableTVTF38.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF38.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                }
                                                                                                                                                                                                                else
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F38", "55", IDThanhVien, tableTVTF38.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF38.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                }
                                                                                                                                                                                                            }
                                                                                                                                                                                                        }
                                                                                                                                                                                                        catch (Exception)
                                                                                                                                                                                                        { }
                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                    }
                                                                                                                                                                                                    #region Hoa Hồng Gián tiếp F39
                                                                                                                                                                                                    user tableTVTF39 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF38.GioiThieu.ToString()));
                                                                                                                                                                                                    if (tableTVTF39 != null)
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (tableTVTF39.GioiThieu.ToString() != "0")
                                                                                                                                                                                                        {
                                                                                                                                                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                            try
                                                                                                                                                                                                            {
                                                                                                                                                                                                                if (tableTVTF39.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    Dung = false;
                                                                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                                                                }
                                                                                                                                                                                                                else
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    Dung = true;
                                                                                                                                                                                                                    Plevel = Plevel + "," + tableTVTF39.LevelThanhVien.ToString();
                                                                                                                                                                                                                }
                                                                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                {

                                                                                                                                                                                                                }
                                                                                                                                                                                                                else
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                }
                                                                                                                                                                                                                if (Dung == true)
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F39", "55", IDThanhVien, tableTVTF39.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF39.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    else
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F39", "55", IDThanhVien, tableTVTF39.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF39.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                }
                                                                                                                                                                                                            }
                                                                                                                                                                                                            catch (Exception)
                                                                                                                                                                                                            { }
                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                        }
                                                                                                                                                                                                        #region Hoa Hồng Gián tiếp F40
                                                                                                                                                                                                        user tableTVTF40 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF39.GioiThieu.ToString()));
                                                                                                                                                                                                        if (tableTVTF40 != null)
                                                                                                                                                                                                        {
                                                                                                                                                                                                            if (tableTVTF40.GioiThieu.ToString() != "0")
                                                                                                                                                                                                            {
                                                                                                                                                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                                try
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    if (tableTVTF40.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        Dung = false;
                                                                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    else
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        Dung = true;
                                                                                                                                                                                                                        Plevel = Plevel + "," + tableTVTF40.LevelThanhVien.ToString();
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                    {

                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    else
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    if (Dung == true)
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        if (TongLevel != "8")
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            if (TongLevel != "45")
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F40", "55", IDThanhVien, tableTVTF40.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF40.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        else
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F40", "55", IDThanhVien, tableTVTF40.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF40.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                }
                                                                                                                                                                                                                catch (Exception)
                                                                                                                                                                                                                { }
                                                                                                                                                                                                                #endregion
                                                                                                                                                                                                            }
                                                                                                                                                                                                            #region Hoa Hồng Gián tiếp F41
                                                                                                                                                                                                            user tableTVTF41 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF40.GioiThieu.ToString()));
                                                                                                                                                                                                            if (tableTVTF41 != null)
                                                                                                                                                                                                            {
                                                                                                                                                                                                                if (tableTVTF41.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                                    try
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        if (tableTVTF41.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            Dung = false;
                                                                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        else
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            Dung = true;
                                                                                                                                                                                                                            Plevel = Plevel + "," + tableTVTF41.LevelThanhVien.ToString();
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                        {

                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        else
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        if (Dung == true)
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            if (TongLevel != "8")
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F41", "55", IDThanhVien, tableTVTF41.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF41.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            else
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F41", "55", IDThanhVien, tableTVTF41.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF41.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    catch (Exception)
                                                                                                                                                                                                                    { }
                                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                                }
                                                                                                                                                                                                                #region Hoa Hồng Gián tiếp F42
                                                                                                                                                                                                                user tableTVTF42 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF41.GioiThieu.ToString()));
                                                                                                                                                                                                                if (tableTVTF42 != null)
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    if (tableTVTF42.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                                        try
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            if (tableTVTF42.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                Dung = false;
                                                                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            else
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                Dung = true;
                                                                                                                                                                                                                                Plevel = Plevel + "," + tableTVTF42.LevelThanhVien.ToString();
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                            {

                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            else
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            if (Dung == true)
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F42", "55", IDThanhVien, tableTVTF42.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF42.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F42", "55", IDThanhVien, tableTVTF42.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF42.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        catch (Exception)
                                                                                                                                                                                                                        { }
                                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                                    }

                                                                                                                                                                                                                    #region Hoa Hồng Gián tiếp F43
                                                                                                                                                                                                                    user tableTVTF43 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF42.GioiThieu.ToString()));
                                                                                                                                                                                                                    if (tableTVTF43 != null)
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        if (tableTVTF43.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                                            try
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                if (tableTVTF43.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    Dung = false;
                                                                                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    Dung = true;
                                                                                                                                                                                                                                    Plevel = Plevel + "," + tableTVTF43.LevelThanhVien.ToString();
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                                {

                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                if (Dung == true)
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F43", "55", IDThanhVien, tableTVTF43.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF43.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F43", "55", IDThanhVien, tableTVTF43.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF43.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            catch (Exception)
                                                                                                                                                                                                                            { }
                                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        #region Hoa Hồng Gián tiếp F44
                                                                                                                                                                                                                        user tableTVTF44 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF43.GioiThieu.ToString()));
                                                                                                                                                                                                                        if (tableTVTF44 != null)
                                                                                                                                                                                                                        {
                                                                                                                                                                                                                            if (tableTVTF44.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                                                try
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    if (tableTVTF44.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        Dung = false;
                                                                                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        Dung = true;
                                                                                                                                                                                                                                        Plevel = Plevel + "," + tableTVTF44.LevelThanhVien.ToString();
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                                    {

                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    if (Dung == true)
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        if (TongLevel != "8")
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            if (TongLevel != "45")
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F44", "55", IDThanhVien, tableTVTF44.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF44.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F44", "55", IDThanhVien, tableTVTF44.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF44.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                catch (Exception)
                                                                                                                                                                                                                                { }
                                                                                                                                                                                                                                #endregion
                                                                                                                                                                                                                            }

                                                                                                                                                                                                                            #region Hoa Hồng Gián tiếp F45
                                                                                                                                                                                                                            user tableTVTF45 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF44.GioiThieu.ToString()));
                                                                                                                                                                                                                            if (tableTVTF45 != null)
                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                if (tableTVTF45.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                                                    try
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        if (tableTVTF45.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            Dung = false;
                                                                                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            Dung = true;
                                                                                                                                                                                                                                            Plevel = Plevel + "," + tableTVTF45.LevelThanhVien.ToString();
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                                        {

                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        if (Dung == true)
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            if (TongLevel != "8")
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F45", "55", IDThanhVien, tableTVTF45.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF45.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            else
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F45", "55", IDThanhVien, tableTVTF45.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF45.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    catch (Exception)
                                                                                                                                                                                                                                    { }
                                                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                #region Hoa Hồng Gián tiếp F46
                                                                                                                                                                                                                                user tableTVTF46 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF45.GioiThieu.ToString()));
                                                                                                                                                                                                                                if (tableTVTF46 != null)
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                    if (tableTVTF46.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                                                        try
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            if (tableTVTF46.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                Dung = false;
                                                                                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            else
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                Dung = true;
                                                                                                                                                                                                                                                Plevel = Plevel + "," + tableTVTF46.LevelThanhVien.ToString();
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                                            {

                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            else
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            if (Dung == true)
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F46", "55", IDThanhVien, tableTVTF46.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF46.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F46", "55", IDThanhVien, tableTVTF46.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF46.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        catch (Exception)
                                                                                                                                                                                                                                        { }
                                                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    #region Hoa Hồng Gián tiếp F47
                                                                                                                                                                                                                                    user tableTVTF47 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF46.GioiThieu.ToString()));
                                                                                                                                                                                                                                    if (tableTVTF47 != null)
                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                        if (tableTVTF47.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                                                            try
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                if (tableTVTF47.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    Dung = false;
                                                                                                                                                                                                                                                    Plevel = "45";
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    Dung = true;
                                                                                                                                                                                                                                                    Plevel = Plevel + "," + tableTVTF47.LevelThanhVien.ToString();
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                                                if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                                                {

                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                if (Dung == true)
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    if (TongLevel != "8")
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        if (TongLevel != "45")
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F47", "55", IDThanhVien, tableTVTF47.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF47.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F47", "55", IDThanhVien, tableTVTF47.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF47.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            catch (Exception)
                                                                                                                                                                                                                                            { }
                                                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        #region Hoa Hồng Gián tiếp F48
                                                                                                                                                                                                                                        user tableTVTF48 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF47.GioiThieu.ToString()));
                                                                                                                                                                                                                                        if (tableTVTF48 != null)
                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                            if (tableTVTF48.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                                                                try
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    if (tableTVTF48.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        Dung = false;
                                                                                                                                                                                                                                                        Plevel = "45";
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        Dung = true;
                                                                                                                                                                                                                                                        Plevel = Plevel + "," + tableTVTF48.LevelThanhVien.ToString();
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                    Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                                                    if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                                                    {

                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                    else
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                    if (Dung == true)
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        if (TongLevel != "8")
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            if (TongLevel != "45")
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F48", "55", IDThanhVien, tableTVTF48.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF48.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F48", "55", IDThanhVien, tableTVTF48.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF48.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                catch (Exception)
                                                                                                                                                                                                                                                { }
                                                                                                                                                                                                                                                #endregion
                                                                                                                                                                                                                                            }

                                                                                                                                                                                                                                            #region Hoa Hồng Gián tiếp F49
                                                                                                                                                                                                                                            user tableTVTF49 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF48.GioiThieu.ToString()));
                                                                                                                                                                                                                                            if (tableTVTF49 != null)
                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                if (tableTVTF49.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                                                                    try
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        if (tableTVTF49.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            Dung = false;
                                                                                                                                                                                                                                                            Plevel = "45";
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            Dung = true;
                                                                                                                                                                                                                                                            Plevel = Plevel + "," + tableTVTF49.LevelThanhVien.ToString();
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                                                        if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                                                        {

                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        else
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        if (Dung == true)
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            if (TongLevel != "8")
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                if (TongLevel != "45")
                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F49", "55", IDThanhVien, tableTVTF49.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF49.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                            else
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F49", "55", IDThanhVien, tableTVTF49.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF49.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                    catch (Exception)
                                                                                                                                                                                                                                                    { }
                                                                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                                                                }

                                                                                                                                                                                                                                                #region Hoa Hồng Gián tiếp F50
                                                                                                                                                                                                                                                user tableTVTF50 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(tableTVTF49.GioiThieu.ToString()));
                                                                                                                                                                                                                                                if (tableTVTF50 != null)
                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                    if (tableTVTF50.GioiThieu.ToString() != "0")
                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                        #region Nếu level =45% (tưng ứng 5) thì dừng ko cho cấp dưới hưởng % hoa hồng thêm theo level nữa
                                                                                                                                                                                                                                                        try
                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                            if (tableTVTF50.LevelThanhVien.ToString() == "5")
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                Dung = false;
                                                                                                                                                                                                                                                                Plevel = "45";
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                            else
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                Dung = true;
                                                                                                                                                                                                                                                                Plevel = Plevel + "," + tableTVTF50.LevelThanhVien.ToString();
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                            Plevel = Plevel.ToString().Replace("99999999999,", "");
                                                                                                                                                                                                                                                            if (Plevel.ToString() == "99999999999")
                                                                                                                                                                                                                                                            {

                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                            else
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                TongLevel = MinAndMax(Plevel.ToString());
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                            if (Dung == true)
                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                if (TongLevel != "8")
                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                    if (TongLevel != "45")
                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                        ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F50", "55", IDThanhVien, tableTVTF50.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF50.GioiThieu.ToString()), TongLevel, hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                else
                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                    ThemHoaHong_ThuongLevel(dtcart[i].ipid.ToString(), "F50", "55", IDThanhVien, tableTVTF50.GioiThieu.ToString(), Diemcoin.ToString(), TimLevelB(tableTVTF50.GioiThieu.ToString()), "0", hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                        catch (Exception)
                                                                                                                                                                                                                                                        { }
                                                                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                #endregion
                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                #endregion
                                                                                                                                                                                                                            }
                                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                                        }
                                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                                }
                                                                                                                                                                                                                #endregion
                                                                                                                                                                                                            }
                                                                                                                                                                                                            #endregion
                                                                                                                                                                                                        }
                                                                                                                                                                                                        #endregion
                                                                                                                                                                                                    }
                                                                                                                                                                                                    #endregion
                                                                                                                                                                                                }
                                                                                                                                                                                                #endregion
                                                                                                                                                                                            }
                                                                                                                                                                                            #endregion

                                                                                                                                                                                        }
                                                                                                                                                                                        #endregion
                                                                                                                                                                                    }
                                                                                                                                                                                    #endregion

                                                                                                                                                                                }
                                                                                                                                                                                #endregion
                                                                                                                                                                            }
                                                                                                                                                                            #endregion
                                                                                                                                                                        }
                                                                                                                                                                        #endregion
                                                                                                                                                                    }
                                                                                                                                                                    #endregion
                                                                                                                                                                }
                                                                                                                                                                #endregion
                                                                                                                                                            }
                                                                                                                                                            #endregion
                                                                                                                                                        }
                                                                                                                                                        #endregion
                                                                                                                                                    }
                                                                                                                                                    #endregion
                                                                                                                                                }
                                                                                                                                                #endregion
                                                                                                                                            }
                                                                                                                                            #endregion
                                                                                                                                        }
                                                                                                                                        #endregion
                                                                                                                                    }
                                                                                                                                    #endregion
                                                                                                                                }
                                                                                                                                #endregion
                                                                                                                            }
                                                                                                                            #endregion
                                                                                                                        }
                                                                                                                        #endregion
                                                                                                                    }
                                                                                                                    #endregion
                                                                                                                }
                                                                                                                #endregion
                                                                                                            }
                                                                                                            #endregion
                                                                                                        }
                                                                                                        #endregion
                                                                                                    }
                                                                                                    #endregion
                                                                                                }
                                                                                                #endregion
                                                                                            }
                                                                                            #endregion
                                                                                        }
                                                                                        #endregion
                                                                                    }
                                                                                    #endregion
                                                                                }
                                                                                #endregion
                                                                            }
                                                                            #endregion
                                                                        }
                                                                        #endregion
                                                                    }
                                                                    #endregion


                                                                }
                                                                #endregion

                                                            }
                                                            #endregion
                                                        }
                                                        #endregion
                                                    }
                                                    #endregion
                                                }
                                                #endregion
                                                #endregion
                                            }
                                        }
                                        #endregion


                                    }
                                    #endregion

                                    #region Tính hoa hồng Nhà Cung Cấp Và Chi Nhánh cho kiểu sản phẩm (TrangThaiAgLang=1)
                                    if (TrangThaiAgLang == "1")
                                    {
                                        #region Mua hàng
                                        #region Chi nhánh được hưởng 10 khi có giao dịch mua%
                                        // Đối với chi nhánh hưởng 10 mua hàng%
                                        if (!table.IDChiNhanh.Equals("0"))
                                        {
                                            try
                                            {
                                                if (ShowIDChiNhanh(table.IDChiNhanh.ToString()) != "0")
                                                {
                                                    double HoaHongChiNhanhMuaHang = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongChiNhanhMuaHang"));
                                                    double TongCoinChiNhanh = (TongCoin * HoaHongChiNhanhMuaHang) / 100;
                                                    //Ngày 10/02/2020 anh Đào sang trao đổi rào lại va chỉ cho hoa hồng rơi =0
                                                    //ThemHoaHong(dtcart[i].ipid.ToString(), "9", "Hoa Hồng (Chi Nhánh Mua Hàng) 10%", table.iuser_id.ToString(), ShowIDChiNhanh(table.IDChiNhanh.ToString()),  HoaHongChiNhanhMuaHang.ToString(), TongCoinChiNhanh.ToString());
                                                    ThemHoaHong(dtcart[i].ipid.ToString(), "9", "Hoa Hồng (Chi Nhánh Mua Hàng) ", table.iuser_id.ToString(), ShowIDChiNhanh(table.IDChiNhanh.ToString()), HoaHongChiNhanhMuaHang.ToString(), TongCoinChiNhanh.ToString(), hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                }
                                            }
                                            catch (Exception)
                                            { }
                                        }
                                        #endregion


                                        #region Hoa Hồng Leader cho người giới thiệu mua hàng
                                        if (!TimLeader(table.GioiThieu).Equals("0"))
                                        {
                                            try
                                            {
                                                double HoaHongLeaderMuaHang = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongLeaderMuaHang"));
                                                if (!MoreAll.Other.Giatri("HoaHongLeaderMuaHang").Equals("0"))
                                                {
                                                    double TongLeader = (TongCoin * HoaHongLeaderMuaHang) / 100;
                                                    ThemHoaHong(dtcart[i].ipid.ToString(), "13", "Hoa Hồng (Leader - Mua Hàng)", table.iuser_id.ToString(), TimLeader(table.GioiThieu), HoaHongLeaderMuaHang.ToString(), TongLeader.ToString(), hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                }
                                            }
                                            catch (Exception)
                                            { }
                                        }
                                        #endregion
                                        #endregion

                                        #region TH: Đối với người cấp dưới bán hàng
                                        //TH: Đối với người cấp dưới bán hàng
                                        // Đi tìm sản phẩm đăng lên bán này là ai giới thiệu và cho ng giới thiệu %
                                        // Đi tìm nhà cung A đã giới thiệu cho B
                                        string IDNhaCungCap = ShowNhaCungCap(dtcart[i].ipid.ToString());
                                        if (IDNhaCungCap != "0")
                                        {
                                            //Thành viên giới thiệu A bán hàng dc hưởng 1%
                                            #region Thành viên và leader
                                            user Cungcap = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(IDNhaCungCap));//&& p.Type == 2//&& p.ChiNhanh == 0// Type=1 là thành viên  hoặc là ng mua hàng , type=2 là nhà cung cấp
                                            if (Cungcap != null)
                                            {
                                                #region ******* đang để là trực tiếp là 10%
                                                //B là nhà cung cấp được A thành viên giới thiệu
                                                //kiểm tra b (Kiểm tra sản phẩm này có đúng là B đăng lên bán ko ?) xem có phải là nhà cung cấp ko ,nếu đúng lúc bán hàng thì cho họ %
                                                // Thưởng cho A 1%
                                                try
                                                {
                                                    double HoaHongGioiThieuTrucTiepNhaCungCap = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongGioiThieuTrucTiepNhaCungCap"));
                                                    double TongCoinCC = (TongCoin) * (HoaHongGioiThieuTrucTiepNhaCungCap) / 100;
                                                    if (Cungcap.GioiThieu.ToString() != "0")
                                                    {
                                                        ThemHoaHong(dtcart[i].ipid.ToString(), "10", "Hoa hồng (Giới Thiệu Nhà Cung Cấp) Trực tiếp", table.iuser_id.ToString(), Cungcap.GioiThieu.ToString(), HoaHongGioiThieuTrucTiepNhaCungCap.ToString(), TongCoinCC.ToString(), hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                    }
                                                    //#region Chia Thu Nhập 5F cho Nhà cung cấp
                                                    //user NCCFF1 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(Cungcap.GioiThieu.ToString()));
                                                    //if (NCCFF1 != null)
                                                    //{
                                                    //    double HHNCCFF1 = Convert.ToDouble(MoreAll.Other.Giatri("txtThuNhapNCC"));
                                                    //    double HoaHongNCCF1 = (TongCoinCC * HHNCCFF1) / 100;
                                                    //    if (NCCFF1.GioiThieu.ToString() != "0")
                                                    //    {
                                                    //        ThemHoaHong(dtcart[i].ipid.ToString(), "11", "Hoa hồng (Giới Thiệu Nhà Cung Cấp) F1 ", table.iuser_id.ToString(), NCCFF1.GioiThieu.ToString(), HHNCCFF1.ToString(), HoaHongNCCF1.ToString(), hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                    //    }
                                                    //    user NCCFF2 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(NCCFF1.GioiThieu.ToString()));
                                                    //    if (NCCFF2 != null)
                                                    //    {
                                                    //        double HHNCCFF2 = Convert.ToDouble(MoreAll.Other.Giatri("txtThuNhapNCC"));
                                                    //        double HoaHongNCCF2 = (HoaHongNCCF1 * HHNCCFF2) / 100;
                                                    //        if (NCCFF1.GioiThieu.ToString() != "0")
                                                    //        {
                                                    //            ThemHoaHong(dtcart[i].ipid.ToString(), "11", "Hoa hồng (Giới Thiệu Nhà Cung Cấp) F2 ", table.iuser_id.ToString(), NCCFF2.GioiThieu.ToString(), HHNCCFF2.ToString(), HoaHongNCCF2.ToString(), hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                    //        }
                                                    //        user NCCFF3 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(NCCFF2.GioiThieu.ToString()));
                                                    //        if (NCCFF3 != null)
                                                    //        {
                                                    //            double HHNCCFF3 = Convert.ToDouble(MoreAll.Other.Giatri("txtThuNhapNCC"));
                                                    //            double HoaHongNCCF3 = (HoaHongNCCF2 * HHNCCFF3) / 100;
                                                    //            if (NCCFF3.GioiThieu.ToString() != "0")
                                                    //            {
                                                    //                ThemHoaHong(dtcart[i].ipid.ToString(), "11", "Hoa hồng (Giới Thiệu Nhà Cung Cấp) F3 ", table.iuser_id.ToString(), NCCFF3.GioiThieu.ToString(), HHNCCFF3.ToString(), HoaHongNCCF3.ToString(), hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                    //            }
                                                    //            user NCCFF4 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(NCCFF3.GioiThieu.ToString()));
                                                    //            if (NCCFF4 != null)
                                                    //            {
                                                    //                double HHNCCFF4 = Convert.ToDouble(MoreAll.Other.Giatri("txtThuNhapNCC"));
                                                    //                double HoaHongNCCF4 = (HoaHongNCCF3 * HHNCCFF4) / 100;
                                                    //                if (NCCFF4.GioiThieu.ToString() != "0")
                                                    //                {
                                                    //                    ThemHoaHong(dtcart[i].ipid.ToString(), "11", "Hoa hồng (Giới Thiệu Nhà Cung Cấp) F4 ", table.iuser_id.ToString(), NCCFF4.GioiThieu.ToString(), HHNCCFF4.ToString(), HoaHongNCCF4.ToString(), hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                    //                }
                                                    //                user NCCFF5 = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(NCCFF4.GioiThieu.ToString()));
                                                    //                if (NCCFF5 != null)
                                                    //                {
                                                    //                    double HHNCCFF5 = Convert.ToDouble(MoreAll.Other.Giatri("txtThuNhapNCC"));
                                                    //                    double HoaHongNCCF5 = (HoaHongNCCF4 * HHNCCFF5) / 100;
                                                    //                    if (NCCFF5.GioiThieu.ToString() != "0")
                                                    //                    {
                                                    //                        ThemHoaHong(dtcart[i].ipid.ToString(), "11", "Hoa hồng (Giới Thiệu Nhà Cung Cấp) F5 ", table.iuser_id.ToString(), NCCFF5.GioiThieu.ToString(), HHNCCFF5.ToString(), HoaHongNCCF5.ToString(), hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                    //                    }
                                                    //                }
                                                    //            }
                                                    //        }
                                                    //    }
                                                    //}
                                                    //#endregion
                                                }
                                                catch (Exception)
                                                { }
                                                #endregion
                                            }
                                            #endregion


                                            #region Đối với chi nhánh
                                            // ?? có cần DK Tìm nhà cung cấp hay ko (Type == 2) ? hay chỉ cần người giới thiệu là có %
                                            // Tìm nhà chi nhánh
                                            user Cungcapchinhanh = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(IDNhaCungCap));//// && p.ChiNhanh == 1  --- Type=1 là thành viên  hoặc là ng mua hàng , type=2 là nhà cung cấp
                                            if (Cungcapchinhanh != null)
                                            {
                                                try
                                                {
                                                    if (ShowIDChiNhanh(Cungcapchinhanh.IDChiNhanh.ToString()) != "0")
                                                    {
                                                        double HoaHongChiNhanhBanHang = Convert.ToDouble(MoreAll.Other.Giatri("HoaHongChiNhanhBanHang"));
                                                        if (HoaHongChiNhanhBanHang > 0)
                                                        {
                                                            double TongCoinChinhanh = (TongCoin * HoaHongChiNhanhBanHang) / 100;
                                                            ThemHoaHong(dtcart[i].ipid.ToString(), "12", "Hoa Hồng (Chi Nhánh Bán Hàng)", table.iuser_id.ToString(), ShowIDChiNhanh(Cungcapchinhanh.IDChiNhanh.ToString()), HoaHongChiNhanhBanHang.ToString(), TongCoinChinhanh.ToString(), hdIDGiohang.Value, dtcart[i].ipid.ToString());
                                                        }
                                                    }
                                                }
                                                catch (Exception)
                                                { }
                                            }
                                            #endregion
                                        }
                                        #endregion
                                    }
                                    #endregion


                                    #endregion
                                }

                                #region Hoa hồng Lãi suất của AGLANG
                                //Lưu vào bảng Lãi suất AGLANG
                                //Lưu vào bảng Service AGLANG theo từng sản phẩm --> sau đó chạy service chạy theo từng ngày để phát sinh ra số tiền lãi và trả theo ngày
                                //Công tiền lãi suất vào ví thương mại
                                if (TrangThaiAgLang == "2")
                                {
                                    //Nếu gói có số tiền <=200 triệu thì sẽ có lãi suất là 28%/ Năm
                                    //KieuPhatSinhGiaoDich: =1 là đặt hàng , =2 là chạy Service_LaiSuatAGLANG
                                    double GoiDauTu = Convert.ToDouble(dtcart[i].Money.ToString());
                                    double SoTienDauTu = Convert.ToDouble("200000000");
                                    if (GoiDauTu <= SoTienDauTu)
                                    {
                                        double HaiTamPhanTram = Convert.ToDouble(MoreAll.Other.Giatri("HaiTamPhanTram"));
                                        double ChiaHH = (HaiTamPhanTram) / 100;

                                        double DauVao1 = Convert.ToDouble(ChiaHH);// VD: 0.28
                                        double DauVao2 = Convert.ToDouble("365");
                                        // double DauVao = Convert.ToDouble("200000000");

                                        Double HoaHong2 = (DauVao1 / DauVao2);
                                        //Response.Write(HoaHong2.ToString() + "<br>");
                                        Double HoaHong1 = (HoaHong2) * GoiDauTu;
                                        // Response.Write(HoaHong1.ToString() + "<br>");
                                        Double HoaHong3 = (HoaHong1) / 1000;

                                        #region Thêm vào Bảng ChuyenDiemThanhVien
                                        LaiSuatAGLANG bds = new LaiSuatAGLANG();
                                        bds.IDSanPham = int.Parse(dtcart[i].ipid.ToString());
                                        bds.IDThanhVienBan = int.Parse(IDNhaCungCapBanHang);
                                        bds.IDThanhVienHuongHH = int.Parse(dtcart[i].IDThanhVien.ToString());// Người được hưởng chính là nhà đầu tư
                                        //bds.IDThanhVienHuongHH = int.Parse(IDNhaCungCapBanHang);
                                        bds.LaiSuat = HoaHong3.ToString();
                                        bds.NgayNhan = DateTime.Now;
                                        bds.SoTienDauTu = dtcart[i].Money.ToString();
                                        bds.NguoiDuyet = MoreAll.MoreAll.GetCookies("UName").ToString();
                                        bds.TrangThai = 1;
                                        bds.KieuPhatSinhGiaoDich = 1;// 1, đặt hàng, 2= chạy Service
                                        bds.KieuLaiSuat = 1;// kiểu lãi suất chính là 28% theo tháng hay 32%  theo quý
                                        bds.NgayThamGia = DateTime.Now;
                                        bds.IDCart = int.Parse(hdIDGiohang.Value);
                                        bds.NoiDung = Commond.ShowPro(dtcart[i].ipid.ToString());
                                        bds.IDGioiThieuTrucTiep = Convert.ToInt64(TimF1Agland);
                                        bds.MTreeHuong = Commond.ShowMTrees(dtcart[i].IDThanhVien.ToString());
                                        db.LaiSuatAGLANGs.InsertOnSubmit(bds);
                                        db.SubmitChanges();
                                        #endregion

                                        #region Lưu vào bảng Service_LaiSuatAGLANG
                                        LaiSuatAGLANG tbn = db.LaiSuatAGLANGs.Where(s => s.TrangThai == 1).OrderByDescending(s => s.ID).FirstOrDefault();
                                        string LaiSuatID = tbn.ID.ToString();

                                        Service_LaiSuatAGLANG svbds = new Service_LaiSuatAGLANG();
                                        svbds.IDLaiSuatAGLANG = int.Parse(LaiSuatID);
                                        svbds.IDSanPham = int.Parse(dtcart[i].ipid.ToString());
                                        svbds.IDThanhVienBan = int.Parse(IDNhaCungCapBanHang);
                                        svbds.IDThanhVienHuongHH = int.Parse(dtcart[i].IDThanhVien.ToString());// Người được hưởng chính là nhà đầu tư
                                        //svbds.IDThanhVienHuongHH = int.Parse(IDNhaCungCapBanHang);
                                        svbds.LaiSuat = HoaHong3.ToString();
                                        svbds.NgayNhan = DateTime.Now;
                                        svbds.SoTienDauTu = dtcart[i].Money.ToString();
                                        svbds.KieuLaiSuat = 1;// kiểu lãi suất chính là 28% theo tháng hay 32%  theo quý
                                        svbds.SoNgayDaChay = 1;//Tổng sẽ chạy = 365 ngày, // Ngày đầu tiên khi duyệt đơn hàng sẽ được tính là 1 ngày
                                        svbds.SoNgayDaChay = 1;//Tổng sẽ chạy = 365 ngày, // Ngày đầu tiên khi duyệt đơn hàng sẽ được tính là 1 ngày
                                        svbds.NgayThamGia = DateTime.Now;
                                        svbds.IDCart = int.Parse(hdIDGiohang.Value);
                                        svbds.NoiDung = Commond.ShowPro(dtcart[i].ipid.ToString());
                                        svbds.IDGioiThieuTrucTiep = Convert.ToInt64(TimF1Agland);
                                        svbds.MTreeHuong = Commond.ShowMTrees(dtcart[i].IDThanhVien.ToString());
                                        db.Service_LaiSuatAGLANGs.InsertOnSubmit(svbds);
                                        db.SubmitChanges();

                                        // CongDiemAgLand(dtcart[i].IDThanhVien.ToString(), HoaHong3.ToString());

                                        CongDiemAgLand(dtcart[i].ipid.ToString(), "80", "Lãi suất AG LAND - Đặt Hàng", IDNhaCungCapBanHang, dtcart[i].IDThanhVien.ToString(), HoaHong3.ToString(), GoiDauTu.ToString(), TimF1Agland, hdIDGiohang.Value);

                                        //Lưu vào ví AGLAng ở bảng thành viên
                                        //Cộng điểm vào Ví thương mại

                                        // Lưu ý : khi viết Service_LaiSuatAGLANG phải Loại bỏ ngày đặt hàng này ra vì hôm đặt hàng là đã rơi hoa hồng ngày hôm đó rồi ,thì được tính là 1 ngày ở SoNgayDaChay...
                                        #endregion
                                    }
                                    else if (GoiDauTu >= SoTienDauTu)
                                    {
                                        double BaHaiPhanTram = Convert.ToDouble(MoreAll.Other.Giatri("BaHaiPhanTram"));
                                        double ChiaHH = (BaHaiPhanTram) / 100;

                                        double DauVao1 = Convert.ToDouble(ChiaHH);// VD: "0.32"
                                        double DauVao2 = Convert.ToDouble("365");
                                        // double DauVao = Convert.ToDouble("200000000");

                                        Double HoaHong2 = (DauVao1 / DauVao2);
                                        //Response.Write(HoaHong2.ToString() + "<br>");
                                        Double HoaHong1 = (HoaHong2) * GoiDauTu;
                                        // Response.Write(HoaHong1.ToString() + "<br>");
                                        Double HoaHong3 = (HoaHong1) / 1000;

                                        #region Thêm vào Bảng ChuyenDiemThanhVien
                                        LaiSuatAGLANG bds = new LaiSuatAGLANG();
                                        bds.IDSanPham = int.Parse(dtcart[i].ipid.ToString());
                                        bds.IDThanhVienBan = int.Parse(IDNhaCungCapBanHang);
                                        bds.IDThanhVienHuongHH = int.Parse(dtcart[i].IDThanhVien.ToString());// Người được hưởng chính là nhà đầu tư
                                        //bds.IDThanhVienHuongHH = int.Parse(IDNhaCungCapBanHang);
                                        bds.LaiSuat = HoaHong3.ToString();
                                        bds.NgayNhan = DateTime.Now;
                                        bds.SoTienDauTu = dtcart[i].Money.ToString();
                                        bds.NguoiDuyet = MoreAll.MoreAll.GetCookies("UName").ToString();
                                        bds.TrangThai = 1;
                                        bds.KieuPhatSinhGiaoDich = 1;// 1, đặt hàng, 2= chạy Service
                                        bds.KieuLaiSuat = 2;// kiểu lãi suất chính là 28% theo tháng hay 32%  theo quý
                                        bds.NgayThamGia = DateTime.Now;
                                        bds.IDCart = int.Parse(hdIDGiohang.Value);
                                        bds.NoiDung = Commond.ShowPro(dtcart[i].ipid.ToString());
                                        bds.IDGioiThieuTrucTiep = Convert.ToInt64(TimF1Agland);
                                        bds.MTreeHuong = Commond.ShowMTrees(dtcart[i].IDThanhVien.ToString());
                                        db.LaiSuatAGLANGs.InsertOnSubmit(bds);
                                        db.SubmitChanges();
                                        #endregion

                                        #region Lưu vào bảng Service_LaiSuatAGLANG
                                        LaiSuatAGLANG tbn = db.LaiSuatAGLANGs.Where(s => s.TrangThai == 1).OrderByDescending(s => s.ID).FirstOrDefault();
                                        string LaiSuatID = tbn.ID.ToString();

                                        Service_LaiSuatAGLANG svbds = new Service_LaiSuatAGLANG();
                                        svbds.IDLaiSuatAGLANG = int.Parse(LaiSuatID);
                                        svbds.IDSanPham = int.Parse(dtcart[i].ipid.ToString());
                                        svbds.IDThanhVienBan = int.Parse(IDNhaCungCapBanHang);
                                        svbds.IDThanhVienHuongHH = int.Parse(dtcart[i].IDThanhVien.ToString());// Người được hưởng chính là nhà đầu tư
                                        //svbds.IDThanhVienHuongHH = int.Parse(IDNhaCungCapBanHang);
                                        svbds.LaiSuat = HoaHong3.ToString();
                                        svbds.NgayNhan = DateTime.Now;
                                        svbds.SoTienDauTu = dtcart[i].Money.ToString();
                                        svbds.KieuLaiSuat = 2;// kiểu lãi suất chính là 28% theo tháng hay 32%  theo quý
                                        svbds.SoNgayDaChay = 1;//Tổng sẽ chạy = 365 ngày, // Ngày đầu tiên khi duyệt đơn hàng sẽ được tính là 1 ngày
                                        svbds.NgayThamGia = DateTime.Now;
                                        svbds.IDCart = int.Parse(hdIDGiohang.Value);
                                        svbds.NoiDung = Commond.ShowPro(dtcart[i].ipid.ToString());
                                        svbds.IDGioiThieuTrucTiep = Convert.ToInt64(TimF1Agland);
                                        svbds.MTreeHuong = Commond.ShowMTrees(dtcart[i].IDThanhVien.ToString());
                                        db.Service_LaiSuatAGLANGs.InsertOnSubmit(svbds);
                                        db.SubmitChanges();

                                        // CongDiemAgLand(dtcart[i].IDThanhVien.ToString(), HoaHong3.ToString());
                                        CongDiemAgLand(dtcart[i].ipid.ToString(), "80", "Lãi suất AG LAND - Đặt Hàng", IDNhaCungCapBanHang, dtcart[i].IDThanhVien.ToString(), HoaHong3.ToString(), GoiDauTu.ToString(), TimF1Agland, hdIDGiohang.Value);
                                        #endregion
                                    }
                                    // Cập nhật thành viên là ThanhVienAgLang=1 khi đặt sản phẩm là Ag LanD Nhé
                                    Susers.Name_Text("UPDATE [users] SET ThanhVienAgLang=1 WHERE iuser_id=" + dtcart[i].IDThanhVien.ToString() + "");
                                }
                                #endregion


                                //#region Cập nhật lại tại thời điểm thanh toán level đang là bao nhiêu và phần trăm hoa hồng là bao nhiêu vào bảng CartDetail. để còn hiển thị ở lịch sử .... đơn hàng đã xử lý
                                //string VCapdoLevelHoaHong = CapoLevelHoaHong(table.LevelThanhVien.ToString());
                                //double VHoaHongs = Convert.ToDouble(VCapdoLevelHoaHong);
                                //SCartDetail.Name_Text("UPDATE [CartDetail] SET HoaHongTheoLevel=" + VHoaHongs + " WHERE id=" + dtcart[i].ID.ToString() + "");
                                //#endregion
                                if (TrangThaiAgLang == "1")
                                {
                                    #region Vi Loi Nhuan sau khi da chia HH
                                    try
                                    {
                                        var tongdiemdachia = db.S_TongDiemDaChia(int.Parse(hdIDGiohang.Value), int.Parse(dtcart[i].ipid.ToString())).ToList();
                                        if (tongdiemdachia[0].sodiem >= 0)
                                        {
                                            Double TongDaChia = Convert.ToDouble(tongdiemdachia[0].sodiem.ToString());
                                            Double TongCongs = Diemcoin - TongDaChia;


                                            //string LogFile = ConfigurationManager.AppSettings.Get("LogFile");
                                            //if (LogFile == "true")
                                            //{
                                            //    Library.WriteErrorLogTongThanhToan("ipid:" + dtcart[i].ipid.ToString());
                                            //    Library.WriteErrorLogTongThanhToan(" Diemcoin  " + Diemcoin + "TongDaChia  " + TongDaChia + " TongCongs  " + TongCongs);
                                            //}

                                            Double MoneyMua = Convert.ToDouble(dtcart[i].Money.ToString());
                                            Double TongSoTienDaiLyMua = (MoneyMua / 1000);

                                            Double TongSoMua = Convert.ToDouble(GiaNhap(dtcart[i].ipid.ToString(), dtcart[i].Quantity.ToString()));
                                            Double TongSoTienNhaCCBan = (TongSoMua / 1000);

                                            LoiNhuanMuaBan abln = new LoiNhuanMuaBan();
                                            abln.IDThanhVienMua = int.Parse(dtcart[i].IDThanhVien.ToString());
                                            abln.IDThanhVienBan = int.Parse(dtcart[i].IDNhaCungCap.ToString());
                                            abln.IDDonHang = int.Parse(hdIDGiohang.Value);
                                            abln.IDSanPham = int.Parse(dtcart[i].ipid.ToString());
                                            abln.MoTa = Commond.ShowPro(dtcart[i].ipid.ToString());
                                            abln.NgayTao = DateTime.Now;
                                            abln.SoDiemGoc = Diemcoin.ToString();
                                            abln.SoDiemConLai = TongCongs.ToString();
                                            abln.SoDiemDaChia = TongDaChia.ToString();

                                            abln.MTreeIDThanhVienMua = Commond.ShowMTree(dtcart[i].IDThanhVien.ToString());
                                            abln.MTReIDThanhVienBan = Commond.ShowMTree(dtcart[i].IDNhaCungCap.ToString());

                                            abln.SoTienNhaCCBan = TongSoTienNhaCCBan.ToString();
                                            abln.SoTienDaiLyMua = TongSoTienDaiLyMua.ToString();
                                            abln.TienLayOViNao = 1;
                                            abln.SoLuong = int.Parse(dtcart[i].Quantity.ToString());

                                            db.LoiNhuanMuaBans.InsertOnSubmit(abln);
                                            db.SubmitChanges();
                                        }
                                        else
                                        {
                                            Double TongCongs = Diemcoin;
                                            Double MoneyMua = Convert.ToDouble(dtcart[i].Money.ToString());
                                            Double TongSoTienDaiLyMua = (MoneyMua / 1000);

                                            Double TongSoMua = Convert.ToDouble(GiaNhap(dtcart[i].ipid.ToString(), dtcart[i].Quantity.ToString()));
                                            Double TongSoTienNhaCCBan = (TongSoMua / 1000);

                                            LoiNhuanMuaBan abln = new LoiNhuanMuaBan();
                                            abln.IDThanhVienMua = int.Parse(dtcart[i].IDThanhVien.ToString());
                                            abln.IDThanhVienBan = int.Parse(dtcart[i].IDNhaCungCap.ToString());
                                            abln.IDDonHang = int.Parse(hdIDGiohang.Value);
                                            abln.IDSanPham = int.Parse(dtcart[i].ipid.ToString());
                                            abln.MoTa = Commond.ShowPro(dtcart[i].ipid.ToString());
                                            abln.NgayTao = DateTime.Now;
                                            abln.SoDiemGoc = Diemcoin.ToString();
                                            abln.SoDiemConLai = TongCongs.ToString();
                                            abln.SoDiemDaChia = "0";

                                            abln.MTreeIDThanhVienMua = Commond.ShowMTree(dtcart[i].IDThanhVien.ToString());
                                            abln.MTReIDThanhVienBan = Commond.ShowMTree(dtcart[i].IDNhaCungCap.ToString());

                                            abln.SoTienNhaCCBan = TongSoTienNhaCCBan.ToString();
                                            abln.SoTienDaiLyMua = TongSoTienDaiLyMua.ToString();
                                            abln.TienLayOViNao = 1;
                                            abln.SoLuong = int.Parse(dtcart[i].Quantity.ToString());

                                            db.LoiNhuanMuaBans.InsertOnSubmit(abln);
                                            db.SubmitChanges();
                                        }
                                    }
                                    catch (Exception)
                                    { }
                                    #endregion
                                }

                                if (TrangThaiAgLang == "2") // Đối với bất động sản thì phải chia thêm 2% hoa hồng đầu tư để cho chuyên gia.
                                {
                                    //// AGland
                                    //double DauVao = Convert.ToDouble("12000000");
                                    //double PhanTram = Convert.ToDouble("2");
                                    //double ThanhTien = (DauVao * PhanTram) / 100;
                                    //Response.Write(ThanhTien);

                                    //#region ViHoaHongChuyenGia
                                    //double TongDiemCanChia = Convert.ToDouble(dtcart[i].TongDiemDemDiChia.ToString());
                                    //double PhanTram = Convert.ToDouble(Commond.Setting("txtAGLandChuyengia"));
                                    //double ThanhTien = (TongDiemCanChia * PhanTram) / 100; // VD: 12000000*0,02=240
                                    //double TongThanhTien = (ThanhTien);/// 1000;
                                    //// ví chuyên gia AFF
                                    //ViHoaHongChuyenGia obp = new ViHoaHongChuyenGia();
                                    //obp.IDDonHang = Convert.ToInt64(hdIDGiohang.Value);
                                    //obp.IDThanhVien = Convert.ToInt64(Commond.SetThanhVienChuyenGia());
                                    //obp.IDThanhVienMua_KichHoat = Convert.ToInt64(dtcart[i].IDThanhVien.ToString());//Thành viên mua hàng
                                    //obp.TongDiem = TongThanhTien.ToString();
                                    //obp.LoaiHoaHong = 2; //1: Hoa hồng AGland
                                    //obp.NgayTao = DateTime.Now;
                                    //obp.PhanTram = int.Parse(Commond.Setting("txtAGLandChuyengia").ToString());
                                    //db.ViHoaHongChuyenGias.InsertOnSubmit(obp);
                                    //db.SubmitChanges();
                                    // #endregion


                                    #region Vi Loi Nhuan sau khi da chia HH
                                    try
                                    {
                                        var tongdiemdachia = db.S_TongDiemDaChia(int.Parse(hdIDGiohang.Value), int.Parse(dtcart[i].ipid.ToString())).ToList();
                                        if (tongdiemdachia[0].sodiem >= 0)
                                        {
                                            // double Diemcoin = Convert.ToDouble(dtcart[i].Diemcoin.ToString());
                                            Double TongDaChia = Convert.ToDouble(tongdiemdachia[0].sodiem.ToString());
                                            Double TongDaChiaConlai = TongDaChia;//+ TongThanhTien;
                                            Double TongCongs = Diemcoin - TongDaChiaConlai;

                                            //string LogFile = ConfigurationManager.AppSettings.Get("LogFile");
                                            //if (LogFile == "true")
                                            //{
                                            //    Library.WriteErrorLogTongThanhToan("ipid:" + dtcart[i].ipid.ToString());
                                            //    Library.WriteErrorLogTongThanhToan(" Diemcoin  " + Diemcoin + "TongDaChia  " + TongDaChia + " TongCongs  " + TongCongs);
                                            //}

                                            Double MoneyMua = Convert.ToDouble(dtcart[i].Money.ToString());
                                            Double TongSoTienDaiLyMua = (MoneyMua / 1000);

                                            Double TongSoMua = Convert.ToDouble(GiaNhap(dtcart[i].ipid.ToString(), dtcart[i].Quantity.ToString()));
                                            Double TongSoTienNhaCCBan = (TongSoMua / 1000);

                                            LoiNhuanMuaBan abln = new LoiNhuanMuaBan();
                                            abln.IDThanhVienMua = int.Parse(dtcart[i].IDThanhVien.ToString());
                                            abln.IDThanhVienBan = int.Parse(dtcart[i].IDNhaCungCap.ToString());
                                            abln.IDDonHang = int.Parse(hdIDGiohang.Value);
                                            abln.IDSanPham = int.Parse(dtcart[i].ipid.ToString());
                                            abln.MoTa = Commond.ShowPro(dtcart[i].ipid.ToString());
                                            abln.NgayTao = DateTime.Now;
                                            abln.SoDiemGoc = Diemcoin.ToString();
                                            abln.SoDiemConLai = TongCongs.ToString();
                                            abln.SoDiemDaChia = TongDaChiaConlai.ToString();

                                            abln.MTreeIDThanhVienMua = Commond.ShowMTree(dtcart[i].IDThanhVien.ToString());
                                            abln.MTReIDThanhVienBan = Commond.ShowMTree(dtcart[i].IDNhaCungCap.ToString());

                                            abln.SoTienNhaCCBan = TongSoTienNhaCCBan.ToString();
                                            abln.SoTienDaiLyMua = TongSoTienDaiLyMua.ToString();
                                            abln.TienLayOViNao = 1;
                                            abln.SoLuong = int.Parse(dtcart[i].Quantity.ToString());

                                            db.LoiNhuanMuaBans.InsertOnSubmit(abln);
                                            db.SubmitChanges();
                                        }
                                        //else
                                        //{
                                        //    Double TongDaChiaConlai = TongThanhTien;
                                        //    Double TongCongs = Diemcoin - TongDaChiaConlai;
                                        //    Double MoneyMua = Convert.ToDouble(dtcart[i].Money.ToString());
                                        //    Double TongSoTienDaiLyMua = (MoneyMua / 1000);

                                        //    Double TongSoMua = Convert.ToDouble(GiaNhap(dtcart[i].ipid.ToString(), dtcart[i].Quantity.ToString()));
                                        //    Double TongSoTienNhaCCBan = (TongSoMua / 1000);

                                        //    LoiNhuanMuaBan abln = new LoiNhuanMuaBan();
                                        //    abln.IDThanhVienMua = int.Parse(dtcart[i].IDThanhVien.ToString());
                                        //    abln.IDThanhVienBan = int.Parse(dtcart[i].IDNhaCungCap.ToString());
                                        //    abln.IDDonHang = int.Parse(hdIDGiohang.Value);
                                        //    abln.IDSanPham = int.Parse(dtcart[i].ipid.ToString());
                                        //    abln.MoTa = Commond.ShowPro(dtcart[i].ipid.ToString());
                                        //    abln.NgayTao = DateTime.Now;
                                        //    abln.SoDiemGoc = Diemcoin.ToString();
                                        //    abln.SoDiemConLai = TongCongs.ToString();
                                        //    abln.SoDiemDaChia = TongDaChiaConlai.ToString();

                                        //    abln.MTreeIDThanhVienMua = Commond.ShowMTree(dtcart[i].IDThanhVien.ToString());
                                        //    abln.MTReIDThanhVienBan = Commond.ShowMTree(dtcart[i].IDNhaCungCap.ToString());

                                        //    abln.SoTienNhaCCBan = TongSoTienNhaCCBan.ToString();
                                        //    abln.SoTienDaiLyMua = TongSoTienDaiLyMua.ToString();
                                        //    abln.TienLayOViNao = 1;
                                        //    abln.SoLuong = int.Parse(dtcart[i].Quantity.ToString());

                                        //    db.LoiNhuanMuaBans.InsertOnSubmit(abln);
                                        //    db.SubmitChanges();
                                        //}
                                    }
                                    catch (Exception)
                                    { }
                                    #endregion
                                }
                            }
                        }
                        #endregion

                        #region Nâng cấp level thành viên
                        NangLevel.UpDate_NangLevel(dt1.IDThanhVien.ToString());
                        #endregion

                        //Duyệt đơn hàng
                        SCartDetail.Name_Text("update CartDetail set TrangThaiNhaCungCap=1,TrangThaiNguoiMuaHang=1,TrangThaiKhieuKien=0  where ID_Cart=" + hdid.Value + "");

                        // kích hoạt thành viên khi có đơn hàng mua sp chiến lược đủ số tiền 998 điểm
                        //if (ThanhVienKichHoat == 0)
                        //{
                        //    Double Cauhinhtien = Convert.ToDouble(Commond.Setting("txtTienkichhoatdaily"));
                        //    if (TongTienDonHangKichHoatThanhVien >= Cauhinhtien)
                        //    {
                        //     //   Susers.Name_Text("update users set DuyetTienDanap=1 where iuser_id=" + dt1.IDThanhVien.ToString() + "");
                        //        #region Cập nhật ngày kích hoạt 1 năm để kiểm soát
                        //        Commond.SetLichSuKichHoat(dt1.IDThanhVien.ToString(), "Qua hình thức mua hàng");
                        //        #endregion
                        //    }
                        //}
                        // ThanhVienKichHoat

                        SCarts.UpdateStatus(hdid.Value, "1");
                        btduyet.Visible = false;
                    }
                }
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
        void CongDiemAgLand(string IDProducts, string IDType, string Type, string IDThanhVien, string IDUserNguoiDuocHuong, string TongTien, string GoiDauTu, string TimF1Agland, string IDDonHang)
        {
            List<Entity.users> iitem = Susers.Name_Text("select * from users where iuser_id=" + IDUserNguoiDuocHuong + "");
            if (iitem != null)
            {
                double VGoiDauTu = Convert.ToDouble(GoiDauTu);
                double VTongTienCoinDuocCap = Convert.ToDouble(iitem[0].TongTienCoinDuocCap);
                double VVViUuTien = Convert.ToDouble(iitem[0].ViUuTien);
                double Tienmotcophan = Convert.ToDouble(Commond.Setting("tienmotcophan"));

                double VTienDangSoHuuBatDongSan = Convert.ToDouble(iitem[0].TienDangSoHuuBatDongSan);

                double CoPhan = 0;
                CoPhan = ((VGoiDauTu) / (Tienmotcophan));

                double TongCoPhan = 0;
                TongCoPhan = ((CoPhan) + (VTienDangSoHuuBatDongSan));


                double TongSo = Convert.ToDouble(iitem[0].ViAgLang);
                double TongTienNapVao = Convert.ToDouble(TongTien);
                double Conglai = 0;
                Conglai = ((TongSo) + (TongTienNapVao));

                double TConglai = 0;
                TConglai = ((VTongTienCoinDuocCap) + (TongTienNapVao));

                double VVConglai = 0;
                VVConglai = ((VVViUuTien) + (TongTienNapVao));

                //Uutien=1 sẽ roi Toan bộ hh vào ví uwutien ko roi vào ví Thương mai
                if (iitem[0].Uutien.ToString() == "1")
                {
                    Susers.Name_Text("update users set ViAgLang=" + Conglai.ToString() + ",ViUuTien=" + VVConglai + ",TienDangSoHuuBatDongSan=" + TongCoPhan + "  where iuser_id=" + iitem[0].iuser_id.ToString() + "");
                }
                else
                {
                    Susers.Name_Text("update users set ViAgLang=" + Conglai.ToString() + ",TienDangSoHuuBatDongSan=" + TongCoPhan + "  where iuser_id=" + iitem[0].iuser_id.ToString() + "");
                }

                //F1 sẽ hưởng lãi 10% của lãi Agland
                if (TimF1Agland != "0")
                {
                    List<Entity.users> igt = Susers.Name_Text("select * from users where iuser_id=" + TimF1Agland + "");
                    if (igt != null)
                    {
                        double TongDiemATL = Convert.ToDouble(igt[0].ViFMotAnTheoAgland.ToString());
                        double HHF1AnTheoAgland = Convert.ToDouble(Commond.Setting("txtF1AnTheoAgland"));
                        double TongTienNapVaos = Convert.ToDouble(TongTien);
                        double ChiaHH = (TongTienNapVaos * HHF1AnTheoAgland) / 100;
                        double CVConglai = 0;
                        CVConglai = ((TongDiemATL) + (ChiaHH));
                        Susers.Name_Text("update users set ViFMotAnTheoAgland=" + CVConglai.ToString() + "  where iuser_id=" + TimF1Agland.ToString() + "");

                        LaiSuatTheoAgLand obj = new LaiSuatTheoAgLand();
                        obj.IDThanhVienMua = Convert.ToInt64(IDUserNguoiDuocHuong);
                        obj.IDHuongF1 = Convert.ToInt64(TimF1Agland);
                        obj.HoaHong = ChiaHH.ToString();
                        obj.PhanTramLai = int.Parse(HHF1AnTheoAgland.ToString());
                        obj.NgayTao = DateTime.Now;
                        obj.IDDonHang = Convert.ToInt64(IDDonHang);
                        db.LaiSuatTheoAgLands.InsertOnSubmit(obj);
                        db.SubmitChanges();
                    }
                }
            }

            #region LichSuGiaoDich
            LichSuGiaoDich obl = new LichSuGiaoDich();
            obl.IDProducts = int.Parse(IDProducts);
            obl.IDType = int.Parse(IDType);
            obl.Type = Type;
            obl.IDThanhVien = int.Parse(IDThanhVien);
            obl.IDUserNguoiDuocHuong = int.Parse(IDUserNguoiDuocHuong);
            obl.PhamTramHoaHong = "0";
            obl.SoCoin = TongTien.ToString();
            obl.NgayTao = DateTime.Now;
            db.LichSuGiaoDiches.InsertOnSubmit(obl);
            db.SubmitChanges();
            #endregion
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
        void ThemHoaHong_ThuongLevel(string IDProducts, string ThuTu, string IDType, string IDThanhVien, string IDUserNguoiDuocHuong, string SoCoin, string LevelThanhVienA, string LevelThanhVienB, string IDCart, string NoiDung)
        {
            // string LogFile = ConfigurationManager.AppSettings.Get("LogFile");
            //if (LogFile == "true")
            //{
            //    Library.WriteErrorLog("Người Duyệt đơn hàng : " + MoreAll.MoreAll.GetCookies("UName").ToString() + " - Mã Đơn hàng: " + hdIDGiohang.Value + " DuyetNhanh - Sản phẩm: " + IDProducts + " - " + ThuTu + " - IDThanhVien: " + IDThanhVien + " - IDUserNguoiDuocHuong: " + IDUserNguoiDuocHuong + " - LevelThanhVienA: " + LevelThanhVienA + " - LevelThanhVienB: " + LevelThanhVienB);
            //}
            if (TinhDiemthuongGiantiep(LevelThanhVienA.ToString(), LevelThanhVienB.ToString()) != "0")
            {
                #region Hoa hồng gián tiếp khi giới thiệu được hưởng sau khi lấy level A - level B
                double SoPhanTram = Convert.ToDouble(TinhDiemthuongGiantiep(LevelThanhVienA.ToString(), LevelThanhVienB.ToString()));
                double TongTien = Convert.ToDouble(SoCoin);
                double ThuongLevel = (TongTien * SoPhanTram) / 100;
                ThemHoaHong(IDProducts, IDType, "Hoa hồng (Cấp) quản lý " + ThuTu, IDThanhVien, IDUserNguoiDuocHuong, SoPhanTram.ToString(), ThuongLevel.ToString(), IDCart, NoiDung);
                #endregion
            }
        }


        void ThemHoaHongNCC(string IDProducts, string IDType, string Type, string IDThanhVien, string IDUserNguoiDuocHuong, string PhamTramHoaHong, string SoCoin, string IDCart, string NoiDung)
        {
            #region Kiểm tra ngày hết hạn Update lại trạng thái kích hoạt nạp 480K
            Commond.CheckNgayHetHan(IDUserNguoiDuocHuong.ToString());
            #endregion

            List<Entity.users> F1 = Susers.Name_Text("select * from users  where iuser_id=" + IDUserNguoiDuocHuong.ToString() + " ");
            if (F1.Count() > 0 || (IDUserNguoiDuocHuong == "0") || IDThanhVien == IDUserNguoiDuocHuong)
            {
                #region HoaHongThanhVien
                HoaHongThanhVien obj = new HoaHongThanhVien();
                obj.IDProducts = int.Parse(IDProducts);
                obj.IDType = int.Parse(IDType);
                obj.Type = Type;
                obj.IDThanhVien = int.Parse(IDThanhVien);
                obj.IDUserNguoiDuocHuong = int.Parse(IDUserNguoiDuocHuong);
                obj.PhamTramHoaHong = PhamTramHoaHong;
                obj.SoCoin = SoCoin.ToString();
                obj.NgayTao = DateTime.Now;
                obj.TrangThai = 1;
                obj.NoiDung = Commond.ShowPro(NoiDung);
                obj.IDCart = Convert.ToInt64(IDCart);
                db.HoaHongThanhViens.InsertOnSubmit(obj);
                db.SubmitChanges();
                #endregion

                //string LogFile = ConfigurationManager.AppSettings.Get("LogFilehh");
                //if (LogFile == "true")
                //{
                //    Library.WriteErrorLogHoaHong("Người Duyệt đơn hàng : " + MoreAll.MoreAll.GetCookies("UName").ToString() + " - IDProducts: " + IDProducts + " IDType" + IDType + " " + IDThanhVien + " - IDUserNguoiDuocHuong: " + IDUserNguoiDuocHuong + " - PhamTramHoaHong: " + PhamTramHoaHong + " - SoCoin: " + SoCoin);
                //}
                #region LichSuGiaoDich
                LichSuGiaoDich obl = new LichSuGiaoDich();
                obl.IDProducts = int.Parse(IDProducts);
                obl.IDType = int.Parse(IDType);
                obl.Type = Type;
                obl.IDThanhVien = int.Parse(IDThanhVien);
                obl.IDUserNguoiDuocHuong = int.Parse(IDUserNguoiDuocHuong);
                obl.PhamTramHoaHong = PhamTramHoaHong;
                obl.SoCoin = SoCoin.ToString();
                obl.NgayTao = DateTime.Now;
                obl.NoiDung = Commond.ShowPro(NoiDung);
                obl.IDCart = Convert.ToInt64(IDCart);
                db.LichSuGiaoDiches.InsertOnSubmit(obl);
                db.SubmitChanges();
                #endregion

                CongTien(IDType, IDUserNguoiDuocHuong, SoCoin);
               
            }

        }
        void ThemHoaHong(string IDProducts, string IDType, string Type, string IDThanhVien, string IDUserNguoiDuocHuong, string PhamTramHoaHong, string SoCoin, string IDCart, string NoiDung)
        {
            #region Kiểm tra ngày hết hạn Update lại trạng thái kích hoạt nạp 480K
            Commond.CheckNgayHetHan(IDUserNguoiDuocHuong.ToString());
            #endregion

            List<Entity.users> F1 = Susers.Name_Text("select * from users  where iuser_id=" + IDUserNguoiDuocHuong.ToString() + " and DuyetTienDanap=1 ");
            if (F1.Count() > 0 || (IDUserNguoiDuocHuong == "0") || IDThanhVien == IDUserNguoiDuocHuong)
            {
                #region HoaHongThanhVien
                HoaHongThanhVien obj = new HoaHongThanhVien();
                obj.IDProducts = int.Parse(IDProducts);
                obj.IDType = int.Parse(IDType);
                obj.Type = Type;
                obj.IDThanhVien = int.Parse(IDThanhVien);
                obj.IDUserNguoiDuocHuong = int.Parse(IDUserNguoiDuocHuong);
                obj.PhamTramHoaHong = PhamTramHoaHong;
                obj.SoCoin = SoCoin.ToString();
                obj.NgayTao = DateTime.Now;
                obj.TrangThai = 1;
                obj.NoiDung = Commond.ShowPro(NoiDung);
                obj.IDCart = Convert.ToInt64(IDCart);
                db.HoaHongThanhViens.InsertOnSubmit(obj);
                db.SubmitChanges();
                #endregion

                //string LogFile = ConfigurationManager.AppSettings.Get("LogFilehh");
                //if (LogFile == "true")
                //{
                //    Library.WriteErrorLogHoaHong("Người Duyệt đơn hàng : " + MoreAll.MoreAll.GetCookies("UName").ToString() + " - IDProducts: " + IDProducts + " IDType" + IDType + " " + IDThanhVien + " - IDUserNguoiDuocHuong: " + IDUserNguoiDuocHuong + " - PhamTramHoaHong: " + PhamTramHoaHong + " - SoCoin: " + SoCoin);
                //}
                #region LichSuGiaoDich
                LichSuGiaoDich obl = new LichSuGiaoDich();
                obl.IDProducts = int.Parse(IDProducts);
                obl.IDType = int.Parse(IDType);
                obl.Type = Type;
                obl.IDThanhVien = int.Parse(IDThanhVien);
                obl.IDUserNguoiDuocHuong = int.Parse(IDUserNguoiDuocHuong);
                obl.PhamTramHoaHong = PhamTramHoaHong;
                obl.SoCoin = SoCoin.ToString();
                obl.NgayTao = DateTime.Now;
                obl.NoiDung = Commond.ShowPro(NoiDung);
                obl.IDCart = Convert.ToInt64(IDCart);
                db.LichSuGiaoDiches.InsertOnSubmit(obl);
                db.SubmitChanges();
                #endregion

                CongTien(IDType, IDUserNguoiDuocHuong, SoCoin);
                //CongTien_ViTienHHGioiThieu(IDProducts, IDThanhVien, IDUserNguoiDuocHuong, PhamTramHoaHong, SoCoin);
            }

        }
        void CongTien_ViTienHHGioiThieu(string IDProducts, string IDThanhVien, string IDUserNguoiDuocHuong, string PhamTramHoaHong, string SoCoin, string IDCart, string NoiDung)
        {
            #region Cộng điểm  theo hoa hồng coin vào bảng thành viên để tích điểm trong các trường hợp mua hàng cần
            List<Entity.users> iitem = Susers.Name_Text("select * from users where iuser_id=" + IDUserNguoiDuocHuong.ToString() + "");
            if (iitem.Count() > 0)
            {
                double TongSoCoinDaCo = Convert.ToDouble(iitem[0].TongTienCoinDuocCap);
                double TongTienNapVao = Convert.ToDouble(SoCoin);
                double Conglai = 0;
                Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));
                Susers.Name_Text("update users set TongTienCoinDuocCap=" + Conglai.ToString() + "  where iuser_id=" + iitem[0].iuser_id.ToString() + "");

                // sẽ xóa đoạn code này khi ViTienHHGioiThieu=0 đồng nhé
                #region Làm thêm phần lấy điểm từ ví ViTienHHGioiThieu trừ đi và cộng thêm vào TongTienCoinDuocCap
                //06/01/2020
                //Làm thêm phần lấy điểm từ ví ViTienHHGioiThieu trừ đi và cộng thêm vào TongTienCoinDuocCap
                List<Entity.users> truvi = Susers.Name_Text("select * from users where iuser_id=" + IDUserNguoiDuocHuong.ToString() + "  and HoTro=1");
                if (truvi.Count() > 0)
                {
                    double ViTienHHGioiThieu = Convert.ToDouble(truvi[0].ViTienHHGioiThieu);
                    if (ViTienHHGioiThieu >= TongTienNapVao)
                    {
                        double ConglaiBiTru = ((ViTienHHGioiThieu) - (TongTienNapVao));

                        double TongSo = Convert.ToDouble(truvi[0].TongTienCoinDuocCap);
                        double VConglai = ((TongSo) + (TongTienNapVao));

                        Susers.Name_Text("update users set ViTienHHGioiThieu=" + ConglaiBiTru.ToString() + "  where iuser_id=" + truvi[0].iuser_id.ToString() + "");
                        Susers.Name_Text("update users set TongTienCoinDuocCap=" + VConglai.ToString() + "  where iuser_id=" + truvi[0].iuser_id.ToString() + "");

                        // Hoa hồng này là lấy từ ví ViTienHHGioiThieu cộng sang ví TongTienCoinDuocCap khi có phát sinh tiền hoa hồng
                        // lưu ý: IDThanhVien ở đây chỉ mang tính chất minh họa khi có phát sinh, chứ ko phải được hoa hồng từ người này nhé.
                        ThemHoaHongThem_ViTienHHGioiThieu(IDProducts, "31", "Hoa Hồng (Hỗ Trợ)", IDThanhVien, IDUserNguoiDuocHuong, PhamTramHoaHong, TongTienNapVao.ToString(), IDCart, NoiDung);
                        //Mục 32 này làm để lưu lịch sử để sau này nhỡ có lỗi còn lục lại được là đã bị trừ ntn
                        ThemHoaHongThem_ViTienHHGioiThieu(IDProducts, "32", "Hoa Hồng hỗ trợ (Bị trừ từ ví hoa hồng Hỗ Trợ)", IDThanhVien, IDUserNguoiDuocHuong, PhamTramHoaHong, TongTienNapVao.ToString(), IDCart, NoiDung);
                    }
                }
                #endregion
            }
            #endregion
        }
        // Sẽ xóa đoạn code này khi ViTienHHGioiThieu=0 đồng nhé ThemHoaHongThem_ViTienHHGioiThieu
        void ThemHoaHongThem_ViTienHHGioiThieu(string IDProducts, string IDType, string Type, string IDThanhVien, string IDUserNguoiDuocHuong, string PhamTramHoaHong, string SoCoin, string IDCart, string NoiDung)
        {
            #region Kiểm tra ngày hết hạn Update lại trạng thái kích hoạt nạp 480K
            Commond.CheckNgayHetHan(IDUserNguoiDuocHuong.ToString());
            #endregion

            List<Entity.users> F1 = Susers.Name_Text("select * from users  where iuser_id=" + IDUserNguoiDuocHuong.ToString() + " and DuyetTienDanap=1 ");
            if (F1.Count() > 0)
            {
                #region HoaHongThanhVien
                HoaHongThanhVien obj = new HoaHongThanhVien();
                obj.IDProducts = int.Parse(IDProducts);
                obj.IDType = int.Parse(IDType);
                obj.Type = Type;
                obj.IDThanhVien = int.Parse(IDThanhVien);
                obj.IDUserNguoiDuocHuong = int.Parse(IDUserNguoiDuocHuong);
                obj.PhamTramHoaHong = PhamTramHoaHong;
                obj.SoCoin = SoCoin.ToString();
                obj.NgayTao = DateTime.Now;
                obj.TrangThai = 1;
                obj.NoiDung = Commond.ShowPro(NoiDung);
                obj.IDCart = Convert.ToInt64(IDCart);
                db.HoaHongThanhViens.InsertOnSubmit(obj);
                db.SubmitChanges();
                #endregion

                #region LichSuGiaoDich
                LichSuGiaoDich obl = new LichSuGiaoDich();
                obl.IDProducts = int.Parse(IDProducts);
                obl.IDType = int.Parse(IDType);
                obl.Type = Type;
                obl.IDThanhVien = int.Parse(IDThanhVien);
                obl.IDUserNguoiDuocHuong = int.Parse(IDUserNguoiDuocHuong);
                obl.PhamTramHoaHong = PhamTramHoaHong;
                obl.SoCoin = SoCoin.ToString();
                obl.NgayTao = DateTime.Now;
                obl.NoiDung = Commond.ShowPro(NoiDung);
                obl.IDCart = Convert.ToInt64(IDCart);
                db.LichSuGiaoDiches.InsertOnSubmit(obl);
                db.SubmitChanges();
                #endregion
            }
        }

        // sau khi xóa code ViTienHHGioiThieu thì dùng lại code này nhé
        void CongTien(string Type, string IDUserNguoiDuocHuong, string SoCoin)
        {
            #region Cộng điểm  theo hoa hồng coin vào bảng thành viên để tích điểm trong các trường hợp mua hàng cần
            List<Entity.users> iitem = Susers.Name_Text("select * from users where iuser_id=" + IDUserNguoiDuocHuong.ToString() + "");
            if (iitem != null)
            {
                if (Type == "30" || Type == "401")// 30 la thanh toan truc tiếp thì cho vào ví thương mại luôn. ngoài
                {
                    double TongSoCoinDaCo = Convert.ToDouble(iitem[0].TongTienCoinDuocCap);
                    double TongTienNapVao = Convert.ToDouble(SoCoin);
                    double Conglai = 0;
                    Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));
                    Susers.Name_Text("update users set TongTienCoinDuocCap=" + Conglai.ToString() + "  where iuser_id=" + iitem[0].iuser_id.ToString() + "");
                    // Library.WriteErrorLogChiNhanh("Type == 30 -- Tổng ví:" + TongSoCoinDaCo + " TongTienCoinDuocCap:" + Conglai + " iuser_id=" + iitem[0].iuser_id.ToString());

                }
                else if (Type == "300")// 300 là thành viên Free tặng điểm là về ví mua hàng
                {
                    double TongSoCoinDaCo = Convert.ToDouble(iitem[0].TongTienCoinDuocCap);
                    double TongTienNapVao = Convert.ToDouble(SoCoin);
                    double Conglai = 0;
                    Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));
                    Susers.Name_Text("update users set TongTienCoinDuocCap=" + Conglai.ToString() + "  where iuser_id=" + iitem[0].iuser_id.ToString() + "");

                    // Library.WriteErrorLogChiNhanh("Type == 300 -- Tổng ví:" + TongSoCoinDaCo + " TongTienCoinDuocCap:" + Conglai + " iuser_id=" + iitem[0].iuser_id.ToString());
                }
                else if (Type != "301")// 301 đổ về ví công ty AG, chỉ đổ về lịch sử giao dịch rồi show tổng lên thôi, chứ ko đổ về ví nào cả
                {
                    double TongSoCoinDaCo = Convert.ToDouble(iitem[0].ViHoaHongMuaBan);
                    double TongTienNapVao = Convert.ToDouble(SoCoin);
                    double Conglai = 0;
                    Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));

                    Susers.Name_Text("update users set ViHoaHongMuaBan=" + Conglai.ToString() + "  where iuser_id=" + iitem[0].iuser_id.ToString() + "");
                    // Library.WriteErrorLogChiNhanh("Type != 301 -- Tổng ví:" + TongSoCoinDaCo + " TongTienCoinDuocCap:" + Conglai + " iuser_id=" + iitem[0].iuser_id.ToString());

                }
            }
            #endregion
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

        protected string HoaHongTheoLevel_TheoThoiDiemMuahang_News(string CapoLevelHoaHongs, string Tongd)
        {
            double TongCoin = Convert.ToDouble(Tongd);
            double HoaHongs = Convert.ToDouble("30");//CapoLevelHoaHongs
            double Tong = (TongCoin * HoaHongs) / 100;
            return Tong.ToString();
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
        protected bool ShowSuKienMua(string id)
        {
            List<Entity.CartDetail> list = SCartDetail.Name_Text("select * from CartDetail where TrangThaiNhaCungCap!=3 and ID_Cart=" + id + "");
            if (list.Count > 0)
            {
                return false;
            }
            List<Entity.CartDetail> list1 = SCartDetail.Name_Text("select * from CartDetail where TrangThaiNguoiMuaHang!=3 and ID_Cart=" + id + "");
            if (list1.Count > 0)
            {
                return false;
            }
            List<Entity.CartDetail> list2 = SCartDetail.Name_Text("select * from CartDetail where TrangThaiKhieuKien!=0 and ID_Cart=" + id + "");
            if (list2.Count > 0)
            {
                return false;
            }
            return true;
        }

        #region Tìm ra người giới thiệu gần nhất để cho Level phần Ag Land
        protected string ShowF2(string IDF1, string IDF2)
        {
            List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + " and ThanhVienAgLang=1");
            if (dtf1.Count > 0)
            {
                return dtf1[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  and ThanhVienAgLang=1");
            if (dtf2.Count > 0)
            {
                return dtf2[0].LevelThanhVien.ToString();
            }
            return "0";
        }
        protected string ShowF3(string IDF1, string IDF2, string IDF3)
        {
            List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  and ThanhVienAgLang=1");
            if (dtf1.Count > 0)
            {
                return dtf1[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  and ThanhVienAgLang=1");
            if (dtf2.Count > 0)
            {
                return dtf2[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  and ThanhVienAgLang=1");
            if (dtf3.Count > 0)
            {
                return dtf3[0].LevelThanhVien.ToString();
            }
            return "0";
        }
        protected string ShowF4(string IDF1, string IDF2, string IDF3, string IDF4)
        {
            List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  and ThanhVienAgLang=1");
            if (dtf1.Count > 0)
            {
                return dtf1[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  and ThanhVienAgLang=1");
            if (dtf2.Count > 0)
            {
                return dtf2[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  and ThanhVienAgLang=1");
            if (dtf3.Count > 0)
            {
                return dtf3[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf4 = Susers.Name_Text("select * from users  where iuser_id=" + IDF4 + "  and ThanhVienAgLang=1");
            if (dtf4.Count > 0)
            {
                return dtf4[0].LevelThanhVien.ToString();
            }
            return "0";
        }
        protected string ShowF5(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5)
        {
            List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  and ThanhVienAgLang=1");
            if (dtf1.Count > 0)
            {
                return dtf1[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  and ThanhVienAgLang=1");
            if (dtf2.Count > 0)
            {
                return dtf2[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  and ThanhVienAgLang=1");
            if (dtf3.Count > 0)
            {
                return dtf3[0].LevelThanhVien.ToString();
            }
            List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  and ThanhVienAgLang=1");
            if (dtf5.Count > 0)
            {
                return dtf5[0].LevelThanhVien.ToString();
            }
            return "0";
        }
        #endregion

        protected void rpcartdetail_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string str = e.CommandName.Trim();
            string ID = e.CommandArgument.ToString();
            string str4 = str;
            if (str4 != null)
            {
                if (str4 == "Delete")
                {
                    SCartDetail.Name_Text("delete from CartDetail   where ID=" + ID.ToString() + " and TrangThaiNhaCungCap=3");
                }
            }
            Response.Redirect(Request.RawUrl.ToString());
        }
        protected bool EnableLock_DuyetHang(string Enable)
        {
            // 1: Đã duyệt
            // 2: Hủy đơn hàng
            // 3: Chờ xử lý
            // 4: Bị người mua trả lại hàng
            // 5: Hoàn tiền
            // 6: Khiếu kiện gửi admin
            if (Enable.ToString() == "8" || Enable.ToString() == "7" || Enable.ToString() == "1" || Enable.ToString() == "6" || Enable.ToString() == "5" || Enable.ToString() == "2" || Enable.ToString() == "4")
            {
                return false;
            }
            return true;
        }
    }
}

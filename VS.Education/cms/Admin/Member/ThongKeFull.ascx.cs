using MoreAll;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Admin.Member
{
    public partial class ThongKeFull : System.Web.UI.UserControl
    {
        private string language = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowBaoCao();
        }

        public void ShowBaoCao()
        {
            //var tongsoddon = SCarts.Name_Text("SELECT  COUNT(*) as COUNT from Carts");

            //THỐNG KÊ BÁN HÀNG
            var query = db.LCarts.Count();
            lttongsodon.Text = query.ToString();

            var query1 = db.CartDetails.Where(x => x.TrangThaiKhieuKien == 0 && x.TrangThaiNguoiMuaHang == 1 && x.TrangThaiNhaCungCap == 1).ToList().Count();
            tongsosanphamdaban.Text = query1.ToString();

            // THỐNG KÊ SẢN PHẨM

            var query2 = db.products.Where(x => x.Status == 1).ToList().Count();
            tongsosanphamphamdaduyet.Text = query2.ToString();

            var query4 = db.products.Where(x => x.PhapLy == 1).ToList().Count();
            lttoilanhasanSuat.Text = query4.ToString();

            var query5 = db.products.Where(x => x.PhapLy == 2).ToList().Count();
            toilanhaphaphoi.Text = query5.ToString();

            var query6 = db.products.Where(x => x.PhapLy == 3).ToList().Count();
            toiladaily.Text = query6.ToString();

            var query7 = db.products.Where(x => x.PhapLy == 4).ToList().Count();
            toiladoituongkhac.Text = query7.ToString();

            //THỐNG KÊ HOA HỒNG BÁN HÀNG

            var itemsInCart = from o in db.LoiNhuanMuaBans select new { o.SoDiemGoc };
            var sum = itemsInCart.ToList().Select(c => Convert.ToDouble(c.SoDiemGoc)).Sum();
            tongdiemgoc.Text = sum.ToString();

            var itemsInCart2 = from o in db.LoiNhuanMuaBans select new { o.SoDiemDaChia };
            var sum2 = itemsInCart2.ToList().Select(c => Convert.ToDouble(c.SoDiemDaChia)).Sum();
            sodiemdachiahh.Text = sum2.ToString();

            var itemsInCart3 = from o in db.LoiNhuanMuaBans select new { o.SoDiemConLai };
            var sum3 = itemsInCart3.ToList().Select(c => Convert.ToDouble(c.SoDiemConLai)).Sum();
            sodiemconlai.Text = sum3.ToString();


            //THỐNG KÊ HOA HỒNG KÍCH HOẠT 480

            var itemsInCart4 = from o in db.LoiNhuanDangKyThanhViens select new { o.SoDiemNapVao };
            var sum4 = itemsInCart4.ToList().Select(c => Convert.ToDouble(c.SoDiemNapVao)).Sum();
            sodiemgoc.Text = sum4.ToString();


            var itemsInCart5 = from o in db.LoiNhuanDangKyThanhViens select new { o.SoDiemDaChia };
            var sum5 = itemsInCart5.ToList().Select(c => Convert.ToDouble(c.SoDiemDaChia)).Sum();
            ltdiemdachiahhdk.Text = sum5.ToString();


            var itemsInCart6 = from o in db.LoiNhuanDangKyThanhViens select new { o.SoDiemConLai };
            var sum6 = itemsInCart6.ToList().Select(c => Convert.ToDouble(c.SoDiemConLai)).Sum();
            sodiemconlaidk.Text = sum6.ToString();

            //THỐNG KÊ SAU KHI TRỪ THUẾ 10%

            var itemsInCart7 = from o in db.ChuyenDiemSangVi_Thues select new { o.SoDienBiTru };
            var sum7 = itemsInCart7.ToList().Select(c => Convert.ToDouble(c.SoDienBiTru)).Sum();
            sodiemthuthue.Text = sum7.ToString();

            //THỐNG KÊ RÚT TIỀN

            var itemsInCart8 = from o in db.LichSuRutTiens where o.TrangThai == 1 select new { o.SoTienCanRut };
            var sum8 = itemsInCart8.ToList().Select(c => Convert.ToDouble(c.SoTienCanRut)).Sum();
            tongtiendarut.Text = sum8.ToString();

            //THỐNG KÊ ADMIN CẤP ĐIỂM

            var Vi1 = from o in db.CapDiemThanhViens where o.IDNguoiCap == 0 && o.KieuVi == 1 select new { o.SoDiemCoin };
            var Tong1 = Vi1.ToList().Select(c => Convert.ToDouble(c.SoDiemCoin)).Sum();
            adminVITM.Text = Tong1.ToString();


            var Vi2 = from o in db.CapDiemThanhViens where o.IDNguoiCap == 0 && o.KieuVi == 2 select new { o.SoDiemCoin };
            var Tong2 = Vi2.ToList().Select(c => Convert.ToDouble(c.SoDiemCoin)).Sum();
            viquanly.Text = Tong2.ToString();

            var Vi3 = from o in db.CapDiemThanhViens where o.IDNguoiCap == 0 && o.KieuVi == 5 select new { o.SoDiemCoin };
            var Tong3 = Vi3.ToList().Select(c => Convert.ToDouble(c.SoDiemCoin)).Sum();
            vivip.Text = Tong3.ToString();

            //THỐNG KÊ TỔNG THU NHẬP CHÊNH LỆCH GIÁ

            var chenh = from o in db.HoaHongThanhViens where o.IDType == 301 select new { o.SoCoin };
            var Tong4 = chenh.ToList().Select(c => Convert.ToDouble(c.SoCoin)).Sum();
            tongdiemchenhlech.Text = Tong4.ToString();

            //Thống kê Thành viên

            var Thanhvien1 = db.users.ToList().Count();
            tongthanhvien.Text = Thanhvien1.ToString();

            var Thanhvien2 = db.users.Where(x => x.DuyetTienDanap == 1).ToList().Count();
            thanhviendakichhoat.Text = Thanhvien2.ToString();

            var Thanhvien3 = db.users.Where(x => x.DuyetTienDanap == 0).ToList().Count();
            thanhvienchuakichhoat.Text = Thanhvien3.ToString();

            var tlevel0 = db.users.Where(x => x.LevelThanhVien == 0).ToList().Count();
            Level0.Text = tlevel0.ToString();

            var tlevel1 = db.users.Where(x => x.LevelThanhVien == 1).ToList().Count();
            Level1.Text = tlevel1.ToString();

            var tlevel2 = db.users.Where(x => x.LevelThanhVien == 2).ToList().Count();
            Level2.Text = tlevel2.ToString();

            var tlevel3 = db.users.Where(x => x.LevelThanhVien == 3).ToList().Count();
            Level3.Text = tlevel3.ToString();

            var tlevel4 = db.users.Where(x => x.LevelThanhVien == 4).ToList().Count();
            Level4.Text = tlevel4.ToString();

            var tlevel5 = db.users.Where(x => x.LevelThanhVien == 5).ToList().Count();
            Level5.Text = tlevel5.ToString();

            var chinhanh = db.Menus.Where(x => x.capp == More.DL).ToList().Count();
            ltchinhanh.Text = chinhanh.ToString();

            var thanhvienss = db.users.Where(x => x.Type == 1).ToList().Count();
            thanhviendaily.Text = thanhvienss.ToString();

            var thanhvienss2 = db.users.Where(x => x.Type == 2).ToList().Count();
            thanhviennhacungcap.Text = thanhvienss2.ToString();

            var thanhvienss3 = db.users.Where(x => x.Type == 2 && x.TongSoSanPham != 0).ToList().Count();
            thanhviendangsanpham.Text = thanhvienss3.ToString();

            var thanhvienss4 = db.users.Where(x => x.istatus == 0).ToList().Count();
            ltbikhoa.Text = thanhvienss4.ToString();

            var thanhvienss5 = db.users.Where(x => x.TatChucNang == 1).ToList().Count();
            ltkhoachucnang.Text = thanhvienss5.ToString();

            var thanhvienss6 = db.users.Where(x => x.CuaHang == 1).ToList().Count();
            ltcuahang.Text = thanhvienss6.ToString();

            var thanhvienss7 = db.users.Where(x => x.Leader == 1).ToList().Count();
            leader.Text = thanhvienss7.ToString();



            //// var query = from ee in db.Employees.ToList()
            //                join cc in db.Orders.ToList()
            //                on ee.EmployeeID equals cc.EmployeeID
            //                where ee.HireDate.Value.Year == 1992
            //                where ee.HireDate.Value.Month == 8
            //                select new
            //                {
            //                    ee.EmployeeID,
            //                    ee.FirstName,
            //                    ee.HireDate,
            //                    cc.OrderDate
            //                };


            //  var result = obj.Where(r => Convert.ToDateTime(r.Date).Month == month);
           // List <content>= db.content.ToList().Where(u =>Convert.ToDateTime(u.Date).Month==M_no)).ToList(); //first convert to List then make the where conditon
        }
    }
}
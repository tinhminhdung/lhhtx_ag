using Entity;
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
    public partial class ThongKe : System.Web.UI.UserControl
    {
        private string language = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowThanhVien();
        }
        private void ShowThanhVien()
        {
            //lttongcapdiem.Text = ShowCapDiem();
            //ltdanhthuagland.Text = ShowLaiSuatAGLANG();
            //lttongdoanhthubanhang.Text = ShowSanPham();
            //lttonghoahongmuaban.Text = ShowHoaHong();
            //try
            //{
            //    Double Tong1 = Convert.ToDouble(ShowCapDiem().Replace(",", ""));
            //    Double Tong2 = Convert.ToDouble(ShowLaiSuatAGLANG().Replace(",", ""));
            //    Double Tong3 = Convert.ToDouble(ShowSanPham().Replace(",", ""));
            //    Double Tong4 = Convert.ToDouble(ShowHoaHong().Replace(",", ""));

            //    Double Tong5 = (Tong1 + Tong2 + Tong3) - Tong4;
            //    ltlaisuat.Text = AllQuery.MorePro.FormatMoney_NO(Tong5.ToString());
            //}
            //catch (Exception)
            //{ }
        }
        //protected string ShowSanPham()
        //{
        //    List<product> table = db.products.Where(s => s.Status == 1).ToList();
        //    if (table.Count > 0)
        //    {
        //        double coin = 0.0;
        //        for (int i = 0; i < table.Count; i++)
        //        {
        //            try
        //            {
        //                coin += Convert.ToDouble(Commond.DiemTichLuyAdd(table[i].GiaThanhVien.ToString(), table[i].Giacongtynhapvao.ToString()));
        //            }
        //            catch (Exception)
        //            {
        //            }
        //        }
        //        return AllQuery.MorePro.FormatMoney_NO(coin.ToString());
        //    }
        //    else
        //    {
        //        return "0";
        //    }
        //}

        //protected string ShowHoaHong()
        //{
        //    List<EHoaHongThanhVien> table = SHoaHongThanhVien.Name_Text("SELECT * from HoaHongThanhVien where IDType in (10,11,12,6,7,71,72,73,74,75,76,77,78,79,9,13,30)");
        //    if (table.Count > 0)
        //    {
        //        double coin = 0.0;
        //        for (int i = 0; i < table.Count; i++)
        //        {
        //            coin += Convert.ToDouble(table[i].SoCoin.ToString());
        //        }
        //        return AllQuery.MorePro.FormatMoney_NO(coin.ToString());
        //    }
        //    else
        //    {
        //        return "0";
        //    }
        //}

        //protected string ShowCapDiem()
        //{
        //    List<CapDiemThanhVien> table = db.CapDiemThanhViens.Where(s => s.IDNguoiCap == 0).ToList();
        //    if (table.Count > 0)
        //    {
        //        double coin = 0.0;
        //        for (int i = 0; i < table.Count; i++)
        //        {
        //            coin += Convert.ToDouble(table[i].SoDiemCoin.ToString());
        //        }
        //        return AllQuery.MorePro.FormatMoney_NO(coin.ToString());
        //    }
        //    else
        //    {
        //        return "0";
        //    }
        //}
        //protected string ShowLaiSuatAGLANG()
        //{
        //    List<LaiSuatAGLANG> table = db.LaiSuatAGLANGs.Where(s => s.TrangThai == 1).ToList();
        //    if (table.Count > 0)
        //    {
        //        double coin = 0.0;
        //        for (int i = 0; i < table.Count; i++)
        //        {
        //            coin += Convert.ToDouble(table[i].SoTienDauTu.ToString());
        //        }
        //        Double Tong = Convert.ToDouble(coin / 1000);
        //        return AllQuery.MorePro.FormatMoney_NO(Tong.ToString());
        //    }
        //    else
        //    {
        //        return "0";
        //    }
        //}

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce
{
    public partial class TinhGiaNewsFree : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Double GiaNY = Convert.ToDouble(400000) / 1000;
            Double GiaBanChoAg = Convert.ToDouble(142000) / 1000;
            Double GiaGoc = Convert.ToDouble(145000) / 1000;

            // Nếu thành viên là Free
            Double TongTien = 0;
            //  if()
            TongTien = GiaNY - GiaGoc;
            Response.Write("Free " + TongTien.ToString() + " .<br>");

            Double TraChoCongtTy = GiaGoc - GiaBanChoAg;

            Response.Write("Trả cho Coong ty Aggroupusa : " + TraChoCongtTy.ToString() + " điểm <br>");

            Double Free25 = (TongTien * 25) / 100;

            Response.Write("Trả cho thành viên Free " + Free25.ToString() + " điểm vào ví mua hàng.<br>");

            Double DemdichiaHHFree = (TongTien - Free25);

            Response.Write("Chia cho thành viên Freee Hoa Hồng " + DemdichiaHHFree.ToString() + " mang đi chia hoa hồng.<br>");


            //***************************

            Response.Write("<hr><br>");

            Double DaiLy = (TongTien * 70) / 100;

            Response.Write("Trả cho thành viên Đại lý " + DaiLy.ToString() + " Trừ luôn vào giỏ hàng khi đi mua hàng.<br>");

            Double DemdichiaHHDaiLy = (TongTien - DaiLy);

            Response.Write("Chia cho thành viên Đại lý Hoa Hồng " + DemdichiaHHDaiLy.ToString() + " mang đi chia hoa hồng.<br>");

           // double Tong = (TongCoin * HoaHongs) / 100;

            //Response.Write("Chia cho thành viên Freee Hoa Hồng " + Free.ToString() + " mang đi chia hoa hồng.<br>");

        }
    }
}
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce
{
    public partial class tinhhoahong : System.Web.UI.Page
    {
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                //#region Chép hoa hồng sang ví tiền hoa hồng mới
                //List<Entity.users> bt = Susers.Name_Text("select * from users  order by iuser_id asc");// laatys toàn bộ danh sách thành viên
                //if (bt.Count > 0)
                //{
                //    foreach (var item in bt)
                //    {
                //        //Lấy ra 5 thành viên cấp dưới 
                //        List<Entity.users> bt1 = Susers.Name_Text("SELECT top 5 * FROM users WHERE((MTree LIKE N'%|'+CONVERT(varchar, " + item.iuser_id.ToString() + ")+'|%')) order by iuser_id asc");
                //        if (bt1.Count > 0)
                //        {
                //            foreach (var item1 in bt1)
                //            {
                //                string comand = "SELECT * FROM HoaHongThanhVien where IDType in(1,3) and IDUserNguoiDuocHuong=" + item1.iuser_id.ToString() + " and TrangThai=1";
                //                // tính tiền cho thành viên
                //                List<HoaHongThanhVien> dt = db.ExecuteQuery<HoaHongThanhVien>(@"" + comand + "").ToList();
                //                if (dt.Count > 0)
                //                {
                //                    for (int i = 0; i < dt.Count; i++)
                //                    {
                //                        #region Cộng điểm coin vào bảng thành viên
                //                        List<Entity.users> iitem = Susers.Name_Text("select * from users where iuser_id=" + dt[i].IDUserNguoiDuocHuong.ToString() + "");
                //                        if (iitem != null)
                //                        {
                //                            double TongSoCoinDaCo = Convert.ToDouble(iitem[0].ViTienHHGioiThieu);
                //                            double TongTienNapVao = Convert.ToDouble(dt[i].SoCoin.ToString());
                //                            double Conglai = 0;
                //                            Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));
                //                            Susers.Name_Text("update users set ViTienHHGioiThieu=" + Conglai.ToString() + "  where iuser_id=" + iitem[0].iuser_id.ToString() + "");
                //                        }
                //                        #endregion
                //                    }
                //                }
                //                // cập nhật trạng thái đã quyet rồi 
                //                db.S_TTTTTTTTTTTTT(int.Parse(item1.iuser_id.ToString()));
                //            }
                //        }
                //    }
                //}
                //#endregion
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            #region Chép hoa hồng sang ví tiền hoa hồng mới
            List<Entity.users> bt = Susers.Name_Text("select * from users where iuser_id in(" + txtid.Text + ") order by iuser_id asc");// laatys toàn bộ danh sách thành viên
            if (bt.Count > 0)
            {
                foreach (var item in bt)
                {
                    #region 5 thành viên giới thiệu trực tiếp
                    string comand = "SELECT top 5 * FROM HoaHongThanhVien where IDType=1 and IDUserNguoiDuocHuong=" + item.iuser_id.ToString() + "";
                    // tính tiền cho thành viên
                    List<Entity.EHoaHongThanhVien> dt = SHoaHongThanhVien.Name_Text(comand);
                    if (dt.Count > 0)
                    {
                        for (int i = 0; i < dt.Count; i++)
                        {
                            #region Cộng điểm coin vào bảng thành viên
                            List<Entity.users> iitem = Susers.Name_Text("select * from users where iuser_id=" + item.iuser_id.ToString() + "");
                            if (iitem != null)
                            {
                                double TongSoCoinDaCo = Convert.ToDouble(iitem[0].ViTienHHGioiThieu);
                                double TongTienNapVao = Convert.ToDouble(dt[i].SoCoin.ToString());
                                double Conglai = 0;
                                Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));
                                Susers.Name_Text("update users set ViTienHHGioiThieu=" + Conglai.ToString() + "  where iuser_id=" + item.iuser_id.ToString() + "");
                            }
                            #endregion
                        }
                    }
                    #endregion

                    #region #region 5 thành viên giới thiệu gián tiếp
                    string comand2 = "SELECT top 5 * FROM HoaHongThanhVien where IDType=3 and IDUserNguoiDuocHuong=" + item.iuser_id.ToString() + " ";
                    // tính tiền cho thành viên
                    List<Entity.EHoaHongThanhVien> dt2 = SHoaHongThanhVien.Name_Text(comand2);
                    if (dt2.Count > 0)
                    {
                        for (int i = 0; i < dt2.Count; i++)
                        {
                            #region Cộng điểm coin vào bảng thành viên
                            List<Entity.users> iitem = Susers.Name_Text("select * from users where iuser_id=" + item.iuser_id.ToString() + "");
                            if (iitem != null)
                            {
                                double TongSoCoinDaCo = Convert.ToDouble(iitem[0].ViTienHHGioiThieu);
                                double TongTienNapVao = Convert.ToDouble(dt2[i].SoCoin.ToString());
                                double Conglai = 0;
                                Conglai = ((TongSoCoinDaCo) + (TongTienNapVao));
                                Susers.Name_Text("update users set ViTienHHGioiThieu=" + Conglai.ToString() + "  where iuser_id=" + item.iuser_id.ToString() + "");
                            }
                            #endregion
                        }
                    }
                    #endregion
                    // cập nhật trạng thái đã quyet rồi 
                    //  db.S_TTTTTTTTTTTTT(int.Parse(item.iuser_id.ToString()));
                }
                Literal1.Text += "Tổng cộng " + bt.Count.ToString();
            }
            #endregion
            Literal1.Text += "  / kết thúc ";

        }
    }
}
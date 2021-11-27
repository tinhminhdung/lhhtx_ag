using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce
{
    public partial class DemoLayraLeader : System.Web.UI.Page
    {
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            string chuoi = "";
            //List<Entity.users> dt = Susers.Name_Text("select * from users order by iuser_id asc");
            //List<Entity.users> dt1 = dt;
            //if (dt.Count > 0)
            //{
            //    foreach (var item in dt)
            //    {
            //        foreach (var item2 in dt1)
            //        {
            //            if ((item.iuser_id != item2.iuser_id) && (item2.GioiThieu != "0"))
            //            {
            //                if ((item.iuser_id == int.Parse(item2.GioiThieu)))
            //                {
            //                    user abcv = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(item2.GioiThieu.ToString()));

            //                    chuoi = abcv.MTree + "|" + item2.GioiThieu + "|";

            //                    //user abc = db.users.SingleOrDefault(p => p.iuser_id == int.Parse(item2.iuser_id.ToString()));
            //                    //string ccc = abcv.MTree + "|" + item2.GioiThieu + "|";
            //                    //abc.MTree = ccc.Replace("||", "|");
            //                    //db.SubmitChanges();
            //                }
            //            }

            //        }
            //    }
            //}
            //Response.Write(chuoi + "<br><br>");

            //string str = "";
            //List<Entity.users> F00 = Susers.Name_Text("select * from users  where iuser_id in (15,15,23,24,25,26,28,81,83,123,9418)");
            //if (F00.Count() > 0)
            //{
            //    foreach (var item in F00)
            //    {
            //        str += item.iuser_id + "<br>";
            //    }
            //}
            //Response.Write(str);
        }


        //Truy vấn ra 1 danh sách cấp dưới mình 
        //Ví dụ:  <%=ShowCapDuoi("6034") %>
        //=kết quả: 6034|6032|6029|6027|6024|6023|6022|6018|6016|6015|6013|6008|6005|6004|6001|5997|5990|5985|5122|4738|3175|3018|2967|1280|228|143|120|83|81|28|26|25|24|23|15|
        protected string ShowCapDuoi(string id)
        {
            string str = "";
            List<Entity.users> dt = Susers.Name_Text("select * from users  where iuser_id =" + id + "");
            if (dt.Count > 0)
            {
                foreach (Entity.users item in dt)
                {
                    str += item.iuser_id + "|" + ShowCapDuoi(item.GioiThieu.ToString());
                }
            }
            return str.ToString();
            // sau khi có kết quả thì truy vấn để lấy ra leader gần nhất để cho hoa hồng
            //select top 1 * from users where iuser_id in(6034,6032,6029,6027,6024,6023,6022,6018,6016,6015,6013,6008,6005,6004,6001,5997,5990,5985,5122,4738,3175,3018,2967,1280,228,143,120,83,81,28,26,25,24,23,15) and LevelThanhVien=1 order by iuser_id desc
       
        
             //Dựa vào đây lọc ra các ID có level và sẽ dựa vào đây để cho hoa hồng, bớt đi cách viết 50 vòng lặp
            //select LevelThanhVien, iuser_id from users where iuser_id in(6034,6032,6029,6027,6024,6023,6022,6018,6016,6015,6013,6008,6005,6004,6001,5997,5990,5985,5122,4738,3175,3018,2967,1280,228,143,120,83,81,28,26,25,24,23,15) and LevelThanhVien!=0 order by iuser_id desc

        
        }
      
    }
}
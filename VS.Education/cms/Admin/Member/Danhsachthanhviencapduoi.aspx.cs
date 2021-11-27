using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Admin.Member
{
    public partial class Danhsachthanhviencapduoi : System.Web.UI.Page
    {
        string IDThanhVien = "";
        DatalinqDataContext db = new DatalinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["IDThanhVien"] != null && !Request["IDThanhVien"].Equals(""))
            {
                IDThanhVien = Request["IDThanhVien"];
                var table = db.S_ThanhVienCapDuoi(int.Parse(IDThanhVien)).ToList();
                if (table.Count > 0)
                {
                    Literal1.Text = "";
                    rp_pagelist.DataSource = table;
                    rp_pagelist.DataBind();
                }
                else
                {
                    Literal1.Text = "<b style='color:red'>Không có dữ liệu</b>";
                }
            }
            //SELECT iuser_id,vuserun,vphone,vemail,vaddress,MTree FROM users  WHERE((MTree LIKE N'%|'+CONVERT(varchar,86)+'|%'))
        }

        protected void btxuatEXEL_Click(object sender, EventArgs e)
        {
            string Namefile = "DanhSachThanhVienCapDuoi";
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
            sb.Append("</tr>");
            var dt = db.S_ThanhVienCapDuoi(int.Parse(IDThanhVien)).ToList();
            if (dt.Count > 0)
            {
                int i = 1;
                foreach (var item in dt)
                {
                    sb.Append("    <tr>");
                    sb.Append("    <td style=\"width:50px; vertical-align:middle; height: 22px; text-align: center;\">" + i++ + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + item.vuserun + "</td>");
                    sb.Append("    <td style=\"width:400px; vertical-align:middle; text-align: left;\">" + item.vfname + "</td>");
                    sb.Append("    <td style=\"width:250px; vertical-align:middle;text-align: left;\">" + item.vaddress + "</td>");
                    sb.Append("    <td style=\"width:150px; vertical-align:middle;text-align: left;\">" + item.vphone + "</td>");
                    sb.Append("    <td style=\"width:520px; vertical-align:middle; text-align: left;\">" + item.vemail + "</td>");
                    sb.Append("  </tr>");
                }
            }
            sb.Append("</table>");
            Response.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }
    }
}
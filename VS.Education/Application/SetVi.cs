using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using TestWindowService;
using VS.E_Commerce;

public class SetVi
{
    // khóa chức năng cho chuyên gia(Xếp vân yêu cầu / 01/09/2020 - Chiều)
    // Ngày 20/11/2020 xếp vân và a thủy trao đổi làm 2 loại hoa hồng chuyện gia , 1 đăng ký 480 và mua bán. Sếp vân có ký sổ
    public static string SetThanhVienChuyenGia()
    {
        // 9443 là tạm thời để test còn ID chính là : 67357
        string bc = System.Web.HttpContext.Current.Request.Url.Authority + System.Web.HttpContext.Current.Request.RawUrl.ToString();
        if (!bc.Contains("localhost"))
        {
            return "67357";
        }
        return "9443";
    }

    public static string SetViDongHuongDoanhSo()
    {
        // 231 là tạm thời để test còn ID chính là : 69460
        string bc = System.Web.HttpContext.Current.Request.Url.Authority + System.Web.HttpContext.Current.Request.RawUrl.ToString();
        if (!bc.Contains("localhost"))
        {
            return "69460";
        }
        return "231";
    }

    public static string SetViThuongQuanLy()
    {
        // 233 là tạm thời để test còn ID chính là : 69501
        string bc = System.Web.HttpContext.Current.Request.Url.Authority + System.Web.HttpContext.Current.Request.RawUrl.ToString();
        if (!bc.Contains("localhost"))
        {
            return "69501";
        }
        return "233";
    }
}

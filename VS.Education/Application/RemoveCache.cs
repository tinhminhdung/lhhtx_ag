using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class RemoveCache
{
    public static string Menu()
    {
        HttpContext.Current.Cache.Remove("MenuBottom");
        HttpContext.Current.Cache.Remove("MenuTop");
        HttpContext.Current.Cache.Remove("Showtab");
        HttpContext.Current.Cache.Remove("Menuleft");
        HttpContext.Current.Cache.Remove("MenuDuoi");
        HttpContext.Current.Cache.Remove("MenuPro");
        HttpContext.Current.Cache.Remove("MenuMobile");
        HttpContext.Current.Cache.Remove("MenuNews");
        return "";
    }
    public static string Products()
    {
        HttpContext.Current.Cache.Remove("AGShowNhomsanpham");
        HttpContext.Current.Cache.Remove("LoadCoTheBanThich");
        HttpContext.Current.Cache.Remove("ShowLoadPro");
        HttpContext.Current.Cache.Remove("ShowBanchay");
        HttpContext.Current.Cache.Remove("ShowLoadProNoiBat");
        return "";
    }

    public static string Products_()
    {
        HttpContext.Current.Cache.Remove("LocThuongHieu");
        HttpContext.Current.Cache.Remove("LocTheoGia");

        return "";
    }

    public static string News()
    {
       HttpContext.Current.Cache.Remove("Showtinxemnhieu");
       HttpContext.Current.Cache.Remove("Newsss");
        return "";
    }
    public static string Album()
    {
        //HttpContext.Current.Cache.Remove("ShowNhomsanpham");
        return "";
    }
    public static string Video()
    {
        //HttpContext.Current.Cache.Remove("ShowNhomsanpham");
        return "";
    }

    public static string NewsFooter()
    {
        //HttpContext.Current.Cache.Remove("ShowNhomsanpham");
        return "";
    }
    public static string QuangCao()
    {
        HttpContext.Current.Cache.Remove("ShowPhuogThucThanhToan");
        HttpContext.Current.Cache.Remove("ShowTimKiem");
        HttpContext.Current.Cache.Remove("Anhbenphai");
        HttpContext.Current.Cache.Remove("2anhduoibanner");
        HttpContext.Current.Cache.Remove("Sildechinh");
        HttpContext.Current.Cache.Remove("ShowDanhMuc");

        return "";
    }
}

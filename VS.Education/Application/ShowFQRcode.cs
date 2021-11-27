using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VS.E_Commerce;

public class ShowFQRcode
{
    public static string ShowF2(string IDF1, string IDF2)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + " ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF3(string IDF1, string IDF2, string IDF3)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF4(string IDF1, string IDF2, string IDF3, string IDF4)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf4 = Susers.Name_Text("select * from users  where iuser_id=" + IDF4 + "  ");
        if (dtf4.Count > 0)
        {
            return dtf4[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF5(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        return "0";
    }


    public static string ShowF6(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF7(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF8(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF9(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }

        return "0";
    }
    public static string ShowF10(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        return "0";
    }


    public static string ShowF11(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF12(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF13(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF14(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF15(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF16(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF17(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF18(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF19(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF20(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        return "0";
    }



    public static string ShowF21(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF22(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF23(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF24(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF25(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF26(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF27(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF28(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF29(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF30(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        return "0";
    }


    public static string ShowF31(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30, string IDF31)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf31 = Susers.Name_Text("select * from users  where iuser_id=" + IDF31 + "  ");
        if (dtf31.Count > 0)
        {
            return dtf31[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF32(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30, string IDF31, string IDF32)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf31 = Susers.Name_Text("select * from users  where iuser_id=" + IDF31 + "  ");
        if (dtf31.Count > 0)
        {
            return dtf31[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf32 = Susers.Name_Text("select * from users  where iuser_id=" + IDF32 + "  ");
        if (dtf32.Count > 0)
        {
            return dtf32[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF33(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30, string IDF31, string IDF32, string IDF33)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf31 = Susers.Name_Text("select * from users  where iuser_id=" + IDF31 + "  ");
        if (dtf31.Count > 0)
        {
            return dtf31[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf32 = Susers.Name_Text("select * from users  where iuser_id=" + IDF32 + "  ");
        if (dtf32.Count > 0)
        {
            return dtf32[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf33 = Susers.Name_Text("select * from users  where iuser_id=" + IDF33 + "  ");
        if (dtf33.Count > 0)
        {
            return dtf33[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF34(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30, string IDF31, string IDF32, string IDF33, string IDF34)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf31 = Susers.Name_Text("select * from users  where iuser_id=" + IDF31 + "  ");
        if (dtf31.Count > 0)
        {
            return dtf31[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf32 = Susers.Name_Text("select * from users  where iuser_id=" + IDF32 + "  ");
        if (dtf32.Count > 0)
        {
            return dtf32[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf33 = Susers.Name_Text("select * from users  where iuser_id=" + IDF33 + "  ");
        if (dtf33.Count > 0)
        {
            return dtf33[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf34 = Susers.Name_Text("select * from users  where iuser_id=" + IDF34 + "  ");
        if (dtf34.Count > 0)
        {
            return dtf34[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF35(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30, string IDF31, string IDF32, string IDF33, string IDF34, string IDF35)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf31 = Susers.Name_Text("select * from users  where iuser_id=" + IDF31 + "  ");
        if (dtf31.Count > 0)
        {
            return dtf31[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf32 = Susers.Name_Text("select * from users  where iuser_id=" + IDF32 + "  ");
        if (dtf32.Count > 0)
        {
            return dtf32[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf33 = Susers.Name_Text("select * from users  where iuser_id=" + IDF33 + "  ");
        if (dtf33.Count > 0)
        {
            return dtf33[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf34 = Susers.Name_Text("select * from users  where iuser_id=" + IDF34 + "  ");
        if (dtf34.Count > 0)
        {
            return dtf34[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf35 = Susers.Name_Text("select * from users  where iuser_id=" + IDF35 + "  ");
        if (dtf35.Count > 0)
        {
            return dtf35[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF36(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30, string IDF31, string IDF32, string IDF33, string IDF34, string IDF35, string IDF36)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf31 = Susers.Name_Text("select * from users  where iuser_id=" + IDF31 + "  ");
        if (dtf31.Count > 0)
        {
            return dtf31[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf32 = Susers.Name_Text("select * from users  where iuser_id=" + IDF32 + "  ");
        if (dtf32.Count > 0)
        {
            return dtf32[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf33 = Susers.Name_Text("select * from users  where iuser_id=" + IDF33 + "  ");
        if (dtf33.Count > 0)
        {
            return dtf33[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf34 = Susers.Name_Text("select * from users  where iuser_id=" + IDF34 + "  ");
        if (dtf34.Count > 0)
        {
            return dtf34[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf35 = Susers.Name_Text("select * from users  where iuser_id=" + IDF35 + "  ");
        if (dtf35.Count > 0)
        {
            return dtf35[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf36 = Susers.Name_Text("select * from users  where iuser_id=" + IDF36 + "  ");
        if (dtf36.Count > 0)
        {
            return dtf36[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF37(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30, string IDF31, string IDF32, string IDF33, string IDF34, string IDF35, string IDF36, string IDF37)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf31 = Susers.Name_Text("select * from users  where iuser_id=" + IDF31 + "  ");
        if (dtf31.Count > 0)
        {
            return dtf31[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf32 = Susers.Name_Text("select * from users  where iuser_id=" + IDF32 + "  ");
        if (dtf32.Count > 0)
        {
            return dtf32[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf33 = Susers.Name_Text("select * from users  where iuser_id=" + IDF33 + "  ");
        if (dtf33.Count > 0)
        {
            return dtf33[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf34 = Susers.Name_Text("select * from users  where iuser_id=" + IDF34 + "  ");
        if (dtf34.Count > 0)
        {
            return dtf34[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf35 = Susers.Name_Text("select * from users  where iuser_id=" + IDF35 + "  ");
        if (dtf35.Count > 0)
        {
            return dtf35[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf36 = Susers.Name_Text("select * from users  where iuser_id=" + IDF36 + "  ");
        if (dtf36.Count > 0)
        {
            return dtf36[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf37 = Susers.Name_Text("select * from users  where iuser_id=" + IDF37 + "  ");
        if (dtf37.Count > 0)
        {
            return dtf37[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF38(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30, string IDF31, string IDF32, string IDF33, string IDF34, string IDF35, string IDF36, string IDF37, string IDF38)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf31 = Susers.Name_Text("select * from users  where iuser_id=" + IDF31 + "  ");
        if (dtf31.Count > 0)
        {
            return dtf31[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf32 = Susers.Name_Text("select * from users  where iuser_id=" + IDF32 + "  ");
        if (dtf32.Count > 0)
        {
            return dtf32[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf33 = Susers.Name_Text("select * from users  where iuser_id=" + IDF33 + "  ");
        if (dtf33.Count > 0)
        {
            return dtf33[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf34 = Susers.Name_Text("select * from users  where iuser_id=" + IDF34 + "  ");
        if (dtf34.Count > 0)
        {
            return dtf34[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf35 = Susers.Name_Text("select * from users  where iuser_id=" + IDF35 + "  ");
        if (dtf35.Count > 0)
        {
            return dtf35[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf36 = Susers.Name_Text("select * from users  where iuser_id=" + IDF36 + "  ");
        if (dtf36.Count > 0)
        {
            return dtf36[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf37 = Susers.Name_Text("select * from users  where iuser_id=" + IDF37 + "  ");
        if (dtf37.Count > 0)
        {
            return dtf37[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf38 = Susers.Name_Text("select * from users  where iuser_id=" + IDF38 + "  ");
        if (dtf38.Count > 0)
        {
            return dtf38[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF39(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30, string IDF31, string IDF32, string IDF33, string IDF34, string IDF35, string IDF36, string IDF37, string IDF38, string IDF39)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf31 = Susers.Name_Text("select * from users  where iuser_id=" + IDF31 + "  ");
        if (dtf31.Count > 0)
        {
            return dtf31[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf32 = Susers.Name_Text("select * from users  where iuser_id=" + IDF32 + "  ");
        if (dtf32.Count > 0)
        {
            return dtf32[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf33 = Susers.Name_Text("select * from users  where iuser_id=" + IDF33 + "  ");
        if (dtf33.Count > 0)
        {
            return dtf33[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf34 = Susers.Name_Text("select * from users  where iuser_id=" + IDF34 + "  ");
        if (dtf34.Count > 0)
        {
            return dtf34[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf35 = Susers.Name_Text("select * from users  where iuser_id=" + IDF35 + "  ");
        if (dtf35.Count > 0)
        {
            return dtf35[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf36 = Susers.Name_Text("select * from users  where iuser_id=" + IDF36 + "  ");
        if (dtf36.Count > 0)
        {
            return dtf36[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf37 = Susers.Name_Text("select * from users  where iuser_id=" + IDF37 + "  ");
        if (dtf37.Count > 0)
        {
            return dtf37[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf38 = Susers.Name_Text("select * from users  where iuser_id=" + IDF38 + "  ");
        if (dtf38.Count > 0)
        {
            return dtf38[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf39 = Susers.Name_Text("select * from users  where iuser_id=" + IDF39 + "  ");
        if (dtf39.Count > 0)
        {
            return dtf39[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF40(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30, string IDF31, string IDF32, string IDF33, string IDF34, string IDF35, string IDF36, string IDF37, string IDF38, string IDF39, string IDF40)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf31 = Susers.Name_Text("select * from users  where iuser_id=" + IDF31 + "  ");
        if (dtf31.Count > 0)
        {
            return dtf31[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf32 = Susers.Name_Text("select * from users  where iuser_id=" + IDF32 + "  ");
        if (dtf32.Count > 0)
        {
            return dtf32[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf33 = Susers.Name_Text("select * from users  where iuser_id=" + IDF33 + "  ");
        if (dtf33.Count > 0)
        {
            return dtf33[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf34 = Susers.Name_Text("select * from users  where iuser_id=" + IDF34 + "  ");
        if (dtf34.Count > 0)
        {
            return dtf34[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf35 = Susers.Name_Text("select * from users  where iuser_id=" + IDF35 + "  ");
        if (dtf35.Count > 0)
        {
            return dtf35[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf36 = Susers.Name_Text("select * from users  where iuser_id=" + IDF36 + "  ");
        if (dtf36.Count > 0)
        {
            return dtf36[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf37 = Susers.Name_Text("select * from users  where iuser_id=" + IDF37 + "  ");
        if (dtf37.Count > 0)
        {
            return dtf37[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf38 = Susers.Name_Text("select * from users  where iuser_id=" + IDF38 + "  ");
        if (dtf38.Count > 0)
        {
            return dtf38[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf39 = Susers.Name_Text("select * from users  where iuser_id=" + IDF39 + "  ");
        if (dtf39.Count > 0)
        {
            return dtf39[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf40 = Susers.Name_Text("select * from users  where iuser_id=" + IDF40 + "  ");
        if (dtf40.Count > 0)
        {
            return dtf40[0].LevelThanhVien.ToString();
        }
        return "0";
    }


    public static string ShowF41(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30, string IDF31, string IDF32, string IDF33, string IDF34, string IDF35, string IDF36, string IDF37, string IDF38, string IDF39, string IDF40, string IDF41)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf31 = Susers.Name_Text("select * from users  where iuser_id=" + IDF31 + "  ");
        if (dtf31.Count > 0)
        {
            return dtf31[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf32 = Susers.Name_Text("select * from users  where iuser_id=" + IDF32 + "  ");
        if (dtf32.Count > 0)
        {
            return dtf32[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf33 = Susers.Name_Text("select * from users  where iuser_id=" + IDF33 + "  ");
        if (dtf33.Count > 0)
        {
            return dtf33[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf34 = Susers.Name_Text("select * from users  where iuser_id=" + IDF34 + "  ");
        if (dtf34.Count > 0)
        {
            return dtf34[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf35 = Susers.Name_Text("select * from users  where iuser_id=" + IDF35 + "  ");
        if (dtf35.Count > 0)
        {
            return dtf35[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf36 = Susers.Name_Text("select * from users  where iuser_id=" + IDF36 + "  ");
        if (dtf36.Count > 0)
        {
            return dtf36[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf37 = Susers.Name_Text("select * from users  where iuser_id=" + IDF37 + "  ");
        if (dtf37.Count > 0)
        {
            return dtf37[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf38 = Susers.Name_Text("select * from users  where iuser_id=" + IDF38 + "  ");
        if (dtf38.Count > 0)
        {
            return dtf38[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf39 = Susers.Name_Text("select * from users  where iuser_id=" + IDF39 + "  ");
        if (dtf39.Count > 0)
        {
            return dtf39[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf40 = Susers.Name_Text("select * from users  where iuser_id=" + IDF40 + "  ");
        if (dtf40.Count > 0)
        {
            return dtf40[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf41 = Susers.Name_Text("select * from users  where iuser_id=" + IDF41 + "  ");
        if (dtf41.Count > 0)
        {
            return dtf41[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF42(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30, string IDF31, string IDF32, string IDF33, string IDF34, string IDF35, string IDF36, string IDF37, string IDF38, string IDF39, string IDF40, string IDF41, string IDF42)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf31 = Susers.Name_Text("select * from users  where iuser_id=" + IDF31 + "  ");
        if (dtf31.Count > 0)
        {
            return dtf31[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf32 = Susers.Name_Text("select * from users  where iuser_id=" + IDF32 + "  ");
        if (dtf32.Count > 0)
        {
            return dtf32[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf33 = Susers.Name_Text("select * from users  where iuser_id=" + IDF33 + "  ");
        if (dtf33.Count > 0)
        {
            return dtf33[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf34 = Susers.Name_Text("select * from users  where iuser_id=" + IDF34 + "  ");
        if (dtf34.Count > 0)
        {
            return dtf34[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf35 = Susers.Name_Text("select * from users  where iuser_id=" + IDF35 + "  ");
        if (dtf35.Count > 0)
        {
            return dtf35[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf36 = Susers.Name_Text("select * from users  where iuser_id=" + IDF36 + "  ");
        if (dtf36.Count > 0)
        {
            return dtf36[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf37 = Susers.Name_Text("select * from users  where iuser_id=" + IDF37 + "  ");
        if (dtf37.Count > 0)
        {
            return dtf37[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf38 = Susers.Name_Text("select * from users  where iuser_id=" + IDF38 + "  ");
        if (dtf38.Count > 0)
        {
            return dtf38[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf39 = Susers.Name_Text("select * from users  where iuser_id=" + IDF39 + "  ");
        if (dtf39.Count > 0)
        {
            return dtf39[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf40 = Susers.Name_Text("select * from users  where iuser_id=" + IDF40 + "  ");
        if (dtf40.Count > 0)
        {
            return dtf40[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf41 = Susers.Name_Text("select * from users  where iuser_id=" + IDF41 + "  ");
        if (dtf41.Count > 0)
        {
            return dtf41[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf42 = Susers.Name_Text("select * from users  where iuser_id=" + IDF42 + "  ");
        if (dtf42.Count > 0)
        {
            return dtf42[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF43(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30, string IDF31, string IDF32, string IDF33, string IDF34, string IDF35, string IDF36, string IDF37, string IDF38, string IDF39, string IDF40, string IDF41, string IDF42, string IDF43)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf31 = Susers.Name_Text("select * from users  where iuser_id=" + IDF31 + "  ");
        if (dtf31.Count > 0)
        {
            return dtf31[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf32 = Susers.Name_Text("select * from users  where iuser_id=" + IDF32 + "  ");
        if (dtf32.Count > 0)
        {
            return dtf32[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf33 = Susers.Name_Text("select * from users  where iuser_id=" + IDF33 + "  ");
        if (dtf33.Count > 0)
        {
            return dtf33[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf34 = Susers.Name_Text("select * from users  where iuser_id=" + IDF34 + "  ");
        if (dtf34.Count > 0)
        {
            return dtf34[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf35 = Susers.Name_Text("select * from users  where iuser_id=" + IDF35 + "  ");
        if (dtf35.Count > 0)
        {
            return dtf35[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf36 = Susers.Name_Text("select * from users  where iuser_id=" + IDF36 + "  ");
        if (dtf36.Count > 0)
        {
            return dtf36[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf37 = Susers.Name_Text("select * from users  where iuser_id=" + IDF37 + "  ");
        if (dtf37.Count > 0)
        {
            return dtf37[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf38 = Susers.Name_Text("select * from users  where iuser_id=" + IDF38 + "  ");
        if (dtf38.Count > 0)
        {
            return dtf38[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf39 = Susers.Name_Text("select * from users  where iuser_id=" + IDF39 + "  ");
        if (dtf39.Count > 0)
        {
            return dtf39[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf40 = Susers.Name_Text("select * from users  where iuser_id=" + IDF40 + "  ");
        if (dtf40.Count > 0)
        {
            return dtf40[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf41 = Susers.Name_Text("select * from users  where iuser_id=" + IDF41 + "  ");
        if (dtf41.Count > 0)
        {
            return dtf41[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf42 = Susers.Name_Text("select * from users  where iuser_id=" + IDF42 + "  ");
        if (dtf42.Count > 0)
        {
            return dtf42[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf43 = Susers.Name_Text("select * from users  where iuser_id=" + IDF43 + "  ");
        if (dtf43.Count > 0)
        {
            return dtf43[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF44(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30, string IDF31, string IDF32, string IDF33, string IDF34, string IDF35, string IDF36, string IDF37, string IDF38, string IDF39, string IDF40, string IDF41, string IDF42, string IDF43, string IDF44)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf31 = Susers.Name_Text("select * from users  where iuser_id=" + IDF31 + "  ");
        if (dtf31.Count > 0)
        {
            return dtf31[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf32 = Susers.Name_Text("select * from users  where iuser_id=" + IDF32 + "  ");
        if (dtf32.Count > 0)
        {
            return dtf32[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf33 = Susers.Name_Text("select * from users  where iuser_id=" + IDF33 + "  ");
        if (dtf33.Count > 0)
        {
            return dtf33[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf34 = Susers.Name_Text("select * from users  where iuser_id=" + IDF34 + "  ");
        if (dtf34.Count > 0)
        {
            return dtf34[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf35 = Susers.Name_Text("select * from users  where iuser_id=" + IDF35 + "  ");
        if (dtf35.Count > 0)
        {
            return dtf35[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf36 = Susers.Name_Text("select * from users  where iuser_id=" + IDF36 + "  ");
        if (dtf36.Count > 0)
        {
            return dtf36[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf37 = Susers.Name_Text("select * from users  where iuser_id=" + IDF37 + "  ");
        if (dtf37.Count > 0)
        {
            return dtf37[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf38 = Susers.Name_Text("select * from users  where iuser_id=" + IDF38 + "  ");
        if (dtf38.Count > 0)
        {
            return dtf38[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf39 = Susers.Name_Text("select * from users  where iuser_id=" + IDF39 + "  ");
        if (dtf39.Count > 0)
        {
            return dtf39[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf40 = Susers.Name_Text("select * from users  where iuser_id=" + IDF40 + "  ");
        if (dtf40.Count > 0)
        {
            return dtf40[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf41 = Susers.Name_Text("select * from users  where iuser_id=" + IDF41 + "  ");
        if (dtf41.Count > 0)
        {
            return dtf41[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf42 = Susers.Name_Text("select * from users  where iuser_id=" + IDF42 + "  ");
        if (dtf42.Count > 0)
        {
            return dtf42[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf43 = Susers.Name_Text("select * from users  where iuser_id=" + IDF43 + "  ");
        if (dtf43.Count > 0)
        {
            return dtf43[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf44 = Susers.Name_Text("select * from users  where iuser_id=" + IDF44 + "  ");
        if (dtf44.Count > 0)
        {
            return dtf44[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF45(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30, string IDF31, string IDF32, string IDF33, string IDF34, string IDF35, string IDF36, string IDF37, string IDF38, string IDF39, string IDF40, string IDF41, string IDF42, string IDF43, string IDF44, string IDF45)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf31 = Susers.Name_Text("select * from users  where iuser_id=" + IDF31 + "  ");
        if (dtf31.Count > 0)
        {
            return dtf31[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf32 = Susers.Name_Text("select * from users  where iuser_id=" + IDF32 + "  ");
        if (dtf32.Count > 0)
        {
            return dtf32[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf33 = Susers.Name_Text("select * from users  where iuser_id=" + IDF33 + "  ");
        if (dtf33.Count > 0)
        {
            return dtf33[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf34 = Susers.Name_Text("select * from users  where iuser_id=" + IDF34 + "  ");
        if (dtf34.Count > 0)
        {
            return dtf34[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf35 = Susers.Name_Text("select * from users  where iuser_id=" + IDF35 + "  ");
        if (dtf35.Count > 0)
        {
            return dtf35[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf36 = Susers.Name_Text("select * from users  where iuser_id=" + IDF36 + "  ");
        if (dtf36.Count > 0)
        {
            return dtf36[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf37 = Susers.Name_Text("select * from users  where iuser_id=" + IDF37 + "  ");
        if (dtf37.Count > 0)
        {
            return dtf37[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf38 = Susers.Name_Text("select * from users  where iuser_id=" + IDF38 + "  ");
        if (dtf38.Count > 0)
        {
            return dtf38[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf39 = Susers.Name_Text("select * from users  where iuser_id=" + IDF39 + "  ");
        if (dtf39.Count > 0)
        {
            return dtf39[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf40 = Susers.Name_Text("select * from users  where iuser_id=" + IDF40 + "  ");
        if (dtf40.Count > 0)
        {
            return dtf40[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf41 = Susers.Name_Text("select * from users  where iuser_id=" + IDF41 + "  ");
        if (dtf41.Count > 0)
        {
            return dtf41[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf42 = Susers.Name_Text("select * from users  where iuser_id=" + IDF42 + "  ");
        if (dtf42.Count > 0)
        {
            return dtf42[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf43 = Susers.Name_Text("select * from users  where iuser_id=" + IDF43 + "  ");
        if (dtf43.Count > 0)
        {
            return dtf43[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf44 = Susers.Name_Text("select * from users  where iuser_id=" + IDF44 + "  ");
        if (dtf44.Count > 0)
        {
            return dtf44[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf45 = Susers.Name_Text("select * from users  where iuser_id=" + IDF45 + "  ");
        if (dtf45.Count > 0)
        {
            return dtf45[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF46(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30, string IDF31, string IDF32, string IDF33, string IDF34, string IDF35, string IDF36, string IDF37, string IDF38, string IDF39, string IDF40, string IDF41, string IDF42, string IDF43, string IDF44, string IDF45, string IDF46)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf31 = Susers.Name_Text("select * from users  where iuser_id=" + IDF31 + "  ");
        if (dtf31.Count > 0)
        {
            return dtf31[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf32 = Susers.Name_Text("select * from users  where iuser_id=" + IDF32 + "  ");
        if (dtf32.Count > 0)
        {
            return dtf32[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf33 = Susers.Name_Text("select * from users  where iuser_id=" + IDF33 + "  ");
        if (dtf33.Count > 0)
        {
            return dtf33[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf34 = Susers.Name_Text("select * from users  where iuser_id=" + IDF34 + "  ");
        if (dtf34.Count > 0)
        {
            return dtf34[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf35 = Susers.Name_Text("select * from users  where iuser_id=" + IDF35 + "  ");
        if (dtf35.Count > 0)
        {
            return dtf35[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf36 = Susers.Name_Text("select * from users  where iuser_id=" + IDF36 + "  ");
        if (dtf36.Count > 0)
        {
            return dtf36[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf37 = Susers.Name_Text("select * from users  where iuser_id=" + IDF37 + "  ");
        if (dtf37.Count > 0)
        {
            return dtf37[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf38 = Susers.Name_Text("select * from users  where iuser_id=" + IDF38 + "  ");
        if (dtf38.Count > 0)
        {
            return dtf38[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf39 = Susers.Name_Text("select * from users  where iuser_id=" + IDF39 + "  ");
        if (dtf39.Count > 0)
        {
            return dtf39[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf40 = Susers.Name_Text("select * from users  where iuser_id=" + IDF40 + "  ");
        if (dtf40.Count > 0)
        {
            return dtf40[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf41 = Susers.Name_Text("select * from users  where iuser_id=" + IDF41 + "  ");
        if (dtf41.Count > 0)
        {
            return dtf41[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf42 = Susers.Name_Text("select * from users  where iuser_id=" + IDF42 + "  ");
        if (dtf42.Count > 0)
        {
            return dtf42[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf43 = Susers.Name_Text("select * from users  where iuser_id=" + IDF43 + "  ");
        if (dtf43.Count > 0)
        {
            return dtf43[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf44 = Susers.Name_Text("select * from users  where iuser_id=" + IDF44 + "  ");
        if (dtf44.Count > 0)
        {
            return dtf44[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf45 = Susers.Name_Text("select * from users  where iuser_id=" + IDF45 + "  ");
        if (dtf45.Count > 0)
        {
            return dtf45[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf46 = Susers.Name_Text("select * from users  where iuser_id=" + IDF46 + "  ");
        if (dtf46.Count > 0)
        {
            return dtf46[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF47(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30, string IDF31, string IDF32, string IDF33, string IDF34, string IDF35, string IDF36, string IDF37, string IDF38, string IDF39, string IDF40, string IDF41, string IDF42, string IDF43, string IDF44, string IDF45, string IDF46, string IDF47)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf31 = Susers.Name_Text("select * from users  where iuser_id=" + IDF31 + "  ");
        if (dtf31.Count > 0)
        {
            return dtf31[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf32 = Susers.Name_Text("select * from users  where iuser_id=" + IDF32 + "  ");
        if (dtf32.Count > 0)
        {
            return dtf32[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf33 = Susers.Name_Text("select * from users  where iuser_id=" + IDF33 + "  ");
        if (dtf33.Count > 0)
        {
            return dtf33[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf34 = Susers.Name_Text("select * from users  where iuser_id=" + IDF34 + "  ");
        if (dtf34.Count > 0)
        {
            return dtf34[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf35 = Susers.Name_Text("select * from users  where iuser_id=" + IDF35 + "  ");
        if (dtf35.Count > 0)
        {
            return dtf35[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf36 = Susers.Name_Text("select * from users  where iuser_id=" + IDF36 + "  ");
        if (dtf36.Count > 0)
        {
            return dtf36[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf37 = Susers.Name_Text("select * from users  where iuser_id=" + IDF37 + "  ");
        if (dtf37.Count > 0)
        {
            return dtf37[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf38 = Susers.Name_Text("select * from users  where iuser_id=" + IDF38 + "  ");
        if (dtf38.Count > 0)
        {
            return dtf38[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf39 = Susers.Name_Text("select * from users  where iuser_id=" + IDF39 + "  ");
        if (dtf39.Count > 0)
        {
            return dtf39[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf40 = Susers.Name_Text("select * from users  where iuser_id=" + IDF40 + "  ");
        if (dtf40.Count > 0)
        {
            return dtf40[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf41 = Susers.Name_Text("select * from users  where iuser_id=" + IDF41 + "  ");
        if (dtf41.Count > 0)
        {
            return dtf41[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf42 = Susers.Name_Text("select * from users  where iuser_id=" + IDF42 + "  ");
        if (dtf42.Count > 0)
        {
            return dtf42[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf43 = Susers.Name_Text("select * from users  where iuser_id=" + IDF43 + "  ");
        if (dtf43.Count > 0)
        {
            return dtf43[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf44 = Susers.Name_Text("select * from users  where iuser_id=" + IDF44 + "  ");
        if (dtf44.Count > 0)
        {
            return dtf44[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf45 = Susers.Name_Text("select * from users  where iuser_id=" + IDF45 + "  ");
        if (dtf45.Count > 0)
        {
            return dtf45[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf46 = Susers.Name_Text("select * from users  where iuser_id=" + IDF46 + "  ");
        if (dtf46.Count > 0)
        {
            return dtf46[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf47 = Susers.Name_Text("select * from users  where iuser_id=" + IDF47 + "  ");
        if (dtf47.Count > 0)
        {
            return dtf47[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF48(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30, string IDF31, string IDF32, string IDF33, string IDF34, string IDF35, string IDF36, string IDF37, string IDF38, string IDF39, string IDF40, string IDF41, string IDF42, string IDF43, string IDF44, string IDF45, string IDF46, string IDF47, string IDF48)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf31 = Susers.Name_Text("select * from users  where iuser_id=" + IDF31 + "  ");
        if (dtf31.Count > 0)
        {
            return dtf31[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf32 = Susers.Name_Text("select * from users  where iuser_id=" + IDF32 + "  ");
        if (dtf32.Count > 0)
        {
            return dtf32[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf33 = Susers.Name_Text("select * from users  where iuser_id=" + IDF33 + "  ");
        if (dtf33.Count > 0)
        {
            return dtf33[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf34 = Susers.Name_Text("select * from users  where iuser_id=" + IDF34 + "  ");
        if (dtf34.Count > 0)
        {
            return dtf34[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf35 = Susers.Name_Text("select * from users  where iuser_id=" + IDF35 + "  ");
        if (dtf35.Count > 0)
        {
            return dtf35[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf36 = Susers.Name_Text("select * from users  where iuser_id=" + IDF36 + "  ");
        if (dtf36.Count > 0)
        {
            return dtf36[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf37 = Susers.Name_Text("select * from users  where iuser_id=" + IDF37 + "  ");
        if (dtf37.Count > 0)
        {
            return dtf37[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf38 = Susers.Name_Text("select * from users  where iuser_id=" + IDF38 + "  ");
        if (dtf38.Count > 0)
        {
            return dtf38[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf39 = Susers.Name_Text("select * from users  where iuser_id=" + IDF39 + "  ");
        if (dtf39.Count > 0)
        {
            return dtf39[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf40 = Susers.Name_Text("select * from users  where iuser_id=" + IDF40 + "  ");
        if (dtf40.Count > 0)
        {
            return dtf40[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf41 = Susers.Name_Text("select * from users  where iuser_id=" + IDF41 + "  ");
        if (dtf41.Count > 0)
        {
            return dtf41[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf42 = Susers.Name_Text("select * from users  where iuser_id=" + IDF42 + "  ");
        if (dtf42.Count > 0)
        {
            return dtf42[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf43 = Susers.Name_Text("select * from users  where iuser_id=" + IDF43 + "  ");
        if (dtf43.Count > 0)
        {
            return dtf43[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf44 = Susers.Name_Text("select * from users  where iuser_id=" + IDF44 + "  ");
        if (dtf44.Count > 0)
        {
            return dtf44[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf45 = Susers.Name_Text("select * from users  where iuser_id=" + IDF45 + "  ");
        if (dtf45.Count > 0)
        {
            return dtf45[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf46 = Susers.Name_Text("select * from users  where iuser_id=" + IDF46 + "  ");
        if (dtf46.Count > 0)
        {
            return dtf46[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf47 = Susers.Name_Text("select * from users  where iuser_id=" + IDF47 + "  ");
        if (dtf47.Count > 0)
        {
            return dtf47[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf48 = Susers.Name_Text("select * from users  where iuser_id=" + IDF48 + "  ");
        if (dtf48.Count > 0)
        {
            return dtf48[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF49(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30, string IDF31, string IDF32, string IDF33, string IDF34, string IDF35, string IDF36, string IDF37, string IDF38, string IDF39, string IDF40, string IDF41, string IDF42, string IDF43, string IDF44, string IDF45, string IDF46, string IDF47, string IDF48, string IDF49)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf31 = Susers.Name_Text("select * from users  where iuser_id=" + IDF31 + "  ");
        if (dtf31.Count > 0)
        {
            return dtf31[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf32 = Susers.Name_Text("select * from users  where iuser_id=" + IDF32 + "  ");
        if (dtf32.Count > 0)
        {
            return dtf32[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf33 = Susers.Name_Text("select * from users  where iuser_id=" + IDF33 + "  ");
        if (dtf33.Count > 0)
        {
            return dtf33[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf34 = Susers.Name_Text("select * from users  where iuser_id=" + IDF34 + "  ");
        if (dtf34.Count > 0)
        {
            return dtf34[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf35 = Susers.Name_Text("select * from users  where iuser_id=" + IDF35 + "  ");
        if (dtf35.Count > 0)
        {
            return dtf35[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf36 = Susers.Name_Text("select * from users  where iuser_id=" + IDF36 + "  ");
        if (dtf36.Count > 0)
        {
            return dtf36[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf37 = Susers.Name_Text("select * from users  where iuser_id=" + IDF37 + "  ");
        if (dtf37.Count > 0)
        {
            return dtf37[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf38 = Susers.Name_Text("select * from users  where iuser_id=" + IDF38 + "  ");
        if (dtf38.Count > 0)
        {
            return dtf38[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf39 = Susers.Name_Text("select * from users  where iuser_id=" + IDF39 + "  ");
        if (dtf39.Count > 0)
        {
            return dtf39[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf40 = Susers.Name_Text("select * from users  where iuser_id=" + IDF40 + "  ");
        if (dtf40.Count > 0)
        {
            return dtf40[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf41 = Susers.Name_Text("select * from users  where iuser_id=" + IDF41 + "  ");
        if (dtf41.Count > 0)
        {
            return dtf41[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf42 = Susers.Name_Text("select * from users  where iuser_id=" + IDF42 + "  ");
        if (dtf42.Count > 0)
        {
            return dtf42[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf43 = Susers.Name_Text("select * from users  where iuser_id=" + IDF43 + "  ");
        if (dtf43.Count > 0)
        {
            return dtf43[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf44 = Susers.Name_Text("select * from users  where iuser_id=" + IDF44 + "  ");
        if (dtf44.Count > 0)
        {
            return dtf44[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf45 = Susers.Name_Text("select * from users  where iuser_id=" + IDF45 + "  ");
        if (dtf45.Count > 0)
        {
            return dtf45[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf46 = Susers.Name_Text("select * from users  where iuser_id=" + IDF46 + "  ");
        if (dtf46.Count > 0)
        {
            return dtf46[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf47 = Susers.Name_Text("select * from users  where iuser_id=" + IDF47 + "  ");
        if (dtf47.Count > 0)
        {
            return dtf47[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf48 = Susers.Name_Text("select * from users  where iuser_id=" + IDF48 + "  ");
        if (dtf48.Count > 0)
        {
            return dtf48[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf49 = Susers.Name_Text("select * from users  where iuser_id=" + IDF49 + "  ");
        if (dtf49.Count > 0)
        {
            return dtf49[0].LevelThanhVien.ToString();
        }
        return "0";
    }
    public static string ShowF50(string IDF1, string IDF2, string IDF3, string IDF4, string IDF5, string IDF6, string IDF7, string IDF8, string IDF9, string IDF10, string IDF11, string IDF12, string IDF13, string IDF14, string IDF15, string IDF16, string IDF17, string IDF18, string IDF19, string IDF20, string IDF21, string IDF22, string IDF23, string IDF24, string IDF25, string IDF26, string IDF27, string IDF28, string IDF29, string IDF30, string IDF31, string IDF32, string IDF33, string IDF34, string IDF35, string IDF36, string IDF37, string IDF38, string IDF39, string IDF40, string IDF41, string IDF42, string IDF43, string IDF44, string IDF45, string IDF46, string IDF47, string IDF48, string IDF49, string IDF50)
    {
        List<Entity.users> dtf1 = Susers.Name_Text("select * from users  where iuser_id=" + IDF1 + "  ");
        if (dtf1.Count > 0)
        {
            return dtf1[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf2 = Susers.Name_Text("select * from users  where iuser_id=" + IDF2 + "  ");
        if (dtf2.Count > 0)
        {
            return dtf2[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf3 = Susers.Name_Text("select * from users  where iuser_id=" + IDF3 + "  ");
        if (dtf3.Count > 0)
        {
            return dtf3[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf5 = Susers.Name_Text("select * from users  where iuser_id=" + IDF5 + "  ");
        if (dtf5.Count > 0)
        {
            return dtf5[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf6 = Susers.Name_Text("select * from users  where iuser_id=" + IDF6 + "  ");
        if (dtf6.Count > 0)
        {
            return dtf6[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf7 = Susers.Name_Text("select * from users  where iuser_id=" + IDF7 + "  ");
        if (dtf7.Count > 0)
        {
            return dtf7[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf8 = Susers.Name_Text("select * from users  where iuser_id=" + IDF8 + "  ");
        if (dtf8.Count > 0)
        {
            return dtf8[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf9 = Susers.Name_Text("select * from users  where iuser_id=" + IDF9 + "  ");
        if (dtf9.Count > 0)
        {
            return dtf9[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf10 = Susers.Name_Text("select * from users  where iuser_id=" + IDF10 + "  ");
        if (dtf10.Count > 0)
        {
            return dtf10[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf11 = Susers.Name_Text("select * from users  where iuser_id=" + IDF11 + "  ");
        if (dtf11.Count > 0)
        {
            return dtf11[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf12 = Susers.Name_Text("select * from users  where iuser_id=" + IDF12 + "  ");
        if (dtf12.Count > 0)
        {
            return dtf12[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf13 = Susers.Name_Text("select * from users  where iuser_id=" + IDF13 + "  ");
        if (dtf13.Count > 0)
        {
            return dtf13[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf14 = Susers.Name_Text("select * from users  where iuser_id=" + IDF14 + "  ");
        if (dtf14.Count > 0)
        {
            return dtf14[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf15 = Susers.Name_Text("select * from users  where iuser_id=" + IDF15 + "  ");
        if (dtf15.Count > 0)
        {
            return dtf15[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf16 = Susers.Name_Text("select * from users  where iuser_id=" + IDF16 + "  ");
        if (dtf16.Count > 0)
        {
            return dtf16[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf17 = Susers.Name_Text("select * from users  where iuser_id=" + IDF17 + "  ");
        if (dtf17.Count > 0)
        {
            return dtf17[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf18 = Susers.Name_Text("select * from users  where iuser_id=" + IDF18 + "  ");
        if (dtf18.Count > 0)
        {
            return dtf18[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf19 = Susers.Name_Text("select * from users  where iuser_id=" + IDF19 + "  ");
        if (dtf19.Count > 0)
        {
            return dtf19[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf20 = Susers.Name_Text("select * from users  where iuser_id=" + IDF20 + "  ");
        if (dtf20.Count > 0)
        {
            return dtf20[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf21 = Susers.Name_Text("select * from users  where iuser_id=" + IDF21 + "  ");
        if (dtf21.Count > 0)
        {
            return dtf21[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf22 = Susers.Name_Text("select * from users  where iuser_id=" + IDF22 + "  ");
        if (dtf22.Count > 0)
        {
            return dtf22[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf23 = Susers.Name_Text("select * from users  where iuser_id=" + IDF23 + "  ");
        if (dtf23.Count > 0)
        {
            return dtf23[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf24 = Susers.Name_Text("select * from users  where iuser_id=" + IDF24 + "  ");
        if (dtf24.Count > 0)
        {
            return dtf24[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf25 = Susers.Name_Text("select * from users  where iuser_id=" + IDF25 + "  ");
        if (dtf25.Count > 0)
        {
            return dtf25[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf26 = Susers.Name_Text("select * from users  where iuser_id=" + IDF26 + "  ");
        if (dtf26.Count > 0)
        {
            return dtf26[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf27 = Susers.Name_Text("select * from users  where iuser_id=" + IDF27 + "  ");
        if (dtf27.Count > 0)
        {
            return dtf27[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf28 = Susers.Name_Text("select * from users  where iuser_id=" + IDF28 + "  ");
        if (dtf28.Count > 0)
        {
            return dtf28[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf29 = Susers.Name_Text("select * from users  where iuser_id=" + IDF29 + "  ");
        if (dtf29.Count > 0)
        {
            return dtf29[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf30 = Susers.Name_Text("select * from users  where iuser_id=" + IDF30 + "  ");
        if (dtf30.Count > 0)
        {
            return dtf30[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf31 = Susers.Name_Text("select * from users  where iuser_id=" + IDF31 + "  ");
        if (dtf31.Count > 0)
        {
            return dtf31[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf32 = Susers.Name_Text("select * from users  where iuser_id=" + IDF32 + "  ");
        if (dtf32.Count > 0)
        {
            return dtf32[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf33 = Susers.Name_Text("select * from users  where iuser_id=" + IDF33 + "  ");
        if (dtf33.Count > 0)
        {
            return dtf33[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf34 = Susers.Name_Text("select * from users  where iuser_id=" + IDF34 + "  ");
        if (dtf34.Count > 0)
        {
            return dtf34[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf35 = Susers.Name_Text("select * from users  where iuser_id=" + IDF35 + "  ");
        if (dtf35.Count > 0)
        {
            return dtf35[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf36 = Susers.Name_Text("select * from users  where iuser_id=" + IDF36 + "  ");
        if (dtf36.Count > 0)
        {
            return dtf36[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf37 = Susers.Name_Text("select * from users  where iuser_id=" + IDF37 + "  ");
        if (dtf37.Count > 0)
        {
            return dtf37[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf38 = Susers.Name_Text("select * from users  where iuser_id=" + IDF38 + "  ");
        if (dtf38.Count > 0)
        {
            return dtf38[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf39 = Susers.Name_Text("select * from users  where iuser_id=" + IDF39 + "  ");
        if (dtf39.Count > 0)
        {
            return dtf39[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf40 = Susers.Name_Text("select * from users  where iuser_id=" + IDF40 + "  ");
        if (dtf40.Count > 0)
        {
            return dtf40[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf41 = Susers.Name_Text("select * from users  where iuser_id=" + IDF41 + "  ");
        if (dtf41.Count > 0)
        {
            return dtf41[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf42 = Susers.Name_Text("select * from users  where iuser_id=" + IDF42 + "  ");
        if (dtf42.Count > 0)
        {
            return dtf42[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf43 = Susers.Name_Text("select * from users  where iuser_id=" + IDF43 + "  ");
        if (dtf43.Count > 0)
        {
            return dtf43[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf44 = Susers.Name_Text("select * from users  where iuser_id=" + IDF44 + "  ");
        if (dtf44.Count > 0)
        {
            return dtf44[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf45 = Susers.Name_Text("select * from users  where iuser_id=" + IDF45 + "  ");
        if (dtf45.Count > 0)
        {
            return dtf45[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf46 = Susers.Name_Text("select * from users  where iuser_id=" + IDF46 + "  ");
        if (dtf46.Count > 0)
        {
            return dtf46[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf47 = Susers.Name_Text("select * from users  where iuser_id=" + IDF47 + "  ");
        if (dtf47.Count > 0)
        {
            return dtf47[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf48 = Susers.Name_Text("select * from users  where iuser_id=" + IDF48 + "  ");
        if (dtf48.Count > 0)
        {
            return dtf48[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf49 = Susers.Name_Text("select * from users  where iuser_id=" + IDF49 + "  ");
        if (dtf49.Count > 0)
        {
            return dtf49[0].LevelThanhVien.ToString();
        }
        List<Entity.users> dtf50 = Susers.Name_Text("select * from users  where iuser_id=" + IDF50 + "  ");
        if (dtf50.Count > 0)
        {
            return dtf50[0].LevelThanhVien.ToString();
        }
        return "0";
    }
}

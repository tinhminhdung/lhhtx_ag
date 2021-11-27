using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework;
using Entity;

namespace Services
{
    public class SMuaDiemThanhVien
    {
        private static FMuaDiemThanhVien db = new FMuaDiemThanhVien();

        #region CATEGORY_PHANTRANG
        public static List<EMuaDiemThanhVien> CATEGORY_PHANTRANG2(string sql, int PageIndex, int Tongpage)
        {
            return db.CATEGORY_PHANTRANG2(sql, PageIndex, Tongpage);
        }
        public static List<EMuaDiemThanhVien> CATEGORY_PHANTRANG1(string sql)
        {
            return db.CATEGORY_PHANTRANG1(sql);
        }
        #endregion

        #region Name_Text
        public static List<EMuaDiemThanhVien> Name_Text(string Name_Text)
        {
            return db.Name_Text(Name_Text);
        }
        #endregion
    }
}

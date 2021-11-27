using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework;
using Entity;

namespace Services
{
    public class SLichSuLevel
    {
        private static FLichSuLevel db = new FLichSuLevel();

        #region CATEGORY_PHANTRANG
        public static List<ELichSuLevel> CATEGORY_PHANTRANG2(string sql, int PageIndex, int Tongpage)
        {
            return db.CATEGORY_PHANTRANG2(sql, PageIndex, Tongpage);
        }
        public static List<ELichSuLevel> CATEGORY_PHANTRANG1(string sql)
        {
            return db.CATEGORY_PHANTRANG1(sql);
        }
        public static List<ELichSuLevel> Exel(string sql)
        {
            return db.Exel(sql);
        }
        #endregion

        #region Name_Text
        public static List<ELichSuLevel> Name_Text(string Name_Text)
        {
            return db.Name_Text(Name_Text);
        }
        #endregion
    }
}

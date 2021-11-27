using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework;
using Entity;

namespace Services
{
    public class SLoiNhuanMuaBan_BatDongSan
    {
        private static FLoiNhuanMuaBan_BatDongSan db = new FLoiNhuanMuaBan_BatDongSan();

        #region CATEGORY_PHANTRANG
        public static List<ELoiNhuanMuaBan_BatDongSan> CATEGORY_PHANTRANG2(string sql, int PageIndex, int Tongpage)
        {
            return db.CATEGORY_PHANTRANG2(sql, PageIndex, Tongpage);
        }
        public static List<ELoiNhuanMuaBan_BatDongSan> CATEGORY_PHANTRANG1(string sql)
        {
            return db.CATEGORY_PHANTRANG1(sql);
        }
        #endregion

        #region Name_Text
        public static List<ELoiNhuanMuaBan_BatDongSan> Name_Text(string Name_Text)
        {
            return db.Name_Text(Name_Text);
        }
        #endregion
    }
}

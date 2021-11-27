﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework;
using Entity;

namespace Services
{
    public class SLaiSuatTheoAgLand
    {
        private static FLaiSuatTheoAgLand db = new FLaiSuatTheoAgLand();

        #region CATEGORY_PHANTRANG
        public static List<ELaiSuatTheoAgLand> CATEGORY_PHANTRANG2(string sql, int PageIndex, int Tongpage)
        {
            return db.CATEGORY_PHANTRANG2(sql, PageIndex, Tongpage);
        }
        public static List<ELaiSuatTheoAgLand> CATEGORY_PHANTRANG1(string sql)
        {
            return db.CATEGORY_PHANTRANG1(sql);
        }
        #endregion

        #region Name_Text
        public static List<ELaiSuatTheoAgLand> Name_Text(string Name_Text)
        {
            return db.Name_Text(Name_Text);
        }
        #endregion
    }
}

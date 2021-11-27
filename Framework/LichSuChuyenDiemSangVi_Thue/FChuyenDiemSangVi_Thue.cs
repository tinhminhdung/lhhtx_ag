﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using System.Data.SqlClient;
using System.Data;

namespace Framework
{
    public class FChuyenDiemSangVi_Thue
    {
        public List<EChuyenDiemSangVi_Thue> CATEGORY_PHANTRANG1(string Sql)
        {
            string sql = @"SELECT * FROM ChuyenDiemSangVi_Thue where 1=1  " + Sql + " order by NgayGiaoDich desc ";
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<EChuyenDiemSangVi_Thue>(comm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        public List<EChuyenDiemSangVi_Thue> CATEGORY_PHANTRANG2(string Sql, int PageIndex, int Tongpage)
        {
            int StartRecord = PageIndex * Tongpage;
            int EndRecord = StartRecord + Tongpage + 1;
            string sql = @"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY NgayGiaoDich DESC) AS rowindex ,* 
                                FROM  ChuyenDiemSangVi_Thue 
        		                where 1=1 " + Sql + " ";
            sql += ") AS A WHERE  ( A.rowindex >  " + StartRecord.ToString() + " AND A.rowindex < " + EndRecord + ")";
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<EChuyenDiemSangVi_Thue>(comm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }


        #region Name Text
        public List<EChuyenDiemSangVi_Thue> Name_Text(string Name_Text)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(Name_Text, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<EChuyenDiemSangVi_Thue>(comm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion
    }
}
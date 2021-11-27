using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using System.Data.SqlClient;
using System.Data;

namespace Framework
{
    public class FLichSuLevel
    {
        public List<ELichSuLevel> Exel(string Sql)
        {
            string sql = @"SELECT * FROM LichSuLevel where 1=1  " + Sql + "  order by CONVERT(datetime, NgayLenCap, 103) desc ";
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<ELichSuLevel>(comm);
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
        public List<ELichSuLevel> CATEGORY_PHANTRANG1(string Sql)
        {
            string sql = @"SELECT * FROM LichSuLevel where 1=1  " + Sql + " order by NgayLenCap desc ";
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<ELichSuLevel>(comm);
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
        public List<ELichSuLevel> CATEGORY_PHANTRANG2(string Sql, int PageIndex, int Tongpage)
        {
            int StartRecord = PageIndex * Tongpage;
            int EndRecord = StartRecord + Tongpage + 1;
            string sql = @"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY NgayLenCap DESC) AS rowindex ,* 
                                FROM  LichSuLevel 
        		                where 1=1 " + Sql + " ";
            sql += ") AS A WHERE  ( A.rowindex >  " + StartRecord.ToString() + " AND A.rowindex < " + EndRecord + ")";
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<ELichSuLevel>(comm);
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
        public List<ELichSuLevel> Name_Text(string Name_Text)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(Name_Text, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<ELichSuLevel>(comm);
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

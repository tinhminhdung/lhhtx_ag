﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using System.Data.SqlClient;
using System.Data;

namespace Framework
{
    public class FDownload
    {
        #region GET BY ID
        public List<Download> GETBYID(string Id)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_Download_GetById", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add(new SqlParameter("@ID", Id));
            try
            {
                return Database.Bind_List_Reader<Download>(comm);
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

        #region GET BY TOP
        public List<Download> GETBYTOP(string Top, string Where, string Order)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("sp_Download_GetByTop", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add(new SqlParameter("@Top", Top));
            comm.Parameters.Add(new SqlParameter("@Where", Where));
            comm.Parameters.Add(new SqlParameter("@Order", Order));
            try
            {
                return Database.Bind_List_Reader<Download>(comm);
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

        #region GET BY ALL
        public List<Download> GETBYALL(string Lang)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_Download_GetByAll", conn);
            comm.CommandType = CommandType.StoredProcedure;
            
            comm.Parameters.Add(new SqlParameter("@Lang", Lang));
            try
            {
                return Database.Bind_List_Reader<Download>(comm);
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

        #region PAGING
        public List<Download> PAGING(string CurentPage, string PageSize, string Lang)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("sp_Download_Paging", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add(new SqlParameter("@CurentPage", CurentPage));
            comm.Parameters.Add(new SqlParameter("@PageSize", PageSize));
            comm.Parameters.Add(new SqlParameter("@Lang", Lang));
            try
            {
                return Database.Bind_List_Reader<Download>(comm);
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

        #region INSERT
        public bool Insert(Download obj)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_Download_Insert", conn);
            comm.CommandType = CommandType.StoredProcedure;
            
            SqlTransaction objtran = conn.BeginTransaction();
            comm.Transaction = objtran;
            comm.Parameters.Add(new SqlParameter("@Title", obj.Title));
            comm.Parameters.Add(new SqlParameter("@Brief", obj.Brief));
            comm.Parameters.Add(new SqlParameter("@Contents", obj.Contents));
            comm.Parameters.Add(new SqlParameter("@Keywords", obj.Keywords));
            comm.Parameters.Add(new SqlParameter("@search", obj.search));
            comm.Parameters.Add(new SqlParameter("@Images", obj.Images));
            comm.Parameters.Add(new SqlParameter("@Equals", obj.Equals));
            comm.Parameters.Add(new SqlParameter("@Chekdata", obj.Chekdata));
            comm.Parameters.Add(new SqlParameter("@Create_Date", obj.Create_Date));
            comm.Parameters.Add(new SqlParameter("@Modified_Date", obj.Modified_Date));
            comm.Parameters.Add(new SqlParameter("@Views", obj.Views));
            comm.Parameters.Add(new SqlParameter("@lang", obj.lang));
            comm.Parameters.Add(new SqlParameter("@Status", obj.Status));
            comm.Parameters.Add(new SqlParameter("@TangName", obj.TangName));
            try
            {
                comm.ExecuteNonQuery();
                objtran.Commit();
                System.Web.HttpContext.Current.Cache.Remove("Download");
                return true;
            }
            catch
            {
                objtran.Rollback();
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region UPDATE
        public bool Update(Download obj)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_Download_Update", conn);
            comm.CommandType = CommandType.StoredProcedure;
            
            SqlTransaction objtran = conn.BeginTransaction();
            comm.Transaction = objtran;
            comm.Parameters.Add(new SqlParameter("@ID", obj.ID));
            comm.Parameters.Add(new SqlParameter("@Title", obj.Title));
            comm.Parameters.Add(new SqlParameter("@Brief", obj.Brief));
            comm.Parameters.Add(new SqlParameter("@Contents", obj.Contents));
            comm.Parameters.Add(new SqlParameter("@Keywords", obj.Keywords));
            comm.Parameters.Add(new SqlParameter("@search", obj.search));
            comm.Parameters.Add(new SqlParameter("@Images", obj.Images));
            comm.Parameters.Add(new SqlParameter("@Equals", obj.Equals));
            comm.Parameters.Add(new SqlParameter("@Chekdata", obj.Chekdata));
            comm.Parameters.Add(new SqlParameter("@Create_Date", obj.Create_Date));
            comm.Parameters.Add(new SqlParameter("@Modified_Date", obj.Modified_Date));
            comm.Parameters.Add(new SqlParameter("@lang", obj.lang));
            comm.Parameters.Add(new SqlParameter("@Status", obj.Status));
            comm.Parameters.Add(new SqlParameter("@TangName", obj.TangName));
            try
            {
                comm.ExecuteNonQuery();
                objtran.Commit();
                System.Web.HttpContext.Current.Cache.Remove("Download");
                return true;
            }
            catch
            {
                objtran.Rollback();
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region DELETE
        public void DELETE(string ID)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("S_Download_Delete", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add(new SqlParameter("@ID", ID));
            SqlTransaction objtran = conn.BeginTransaction();
            comm.Transaction = objtran;
            try
            {
                comm.ExecuteNonQuery();
                objtran.Commit();
                System.Web.HttpContext.Current.Cache.Remove("Download");
            }
            catch
            {
                objtran.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region CATEGORY ADMIN
        public List<Download> CATEGORY_ADMIN(string orderby, string searchkeyword, string[] searchfields, string lang, string Status)
        {
            int i;
            string shortbydate = "";
            if (orderby.Length < 1)
            {
                shortbydate = "order by Create_Date desc";
            }
            else
            {
                shortbydate = "order by " + orderby;
            }
            string sql = @"select * from Download where lang=@lang ";
            if (!Status.Equals("-1"))
            {
                sql += " and Status=@Status";
            }
            if ((searchkeyword.Length > 0) && (searchfields.Length > 0))
            {
                sql = sql + " and ";
                string strsearch = "(";
                for (i = 0; i < searchfields.Length; i++)
                {
                    if (i < (searchfields.Length - 1))
                    {
                        strsearch = strsearch + searchfields[i] + " like N'%" + searchkeyword + "%' or ";
                    }
                    else
                    {
                        strsearch = strsearch + searchfields[i] + " like N'%" + searchkeyword + "%'";
                    }
                }
                strsearch = strsearch + ")";
                sql = sql + strsearch;
            }
            sql += " " + shortbydate + "";
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            comm.Parameters.Add(new SqlParameter("@lang", lang));
            if (!Status.Equals("-1"))
            {
                comm.Parameters.Add(new SqlParameter("@Status", Status));
            }
            if ((searchkeyword.Length > 0) && (searchfields.Length > 0))
            {
                for (i = 0; i < searchfields.Length; i++)
                {
                    comm.Parameters.Add(new SqlParameter("@" + searchfields[i], searchkeyword));
                }
            }
            try
            {
                return Database.Bind_List_Reader<Download>(comm);
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

        #region GET CATEGORY BY ID
        public List<Download> GET_CATEGORY_BYID(string id)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("select * from Download where ID = @ID", conn);
            comm.CommandType = CommandType.Text;
            comm.Parameters.Add(new SqlParameter("@ID", id));
            try
            {
                return Database.Bind_List_Reader<Download>(comm);
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

        #region CATE DELETE MENU ID
        public void CATE_DELETE_MENUID(string Menu_ID, string imcid)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("delete from Download where Menu_ID = @Menu_ID  or Menu_ID in (" + imcid + ") ", conn);
            comm.CommandType = CommandType.Text;
            comm.Parameters.Add(new SqlParameter("@Menu_ID", Menu_ID));
            SqlTransaction objtran = conn.BeginTransaction();
            comm.Transaction = objtran;
            try
            {
                comm.ExecuteNonQuery();
                objtran.Commit();
                System.Web.HttpContext.Current.Cache.Remove("Download");
            }
            catch
            {
                objtran.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region GET DETAIL BY MENU ID
        public List<Download> GETDETAIL_BYMENUID(string ID_Menu, string imcid)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("select * from Download where Menu_ID = @ID_Menu  or Menu_ID in (" + imcid + ") ", conn);
            comm.CommandType = CommandType.StoredProcedure;
            
            comm.Parameters.Add(new SqlParameter("@ID_Menu", ID_Menu));
            try
            {
                return Database.Bind_List_Reader<Download>(comm);
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

        #region UPDATE VIEWS
        public bool UPDATEVIEWS(string ID, int iviews)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("update Download set iviews = iviews + @iviews where ID = @ID", conn);
            comm.CommandType = CommandType.StoredProcedure;
            
            SqlTransaction objtran = conn.BeginTransaction();
            comm.Transaction = objtran;
            comm.Parameters.Add(new SqlParameter("@ID", ID));
            comm.Parameters.Add(new SqlParameter("@iviews", iviews));
            try
            {
                comm.ExecuteNonQuery();
                objtran.Commit();
                System.Web.HttpContext.Current.Cache.Remove("Download");
                return true;
            }
            catch
            {
                objtran.Rollback();
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region UPDATE VIEWS TIME
        public bool UPDATE_VIEWSTIME(string ID)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("update Download set Views=Views + 1 where ID = @ID", conn);
            comm.CommandType = CommandType.Text;
            
            SqlTransaction objtran = conn.BeginTransaction();
            comm.Transaction = objtran;
            comm.Parameters.Add(new SqlParameter("@ID", ID));
            try
            {
                comm.ExecuteNonQuery();
                objtran.Commit();
                System.Web.HttpContext.Current.Cache.Remove("Download");
                return true;
            }
            catch
            {
                objtran.Rollback();
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region UPDATE STATUS
        public bool UPDATE_STATUS(string ID, string status)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("UPDATE [Download] SET [status] = @Status WHERE ID =@ID", conn);
            comm.CommandType = CommandType.Text;
            
            SqlTransaction objtran = conn.BeginTransaction();
            comm.Transaction = objtran;
            comm.Parameters.Add(new SqlParameter("@status", status));
            comm.Parameters.Add(new SqlParameter("@ID", ID));
            try
            {
                comm.ExecuteNonQuery();
                objtran.Commit();
                System.Web.HttpContext.Current.Cache.Remove("Download");
                return true;
            }
            catch
            {
                objtran.Rollback();
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        # region UPDATE TIMES
        public bool UPDATE_DATETIME(string ID, DateTime Create_Date, DateTime Modified_Date)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("update Download set Create_Date=@Create_Date,Modified_Date=@Modified_Date where ID= @ID", conn);
            comm.CommandType = CommandType.Text;
            
            SqlTransaction objtran = conn.BeginTransaction();
            comm.Transaction = objtran;
            comm.Parameters.Add(new SqlParameter("@Create_Date", Create_Date));
            comm.Parameters.Add(new SqlParameter("@Modified_Date", Modified_Date));
            comm.Parameters.Add(new SqlParameter("@ID", ID));
            try
            {
                comm.ExecuteNonQuery();
                objtran.Commit();
                System.Web.HttpContext.Current.Cache.Remove("Download");
                return true;
            }
            catch
            {
                objtran.Rollback();
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        # region CHECK DATA
        public bool CHECKDATA(string ID, string Chekdata, DateTime Create_Date, DateTime Modified_Date)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("update Download set Chekdata=@Chekdata,Create_Date=@Create_Date,Modified_Date=@Modified_Date where ID= @ID", conn);
            comm.CommandType = CommandType.Text;
            
            SqlTransaction objtran = conn.BeginTransaction();
            comm.Transaction = objtran;
            comm.Parameters.Add(new SqlParameter("@Chekdata", Chekdata));
            comm.Parameters.Add(new SqlParameter("@Create_Date", Create_Date));
            comm.Parameters.Add(new SqlParameter("@Modified_Date", Modified_Date));
            comm.Parameters.Add(new SqlParameter("@ID", ID));
            try
            {
                comm.ExecuteNonQuery();
                objtran.Commit();
                System.Web.HttpContext.Current.Cache.Remove("Download");
                return true;
            }
            catch
            {
                objtran.Rollback();
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region UPDATE IMG
        public bool UPDATEIMG(string id)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("update Download set Images='' where ID = @ID", conn);
            comm.CommandType = CommandType.Text;
            
            SqlTransaction objtran = conn.BeginTransaction();
            comm.Transaction = objtran;
            comm.Parameters.Add(new SqlParameter("@ID", id));
            try
            {
                comm.ExecuteNonQuery();
                objtran.Commit();
                System.Web.HttpContext.Current.Cache.Remove("Download");
                return true;
            }
            catch
            {
                objtran.Rollback();
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region CATEGORY
        public List<Download> CATEGORY(string lang)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand("select * from Download where  Status= 1 and lang=@lang and (Create_Date<=getdate() and getdate()<=Modified_Date) order by Create_Date asc", conn);
            comm.CommandType = CommandType.Text;
            comm.Parameters.Add(new SqlParameter("@lang", lang));
            try
            {
                return Database.Bind_List_Reader<Download>(comm);
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

        #region Name StoredProcedure
        public List<Download> Name_StoredProcedure(string Name_StoredProcedure)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(Name_StoredProcedure, conn);
            comm.CommandType = CommandType.StoredProcedure;
            try
            {
                return Database.Bind_List_Reader<Download>(comm);
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

        #region Name Text
        public List<Download> Name_Text(string Name_Text)
        {
            SqlConnection conn = Database.Connection();
            SqlCommand comm = new SqlCommand(Name_Text, conn);
            comm.CommandType = CommandType.Text;
            try
            {
                return Database.Bind_List_Reader<Download>(comm);
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

using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce
{
    public partial class LoadScollAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptCustomers.DataSource = GetCustomersData(1);
                rptCustomers.DataBind();
            }
        }

        public static DataSet GetCustomersData(int pageIndex)
        {
            string query = "[GetCustomersPageWise]";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@PageSize", 10);
            cmd.Parameters.Add("@PageCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
            return GetData(cmd);
        }

     


        [WebMethod]
        public static string Showsanpham(int top)
        {
            string str = "";
            List<Entity.Products> iitem = SProducts.Name_Text("select top " + top.ToString() + " * from  products where Status=1  order by Create_Date desc ");
            if (iitem.Count() > 0)
            {
                foreach (var item in iitem)
                {
                    str += "Tên:" + item.Name + " <br />";
                }
            }
            return str;
        }


        private static DataSet GetData(SqlCommand cmd)
        {
            string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds, "products");
                        DataTable dt = new DataTable("PageCount");
                        dt.Columns.Add("PageCount");
                        dt.Rows.Add();
                        dt.Rows[0][0] = cmd.Parameters["@PageCount"].Value;
                        ds.Tables.Add(dt);
                        return ds;
                    }
                }
            }
        }

        [WebMethod]
        public static string GetCustomers(int pageIndex)
        {
            return GetCustomersData(pageIndex).GetXml();
        }
    }
}
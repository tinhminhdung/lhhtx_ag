using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce
{
    public partial class CapCha : System.Web.UI.Page
    {
        private static Random random = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetCapCha();
            }
        }

        public void SetCapCha()
        {
            string hash = RandomString(6);
            ltshowcapcha.Text = hash;
            Session["RandomCapCha"] = hash;
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdsfghjklqwertyuiopzxcvbnm0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Nhap = txtinputcapcha.Text.Trim();
            if (Session["RandomCapCha"].ToString() != Nhap)
            {
                Response.Write("Không đúng");
            }
            else
            {
                Response.Write("OK");
            }
            SetCapCha();

        }
    }
}
using MoreAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce
{
    public partial class CheckMatKhau : System.Web.UI.Page
    {
        string matkhau = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["matkhau"] != null && !Request["matkhau"].Equals(""))
            {
                matkhau = Request["matkhau"];
            }
            if (!Password(matkhau.Trim()) == true)
            {
                Response.Write("Mật khẩu bao gồm : 8 ký tự bao gồm 1 chữ cái viết hoa, 1 ký tự đặc biệt, ký tự chữ và số");
            }
            else
            {
                Response.Write("OK");
            }
            //if (RegularExpressions.phone("0976658433") == true)
            //{
            //    Response.Write("SAI 0976658433");
            //}
            //else
            //{
            //    Response.Write("OK");
            //}

            //^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*\[\]"\';:_\-<>\., =\+\/\\]).{8,}$

            Match password = Regex.Match(matkhau, @"^(?=(.*[a-z]){1,})(?=(.*[A-Z]){1,})(?=(.*[0-9]){1,})(?=(.*[!@#$%^&*()\-__+.]){1,}).{8,}$", RegexOptions.IgnorePatternWhitespace);
            if (password.Success)
            {
                Response.Write(": OK");
            }
            else
            {
                Response.Write(": invalid");
            }
        }

        public static bool Password(string pass)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(pass, @"^(?=(.*[a-z]){1,})(?=(.*[A-Z]){1,})(?=(.*[0-9]){1,})(?=(.*[!@#$%^&*()\-__+.]){1,}).{8,}$");
        }

    }
}
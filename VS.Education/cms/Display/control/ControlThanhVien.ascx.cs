using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VS.E_Commerce.cms.Display.control
{
    public partial class ControlThanhVien : System.Web.UI.UserControl
    {
        #region string
        #endregion
        string hp = "";
        int iEmptyIndex = 0;
        public string Moldul = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            DatalinqDataContext db = new DatalinqDataContext();
            #region Requesthp
            if (Request["hp"] != null && !Request["hp"].Equals(""))
            {
                hp = Request["hp"].ToString();
            }
            iEmptyIndex = hp.IndexOf("?");
            if (iEmptyIndex != -1)
            {
                hp = hp.Substring(0, iEmptyIndex);
            }

            #endregion

            #region Request_e
            //try
            // {
            if (Request["e"] != null)
            {
                if (Request["e"].ToString() == "load")
                {
                    string request = Request["hp"] != null ? Request["hp"].ToString() : Request.Path;
                    string t = Request["hp"].ToString() + ".html";
                    if (!request.ToLower().Contains("index.aspx"))
                    {
                        Moldul = Commond.RequestMenu(Request["hp"]);
                        switch (Moldul)
                        {
                            case "99":// trang page
                                phcontrol.Controls.Add(LoadControl("~/cms/Display/Page/Detail.ascx"));
                                break;
                            //News
                            case "1":
                                phcontrol.Controls.Add(LoadControl("~/cms/Display/News/Category.ascx"));
                                break;
                            case "2":
                                phcontrol.Controls.Add(LoadControl("~/cms/Display/News/Detail.ascx"));
                                break;
                        }
                    }
                }

            }
            //}
            //catch (Exception)
            //{
            //    Response.Redirect("/page-404.html");
            //}
            #endregion
            #region Request_su
            switch (Request["su"])
            {
                case "Register":
                    phcontrol.Controls.Add(LoadControl("~/cms/Display/Members/Register.ascx"));
                    break;
                case "quanlysanpham":
                    phcontrol.Controls.Add(LoadControl("~/cms/Display/QuanLyDangBai/ThanhVienDangBai.ascx"));
                    break;
                case "Infos":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        phcontrol.Controls.Add(LoadControl("~/cms/Display/Members/Info.ascx"));
                    }
                    else
                    {
                        Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    break;
                case "changepass":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        this.phcontrol.Controls.Add(LoadControl("~/cms/Display/Members/Changepassword.ascx"));
                    }
                    else
                    {
                       Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    return;
                case "changinfo":
                    if (MoreAll.MoreAll.GetCookies("Members") != "")
                    {
                        this.phcontrol.Controls.Add(LoadControl("~/cms/Display/Members/Changinfo.ascx"));
                    }
                    else
                    {
                       Response.Redirect("/dang-nhap.html?ReturnUrl=" + Request.RawUrl.ToString() + "");
                    }
                    return;
                case "resetpassword":
                    phcontrol.Controls.Add(LoadControl("~/cms/Display/Members/Resetpassword.ascx"));
                    break;
                case "Login":
                    this.phcontrol.Controls.Add(LoadControl("~/cms/Display/Members/Login.ascx"));
                    break;


            }
            #endregion

        }
    }
}
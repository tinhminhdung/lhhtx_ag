using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using System.IO;

namespace VS.E_Commerce.cms.Display.Downloads
{
    public partial class Defaul : System.Web.UI.Page
    {
        string ID = "-1";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["ID"] != null && !Request["ID"].Equals(""))
                {
                    ID = Request["ID"];
                }
                List<Entity.Download> dt = SDownload.GET_BY_ID(ID);
                if (dt.Count > 0)
                {
                    SDownload.UPDAE_VIEW_TIMES(ID);
                    if (!dt[0].Images.ToString().Equals(""))
                    {
                        base.Response.Redirect(Images(dt[0].Images.ToString(), dt[0].Equals.ToString()));
                    }
                    else
                    {
                        string path = AppDomain.CurrentDomain.BaseDirectory + Images(dt[0].Images.ToString(), dt[0].Equals.ToString());
                        if (File.Exists(path))
                        {
                            FileStream stream = new FileStream(path, FileMode.Open);
                            FileInfo info = new FileInfo(path);
                            string extension = info.Extension;
                            string str3 = dt[0].Images.ToString();
                            byte[] buffer = new byte[(int)stream.Length];
                            stream.Read(buffer, 0, (int)stream.Length);
                            stream.Close();
                            base.Response.ContentType = "application/" + extension;
                            base.Response.AddHeader("content-disposition", "attachment; filename=" + str3);
                            base.Response.BinaryWrite(buffer);
                        }
                    }
                    base.Response.Redirect("/");
                }
                else
                {
                    base.Response.Redirect("/");
                }
            }
            catch (Exception)
            { }
        }

        protected string Images(string image, string vkey)
        {
            if (image.Length > 0)
            {
                if (vkey.Equals("0"))
                {
                    return image;
                }
                else if (vkey.Equals("1"))
                {
                    return image;
                }
            }
            return "";
        }
    }
}
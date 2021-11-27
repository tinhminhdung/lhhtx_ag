using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MoreAll;
using Framework;
using System.Web.UI.HtmlControls;
using Services;
using MoreAll.Templates;
using System.Net;
using System.Data;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Entity;
using AjaxPro;
using System.Web.Services;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.ComponentModel;
using System.Xml;


namespace VS.E_Commerce
{
    public partial class index1 : System.Web.UI.Page
    {
        public static int counter;
        public string hp = "";
        public string Modul = "";
        int iEmptyIndex = 0;
        private string language = Captionlanguage.Language;
        DatalinqDataContext db = new DatalinqDataContext();
        string ssl = "http://";
        string Rating = "";
        string link = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["language"] != null)
            {
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["language"] = this.language;
                this.language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            #region Sét link giới thiệu
            if (Request["link"] != null && !Request["link"].Equals(""))
            {
                link = Request["link"].ToString();//lấy ID qua Request
                if (Request.RawUrl.Contains("?link="))
                {
                    Response.Redirect("/dang-ky.html?info=" + link + "");
                }
            }
            #endregion

            if (!base.IsPostBack)
            {


                if (Commond.Setting("SSL").Equals("1"))
                {
                    ssl = "https://";
                }

                #region Page_404
                if (Commond.Setting("ErrPage").Equals("1"))
                {
                    try
                    {
                        if (Response.StatusCode == 404)
                        {
                            Response.Redirect("/page-404.html");
                        }
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        string bc = Request.Url.Authority + Request.RawUrl.ToString();
                        if (bc.Contains("?404"))
                        {
                            Response.Redirect("/page-404.html");
                        }
                        //else if (bc.Contains("/san-pham.aspx"))
                        //{
                        //    Link301();
                        //}
                    }
                    catch (Exception)
                    { }
                }
                #endregion

                #region Request["hp"] && Page_404

                #region Request["hp"]
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

                #region ErrPage and Redirect Status 301
                if (Commond.Setting("ErrPage").Equals("1"))
                {
                    try
                    {
                        if (Request["e"] != null)
                        {
                            if (Request["e"].ToString() == "load")
                            {
                                Modul = Commond.RequestMenu(Request["hp"]);
                                if (Modul == "")
                                {
                                    Link301();
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        if (Modul == "")
                        {
                            Link301();
                        }
                    }
                }
                else
                {
                    if (Request["e"] != null)
                    {
                        if (Request["e"].ToString() == "load")
                        {
                            Modul = Commond.RequestMenu(Request["hp"]);
                        }
                    }
                }
                #endregion

                #endregion

                #region MetaFacebook
                ltFacebook.Text += App.Template.Facebook(ssl + Request.Url.Host, hp, Modul);
                ltFacebook.Text += "<link rel=\"alternate\" href=\"" + ssl + Request.Url.Host + "\" hreflang=\"vi-vn\" />";
                ltFacebook.Text += "<meta property=\"article:author\" content=\"" + Commond.Setting("Facebook") + "\" />";
                if (Request["su"] == null && Modul == "")
                {
                    ltFacebook.Text += "<link  rel=\"canonical\" href=\"" + ssl + Request.Url.Host + "\" />";
                }
                else
                {
                    ltFacebook.Text += "<link  rel=\"canonical\" href=\"https://" + Request.Url.Host + Request.RawUrl + "\" />";
                }
                if (Commond.Setting("Icon").Length > 0)
                {
                    ltFacebook.Text += "<link rel='icon' href='" + Commond.Setting("Icon") + "' type='image/x-icon' /><link rel='shortcut icon' href='" + Commond.Setting("Icon") + "' type='image/x-icon' />";
                }
                if (Commond.Setting("txtfbapp_id") != "")
                {
                    string fbapp = Commond.Setting("txtfbapp_id");
                    // ltFacebook.Text += "<div id=\"fb-root\"></div><script> window.fbAsyncInit = function() { FB.init({ appId : '" + fbapp + "', autoLogAppEvents : true, xfbml : true, version : 'v2.11' }); }; (function(d, s, id){ var js, fjs = d.getElementsByTagName(s)[0]; if (d.getElementById(id)) {return;} js = d.createElement(s); js.id = id; js.src = \"https://connect.facebook.net/en_US/sdk/xfbml.customerchat.js\"; fjs.parentNode.insertBefore(js, fjs); }(document, 'script', 'facebook-jssdk')); </script>";
                }
                else
                {
                    //ltFacebook.Text += "<div id=\"fb-root\"></div> <script async defer crossorigin=\"anonymous\" src=\"https://connect.facebook.net/vi_VN/sdk.js#xfbml=1&version=v3.3\"></script>";
                }
                if (Commond.Setting("GoogleAnalytics") != "")
                {
                    ltFacebook.Text += "<script> (function (i, s, o, g, r, a, m) { i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () { (i[r].q = i[r].q || []).push(arguments) }, i[r].l = 1 * new Date(); a = s.createElement(o), m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m) })(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga'); ga('create', '" + Commond.Setting("GoogleAnalytics") + "', 'auto'); ga('send', 'pageview'); </script>";
                }
                if (Commond.Setting("head") != "")
                {
                    ltFacebook.Text += Commond.Setting("head");
                }
                if (Commond.Setting("body") != "")
                {
                    ltShowbody.Text = Commond.Setting("body");
                }
                #endregion

                #region ReviewRating
                Rating += "<script type=\"application/ld+json\">";
                Rating += "    {";
                Rating += "        \"@context\": \"https://schema.org/\",";
                Rating += "        \"@type\": \"Review\",";
                Rating += "        \"itemReviewed\": {";
                Rating += "            \"@type\": \"Thing\",";
                Rating += "            \"name\": \"" + Commond.Setting("webname") + "\"";
                Rating += "        },";
                Rating += "        \"author\": {";
                Rating += "            \"@type\": \"Person\",";
                Rating += "            \"name\": \"79850 người\"";
                Rating += "        },";
                Rating += "        \"reviewRating\": {";
                Rating += "            \"@type\": \"Rating\",";
                Rating += "            \"ratingValue\": \"56970\",";
                Rating += "            \"bestRating\": \"79850\"";
                Rating += "        },";
                Rating += "        \"publisher\": {";
                Rating += "            \"@type\": \"Organization\",";
                Rating += "            \"name\": \"" + Commond.Setting("webname") + "\"";
                Rating += "        }";
                Rating += "    }";
                Rating += "</script>";
                ltFacebook.Text += Rating;
                #endregion

                //#region Sitemap
                //try
                //{
                //    if (Commond.Setting("Sitem").Equals("1"))
                //    {
                //        string bc = Request.Url.Authority + Request.RawUrl.ToString();
                //        if (!bc.Contains("localhost"))
                //        {
                //            if (Session["Sitemap"] != "Sitemap")
                //            {
                //                ShowSitemap();
                //                System.Web.HttpContext.Current.Session["Sitemap"] = "Sitemap";
                //            }
                //        }
                //    }
                //}
                //catch (Exception)
                //{ }
                //#endregion

                #region PopUp
                try
                {
                    List<Entity.Advertisings> dt = SAdvertisings.VALUES(MoreAll.MoreAll.Language, "9", "1");
                    if (dt.Count > 0)
                    {
                        string bc = Request.Url.Authority + Request.RawUrl.ToString();
                        if (!bc.Contains("localhost"))
                        {
                            if (System.Web.HttpContext.Current.Session["PopUp"] != "PopUp")
                            {
                                ltpopup.Text = "<script type=\"text/javascript\"> window.onload = function () { loadPopup(); } </script>";
                                ltpopup.Text += "<div id=\"popupContact\">";
                                ltpopup.Text += "<a id=\"popupContactClose\"><img src=\"/Resources/images/b_close.png\" /></a>";
                                ltpopup.Text += "<div id=\"contactArea\" class=\"popupanh\">";
                                ltpopup.Text += ("<a  target=\"_blank\" href='" + dt[0].Path + "'><img   alt=\"" + dt[0].Name.Replace("\"", "&rdquo;") + "\"  title=\"" + dt[0].Name.Replace("\"", "&rdquo;") + "\" src='" + dt[0].vimg + "' border=0   style='" + MoreAll.MoreAll.Style_Width(dt[0].Width.ToString()) + ";" + MoreAll.MoreAll.Style_Height(dt[0].Height.ToString()) + "'    /></a>");
                                ltpopup.Text += " </div>";
                                ltpopup.Text += "</div>";
                                System.Web.HttpContext.Current.Session["PopUp"] = "PopUp";
                            }
                        }
                    }

                    List<Entity.Advertisings> dt1 = SAdvertisings.VALUES(MoreAll.MoreAll.Language, "10", "1");
                    if (dt1.Count > 0)
                    {
                        string bc = Request.Url.Authority + Request.RawUrl.ToString();
                        if (!bc.Contains("localhost"))
                        {
                            if (System.Web.HttpContext.Current.Session["PopUp"] != "PopUp")
                            {
                                ltpopup.Text = "<script type=\"text/javascript\"> window.onload = function () { loadPopup(); } </script>";
                                ltpopup.Text += "<div id=\"popupContact\">";
                                ltpopup.Text += "<a id=\"popupContactClose\"><img src=\"/Resources/images/b_close.png\" /></a>";
                                ltpopup.Text += "<div id=\"contactArea\" class=\"popupnd\">";
                                ltpopup.Text += dt1[0].Contents;
                                ltpopup.Text += " </div>";
                                ltpopup.Text += "</div>";
                                System.Web.HttpContext.Current.Session["PopUp"] = "PopUp";
                            }
                        }
                    }

                }
                catch (Exception)
                { }
                #endregion


                #region On Off Website
                if (OnOffs.StatusOnOff().Equals("1"))
                {
                    Response.Redirect("/cms/display/OnOff/index.aspx");
                }
                #endregion

                #region HtmlMeta
                try
                {
                    HtmlMeta child = new HtmlMeta();
                    child.Name = "keywords";
                    child.Content = MoreAll.MoreAll.RemoveHTMLTags2(App.Template.Keyword(hp, Modul));
                    base.Header.Controls.Add(child);
                    HtmlMeta meta2 = new HtmlMeta();
                    meta2.Name = "description";
                    meta2.Content = MoreAll.MoreAll.RemoveHTMLTags2(App.Template.Description(hp, Modul));
                    base.Header.Controls.Add(meta2);
                }
                catch (Exception)
                { }
                #endregion


                #region Viphamcontent
                if (!base.IsPostBack)
                {
                    try
                    {
                        website();
                    }
                    catch (Exception)
                    { }
                }
                #endregion
            }
        }

        public void website()
        {
            string bc = Request.Url.Authority + Request.RawUrl.ToString();
            if (!bc.Contains("localhost"))
            {
                if (Request["su"] == null)
                {
                    string Chuoi = "";//abc.com','abc.org.vn
                    Chuoi = MoreAll.Other.website().ToString();
                    List<Entity.Setting> dt = SSetting.Name_Text("select * from Setting where Properties='website' and Lang='" + language + "' and '" + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "' in('" + Chuoi + "')");
                    if (dt.Count == 0)
                    {
                        //if (MoreAll.Other.Giatri("Email").Equals("1"))
                        //{ }
                        Senmail();
                    }
                    if (MoreAll.Other.Giatri("Thongbao").Equals("1"))
                    {
                        if (MoreAll.Other.Giatri("Show").Length > 5)
                        {
                            Response.Write("<div style='display : block ! important;visibility : visible ! important;margin : 0 auto 10px ;position : relative ! important;z-index : 2147483647 ! important;width : 1000px;color:#fff; Background:#ffcb08; width:100%; padding:20px; border-radius:5px;'>" + MoreAll.Other.Giatri("Show") + "</div>");
                        }
                    }
                    else if (MoreAll.Other.Giatri("Thongbao").Equals("2"))
                    {
                        Response.Write("<div style='display : block ! important;visibility : visible ! important;margin : 0 auto 10px ;position : relative ! important;z-index : 2147483647 ! important;width : 1000px;color:#fff; Background:#ffcb08; width:100%; padding:20px; border-radius:5px;'>Vi phạm bản quyền nhân website. mà chưa được sự đồng ý của chủ web. Alo cho Lập trình để biết thêm thông tin chi tiết tại sao nhé?. 097.665.8433</div>");
                    }

                    if (MoreAll.Other.Giatri("Redirectwebsite") != "")
                    {
                        Response.Redirect(ssl + MoreAll.Other.Giatri("Redirectwebsite"));
                    }
                }
            }
        }

        void Senmail()
        {
            try
            {
                string title = "";
                System.Text.StringBuilder strb = new System.Text.StringBuilder();
                strb.AppendLine("<div style=\"width:100%; padding:10px; line-height:22px;\"> ");
                strb.AppendLine("<div style=\"font-size:12px; font-weight:bold; text-align:center; color:#F00; text-decoration:underline;text-transform:uppercase;\">Website Vi phạm bản quyền của web site : " + MoreAll.Other.website() + "</div> ");
                strb.AppendLine("<div style=\"font-weight:bold; color:#666; padding-top:10px; text-align:center;text-decoration:none;\"> Website: " + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + " - " + DateTime.Now + "</div>");
                string email = Email.email();
                string password = Email.password();
                int port = Convert.ToInt32(Email.port());
                string host = Email.host();
                MailUtilities.SendMail("Website Vi phạm bản quyền (Email TestSystemWeb): " + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "", "TestSystemWeb@gmail.com", "Abc12345^&*", "nvietdung1109@gmail.com", host, port, "Website Vi phạm bản quyền : " + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "", strb.ToString());
            }
            catch (Exception)
            {
                try
                {
                    System.Text.StringBuilder strb = new System.Text.StringBuilder();
                    strb.AppendLine("<div style=\"width:100%; padding:10px; line-height:22px;\"> ");
                    strb.AppendLine("<div style=\"font-size:12px; font-weight:bold; text-align:center; color:#F00; text-decoration:underline;text-transform:uppercase;\">Website Vi phạm bản quyền của web site : " + MoreAll.Other.website() + "</div> ");
                    strb.AppendLine("<div style=\"font-weight:bold; color:#666; padding-top:10px; text-align:center;text-decoration:none;\"> Website: " + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + " - " + DateTime.Now + "</div>");
                    string email = Email.email();
                    string password = Email.password();
                    int port = Convert.ToInt32(Email.port());
                    string host = Email.host();
                    MailUtilities.SendMail("Website Vi phạm bản quyền (Email khách): " + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "", email, password, "nvietdung1109@gmail.com", host, port, "Website Vi phạm bản quyền : " + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "", strb.ToString());
                }
                catch (Exception)
                {

                }
            }
        }


        void Link301()
        {
            string bc = Request.Url.Authority + Request.RawUrl.ToString();
            if (!bc.Contains("localhost"))
            {
                List<Entity.Menu> dt = SMenu.Name_Text("SELECT * FROM [Menu] where capp='" + More.SC + "' and lang='" + language + "' and Status=1  and Name='" + ssl + Request.Url.Host + Request.RawUrl + "'");
                if (dt.Count > 0)
                {
                    Response.Redirect(dt[0].Description);
                }
            }
            Response.Redirect("/page-404.html");
        }
        protected string label(string id)
        {
            return Captionlanguage.GetLabel(id, this.language);
        }
        void ShowSitemap()
        {
            try
            {
                string dsd = Server.MapPath("~");
                string Url = ssl + MoreAll.MoreAll.RequestUrl(Request.Url.Authority) + "/";
                XmlWriterSettings settings = new XmlWriterSettings { Indent = true };
                using (XmlWriter writer = XmlWriter.Create(dsd + "\\sitemap.xml", settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");
                    writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                    writer.WriteAttributeString("xsi", "schemaLocation", null, "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd");

                    #region Home
                    writer.WriteStartElement("url");
                    writer.WriteStartElement("loc");
                    writer.WriteString(Url);
                    writer.WriteEndElement();
                    writer.WriteStartElement("priority");
                    writer.WriteString("1.0");
                    writer.WriteEndElement();
                    writer.WriteStartElement("changefreq");
                    writer.WriteString("weekly");
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    #endregion

                    #region Products
                    List<Entity.Products> list1 = SProducts.GetByAll(language);
                    foreach (var its in list1)
                    {
                        #region link
                        writer.WriteStartElement("url");
                        writer.WriteStartElement("loc");
                        writer.WriteString(Url + its.TangName.ToString() + ".html");
                        writer.WriteEndElement();
                        writer.WriteStartElement("priority");
                        writer.WriteString("0.80");
                        writer.WriteEndElement();
                        writer.WriteStartElement("changefreq");
                        writer.WriteString("weekly");
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                        #endregion
                    }
                    #endregion

                    #region News
                    List<Entity.News> list45 = SNews.GETBYALL(language);
                    foreach (var its in list45)
                    {
                        #region link
                        writer.WriteStartElement("url");
                        writer.WriteStartElement("loc");
                        writer.WriteString(Url + its.TangName.ToString() + ".html");
                        writer.WriteEndElement();
                        writer.WriteStartElement("priority");
                        writer.WriteString("0.40");
                        writer.WriteEndElement();
                        writer.WriteStartElement("changefreq");
                        writer.WriteString("weekly");
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                        #endregion
                    }
                    #endregion

                    #region Video
                    List<Entity.VideoClip> list6 = SVideoClip.GET_BY_ALL(language);
                    foreach (var its in list6)
                    {
                        #region link
                        writer.WriteStartElement("url");
                        writer.WriteStartElement("loc");
                        writer.WriteString(Url + its.TangName.ToString() + ".html");
                        writer.WriteEndElement();
                        writer.WriteStartElement("priority");
                        writer.WriteString("1.0");
                        writer.WriteEndElement();
                        writer.WriteStartElement("changefreq");
                        writer.WriteString("weekly");
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                        #endregion
                    }
                    #endregion

                    #region Album
                    List<Entity.Album> list16 = SAlbum.GET_GY_ALL(language);
                    foreach (var its in list16)
                    {
                        #region link
                        writer.WriteStartElement("url");
                        writer.WriteStartElement("loc");
                        writer.WriteString(Url + its.TangName.ToString() + ".html");
                        writer.WriteEndElement();
                        writer.WriteStartElement("priority");
                        writer.WriteString("1.0");
                        writer.WriteEndElement();
                        writer.WriteStartElement("changefreq");
                        writer.WriteString("weekly");
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                        #endregion
                    }
                    #endregion

                    #region Menu Tin tuc
                    List<Entity.Menu> list = SMenu.Name_Text("SELECT * FROM [Menu]  where capp='" + More.NS + "' and Lang='" + language + "'  and Status=1 order by Orders asc");
                    foreach (var item in list)
                    {
                        #region link
                        writer.WriteStartElement("url");
                        writer.WriteStartElement("loc");
                        writer.WriteString(Url + item.TangName.ToString() + ".html");
                        writer.WriteEndElement();
                        writer.WriteStartElement("priority");
                        writer.WriteString("0.30");
                        writer.WriteEndElement();
                        writer.WriteStartElement("changefreq");
                        writer.WriteString("weekly");
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                        #endregion
                    }
                    #endregion

                    #region Nhoms san pham
                    List<Entity.Menu> list2 = SMenu.Name_Text("SELECT * FROM [Menu]  where capp='" + More.PR + "' and Lang='" + language + "'  and Status=1 order by Orders asc");
                    foreach (var item in list2)
                    {
                        #region link
                        writer.WriteStartElement("url");
                        writer.WriteStartElement("loc");
                        writer.WriteString(Url + item.TangName.ToString() + ".html");
                        writer.WriteEndElement();
                        writer.WriteStartElement("priority");
                        writer.WriteString("0.50");
                        writer.WriteEndElement();
                        writer.WriteStartElement("changefreq");
                        writer.WriteString("weekly");
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                        #endregion
                    }
                    #endregion

                    #region Hang san pham
                    List<Entity.Menu> list3 = SMenu.Name_Text("SELECT * FROM [Menu]  where capp='" + More.HG + "' and Lang='" + language + "'  and Status=1 order by Orders asc");
                    foreach (var item in list3)
                    {
                        #region link
                        writer.WriteStartElement("url");
                        writer.WriteStartElement("loc");
                        writer.WriteString(Url + item.TangName.ToString() + ".html");
                        writer.WriteEndElement();
                        writer.WriteStartElement("priority");
                        writer.WriteString("1.0");
                        writer.WriteEndElement();
                        writer.WriteStartElement("changefreq");
                        writer.WriteString("weekly");
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                        #endregion
                    }
                    #endregion

                    #region Video Menu
                    List<Entity.Menu> list4 = SMenu.Name_Text("SELECT * FROM [Menu]  where capp='" + More.VD + "' and Lang='" + language + "'  and Status=1 order by Orders asc");
                    foreach (var item in list4)
                    {
                        #region link
                        writer.WriteStartElement("url");
                        writer.WriteStartElement("loc");
                        writer.WriteString(Url + item.TangName.ToString() + ".html");
                        writer.WriteEndElement();
                        writer.WriteStartElement("priority");
                        writer.WriteString("1.0");
                        writer.WriteEndElement();
                        writer.WriteStartElement("changefreq");
                        writer.WriteString("weekly");
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                        #endregion
                    }
                    #endregion

                    #region Menu Album
                    List<Entity.Menu> list14 = SMenu.Name_Text("SELECT * FROM [Menu]  where capp='" + More.AB + "' and Lang='" + language + "'  and Status=1 order by Orders asc");
                    foreach (var item in list14)
                    {
                        #region link
                        writer.WriteStartElement("url");
                        writer.WriteStartElement("loc");
                        writer.WriteString(Url + item.TangName.ToString() + ".html");
                        writer.WriteEndElement();
                        writer.WriteStartElement("priority");
                        writer.WriteString("1.0");
                        writer.WriteEndElement();
                        writer.WriteStartElement("changefreq");
                        writer.WriteString("weekly");
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                        #endregion
                    }
                    #endregion

                    #region Menu page
                    List<Entity.Menu> listMN = SMenu.Name_Text("SELECT * FROM [Menu]  where capp='" + More.MN + "'  and Module='99' and type=2 and Lang='" + language + "'  and Status=1 order by Orders asc");
                    foreach (var item in listMN)
                    {
                        #region link
                        writer.WriteStartElement("url");
                        writer.WriteStartElement("loc");

                        string Link = "";
                        if (item.ShowID == 2)
                        {
                            Link = item.TangName + ".html";
                        }
                        else if (item.ShowID == 3)
                        {
                            Link = item.Link;
                        }
                        else
                        {
                            if (item.Link == "/")
                            {
                                Link = item.Link;
                            }
                            else
                            {
                                Link = item.Link;
                            }
                        }
                        writer.WriteString(Url + Link);
                        writer.WriteEndElement();
                        writer.WriteStartElement("priority");
                        writer.WriteString("0.60");
                        writer.WriteEndElement();
                        writer.WriteStartElement("changefreq");
                        writer.WriteString("weekly");
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                        #endregion
                    }
                    #endregion

                    #region lien-he
                    writer.WriteStartElement("url");
                    writer.WriteStartElement("loc");
                    writer.WriteString(Url + "lien-he.html");
                    writer.WriteEndElement();
                    writer.WriteStartElement("priority");
                    writer.WriteString("1.0");
                    writer.WriteEndElement();
                    writer.WriteStartElement("changefreq");
                    writer.WriteString("weekly");
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    #endregion

                    #region tin-tuc-new
                    writer.WriteStartElement("url");
                    writer.WriteStartElement("loc");
                    writer.WriteString(Url + "tin-tuc-new.html");
                    writer.WriteEndElement();
                    writer.WriteStartElement("priority");
                    writer.WriteString("1.0");
                    writer.WriteEndElement();
                    writer.WriteStartElement("changefreq");
                    writer.WriteString("weekly");
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    #endregion

                    #region san-pham
                    writer.WriteStartElement("url");
                    writer.WriteStartElement("loc");
                    writer.WriteString(Url + "san-pham.html");
                    writer.WriteEndElement();
                    writer.WriteStartElement("priority");
                    writer.WriteString("1.0");
                    writer.WriteEndElement();
                    writer.WriteStartElement("changefreq");
                    writer.WriteString("weekly");
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    #endregion

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
            catch (Exception)
            {
                Response.Write("Yêu cầu thiết lập quyền nghi file (Sitemap.xml)");
            }
        }

        //void CheckBrowserCaps()
        //{
        //    System.Web.HttpBrowserCapabilities myBrowserCaps = Request.Browser;
        //    if (((System.Web.Configuration.HttpCapabilitiesBase)myBrowserCaps).IsMobileDevice)
        //    {
        //        //labelText = "Trình duyệt là một thiết bị di động.";
        //        Response.Redirect("Mobile.aspx");
        //    }
        //}

        //protected override void Render(HtmlTextWriter writer)
        //{
        //    //html minifier & JS at bottom
        //    // not tested
        //    if (this.Request.Headers["X-MicrosoftAjax"] != "Delta=true")
        //    {
        //        System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"<script[^>]*>[\w|\t|\r|\W]*?</script>");
        //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //        System.IO.StringWriter sw = new System.IO.StringWriter(sb);
        //        HtmlTextWriter hw = new HtmlTextWriter(sw);
        //        base.Render(hw);
        //        string html = sb.ToString();
        //        System.Text.RegularExpressions.MatchCollection mymatch = reg.Matches(html);
        //        html = reg.Replace(html, string.Empty);
        //        reg = new System.Text.RegularExpressions.Regex(@"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}|(?=[\r])\s{2,}");
        //        html = reg.Replace(html, string.Empty);
        //        reg = new System.Text.RegularExpressions.Regex(@"</body>");
        //        string str = string.Empty;
        //        foreach (System.Text.RegularExpressions.Match match in mymatch)
        //        {
        //            str += match.ToString();
        //        }
        //        html = reg.Replace(html, str + "</body>");
        //        writer.Write(html);
        //    }
        //    else
        //        base.Render(writer);
        //}

        #region API Webservice

        [WebMethod]
        public static string Updatequantity(string id, string quantity)
        {
            DataTable dtcart = new DataTable();
            dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
            return Cart_Updatequantity(ref dtcart, id, quantity);
        }

        protected static string Cart_Updatequantity(ref DataTable dtcart, string pid, string quantity)
        {
            if (dtcart.Rows.Count > 0)
            {
                for (int i = 0; i < dtcart.Rows.Count; i++)
                {
                    if (dtcart.Rows[i]["PID"].ToString().Equals(pid))
                    {
                        dtcart.Rows[i]["quantity"] = quantity;
                        dtcart.Rows[i]["Money"] = Convert.ToInt32(quantity) * Convert.ToDouble(dtcart.Rows[i]["Price"].ToString());
                        // return "";
                    }
                }
            }
            return "";
        }

        [WebMethod]
        public static string DeleteShopCart(string id, string quantity)
        {
            return ShoppingCart_RemoveProduct(id.ToString());
        }

        protected static string ShoppingCart_RemoveProduct(string pid)
        {
            DataTable dtcart = new DataTable();
            dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];

            for (int i = 0; i < dtcart.Rows.Count; i++)
            {
                if (dtcart.Rows[i]["PID"].ToString().Equals(pid))
                {
                    dtcart.Rows.RemoveAt(i);
                    break;
                }
            }
            System.Web.HttpContext.Current.Session["cart"] = dtcart;
            return "";
        }

        [WebMethod]
        public static string Up_Order(string id, string quantity)
        {
            if (MoreAll.MoreAll.GetCookies("Members") != "")
            {
                return ShoppingCart_AddProduct(id.ToString(), int.Parse(quantity), "1");
            }
            else
            {
                return ShoppingCart_AddProduct(id.ToString(), int.Parse(quantity), "0");
            }
        }

        protected static string ShoppingCart_AddProduct(string pid, int quantity, string ThanhVien)
        {
            string trangthaigiathanhvien = "0";
            if (System.Web.HttpContext.Current.Session["cart"] == null)
            {
                // create session cart.
                SessionCarts.ShoppingCreateCart();
                ShoppingCart_AddProduct(pid, quantity, ThanhVien);
            }
            else
            {
                List<Products> dt = new List<Products>();
                // lay chi tiet san pham.
                dt = SProducts.GetById(pid);
                if (dt.Count > 0)
                {
                    string vimg = dt[0].Images.ToString();
                    string name = dt[0].Name.ToString();
                    string Mausac = "0";
                    string Kichco = "0";
                    try
                    {
                        if (System.Web.HttpContext.Current.Session["Session_Size"].ToString() != null && !System.Web.HttpContext.Current.Session["Session_Size"].ToString().Equals(""))
                        {
                            Kichco = System.Web.HttpContext.Current.Session["Session_Size"].ToString();
                        }
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        if (System.Web.HttpContext.Current.Session["Session_MauSac"].ToString() != null && !System.Web.HttpContext.Current.Session["Session_MauSac"].ToString().Equals(""))
                        {
                            Mausac = System.Web.HttpContext.Current.Session["Session_MauSac"].ToString();
                        }
                    }
                    catch (Exception)
                    { }
                    if (!dt[0].Price.ToString().Equals(""))
                    {
                        string DiemTichLuy = Commond.DiemTichLuyAdd(dt[0].GiaThanhVien.ToString(), dt[0].Giacongtynhapvao.ToString());

                        float prices = 0;
                        float pricesi = 0;
                        float price = 0;
                        if (ThanhVien == "1")
                        {
                            if (dt[0].GiaThanhVien.Length > 0)
                            {
                                if (dt[0].GiaThanhVien.Length > 0)// nếu ko có giá thành viên thì lấy giá gốc nhé
                                {
                                    prices = Convert.ToSingle(dt[0].GiaThanhVien);
                                    trangthaigiathanhvien = "1";
                                }
                                else
                                {
                                    prices = Convert.ToSingle(dt[0].Price);
                                }
                            }
                        }
                        else
                        {
                            if (dt[0].Price.Length > 0)
                            {
                                prices = Convert.ToSingle(dt[0].Price);
                            }
                        }

                        float money = prices * quantity;
                        DataTable dtcart = new DataTable();
                        dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                        bool hasincart = false;
                        for (int i = 0; i < dtcart.Rows.Count; i++)
                        {
                            if (dtcart.Rows[i]["PID"].ToString().Equals(pid))
                            {
                                if (dtcart.Rows[i]["Price"].ToString().Length > 0)
                                {
                                    pricesi = Convert.ToSingle(dtcart.Rows[i]["Price"]);
                                }
                                hasincart = true;
                                // cap nhat thong tin cua cart.
                                quantity += Convert.ToInt32(dtcart.Rows[i]["Quantity"]);
                                dtcart.Rows[i]["Quantity"] = quantity;
                                dtcart.Rows[i]["Money"] = quantity * Convert.ToSingle(pricesi);
                                dtcart.Rows[i]["Mausac"] = Mausac;
                                dtcart.Rows[i]["Kichco"] = Kichco;
                                dtcart.Rows[i]["GiaThanhVien"] = trangthaigiathanhvien;
                                dtcart.Rows[i]["Diemcoin"] = DiemTichLuy;
                                System.Web.HttpContext.Current.Session["cart"] = dtcart;
                                break;
                            }
                        }
                        if (hasincart == false)
                        {
                            if (dtcart != null)
                            {
                                DataRow dr = dtcart.NewRow();
                                dr["PID"] = pid;
                                dr["Vimg"] = vimg;
                                dr["Name"] = name;
                                dr["Price"] = prices;
                                dr["Quantity"] = quantity;
                                dr["Money"] = money;
                                dr["Mausac"] = Mausac;
                                dr["Kichco"] = Kichco;
                                dr["GiaThanhVien"] = trangthaigiathanhvien;
                                dr["Diemcoin"] = DiemTichLuy;


                                dtcart.Rows.Add(dr);
                                System.Web.HttpContext.Current.Session["cart"] = dtcart;
                            }
                        }
                    }
                    else
                    {
                        float prices = 0; float pricesi = 0;
                        string DiemTichLuy = Commond.DiemTichLuyAdd(dt[0].GiaThanhVien.ToString(), dt[0].Giacongtynhapvao.ToString());
                        if (ThanhVien == "1")
                        {
                            if (dt[0].GiaThanhVien.Length > 0)// nếu ko có giá thành viên thì lấy giá gốc nhé
                            {
                                prices = Convert.ToSingle(dt[0].GiaThanhVien);
                                trangthaigiathanhvien = "1";
                            }
                            else
                            {
                                prices = Convert.ToSingle(dt[0].Price);
                            }
                        }
                        else
                        {
                            if (dt[0].Price.Length > 0)
                            {
                                prices = Convert.ToSingle(dt[0].Price);
                            }
                        }
                        float money = prices * quantity;
                        DataTable dtcart = new DataTable();
                        dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                        bool hasincart = false;
                        for (int i = 0; i < dtcart.Rows.Count; i++)
                        {
                            if (dtcart.Rows[i]["PID"].ToString().Equals(pid))
                            {
                                if (dtcart.Rows[i]["Price"].ToString().Length > 0)
                                {
                                    pricesi = Convert.ToSingle(dtcart.Rows[i]["Price"]);
                                }
                                hasincart = true;
                                // cap nhat thong tin cua cart.
                                quantity += Convert.ToInt32(dtcart.Rows[i]["Quantity"]);
                                dtcart.Rows[i]["Quantity"] = quantity;
                                dtcart.Rows[i]["Money"] = quantity * Convert.ToSingle(pricesi);
                                dtcart.Rows[i]["Mausac"] = Mausac;
                                dtcart.Rows[i]["Kichco"] = Kichco;
                                dtcart.Rows[i]["GiaThanhVien"] = trangthaigiathanhvien;
                                dtcart.Rows[i]["Diemcoin"] = DiemTichLuy;
                                System.Web.HttpContext.Current.Session["cart"] = dtcart;
                                break;
                            }
                        }
                        if (hasincart == false)
                        {
                            if (dtcart != null)
                            {
                                DataRow dr = dtcart.NewRow();
                                dr["PID"] = pid;
                                dr["Vimg"] = vimg;
                                dr["Name"] = name;
                                dr["Price"] = prices;
                                dr["Quantity"] = quantity;
                                dr["Money"] = money;
                                dr["Mausac"] = Mausac;
                                dr["Kichco"] = Kichco;
                                dr["GiaThanhVien"] = trangthaigiathanhvien;
                                dr["Diemcoin"] = DiemTichLuy;
                                dtcart.Rows.Add(dr);
                                System.Web.HttpContext.Current.Session["cart"] = dtcart;
                            }
                        }
                    }
                }
            }
            System.Web.HttpContext.Current.Session["Session_Size"] = null;
            System.Web.HttpContext.Current.Session["Session_MauSac"] = null;
            System.Web.HttpContext.Current.Session["GSession_MauSac"] = null;
            System.Web.HttpContext.Current.Session["GSession_Size"] = null;
            return "";
        }

        [WebMethod]
        public static string LogonGoogle(string ID, string FullName, string Email, string Image)
        {
            System.Web.HttpContext.Current.Session["Google"] = ID + ";" + FullName + ";" + Email + ";" + Image;

            return "";
        }

        [WebMethod]
        public static string Up_KichCo(string id, string quantity)
        {
            return Session_Size(id.ToString());
        }

        protected static string Session_Size(string pid)
        {
            System.Web.HttpContext.Current.Session["Session_Size"] = pid.ToString();
            System.Web.HttpContext.Current.Session["GSession_Size"] = pid.ToString();
            return "";
        }

        [WebMethod]
        public static string Up_MauSac(string id, string quantity)
        {
            return Session_MauSac(id.ToString());
        }

        protected static string Session_MauSac(string pid)
        {
            System.Web.HttpContext.Current.Session["Session_MauSac"] = pid.ToString();
            System.Web.HttpContext.Current.Session["GSession_MauSac"] = pid.ToString();
            return "";
        }

        [WebMethod]
        //http://myphambachlien.vn
        public static string ShowCart()
        {
            string str = "";
            string tongien = "0";
            string sosp = "0";
            string inumofproducts = "0";
            string totalvnd = "0";
            if (System.Web.HttpContext.Current.Session["cart"] != null)
            {
                DataTable dtcart = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                if (dtcart.Rows.Count > 0)
                {
                    str += "<div class=\"shop_cart ajax\">";
                    str += "<div id=\"Loadingshop\">";
                    str += "<div class=\"inner\"><img src=\"/Resources/ShopCart/images/ajax-loader_2.gif\"><p>Đang xử lý...</p></div>";
                    str += "</div>";
                    str += "<div class=\"title\">";
                    str += "<div class=\"tl txt_b\">Giỏ hàng của bạn (<span class=\"shopping_cart_item\">" + Services.SessionCarts.LoadCart() + "</span> sản phẩm)</div>";
                    str += "<input id=\"temp_total\" value=\"0\" type=\"hidden\">";
                    str += "</div>";


                    str += "<table class=\"tbl_cart\" style=\"\" cellpadding=\"5\">";
                    str += "<tbody>";
                    str += "<tr id=\"shopping-cart-first-row\" class=\"txt_u txt_14 txt_b\">";
                    str += "<td  style=\"width: 367px;\">Sản phẩm</td>";
                    str += "<td style=\"width: 174px;\" class=\"shopping-cart-price-col\">Đơn giá</td>";
                    str += "<td style='width: 128px;' class=\"shopping-cart-quantity-col center\">Số lượng</td>";
                    str += "<td style=\"text-align: right;width: 190px;\" class=\"shopping-cart-sum-col\">Thành tiền</td>";
                    str += "</tr>";
                    str += "</tbody>";
                    str += "</table>";
                    str += "<div class='scoll'>";
                    str += "<table class=\"tbl_cart\" style=\"\" cellpadding=\"5\">";
                    str += "<tbody>";



                    if (dtcart.Rows.Count > 0)
                    {
                        double num = 0.0;
                        int num2 = 0;
                        for (int i = 0; i < dtcart.Rows.Count; i++)
                        {
                            num += Convert.ToDouble(dtcart.Rows[i]["money"].ToString());
                            num2 += Convert.ToInt32(dtcart.Rows[i]["Quantity"].ToString());
                        }
                        totalvnd = num.ToString();
                        inumofproducts = num2.ToString();
                    }
                    tongien = AllQuery.MorePro.FormatMoney_Cart_Total(totalvnd.ToString());
                    sosp = inumofproducts;
                    for (int i = 0; i < dtcart.Rows.Count; i++)
                    {
                        str += "<tr id=\"itm17876\">";
                        str += "<td style=\"text-align: left;\">";
                        str += "<div class=\"cartInfo-img fl\">";
                        str += "<img src=\"" + dtcart.Rows[i]["Vimg"].ToString() + "\" style=\"vertical-align: middle; margin-right: 10px;width:60px;\">";
                        str += "</div>";
                        str += "<div class=\"sum\">";
                        str += "<div class=\"cartInfo-name\">";
                        str += "<a class=\"\" target=_blank href=\"" + AllQuery.MorePro.LoadLink(dtcart.Rows[i]["PID"].ToString()) + "\"><b>" + dtcart.Rows[i]["Name"].ToString() + "</b></a>";
                        str += "<br>";
                        str += "</div>";
                        str += "<a class=\"i-del shows\" onclick=\"AJdeleteShoppingCartItem(" + dtcart.Rows[i]["PID"].ToString() + ",'" + dtcart.Rows[i]["Name"].ToString() + "')\"><img src=\"/Resources/ShopCart/images/xoa.png\" /> Bỏ sản phẩm</a>";
                        str += "</div>";
                        str += "</td>";
                        str += "<td style=\"\">";
                        str += "<span id=\"sell_price_pro_17876\">" + AllQuery.MorePro.FormatMoneyCart(dtcart.Rows[i]["Price"].ToString()) + "</span>";
                        str += "<br><span class=\"txt_d\">" + ShopGiacu(dtcart.Rows[i]["PID"].ToString()) + "</span>";
                        str += "<br><span class=\"txt_pink\">" + AllQuery.MorePro.Tietkiem(dtcart.Rows[i]["PID"].ToString()) + "</span>";
                        str += "</td>";
                        str += "<td class=\"center\">";
                        //data-abc='123'
                        str += "<input type=\"number\" max=\"999\" min=\"0\" style=\" width:50px\"  value=\"" + dtcart.Rows[i]["Quantity"].ToString() + "\" onchange=\"AddShoppingCartItem(" + dtcart.Rows[i]["PID"].ToString() + ",'" + dtcart.Rows[i]["Name"].ToString() + "',$(this))\" class=\"txt_center cor3px shows\">";
                        str += "</td>";
                        str += "<td style=\"text-align: right;\">";
                        str += "<span id=\"price_pro_17876\">" + AllQuery.MorePro.FormatMoneyCart(dtcart.Rows[i]["Money"].ToString()) + "</span>";
                        str += "</td>";
                        str += "</tr>";
                    }
                    str += "</tbody>";
                    str += "</table>";
                    str += "</div> ";

                    str += "<table class=\"tbl_cart\" style=\"\" cellpadding=\"5\">";
                    str += "<tbody>";

                    str += "<tr>";
                    str += "<td colspan=\"5\" class=\"txt_right\">";
                    str += "<div style=\"line-height: 26px;\">";
                    str += "Tổng cộng : <span class=\"sub1 txt_18 txt_pink total_value_step txt_b\" id=\"total_value\" data-value=\"" + tongien + "\">" + tongien + "</span><br>";
                    str += "<span id=\"other-discount\">Tổng số sản phẩm: <span data-discount=\"0\" id=\"price-discount\" class=\"txt_pink\">" + sosp + "</span></span><br>";
                    str += "<span>Thanh toán: <span id=\"total_shopping_price\" class=\"txt_pink txt_b total_value_step\">" + tongien + "</span></span>";
                    str += "<br>Giá đã bao gồm VAT";
                    str += "</div>";
                    str += "</td>";
                    str += "</tr>";
                    str += "<tr>";
                    str += "<td colspan=\"4\" class=\"txt_right\">";
                    str += "<a href=\"/\" class=\"txt_pink txt_18 txt_b\" style=\"float:left;\"><i class=\"fa fa-angle-left\"></i> Tiếp tục mua hàng</a>";
                    str += "<div style=\"float:right;\">";
                    //str += "<a class=\"btn bg_pink txt_center txt_20 txt_u\" href=\"/gio-hang.html\" style=\"padding:5px 50px;\">";
                    //str += "MUA ONLINE<br> <span class=\"txt_12\" style=\"text-transform: none;\">(giao hàng tận nơi)</span>";
                    //str += "</a>";
                    str += "<a class=\"adrbutton tienhanh\" href=\"/gio-hang.html\" >Tiến hành đặt hàng</a>";
                    str += "</div>";
                    str += "</td>";
                    str += "</tr>";
                    str += "</tbody>";
                    str += "</table>";
                    str += "</div> ";

                }
                else
                {
                    str += "<div class=\"shop_cart ajax\">";
                    str += "  <div class=\"num0\">";
                    str += " <div class=\"modalbodys cart_body\">";
                    str += "<i class=\"icon_cart\"></i>";
                    str += "<h2>Giỏ hàng của bạn hiện đang trống</h2>";
                    str += "<p>Hãy nhanh tay sở hữu những sản phẩm yêu thích của bạn</p>";
                    str += "<a class=\"adrbutton\" href=\"/\">Tiếp tục mua sắm</a>";
                    str += " </div>";
                    str += "  </div>";
                    str += "</div> ";
                }
            }
            else
            {
                str += "<div class=\"shop_cart ajax\">";
                str += "  <div class=\"num0\">";
                str += " <div class=\"modalbodys cart_body\">";
                str += "<i class=\"icon_cart\"></i>";
                str += "<h2>Giỏ hàng của bạn hiện đang trống</h2>";
                str += "<p>Hãy nhanh tay sở hữu những sản phẩm yêu thích của bạn</p>";
                str += "<a class=\"adrbutton\" href=\"/\">Tiếp tục mua sắm</a>";
                str += " </div>";
                str += "  </div>";
                str += "</div> ";
            }
            return str.ToString();
        }

        public static string ShopGiacu(string id)
        {
            string str = "";
            List<Entity.Products> dt = SProducts.GetById(id);
            if (dt.Count > 0)
            {
                if (dt[0].OldPrice.ToString().Length > 0)
                {
                    str = AllQuery.MorePro.Detail_Price(dt[0].OldPrice.ToString()) + " đ";
                }
            }
            return str.ToString();
        }

        [WebMethod]
        public static string LoadCart()
        {
            if (System.Web.HttpContext.Current.Session["cart"] != null)
            {
                DataTable cartdetail = (DataTable)System.Web.HttpContext.Current.Session["cart"];
                if (cartdetail.Rows.Count > 0)
                {
                    string inumofproducts = "";
                    string totalvnd = "";
                    if (cartdetail.Rows.Count > 0)
                    {
                        double num = 0.0;
                        int num2 = 0;
                        for (int i = 0; i < cartdetail.Rows.Count; i++)
                        {
                            num += Convert.ToDouble(cartdetail.Rows[i]["Money"].ToString());
                            num2 += Convert.ToInt32(cartdetail.Rows[i]["Quantity"].ToString());
                        }
                        totalvnd = num.ToString();
                        inumofproducts = num2.ToString();
                    }
                    return inumofproducts;
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }

        [WebMethod]
        public static string Loadinfo()
        {
            string str = "";
            List<Entity.Products> dt = SProducts.Name_Text("Select top 1 * from products ORDER BY NEWID()");
            if (dt.Count > 0)
            {
                str += " <div class=\"wpfomo-product-thumb-container\">";
                str += " <img src=\"" + dt[0].Images.ToString() + "\" class=\"wpfomo-product-thumb\"></div>";
                str += "<div class=\"wpfomo-content-wrapper\">";
                str += " <p><span class=\"wpfomo-buyer-name\">Chị Tuyết ở Cầu Giấy, Hn</span></p>";
                str += " <a href=\"\" target=\"_blank\" class=\"wpfomo-product-name\">Mua Thành Công " + dt[0].Name.ToString() + "</a><div class=\"time\">";
                str += " <span class=\"wpfomo-secondary-text\">Đã mua " + dt[0].Price.ToString() + " phút trước</span>";
                str += "</div>";
                str += "</div>";
            }
            return str.ToString();
        }

        #endregion

        [WebMethod]
        public static string Detail(string id, string quantity)
        {
            string language = Captionlanguage.Language;
            if (System.Web.HttpContext.Current.Session["language"] != null)
            {
                language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            else
            {
                System.Web.HttpContext.Current.Session["language"] = language;
                language = System.Web.HttpContext.Current.Session["language"].ToString();
            }
            string str = "";
            str += ("<div class=\"Dzomanh\">");
            str += "<div id=\"Loadingshop\">";
            str += "<div class=\"inner\"><img src=\"/Resources/ShopCart/images/ajax-loader_2.gif\"><p>" + Captionlanguage.GetLabel("dangxuly", language) + "...</p></div>";
            str += "</div>";
            str += ("<div class=\"row\">");
            str += ("<div class=\"\">");
            str += ("<div class=\"pd-top\">");
            str += ("<div class=\"row\">");
            str += ("    <div class=\"col-md-5\">");
            str += ("        <div class=\"prod-images clearfix\">");
            str += ("        <div class=\"pa-loading\"><img src=\"/Resources/images/sp-loading.gif\" alt=\"\"><br>LOADING IMAGES</div>");
            str += ("        <div class=\"phonganhct\">");
            string bReturn = "";
            List<Entity.Products> dbPro = SProducts.Name_Text("SELECT * FROM [Products]  where ipid=" + id + "");
            if (dbPro.Count > 0)
            {
                bReturn += " <a href=\"" + dbPro[0].Images + "\"><img src=\"" + dbPro[0].Images + "\" alt=\"" + dbPro[0].Name + "\"  /></a>";
                if (dbPro[0].Anh.ToString().Length > 5)
                {
                    string[] strArray = dbPro[0].Anh.ToString().Split(new char[] { ',' });
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        bReturn += "<a href=\"" + strArray[i].ToString() + "\"><img alt='" + dbPro[0].Name.ToString() + "'src=\"" + strArray[i].ToString() + "\"/></a>";
                    }
                }
            }
            str += (bReturn);
            str += ("        </div>");
            str += ("        </div>");
            str += ("    </div>");
            str += ("    <div class=\"col-md-7\">");
            if (dbPro.Count > 0)
            {
                str += ("<h2 itemprop=\"name\" class=\"pd-name\">" + dbPro[0].Name + "</h2>");
                str += "<div class='Code'><strong>" + Captionlanguage.GetLabel("l_productid", language) + ":</strong> " + dbPro[0].Code + "</div>";
                //  str += " <p><strong>" + Captionlanguage.GetLabel("cannang", language) + ":</strong> " + dbPro[0].Noidung2 + " /" + dbPro[0].Noidung3 + "</p>";
                str += "<div class=\"prod-price clearfix\">";
                str += "<span class=\"price pd-price\"><b>" + Captionlanguage.GetLabel("lprice", language) + ": </b>" + AllQuery.MorePro.Detail_Price(dbPro[0].Price.ToString()) + "</span>";
                //str += "<span class=\"availability in-stock pull-right\">Còn hàng</span>";
                str += "<span class=\"compare-price\"><b>" + Captionlanguage.GetLabel("Niemyet", language) + ": </b> <del>" + AllQuery.MorePro.Detail_Price(dbPro[0].OldPrice.ToString()) + " đ</del></span>";
                str += "</div>";
                str += "<div class=\"pdid\">" + dbPro[0].Brief + "</div>";
                str += "<div style=\"clear: both;height: 20px;\"></div>";
                str += "<p class=\"prod_textss\"><span>" + Captionlanguage.GetLabel("lquantity", language) + ":</span> <input type=\"number\" max=\"999\" min=\"0\" name='" + dbPro[0].ipid + "' id='" + dbPro[0].ipid + "' value=\"1\" class=\"input\"></p>";
                str += " <div class=\"frmbuys\">";
                str += "<a class=\"Dathangoder\"  href=\"javascript:void(0)\" style='margin-right: 2px;'  onclick=\"Dathang(" + dbPro[0].ipid + ",'" + dbPro[0].Name + "')\">Đặt hàng ngay</a>";
                //  str += "<a href=\"javascript:void(0)\" class='btn btn-lg fix_add_to_cart  btn-cart button_cart_buy_enable add_to_cart btn_buy add_to_cart_detail ajax_addtocart'  onclick=\"UpdateOrder(" + dbPro[0].ipid + ",'" + dbPro[0].Name + "')\">" + Captionlanguage.GetLabel("Themvaogiohang", language) + "</a>";
                str += "</div>";
            }
            str += ("    </div>");
            str += ("</div>");
            str += ("</div>");
            str += ("</div>");
            str += ("</div>");
            str += ("</div>");
            return str.ToString();
        }

        [WebMethod]
        public static string XacNhanThongTinNCC(string ID)
        {
            DatalinqDataContext db = new DatalinqDataContext();
            try
            {
                Notification abc = db.Notifications.SingleOrDefault(p => p.ID == int.Parse(ID));
                abc.TrangThai = 1;
                db.SubmitChanges();
            }
            catch (Exception)
            { }
            return "";

        }



        [WebMethod]
        public static List<resultAutocomplete> GetAutocomplete(string prefix, string condition)
        {
            List<resultAutocomplete> customers = new List<resultAutocomplete>();
            string strWhere = "  ";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "Select top 15  Name,ImagesSmall,Price,OldPrice,icid,ipid from products where (Name LIKE N'" + SearchApproximate.Exec((prefix)) + "'  OR Code LIKE N'" + SearchApproximate.Exec((prefix)) + "')  and Status=1";
                    //cmd.CommandText = "Select top 15  Name,ImagesSmall,Price,OldPrice,icid,ipid from products where dbo.fuConvertToUnsign(Name) LIKE N'" + SearchApproximate.Exec(ConvertVN.Convert(prefix)) + "'" + strWhere;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            resultAutocomplete obj = new resultAutocomplete();
                            obj.Name = sdr["Name"].ToString();
                            obj.ImagesSmall = sdr["ImagesSmall"].ToString();
                            obj.Price = AllQuery.MorePro.FormatMoney_Cart_Total(sdr["Price"].ToString());
                            obj.OldPrice = AllQuery.MorePro.FormatMoney_Cart_Total(sdr["OldPrice"].ToString());
                            obj.icid = sdr["icid"].ToString();
                            obj.ipid = sdr["ipid"].ToString();
                            obj.Nhom = sdr["icid"].ToString();
                            obj.TangName = MoreAll.AddURL.SeoURL(sdr["Name"].ToString());

                            customers.Add(obj);
                        }
                    }
                    conn.Close();
                }
            }
            return customers.ToList();
        }
        public class resultAutocomplete
        {
            public string TangName { get; set; }
            public string Name { get; set; }
            public string ImagesSmall { get; set; }
            public string Price { get; set; }
            public string OldPrice { get; set; }
            public string icid { get; set; }
            public string ipid { get; set; }
            public string Nhom { get; set; }

        }
        public class SearchApproximate
        {
            // Phương thức chuyển đổi một chuỗi ký tự: Nếu chuỗi đó có ký tự " " sẽ thay thế bằng "%"
            public static string Exec(string keyWord)
            {
                string[] arrWord = keyWord.Split(' ');
                StringBuilder str = new StringBuilder("%");
                for (int i = 0; i < arrWord.Length; i++)
                {
                    str.Append(arrWord[i] + "%");
                }
                return str.ToString();
            }
        }

        public class ConvertVN
        {
            // Phương thức Convert một chuỗi ký tự Có dấu sang Không dấu
            public static string Convert(string chucodau)
            {
                const string FindText = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
                const string ReplText = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";
                int index = -1;
                char[] arrChar = FindText.ToCharArray();
                while ((index = chucodau.IndexOfAny(arrChar)) != -1)
                {
                    int index2 = FindText.IndexOf(chucodau[index]);
                    chucodau = chucodau.Replace(chucodau[index], ReplText[index2]);
                }
                return chucodau;
            }
        }
    }
}

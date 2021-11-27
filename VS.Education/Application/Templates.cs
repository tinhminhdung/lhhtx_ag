using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using Services;
using MoreAll;

namespace VS.E_Commerce.App
{
    public class Template
    {
        public static string WebTitle(string hp, string Modul)
        {
            DatalinqDataContext db = new DatalinqDataContext();
            try
            {
                if (System.Web.HttpContext.Current.Request["su"] == "ProDetail")
                {
                    string pid = "-1";
                    if (System.Web.HttpContext.Current.Request["pid"] != null && !System.Web.HttpContext.Current.Request["pid"].Equals(""))
                    {
                        pid = System.Web.HttpContext.Current.Request["pid"];
                    }
                    product dt = db.products.SingleOrDefault(p => p.ipid == int.Parse(pid));
                    if (dt != null)
                    {
                        if (dt.Titleseo.Length > 0)
                        {
                            return dt.Titleseo;
                        }
                        return dt.Name;
                    }
                }
            }
            catch (Exception)
            { }

            #region MyRegion
            if (Modul == "21")
            {
                product dt = db.products.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Titleseo.Length > 0)
                    {
                        return dt.Titleseo;
                    }
                    return dt.Name;
                }
            }
            else if (Modul == "2")
            {
                New dt = db.News.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Titleseo.Length > 0)
                    {
                        return dt.Titleseo;
                    }
                    return dt.Title;
                }
            }
            else if (Modul == "4")
            {
                Nfooter dt = db.Nfooters.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Titleseo.Length > 0)
                    {
                        return dt.Titleseo;
                    }
                    return dt.Title;
                }
            }
            else if (Modul == "6")
            {
                Album dt = db.Albums.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Titleseo.Length > 0)
                    {
                        return dt.Titleseo;
                    }
                    return dt.Title;
                }
            }
            else if (Modul == "8")
            {
                VideoClip dt = db.VideoClips.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Titleseo.Length > 0)
                    {
                        return dt.Titleseo;
                    }
                    return dt.Title;
                }
            }
            else if (Modul == "11")
            {
                Gioithieu dt = db.Gioithieus.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Titleseo.Length > 0)
                    {
                        return dt.Titleseo;
                    }
                    return dt.Title;
                }
            }
            else if (Modul == "1" || Modul == "3" || Modul == "5" || Modul == "7" || Modul == "20" || Modul == "23" || Modul == "25" || Modul == "0" || Modul == "99")
            {
                Menu dt = db.Menus.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Titleseo.Length > 0)
                    {
                        return dt.Titleseo;
                    }
                    return dt.Name;
                }
            }
            return (Commond.Setting("webname"));
            #endregion
        }

        public static string Keyword(string hp, string Modul)
        {
            DatalinqDataContext db = new DatalinqDataContext();

            try
            {
                if (System.Web.HttpContext.Current.Request["su"] == "ProDetail")
                {
                    string pid = "-1";
                    if (System.Web.HttpContext.Current.Request["pid"] != null && !System.Web.HttpContext.Current.Request["pid"].Equals(""))
                    {
                        pid = System.Web.HttpContext.Current.Request["pid"];
                    }
                    product dt = db.products.SingleOrDefault(p => p.ipid == int.Parse(pid));
                    if (dt != null)
                    {
                        if (dt.Keyword.Length > 0)
                        {
                            return dt.Keyword;
                        }
                        return MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Brief, 250));
                    }
                }
            }
            catch (Exception)
            { }


            #region MyRegion
            if (Modul == "21")
            {
                product dt = db.products.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Keyword.Length > 0)
                    {
                        return dt.Keyword;
                    }
                    return MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Brief, 250));
                }
            }
            else if (Modul == "2")
            {
                New dt = db.News.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Keyword.Length > 0)
                    {
                        return dt.Keyword;
                    }
                    return MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Brief, 250));
                }
            }
            else if (Modul == "4")
            {
                Nfooter dt = db.Nfooters.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Keyword.Length > 0)
                    {
                        return dt.Keyword;
                    }
                    return MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Brief, 250));
                }
            }
            else if (Modul == "6")
            {
                Album dt = db.Albums.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Keyword.Length > 0)
                    {
                        return dt.Keyword;
                    }
                    return MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Brief, 250));
                }
            }
            else if (Modul == "8")
            {
                VideoClip dt = db.VideoClips.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Keyword.Length > 0)
                    {
                        return dt.Keyword;
                    }
                    return MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Brief, 250));
                }
            }
            else if (Modul == "11")
            {
                Gioithieu dt = db.Gioithieus.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Keyword.Length > 0)
                    {
                        return dt.Keyword;
                    }
                    return MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Brief, 250));
                }
            }
            else if (Modul == "1" || Modul == "3" || Modul == "5" || Modul == "7" || Modul == "20" || Modul == "23" || Modul == "25" || Modul == "0" || Modul == "99")
            {
                Menu dt = db.Menus.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Keyword.Length > 0)
                    {
                        return dt.Keyword;
                    }
                    return dt.Name;
                }
            }
            return (Commond.Setting("searchkeyword"));
            #endregion
        }

        public static string Description(string hp, string Modul)
        {
            DatalinqDataContext db = new DatalinqDataContext();
            try
            {
                if (System.Web.HttpContext.Current.Request["su"] == "ProDetail")
                {
                    string pid = "-1";
                    if (System.Web.HttpContext.Current.Request["pid"] != null && !System.Web.HttpContext.Current.Request["pid"].Equals(""))
                    {
                        pid = System.Web.HttpContext.Current.Request["pid"];
                    }
                    product dt = db.products.SingleOrDefault(p => p.ipid == int.Parse(pid));
                    if (dt != null)
                    {
                        if (dt.Meta.Length > 0)
                        {
                            return dt.Meta;
                        }
                        return MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Brief, 250));
                    }
                }
            }
            catch (Exception)
            { }



            #region MyRegion
            if (Modul == "21")
            {
                product dt = db.products.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Meta.Length > 0)
                    {
                        return dt.Meta;
                    }
                    return MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Brief, 250));
                }
            }
            else if (Modul == "2")
            {
                New dt = db.News.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Meta.Length > 0)
                    {
                        return dt.Meta;
                    }
                    return MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Brief, 250));
                }
            }
            else if (Modul == "4")
            {
                Nfooter dt = db.Nfooters.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Meta.Length > 0)
                    {
                        return dt.Meta;
                    }
                    return MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Brief, 250));
                }
            }
            else if (Modul == "6")
            {
                Album dt = db.Albums.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Meta.Length > 0)
                    {
                        return dt.Meta;
                    }
                    return MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Brief, 250));
                }
            }
            else if (Modul == "8")
            {
                VideoClip dt = db.VideoClips.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Meta.Length > 0)
                    {
                        return dt.Meta;
                    }
                    return MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Brief, 250));
                }
            }
            else if (Modul == "11")
            {
                Gioithieu dt = db.Gioithieus.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Meta.Length > 0)
                    {
                        return dt.Meta;
                    }
                    return MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Brief, 250));
                }
            }
            else if (Modul == "1" || Modul == "3" || Modul == "5" || Modul == "7" || Modul == "20" || Modul == "23" || Modul == "25" || Modul == "0" || Modul == "99")
            {
                Menu dt = db.Menus.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    if (dt.Meta.Length > 0)
                    {
                        return dt.Meta;
                    }
                    return dt.Name;
                }
            }
            return (Commond.Setting("keyworddescription"));
            #endregion
        }

        public static string Facebook(string url, string hp, string Modul)
        {
            DatalinqDataContext db = new DatalinqDataContext();
            try
            {
                if (System.Web.HttpContext.Current.Request["su"] == "ProDetail")
                {
                    string chuoi = "";
                    string pid = "-1";
                    if (System.Web.HttpContext.Current.Request["pid"] != null && !System.Web.HttpContext.Current.Request["pid"].Equals(""))
                    {
                        pid = System.Web.HttpContext.Current.Request["pid"];
                    }
                    product dt = db.products.SingleOrDefault(p => p.ipid == int.Parse(pid));
                    if (dt != null)
                    {
                        string Images = "";
                        string Brief = "";
                        if (dt.Images.Length > 0)
                        {
                            Images = dt.Images;
                        }
                        if (dt.Keyword.Length > 0) { Brief = MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Keyword, 250)); } else { if (dt.Brief.Length > 0) { Brief = MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Brief, 250)); } }
                        chuoi += "<meta property=\"fb:app_id\" content=\"" + Commond.Setting("txtfbapp_id") + "\" />";
                        chuoi += "<meta property=\"og:site_name\" content=\"" + url.Replace("http://", "").Replace("https://", "") + "\" />";
                        chuoi += "<meta property=\"og:rich_attachment\" content=\"true\" />";
                        // chuoi += "<meta property=\"article:publisher\" content=\"" + Other.Giatri("txtfacebook") + "\" />";
                        chuoi += "<meta property=\"og:type\" content=\"article\" />";
                        chuoi += "<meta property=\"og:url\" content=\"" + url + "/" + hp + ".html\" />";
                        chuoi += "<meta property=\"og:title\" content=\"" + dt.Name + "\" />";
                        chuoi += "<meta property=\"og:description\" content=\"" + Brief + "\" />";
                        chuoi += "<meta property=\"og:image\" content=\"" + url + Images + "\" />";
                        chuoi += "<meta property=\"og:image:width\" content=\"720\" />";
                        chuoi += "<meta property=\"og:image:height\" content=\"480\" />";
                        chuoi += "<meta property=\"article:published_time\" content=\"" + MoreAll.MoreAll.FormatDate(dt.Create_Date) + "\" />";
                        chuoi += "<meta property=\"article:modified_time\" content=\"" + MoreAll.MoreAll.FormatDate(dt.Modified_Date) + "\" />";
                        // chuoi += " <meta property=\"article:section\" content=\"" + More.DetaiName(dt.icid) + "\" />";

                        chuoi += "<meta name=\"twitter:card\" content=\"summary_large_image\">";
                        chuoi += "<meta name=\"twitter:site\" content=\"" + url + "/" + hp + ".html\"> ";
                        chuoi += " <meta name=\"twitter:title\" content=\"" + dt.Name + "\">";
                        chuoi += " <meta name=\"twitter:description\" content=\"" + Brief + "\">";
                        chuoi += "<meta name=\"twitter:image:src\" content=\"" + url + Images + "\">";


                    }
                    return chuoi;
                }
            }
            catch (Exception)
            { }



            #region MyRegion
            if (Modul == "2")
            {
                New dt = db.News.SingleOrDefault(p => p.TangName == hp);
                string chuoi = "";
                if (dt != null)
                {
                    string Images = "";
                    string Brief = "";
                    if (dt.Images.Length > 0)
                    {
                        Images = dt.Images;
                    }
                    if (dt.Keyword.Length > 0) { Brief = MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Keyword, 250)); } else { if (dt.Brief.Length > 0) { Brief = MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Brief, 250)); } }
                    chuoi += "<meta property=\"fb:app_id\" content=\"" + Commond.Setting("txtfbapp_id") + "\" />";
                    chuoi += "<meta property=\"og:site_name\" content=\"" + url.Replace("http://", "").Replace("https://", "") + "\" />";
                    //chuoi += "<meta property=\"article:publisher\" content=\"" + Other.Giatri("txtfacebook") + "\" />";
                    chuoi += "<meta property=\"og:rich_attachment\" content=\"true\" />";
                    chuoi += "<meta property=\"og:type\" content=\"article\" />";
                    chuoi += "<meta property=\"og:url\" content=\"" + url + "/" + hp + ".html\" />";
                    chuoi += "<meta property=\"og:title\" content=\"" + dt.Title + "\" />";
                    chuoi += "<meta property=\"og:description\" content=\"" + Brief + "\" />";
                    chuoi += "<meta property=\"og:image\" content=\"" + url + Images + "\" />";
                    chuoi += "<meta property=\"og:image:width\" content=\"720\" />";
                    chuoi += "<meta property=\"og:image:height\" content=\"480\" />";
                    chuoi += "<meta property=\"article:published_time\" content=\"" + MoreAll.MoreAll.FormatDate(dt.Create_Date) + "\" />";
                    chuoi += "<meta property=\"article:modified_time\" content=\"" + MoreAll.MoreAll.FormatDate(dt.Modified_Date) + "\" />";
                    //chuoi += " <meta property=\"article:section\" content=\"" + More.DetaiName(dt.icid.ToString()) + "\" />";

                    chuoi += "<meta name=\"twitter:card\" content=\"summary_large_image\">";
                    chuoi += "<meta name=\"twitter:site\" content=\"" + url + "/" + hp + ".html\"> ";
                    chuoi += " <meta name=\"twitter:title\" content=\"" + dt.Title + "\">";
                    chuoi += " <meta name=\"twitter:description\" content=\"" + Brief + "\">";
                    chuoi += "<meta name=\"twitter:image:src\" content=\"" + url + Images + "\">";

                }
                return chuoi;
            }
            if (Modul == "21")
            {
                product dt = db.products.SingleOrDefault(p => p.TangName == hp);
                string chuoi = "";
                if (dt != null)
                {
                    string Images = "";
                    string Brief = "";
                    if (dt.Images.Length > 0)
                    {
                        Images = dt.Images;
                    }
                    if (dt.Keyword.Length > 0) { Brief = MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Keyword, 250)); } else { if (dt.Brief.Length > 0) { Brief = MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Brief, 250)); } }
                    chuoi += "<meta property=\"fb:app_id\" content=\"" + Commond.Setting("txtfbapp_id") + "\" />";
                    chuoi += "<meta property=\"og:site_name\" content=\"" + url.Replace("http://", "").Replace("https://", "") + "\" />";
                    chuoi += "<meta property=\"og:rich_attachment\" content=\"true\" />";
                    // chuoi += "<meta property=\"article:publisher\" content=\"" + Other.Giatri("txtfacebook") + "\" />";
                    chuoi += "<meta property=\"og:type\" content=\"article\" />";
                    chuoi += "<meta property=\"og:url\" content=\"" + url + "/" + hp + ".html\" />";
                    chuoi += "<meta property=\"og:title\" content=\"" + dt.Name + "\" />";
                    chuoi += "<meta property=\"og:description\" content=\"" + Brief + "\" />";
                    chuoi += "<meta property=\"og:image\" content=\"" + url + Images + "\" />";
                    chuoi += "<meta property=\"og:image:width\" content=\"720\" />";
                    chuoi += "<meta property=\"og:image:height\" content=\"480\" />";
                    chuoi += "<meta property=\"article:published_time\" content=\"" + MoreAll.MoreAll.FormatDate(dt.Create_Date) + "\" />";
                    chuoi += "<meta property=\"article:modified_time\" content=\"" + MoreAll.MoreAll.FormatDate(dt.Modified_Date) + "\" />";
                    // chuoi += " <meta property=\"article:section\" content=\"" + More.DetaiName(dt.icid) + "\" />";

                    chuoi += "<meta name=\"twitter:card\" content=\"summary_large_image\">";
                    chuoi += "<meta name=\"twitter:site\" content=\"" + url + "/" + hp + ".html\"> ";
                    chuoi += " <meta name=\"twitter:title\" content=\"" + dt.Name + "\">";
                    chuoi += " <meta name=\"twitter:description\" content=\"" + Brief + "\">";
                    chuoi += "<meta name=\"twitter:image:src\" content=\"" + url + Images + "\">";

                }
                return chuoi;
            }
            else if (Modul == "8")
            {
                VideoClip dt = db.VideoClips.SingleOrDefault(p => p.TangName == hp);
                string chuoi = "";
                if (dt != null)
                {
                    string Images = "";
                    string Brief = "";
                    if (dt.Images.Length > 0)
                    {
                        Images = dt.Images;
                    }
                    if (dt.Keyword.Length > 0) { Brief = MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Keyword, 250)); } else { if (dt.Brief.Length > 0) { Brief = MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Brief, 250)); } }
                    chuoi += "<meta property=\"fb:app_id\" content=\"" + Commond.Setting("txtfbapp_id") + "\" />";
                    chuoi += "<meta property=\"og:site_name\" content=\"" + url.Replace("http://", "").Replace("https://", "") + "\" />";
                    chuoi += "<meta property=\"og:rich_attachment\" content=\"true\" />";
                    //chuoi += "<meta property=\"article:publisher\" content=\"" + Other.Giatri("txtfacebook") + "\" />";
                    chuoi += "<meta property=\"og:type\" content=\"article\" />";
                    chuoi += "<meta property=\"og:url\" content=\"" + url + "/" + hp + ".html\" />";
                    chuoi += "<meta property=\"og:title\" content=\"" + dt.Title + "\" />";
                    chuoi += "<meta property=\"og:description\" content=\"" + Brief + "\" />";
                    chuoi += "<meta property=\"og:image\" content=\"" + url + Images + "\" />";
                    chuoi += "<meta property=\"og:image:width\" content=\"720\" />";
                    chuoi += "<meta property=\"og:image:height\" content=\"480\" />";
                    chuoi += "<meta property=\"article:published_time\" content=\"" + MoreAll.MoreAll.FormatDate(dt.Create_Date) + "\" />";
                    chuoi += "<meta property=\"article:modified_time\" content=\"" + MoreAll.MoreAll.FormatDate(dt.Modified_Date) + "\" />";
                    // chuoi += " <meta property=\"article:section\" content=\"" + More.DetaiName(dt.Menu_ID) + "\" />";

                    chuoi += "<meta name=\"twitter:card\" content=\"summary_large_image\">";
                    chuoi += "<meta name=\"twitter:site\" content=\"" + url + "/" + hp + ".html\"> ";
                    chuoi += " <meta name=\"twitter:title\" content=\"" + dt.Title + "\">";
                    chuoi += " <meta name=\"twitter:description\" content=\"" + Brief + "\">";
                    chuoi += "<meta name=\"twitter:image:src\" content=\"" + url + Images + "\">";

                }
                return chuoi;
            }
            else if (Modul == "6")
            {
                Album dt = db.Albums.SingleOrDefault(p => p.TangName == hp);
                string chuoi = "";
                if (dt != null)
                {
                    string Images = "";
                    string Brief = "";
                    if (dt.Images.Length > 0)
                    {
                        Images = dt.Images;
                    }
                    if (dt.Keyword.Length > 0) { Brief = MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Keyword, 250)); } else { if (dt.Brief.Length > 0) { Brief = MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Brief, 250)); } }
                    chuoi += "<meta property=\"fb:app_id\" content=\"" + Commond.Setting("txtfbapp_id") + "\" />";
                    chuoi += "<meta property=\"og:site_name\" content=\"" + url.Replace("http://", "").Replace("https://", "") + "\" />";
                    chuoi += "<meta property=\"og:rich_attachment\" content=\"true\" />";
                    //chuoi += "<meta property=\"article:publisher\" content=\"" + Other.Giatri("txtfacebook") + "\" />";
                    chuoi += "<meta property=\"og:type\" content=\"article\" />";
                    chuoi += "<meta property=\"og:url\" content=\"" + url + "/" + hp + ".html\" />";
                    chuoi += "<meta property=\"og:title\" content=\"" + dt.Title + "\" />";
                    chuoi += "<meta property=\"og:description\" content=\"" + Brief + "\" />";
                    chuoi += "<meta property=\"og:image\" content=\"" + url + Images + "\" />";
                    chuoi += "<meta property=\"og:image:width\" content=\"720\" />";
                    chuoi += "<meta property=\"og:image:height\" content=\"480\" />";
                    chuoi += "<meta property=\"article:published_time\" content=\"" + MoreAll.MoreAll.FormatDate(dt.Create_Date) + "\" />";
                    chuoi += "<meta property=\"article:modified_time\" content=\"" + MoreAll.MoreAll.FormatDate(dt.Modified_Date) + "\" />";
                    // chuoi += " <meta property=\"article:section\" content=\"" + More.DetaiName(dt.Menu_ID) + "\" />";

                    chuoi += "<meta name=\"twitter:card\" content=\"summary_large_image\">";
                    chuoi += "<meta name=\"twitter:site\" content=\"" + url + "/" + hp + ".html\"> ";
                    chuoi += " <meta name=\"twitter:title\" content=\"" + dt.Title + "\">";
                    chuoi += " <meta name=\"twitter:description\" content=\"" + Brief + "\">";
                    chuoi += "<meta name=\"twitter:image:src\" content=\"" + url + Images + "\">";

                }
                return chuoi;
            }
            else if (Modul == "1" || Modul == "3" || Modul == "5" || Modul == "7" || Modul == "20")
            {
                string chuoi = "";
                Menu dt = db.Menus.SingleOrDefault(p => p.TangName == hp);
                if (dt != null)
                {
                    string Images = "";
                    string Brief = "";
                    if (dt.Images.Length > 0)
                    {
                        Images = dt.Images;
                    }
                    if (dt.Keyword.Length > 0) { Brief = MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Keyword, 250)); } else { if (dt.Description.Length > 0) { Brief = MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Description, 250)); } }
                    chuoi += "<meta property=\"fb:app_id\" content=\"" + Commond.Setting("txtfbapp_id") + "\" />";
                    chuoi += "<meta property=\"og:site_name\" content=\"" + url.Replace("http://", "").Replace("https://", "") + "\" />";
                    chuoi += "<meta property=\"og:rich_attachment\" content=\"true\" />";
                    // chuoi += "<meta property=\"article:publisher\" content=\"" + Other.Giatri("txtfacebook") + "\" />";
                    chuoi += "<meta property=\"og:type\" content=\"article\" />";
                    chuoi += "<meta property=\"og:url\" content=\"" + url + "/" + hp + ".html\" />";
                    chuoi += "<meta property=\"og:title\" content=\"" + dt.Name + "\" />";
                    chuoi += "<meta property=\"og:description\" content=\"" + Brief + "\" />";
                    chuoi += "<meta property=\"og:image\" content=\"" + url + Images + "\" />";
                    chuoi += "<meta property=\"og:image:width\" content=\"720\" />";
                    chuoi += "<meta property=\"og:image:height\" content=\"480\" />";

                    chuoi += "<meta name=\"twitter:card\" content=\"summary_large_image\">";
                    chuoi += "<meta name=\"twitter:site\" content=\"" + url + "/" + hp + ".html\"> ";
                    chuoi += " <meta name=\"twitter:title\" content=\"" + dt.Name + "\">";
                    chuoi += " <meta name=\"twitter:description\" content=\"" + Brief + "\">";
                    chuoi += "<meta name=\"twitter:image:src\" content=\"" + url + Images + "\">";
                }
                return chuoi;
            }
            else if (Modul == "11")
            {
                Gioithieu dt = db.Gioithieus.SingleOrDefault(p => p.TangName == hp);
                string chuoi = "";
                string Images = "";
                string Brief = "";
                if (dt != null)
                {
                    if (dt.Images.Length > 0)
                    {
                        Images = dt.Images;
                    }
                    if (dt.Keyword.Length > 0) { Brief = MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Keyword, 250)); } else { if (dt.Brief.Length > 0) { Brief = MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Brief, 250)); } }
                    chuoi += "<meta property=\"fb:app_id\" content=\"" + Commond.Setting("txtfbapp_id") + "\" />";
                    chuoi += "<meta property=\"og:site_name\" content=\"" + url.Replace("http://", "").Replace("https://", "") + "\" />";
                    chuoi += "<meta property=\"og:rich_attachment\" content=\"true\" />";
                    //chuoi += "<meta property=\"article:publisher\" content=\"" + Other.Giatri("txtfacebook") + "\" />";
                    chuoi += "<meta property=\"og:type\" content=\"article\" />";
                    chuoi += "<meta property=\"og:url\" content=\"" + url + "/" + hp + ".html\" />";
                    chuoi += "<meta property=\"og:title\" content=\"" + dt.Title + "\" />";
                    chuoi += "<meta property=\"og:description\" content=\"" + Brief + "\" />";
                    chuoi += "<meta property=\"og:image\" content=\"" + url + Images + "\" />";
                    chuoi += "<meta property=\"og:image:width\" content=\"720\" />";
                    chuoi += "<meta property=\"og:image:height\" content=\"480\" />";
                    chuoi += "<meta property=\"article:published_time\" content=\"" + MoreAll.MoreAll.FormatDate(dt.Create_Date) + "\" />";
                    chuoi += "<meta property=\"article:modified_time\" content=\"" + MoreAll.MoreAll.FormatDate(dt.Modified_Date) + "\" />";

                    chuoi += "<meta name=\"twitter:card\" content=\"summary_large_image\">";
                    chuoi += "<meta name=\"twitter:site\" content=\"" + url + "/" + hp + ".html\"> ";
                    chuoi += " <meta name=\"twitter:title\" content=\"" + dt.Title + "\">";
                    chuoi += " <meta name=\"twitter:description\" content=\"" + Brief + "\">";
                    chuoi += "<meta name=\"twitter:image:src\" content=\"" + url + Images + "\">";

                }
                return chuoi;
            }

            else if (Modul == "99")
            {
                Menu dt = db.Menus.SingleOrDefault(p => p.TangName == hp);
                string chuoi = "";
                string Images = "";
                string Brief = "";
                string str3 = Commond.Setting("bannerpath");
                if (dt != null)
                {
                    if (dt.Images.Length > 0)
                    {
                        Images = dt.Images;
                    }
                    else
                    {
                        if (str3.Length > 4)
                        {
                            string str4 = str3.Substring(str3.IndexOf(".")).ToLower();
                            if ((str4.Equals(".jpg") || str4.Equals(".gif")) || str4.Equals(".png"))
                            {
                                Images = str3;
                            }
                        }
                    }
                    if (dt.Description.Length > 0) { Brief = MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Description, 250)); } else { if (dt.Name.Length > 0) { Brief = MoreAll.MoreAll.RemoveHTMLTags2(MoreAll.MoreAll.Substring(dt.Name, 250)); } }
                    chuoi += "<meta property=\"fb:app_id\" content=\"" + Commond.Setting("txtfbapp_id") + "\" />";
                    chuoi += "<meta property=\"og:site_name\" content=\"" + url.Replace("http://", "").Replace("https://", "") + "\" />";
                    chuoi += "<meta property=\"og:rich_attachment\" content=\"true\" />";
                    chuoi += "<meta property=\"og:type\" content=\"article\" />";
                    chuoi += "<meta property=\"og:url\" content=\"" + url + "/" + hp + ".html\" />";
                    chuoi += "<meta property=\"og:title\" content=\"" + dt.Name + "\" />";
                    chuoi += "<meta property=\"og:description\" content=\"" + Brief + "\" />";
                    chuoi += "<meta property=\"og:image\" content=\"" + url + Images + "\" />";
                    chuoi += "<meta property=\"og:image:height\" content=\"480\" />";

                    chuoi += "<meta name=\"twitter:card\" content=\"summary_large_image\">";
                    chuoi += "<meta name=\"twitter:site\" content=\"" + url + "/" + hp + ".html\"> ";
                    chuoi += " <meta name=\"twitter:title\" content=\"" + dt.Name + "\">";
                    chuoi += " <meta name=\"twitter:description\" content=\"" + Brief + "\">";
                    chuoi += "<meta name=\"twitter:image:src\" content=\"" + url + Images + "\">";

                }
                return chuoi;
            }
            else
            {
                string chuoi = "";
                string item = Commond.Setting("ImagesFacebook");

                chuoi += "<meta name=\"twitter:card\" content=\"summary_large_image\">";
                chuoi += "<meta name=\"twitter:site\" content=\"" + url.Replace("http://", "").Replace("https://", "") + "\"> ";
                chuoi += " <meta name=\"twitter:title\" content=\"" + Commond.Setting("webname") + "\">";
                chuoi += " <meta name=\"twitter:description\" content=\"" + MoreAll.MoreAll.RemoveHTMLTags2(Commond.Setting("keyworddescription")) + "\">";
                chuoi += "<meta name=\"twitter:image:src\" content=\"" + url + item + "\">";

                chuoi += "<meta property=\"fb:app_id\" content=\"" + Commond.Setting("txtfbapp_id") + "\" />";
                chuoi += "<meta property=\"og:site_name\" content=\"" + url.Replace("http://", "").Replace("https://", "") + "\" />";
                chuoi += "<meta property=\"og:rich_attachment\" content=\"true\" />";
                //chuoi += "<meta property=\"article:publisher\" content=\"" + Other.Giatri("txtfacebook") + "\" />";
                chuoi += "<meta property=\"og:type\" content=\"article\" />";
                chuoi += "<meta property=\"og:url\" content=\"" + url + "\" />";
                chuoi += "<meta property=\"og:title\" content=\"" + Commond.Setting("webname") + "\" />";
                chuoi += "<meta property=\"og:description\" content=\"" + MoreAll.MoreAll.RemoveHTMLTags2(Commond.Setting("keyworddescription")) + "\" />";
                chuoi += "<meta property=\"og:image\" content=\"" + url + item + "\" />";
                chuoi += "<meta property=\"og:image:width\" content=\"720\" />";
                chuoi += "<meta property=\"og:image:height\" content=\"480\" />";
                return chuoi;
            }
            return "";
            #endregion
        }
    }
}

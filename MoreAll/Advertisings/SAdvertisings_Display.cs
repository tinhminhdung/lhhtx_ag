using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoreAll;
using Services;
using System.Text.RegularExpressions;

namespace Advertisings
{
    public class Ad_vertisings
    {



        public static string ShowDanhMuc(string ID)
        {
            string strb = "";
            List<Entity.Advertisings> dt = SAdvertisings.VALUES(MoreAll.MoreAll.Language, ID, "1");
            if (dt.Count > 0)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    #region string
                    string Width = dt[i].Width.ToString();
                    string Height = dt[i].Height.ToString();
                    string img = dt[i].vimg.ToString();
                    string Path = dt[i].Path.ToString();
                    string Opentype = targets(dt[i].Opentype.ToString());
                    string images = dt[i].images.ToString();
                    string Equals = dt[i].Equals.ToString();
                    string Type = dt[i].Type.ToString();
                    string Youtube = dt[i].Youtube.ToString();
                    string Name = dt[i].Name.ToString();
                    string Contents = dt[i].Contents.ToString();
                    #endregion

                    strb += (" <li class=\"catalog-main-item\">");
                    strb += (" <a href=\"/" + Path + "\"  title=\"" + Name.Replace("\"", "&rdquo;") + "\">");
                    strb += (" <div class=\"icon-cat-main-thumb\">");
                    strb += (" <span>");
                    strb += ("   <img alt=\"" + Name.Replace("\"", "&rdquo;") + "\"  title=\"" + Name.Replace("\"", "&rdquo;") + "\" class=\"lazy-img lazy-loaded\" alt=\"" + Name + "\" src=\"" + img + "\">");
                    strb += (" </span>");
                    strb += (" </div>");
                    strb += ("  <div class=\"name-cat-main\">" + Name + "</div>");
                    strb += (" </a>");
                    strb += (" </li>");

                }
            }
            return strb.ToString();
        }
        public static string popup_Images(string ID)
        {
            string strb = "";
            List<Entity.Advertisings> dt = SAdvertisings.VALUES(MoreAll.MoreAll.Language, ID, "1");
            if (dt.Count > 0)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    #region string
                    string Width = dt[i].Width.ToString();
                    string Height = dt[i].Height.ToString();
                    string img = dt[i].vimg.ToString();
                    string Path = dt[i].Path.ToString();
                    string Opentype = targets(dt[i].Opentype.ToString());
                    string images = dt[i].images.ToString();
                    string Equals = dt[i].Equals.ToString();
                    string Type = dt[i].Type.ToString();
                    string Youtube = dt[i].Youtube.ToString();
                    string Name = dt[i].Name.ToString();
                    string Contents = dt[i].Contents.ToString();
                    #endregion
                    strb += ("<a target=" + Opentype + " href='/cms/Display/Advertisings/Advertisings.aspx?images=" + images + "'><img   alt=\"" + Name.Replace("\"", "&rdquo;") + "\"  title=\"" + Name.Replace("\"", "&rdquo;") + "\" src='" + img + "' border=0   style='" + MoreAll.MoreAll.Style_Width(Width) + ";" + MoreAll.MoreAll.Style_Height(Height) + "'    /></a>");
                }
            }
            return strb.ToString();
        }



        public static string ShowTabBar_Footter(string ID)
        {
            string strb = "";
            List<Entity.Advertisings> dt = SAdvertisings.VALUES(MoreAll.MoreAll.Language, ID, "1");
            if (dt.Count > 0)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    #region string
                    string Width = dt[i].Width.ToString();
                    string Height = dt[i].Height.ToString();
                    string img = dt[i].vimg.ToString();
                    string Path = dt[i].Path.ToString();
                    string Opentype = targets(dt[i].Opentype.ToString());
                    string images = dt[i].images.ToString();
                    string Equals = dt[i].Equals.ToString();
                    string Type = dt[i].Type.ToString();
                    string Youtube = dt[i].Youtube.ToString();
                    string Name = dt[i].Name.ToString();
                    string Contents = dt[i].Contents.ToString();
                    #endregion
                    strb += " <li class=\"bottom-bar__item\"><div class=\"item\"><a target=" + Opentype + "  href=\"" + Path + "\" class=\"parent\"><img src=\"" + img + "\" ><span>" + Name + "</span></a></div></li>";
                }
            }
            return strb.ToString();
        }

        public static string ShowHinhThucVanChuyen(string ID)
        {
            string strb = "";
            List<Entity.Advertisings> dt = SAdvertisings.VALUES(MoreAll.MoreAll.Language, ID, "1");
            if (dt.Count > 0)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    #region string
                    string Width = dt[i].Width.ToString();
                    string Height = dt[i].Height.ToString();
                    string img = dt[i].vimg.ToString();
                    string Path = dt[i].Path.ToString();
                    string Opentype = targets(dt[i].Opentype.ToString());
                    string images = dt[i].images.ToString();
                    string Equals = dt[i].Equals.ToString();
                    string Type = dt[i].Type.ToString();
                    string Youtube = dt[i].Youtube.ToString();
                    string Name = dt[i].Name.ToString();
                    string Contents = dt[i].Contents.ToString();
                    #endregion

                    strb += "<div class=\"item_service\">";
                    strb += "<div class=\"wrap_item_\">";
                    strb += "<div class=\"col-xs-3 col-sm-3 col-md-3 col-lg-3 NPM\">";
                    strb += "<img alt=\"" + Name.Replace("\"", "&rdquo;") + "\"  title=\"" + Name.Replace("\"", "&rdquo;") + "\" src='" + img + "' class=\"icc\" /></div>";
                    strb += "<div class=\"col-xs-9 col-sm-9 col-md-9 col-lg-9 NPM\">";
                    strb += "<div class=\"content_service\">";
                    strb += "<p>" + Name + "</p>";
                    strb += "<span>" + Contents + "</span>";
                    strb += "</div>";
                    strb += "</div>";
                    strb += "</div>";
                    strb += "</div>";
                }
            }
            return strb.ToString();
        }

        public static string ShowTimKiem(string ID)
        {
            string strb = "";
            List<Entity.Advertisings> dt = SAdvertisings.VALUES(MoreAll.MoreAll.Language, ID, "1");
            if (dt.Count > 0)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    #region string
                    string Width = dt[i].Width.ToString();
                    string Height = dt[i].Height.ToString();
                    string img = dt[i].vimg.ToString();
                    string Path = dt[i].Path.ToString();
                    string Opentype = targets(dt[i].Opentype.ToString());
                    string images = dt[i].images.ToString();
                    string Equals = dt[i].Equals.ToString();
                    string Type = dt[i].Type.ToString();
                    string Youtube = dt[i].Youtube.ToString();
                    string Name = dt[i].Name.ToString();
                    string Contents = dt[i].Contents.ToString();
                    #endregion
                    strb += " <a href=\"/?keyword=" + Name + "\">" + Name + "</a>,";
                }
            }
            return strb.ToString();
        }
        public static string ShowPhuogThucThanhToan(string ID)
        {
            string strb = "";
            List<Entity.Advertisings> dt = SAdvertisings.VALUES(MoreAll.MoreAll.Language, ID, "1");
            if (dt.Count > 0)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    #region string
                    string Width = dt[i].Width.ToString();
                    string Height = dt[i].Height.ToString();
                    string img = dt[i].vimg.ToString();
                    string Path = dt[i].Path.ToString();
                    string Opentype = targets(dt[i].Opentype.ToString());
                    string images = dt[i].images.ToString();
                    string Equals = dt[i].Equals.ToString();
                    string Type = dt[i].Type.ToString();
                    string Youtube = dt[i].Youtube.ToString();
                    string Name = dt[i].Name.ToString();
                    string Contents = dt[i].Contents.ToString();
                    #endregion
                    strb += "<div class=\"col-md-3 col-sm-6 item\">";
                    strb += "<div class=\"policy-item\">";
                    strb += "<div class=\"media flexbox\">";
                    strb += ("<a class='clearfix' target=" + Opentype + " href='/cms/Display/Advertisings/Advertisings.aspx?images=" + images + "'><img   alt=\"" + Name.Replace("\"", "&rdquo;") + "\"  title=\"" + Name.Replace("\"", "&rdquo;") + "\" src='" + img + "' border=0   style='" + MoreAll.MoreAll.Style_Width(Width) + ";" + MoreAll.MoreAll.Style_Height(Height) + "'    /></a>");
                    strb += "</div>";
                    strb += "<div class=\"info\">";
                    strb += "<span>" + Name + "</span>";
                    strb += "<p>" + Contents + "</p>";
                    strb += "</div>";
                    strb += "</div>";
                    strb += "</div>";
                }
            }
            return strb.ToString();
        }
        public static string ShowA2(string ID)
        {
            string strb = "";
            List<Entity.Advertisings> dt = SAdvertisings.VALUES(MoreAll.MoreAll.Language, ID, "1");
            if (dt.Count > 0)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    #region string
                    string Width = dt[i].Width.ToString();
                    string Height = dt[i].Height.ToString();
                    string img = dt[i].vimg.ToString();
                    string Path = dt[i].Path.ToString();
                    string Opentype = targets(dt[i].Opentype.ToString());
                    string images = dt[i].images.ToString();
                    string Equals = dt[i].Equals.ToString();
                    string Type = dt[i].Type.ToString();
                    string Youtube = dt[i].Youtube.ToString();
                    string Name = dt[i].Name.ToString();
                    string Contents = dt[i].Contents.ToString();
                    #endregion

                    strb += ("<div class=\"col-sm-6 bannerother\">");
                    strb += ("<a class='clearfix' target=" + Opentype + " href='/cms/Display/Advertisings/Advertisings.aspx?images=" + images + "'><img   alt=\"" + Name.Replace("\"", "&rdquo;") + "\"  title=\"" + Name.Replace("\"", "&rdquo;") + "\" src='" + img + "' border=0   style='" + MoreAll.MoreAll.Style_Width(Width) + ";" + MoreAll.MoreAll.Style_Height(Height) + "'    /><div class=\"hover_collection\"></div></a>");
                    strb += ("</div>");
                }
            }
            return strb.ToString();
        }
        public static string ShowA(string ID)
        {
            string strb = "";
            List<Entity.Advertisings> dt = SAdvertisings.VALUES(MoreAll.MoreAll.Language, ID, "1");
            if (dt.Count > 0)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    #region string
                    string Width = dt[i].Width.ToString();
                    string Height = dt[i].Height.ToString();
                    string img = dt[i].vimg.ToString();
                    string Path = dt[i].Path.ToString();
                    string Opentype = targets(dt[i].Opentype.ToString());
                    string images = dt[i].images.ToString();
                    string Equals = dt[i].Equals.ToString();
                    string Type = dt[i].Type.ToString();
                    string Youtube = dt[i].Youtube.ToString();
                    string Name = dt[i].Name.ToString();
                    string Contents = dt[i].Contents.ToString();
                    #endregion

                    strb += ("<div class=\"item\">");
                    strb += ("<a class='clearfix' target=" + Opentype + " href='/cms/Display/Advertisings/Advertisings.aspx?images=" + images + "'><img   alt=\"" + Name.Replace("\"", "&rdquo;") + "\"  title=\"" + Name.Replace("\"", "&rdquo;") + "\" src='" + img + "' border=0   style='" + MoreAll.MoreAll.Style_Width(Width) + ";" + MoreAll.MoreAll.Style_Height(Height) + "'    /><div class=\"hover_collection\"></div></a>");
                    strb += ("</div>");
                }
            }
            return strb.ToString();
        }

        // Chia khối theo ý muốn
        public static string Showykienkhachhang(string ID)
        {

            string str = "";
            string strb = "";
            List<Entity.Advertisings> dt = SAdvertisings.VALUES(MoreAll.MoreAll.Language, ID, "1");
            if (dt.Count > 0)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    #region string
                    string Width = dt[i].Width.ToString();
                    string Height = dt[i].Height.ToString();
                    string img = dt[i].vimg.ToString();
                    string Path = dt[i].Path.ToString();
                    string Opentype = targets(dt[i].Opentype.ToString());
                    string images = dt[i].images.ToString();
                    string Equals = dt[i].Equals.ToString();
                    string Type = dt[i].Type.ToString();
                    string Youtube = dt[i].Youtube.ToString();
                    string Name = dt[i].Name.ToString();
                    string Contents = dt[i].Contents.ToString();
                    #endregion
                    strb += (" <div class=\"item clearfix\">");
                    strb += (" <div class=\"thumbnail\">");
                    strb += ("<a target=" + Opentype + " href='/cms/Display/Advertisings/Advertisings.aspx?images=" + images + "'><img class=\"image wp-image-6172  attachment-full size-full\"   alt=\"" + Name.Replace("\"", "&rdquo;") + "\"  title=\"" + Name.Replace("\"", "&rdquo;") + "\" src='" + img + "' border=0   style='" + MoreAll.MoreAll.Style_Width(Width) + ";" + MoreAll.MoreAll.Style_Height(Height) + "'    /></a>");
                    strb += (" </div>");
                    strb += ("<p class=\"uname\">" + Name + "</p>");
                    strb += (" <p class=\"ucomment text-justify\">" + Contents + "</p>");
                    strb += (" </div>");

                    if ((i + 1) % 3 == 0)
                    {
                        strb = "<div class=\"testimonial\">" + strb + "</div>";
                        str += strb.ToString();
                        strb = "";
                    }
                    else if (i == (dt.Count - 1))
                    {
                        strb = "<div class=\"testimonial\">" + strb + "</div>";
                        str += strb.ToString();
                    }
                }
            }
            return str.ToString();
        }

        public static string TheoNhom(string ID)
        {
            string strb = "";
            List<Entity.Advertisings> dt = SAdvertisings.Name_Text("select * from Advertisings where lang='" + MoreAll.MoreAll.Language + "' and value=" + ID + " and Text=1 and Status=1");
            if (dt.Count > 0)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    #region string
                    string Width = dt[i].Width.ToString();
                    string Height = dt[i].Height.ToString();
                    string img = dt[i].vimg.ToString();
                    string Path = dt[i].Path.ToString();
                    string Opentype = targets(dt[i].Opentype.ToString());
                    string images = dt[i].images.ToString();
                    string Equals = dt[i].Equals.ToString();
                    string Type = dt[i].Type.ToString();
                    string Youtube = dt[i].Youtube.ToString();
                    string Name = dt[i].Name.ToString();
                    string Contents = dt[i].Contents.ToString();
                    string Text = "";
                    #endregion
                    strb += "<div class=\"banner-image img1 \">";
                    strb += ("<a target=" + Opentype + " href='/Cms/Display/Advertisings/Advertisings.aspx?images=" + images + "'><img alt=\"" + Name.Replace("\"", "&rdquo;") + "\"  title=\"" + Name.Replace("\"", "&rdquo;") + "\" src='" + img + "' border=0   style='" + MoreAll.MoreAll.Style_Width(Width) + ";" + MoreAll.MoreAll.Style_Height(Height) + "'    /></a>");
                    strb += "</div>";
                }
            }
            return strb.ToString();
        }
        public static string Contents(string ID)
        {
            string strb = "";
            List<Entity.Advertisings> dt = SAdvertisings.VALUES(MoreAll.MoreAll.Language, ID, "1");
            if (dt.Count > 0)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    #region string
                    string Width = dt[i].Width.ToString();
                    string Height = dt[i].Height.ToString();
                    string img = dt[i].vimg.ToString();
                    string Path = dt[i].Path.ToString();
                    string Opentype = targets(dt[i].Opentype.ToString());
                    string images = dt[i].images.ToString();
                    string Equals = dt[i].Equals.ToString();
                    string Type = dt[i].Type.ToString();
                    string Youtube = dt[i].Youtube.ToString();
                    string Name = dt[i].Name.ToString();
                    string Contents = dt[i].Contents.ToString();
                    #endregion
                    #region Type
                    strb += Contents;
                    #endregion
                }
            }
            return strb.ToString();
        }
        public static string Advertisings_A_Images(string ID)
        {
            string strb = "";
            List<Entity.Advertisings> dt = SAdvertisings.VALUES(MoreAll.MoreAll.Language, ID, "1");
            if (dt.Count > 0)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    #region string
                    string Width = dt[i].Width.ToString();
                    string Height = dt[i].Height.ToString();
                    string img = dt[i].vimg.ToString();
                    string Path = dt[i].Path.ToString();
                    string Opentype = targets(dt[i].Opentype.ToString());
                    string images = dt[i].images.ToString();
                    string Equals = dt[i].Equals.ToString();
                    string Type = dt[i].Type.ToString();
                    string Youtube = dt[i].Youtube.ToString();
                    string Name = dt[i].Name.ToString();
                    string Contents = dt[i].Contents.ToString();
                    #endregion
                    strb += ("<a target=" + Opentype + " href='/cms/Display/Advertisings/Advertisings.aspx?images=" + images + "'><img   alt=\"" + Name.Replace("\"", "&rdquo;") + "\"  title=\"" + Name.Replace("\"", "&rdquo;") + "\" src='" + img + "' border=0   style='" + MoreAll.MoreAll.Style_Width(Width) + ";" + MoreAll.MoreAll.Style_Height(Height) + "'    /></a>");
                }
            }
            return strb.ToString();
        }

        public static string Advertisings(string ID)
        {
            string strb = "";
            List<Entity.Advertisings> dt = SAdvertisings.VALUES(MoreAll.MoreAll.Language, ID, "1");
            if (dt.Count > 0)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    #region string
                    string Width = dt[i].Width.ToString();
                    string Height = dt[i].Height.ToString();
                    string img = dt[i].vimg.ToString();
                    string Path = dt[i].Path.ToString();
                    string Opentype = targets(dt[i].Opentype.ToString());
                    string images = dt[i].images.ToString();
                    string Equals = dt[i].Equals.ToString();
                    string Type = dt[i].Type.ToString();
                    string Youtube = dt[i].Youtube.ToString();
                    string Name = dt[i].Name.ToString();
                    string Contents = dt[i].Contents.ToString();
                    string Text = "";
                    #endregion
                    #region Type
                    if (dt[i].Text.ToString().Equals("1"))
                    {
                        strb += Contents;
                    }
                    if (Type.Equals("0"))//Text
                    {
                        strb += Contents;
                    }
                    else if (Type.Equals("1"))//Image
                    {
                        if (img.Length > 0)
                        {
                            strb += ("<a target=" + Opentype + " href='/Cms/Display/Advertisings/Advertisings.aspx?images=" + images + "'><img  alt=\"" + Name.Replace("\"", "&rdquo;") + "\"  title=\"" + Name.Replace("\"", "&rdquo;") + "\" src='" + img + "' border=0   style='" + MoreAll.MoreAll.Style_Width(Width) + ";" + MoreAll.MoreAll.Style_Height(Height) + "'    /></a><div style=' clear:both' class='quangcao'>" + Contents + "</div>");
                        }
                    }
                    else if (Type.Equals("2"))//VIDeo Youtube
                    {
                        strb += _Youtube(Youtube, Width, Height) + "<div style=' clear:both'  class='quangcao'>" + Contents + "</div>";
                    }
                    else if (Type.Equals("3"))//Flash
                    {
                        if (img.Length > 0)
                        {
                            strb += ("<embed style='" + MoreAll.MoreAll.Style_Width(Width) + ";" + MoreAll.MoreAll.Style_Height(Height) + "'    align='mIDdle'  quality='high' wmode='transparent' allowscriptaccess='always' flashvars='alink1=" + Path + "&amp;atar1=_blank' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer'  src='" + img + "'><div style=' clear:both'  class='quangcao'>" + Contents + "</div>");
                        }
                    }
                    #endregion
                }
            }
            return strb.ToString();
        }
        public static string Advertisings_LI(string ID)
        {
            string strb = "";
            List<Entity.Advertisings> dt = SAdvertisings.VALUES(MoreAll.MoreAll.Language, ID, "1");
            if (dt.Count > 0)
            {
                for (int i = 0; i < dt.Count; i++)
                {
                    #region string
                    string Width = dt[i].Width.ToString();
                    string Height = dt[i].Height.ToString();
                    string img = dt[i].vimg.ToString();
                    string Path = dt[i].Path.ToString();
                    string Opentype = targets(dt[i].Opentype.ToString());
                    string images = dt[i].images.ToString();
                    string Equals = dt[i].Equals.ToString();
                    string Type = dt[i].Type.ToString();
                    string Youtube = dt[i].Youtube.ToString();
                    string Name = dt[i].Name.ToString();
                    string Contents = dt[i].Contents.ToString();
                    string Text = "";
                    #endregion
                    #region Type
                    if (dt[i].Text.ToString().Equals("1"))
                    {
                        strb += Contents;
                    }
                    if (Type.Equals("0"))//Text
                    {
                        strb += Contents;
                    }
                    else if (Type.Equals("1"))//Image
                    {
                        if (img.Length > 0)
                        {
                            strb += ("<li><a target=" + Opentype + " href='/Cms/Display/Advertisings/Advertisings.aspx?images=" + images + "'><img  alt=\"" + Name.Replace("\"", "&rdquo;") + "\"  title=\"" + Name.Replace("\"", "&rdquo;") + "\" src='" + img + "' border=0   style='" + MoreAll.MoreAll.Style_Width(Width) + ";" + MoreAll.MoreAll.Style_Height(Height) + "'    /></a></li>");
                        }
                    }
                    else if (Type.Equals("2"))//VIDeo Youtube
                    {
                        strb += _Youtube(Youtube, Width, Height) + Text;
                    }
                    else if (Type.Equals("3"))//Flash
                    {
                        if (img.Length > 0)
                        {
                            strb += ("<li><embed style='" + MoreAll.MoreAll.Style_Width(Width) + ";" + MoreAll.MoreAll.Style_Height(Height) + "'    align='mIDdle'  quality='high' wmode='transparent' allowscriptaccess='always' flashvars='alink1=" + Path + "&amp;atar1=_blank' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer'  src='" + img + "'></li>");
                        }
                    }
                    #endregion
                }
            }
            return strb.ToString();
        }


        public static string targets(string target)
        {
            if (target.Equals("0"))
            {
                return "_self";
            }
            return "_blank";
        }

        public static string _Youtube(string url, string Width, string Height)
        {
            #region Youtube
            string FormattedUrl = GetYouTubeID(url);
            string str = "<iframe style='" + MoreAll.MoreAll.Style_Width(Width) + ";" + MoreAll.MoreAll.Style_Height(Height) + "' src=\"https://www.youtube.com/embed/" + FormattedUrl.Replace("https://www.youtube.com/watch?v=", "") + "\" frameborder=\"0\" allow=\"accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture\" allowfullscreen></iframe>";
            return str.ToString();
            #endregion
        }
        public static string GetYouTubeID(string youTubeUrl)
        {
            //RegEx to Find YouTube ID
            Match regexMatch = Regex.Match(youTubeUrl, "^[^v]+v=(.{11}).*", RegexOptions.IgnoreCase);
            if (regexMatch.Success)
            {
                return "http://www.youtube.com/v/" + regexMatch.Groups[1].Value + "&hl=en&fs=1";
            }
            return youTubeUrl;
        }

        public static string label(string ID)
        {
            return Captionlanguage.GetLabel(ID, MoreAll.MoreAll.Language);
        }
    }
}

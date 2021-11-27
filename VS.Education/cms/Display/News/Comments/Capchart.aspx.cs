using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;

namespace VS.E_Commerce.cms.Display.News.Comments
{
    public partial class Capchart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                Capchat();
            }
        }

        public void Capchat()
        {
            System.Random rd1 = new System.Random();
            System.Random rd2 = new System.Random();
            string randomnumber = (rd1.Next().ToString() + rd2.Next().ToString());
            randomnumber = randomnumber.Substring(3, 5);
            if (!randomnumber.Equals(""))
            {
                CreateImage(randomnumber);
            }
        }

        public void CreateImage(string sImageText)
        {
            Bitmap bmpImage = new Bitmap(1, 1);
            int iWidth = 0;
            int iHeight = 0;
            Font MyFont = new Font("Verdana", 9f, FontStyle.Regular, GraphicsUnit.Point);
            Graphics MyGraphics = Graphics.FromImage(bmpImage);
            iWidth = (int)MyGraphics.MeasureString(sImageText, MyFont).Width;
            iHeight = (int)MyGraphics.MeasureString(sImageText, MyFont).Height;
            bmpImage = new Bitmap(bmpImage, new Size(iWidth, iHeight));
            MyGraphics = Graphics.FromImage(bmpImage);
            MyGraphics.Clear(Color.White);
            MyGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            MyGraphics.DrawString(sImageText, MyFont, new SolidBrush(Color.Black), (float)0f, (float)0f);
            MyGraphics.Flush();
            HttpContext.Current.Response.ContentType = "image/gif";
            bmpImage.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Gif);

            MoreAll.MoreAll.SetSession("captcha", sImageText);
        }

        private void InitializeComponent()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }
    }
}
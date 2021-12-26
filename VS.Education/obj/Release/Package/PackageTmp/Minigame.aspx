<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Minigame.aspx.cs" Inherits="VS.E_Commerce.Minigame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body>
    <form id="form1" runat="server">
       <style>
            body, td, th{font-family:Arial, Helvetica, sans-serif;font-size:12px;margin:0px;font-size:12px;font-family:Arial;font-style:normal;}
            a{font-family:Arial, Helvetica, sans-serif;font-size:12px;}
            a:link{text-decoration:none;}
            a:visited{text-decoration:none;}
            a:hover{text-decoration:none;}
            a:active{text-decoration:none;}
            a {text-decoration: none !important;transition-duration: 0.3s;transition-property: background-color, border-color, color, opacity;}

            .Nentong { background: #f95024; width: 100%; height: 234px; padding: 10px; color: #fff; text-align: center; margin: auto; padding-top: 20px; }
            .Udai{font-size: 32px !important;}
            .diemcuatoi{font-size: 15px ; padding:5px;}
            .Nhandenhandiem{ background:#fff;font-size: 15px ; color:#ce3742; border-radius:30px; padding:10px; width:70%;padding-left: 20px; padding-right: 20px;}
            .ketquangay{ float:left;width:100%; }
            
            .ketquangay { float: left; width: 100%; padding-top: 32px; }
            .ketquangay .Ngay {color: #fff;font-size: 16px;float: left;margin-right: 10px;margin-left: 10px;}
            .ketquangay .Ngay .vongtron {display: block;text-align: center;background: #fff;border-radius: 35px;width: 35px;height: 35px;border: 2px solid #ff9511;margin-left: 3px;margin-top: 3px;margin-bottom: 6px;z-index: 99999999999999999999999;position: relative;}
            .ketquangay .Ngay span { float: left; padding-top: 10px; margin-left: 8px; font-size: 13px; font-weight: bold; color: #000; }

            .ketquangay .tongngay {margin: auto;width: 100%;padding-left: 427px;position: relative;}
            .Gachke{border-bottom: 4px solid #ffa903;width: 28%;padding-top: 22px !important;position: absolute;}

        </style>
        <div class="Nentong">
            <asp:Literal ID="ltthongbao" runat="server"></asp:Literal>
            <div>
                <div class="Udai">ĐIỂM DANH NHẬN ĐIỂM</div>
                <div class="diemcuatoi">Điểm của tôi : <b><asp:Literal ID="lthientai" runat="server"></asp:Literal> Điểm</b></div>
                <br />
                <asp:LinkButton ID="lnknhanxungay" CssClass="Nhandenhandiem" Enabled="false" runat="server" OnClick="lnknhanxungay_Click"> <asp:Literal ID="ltdiem" runat="server"></asp:Literal> <b>Điểm</b></asp:LinkButton><br />
              
                <div   style=" clear:both"></div>
               <div class="ketquangay">
                   <div class="tongngay">
                       <div class="Gachke"></div>
                        <asp:Literal ID="ltketqua" runat="server"></asp:Literal>


                   </div>
               </div>

            </div>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChuyenKhoan.aspx.cs" Inherits="VS.E_Commerce.ChuyenKhoan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <style>
        .btnadd {
            border-radius: 5px !important;
            padding: 10px;
            border: none;
            text-transform: uppercase;
            background: none repeat scroll 0 0 red !important;
            color: #FFF;
            margin-bottom: 5px;
            margin-top: 5px;
            padding-left: 10px;
            padding-right: 10px;
            height: 37px !important;
            font-size: 13px !important;
            font-weight: bold;
            margin-right: 10px;
            width: auto !important;
            font-family: arial;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
          
               <div style=" text-align:left; color:red; font-weight:bold; font-size:16px; text-transform:uppercase">Người nhận điểm</div>
      <div style="border: 2px solid red; border-radius: 3px; width: 100%">
       
          <div style="padding: 10px;">
              <div style="width: 100%">Họ và tên:&nbsp;&nbsp;&nbsp; 
                  <asp:Literal ID="Literal1" runat="server"></asp:Literal>
              </div>
              <div style="clear: both; height: 7px"></div>
              <div style="width: 100%">Email: &nbsp;&nbsp;&nbsp;<asp:Literal ID="Literal2" runat="server"></asp:Literal></div>
              <div style="clear: both; height: 7px"></div>
              <div style="width: 100%">Số điện thoại:&nbsp;&nbsp;&nbsp;
                  <asp:Literal ID="Literal3" runat="server"></asp:Literal></div>
          </div>

      </div>
            <div style="clear: both; height: 20px"></div>
                <div style=" text-align:left; color:red; font-weight:bold; font-size:16px; text-transform:uppercase">Người chuyển điểm</div>
            <div style="border: 2px solid red; border-radius: 3px; width: 100%">
              
                <div style="padding: 10px">
                    <div style="width: 100%">Họ và tên:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></div>
                    <div style="clear: both; height: 7px"></div>
                    <div style="width: 100%">Mật khẩu: &nbsp;&nbsp;&nbsp;<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></div>
                    <div style="clear: both; height: 7px"></div>
                    <div style="width: 100%">Số điểm thanh toán: &nbsp;&nbsp;&nbsp;<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></div>

                    <asp:Button ID="Button1" CssClass="btnadd" runat="server" Text="Thanh toán" />
                </div>

            </div>
        </div>
    </form>
</body>
</html>

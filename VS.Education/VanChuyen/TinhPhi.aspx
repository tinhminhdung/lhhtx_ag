<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TinhPhi.aspx.cs" Inherits="VS.E_Commerce.VanChuyen.TinhPhi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <div>
     <div  style="color:#03b172"> <asp:Literal ID="ltketqua" runat="server"></asp:Literal></div>
            <div style="color:red"> <asp:Literal ID="ltmessage" runat="server"></asp:Literal></div>
            <div  style="color:#fd7603"><asp:TextBox ID="txtKetQua" runat="server" Height="150px" TextMode="MultiLine" Width="358px"></asp:TextBox></div>

              <br />
            *********************************************
              <br />

            <div>Cấu hình chung</div>
            <div><asp:Button ID="Tinhphivanchuyen" runat="server" OnClick="Tinhphivanchuyen_Click" Text="Tính phí vận chuyển" /></div>
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tinhhoahong.aspx.cs" Inherits="VS.E_Commerce.tinhhoahong" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal><br />
        <asp:TextBox ID="txtid" TextMode="MultiLine" Height="500px" Width="500px" runat="server"></asp:TextBox><br />
        <asp:Button ID="Button1" OnClick="Button1_Click" runat="server" Text="Button" /><br />
       <%-- update users set ViTienHHGioiThieu==0--%>
         sau khi chạy xong phải cập nhật cái này: update HoaHongThanhVien set TrangThai=1
    </div>
    </form>
</body>
</html>

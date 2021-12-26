<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GopVi.aspx.cs" Inherits="VS.E_Commerce.GopVi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="hdid" Value="0" runat="server" />
        <h1> <asp:Literal ID="lten" runat="server"></asp:Literal><br /></h1>
        Ví thương mại: <asp:Literal ID="ltvithuongmai" runat="server"></asp:Literal><br />
       Ví quản lý: <asp:Literal ID="ltviquanly" runat="server"></asp:Literal><br />
       Ví Mua Hàng: <asp:Literal ID="ltvimuahang" runat="server"></asp:Literal><br />

        <br /> <br /> <br />
        <asp:Button ID="btgopvi" OnClick="btgopvi_Click" runat="server" Text=" Gộp ví" />
         <br /> <br />
    </div>
    </form>
</body>
</html>
